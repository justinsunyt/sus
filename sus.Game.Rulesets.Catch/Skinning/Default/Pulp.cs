// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Bindables;
using sus.Framework.Extensions.Color4Extensions;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Effects;
using sus.Framework.Graphics.Shapes;
using susTK.Graphics;

namespace sus.Game.Rulesets.Catch.Skinning.Default
{
    public partial class Pulp : Circle
    {
        public readonly Bindable<Color4> AccentColour = new Bindable<Color4>();

        public Pulp()
        {
            RelativePositionAxes = Axes.Both;
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;

            Blending = BlendingParameters.Additive;
            Colour = Color4.White.Opacity(0.9f);
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            AccentColour.BindValueChanged(updateAccentColour, true);
        }

        private void updateAccentColour(ValueChangedEvent<Color4> colour)
        {
            EdgeEffect = new EdgeEffectParameters
            {
                Type = EdgeEffectType.Glow,
                Radius = DrawWidth / 2,
                Colour = colour.NewValue.Darken(0.2f).Opacity(0.75f)
            };
        }
    }
}
