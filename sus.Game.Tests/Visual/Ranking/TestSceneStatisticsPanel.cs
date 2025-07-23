// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

#nullable disable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using NUnit.Framework;
using sus.Framework.Allocation;
using sus.Framework.Audio;
using sus.Framework.Bindables;
using sus.Framework.Extensions;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Framework.Graphics.Cursor;
using sus.Framework.Graphics.Shapes;
using sus.Framework.Platform;
using sus.Framework.Testing;
using sus.Game.Beatmaps;
using sus.Game.Graphics;
using sus.Game.Graphics.Sprites;
using sus.Game.Graphics.UserInterface;
using sus.Game.Online;
using sus.Game.Online.API;
using sus.Game.Online.API.Requests;
using sus.Game.Online.API.Requests.Responses;
using sus.Game.Overlays;
using sus.Game.Rulesets;
using sus.Game.Rulesets.Difficulty;
using sus.Game.Rulesets.Mania;
using sus.Game.Rulesets.Mods;
using sus.Game.Rulesets.Objects;
using sus.Game.Rulesets.Osu;
using sus.Game.Scoring;
using sus.Game.Screens.Ranking.Statistics;
using sus.Game.Rulesets.Osu.Objects;
using sus.Game.Rulesets.Scoring;
using sus.Game.Rulesets.UI;
using sus.Game.Screens.Ranking.Statistics.User;
using sus.Game.Tests.Resources;
using sus.Game.Users;
using susTK;

namespace sus.Game.Tests.Visual.Ranking
{
    public partial class TestSceneStatisticsPanel : OsuTestScene
    {
        private DummyAPIAccess dummyAPI => (DummyAPIAccess)API;

        private ScoreManager scoreManager = null!;
        private RulesetStore rulesetStore = null!;
        private BeatmapManager beatmapManager = null!;

        [Cached]
        private OverlayColourProvider colourProvider = new OverlayColourProvider(OverlayColourScheme.Aquamarine);

        protected override IReadOnlyDependencyContainer CreateChildDependencies(IReadOnlyDependencyContainer parent)
        {
            var dependencies = new DependencyContainer(base.CreateChildDependencies(parent));

            dependencies.Cache(rulesetStore = new RealmRulesetStore(Realm));
            dependencies.Cache(beatmapManager = new BeatmapManager(LocalStorage, Realm, null, dependencies.Get<AudioManager>(), Resources, dependencies.Get<GameHost>(), Beatmap.Default));
            dependencies.Cache(scoreManager = new ScoreManager(rulesetStore, () => beatmapManager, LocalStorage, Realm, API));
            Dependencies.Cache(Realm);

            return dependencies;
        }

        [Test]
        public void TestScoreWithPositionStatistics()
        {
            var score = TestResources.CreateTestScoreInfo();
            score.OnlineID = 1234;
            score.HitEvents = CreatePositionDistributedHitEvents();

            loadPanel(score);
        }

        [Test]
        public void TestScoreWithTimeStatistics()
        {
            var score = TestResources.CreateTestScoreInfo();
            score.HitEvents = TestSceneHitEventTimingDistributionGraph.CreateDistributedHitEvents();

            loadPanel(score);
        }

        [Test]
        public void TestScoreWithoutStatistics()
        {
            loadPanel(TestResources.CreateTestScoreInfo());
        }

        [Test]
        public void TestScoreInRulesetWhereAllStatsRequireHitEvents()
        {
            loadPanel(TestResources.CreateTestScoreInfo(new TestRulesetAllStatsRequireHitEvents().RulesetInfo));
        }

        [Test]
        public void TestScoreInRulesetWhereNoStatsRequireHitEvents()
        {
            loadPanel(TestResources.CreateTestScoreInfo(new TestRulesetNoStatsRequireHitEvents().RulesetInfo));
        }

        [Test]
        public void TestScoreInMixedRuleset()
        {
            loadPanel(TestResources.CreateTestScoreInfo(new TestRulesetMixed().RulesetInfo));
        }

        [Test]
        public void TestNullScore()
        {
            loadPanel(null);
        }

