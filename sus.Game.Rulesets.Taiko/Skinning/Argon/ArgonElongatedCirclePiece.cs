// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Allocation;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Colour;
using susTK.Graphics;

namespace sus.Game.Rulesets.Taiko.Skinning.Argon
{
    public partial class ArgonElongatedCirclePiece : ArgonCirclePiece
    {
        public ArgonElongatedCirclePiece()
        {
            RelativeSizeAxes = Axes.Y;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            AccentColour = ColourInfo.GradientVertical(
                new Color4(241, 161, 0, 255),
                new Color4(167, 111, 0, 255)
            );
        }

        protected override void Update()
        {
            base.Update();
            Width = Parent!.DrawSize.X + DrawHeight;
        }
    }
}
