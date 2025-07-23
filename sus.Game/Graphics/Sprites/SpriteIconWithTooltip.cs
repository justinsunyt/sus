// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Graphics.Cursor;
using sus.Framework.Graphics.Sprites;
using sus.Framework.Localisation;

namespace sus.Game.Graphics.Sprites
{
    /// <summary>
    /// A <see cref="SpriteIcon"/> with a publicly settable tooltip text.
    /// </summary>
    public partial class SpriteIconWithTooltip : SpriteIcon, IHasTooltip
    {
        public LocalisableString TooltipText { get; set; }
    }
}
