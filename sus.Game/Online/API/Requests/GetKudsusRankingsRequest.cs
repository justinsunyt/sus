// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.IO.Network;

namespace sus.Game.Online.API.Requests
{
    public class GetKudsusRankingsRequest : APIRequest<GetKudsusRankingsResponse>
    {
        private readonly int page;

        public GetKudsusRankingsRequest(int page = 1)
        {
            this.page = page;
        }

        protected override WebRequest CreateWebRequest()
        {
            var req = base.CreateWebRequest();

            req.AddParameter(@"page", page.ToString());

            return req;
        }

        protected override string Target => @"rankings/kudsus";
    }
}
