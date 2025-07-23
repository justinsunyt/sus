// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

#nullable disable

using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using osu.Framework.Allocation;
using osu.Framework.Audio;
using osu.Framework.Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Cursor;
using osu.Framework.Platform;
using osu.Framework.Testing;
using osu.Framework.Utils;
using sus.Game.Beatmaps;
using sus.Game.Database;
using sus.Game.Graphics.Cursor;
using sus.Game.Graphics.UserInterface;
using sus.Game.Models;
using sus.Game.Online.API.Requests.Responses;
using sus.Game.Online.Leaderboards;
using sus.Game.Overlays;
using sus.Game.Rulesets;
using sus.Game.Rulesets.Osu;
using sus.Game.Scoring;
using sus.Game.Screens.Select.Leaderboards;
using sus.Game.Tests.Resources;
using osuTK;
using osuTK.Input;

namespace sus.Game.Tests.Visual.UserInterface
{
    public partial class TestSceneDeleteLocalScore : OsuManualInputManagerTestScene
    {
        private readonly ContextMenuContainer contextMenuContainer;
        private readonly BeatmapLeaderboard leaderboard;

        private BeatmapManager beatmapManager;
        private ScoreManager scoreManager;

        private readonly List<ScoreInfo> importedScores = new List<ScoreInfo>();

        private BeatmapInfo beatmapInfo;

        [Cached(typeof(IDialogOverlay))]
        private readonly DialogOverlay dialogOverlay;

        public TestSceneDeleteLocalScore()
        {
            Children = new Drawable[]
            {
                contextMenuContainer = new OsuContextMenuContainer
                {
                    RelativeSizeAxes = Axes.Both,
                    Child = leaderboard = new BeatmapLeaderboard
                    {
                        Origin = Anchor.Centre,
                        Anchor = Anchor.Centre,
                        Size = new Vector2(550f, 450f),
                        Scope = BeatmapLeaderboardScope.Local,
                        BeatmapInfo = TestResources.CreateTestBeatmapSetInfo().Beatmaps.First()
                    }
                },
                dialogOverlay = new DialogOverlay()
            };
        }

        protected override IReadOnlyDependencyContainer CreateChildDependencies(IReadOnlyDependencyContainer parent)
        {
            var dependencies = new DependencyContainer(base.CreateChildDependencies(parent));

            dependencies.Cache(new RealmRulesetStore(Realm));
            dependencies.Cache(beatmapManager = new BeatmapManager(LocalStorage, Realm, null, dependencies.Get<AudioManager>(), Resources, dependencies.Get<GameHost>(), Beatmap.Default));
            dependencies.Cache(scoreManager = new ScoreManager(dependencies.Get<RulesetStore>(), () => beatmapManager, LocalStorage, Realm, API));
            Dependencies.Cache(Realm);

            return dependencies;
        }

        [BackgroundDependencyLoader]
        private void load() => Schedule(() =>
        {
            var imported = beatmapManager.Import(new ImportTask(TestResources.GetQuickTestBeatmapForImport())).GetResultSafely();

            imported?.PerformRead(s =>
            {
                beatmapInfo = s.Beatmaps[0];

                for (int i = 0; i < 50; i++)
                {
                    var score = new ScoreInfo
                    {
                        OnlineID = i,
                        BeatmapInfo = beatmapInfo,
                        BeatmapHash = beatmapInfo.Hash,
                        Accuracy = RNG.NextDouble(),
                        TotalScore = RNG.Next(1, 1000000),
                        MaxCombo = RNG.Next(1, 1000),
                        Rank = ScoreRank.XH,
                        User = new APIUser { Username = "TestUser" },
                        Ruleset = new OsuRuleset().RulesetInfo,
                        Files = { new RealmNamedFileUsage(new RealmFile { Hash = $"{i}" }, string.Empty) }
                    };

                    importedScores.Add(scoreManager.Import(score)!.Value);
                }
            });
        });

        [SetUpSteps]
        public void SetupSteps()
        {
            AddUntilStep("ensure scores imported", () => importedScores.Count == 50);
            AddStep("undelete scores", () =>
            {
                Realm.Run(r =>
                {
                    // Due to soft deletions, we can re-use deleted scores between test runs
                    scoreManager.Undelete(r.All<ScoreInfo>().Where(s => s.DeletePending).ToList());
                });
            });
            AddStep("set up leaderboard", () =>
            {
                leaderboard.BeatmapInfo = beatmapInfo;
                leaderboard.RefetchScores(); // Required in the case that the beatmap hasn't changed
            });

            // Ensure the leaderboard items have finished showing up
            AddStep("finish transforms", () => leaderboard.FinishTransforms(true));
            AddUntilStep("wait for drawables", () => leaderboard.ChildrenOfType<LeaderboardScore>().Any());
        }

        [Test]
        public void TestDeleteViaRightClick()
        {
            ScoreInfo scoreBeingDeleted = null;
            AddStep("open menu for top score", () =>
            {
                var leaderboardScore = leaderboard.ChildrenOfType<LeaderboardScore>().First();

                scoreBeingDeleted = leaderboardScore.Score;

                InputManager.MoveMouseTo(leaderboardScore);
                InputManager.Click(MouseButton.Right);
            });

            // Ensure the context menu has finished showing
            AddStep("finish transforms", () => contextMenuContainer.FinishTransforms(true));

            AddStep("click delete option", () =>
            {
                InputManager.MoveMouseTo(contextMenuContainer.ChildrenOfType<DrawableOsuMenuItem>().First(i => string.Equals(i.Item.Text.Value.ToString(), "delete", System.StringComparison.OrdinalIgnoreCase)));
                InputManager.Click(MouseButton.Left);
            });

            // Ensure the dialog has finished showing
            AddStep("finish transforms", () => dialogOverlay.FinishTransforms(true));

            AddStep("click delete button", () =>
            {
                InputManager.MoveMouseTo(dialogOverlay.ChildrenOfType<DialogButton>().First());
                InputManager.PressButton(MouseButton.Left);
            });

            AddUntilStep("wait for fetch", () => leaderboard.Scores.Any());
            AddUntilStep("score removed from leaderboard", () => leaderboard.Scores.All(s => s.OnlineID != scoreBeingDeleted.OnlineID));

            // "Clean up"
            AddStep("release left mouse button", () => InputManager.ReleaseButton(MouseButton.Left));
        }

        [Test]
        public void TestDeleteViaDatabase()
        {
            AddStep("delete top score", () => scoreManager.Delete(importedScores[0]));
            AddUntilStep("wait for fetch", () => leaderboard.Scores.Any());
            AddUntilStep("score removed from leaderboard", () => leaderboard.Scores.All(s => s.OnlineID != importedScores[0].OnlineID));
        }
    }
}
