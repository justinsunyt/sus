// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Framework.Graphics.Cursor;
using sus.Framework.Graphics.Shapes;
using sus.Framework.Input.Events;
using sus.Framework.Input.States;
using susTK;
using susTK.Input;

namespace sus.Game.Screens.Utility.SampleComponents
{
    public partial class LatencyCursorContainer : CursorContainer
    {
        protected override Drawable CreateCursor() => new LatencyCursor();

        public override bool IsPresent => base.IsPresent || Scheduler.HasPendingTasks;

        public LatencyCursorContainer()
        {
            State.Value = Visibility.Hidden;
        }

        protected override bool OnMouseMove(MouseMoveEvent e)
        {
            // Scheduling is required to ensure updating of cursor position happens in limited rate.
            // We can alternatively solve this by a PassThroughInputManager layer inside LatencyArea,
            // but that would mean including input lag to this test, which may not be desired.
            Schedule(() => base.OnMouseMove(e));
            return false;
        }

        private partial class LatencyCursor : LatencySampleComponent
        {
            public LatencyCursor()
            {
                AutoSizeAxes = Axes.Both;
                Origin = Anchor.Centre;

                InternalChild = new Circle { Size = new Vector2(40) };
            }

            protected override void UpdateAtLimitedRate(InputState inputState)
            {
                Colour = inputState.Mouse.IsPressed(MouseButton.Left) ? OverlayColourProvider.Content1 : OverlayColourProvider.Colour2;
            }
        }
    }
}
