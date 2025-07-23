// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Graphics.Cursor;
using sus.Framework.Graphics.Sprites;
using sus.Framework.Localisation;

namespace sus.Game.Graphics.Sprites
{
    /// <summary>
    /// A derived version of <see cref="OsuSpriteText"/> which automatically shows non-truncated text in tooltip when required.
    /// </summary>
    public sealed partial class TruncatingSpriteText : OsuSpriteText, IHasTooltip
    {
        /// <summary>
        /// Whether a tooltip should be shown with non-truncated text on hover.
        /// </summary>
        public bool ShowTooltip { get; init; } = true;

        public LocalisableString TooltipText => Text;

        public override bool HandlePositionalInput => IsTruncated && ShowTooltip;

        public TruncatingSpriteText()
        {
            ((SpriteText)this).Truncate = true;
        }
    }
}
