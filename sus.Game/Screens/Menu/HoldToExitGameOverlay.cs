// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Input.Bindings;
using sus.Framework.Input.Events;
using sus.Game.Input.Bindings;
using sus.Game.Overlays;

namespace sus.Game.Screens.Menu
{
    public partial class HoldToExitGameOverlay : HoldToConfirmOverlay, IKeyBindingHandler<GlobalAction>
    {
        protected override bool AllowMultipleFires => true;

        public void Abort() => AbortConfirm();

        public HoldToExitGameOverlay()
            : base(0.7f)
        {
        }

        public bool OnPressed(KeyBindingPressEvent<GlobalAction> e)
        {
            if (e.Repeat)
                return false;

            if (e.Action == GlobalAction.Back)
            {
                BeginConfirm();
                return true;
            }

            return false;
        }

        public void OnReleased(KeyBindingReleaseEvent<GlobalAction> e)
        {
            if (e.Action == GlobalAction.Back)
            {
                if (!Fired)
                    AbortConfirm();
            }
        }
    }
}
