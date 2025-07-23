// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

#nullable disable

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using sus.Framework.Allocation;
using sus.Framework.Extensions;
using sus.Framework.Platform;
using sus.Game.Beatmaps;
using sus.Game.Database;
using sus.Game.IO.Archives;
using sus.Game.Online.API;
using sus.Game.Online.API.Requests;
using sus.Game.Online.API.Requests.Responses;
using sus.Game.Rulesets.Mods;
using sus.Game.Rulesets.Osu;
using sus.Game.Rulesets.Osu.Mods;
using sus.Game.Rulesets.Scoring;
using sus.Game.Scoring;
using sus.Game.Tests.Beatmaps.IO;
using sus.Game.Tests.Resources;
using sus.Game.Users;

namespace sus.Game.Tests.Scores.IO
{
    public class ImportScoreTest : ImportTest
    {
        [Test]
        public void TestBasicImport()
        {
            using (HeadlessGameHost host = new CleanRunHeadlessGameHost())
            {
                try
                {
                    var sus = LoadOsuIntoHost(host, true);

                    var beatmap = BeatmapImportHelper.LoadOszIntoOsu(sus, TestResources.GetQuickTestBeatmapForImport()).GetResultSafely();

                    var toImport = new ScoreInfo
                    {
                        Rank = ScoreRank.B,
                        TotalScore = 987654,
                        Accuracy = 0.8,
                        MaxCombo = 500,
                        Combo = 250,
                        User = new APIUser { Username = "Test user" },
                        Date = DateTimeOffset.Now,
                        OnlineID = 12345,
                        Ruleset = new OsuRuleset().RulesetInfo,
                        BeatmapInfo = beatmap.Beatmaps.First()
                    };

                    var imported = LoadScoreIntoOsu(sus, toImport);

                    Assert.AreEqual(toImport.Rank, imported.Rank);
                    Assert.AreEqual(toImport.TotalScore, imported.TotalScore);
                    Assert.AreEqual(toImport.Accuracy, imported.Accuracy);
                    Assert.AreEqual(toImport.MaxCombo, imported.MaxCombo);
                    Assert.AreEqual(toImport.User.Username, imported.User.Username);
                    Assert.AreEqual(toImport.Date, imported.Date);
                    Assert.AreEqual(toImport.OnlineID, imported.OnlineID);
                }
                finally
                {
                    host.Exit();
                }
            }
        }

        [TestCase(false)]
        [TestCase(true)]
        public void TestLastPlayedUpdate(bool isLocalUser)
        {
            using (HeadlessGameHost host = new CleanRunHeadlessGameHost())
            {
                try
                {
                    var sus = LoadOsuIntoHost(host, true);

                    if (!isLocalUser)
                        sus.API.Logout();

                    var beatmap = BeatmapImportHelper.LoadOszIntoOsu(sus, TestResources.GetQuickTestBeatmapForImport()).GetResultSafely();
                    var beatmapInfo = beatmap.Beatmaps.First();

                    DateTimeOffset replayDate = DateTimeOffset.Now;

                    var toImport = new ScoreInfo
                    {
                        Rank = ScoreRank.B,
                        TotalScore = 987654,
                        Accuracy = 0.8,
                        MaxCombo = 500,
                        Combo = 250,
                        User = new APIUser
                        {
                            Username = "Test user",
                            Id = DummyAPIAccess.DUMMY_USER_ID,
                        },
                        Date = replayDate,
                        OnlineID = 12345,
                        Ruleset = new OsuRuleset().RulesetInfo,
                        BeatmapInfo = beatmapInfo
                    };

                    var imported = LoadScoreIntoOsu(sus, toImport);

                    Assert.AreEqual(toImport.Rank, imported.Rank);
                    Assert.AreEqual(toImport.TotalScore, imported.TotalScore);
                    Assert.AreEqual(toImport.Accuracy, imported.Accuracy);
                    Assert.AreEqual(toImport.MaxCombo, imported.MaxCombo);
                    Assert.AreEqual(toImport.User.Username, imported.User.Username);
                    Assert.AreEqual(toImport.Date, imported.Date);
                    Assert.AreEqual(toImport.OnlineID, imported.OnlineID);

                    if (isLocalUser)
                        Assert.That(imported.BeatmapInfo!.LastPlayed, Is.EqualTo(replayDate));
                    else
                        Assert.That(imported.BeatmapInfo!.LastPlayed, Is.Null);
                }
                finally
                {
                    host.Exit();
                }
            }
        }

