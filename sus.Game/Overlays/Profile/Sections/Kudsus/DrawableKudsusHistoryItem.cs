// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using sus.Game.Graphics;
using sus.Game.Graphics.Containers;
using sus.Game.Online.API.Requests.Responses;
using sus.Game.Online.Chat;
using System;
using osuTK;

namespace sus.Game.Overlays.Profile.Sections.Kudsus
{
    public partial class DrawableKudsusHistoryItem : CompositeDrawable
    {
        private const int height = 25;

        private readonly APIKudsusHistory historyItem;
        private readonly LinkFlowContainer linkFlowContainer;
        private readonly DrawableDate date;

        public DrawableKudsusHistoryItem(APIKudsusHistory historyItem)
        {
            this.historyItem = historyItem;

            Height = height;
            RelativeSizeAxes = Axes.X;
            AddRangeInternal(new Drawable[]
            {
                linkFlowContainer = new LinkFlowContainer
                {
                    Anchor = Anchor.CentreLeft,
                    Origin = Anchor.CentreLeft,
                    AutoSizeAxes = Axes.Both,
                    Spacing = new Vector2(0, 3),
                },
                date = new DrawableDate(historyItem.CreatedAt)
                {
                    Anchor = Anchor.CentreRight,
                    Origin = Anchor.CentreRight,
                }
            });
        }

        [BackgroundDependencyLoader]
        private void load(OverlayColourProvider colourProvider)
        {
            date.Colour = colourProvider.Foreground1;
            var formattedSource = MessageFormatter.FormatText(getString(historyItem));
            linkFlowContainer.AddLinks(formattedSource.Text, formattedSource.Links);
        }

        private string getString(APIKudsusHistory item)
        {
            string amount = $"{Math.Abs(item.Amount)} kudsus";
            string post = $"[{item.Post.Title}]({item.Post.Url})";

            switch (item.Source)
            {
                case KudsusSource.AllowKudsus:
                    switch (item.Action)
                    {
                        case KudsusAction.Give:
                            return $"Received {amount} from kudsus deny repeal of modding post {post}";
                    }

                    break;

                case KudsusSource.DenyKudsus:
                    switch (item.Action)
                    {
                        case KudsusAction.Reset:
                            return $"Denied {amount} from modding post {post}";
                    }

                    break;

                case KudsusSource.Delete:
                    switch (item.Action)
                    {
                        case KudsusAction.Reset:
                            return $"Lost {amount} from modding post deletion of {post}";
                    }

                    break;

                case KudsusSource.Restore:
                    switch (item.Action)
                    {
                        case KudsusAction.Give:
                            return $"Received {amount} from modding post restoration of {post}";
                    }

                    break;

                case KudsusSource.Vote:
                    switch (item.Action)
                    {
                        case KudsusAction.Give:
                            return $"Received {amount} from obtaining votes in modding post of {post}";

                        case KudsusAction.Reset:
                            return $"Lost {amount} from losing votes in modding post of {post}";
                    }

                    break;

                case KudsusSource.Recalculate:
                    switch (item.Action)
                    {
                        case KudsusAction.Give:
                            return $"Received {amount} from votes recalculation in modding post of {post}";

                        case KudsusAction.Reset:
                            return $"Lost {amount} from votes recalculation in modding post of {post}";
                    }

                    break;

                case KudsusSource.Forum:

                    string giver = $"[{item.Giver?.Username}]({item.Giver?.Url})";

                    switch (historyItem.Action)
                    {
                        case KudsusAction.Give:
                            return $"Received {amount} from {giver} for a post at {post}";

                        case KudsusAction.Reset:
                            return $"Kudsus reset by {giver} for the post {post}";

                        case KudsusAction.Revoke:
                            return $"Denied kudsus by {giver} for the post {post}";
                    }

                    break;
            }

            return $"Unknown event ({amount} change)";
        }
    }
}
