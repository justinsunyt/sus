// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Framework.Graphics.Shapes;
using susTK.Graphics;

namespace sus.Game.Rulesets.Taiko.Skinning.Argon
{
    public partial class ArgonPlayfieldBackgroundLeft : CompositeDrawable
    {
        public ArgonPlayfieldBackgroundLeft()
        {
            RelativeSizeAxes = Axes.Both;

            InternalChildren = new Drawable[]
            {
                new Box
                {
                    Colour = Color4.Black,
                    RelativeSizeAxes = Axes.Both,
                },
            };
        }
    }
}
