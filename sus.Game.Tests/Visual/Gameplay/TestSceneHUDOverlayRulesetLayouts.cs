// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Collections.Generic;
using NUnit.Framework;
using osu.Framework.Allocation;
using osu.Framework.Audio;
using osu.Framework.Audio.Track;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Rendering;
using osu.Framework.Graphics.Textures;
using osu.Framework.IO.Stores;
using osu.Framework.Platform;
using osu.Framework.Testing;
using sus.Game.Beatmaps;
using sus.Game.Configuration;
using sus.Game.Database;
using sus.Game.IO;
using sus.Game.Online.API.Requests.Responses;
using sus.Game.Online.Spectator;
using sus.Game.Rulesets;
using sus.Game.Rulesets.Scoring;
using sus.Game.Rulesets.UI.Scrolling;
using sus.Game.Scoring;
using sus.Game.Screens.Play;
using sus.Game.Screens.Select.Leaderboards;
using sus.Game.Skinning;
using sus.Game.Tests.Gameplay;
using sus.Game.Tests.Visual.Spectator;

namespace sus.Game.Tests.Visual.Gameplay
{
    [Description(@"Exercises the appearance of the HUD overlay on various skin and ruleset combinations.")]
    public partial class TestSceneHUDOverlayRulesetLayouts : OsuTestScene, IStorageResourceProvider
    {
        private readonly Dictionary<string, ISkin> skins = new Dictionary<string, ISkin>();

        [Resolved]
        private GameHost host { get; set; } = null!;

        [Resolved]
        private RulesetStore rulesets { get; set; } = null!;

        [Resolved]
        private OsuConfigManager configManager { get; set; } = null!;

        [BackgroundDependencyLoader]
        private void load()
        {
            skins["argon"] = new ArgonSkin(this);
            skins["triangles"] = new TrianglesSkin(this);
            skins["legacy"] = new DefaultLegacySkin(this);
        }

        [SetUpSteps]
        public void SetUpSteps()
        {
            AddToggleStep("toggle leaderboard", b => configManager.SetValue(OsuSetting.GameplayLeaderboard, b));
        }

        [Test]
        public void TestLayout(
            [Values("argon", "triangles", "legacy")]
            string skinName,
            [Values("sus", "taiko", "fruits", "mania")]
            string rulesetName)
        {
            AddStep("create content", () =>
            {
                var rulesetInfo = rulesets.GetRuleset(rulesetName);
                var ruleset = rulesetInfo!.CreateInstance();
                var beatmap = ruleset.CreateBeatmapConverter(new Beatmap()).Convert();
                var drawableRuleset = ruleset.CreateDrawableRulesetWith(beatmap);

                ISkin provider = ruleset.CreateSkinTransformer(skins[skinName], beatmap)!;

                var gameplayState = TestGameplayState.Create(ruleset);
                ((Bindable<LocalUserPlayingState>)gameplayState.PlayingState).Value = LocalUserPlayingState.Playing;
                var spectatorClient = new TestSpectatorClient();

                for (int i = 0; i < 15; ++i)
                {
                    ((ISpectatorClient)spectatorClient).UserStartedWatching([
                        new SpectatorUser
                        {
                            OnlineID = i,
                            Username = $"User {i}"
                        }
                    ]);
                }

                GameplayClockContainer gameplayClock;

                List<(Type, object)> dependencies =
                [
                    (typeof(GameplayState), gameplayState),
                    (typeof(ScoreProcessor), gameplayState.ScoreProcessor),
                    (typeof(HealthProcessor), gameplayState.HealthProcessor),
                    (typeof(IGameplayClock), gameplayClock = new GameplayClockContainer(new TrackVirtual(60000), false, false)),
                    (typeof(SpectatorClient), spectatorClient),
                    (typeof(IGameplayLeaderboardProvider), new TestGameplayLeaderboardProvider()),
                ];

                if (drawableRuleset is IDrawableScrollingRuleset scrolling)
                    dependencies.Add((typeof(IScrollingInfo), scrolling.ScrollingInfo));

                Child = new DependencyProvidingContainer
                {
                    RelativeSizeAxes = Axes.Both,
                    CachedDependencies = dependencies.ToArray(),
                    Children = new Drawable[]
                    {
                        spectatorClient,
                        new SkinProvidingContainer(provider)
                        {
                            RelativeSizeAxes = Axes.Both,
                            Children = new Drawable[]
                            {
                                drawableRuleset,
                                new HUDOverlay(drawableRuleset, [])
                                {
                                    RelativeSizeAxes = Axes.Both,
                                }
                            }
                        }
                    }
                };

                gameplayClock.Start();
            });
        }

        private class TestGameplayLeaderboardProvider : IGameplayLeaderboardProvider
        {
            IBindableList<GameplayLeaderboardScore> IGameplayLeaderboardProvider.Scores => Scores;
            public BindableList<GameplayLeaderboardScore> Scores { get; } = new BindableList<GameplayLeaderboardScore>();

            public TestGameplayLeaderboardProvider()
            {
                for (int i = 0; i < 20; ++i)
                {
                    Scores.Add(new GameplayLeaderboardScore(new ScoreInfo
                    {
                        User = new APIUser { Username = $"User {i}" },
                        TotalScore = (20 - i) * 50_000,
                        Accuracy = i * 0.05,
                        MaxCombo = i * 50,
                    }, i == 19, GameplayLeaderboardScore.ComboDisplayMode.Highest));
                }
            }
        }

        #region IResourceStorageProvider

        public IRenderer Renderer => host.Renderer;
        public AudioManager AudioManager => Audio;
        public IResourceStore<byte[]> Files => null!;
        public new IResourceStore<byte[]> Resources => base.Resources;
        public IResourceStore<TextureUpload> CreateTextureLoaderStore(IResourceStore<byte[]> underlyingStore) => host.CreateTextureLoaderStore(underlyingStore);
        RealmAccess IStorageResourceProvider.RealmAccess => null!;

        #endregion
    }
}
