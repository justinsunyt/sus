// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Graphics;
using sus.Framework.Graphics.Colour;
using sus.Framework.Graphics.Containers;
using sus.Framework.Graphics.Sprites;
using sus.Game.Graphics;
using susTK;

namespace sus.Game.Rulesets.Osu.Skinning.Argon
{
    public partial class ArgonFollowPoint : CompositeDrawable
    {
        public ArgonFollowPoint()
        {
            Blending = BlendingParameters.Additive;

            Colour = ColourInfo.GradientVertical(Colour4.FromHex("FC618F"), Colour4.FromHex("BB1A41"));
            AutoSizeAxes = Axes.Both;

            InternalChildren = new Drawable[]
            {
                new SpriteIcon
                {
                    Icon = FontAwesome.Solid.ChevronRight,
                    Size = new Vector2(8),
                    Colour = OsuColour.Gray(0.2f),
                },
                new SpriteIcon
                {
                    Icon = FontAwesome.Solid.ChevronRight,
                    Size = new Vector2(8),
                    X = 4,
                },
            };
        }
    }
}