        [Test]
        public void TestLastPlayedNotUpdatedDueToNewerPlays()
        {
            using (HeadlessGameHost host = new CleanRunHeadlessGameHost())
            {
                try
                {
                    var sus = LoadOsuIntoHost(host, true);

                    var beatmap = BeatmapImportHelper.LoadOszIntoOsu(sus, TestResources.GetQuickTestBeatmapForImport()).GetResultSafely();
                    var beatmapInfo = beatmap.Beatmaps.First();

                    var realmAccess = sus.Dependencies.Get<RealmAccess>();
                    realmAccess.Write(r => r.Find<BeatmapInfo>(beatmapInfo.ID)!.LastPlayed = new DateTimeOffset(2023, 10, 30, 0, 0, 0, TimeSpan.Zero));

                    var toImport = new ScoreInfo
                    {
                        Rank = ScoreRank.B,
                        TotalScore = 987654,
                        Accuracy = 0.8,
                        MaxCombo = 500,
                        Combo = 250,
                        User = new APIUser
                        {
                            Username = "Test user",
                            Id = DummyAPIAccess.DUMMY_USER_ID,
                        },
                        Date = new DateTimeOffset(2023, 10, 27, 0, 0, 0, TimeSpan.Zero),
                        OnlineID = 12345,
                        Ruleset = new OsuRuleset().RulesetInfo,
                        BeatmapInfo = beatmapInfo
                    };

                    var imported = LoadScoreIntoOsu(sus, toImport);

                    Assert.AreEqual(toImport.Rank, imported.Rank);
                    Assert.AreEqual(toImport.TotalScore, imported.TotalScore);
                    Assert.AreEqual(toImport.Accuracy, imported.Accuracy);
                    Assert.AreEqual(toImport.MaxCombo, imported.MaxCombo);
                    Assert.AreEqual(toImport.User.Username, imported.User.Username);
                    Assert.AreEqual(toImport.Date, imported.Date);
                    Assert.AreEqual(toImport.OnlineID, imported.OnlineID);

                    Assert.That(imported.BeatmapInfo!.LastPlayed, Is.EqualTo(new DateTimeOffset(2023, 10, 30, 0, 0, 0, TimeSpan.Zero)));
                }
                finally
                {
                    host.Exit();
                }
            }
        }

        [Test]
        public void TestImportMods()
        {
            using (HeadlessGameHost host = new CleanRunHeadlessGameHost())
            {
                try
                {
                    var sus = LoadOsuIntoHost(host, true);

                    var beatmap = BeatmapImportHelper.LoadOszIntoOsu(sus, TestResources.GetQuickTestBeatmapForImport()).GetResultSafely();

                    var toImport = new ScoreInfo
                    {
                        User = new APIUser { Username = "Test user" },
                        BeatmapInfo = beatmap.Beatmaps.First(),
                        Ruleset = new OsuRuleset().RulesetInfo,
                        ClientVersion = "12345",
                        Mods = new Mod[] { new OsuModHardRock(), new OsuModDoubleTime() },
                    };

                    var imported = LoadScoreIntoOsu(sus, toImport);

                    Assert.IsTrue(imported.Mods.Any(m => m is OsuModHardRock));
                    Assert.IsTrue(imported.Mods.Any(m => m is OsuModDoubleTime));
                    Assert.That(imported.ClientVersion, Is.EqualTo(toImport.ClientVersion));
                }
                finally
                {
                    host.Exit();
                }
            }
        }

        [Test]
        public void TestScoreWithInvalidModCombinationsWillNotImport()
        {
            using (HeadlessGameHost host = new CleanRunHeadlessGameHost())
            {
                try
                {
                    var sus = LoadOsuIntoHost(host, true);

                    var beatmap = BeatmapImportHelper.LoadOszIntoOsu(sus, TestResources.GetQuickTestBeatmapForImport()).GetResultSafely();

                    var toImport = new ScoreInfo
                    {
                        User = new APIUser { Username = "Test user" },
                        BeatmapInfo = beatmap.Beatmaps.First(),
                        Ruleset = new OsuRuleset().RulesetInfo,
                        ClientVersion = "12345",
                        Mods = new Mod[] { new OsuModHalfTime(), new OsuModDoubleTime() },
                    };

                    Assert.Throws<InvalidOperationException>(() => LoadScoreIntoOsu(sus, toImport));
                }
                finally
                {
                    host.Exit();
                }
            }
        }

