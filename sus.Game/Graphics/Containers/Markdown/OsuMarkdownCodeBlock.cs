// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using Markdig.Syntax;
using sus.Framework.Allocation;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers.Markdown;
using sus.Framework.Graphics.Shapes;
using sus.Game.Overlays;

namespace sus.Game.Graphics.Containers.Markdown
{
    public partial class OsuMarkdownCodeBlock : MarkdownCodeBlock
    {
        // TODO : change to monospace font for this component
        public OsuMarkdownCodeBlock(CodeBlock codeBlock)
            : base(codeBlock)
        {
        }

        protected override Drawable CreateBackground() => new CodeBlockBackground();

        public override MarkdownTextFlowContainer CreateTextFlow() => new CodeBlockTextFlowContainer();

        private partial class CodeBlockBackground : Box
        {
            [BackgroundDependencyLoader]
            private void load(OverlayColourProvider colourProvider)
            {
                RelativeSizeAxes = Axes.Both;
                Colour = colourProvider.Background6;
            }
        }

        private partial class CodeBlockTextFlowContainer : OsuMarkdownTextFlowContainer
        {
            [BackgroundDependencyLoader]
            private void load(OverlayColourProvider colourProvider)
            {
                Colour = colourProvider.Light1;
                Margin = new MarginPadding(10);
            }
        }
    }
}
