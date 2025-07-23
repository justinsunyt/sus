// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Allocation;
using sus.Framework.Extensions.Color4Extensions;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Game.Graphics;
using sus.Game.Graphics.UserInterface;
using sus.Game.Online;
using susTK.Graphics;

namespace sus.Game.Beatmaps.Drawables
{
    public partial class DownloadProgressBar : CompositeDrawable
    {
        private readonly ProgressBar progressBar;
        private readonly BeatmapDownloadTracker downloadTracker;

        public DownloadProgressBar(IBeatmapSetInfo beatmapSet)
        {
            InternalChildren = new Drawable[]
            {
                progressBar = new ProgressBar(false)
                {
                    Height = 0,
                    Alpha = 0,
                },
                downloadTracker = new BeatmapDownloadTracker(beatmapSet),
            };

            AutoSizeAxes = Axes.Y;
            RelativeSizeAxes = Axes.X;
        }

        [BackgroundDependencyLoader(true)]
        private void load(OsuColour colours)
        {
            progressBar.FillColour = colours.Blue;
            progressBar.BackgroundColour = Color4.Black.Opacity(0.7f);
            progressBar.Current.BindTarget = downloadTracker.Progress;

            downloadTracker.State.BindValueChanged(state =>
            {
                switch (state.NewValue)
                {
                    case DownloadState.NotDownloaded:
                        progressBar.Current.Value = 0;
                        progressBar.FadeOut(500);
                        break;

                    case DownloadState.Downloading:
                        progressBar.FadeIn(400, Easing.OutQuint);
                        progressBar.ResizeHeightTo(4, 400, Easing.OutQuint);
                        break;

                    case DownloadState.Importing:
                        progressBar.FadeIn(400, Easing.OutQuint);
                        progressBar.ResizeHeightTo(4, 400, Easing.OutQuint);

                        progressBar.Current.Value = 1;
                        progressBar.FillColour = colours.Yellow;
                        break;

                    case DownloadState.LocallyAvailable:
                        progressBar.FadeOut(500);
                        break;
                }
            }, true);
        }
    }
}
