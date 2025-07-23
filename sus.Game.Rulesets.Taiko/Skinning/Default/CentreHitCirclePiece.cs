// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Allocation;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Framework.Graphics.Shapes;
using sus.Game.Rulesets.Taiko.Objects;
using susTK;

namespace sus.Game.Rulesets.Taiko.Skinning.Default
{
    public partial class CentreHitCirclePiece : CirclePiece
    {
        public CentreHitCirclePiece()
        {
            Add(new CentreHitSymbolPiece());
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            AccentColour = Hit.COLOUR_CENTRE;
        }

        /// <summary>
        /// The symbol used for centre hit pieces.
        /// </summary>
        public partial class CentreHitSymbolPiece : Container
        {
            public CentreHitSymbolPiece()
            {
                Anchor = Anchor.Centre;
                Origin = Anchor.Centre;

                RelativeSizeAxes = Axes.Both;
                Size = new Vector2(SYMBOL_SIZE);
                Padding = new MarginPadding(SYMBOL_BORDER);

                Children = new[]
                {
                    new Circle { RelativeSizeAxes = Axes.Both }
                };
            }
        }
    }
}