        [Test]
        public void TestStatisticsShownCorrectlyIfUpdateDeliveredBeforeLoad()
        {
            UserStatisticsWatcher userStatisticsWatcher = null!;
            ScoreInfo score = null!;

            AddStep("create user statistics watcher", () => Add(userStatisticsWatcher = new UserStatisticsWatcher(new LocalUserStatisticsProvider())));
            AddStep("set user statistics update", () =>
            {
                score = TestResources.CreateTestScoreInfo();
                score.OnlineID = 1234;
                ((Bindable<ScoreBasedUserStatisticsUpdate>)userStatisticsWatcher.LatestUpdate).Value = new ScoreBasedUserStatisticsUpdate(score,
                    new UserStatistics
                    {
                        Level = new UserStatistics.LevelInfo
                        {
                            Current = 5,
                            Progress = 20,
                        },
                        GlobalRank = 38000,
                        CountryRank = 12006,
                        PP = 2134,
                        RankedScore = 21123849,
                        Accuracy = 0.985,
                        PlayCount = 13375,
                        PlayTime = 354490,
                        TotalScore = 128749597,
                        TotalHits = 0,
                        MaxCombo = 1233,
                    }, new UserStatistics
                    {
                        Level = new UserStatistics.LevelInfo
                        {
                            Current = 5,
                            Progress = 30,
                        },
                        GlobalRank = 36000,
                        CountryRank = 12000,
                        PP = (decimal)2134.5,
                        RankedScore = 23897015,
                        Accuracy = 0.984,
                        PlayCount = 13376,
                        PlayTime = 35789,
                        TotalScore = 132218497,
                        TotalHits = 0,
                        MaxCombo = 1233,
                    });
            });
            AddStep("load user statistics panel", () => Child = new DependencyProvidingContainer
            {
                CachedDependencies = [(typeof(UserStatisticsWatcher), userStatisticsWatcher)],
                RelativeSizeAxes = Axes.Both,
                Child = new StatisticsPanel
                {
                    RelativeSizeAxes = Axes.Both,
                    State = { Value = Visibility.Visible },
                    Score = { Value = score, },
                    AchievedScore = score,
                }
            });
            AddUntilStep("overall ranking present", () => this.ChildrenOfType<OverallRanking>().Any());
            AddUntilStep("loading spinner not visible",
                () => this.ChildrenOfType<OverallRanking>().Single()
                          .ChildrenOfType<LoadingLayer>().All(l => l.State.Value == Visibility.Hidden));
        }

        [Test]
        public void TestTagging()
        {
            var score = TestResources.CreateTestScoreInfo();

            setUpTaggingRequests(() => score.BeatmapInfo);
            AddStep("load panel", () =>
            {
                Child = new PopoverContainer
                {
                    RelativeSizeAxes = Axes.Both,
                    Child = new StatisticsPanel
                    {
                        RelativeSizeAxes = Axes.Both,
                        State = { Value = Visibility.Visible },
                        Score = { Value = score },
                        AchievedScore = score,
                    }
                };
            });
        }

        private void setUpTaggingRequests(Func<BeatmapInfo> beatmap) =>
            AddStep("set up network requests", () =>
            {
                dummyAPI.HandleRequest = request =>
                {
                    switch (request)
                    {
                        case ListTagsRequest listTagsRequest:
                        {
                            Scheduler.AddDelayed(() => listTagsRequest.TriggerSuccess(new APITagCollection
                            {
                                Tags =
                                [
                                    new APITag { Id = 1, Name = "song representation/simple", Description = "Accessible and straightforward map design.", },
                                    new APITag { Id = 2, Name = "style/clean", Description = "Visually uncluttered and organised patterns, often involving few overlaps and equal visual spacing between objects.", },
                                    new APITag { Id = 3, Name = "aim/aim control", Description = "Patterns with velocity or direction changes which strongly go against a player's natural movement pattern.", },
                                    new APITag { Id = 4, Name = "tap/bursts", Description = "Patterns requiring continuous movement and alternating, typically 9 notes or less.", },
                                ]
                            }), 500);
                            return true;
                        }

                        case GetBeatmapSetRequest getBeatmapSetRequest:
                        {
                            var beatmapSet = CreateAPIBeatmapSet(beatmap.Invoke());
                            beatmapSet.Beatmaps.Single().TopTags =
                            [
                                new APIBeatmapTag { TagId = 3, VoteCount = 9 },
                            ];
                            Scheduler.AddDelayed(() => getBeatmapSetRequest.TriggerSuccess(beatmapSet), 500);
                            return true;
                        }

                        case AddBeatmapTagRequest:
                        case RemoveBeatmapTagRequest:
                        {
                            Scheduler.AddDelayed(request.TriggerSuccess, 500);
                            return true;
                        }
                    }

                    return false;
                };
            });

