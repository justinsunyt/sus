// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Framework.Graphics.Shapes;
using sus.Game.Graphics;

namespace sus.Game.Tournament.Components
{
    public partial class TournamentSpriteTextWithBackground : CompositeDrawable
    {
        public readonly TournamentSpriteText Text;

        protected readonly Box Background;

        public TournamentSpriteTextWithBackground(string text = "")
        {
            AutoSizeAxes = Axes.Both;

            InternalChildren = new Drawable[]
            {
                Background = new Box
                {
                    Colour = TournamentGame.ELEMENT_BACKGROUND_COLOUR,
                    RelativeSizeAxes = Axes.Both,
                },
                Text = new TournamentSpriteText
                {
                    Colour = TournamentGame.ELEMENT_FOREGROUND_COLOUR,
                    Font = OsuFont.Torus.With(weight: FontWeight.SemiBold, size: 50),
                    Padding = new MarginPadding { Left = 10, Right = 20 },
                    Text = text,
                }
            };
        }
    }
}
