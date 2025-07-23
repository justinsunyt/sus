// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Allocation;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Framework.Graphics.Sprites;
using sus.Framework.Graphics.Textures;

namespace sus.Game.Tournament.Components
{
    public partial class DrawableTournamentHeaderLogo : CompositeDrawable
    {
        public DrawableTournamentHeaderLogo()
        {
            InternalChild = new LogoSprite();

            Height = 82;
            RelativeSizeAxes = Axes.X;
        }

        private partial class LogoSprite : Sprite
        {
            [BackgroundDependencyLoader]
            private void load(TextureStore textures)
            {
                RelativeSizeAxes = Axes.Both;
                FillMode = FillMode.Fit;

                Anchor = Anchor.Centre;
                Origin = Anchor.Centre;

                Texture = textures.Get("header-logo");
            }
        }
    }
}