        [Test]
        public void TestTaggingWhenRankTooLow()
        {
            var score = TestResources.CreateTestScoreInfo();
            score.Rank = ScoreRank.D;

            AddStep("load panel", () =>
            {
                Child = new PopoverContainer
                {
                    RelativeSizeAxes = Axes.Both,
                    Child = new StatisticsPanel
                    {
                        RelativeSizeAxes = Axes.Both,
                        State = { Value = Visibility.Visible },
                        Score = { Value = score },
                        AchievedScore = score,
                    }
                };
            });
        }

        [Test]
        public void TestTaggingConvert()
        {
            var score = TestResources.CreateTestScoreInfo();
            score.Ruleset = new ManiaRuleset().RulesetInfo;

            AddStep("load panel", () =>
            {
                Child = new PopoverContainer
                {
                    RelativeSizeAxes = Axes.Both,
                    Child = new StatisticsPanel
                    {
                        RelativeSizeAxes = Axes.Both,
                        State = { Value = Visibility.Visible },
                        Score = { Value = score },
                        AchievedScore = score,
                    }
                };
            });
        }

        [Test]
        public void TestTaggingInteractionWithLocalScores()
        {
            BeatmapInfo beatmapInfo = null!;

            AddStep(@"Import beatmap", () =>
            {
                beatmapManager.Import(TestResources.GetQuickTestBeatmapForImport()).WaitSafely();
                beatmapInfo = beatmapManager.GetAllUsableBeatmapSets().First().Beatmaps.First();
            });

            AddStep("import bad score", () =>
            {
                var score = TestResources.CreateTestScoreInfo();
                score.BeatmapInfo = beatmapInfo;
                score.BeatmapHash = beatmapInfo.Hash;
                score.Ruleset = beatmapInfo.Ruleset;
                score.Rank = ScoreRank.D;
                score.User = API.LocalUser.Value;
                scoreManager.Import(score);
            });

            AddStep("import score by another user", () =>
            {
                var score = TestResources.CreateTestScoreInfo();
                score.BeatmapInfo = beatmapInfo;
                score.BeatmapHash = beatmapInfo.Hash;
                score.Ruleset = beatmapInfo.Ruleset;
                score.Rank = ScoreRank.D;
                score.User = new APIUser { Username = "notme", Id = 5678 };
                scoreManager.Import(score);
            });

            AddStep("import convert score", () =>
            {
                var score = TestResources.CreateTestScoreInfo();
                score.BeatmapInfo = beatmapInfo;
                score.BeatmapHash = beatmapInfo.Hash;
                score.Ruleset = new OsuRuleset().RulesetInfo;
                score.User = API.LocalUser.Value;
                scoreManager.Import(score);
            });

            AddStep("import correct score", () =>
            {
                var score = TestResources.CreateTestScoreInfo();
                score.BeatmapInfo = beatmapInfo;
                score.BeatmapHash = beatmapInfo.Hash;
                score.Ruleset = beatmapInfo.Ruleset;
                score.User = API.LocalUser.Value;
                scoreManager.Import(score);
            });

            setUpTaggingRequests(() => beatmapInfo);
            AddStep("load panel", () =>
            {
                var score = TestResources.CreateTestScoreInfo();
                score.BeatmapInfo = beatmapInfo;

                Child = new PopoverContainer
                {
                    RelativeSizeAxes = Axes.Both,
                    Child = new StatisticsPanel
                    {
                        RelativeSizeAxes = Axes.Both,
                        State = { Value = Visibility.Visible },
                        Score = { Value = score },
                    }
                };
            });
        }

        private void loadPanel(ScoreInfo score) => AddStep("load panel", () =>
        {
            Child = new PopoverContainer
            {
                RelativeSizeAxes = Axes.Both,
                Child = new StatisticsPanel
                {
                    RelativeSizeAxes = Axes.Both,
                    State = { Value = Visibility.Visible },
                    Score = { Value = score },
                    AchievedScore = score,
                },
            };
        });

