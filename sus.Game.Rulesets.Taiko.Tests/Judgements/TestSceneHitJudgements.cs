// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using NUnit.Framework;
using sus.Game.Beatmaps.ControlPoints;
using sus.Game.Rulesets.Mods;
using sus.Game.Rulesets.Replays;
using sus.Game.Rulesets.Scoring;
using sus.Game.Rulesets.Taiko.Mods;
using sus.Game.Rulesets.Taiko.Objects;
using sus.Game.Rulesets.Taiko.Objects.Drawables;
using sus.Game.Rulesets.Taiko.Replays;
using sus.Game.Rulesets.Taiko.Scoring;

namespace sus.Game.Rulesets.Taiko.Tests.Judgements
{
    public partial class TestSceneHitJudgements : JudgementTest
    {
        [Test]
        public void TestHitCentreHit()
        {
            const double hit_time = 1000;

            PerformTest(new List<ReplayFrame>
            {
                new TaikoReplayFrame(0),
                new TaikoReplayFrame(hit_time, TaikoAction.LeftCentre),
            }, CreateBeatmap(new Hit
            {
                Type = HitType.Centre,
                StartTime = hit_time
            }));

            AssertJudgementCount(1);
            AssertResult<Hit>(0, HitResult.Great);
        }

        [Test]
        public void TestHitWithBothKeysOnSameFrameDoesNotFallThroughToNextObject()
        {
            PerformTest(new List<ReplayFrame>
            {
                new TaikoReplayFrame(0),
                new TaikoReplayFrame(1000, TaikoAction.LeftCentre, TaikoAction.RightCentre),
            }, CreateBeatmap(new Hit
            {
                Type = HitType.Centre,
                StartTime = 1000,
            }, new Hit
            {
                Type = HitType.Centre,
                StartTime = 1020
            }));

            AssertJudgementCount(2);
            AssertResult<Hit>(0, HitResult.Great);
            AssertResult<Hit>(1, HitResult.Miss);
        }

        [Test]
        public void TestHitRimHit()
        {
            const double hit_time = 1000;

            PerformTest(new List<ReplayFrame>
            {
                new TaikoReplayFrame(0),
                new TaikoReplayFrame(hit_time, TaikoAction.LeftRim),
            }, CreateBeatmap(new Hit
            {
                Type = HitType.Rim,
                StartTime = hit_time
            }));

            AssertJudgementCount(1);
            AssertResult<Hit>(0, HitResult.Great);
        }

        [Test]
        public void TestMissHit()
        {
            const double hit_time = 1000;

            PerformTest(new List<ReplayFrame>
            {
                new TaikoReplayFrame(0)
            }, CreateBeatmap(new Hit
            {
                Type = HitType.Centre,
                StartTime = hit_time
            }));

            AssertJudgementCount(1);
            AssertResult<Hit>(0, HitResult.Miss);
        }

        [Test]
        public void TestHitStrongHitWithOneKey()
        {
            const double hit_time = 1000;

            PerformTest(new List<ReplayFrame>
            {
                new TaikoReplayFrame(0),
                new TaikoReplayFrame(hit_time, TaikoAction.LeftCentre),
            }, CreateBeatmap(new Hit
            {
                Type = HitType.Centre,
                StartTime = hit_time,
                IsStrong = true
            }));

            AssertJudgementCount(2);
            AssertResult<Hit>(0, HitResult.Great);
            AssertResult<StrongNestedHitObject>(0, HitResult.IgnoreMiss);
        }

        [Test]
        public void TestHitStrongHitWithBothKeys()
        {
            const double hit_time = 1000;

            PerformTest(new List<ReplayFrame>
            {
                new TaikoReplayFrame(0),
                new TaikoReplayFrame(hit_time, TaikoAction.LeftCentre, TaikoAction.RightCentre),
            }, CreateBeatmap(new Hit
            {
                Type = HitType.Centre,
                StartTime = hit_time,
                IsStrong = true
            }));

            AssertJudgementCount(2);
            AssertResult<Hit>(0, HitResult.Great);
            AssertResult<StrongNestedHitObject>(0, HitResult.LargeBonus);
        }

        [Test]
        public void TestMissStrongHit()
        {
            const double hit_time = 1000;

            PerformTest(new List<ReplayFrame>
            {
                new TaikoReplayFrame(0),
            }, CreateBeatmap(new Hit
            {
                Type = HitType.Centre,
                StartTime = hit_time,
                IsStrong = true
            }));

            AssertJudgementCount(2);
            AssertResult<Hit>(0, HitResult.Miss);
            AssertResult<StrongNestedHitObject>(0, HitResult.IgnoreMiss);
        }

        [Test]
        public void TestHighVelocityHit()
        {
            const double hit_time = 1000;

            var beatmap = CreateBeatmap(new Hit
            {
                Type = HitType.Centre,
                StartTime = hit_time,
            });

            beatmap.ControlPointInfo.Add(0, new TimingControlPoint { BeatLength = 6 });
            beatmap.ControlPointInfo.Add(0, new EffectControlPoint { ScrollSpeed = 10 });

            var hitWindows = new DefaultHitWindows();
            hitWindows.SetDifficulty(beatmap.Difficulty.OverallDifficulty);

            PerformTest(new List<ReplayFrame>
            {
                new TaikoReplayFrame(0),
                new TaikoReplayFrame(hit_time - (hitWindows.WindowFor(HitResult.Great) + 0.1), TaikoAction.LeftCentre),
            }, beatmap);

            AssertJudgementCount(1);
            AssertResult<Hit>(0, HitResult.Ok);
        }

        [Test]
        public void TestStrongHitOneKeyWithHidden()
        {
            const double hit_time = 1000;

            var beatmap = CreateBeatmap(new Hit
            {
                Type = HitType.Centre,
                StartTime = hit_time,
                IsStrong = true
            });

            var hitWindows = new TaikoHitWindows();
            hitWindows.SetDifficulty(beatmap.Difficulty.OverallDifficulty);

            PerformTest(new List<ReplayFrame>
            {
                new TaikoReplayFrame(0),
                new TaikoReplayFrame(hit_time + hitWindows.WindowFor(HitResult.Ok) - 1, TaikoAction.LeftCentre),
            }, beatmap, new Mod[] { new TaikoModHidden() });

            AssertJudgementCount(2);
            AssertResult<Hit>(0, HitResult.Ok);
            AssertResult<Hit.StrongNestedHit>(0, HitResult.IgnoreMiss);
        }

        [Test]
        public void TestStrongHitTwoKeysWithHidden()
        {
            const double hit_time = 1000;

            var beatmap = CreateBeatmap(new Hit
            {
                Type = HitType.Centre,
                StartTime = hit_time,
                IsStrong = true
            });

            var hitWindows = new TaikoHitWindows();
            hitWindows.SetDifficulty(beatmap.Difficulty.OverallDifficulty);

            PerformTest(new List<ReplayFrame>
            {
                new TaikoReplayFrame(0),
                new TaikoReplayFrame(hit_time + hitWindows.WindowFor(HitResult.Ok) - 1, TaikoAction.LeftCentre),
                new TaikoReplayFrame(hit_time + hitWindows.WindowFor(HitResult.Ok) + DrawableHit.StrongNestedHit.SECOND_HIT_WINDOW - 2, TaikoAction.LeftCentre, TaikoAction.RightCentre),
            }, beatmap, new Mod[] { new TaikoModHidden() });

            AssertJudgementCount(2);
            AssertResult<Hit>(0, HitResult.Ok);
            AssertResult<Hit.StrongNestedHit>(0, HitResult.LargeBonus);
        }
    }
}
