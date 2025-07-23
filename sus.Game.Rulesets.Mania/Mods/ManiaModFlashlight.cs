// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using sus.Framework.Bindables;
using sus.Framework.Graphics;
using sus.Framework.Layout;
using sus.Game.Rulesets.Mania.Objects;
using sus.Game.Rulesets.Mods;
using susTK;

namespace sus.Game.Rulesets.Mania.Mods
{
    public partial class ManiaModFlashlight : ModFlashlight<ManiaHitObject>
    {
        public override double ScoreMultiplier => 1;
        public override Type[] IncompatibleMods => new[] { typeof(ModHidden) };

        public override BindableFloat SizeMultiplier { get; } = new BindableFloat(1)
        {
            MinValue = 0.5f,
            MaxValue = 3f,
            Precision = 0.1f
        };

        public override BindableBool ComboBasedSize { get; } = new BindableBool();

        public override float DefaultFlashlightSize => 50;

        protected override Flashlight CreateFlashlight() => new ManiaFlashlight(this);

        private partial class ManiaFlashlight : Flashlight
        {
            private readonly LayoutValue flashlightProperties = new LayoutValue(Invalidation.DrawSize);

            public ManiaFlashlight(ManiaModFlashlight modFlashlight)
                : base(modFlashlight)
            {
                FlashlightSize = new Vector2(DrawWidth, GetSize());

                AddLayout(flashlightProperties);
            }

            protected override void Update()
            {
                base.Update();

                if (!flashlightProperties.IsValid)
                {
                    FlashlightSize = new Vector2(DrawWidth, FlashlightSize.Y);

                    FlashlightPosition = DrawPosition + DrawSize / 2;
                    flashlightProperties.Validate();
                }
            }

            protected override void UpdateFlashlightSize(float size)
            {
                this.TransformTo(nameof(FlashlightSize), new Vector2(DrawWidth, size), FLASHLIGHT_FADE_DURATION);
            }

            protected override string FragmentShader => "RectangularFlashlight";
        }
    }
}
