// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using sus.Game.Graphics;
using sus.Game.Skinning;

namespace sus.Game.Screens.Play.HUD
{
    public partial class DefaultAccuracyCounter : GameplayAccuracyCounter, ISerialisableDrawable
    {
        public bool UsesFixedAnchor { get; set; }

        [BackgroundDependencyLoader]
        private void load(OsuColour colours)
        {
            Colour = colours.BlueLighter;
        }
    }
}
