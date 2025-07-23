// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Allocation;
using sus.Game.Input.Bindings;

namespace sus.Game.Overlays.Toolbar
{
    public partial class ToolbarSettingsButton : ToolbarOverlayToggleButton
    {
        public ToolbarSettingsButton()
        {
            ButtonContent.Width *= 1.4f;
            Hotkey = GlobalAction.ToggleSettings;
        }

        [BackgroundDependencyLoader(true)]
        private void load(SettingsOverlay settings)
        {
            StateContainer = settings;
        }
    }
}
