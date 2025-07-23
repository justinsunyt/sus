// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Allocation;
using sus.Framework.Extensions.Color4Extensions;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Framework.Graphics.Shapes;
using sus.Game.Graphics;
using susTK.Graphics;

namespace sus.Game.Rulesets.Taiko.UI
{
    internal partial class PlayfieldBackgroundLeft : CompositeDrawable
    {
        [BackgroundDependencyLoader]
        private void load(OsuColour colours)
        {
            RelativeSizeAxes = Axes.Both;
            InternalChildren = new Drawable[]
            {
                new Box
                {
                    Colour = colours.Gray1,
                    RelativeSizeAxes = Axes.Both,
                },
                new Box
                {
                    Anchor = Anchor.TopRight,
                    RelativeSizeAxes = Axes.Y,
                    Width = 10,
                    Colour = Framework.Graphics.Colour.ColourInfo.GradientHorizontal(Color4.Black.Opacity(0.6f), Color4.Black.Opacity(0)),
                },
            };
        }
    }
}
