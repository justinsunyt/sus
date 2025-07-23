// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Bindables;
using sus.Framework.Graphics;
using sus.Game.Rulesets.Catch.Objects;
using sus.Game.Rulesets.Catch.UI;
using sus.Game.Rulesets.Mods;
using sus.Game.Rulesets.UI;
using susTK;

namespace sus.Game.Rulesets.Catch.Mods
{
    public partial class CatchModFlashlight : ModFlashlight<CatchHitObject>
    {
        public override double ScoreMultiplier => UsesDefaultConfiguration ? 1.12 : 1;

        public override BindableFloat SizeMultiplier { get; } = new BindableFloat(1)
        {
            MinValue = 0.5f,
            MaxValue = 1.5f,
            Precision = 0.1f
        };

        public override BindableBool ComboBasedSize { get; } = new BindableBool(true);

        public override float DefaultFlashlightSize => 325;

        protected override Flashlight CreateFlashlight() => new CatchFlashlight(this, playfield);

        private CatchPlayfield playfield = null!;

        public override void ApplyToDrawableRuleset(DrawableRuleset<CatchHitObject> drawableRuleset)
        {
            playfield = (CatchPlayfield)drawableRuleset.Playfield;
            base.ApplyToDrawableRuleset(drawableRuleset);
        }

        private partial class CatchFlashlight : Flashlight
        {
            private readonly CatchPlayfield playfield;

            public CatchFlashlight(CatchModFlashlight modFlashlight, CatchPlayfield playfield)
                : base(modFlashlight)
            {
                this.playfield = playfield;

                FlashlightSize = new Vector2(0, GetSize());
                FlashlightSmoothness = 1.4f;
            }

            protected override float GetComboScaleFor(int combo)
            {
                if (combo >= 200)
                    return 0.770f;
                if (combo >= 100)
                    return 0.885f;

                return 1.0f;
            }

            protected override void Update()
            {
                base.Update();

                FlashlightPosition = playfield.CatcherArea.ToSpaceOfOtherDrawable(playfield.Catcher.DrawPosition, this);
            }

            protected override void UpdateFlashlightSize(float size)
            {
                this.TransformTo(nameof(FlashlightSize), new Vector2(0, size), FLASHLIGHT_FADE_DURATION);
            }

            protected override string FragmentShader => "CircularFlashlight";
        }
    }
}
