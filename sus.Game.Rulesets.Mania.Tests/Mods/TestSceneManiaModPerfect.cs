// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using NUnit.Framework;
using sus.Game.Beatmaps;
using sus.Game.Rulesets.Mania.Mods;
using sus.Game.Rulesets.Mania.Objects;
using sus.Game.Rulesets.Mania.Replays;
using sus.Game.Rulesets.Objects;
using sus.Game.Rulesets.Replays;
using sus.Game.Tests.Visual;

namespace sus.Game.Rulesets.Mania.Tests.Mods
{
    public partial class TestSceneManiaModPerfect : ModFailConditionTestScene
    {
        protected override Ruleset CreatePlayerRuleset() => new ManiaRuleset();

        public TestSceneManiaModPerfect()
            : base(new ManiaModPerfect())
        {
        }

        [TestCase(false)]
        [TestCase(true)]
        public void TestNote(bool shouldMiss) => CreateHitObjectTest(new HitObjectTestData(new Note { StartTime = 1000 }), shouldMiss);

        [TestCase(false)]
        [TestCase(true)]
        public void TestHoldNote(bool shouldMiss) => CreateHitObjectTest(new HitObjectTestData(new HoldNote { StartTime = 1000, EndTime = 3000 }), shouldMiss);

        [Test]
        public void TestPerfectHits([Values] bool requirePerfectHits) => CreateModTest(new ModTestData
        {
            Mod = new ManiaModPerfect
            {
                RequirePerfectHits = { Value = requirePerfectHits }
            },
            PassCondition = () => ((ModFailConditionTestPlayer)Player).CheckFailed(false),
            Autoplay = false,
            CreateBeatmap = () => new Beatmap
            {
                HitObjects = new List<HitObject>
                {
                    new Note
                    {
                        StartTime = 1000,
                    }
                },
            },
            ReplayFrames = new List<ReplayFrame>
            {
                new ManiaReplayFrame(1000, ManiaAction.Key1),
                new ManiaReplayFrame(2000)
            }
        });

        [Test]
        public void TestGreatHit([Values] bool requirePerfectHits) => CreateModTest(new ModTestData
        {
            Mod = new ManiaModPerfect
            {
                RequirePerfectHits = { Value = requirePerfectHits }
            },
            PassCondition = () => ((ModFailConditionTestPlayer)Player).CheckFailed(requirePerfectHits),
            Autoplay = false,
            CreateBeatmap = () => new Beatmap
            {
                HitObjects = new List<HitObject>
                {
                    new Note
                    {
                        StartTime = 1000,
                    }
                },
            },
            ReplayFrames = new List<ReplayFrame>
            {
                new ManiaReplayFrame(1020, ManiaAction.Key1),
                new ManiaReplayFrame(2000)
            }
        });

        [Test]
        public void TestBreakOnHoldNote() => CreateModTest(new ModTestData
        {
            Mod = new ManiaModPerfect(),
            PassCondition = () => ((ModFailConditionTestPlayer)Player).CheckFailed(true) && Player.Results.Count == 2,
            Autoplay = false,
            CreateBeatmap = () => new Beatmap
            {
                HitObjects = new List<HitObject>
                {
                    new HoldNote
                    {
                        StartTime = 1000,
                        EndTime = 3000,
                    },
                },
            },
            ReplayFrames = new List<ReplayFrame>
            {
                new ManiaReplayFrame(1000, ManiaAction.Key1),
                new ManiaReplayFrame(2000)
            }
        });
    }
}
