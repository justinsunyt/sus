// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Extensions.LocalisationExtensions;
using sus.Framework.Localisation;
using sus.Game.Graphics;
using sus.Game.Graphics.Sprites;

namespace sus.Game.Screens.Edit
{
    public partial class TableHeaderText : OsuSpriteText
    {
        public TableHeaderText(LocalisableString text)
        {
            Text = text.ToUpper();
            Font = OsuFont.GetFont(size: 12, weight: FontWeight.Bold);
        }
    }
}
