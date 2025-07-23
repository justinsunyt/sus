// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Allocation;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Sprites;
using sus.Framework.Graphics.Textures;
using sus.Framework.Localisation;
using sus.Game.Graphics.Containers;
using sus.Game.Online.API;
using sus.Game.Users;

namespace sus.Game.Overlays.Profile.Header.Components
{
    [LongRunningLoad]
    public partial class DrawableTournamentBanner : OsuClickableContainer
    {
        private const float banner_aspect_ratio = 60 / 1000f;
        private readonly TournamentBanner banner;

        public DrawableTournamentBanner(TournamentBanner banner)
        {
            this.banner = banner;
            RelativeSizeAxes = Axes.X;
        }

        [BackgroundDependencyLoader]
        private void load(LargeTextureStore textures, OsuGame? game, IAPIProvider api)
        {
            Child = new Sprite
            {
                RelativeSizeAxes = Axes.Both,
                Texture = textures.Get(banner.Image),
            };

            Action = () => game?.OpenUrlExternally($@"{api.Endpoints.WebsiteUrl}/community/tournaments/{banner.TournamentId}");
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            this.FadeInFromZero(200);
        }

        protected override void Update()
        {
            base.Update();
            Height = DrawWidth * banner_aspect_ratio;
        }

        public override LocalisableString TooltipText => "view in browser";
    }
}
