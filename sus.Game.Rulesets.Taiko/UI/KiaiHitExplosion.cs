// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using sus.Game.Rulesets.Objects.Drawables;
using sus.Game.Rulesets.Taiko.Objects;
using sus.Game.Rulesets.Taiko.Skinning.Default;
using sus.Game.Skinning;
using osuTK;

namespace sus.Game.Rulesets.Taiko.UI
{
    public partial class KiaiHitExplosion : Container
    {
        public override bool RemoveWhenNotAlive => true;

        [Cached(typeof(DrawableHitObject))]
        public readonly DrawableHitObject JudgedObject;

        private readonly HitType hitType;

        private SkinnableDrawable skinnable = null!;

        public override double LifetimeStart => skinnable.Drawable.LifetimeStart;

        public override double LifetimeEnd => skinnable.Drawable.LifetimeEnd;

        public KiaiHitExplosion(DrawableHitObject judgedObject, HitType hitType)
        {
            JudgedObject = judgedObject;
            this.hitType = hitType;

            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;

            RelativeSizeAxes = Axes.Both;
            Size = new Vector2(TaikoHitObject.DEFAULT_SIZE, 1);
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            Child = skinnable = new SkinnableDrawable(new TaikoSkinComponentLookup(TaikoSkinComponents.TaikoExplosionKiai), _ => new DefaultKiaiHitExplosion(hitType));
        }
    }
}
