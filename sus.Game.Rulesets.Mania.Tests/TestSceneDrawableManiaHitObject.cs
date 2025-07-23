// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

#nullable disable

using NUnit.Framework;
using sus.Framework.Allocation;
using sus.Framework.Graphics;
using sus.Framework.Input.Events;
using sus.Framework.Timing;
using sus.Game.Beatmaps;
using sus.Game.Beatmaps.ControlPoints;
using sus.Game.Rulesets.Mania.Beatmaps;
using sus.Game.Rulesets.Mania.Objects;
using sus.Game.Rulesets.Mania.Objects.Drawables;
using sus.Game.Rulesets.Mania.UI;
using sus.Game.Rulesets.UI.Scrolling;
using sus.Game.Tests.Visual;
using susTK.Graphics;

namespace sus.Game.Rulesets.Mania.Tests
{
    public partial class TestSceneDrawableManiaHitObject : OsuTestScene
    {
        private readonly ManualClock clock = new ManualClock();

        private Column column;

        [Cached]
        private readonly StageDefinition stage = new StageDefinition(1);

        [SetUp]
        public void SetUp() => Schedule(() =>
        {
            Child = new ScrollingTestContainer(ScrollingDirection.Down)
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                AutoSizeAxes = Axes.X,
                RelativeSizeAxes = Axes.Y,
                TimeRange = 2000,
                Clock = new FramedClock(clock),
                Child = column = new Column(0, false)
                {
                    Action = { Value = ManiaAction.Key1 },
                    Height = 0.85f,
                    AccentColour = { Value = Color4.Gray },
                },
            };
        });

        [Test]
        public void TestHoldNoteHeadVisibility()
        {
            DrawableHoldNote note = null;
            AddStep("Add hold note", () =>
            {
                var h = new HoldNote
                {
                    StartTime = 0,
                    Duration = 1000
                };
                h.ApplyDefaults(new ControlPointInfo(), new BeatmapDifficulty());
                column.Add(note = new DrawableHoldNote(h));
            });
            AddStep("Hold key", () =>
            {
                clock.CurrentTime = 0;
                note.OnPressed(new KeyBindingPressEvent<ManiaAction>(GetContainingInputManager()!.CurrentState, ManiaAction.Key1));
            });
            AddStep("progress time", () => clock.CurrentTime = 500);
            AddAssert("head is visible", () => note.Head.Alpha == 1);
        }
    }
}
