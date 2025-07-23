// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using sus.Game.Online.API;
using sus.Game.Online.API.Requests.Responses;
using sus.Game.Skinning;
using osuTK.Graphics;

namespace sus.Game.Screens.Menu
{
    public partial class MenuLogoVisualisation : LogoVisualisation
    {
        private IBindable<APIUser> user = null!;
        private Bindable<Skin> skin = null!;

        [BackgroundDependencyLoader]
        private void load(IAPIProvider api, SkinManager skinManager)
        {
            user = api.LocalUser.GetBoundCopy();
            skin = skinManager.CurrentSkin.GetBoundCopy();

            user.ValueChanged += _ => UpdateColour();
            skin.BindValueChanged(_ => UpdateColour(), true);
        }

        protected virtual void UpdateColour()
        {
            if (user.Value?.IsSupporter ?? false)
                Colour = skin.Value.GetConfig<GlobalSkinColours, Color4>(GlobalSkinColours.MenuGlow)?.Value ?? Color4.White;
            else
                Colour = Color4.White;
        }
    }
}
