// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Allocation;
using sus.Game.Graphics;

namespace sus.Game.Overlays.Settings
{
    /// <summary>
    /// A <see cref="SettingsButton"/> with pink colours to mark dangerous/destructive actions.
    /// </summary>
    public partial class DangerousSettingsButton : SettingsButton
    {
        [BackgroundDependencyLoader]
        private void load(OsuColour colours)
        {
            BackgroundColour = colours.DangerousButtonColour;
        }
    }
}
