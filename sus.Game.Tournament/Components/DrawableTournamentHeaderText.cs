// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Allocation;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Framework.Graphics.Sprites;
using sus.Framework.Graphics.Textures;

namespace sus.Game.Tournament.Components
{
    public partial class DrawableTournamentHeaderText : CompositeDrawable
    {
        public DrawableTournamentHeaderText(bool center = true)
        {
            InternalChild = new TextSprite
            {
                Anchor = center ? Anchor.Centre : Anchor.TopLeft,
                Origin = center ? Anchor.Centre : Anchor.TopLeft,
            };

            Height = 22;
            RelativeSizeAxes = Axes.X;
        }

        private partial class TextSprite : Sprite
        {
            [BackgroundDependencyLoader]
            private void load(TextureStore textures)
            {
                RelativeSizeAxes = Axes.Both;
                FillMode = FillMode.Fit;

                Texture = textures.Get("header-text");
            }
        }
    }
}
