// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

#nullable disable

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using NUnit.Framework;
using sus.Framework.Platform;
using sus.Framework.Testing;
using sus.Game.Beatmaps;
using sus.Game.Database;
using sus.Game.Graphics.Sprites;
using sus.Game.Online.API;
using sus.Game.Online.API.Requests;
using sus.Game.Online.API.Requests.Responses;
using sus.Game.Overlays;
using sus.Game.Overlays.BeatmapListing;
using sus.Game.Rulesets;
using sus.Game.Rulesets.Catch;
using sus.Game.Rulesets.Mania;
using sus.Game.Rulesets.Osu;
using sus.Game.Rulesets.Taiko;
using sus.Game.Screens.SelectV2;
using sus.Game.Tests.Resources;
using sus.Game.Users;
using sus.Game.Utils;
using susTK.Input;

namespace sus.Game.Tests.Visual.SongSelect
{
    public partial class TestSceneBeatmapRecommendations : OsuGameTestScene
    {
        [SetUpSteps]
        public override void SetUpSteps()
        {
            AddStep("populate ruleset statistics", () =>
            {
                ((DummyAPIAccess)API).HandleRequest = r =>
                {
                    switch (r)
                    {
                        case GetUserRequest userRequest:
                            userRequest.TriggerSuccess(new APIUser
                            {
                                Id = 99,
                                Statistics = new UserStatistics
                                {
                                    PP = getNecessaryPP(userRequest.Ruleset?.OnlineID ?? 0)
                                }
                            });

                            return true;

                        default:
                            return false;
                    }
                };
            });

            decimal getNecessaryPP(int? rulesetID)
            {
                switch (rulesetID)
                {
                    case 0:
                        return 337; // recommended star rating of 2

                    case 1:
                        return 973; // SR 3

                    case 2:
                        return 1906; // SR 4

                    case 3:
                        return 3330; // SR 5

                    default:
                        return 0;
                }
            }

            base.SetUpSteps();
        }

        [Test]
        [FlakyTest]
        public void TestPresentedBeatmapIsRecommended()
        {
            List<BeatmapSetInfo> beatmapSets = null;
            const int import_count = 5;

            AddStep("import 5 maps", () =>
            {
                beatmapSets = new List<BeatmapSetInfo>();

                for (int i = 0; i < import_count; ++i)
                {
                    beatmapSets.Add(importBeatmapSet(Enumerable.Repeat(new OsuRuleset().RulesetInfo, 5)));
                }
            });

            AddAssert("all sets imported", () => ensureAllBeatmapSetsImported(beatmapSets));

            presentAndConfirm(() => beatmapSets[3], 2);
        }

        [Test]
        [FlakyTest]
        public void TestCurrentRulesetIsRecommended()
        {
            BeatmapSetInfo catchSet = null, mixedSet = null;

            AddStep("create catch beatmapset", () => catchSet = importBeatmapSet(new[] { new CatchRuleset().RulesetInfo }));
            AddStep("create mixed beatmapset", () => mixedSet = importBeatmapSet(new[] { new TaikoRuleset().RulesetInfo, new CatchRuleset().RulesetInfo, new ManiaRuleset().RulesetInfo }));

            AddAssert("all sets imported", () => ensureAllBeatmapSetsImported(new[] { catchSet, mixedSet }));

            // Switch to catch
            presentAndConfirm(() => catchSet, 1);

            AddAssert("game-wide ruleset changed", () => Game.Ruleset.Value.Equals(catchSet.Beatmaps.First().Ruleset));

            // Present mixed difficulty set, expect current ruleset to be selected
            presentAndConfirm(() => mixedSet, 2);
        }

        [Test]
        public void TestBestRulesetIsRecommended()
        {
            BeatmapSetInfo susSet = null, mixedSet = null;

            AddStep("create sus! beatmapset", () => susSet = importBeatmapSet(new[] { new OsuRuleset().RulesetInfo }));
            AddStep("create mixed beatmapset", () => mixedSet = importBeatmapSet(new[] { new TaikoRuleset().RulesetInfo, new CatchRuleset().RulesetInfo, new ManiaRuleset().RulesetInfo }));

            AddAssert("all sets imported", () => ensureAllBeatmapSetsImported(new[] { susSet, mixedSet }));

            // Make sure we are on standard ruleset
            presentAndConfirm(() => susSet, 1);

            // Present mixed difficulty set, expect ruleset with highest star difficulty
            presentAndConfirm(() => mixedSet, 3);
        }

        [Test]
        [FlakyTest]
        public void TestSecondBestRulesetIsRecommended()
        {
            BeatmapSetInfo susSet = null, mixedSet = null;

            AddStep("create sus! beatmapset", () => susSet = importBeatmapSet(new[] { new OsuRuleset().RulesetInfo }));
            AddStep("create mixed beatmapset", () => mixedSet = importBeatmapSet(new[] { new TaikoRuleset().RulesetInfo, new CatchRuleset().RulesetInfo, new TaikoRuleset().RulesetInfo }));

            AddAssert("all sets imported", () => ensureAllBeatmapSetsImported(new[] { susSet, mixedSet }));

            // Make sure we are on standard ruleset
            presentAndConfirm(() => susSet, 1);

            // Present mixed difficulty set, expect ruleset with second highest star difficulty
            presentAndConfirm(() => mixedSet, 2);
        }

