// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using sus.Framework.Localisation;
using sus.Game.Online.API.Requests.Responses;
using sus.Game.Resources.Localisation.Web;

namespace sus.Game.Overlays.Dashboard.Home
{
    public partial class DrawablePopularBeatmapList : DrawableBeatmapList
    {
        public DrawablePopularBeatmapList(List<APIBeatmapSet> beatmapSets)
            : base(beatmapSets)
        {
        }

        protected override DashboardBeatmapPanel CreateBeatmapPanel(APIBeatmapSet beatmapSet) => new DashboardPopularBeatmapPanel(beatmapSet);

        protected override LocalisableString Title => HomeStrings.UserBeatmapsPopular;
    }
}
