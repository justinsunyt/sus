// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Allocation;
using sus.Framework.Bindables;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Framework.Graphics.Shapes;
using sus.Game.Rulesets.Objects.Drawables;
using susTK;
using susTK.Graphics;

namespace sus.Game.Rulesets.Osu.Skinning.Argon
{
    public partial class ArgonSliderScorePoint : CircularContainer
    {
        private Bindable<Color4> accentColour = null!;

        public const float SIZE = 12;

        [BackgroundDependencyLoader]
        private void load(DrawableHitObject hitObject)
        {
            Masking = true;
            Origin = Anchor.Centre;
            Size = new Vector2(SIZE);
            BorderThickness = 3;
            BorderColour = Color4.White;
            Child = new Box
            {
                RelativeSizeAxes = Axes.Both,
                AlwaysPresent = true,
                Alpha = 0,
            };

            accentColour = hitObject.AccentColour.GetBoundCopy();
            accentColour.BindValueChanged(accent => BorderColour = accent.NewValue, true);
        }
    }
}
