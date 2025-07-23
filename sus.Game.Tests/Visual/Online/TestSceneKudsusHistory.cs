// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Game.Overlays.Profile.Sections.Kudsus;
using System.Collections.Generic;
using System;
using sus.Framework.Graphics.Containers;
using sus.Framework.Allocation;
using sus.Framework.Graphics.Shapes;
using sus.Framework.Graphics;
using sus.Game.Online.API.Requests.Responses;
using sus.Framework.Extensions.IEnumerableExtensions;
using sus.Game.Overlays;

namespace sus.Game.Tests.Visual.Online
{
    public partial class TestSceneKudsusHistory : OsuTestScene
    {
        private readonly Box background;

        [Cached]
        private OverlayColourProvider colourProvider = new OverlayColourProvider(OverlayColourScheme.Pink);

        public TestSceneKudsusHistory()
        {
            FillFlowContainer<DrawableKudsusHistoryItem> content;

            AddRange(new Drawable[]
            {
                background = new Box
                {
                    RelativeSizeAxes = Axes.Both,
                },
                content = new FillFlowContainer<DrawableKudsusHistoryItem>
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.X,
                    Width = 0.7f,
                    AutoSizeAxes = Axes.Y,
                }
            });

            items.ForEach(t => content.Add(new DrawableKudsusHistoryItem(t)));
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            background.Colour = colourProvider.Background4;
        }

