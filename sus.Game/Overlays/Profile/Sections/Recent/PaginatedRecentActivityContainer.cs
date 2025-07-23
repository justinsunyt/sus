// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Graphics;
using sus.Game.Online.API.Requests;
using sus.Framework.Bindables;
using sus.Game.Online.API.Requests.Responses;
using sus.Game.Online.API;
using System.Collections.Generic;
using susTK;
using sus.Framework.Allocation;
using sus.Game.Resources.Localisation.Web;

namespace sus.Game.Overlays.Profile.Sections.Recent
{
    public partial class PaginatedRecentActivityContainer : PaginatedProfileSubsection<APIRecentActivity>
    {
        public PaginatedRecentActivityContainer(Bindable<UserProfileData?> user)
            : base(user, missingText: EventsStrings.Empty)
        {
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            ItemsContainer.Spacing = new Vector2(0, 8);
        }

        protected override APIRequest<List<APIRecentActivity>> CreateRequest(UserProfileData user, PaginationParameters pagination) =>
            new GetUserRecentActivitiesRequest(user.User.Id, pagination);

        protected override Drawable CreateDrawableItem(APIRecentActivity model) => new DrawableRecentActivity(model);
    }
}