        [Test]
        public void TestImportStatistics()
        {
            using (HeadlessGameHost host = new CleanRunHeadlessGameHost())
            {
                try
                {
                    var sus = LoadOsuIntoHost(host, true);

                    var beatmap = BeatmapImportHelper.LoadOszIntoOsu(sus, TestResources.GetQuickTestBeatmapForImport()).GetResultSafely();

                    var toImport = new ScoreInfo
                    {
                        User = new APIUser { Username = "Test user" },
                        BeatmapInfo = beatmap.Beatmaps.First(),
                        Ruleset = new OsuRuleset().RulesetInfo,
                        Statistics = new Dictionary<HitResult, int>
                        {
                            { HitResult.Perfect, 100 },
                            { HitResult.Miss, 50 }
                        }
                    };

                    var imported = LoadScoreIntoOsu(sus, toImport);

                    Assert.AreEqual(toImport.Statistics[HitResult.Perfect], imported.Statistics[HitResult.Perfect]);
                    Assert.AreEqual(toImport.Statistics[HitResult.Miss], imported.Statistics[HitResult.Miss]);
                }
                finally
                {
                    host.Exit();
                }
            }
        }

        [Test]
        public void TestOnlineScoreIsAvailableLocally()
        {
            using (HeadlessGameHost host = new CleanRunHeadlessGameHost())
            {
                try
                {
                    var sus = LoadOsuIntoHost(host, true);

                    var beatmap = BeatmapImportHelper.LoadOszIntoOsu(sus, TestResources.GetQuickTestBeatmapForImport()).GetResultSafely();

                    LoadScoreIntoOsu(sus, new ScoreInfo
                    {
                        User = new APIUser { Username = "Test user" },
                        BeatmapInfo = beatmap.Beatmaps.First(),
                        Ruleset = new OsuRuleset().RulesetInfo,
                        OnlineID = 2
                    }, new TestArchiveReader());

                    var scoreManager = sus.Dependencies.Get<ScoreManager>();

                    // Note: A new score reference is used here since the import process mutates the original object to set an ID
                    Assert.That(scoreManager.IsAvailableLocally(new ScoreInfo
                    {
                        User = new APIUser { Username = "Test user" },
                        BeatmapInfo = beatmap.Beatmaps.First(),
                        OnlineID = 2
                    }));
                }
                finally
                {
                    host.Exit();
                }
            }
        }

        [Test]
        public void TestUserLookedUpByUsernameForOnlineScoreIfUserIDMissing()
        {
            using (HeadlessGameHost host = new CleanRunHeadlessGameHost())
            {
                try
                {
                    var sus = LoadOsuIntoHost(host, true);

                    var api = (DummyAPIAccess)sus.API;
                    api.HandleRequest = req =>
                    {
                        switch (req)
                        {
                            case GetUserRequest userRequest:
                                if (userRequest.Lookup != "Test user")
                                    return false;

                                userRequest.TriggerSuccess(new APIUser
                                {
                                    Username = "Test user",
                                    CountryCode = CountryCode.JP,
                                    Id = 1234
                                });
                                return true;

                            default:
                                return false;
                        }
                    };

                    var beatmap = BeatmapImportHelper.LoadOszIntoOsu(sus, TestResources.GetQuickTestBeatmapForImport()).GetResultSafely();

                    var toImport = new ScoreInfo
                    {
                        Rank = ScoreRank.B,
                        TotalScore = 987654,
                        Accuracy = 0.8,
                        MaxCombo = 500,
                        Combo = 250,
                        User = new APIUser { Username = "Test user" },
                        Date = DateTimeOffset.Now,
                        OnlineID = 12345,
                        Ruleset = new OsuRuleset().RulesetInfo,
                        BeatmapInfo = beatmap.Beatmaps.First()
                    };

                    var imported = LoadScoreIntoOsu(sus, toImport);

                    Assert.AreEqual(toImport.Rank, imported.Rank);
                    Assert.AreEqual(toImport.TotalScore, imported.TotalScore);
                    Assert.AreEqual(toImport.Accuracy, imported.Accuracy);
                    Assert.AreEqual(toImport.MaxCombo, imported.MaxCombo);
                    Assert.AreEqual(toImport.User.Username, imported.User.Username);
                    Assert.AreEqual(toImport.Date, imported.Date);
                    Assert.AreEqual(toImport.OnlineID, imported.OnlineID);
                    Assert.AreEqual(toImport.User.Username, imported.RealmUser.Username);
                    Assert.AreEqual(1234, imported.RealmUser.OnlineID);
                }
                finally
                {
                    host.Exit();
                }
            }
        }

