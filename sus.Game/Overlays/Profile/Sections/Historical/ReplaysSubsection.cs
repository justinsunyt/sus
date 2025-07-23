// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Bindables;
using osu.Framework.Localisation;
using sus.Game.Online.API.Requests.Responses;
using osu.Game.Resources.Localisation.Web;

namespace sus.Game.Overlays.Profile.Sections.Historical
{
    public partial class ReplaysSubsection : ChartProfileSubsection
    {
        protected override LocalisableString GraphCounterName => UsersStrings.ShowExtraHistoricalReplaysWatchedCountsCountLabel;

        public ReplaysSubsection(Bindable<UserProfileData?> user)
            : base(user, UsersStrings.ShowExtraHistoricalReplaysWatchedCountsTitle)
        {
        }

        protected override APIUserHistoryCount[]? GetValues(APIUser? user) => user?.ReplaysWatchedCounts;
    }
}
