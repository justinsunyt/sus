// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Input.Bindings;
using osu.Framework.Input.Events;
using sus.Game.Input.Bindings;
using sus.Game.Screens.OnlinePlay.Match.Components;

namespace sus.Game.Screens.OnlinePlay.Match
{
    public partial class UserModSelectButton : PurpleRoundedButton, IKeyBindingHandler<GlobalAction>
    {
        public bool OnPressed(KeyBindingPressEvent<GlobalAction> e)
        {
            if (e.Action == GlobalAction.ToggleModSelection && !e.Repeat)
            {
                TriggerClick();
                return true;
            }

            return false;
        }

        public void OnReleased(KeyBindingReleaseEvent<GlobalAction> e) { }
    }
}
