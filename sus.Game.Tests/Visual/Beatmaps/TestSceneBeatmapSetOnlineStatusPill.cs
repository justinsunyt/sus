// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using sus.Framework.Extensions.IEnumerableExtensions;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Framework.Testing;
using sus.Game.Beatmaps;
using sus.Game.Beatmaps.Drawables;
using sus.Game.Overlays;
using sus.Game.Tests.Visual.UserInterface;
using susTK;

namespace sus.Game.Tests.Visual.Beatmaps
{
    public partial class TestSceneBeatmapSetOnlineStatusPill : ThemeComparisonTestScene
    {
        private bool showUnknownStatus;
        private bool animated = true;

        protected override Drawable CreateContent() => new FillFlowContainer
        {
            AutoSizeAxes = Axes.Both,
            Anchor = Anchor.Centre,
            Origin = Anchor.Centre,
            Direction = FillDirection.Vertical,
            Spacing = new Vector2(0, 10),
            ChildrenEnumerable = Enum.GetValues(typeof(BeatmapOnlineStatus)).Cast<BeatmapOnlineStatus>().Select(status => new Container
            {
                RelativeSizeAxes = Axes.X,
                Height = 20,
                Children = new Drawable[]
                {
                    new BeatmapSetOnlineStatusPill
                    {
                        ShowUnknownStatus = showUnknownStatus,
                        Animated = animated,
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Status = status
                    },
                }
            })
        };

        private IEnumerable<BeatmapSetOnlineStatusPill> statusPills => this.ChildrenOfType<BeatmapSetOnlineStatusPill>();

        [Test]
        public void TestFixedWidth()
        {
            AddStep("create themed content", () => CreateThemedContent(OverlayColourScheme.Red));

            AddStep("set fixed width", () => statusPills.ForEach(pill =>
            {
                pill.AutoSizeAxes = Axes.Y;
                pill.Width = 90;
            }));

            AddStep("toggle show unknown", () =>
            {
                showUnknownStatus = !showUnknownStatus;
                CreateThemedContent(OverlayColourScheme.Red);
            });

            AddStep("toggle animate", () =>
            {
                animated = !animated;
                CreateThemedContent(OverlayColourScheme.Red);
            });

            AddStep("unset fixed width", () => statusPills.ForEach(pill => pill.AutoSizeAxes = Axes.Both));
        }

        [Test]
        public void TestChangeLabels()
        {
            AddStep("Change labels", () =>
            {
                foreach (var pill in this.ChildrenOfType<BeatmapSetOnlineStatusPill>())
                {
                    switch (pill.Status)
                    {
                        // cycle at end
                        case BeatmapOnlineStatus.Loved:
                            pill.Status = BeatmapOnlineStatus.LocallyModified;
                            break;

                        default:
                            pill.Status = (pill.Status + 1);
                            break;
                    }
                }
            });
        }
    }
}
