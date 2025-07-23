// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Allocation;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Colour;
using sus.Framework.Graphics.Sprites;
using susTK;
using susTK.Graphics;

namespace sus.Game.Rulesets.Taiko.Skinning.Argon
{
    public partial class ArgonCentreCirclePiece : ArgonCirclePiece
    {
        [BackgroundDependencyLoader]
        private void load()
        {
            AccentColour = ColourInfo.GradientVertical(
                new Color4(241, 0, 0, 255),
                new Color4(167, 0, 0, 255)
            );

            AddInternal(new SpriteIcon
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                RelativeSizeAxes = Axes.Both,
                Icon = FontAwesome.Solid.AngleLeft,
                Size = new Vector2(ICON_SIZE),
                Scale = new Vector2(0.8f, 1)
            });
        }
    }
}