        public static List<HitEvent> CreatePositionDistributedHitEvents()
        {
            var hitEvents = TestSceneHitEventTimingDistributionGraph.CreateDistributedHitEvents();

            // Use constant seed for reproducibility
            var random = new Random(0);

            for (int i = 0; i < hitEvents.Count; i++)
            {
                double angle = random.NextDouble() * 2 * Math.PI;
                double radius = random.NextDouble() * 0.5f * OsuHitObject.OBJECT_RADIUS;

                var position = new Vector2((float)(radius * Math.Cos(angle)), (float)(radius * Math.Sin(angle)));

                hitEvents[i] = hitEvents[i].With(position);
            }

            return hitEvents;
        }

        private class TestRuleset : Ruleset
        {
            public override IEnumerable<Mod> GetModsFor(ModType type)
            {
                throw new NotImplementedException();
            }

            public override DrawableRuleset CreateDrawableRulesetWith(IBeatmap beatmap, IReadOnlyList<Mod> mods = null)
            {
                throw new NotImplementedException();
            }

            public override IBeatmapConverter CreateBeatmapConverter(IBeatmap beatmap) => new TestBeatmapConverter(beatmap);

            public override DifficultyCalculator CreateDifficultyCalculator(IWorkingBeatmap beatmap)
            {
                throw new NotImplementedException();
            }

            public override string Description => string.Empty;

            public override string ShortName => string.Empty;

            protected static Drawable CreatePlaceholderStatistic(string message) => new Container
            {
                RelativeSizeAxes = Axes.X,
                Masking = true,
                CornerRadius = 20,
                Height = 250,
                Children = new Drawable[]
                {
                    new Box
                    {
                        RelativeSizeAxes = Axes.Both,
                        Colour = OsuColour.Gray(0.5f),
                        Alpha = 0.5f
                    },
                    new OsuSpriteText
                    {
                        Origin = Anchor.CentreLeft,
                        Anchor = Anchor.CentreLeft,
                        Text = message,
                        Margin = new MarginPadding { Left = 20 }
                    }
                }
            };

            private class TestBeatmapConverter : IBeatmapConverter
            {
#pragma warning disable CS0067 // The event is never used
                public event Action<HitObject, IEnumerable<HitObject>> ObjectConverted;
#pragma warning restore CS0067

                public IBeatmap Beatmap { get; }

                // ReSharper disable once NotNullOrRequiredMemberIsNotInitialized
                public TestBeatmapConverter(IBeatmap beatmap)
                {
                    Beatmap = beatmap;
                }

                public bool CanConvert() => true;

                public IBeatmap Convert(CancellationToken cancellationToken = default) => Beatmap.Clone();
            }
        }

        private class TestRulesetAllStatsRequireHitEvents : TestRuleset
        {
            public override StatisticItem[] CreateStatisticsForScore(ScoreInfo score, IBeatmap playableBeatmap) => new[]
            {
                new StatisticItem("Statistic Requiring Hit Events 1", () => CreatePlaceholderStatistic("Placeholder statistic. Requires hit events"), true),
                new StatisticItem("Statistic Requiring Hit Events 2", () => CreatePlaceholderStatistic("Placeholder statistic. Requires hit events"), true)
            };
        }

        private class TestRulesetNoStatsRequireHitEvents : TestRuleset
        {
            public override StatisticItem[] CreateStatisticsForScore(ScoreInfo score, IBeatmap playableBeatmap)
            {
                return new[]
                {
                    new StatisticItem("Statistic Not Requiring Hit Events 1", () => CreatePlaceholderStatistic("Placeholder statistic. Does not require hit events")),
                    new StatisticItem("Statistic Not Requiring Hit Events 2", () => CreatePlaceholderStatistic("Placeholder statistic. Does not require hit events"))
                };
            }
        }

        private class TestRulesetMixed : TestRuleset
        {
            public override StatisticItem[] CreateStatisticsForScore(ScoreInfo score, IBeatmap playableBeatmap)
            {
                return new[]
                {
                    new StatisticItem("Statistic Requiring Hit Events", () => CreatePlaceholderStatistic("Placeholder statistic. Requires hit events"), true),
                    new StatisticItem("Statistic Not Requiring Hit Events", () => CreatePlaceholderStatistic("Placeholder statistic. Does not require hit events"))
                };
            }
        }
    }
}
