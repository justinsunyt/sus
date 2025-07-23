// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Bindables;
using sus.Framework.Localisation;
using sus.Game.Online.API.Requests.Responses;
using sus.Game.Resources.Localisation.Web;

namespace sus.Game.Overlays.Profile.Sections.Historical
{
    public partial class PlayHistorySubsection : ChartProfileSubsection
    {
        protected override LocalisableString GraphCounterName => UsersStrings.ShowExtraHistoricalMonthlyPlaycountsCountLabel;

        public PlayHistorySubsection(Bindable<UserProfileData?> user)
            : base(user, UsersStrings.ShowExtraHistoricalMonthlyPlaycountsTitle)
        {
        }

        protected override APIUserHistoryCount[]? GetValues(APIUser? user) => user?.MonthlyPlayCounts;
    }
}
