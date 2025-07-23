// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Framework.Graphics.Shapes;
using susTK.Graphics;

namespace sus.Game.Rulesets.Taiko.Edit.Blueprints
{
    public partial class LengthPiece : CompositeDrawable
    {
        public LengthPiece()
        {
            Origin = Anchor.CentreLeft;

            InternalChild = new Container
            {
                Masking = true,
                Colour = Color4.Yellow,
                RelativeSizeAxes = Axes.Both,
                Children = new Drawable[]
                {
                    new Box
                    {
                        RelativeSizeAxes = Axes.X,
                        Height = 8,
                    },
                    new Box
                    {
                        Origin = Anchor.BottomLeft,
                        Anchor = Anchor.BottomLeft,
                        RelativeSizeAxes = Axes.X,
                        Height = 8,
                    }
                }
            };
        }
    }
}
