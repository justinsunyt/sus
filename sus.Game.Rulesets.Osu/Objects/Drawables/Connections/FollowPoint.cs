// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Bindables;
using susTK;
using susTK.Graphics;
using sus.Framework.Extensions.Color4Extensions;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Framework.Graphics.Effects;
using sus.Framework.Graphics.Pooling;
using sus.Framework.Graphics.Shapes;
using sus.Game.Skinning;

namespace sus.Game.Rulesets.Osu.Objects.Drawables.Connections
{
    /// <summary>
    /// A single follow point positioned between two adjacent <see cref="DrawableOsuHitObject"/>s.
    /// </summary>
    public partial class FollowPoint : PoolableDrawable, IAnimationTimeReference
    {
        private const float width = 8;

        public override bool RemoveWhenNotAlive => false;

        public FollowPoint()
        {
            Origin = Anchor.Centre;

            InternalChild = new SkinnableDrawable(new OsuSkinComponentLookup(OsuSkinComponents.FollowPoint), _ => new CircularContainer
            {
                Masking = true,
                AutoSizeAxes = Axes.Both,
                EdgeEffect = new EdgeEffectParameters
                {
                    Type = EdgeEffectType.Glow,
                    Colour = Color4.White.Opacity(0.2f),
                    Radius = 4,
                },
                Child = new Box
                {
                    Size = new Vector2(width),
                    Blending = BlendingParameters.Additive,
                    Origin = Anchor.Centre,
                    Anchor = Anchor.Centre,
                    Alpha = 0.5f,
                }
            });
        }

        public Bindable<double> AnimationStartTime { get; } = new BindableDouble();
    }
}
