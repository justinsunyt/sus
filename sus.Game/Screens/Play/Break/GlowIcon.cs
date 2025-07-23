// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Allocation;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Sprites;
using sus.Game.Graphics;
using sus.Game.Graphics.Sprites;
using susTK;

namespace sus.Game.Screens.Play.Break
{
    public partial class GlowIcon : GlowingDrawable
    {
        private SpriteIcon icon = null!;

        public IconUsage Icon
        {
            set => icon.Icon = value;
            get => icon.Icon;
        }

        public new Vector2 Size
        {
            set => icon.Size = value;
            get => icon.Size;
        }

        public GlowIcon()
        {
            RelativePositionAxes = Axes.X;
        }

        [BackgroundDependencyLoader]
        private void load(OsuColour colours)
        {
            GlowColour = colours.BlueLighter;
        }

        protected override Drawable CreateDrawable() => icon = new SpriteIcon
        {
            Origin = Anchor.Centre,
            Anchor = Anchor.Centre,
            Shadow = false,
        };
    }
}
