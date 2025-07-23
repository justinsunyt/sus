// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Allocation;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Framework.Graphics.Sprites;
using sus.Framework.Graphics.Textures;

namespace sus.Game.Tournament.Screens.Showcase
{
    public partial class TournamentLogo : CompositeDrawable
    {
        public TournamentLogo()
        {
            RelativeSizeAxes = Axes.X;
            Margin = new MarginPadding { Vertical = 5 };

            Height = 100;
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore textures)
        {
            InternalChild = new Sprite
            {
                Anchor = Anchor.TopCentre,
                Origin = Anchor.TopCentre,
                FillMode = FillMode.Fit,
                RelativeSizeAxes = Axes.Both,
                Texture = textures.Get("header-logo"),
            };
        }
    }
}
