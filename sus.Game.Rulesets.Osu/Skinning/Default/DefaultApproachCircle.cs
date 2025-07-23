// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Allocation;
using sus.Framework.Bindables;
using sus.Framework.Graphics.Sprites;
using sus.Framework.Graphics.Textures;
using sus.Game.Rulesets.Objects.Drawables;
using sus.Game.Rulesets.Osu.Objects;
using sus.Game.Skinning;
using susTK;
using susTK.Graphics;

namespace sus.Game.Rulesets.Osu.Skinning.Default
{
    public partial class DefaultApproachCircle : Sprite
    {
        [Resolved]
        private DrawableHitObject drawableObject { get; set; } = null!;

        private IBindable<Color4> accentColour = null!;

        [BackgroundDependencyLoader]
        private void load(TextureStore textures)
        {
            Texture = textures.Get(@"Gameplay/sus/approachcircle").WithMaximumSize(OsuHitObject.OBJECT_DIMENSIONS * 2);

            // In triangles and argon skins, we expanded hitcircles to take up the full 128 px which are clickable,
            // but still use the old approach circle sprite. To make it feel correct (ie. disappear as it collides
            // with the hitcircle, *not when it overlaps the border*) we need to expand it slightly.
            Scale = new Vector2(128 / 118f);
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            accentColour = drawableObject.AccentColour.GetBoundCopy();
            accentColour.BindValueChanged(colour => Colour = LegacyColourCompatibility.DisallowZeroAlpha(colour.NewValue), true);
        }
    }
}
