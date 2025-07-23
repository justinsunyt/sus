// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Allocation;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Framework.Graphics.Shapes;
using sus.Game.Graphics;
using sus.Game.Rulesets.Judgements;
using sus.Game.Rulesets.Osu.Objects.Drawables;
using sus.Game.Rulesets.Scoring;
using susTK;

namespace sus.Game.Rulesets.Osu.Skinning.Default
{
    public partial class DefaultJudgementPieceSliderTickMiss : CompositeDrawable, IAnimatableJudgement
    {
        private readonly HitResult result;
        private Circle piece = null!;

        [Resolved]
        private OsuColour colours { get; set; } = null!;

        public DefaultJudgementPieceSliderTickMiss(HitResult result)
        {
            this.result = result;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            AddInternal(piece = new Circle
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Blending = BlendingParameters.Additive,
                Colour = colours.ForHitResult(result),
                Size = new Vector2(DrawableSliderTick.DEFAULT_TICK_SIZE)
            });
        }

        public void PlayAnimation()
        {
            this.ScaleTo(1.4f);
            this.ScaleTo(1f, 150, Easing.Out);

            this.FadeOutFromOne(600);
        }

        public Drawable? GetAboveHitObjectsProxiedContent() => piece.CreateProxy();
    }
}
