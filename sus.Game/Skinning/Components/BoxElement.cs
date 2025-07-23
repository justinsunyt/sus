// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using sus.Framework.Bindables;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Framework.Graphics.Shapes;
using sus.Game.Configuration;
using sus.Game.Localisation.SkinComponents;
using sus.Game.Overlays.Settings;
using susTK;
using susTK.Graphics;

namespace sus.Game.Skinning.Components
{
    public partial class BoxElement : CompositeDrawable, ISerialisableDrawable
    {
        public bool UsesFixedAnchor { get; set; }

        [SettingSource(typeof(SkinnableComponentStrings), nameof(SkinnableComponentStrings.CornerRadius), nameof(SkinnableComponentStrings.CornerRadiusDescription),
            SettingControlType = typeof(SettingsPercentageSlider<float>))]
        public new BindableFloat CornerRadius { get; } = new BindableFloat(0.25f)
        {
            MinValue = 0,
            MaxValue = 0.5f,
            Precision = 0.01f
        };

        [SettingSource(typeof(SkinnableComponentStrings), nameof(SkinnableComponentStrings.Colour))]
        public BindableColour4 AccentColour { get; } = new BindableColour4(Colour4.White);

        public BoxElement()
        {
            Size = new Vector2(400, 80);

            InternalChildren = new Drawable[]
            {
                new Box
                {
                    Colour = Color4.White,
                    RelativeSizeAxes = Axes.Both,
                },
            };

            Masking = true;
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            AccentColour.BindValueChanged(_ => Colour = AccentColour.Value, true);
        }

        protected override void Update()
        {
            base.Update();

            base.CornerRadius = CornerRadius.Value * Math.Min(DrawWidth, DrawHeight);
        }
    }
}
