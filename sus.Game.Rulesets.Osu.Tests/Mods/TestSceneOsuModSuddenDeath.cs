// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using NUnit.Framework;
using sus.Game.Beatmaps;
using sus.Game.Rulesets.Objects;
using sus.Game.Rulesets.Objects.Types;
using sus.Game.Rulesets.Osu.Mods;
using sus.Game.Rulesets.Osu.Objects;
using sus.Game.Rulesets.Osu.Replays;
using sus.Game.Rulesets.Replays;
using sus.Game.Tests.Visual;
using osuTK;

namespace sus.Game.Rulesets.Osu.Tests.Mods
{
    public partial class TestSceneOsuModSuddenDeath : ModFailConditionTestScene
    {
        protected override Ruleset CreatePlayerRuleset() => new OsuRuleset();

        public TestSceneOsuModSuddenDeath()
            : base(new OsuModSuddenDeath())
        {
        }

        [TestCase(true)]
        [TestCase(false)]
        public void TestMissTail(bool tailMiss) => CreateModTest(new ModTestData
        {
            Mod = new OsuModSuddenDeath
            {
                FailOnSliderTail = { Value = tailMiss }
            },
            PassCondition = () => ((ModFailConditionTestPlayer)Player).CheckFailed(tailMiss),
            Autoplay = false,
            CreateBeatmap = () => new Beatmap
            {
                HitObjects = new List<HitObject>
                {
                    new Slider
                    {
                        Position = new Vector2(256, 192),
                        StartTime = 1000,
                        Path = new SliderPath(PathType.LINEAR, new[] { Vector2.Zero, new Vector2(100, 0), })
                    },
                },
            },
            ReplayFrames = new List<ReplayFrame>
            {
                new OsuReplayFrame(1000, new Vector2(256, 192), OsuAction.LeftButton),
                new OsuReplayFrame(1001, new Vector2(256, 192)),
            }
        });

        [Test]
        public void TestMissTick() => CreateModTest(new ModTestData
        {
            Mod = new OsuModSuddenDeath(),
            PassCondition = () => ((ModFailConditionTestPlayer)Player).CheckFailed(true),
            Autoplay = false,
            CreateBeatmap = () => new Beatmap
            {
                HitObjects = new List<HitObject>
                {
                    new Slider
                    {
                        Position = new Vector2(256, 192),
                        StartTime = 1000,
                        Path = new SliderPath(PathType.LINEAR, new[] { Vector2.Zero, new Vector2(200, 0), })
                    },
                },
            },
            ReplayFrames = new List<ReplayFrame>
            {
                new OsuReplayFrame(1000, new Vector2(256, 192), OsuAction.LeftButton),
                new OsuReplayFrame(1001, new Vector2(256, 192)),
            }
        });
    }
}
