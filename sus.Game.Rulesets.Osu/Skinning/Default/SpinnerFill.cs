// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Framework.Graphics.Effects;
using sus.Framework.Graphics.Shapes;
using sus.Game.Graphics;
using susTK.Graphics;

namespace sus.Game.Rulesets.Osu.Skinning.Default
{
    public partial class SpinnerFill : CircularContainer, IHasAccentColour
    {
        public readonly Box Disc;

        public Color4 AccentColour
        {
            get => Disc.Colour;
            set
            {
                Disc.Colour = value;

                EdgeEffect = new EdgeEffectParameters
                {
                    Hollow = true,
                    Type = EdgeEffectType.Glow,
                    Radius = 40,
                    Colour = value,
                };
            }
        }

        public SpinnerFill()
        {
            RelativeSizeAxes = Axes.Both;
            Masking = true;

            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;

            Children = new Drawable[]
            {
                Disc = new Box
                {
                    Origin = Anchor.Centre,
                    Anchor = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    Alpha = 1,
                },
            };
        }
    }
}
