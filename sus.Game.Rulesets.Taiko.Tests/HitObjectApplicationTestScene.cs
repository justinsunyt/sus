// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

#nullable disable

using sus.Framework.Allocation;
using sus.Framework.Graphics;
using sus.Framework.Testing;
using sus.Framework.Timing;
using sus.Game.Beatmaps;
using sus.Game.Beatmaps.ControlPoints;
using sus.Game.Rulesets.Objects.Drawables;
using sus.Game.Rulesets.Taiko.Objects;
using sus.Game.Rulesets.UI.Scrolling;
using sus.Game.Tests.Visual;

namespace sus.Game.Rulesets.Taiko.Tests
{
    public abstract partial class HitObjectApplicationTestScene : OsuTestScene
    {
        [Cached(typeof(IScrollingInfo))]
        private ScrollingTestContainer.TestScrollingInfo info = new ScrollingTestContainer.TestScrollingInfo
        {
            Direction = { Value = ScrollingDirection.Left },
            TimeRange = { Value = 1000 },
        };

        private ScrollingHitObjectContainer hitObjectContainer;

        [BackgroundDependencyLoader]
        private void load()
        {
            Child = hitObjectContainer = new ScrollingHitObjectContainer
            {
                RelativeSizeAxes = Axes.X,
                Height = 200,
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Clock = new FramedClock(new StopwatchClock())
            };
        }

        [SetUpSteps]
        public void SetUp()
            => AddStep("clear SHOC", () => hitObjectContainer.Clear());

        protected void AddHitObject(DrawableHitObject hitObject)
            => AddStep("add to SHOC", () => hitObjectContainer.Add(hitObject));

        protected void RemoveHitObject(DrawableHitObject hitObject)
            => AddStep("remove from SHOC", () => hitObjectContainer.Remove(hitObject));

        protected TObject PrepareObject<TObject>(TObject hitObject)
            where TObject : TaikoHitObject
        {
            hitObject.ApplyDefaults(new ControlPointInfo(), new BeatmapDifficulty());
            return hitObject;
        }
    }
}
