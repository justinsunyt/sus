// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using sus.Game.Online.API.Requests.Responses;

namespace sus.Game.Online.API.Requests
{
    public class GetUserKudsusHistoryRequest : PaginatedAPIRequest<List<APIKudsusHistory>>
    {
        private readonly long userId;

        public GetUserKudsusHistoryRequest(long userId, PaginationParameters pagination)
            : base(pagination)
        {
            this.userId = userId;
        }

        protected override string Target => $"users/{userId}/kudsus";
    }
}
