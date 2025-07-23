// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using sus.Framework.Graphics;
using sus.Game.Graphics;
using sus.Game.Online.API.Requests.Responses;

namespace sus.Game.Overlays.Dashboard.Home
{
    public partial class DashboardNewBeatmapPanel : DashboardBeatmapPanel
    {
        public DashboardNewBeatmapPanel(APIBeatmapSet beatmapSet)
            : base(beatmapSet)
        {
        }

        protected override Drawable CreateInfo() => new DrawableDate(BeatmapSet.Ranked ?? DateTimeOffset.Now, 10, false)
        {
            Colour = ColourProvider.Foreground1
        };
    }
}
