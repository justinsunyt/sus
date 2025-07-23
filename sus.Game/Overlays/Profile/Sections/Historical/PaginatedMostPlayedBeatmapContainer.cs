// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using sus.Framework.Allocation;
using sus.Framework.Bindables;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Game.Online.API;
using sus.Game.Online.API.Requests;
using sus.Game.Online.API.Requests.Responses;
using sus.Game.Resources.Localisation.Web;
using APIUser = sus.Game.Online.API.Requests.Responses.APIUser;

namespace sus.Game.Overlays.Profile.Sections.Historical
{
    public partial class PaginatedMostPlayedBeatmapContainer : PaginatedProfileSubsection<APIUserMostPlayedBeatmap>
    {
        public PaginatedMostPlayedBeatmapContainer(Bindable<UserProfileData?> user)
            : base(user, UsersStrings.ShowExtraHistoricalMostPlayedTitle)
        {
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            ItemsContainer.Direction = FillDirection.Vertical;
        }

        protected override int GetCount(APIUser user) => user.BeatmapPlayCountsCount;

        protected override APIRequest<List<APIUserMostPlayedBeatmap>> CreateRequest(UserProfileData user, PaginationParameters pagination) =>
            new GetUserMostPlayedBeatmapsRequest(user.User.Id, pagination);

        protected override Drawable CreateDrawableItem(APIUserMostPlayedBeatmap mostPlayed) =>
            new DrawableMostPlayedBeatmap(mostPlayed);
    }
}
