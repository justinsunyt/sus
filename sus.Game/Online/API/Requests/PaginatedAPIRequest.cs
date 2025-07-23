// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Globalization;
using sus.Framework.IO.Network;

namespace sus.Game.Online.API.Requests
{
    public abstract class PaginatedAPIRequest<T> : APIRequest<T> where T : class
    {
        private readonly PaginationParameters pagination;

        protected PaginatedAPIRequest(PaginationParameters pagination)
        {
            this.pagination = pagination;
        }

        protected override WebRequest CreateWebRequest()
        {
            var req = base.CreateWebRequest();

            req.AddParameter("offset", pagination.Offset.ToString(CultureInfo.InvariantCulture));
            req.AddParameter("limit", pagination.Limit.ToString(CultureInfo.InvariantCulture));

            return req;
        }
    }
}
