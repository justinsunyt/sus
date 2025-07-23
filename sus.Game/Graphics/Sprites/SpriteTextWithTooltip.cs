// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Graphics.Cursor;
using sus.Framework.Localisation;

namespace sus.Game.Graphics.Sprites
{
    /// <summary>
    /// An <see cref="OsuSpriteText"/> with a publicly settable tooltip text.
    /// </summary>
    internal partial class SpriteTextWithTooltip : OsuSpriteText, IHasTooltip
    {
        public LocalisableString TooltipText { get; set; }
    }
}
