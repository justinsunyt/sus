// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Allocation;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Game.Beatmaps;
using sus.Game.Graphics.Containers;

namespace sus.Game.Overlays.Profile.Sections
{
    /// <summary>
    /// Display artist/title/mapper information, commonly used as the left portion of a profile or score display row.
    /// </summary>
    public abstract partial class BeatmapMetadataContainer : OsuHoverContainer
    {
        private readonly IBeatmapInfo beatmapInfo;

        protected BeatmapMetadataContainer(IBeatmapInfo beatmapInfo)
        {
            this.beatmapInfo = beatmapInfo;

            AutoSizeAxes = Axes.Both;
        }

        [BackgroundDependencyLoader]
        private void load(BeatmapSetOverlay? beatmapSetOverlay)
        {
            Action = () =>
            {
                beatmapSetOverlay?.FetchAndShowBeatmap(beatmapInfo.OnlineID);
            };

            Child = new FillFlowContainer
            {
                AutoSizeAxes = Axes.Both,
                Children = CreateText(beatmapInfo),
            };
        }

        protected abstract Drawable[] CreateText(IBeatmapInfo beatmapInfo);
    }
}
