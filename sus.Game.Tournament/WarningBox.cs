// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Framework.Graphics.Shapes;
using sus.Game.Graphics;
using susTK.Graphics;

namespace sus.Game.Tournament
{
    internal partial class WarningBox : Container
    {
        public WarningBox(string text)
        {
            Masking = true;
            CornerRadius = 5;
            Depth = float.MinValue;
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            AutoSizeAxes = Axes.Both;

            Children = new Drawable[]
            {
                new Box
                {
                    Colour = Color4.Red,
                    RelativeSizeAxes = Axes.Both,
                },
                new TournamentSpriteText
                {
                    Text = text,
                    Font = OsuFont.Torus.With(weight: FontWeight.Bold),
                    Colour = Color4.White,
                    Padding = new MarginPadding(20)
                }
            };
        }
    }
}
