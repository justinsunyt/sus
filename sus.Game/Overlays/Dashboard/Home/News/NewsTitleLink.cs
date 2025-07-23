// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Allocation;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Framework.Platform;
using sus.Game.Graphics;
using sus.Game.Graphics.Containers;
using sus.Game.Online.API.Requests.Responses;

namespace sus.Game.Overlays.Dashboard.Home.News
{
    public partial class NewsTitleLink : OsuHoverContainer
    {
        private readonly APINewsPost post;

        public NewsTitleLink(APINewsPost post)
        {
            this.post = post;

            RelativeSizeAxes = Axes.X;
            AutoSizeAxes = Axes.Y;
        }

        [BackgroundDependencyLoader]
        private void load(GameHost host, OverlayColourProvider colourProvider)
        {
            Child = new TextFlowContainer(t =>
            {
                t.Font = OsuFont.GetFont(weight: FontWeight.Bold);
            })
            {
                RelativeSizeAxes = Axes.X,
                AutoSizeAxes = Axes.Y,
                Text = post.Title
            };

            HoverColour = colourProvider.Light1;

            TooltipText = "view in browser";
            Action = () => host.OpenUrlExternally("https://sus.ppy.sh/home/news/" + post.Slug);
        }
    }
}