        [Test]
        public void TestUserLookedUpByUsernameForLegacyOnlineScore()
        {
            using (HeadlessGameHost host = new CleanRunHeadlessGameHost())
            {
                try
                {
                    var sus = LoadOsuIntoHost(host, true);

                    var api = (DummyAPIAccess)sus.API;
                    api.HandleRequest = req =>
                    {
                        switch (req)
                        {
                            case GetUserRequest userRequest:
                                if (userRequest.Lookup != "Test user")
                                    return false;

                                userRequest.TriggerSuccess(new APIUser
                                {
                                    Username = "Test user",
                                    CountryCode = CountryCode.JP,
                                    Id = 1234
                                });
                                return true;

                            default:
                                return false;
                        }
                    };

                    var beatmap = BeatmapImportHelper.LoadOszIntoOsu(sus, TestResources.GetQuickTestBeatmapForImport()).GetResultSafely();

                    var toImport = new ScoreInfo
                    {
                        Rank = ScoreRank.B,
                        TotalScore = 987654,
                        Accuracy = 0.8,
                        MaxCombo = 500,
                        Combo = 250,
                        User = new APIUser { Username = "Test user" },
                        Date = DateTimeOffset.Now,
                        LegacyOnlineID = 12345,
                        Ruleset = new OsuRuleset().RulesetInfo,
                        BeatmapInfo = beatmap.Beatmaps.First()
                    };

                    var imported = LoadScoreIntoOsu(sus, toImport);

                    Assert.AreEqual(toImport.Rank, imported.Rank);
                    Assert.AreEqual(toImport.TotalScore, imported.TotalScore);
                    Assert.AreEqual(toImport.Accuracy, imported.Accuracy);
                    Assert.AreEqual(toImport.MaxCombo, imported.MaxCombo);
                    Assert.AreEqual(toImport.User.Username, imported.User.Username);
                    Assert.AreEqual(toImport.Date, imported.Date);
                    Assert.AreEqual(toImport.OnlineID, imported.OnlineID);
                    Assert.AreEqual(toImport.User.Username, imported.RealmUser.Username);
                    Assert.AreEqual(1234, imported.RealmUser.OnlineID);
                }
                finally
                {
                    host.Exit();
                }
            }
        }

