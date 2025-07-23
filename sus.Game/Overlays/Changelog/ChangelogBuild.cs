// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Framework.Graphics.Sprites;
using sus.Game.Graphics;
using sus.Game.Graphics.Containers;
using sus.Game.Online.API.Requests.Responses;
using System;
using System.Linq;
using sus.Game.Graphics.Sprites;
using sus.Framework.Allocation;

namespace sus.Game.Overlays.Changelog
{
    public partial class ChangelogBuild : FillFlowContainer
    {
        public required Action<APIChangelogBuild> SelectBuild { get; init; }

        protected readonly APIChangelogBuild Build;

        public readonly FillFlowContainer ChangelogEntries;

        public ChangelogBuild(APIChangelogBuild build)
        {
            Build = build;

            RelativeSizeAxes = Axes.X;
            AutoSizeAxes = Axes.Y;
            Direction = FillDirection.Vertical;
            Padding = new MarginPadding { Horizontal = WaveOverlayContainer.HORIZONTAL_PADDING };

            Children = new Drawable[]
            {
                CreateHeader(),
                ChangelogEntries = new FillFlowContainer
                {
                    RelativeSizeAxes = Axes.X,
                    AutoSizeAxes = Axes.Y,
                    Direction = FillDirection.Vertical,
                },
            };
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            foreach (var categoryEntries in Build.ChangelogEntries.GroupBy(b => b.Category).OrderBy(c => c.Key))
            {
                ChangelogEntries.Add(new OsuSpriteText
                {
                    Text = categoryEntries.Key,
                    Font = OsuFont.GetFont(weight: FontWeight.Bold, size: 18),
                    Margin = new MarginPadding { Top = 35, Bottom = 15 },
                });

                ChangelogEntries.AddRange(categoryEntries.Select(entry => new ChangelogEntry(entry)));
            }
        }

        protected virtual FillFlowContainer CreateHeader() => new FillFlowContainer
        {
            Anchor = Anchor.TopCentre,
            Origin = Anchor.TopCentre,
            AutoSizeAxes = Axes.Both,
            Direction = FillDirection.Vertical,
            Margin = new MarginPadding { Top = 20 },
            Child = new FillFlowContainer
            {
                Anchor = Anchor.TopCentre,
                Origin = Anchor.TopCentre,
                AutoSizeAxes = Axes.Both,
                Direction = FillDirection.Horizontal,
                Child = new OsuHoverContainer
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    AutoSizeAxes = Axes.Both,
                    Action = () => SelectBuild.Invoke(Build),
                    Child = new FillFlowContainer<SpriteText>
                    {
                        AutoSizeAxes = Axes.Both,
                        Margin = new MarginPadding { Horizontal = 40 },
                        Children = new[]
                        {
                            new OsuSpriteText
                            {
                                Text = Build.UpdateStream.DisplayName,
                                Font = OsuFont.GetFont(weight: FontWeight.Medium, size: 19),
                            },
                            new OsuSpriteText
                            {
                                Text = " ",
                                Font = OsuFont.GetFont(weight: FontWeight.Medium, size: 19),
                            },
                            new OsuSpriteText
                            {
                                Text = Build.DisplayVersion,
                                Font = OsuFont.GetFont(weight: FontWeight.Light, size: 19),
                                Colour = Build.UpdateStream.Colour,
                            },
                        }
                    }
                },
            }
        };
    }
}
