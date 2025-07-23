// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using Markdig.Syntax.Inlines;
using sus.Framework.Graphics.Containers.Markdown;
using sus.Framework.Graphics.Cursor;
using sus.Framework.Localisation;

namespace sus.Game.Graphics.Containers.Markdown
{
    public partial class OsuMarkdownImage : MarkdownImage, IHasTooltip
    {
        public LocalisableString TooltipText { get; }

        public OsuMarkdownImage(LinkInline linkInline)
            : base($"https://osu.ppy.sh/media-url?url={linkInline.Url}")
        {
            TooltipText = linkInline.Title;
        }
    }
}
