// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Allocation;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Framework.Graphics.Shapes;
using sus.Game.Graphics;
using sus.Game.Rulesets.Objects.Drawables;
using sus.Game.Rulesets.Scoring;
using sus.Game.Rulesets.Taiko.Objects;
using sus.Game.Rulesets.Taiko.UI;
using susTK;
using susTK.Graphics;

namespace sus.Game.Rulesets.Taiko.Skinning.Default
{
    internal partial class DefaultHitExplosion : CircularContainer, IAnimatableHitExplosion
    {
        private readonly HitResult result;

        private Box? body;

        [Resolved]
        private OsuColour colours { get; set; } = null!;

        public DefaultHitExplosion(HitResult result)
        {
            this.result = result;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            RelativeSizeAxes = Axes.Both;

            BorderColour = Color4.White;
            BorderThickness = 1;

            Blending = BlendingParameters.Additive;

            Alpha = 0.15f;
            Masking = true;

            if (!result.IsHit())
                return;

            InternalChildren = new[]
            {
                body = new Box
                {
                    RelativeSizeAxes = Axes.Both,
                }
            };

            updateColour();
        }

        private void updateColour(DrawableHitObject? judgedObject = null)
        {
            if (body == null)
                return;

            bool isRim = (judgedObject?.HitObject as Hit)?.Type == HitType.Rim;
            body.Colour = isRim ? colours.BlueDarker : colours.PinkDarker;
        }

        public void Animate(DrawableHitObject drawableHitObject)
        {
            updateColour(drawableHitObject);

            this.ScaleTo(3f, 1000, Easing.OutQuint);
            this.FadeOut(500);
        }

        public void AnimateSecondHit()
        {
            this.ResizeTo(new Vector2(TaikoStrongableHitObject.STRONG_SCALE), 50);
        }
    }
}
