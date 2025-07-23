// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using sus.Framework.Allocation;
using sus.Framework.Extensions;
using sus.Framework.Graphics.Cursor;
using sus.Framework.Graphics.Sprites;
using sus.Framework.Graphics.Textures;
using sus.Framework.Localisation;

namespace sus.Game.Users.Drawables
{
    public partial class DrawableFlag : Sprite, IHasTooltip
    {
        private readonly CountryCode countryCode;

        public LocalisableString TooltipText { get; }

        public DrawableFlag(CountryCode countryCode)
        {
            this.countryCode = countryCode;
            TooltipText = countryCode == CountryCode.Unknown ? string.Empty : countryCode.GetDescription();
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore ts)
        {
            ArgumentNullException.ThrowIfNull(ts);

            string textureName = countryCode == CountryCode.Unknown ? "__" : countryCode.ToString();
            Texture = ts.Get($@"Flags/{textureName}") ?? ts.Get(@"Flags/__");
        }
    }
}
