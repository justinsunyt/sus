// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

#nullable disable

using sus.Framework.Allocation;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Pooling;
using sus.Game.Rulesets.Judgements;
using sus.Game.Skinning;

namespace sus.Game.Rulesets.Mania.UI
{
    public partial class PoolableHitExplosion : PoolableDrawable
    {
        public const double DURATION = 200;

        public JudgementResult Result { get; private set; }

        private SkinnableDrawable skinnableExplosion;

        public PoolableHitExplosion()
        {
            RelativeSizeAxes = Axes.Both;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            InternalChild = skinnableExplosion = new SkinnableDrawable(new ManiaSkinComponentLookup(ManiaSkinComponents.HitExplosion), _ => new DefaultHitExplosion())
            {
                RelativeSizeAxes = Axes.Both
            };
        }

        public void Apply(JudgementResult result)
        {
            Result = result;
        }

        protected override void PrepareForUse()
        {
            base.PrepareForUse();

            LifetimeStart = Time.Current;

            (skinnableExplosion?.Drawable as IHitExplosion)?.Animate(Result);

            this.Delay(DURATION).Then().Expire();
        }
    }
}
