// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Allocation;
using sus.Framework.Graphics;
using sus.Game.Graphics;

namespace sus.Game.Rulesets.Taiko.Skinning.Default
{
    public partial class ElongatedCirclePiece : CirclePiece
    {
        public ElongatedCirclePiece()
        {
            RelativeSizeAxes = Axes.Y;
        }

        [BackgroundDependencyLoader]
        private void load(OsuColour colours)
        {
            AccentColour = colours.YellowDark;
        }

        protected override void Update()
        {
            base.Update();
            Width = Parent!.DrawSize.X + DrawHeight;
        }
    }
}
