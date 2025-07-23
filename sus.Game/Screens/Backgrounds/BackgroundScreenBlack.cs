// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Graphics;
using sus.Framework.Graphics.Shapes;
using sus.Framework.Screens;
using susTK.Graphics;

namespace sus.Game.Screens.Backgrounds
{
    public partial class BackgroundScreenBlack : BackgroundScreen
    {
        public BackgroundScreenBlack()
        {
            InternalChild = new Box
            {
                Colour = Color4.Black,
                RelativeSizeAxes = Axes.Both,
            };
        }

        public override void OnEntering(ScreenTransitionEvent e)
        {
            Show();
        }
    }
}
