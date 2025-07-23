// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

#nullable disable

using NUnit.Framework;
using sus.Framework.Timing;
using sus.Game.Beatmaps;
using sus.Game.Beatmaps.ControlPoints;
using sus.Game.Rulesets.Osu.Objects;
using sus.Game.Rulesets.Osu.Objects.Drawables;
using sus.Game.Tests.Visual;
using susTK;

namespace sus.Game.Rulesets.Osu.Tests
{
    public partial class TestSceneHitCircleApplication : OsuTestScene
    {
        [Test]
        public void TestApplyNewCircle()
        {
            DrawableHitCircle dho = null;

            AddStep("create circle", () => Child = dho = new DrawableHitCircle(prepareObject(new HitCircle
            {
                Position = new Vector2(256, 192),
                IndexInCurrentCombo = 0
            }))
            {
                Clock = new FramedClock(new StopwatchClock())
            });

            AddStep("apply new circle", () => dho.Apply(prepareObject(new HitCircle
            {
                Position = new Vector2(128, 128),
                ComboIndex = 1,
            })));
        }

        private HitCircle prepareObject(HitCircle circle)
        {
            circle.ApplyDefaults(new ControlPointInfo(), new BeatmapDifficulty());
            return circle;
        }
    }
}
