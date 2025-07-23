// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Allocation;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Sprites;
using sus.Framework.Graphics.Textures;
using sus.Framework.Localisation;
using sus.Game.Graphics.Containers;
using sus.Game.Online;
using sus.Game.Users;
using susTK;

namespace sus.Game.Overlays.Profile.Header.Components
{
    [LongRunningLoad]
    public partial class DrawableBadge : OsuClickableContainer
    {
        public static readonly Vector2 DRAWABLE_BADGE_SIZE = new Vector2(86, 40);

        private readonly Badge badge;

        public DrawableBadge(Badge badge)
        {
            this.badge = badge;
            Size = DRAWABLE_BADGE_SIZE;
        }

        [BackgroundDependencyLoader]
        private void load(LargeTextureStore textures, ILinkHandler? linkHandler)
        {
            Child = new Sprite
            {
                FillMode = FillMode.Fit,
                RelativeSizeAxes = Axes.Both,
                Texture = textures.Get(badge.ImageUrl),
            };

            if (!string.IsNullOrEmpty(badge.Url))
                Action = () => linkHandler?.HandleLink(badge.Url);
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            this.FadeInFromZero(200);
        }

        public override LocalisableString TooltipText => badge.Description;
    }
}
