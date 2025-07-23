// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Graphics;
using sus.Game.Online.API.Requests;
using osu.Framework.Bindables;
using sus.Game.Online.API.Requests.Responses;
using sus.Game.Online.API;
using System.Collections.Generic;
using osu.Game.Resources.Localisation.Web;

namespace sus.Game.Overlays.Profile.Sections.Kudsus
{
    public partial class PaginatedKudsusHistoryContainer : PaginatedProfileSubsection<APIKudsusHistory>
    {
        public PaginatedKudsusHistoryContainer(Bindable<UserProfileData?> user)
            : base(user, missingText: UsersStrings.ShowExtraKudosuEntryEmpty)
        {
        }

        protected override APIRequest<List<APIKudsusHistory>> CreateRequest(UserProfileData user, PaginationParameters pagination)
            => new GetUserKudsusHistoryRequest(user.User.Id, pagination);

        protected override Drawable CreateDrawableItem(APIKudsusHistory item) => new DrawableKudsusHistoryItem(item);
    }
}