        [Test]
        public void TestUserNotLookedUpForOfflineScoreIfUserIDMissing()
        {
            using (HeadlessGameHost host = new CleanRunHeadlessGameHost())
            {
                try
                {
                    var sus = LoadOsuIntoHost(host, true);

                    var api = (DummyAPIAccess)sus.API;
                    api.HandleRequest = req =>
                    {
                        switch (req)
                        {
                            case GetUserRequest userRequest:
                                if (userRequest.Lookup != "Test user")
                                    return false;

                                userRequest.TriggerSuccess(new APIUser
                                {
                                    Username = "Test user",
                                    CountryCode = CountryCode.JP,
                                    Id = 1234
                                });
                                return true;

                            default:
                                return false;
                        }
                    };

                    var beatmap = BeatmapImportHelper.LoadOszIntoOsu(sus, TestResources.GetQuickTestBeatmapForImport()).GetResultSafely();

                    var toImport = new ScoreInfo
                    {
                        Rank = ScoreRank.B,
                        TotalScore = 987654,
                        Accuracy = 0.8,
                        MaxCombo = 500,
                        Combo = 250,
                        User = new APIUser { Username = "Test user" },
                        Date = DateTimeOffset.Now,
                        OnlineID = -1,
                        LegacyOnlineID = -1,
                        Ruleset = new OsuRuleset().RulesetInfo,
                        BeatmapInfo = beatmap.Beatmaps.First()
                    };

                    var imported = LoadScoreIntoOsu(sus, toImport);

                    Assert.AreEqual(toImport.Rank, imported.Rank);
                    Assert.AreEqual(toImport.TotalScore, imported.TotalScore);
                    Assert.AreEqual(toImport.Accuracy, imported.Accuracy);
                    Assert.AreEqual(toImport.MaxCombo, imported.MaxCombo);
                    Assert.AreEqual(toImport.User.Username, imported.User.Username);
                    Assert.AreEqual(toImport.Date, imported.Date);
                    Assert.AreEqual(toImport.OnlineID, imported.OnlineID);
                    Assert.AreEqual(toImport.User.Username, imported.RealmUser.Username);
                    Assert.That(imported.RealmUser.OnlineID, Is.LessThanOrEqualTo(1));
                }
                finally
                {
                    host.Exit();
                }
            }
        }

        [Test]
        public void TestUserLookedUpByOnlineIDIfPresent([Values] bool isOnlineScore)
        {
            using (HeadlessGameHost host = new CleanRunHeadlessGameHost())
            {
                try
                {
                    var sus = LoadOsuIntoHost(host, true);

                    var api = (DummyAPIAccess)sus.API;
                    api.HandleRequest = req =>
                    {
                        switch (req)
                        {
                            case GetUserRequest userRequest:
                                if (userRequest.Lookup != "5555")
                                    return false;

                                userRequest.TriggerSuccess(new APIUser
                                {
                                    Username = "Some other guy",
                                    CountryCode = CountryCode.DE,
                                    Id = 5555
                                });
                                return true;

                            default:
                                return false;
                        }
                    };

                    var beatmap = BeatmapImportHelper.LoadOszIntoOsu(sus, TestResources.GetQuickTestBeatmapForImport()).GetResultSafely();

                    var toImport = new ScoreInfo
                    {
                        Rank = ScoreRank.B,
                        TotalScore = 987654,
                        Accuracy = 0.8,
                        MaxCombo = 500,
                        Combo = 250,
                        User = new APIUser { Id = 5555 },
                        Date = DateTimeOffset.Now,
                        Ruleset = new OsuRuleset().RulesetInfo,
                        BeatmapInfo = beatmap.Beatmaps.First()
                    };
                    if (isOnlineScore)
                        toImport.OnlineID = 12345;

                    var imported = LoadScoreIntoOsu(sus, toImport);

                    Assert.AreEqual(toImport.Rank, imported.Rank);
                    Assert.AreEqual(toImport.TotalScore, imported.TotalScore);
                    Assert.AreEqual(toImport.Accuracy, imported.Accuracy);
                    Assert.AreEqual(toImport.MaxCombo, imported.MaxCombo);
                    Assert.AreEqual(toImport.Date, imported.Date);
                    Assert.AreEqual(toImport.OnlineID, imported.OnlineID);
                    Assert.AreEqual("Some other guy", imported.RealmUser.Username);
                    Assert.AreEqual(5555, imported.RealmUser.OnlineID);
                    Assert.AreEqual(CountryCode.DE, imported.RealmUser.CountryCode);
                }
                finally
                {
                    host.Exit();
                }
            }
        }

        public static ScoreInfo LoadScoreIntoOsu(OsuGameBase sus, ScoreInfo score, ArchiveReader archive = null)
        {
            // clone to avoid attaching the input score to realm.
            score = score.DeepClone();

            var scoreManager = sus.Dependencies.Get<ScoreManager>();

            scoreManager.Import(score, archive);

            return scoreManager.Query(_ => true);
        }

        internal class TestArchiveReader : ArchiveReader
        {
            public TestArchiveReader()
                : base("test_archive")
            {
            }

            public override Stream GetStream(string name) => new MemoryStream();

            public override void Dispose()
            {
            }

            public override IEnumerable<string> Filenames => new[] { "test_file.osr" };
        }
    }
}
