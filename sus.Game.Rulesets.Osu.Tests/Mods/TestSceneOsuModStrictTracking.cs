// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using NUnit.Framework;
using sus.Game.Beatmaps;
using sus.Game.Rulesets.Objects;
using sus.Game.Rulesets.Osu.Mods;
using sus.Game.Rulesets.Osu.Objects;
using sus.Game.Rulesets.Osu.Replays;
using sus.Game.Rulesets.Replays;
using osuTK;

namespace sus.Game.Rulesets.Osu.Tests.Mods
{
    public partial class TestSceneOsuModStrictTracking : OsuModTestScene
    {
        [Test]
        public void TestSliderInput() => CreateModTest(new ModTestData
        {
            Mod = new OsuModStrictTracking(),
            Autoplay = false,
            CreateBeatmap = () => new Beatmap
            {
                HitObjects = new List<HitObject>
                {
                    new Slider
                    {
                        StartTime = 1000,
                        Path = new SliderPath
                        {
                            ControlPoints =
                            {
                                new PathControlPoint(),
                                new PathControlPoint(new Vector2(0, 100))
                            }
                        }
                    }
                }
            },
            ReplayFrames = new List<ReplayFrame>
            {
                new OsuReplayFrame(0, new Vector2(), OsuAction.LeftButton),
                new OsuReplayFrame(500, new Vector2(200, 0), OsuAction.LeftButton),
                new OsuReplayFrame(501, new Vector2(200, 0)),
                new OsuReplayFrame(1000, new Vector2(), OsuAction.LeftButton),
                new OsuReplayFrame(1750, new Vector2(0, 100), OsuAction.LeftButton),
                new OsuReplayFrame(1751, new Vector2(0, 100)),
            },
            PassCondition = () => Player.ScoreProcessor.Combo.Value == 2
        });

        [Test]
        public void TestRewind()
        {
            bool seekedBack = false;
            bool missRecorded = false;

            CreateModTest(new ModTestData
            {
                Mod = new OsuModStrictTracking(),
                Autoplay = false,
                CreateBeatmap = () => new Beatmap
                {
                    HitObjects = new List<HitObject>
                    {
                        new Slider
                        {
                            StartTime = 1000,
                            Path = new SliderPath
                            {
                                ControlPoints =
                                {
                                    new PathControlPoint(),
                                    new PathControlPoint(new Vector2(0, 100))
                                }
                            }
                        }
                    }
                },
                ReplayFrames = new List<ReplayFrame>
                {
                    new OsuReplayFrame(0, new Vector2(100, 0)),
                    new OsuReplayFrame(1000, new Vector2(100, 0)),
                    new OsuReplayFrame(1050, new Vector2()),
                    new OsuReplayFrame(1100, new Vector2(), OsuAction.LeftButton),
                    new OsuReplayFrame(1750, new Vector2(0, 100), OsuAction.LeftButton),
                    new OsuReplayFrame(1751, new Vector2(0, 100)),
                },
                PassCondition = () => seekedBack && !missRecorded,
            });
            AddStep("subscribe to new judgements", () => Player.ScoreProcessor.NewJudgement += j =>
            {
                if (!j.IsHit)
                    missRecorded = true;
            });
            AddUntilStep("wait for gameplay completion", () => Player.GameplayState.HasCompleted);
            AddAssert("no misses", () => missRecorded, () => Is.False);
            AddStep("seek back", () =>
            {
                Player.GameplayClockContainer.Stop();
                Player.Seek(1040);
                seekedBack = true;
            });
        }
    }
}
