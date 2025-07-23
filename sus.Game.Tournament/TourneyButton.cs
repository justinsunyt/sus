// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using sus.Game.Overlays.Settings;

namespace sus.Game.Tournament
{
    public partial class TourneyButton : SettingsButton
    {
        public new Box Background => base.Background;

        [BackgroundDependencyLoader]
        private void load()
        {
            Padding = new MarginPadding();
        }
    }
}
