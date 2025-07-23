// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Graphics.Pooling;
using sus.Game.Rulesets.Scoring;

namespace sus.Game.Rulesets.Taiko.UI
{
    /// <summary>
    /// Pool for hit explosions of a specific type.
    /// </summary>
    internal partial class HitExplosionPool : DrawablePool<HitExplosion>
    {
        private readonly HitResult hitResult;

        public HitExplosionPool(HitResult hitResult)
            : base(15)
        {
            this.hitResult = hitResult;
        }

        protected override HitExplosion CreateNewDrawable() => new HitExplosion(hitResult);
    }
}
