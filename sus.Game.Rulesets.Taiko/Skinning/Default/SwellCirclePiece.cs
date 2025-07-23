// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Allocation;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Framework.Graphics.Sprites;
using sus.Game.Graphics;
using susTK;

namespace sus.Game.Rulesets.Taiko.Skinning.Default
{
    public partial class SwellCirclePiece : CirclePiece
    {
        public SwellCirclePiece()
        {
            Add(new SwellSymbolPiece());
        }

        [BackgroundDependencyLoader]
        private void load(OsuColour colours)
        {
            AccentColour = colours.YellowDark;
        }

        /// <summary>
        /// The symbol used for swell pieces.
        /// </summary>
        public partial class SwellSymbolPiece : Container
        {
            public SwellSymbolPiece()
            {
                Anchor = Anchor.Centre;
                Origin = Anchor.Centre;

                RelativeSizeAxes = Axes.Both;
                Size = new Vector2(SYMBOL_SIZE);
                Padding = new MarginPadding(SYMBOL_BORDER);

                Children = new[]
                {
                    new SpriteIcon
                    {
                        RelativeSizeAxes = Axes.Both,
                        Icon = FontAwesome.Solid.Asterisk,
                        Shadow = false
                    }
                };
            }
        }
    }
}