        [Test]
        [FlakyTest]
        public void TestCorrectStarRatingIsUsed()
        {
            BeatmapSetInfo susSet = null, maniaSet = null;

            AddStep("create sus! beatmapset", () => susSet = importBeatmapSet(new[] { new OsuRuleset().RulesetInfo }));
            AddStep("create mania beatmapset", () => maniaSet = importBeatmapSet(Enumerable.Repeat(new ManiaRuleset().RulesetInfo, 10)));

            AddAssert("all sets imported", () => ensureAllBeatmapSetsImported(new[] { susSet, maniaSet }));

            // Make sure we are on standard ruleset
            presentAndConfirm(() => susSet, 1);

            // Present mania set, expect the difficulty that matches recommended mania star rating
            presentAndConfirm(() => maniaSet, 5);
        }

        [Test]
        [FlakyTest]
        public void TestBeatmapListingFilter()
        {
            AddStep("set playmode to taiko", () => ((DummyAPIAccess)API).LocalUser.Value.PlayMode = "taiko");

            AddStep("open beatmap listing", () =>
            {
                InputManager.PressKey(Key.ControlLeft);
                InputManager.PressKey(Key.B);
                InputManager.ReleaseKey(Key.B);
                InputManager.ReleaseKey(Key.ControlLeft);
            });

            AddUntilStep("wait for load", () => Game.ChildrenOfType<BeatmapListingOverlay>().SingleOrDefault()?.IsLoaded, () => Is.True);

            checkRecommendedDifficulty(3);

            AddStep("change mode filter to sus!", () => Game.ChildrenOfType<BeatmapSearchRulesetFilterRow>().Single().ChildrenOfType<FilterTabItem<RulesetInfo>>().ElementAt(1).TriggerClick());

            checkRecommendedDifficulty(2);

            AddStep("change mode filter to sus!taiko", () => Game.ChildrenOfType<BeatmapSearchRulesetFilterRow>().Single().ChildrenOfType<FilterTabItem<RulesetInfo>>().ElementAt(2).TriggerClick());

            checkRecommendedDifficulty(3);

            AddStep("change mode filter to sus!catch", () => Game.ChildrenOfType<BeatmapSearchRulesetFilterRow>().Single().ChildrenOfType<FilterTabItem<RulesetInfo>>().ElementAt(3).TriggerClick());

            checkRecommendedDifficulty(4);

            AddStep("change mode filter to sus!mania", () => Game.ChildrenOfType<BeatmapSearchRulesetFilterRow>().Single().ChildrenOfType<FilterTabItem<RulesetInfo>>().ElementAt(4).TriggerClick());

            checkRecommendedDifficulty(5);

            void checkRecommendedDifficulty(double starRating)
                => AddAssert($"recommended difficulty is {starRating}",
                    () => Game.ChildrenOfType<BeatmapSearchGeneralFilterRow>().Single().ChildrenOfType<OsuSpriteText>().ElementAt(1).Text.ToString(),
                    () => Is.EqualTo($"Recommended difficulty ({starRating.FormatStarRating()})"));
        }

        private BeatmapSetInfo importBeatmapSet(IEnumerable<RulesetInfo> difficultyRulesets)
        {
            var rulesets = difficultyRulesets.ToArray();

            var beatmapSet = TestResources.CreateTestBeatmapSetInfo(rulesets.Length, rulesets);

            var importedBeatmapSet = Game.BeatmapManager.Import(beatmapSet);

            Debug.Assert(importedBeatmapSet != null);

            importedBeatmapSet.PerformWrite(s =>
            {
                for (int i = 0; i < rulesets.Length; i++)
                {
                    var beatmap = s.Beatmaps[i];

                    beatmap.StarRating = i + 1;
                    beatmap.DifficultyName = $"SR{i + 1}";
                }
            });

            return importedBeatmapSet.Value;
        }

        private bool ensureAllBeatmapSetsImported(IEnumerable<BeatmapSetInfo> beatmapSets) => beatmapSets.All(set => set != null);

        private void presentAndConfirm(Func<BeatmapSetInfo> getImport, int expectedDiff)
        {
            AddStep("present beatmap", () => Game.PresentBeatmap(getImport()));

            AddUntilStep("wait for song select", () => Game.ScreenStack.CurrentScreen is SoloSongSelect select && select.CarouselItemsPresented);
            AddUntilStep("recommended beatmap displayed", () => Game.Beatmap.Value.BeatmapInfo.OnlineID, () => Is.EqualTo(getImport().Beatmaps[expectedDiff - 1].OnlineID));
        }

        protected override TestOsuGame CreateTestGame() => new NoBeatmapUpdateGame(LocalStorage, API);

        private partial class NoBeatmapUpdateGame : TestOsuGame
        {
            public NoBeatmapUpdateGame(Storage storage, IAPIProvider api, string[] args = null)
                : base(storage, api, args)
            {
            }

            protected override IBeatmapUpdater CreateBeatmapUpdater() => new TestBeatmapUpdater();

            private class TestBeatmapUpdater : IBeatmapUpdater
            {
                public void Queue(Live<BeatmapSetInfo> beatmapSet, MetadataLookupScope lookupScope = MetadataLookupScope.LocalCacheFirst)
                {
                }

                public void Process(BeatmapSetInfo beatmapSet, MetadataLookupScope lookupScope = MetadataLookupScope.LocalCacheFirst)
                {
                }

                public void ProcessObjectCounts(BeatmapInfo beatmapInfo, MetadataLookupScope lookupScope = MetadataLookupScope.LocalCacheFirst)
                {
                }

                public void Dispose()
                {
                }
            }
        }
    }
}
