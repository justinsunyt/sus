// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using System.Linq;
using sus.Framework.Allocation;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Framework.Localisation;
using sus.Game.Graphics;
using sus.Game.Graphics.Sprites;
using sus.Game.Online.API.Requests.Responses;
using susTK;

namespace sus.Game.Overlays.Dashboard.Home
{
    public abstract partial class DrawableBeatmapList : CompositeDrawable
    {
        private readonly List<APIBeatmapSet> beatmapSets;

        protected DrawableBeatmapList(List<APIBeatmapSet> beatmapSets)
        {
            this.beatmapSets = beatmapSets;
        }

        [BackgroundDependencyLoader]
        private void load(OverlayColourProvider colourProvider)
        {
            FillFlowContainer flow;

            RelativeSizeAxes = Axes.X;
            AutoSizeAxes = Axes.Y;
            InternalChild = flow = new FillFlowContainer
            {
                RelativeSizeAxes = Axes.X,
                AutoSizeAxes = Axes.Y,
                Direction = FillDirection.Vertical,
                Spacing = new Vector2(0, 10),
                Children = new Drawable[]
                {
                    new OsuSpriteText
                    {
                        Font = OsuFont.GetFont(size: 16, weight: FontWeight.Bold),
                        Colour = colourProvider.Light1,
                        Text = Title
                    }
                }
            };

            flow.AddRange(beatmapSets.Select(CreateBeatmapPanel));
        }

        protected abstract LocalisableString Title { get; }

        protected abstract DashboardBeatmapPanel CreateBeatmapPanel(APIBeatmapSet beatmapSet);
    }
}