        private readonly IEnumerable<APIKudsusHistory> items = new[]
        {
            new APIKudsusHistory
            {
                Amount = 10,
                CreatedAt = new DateTimeOffset(new DateTime(2011, 11, 11)),
                Source = KudsusSource.DenyKudsus,
                Action = KudsusAction.Reset,
                Post = new APIKudsusHistory.ModdingPost
                {
                    Title = @"Random post 1",
                    Url = @"https://osu.ppy.sh/b/1234",
                },
                Giver = new APIKudsusHistory.KudsusGiver
                {
                    Username = @"Username1",
                    Url = @"https://osu.ppy.sh/u/1234"
                }
            },
            new APIKudsusHistory
            {
                Amount = 5,
                CreatedAt = new DateTimeOffset(new DateTime(2012, 10, 11)),
                Source = KudsusSource.Forum,
                Action = KudsusAction.Give,
                Post = new APIKudsusHistory.ModdingPost
                {
                    Title = @"Random post 2",
                    Url = @"https://osu.ppy.sh/b/1234",
                },
                Giver = new APIKudsusHistory.KudsusGiver
                {
                    Username = @"Username2",
                    Url = @"https://osu.ppy.sh/u/1234"
                }
            },
            new APIKudsusHistory
            {
                Amount = 8,
                CreatedAt = new DateTimeOffset(new DateTime(2013, 9, 11)),
                Source = KudsusSource.Forum,
                Action = KudsusAction.Reset,
                Post = new APIKudsusHistory.ModdingPost
                {
                    Title = @"Random post 3",
                    Url = @"https://osu.ppy.sh/b/1234",
                },
                Giver = new APIKudsusHistory.KudsusGiver
                {
                    Username = @"Username3",
                    Url = @"https://osu.ppy.sh/u/1234"
                }
            },
            new APIKudsusHistory
            {
                Amount = 7,
                CreatedAt = new DateTimeOffset(new DateTime(2014, 8, 11)),
                Source = KudsusSource.Forum,
                Action = KudsusAction.Revoke,
                Post = new APIKudsusHistory.ModdingPost
                {
                    Title = @"Random post 4",
                    Url = @"https://osu.ppy.sh/b/1234",
                },
                Giver = new APIKudsusHistory.KudsusGiver
                {
                    Username = @"Username4",
                    Url = @"https://osu.ppy.sh/u/1234"
                }
            },
            new APIKudsusHistory
            {
                Amount = 100,
                CreatedAt = new DateTimeOffset(new DateTime(2015, 7, 11)),
                Source = KudsusSource.Vote,
                Action = KudsusAction.Give,
                Post = new APIKudsusHistory.ModdingPost
                {
                    Title = @"Random post 5",
                    Url = @"https://osu.ppy.sh/b/1234",
                },
                Giver = new APIKudsusHistory.KudsusGiver
                {
                    Username = @"Username5",
                    Url = @"https://osu.ppy.sh/u/1234"
                }
            },
            new APIKudsusHistory
            {
                Amount = 20,
                CreatedAt = new DateTimeOffset(new DateTime(2016, 6, 11)),
                Source = KudsusSource.Vote,
                Action = KudsusAction.Reset,
                Post = new APIKudsusHistory.ModdingPost
                {
                    Title = @"Random post 6",
                    Url = @"https://osu.ppy.sh/b/1234",
                },
                Giver = new APIKudsusHistory.KudsusGiver
                {
                    Username = @"Username6",
                    Url = @"https://osu.ppy.sh/u/1234"
                }
            },
            new APIKudsusHistory
            {
                Amount = 11,
                CreatedAt = new DateTimeOffset(new DateTime(2016, 6, 11)),
                Source = KudsusSource.AllowKudsus,
                Action = KudsusAction.Give,
                Post = new APIKudsusHistory.ModdingPost
                {
                    Title = @"Random post 7",
                    Url = @"https://osu.ppy.sh/b/1234",
                },
                Giver = new APIKudsusHistory.KudsusGiver
                {
                    Username = @"Username7",
                    Url = @"https://osu.ppy.sh/u/1234"
                }
            },
            new APIKudsusHistory
            {
                Amount = 24,
                CreatedAt = new DateTimeOffset(new DateTime(2014, 6, 11)),
                Source = KudsusSource.Delete,
                Action = KudsusAction.Reset,
                Post = new APIKudsusHistory.ModdingPost
                {
                    Title = @"Random post 8",
                    Url = @"https://osu.ppy.sh/b/1234",
                },
                Giver = new APIKudsusHistory.KudsusGiver
                {
                    Username = @"Username8",
                    Url = @"https://osu.ppy.sh/u/1234"
                }
            },
            new APIKudsusHistory
            {
                Amount = 12,
                CreatedAt = new DateTimeOffset(new DateTime(2016, 6, 11)),
                Source = KudsusSource.Restore,
                Action = KudsusAction.Give,
                Post = new APIKudsusHistory.ModdingPost
                {
                    Title = @"Random post 9",
                    Url = @"https://osu.ppy.sh/b/1234",
                },
                Giver = new APIKudsusHistory.KudsusGiver
                {
                    Username = @"Username9",
                    Url = @"https://osu.ppy.sh/u/1234"
                }
            },
            new APIKudsusHistory
            {
                Amount = 2,
                CreatedAt = new DateTimeOffset(new DateTime(2012, 6, 11)),
                Source = KudsusSource.Recalculate,
                Action = KudsusAction.Give,
                Post = new APIKudsusHistory.ModdingPost
                {
                    Title = @"Random post 10",
                    Url = @"https://osu.ppy.sh/b/1234",
                },
                Giver = new APIKudsusHistory.KudsusGiver
                {
                    Username = @"Username10",
                    Url = @"https://osu.ppy.sh/u/1234"
                }
            },
            new APIKudsusHistory
            {
                Amount = 32,
                CreatedAt = new DateTimeOffset(new DateTime(2019, 8, 11)),
                Source = KudsusSource.Recalculate,
                Action = KudsusAction.Reset,
                Post = new APIKudsusHistory.ModdingPost
                {
                    Title = @"Random post 11",
                    Url = @"https://osu.ppy.sh/b/1234",
                },
                Giver = new APIKudsusHistory.KudsusGiver
                {
                    Username = @"Username11",
                    Url = @"https://osu.ppy.sh/u/1234"
                }
            }
        };
    }
}
