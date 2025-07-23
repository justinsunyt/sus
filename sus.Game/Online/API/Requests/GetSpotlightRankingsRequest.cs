// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.IO.Network;
using sus.Game.Overlays.Rankings;
using sus.Game.Rulesets;

namespace sus.Game.Online.API.Requests
{
    public class GetSpotlightRankingsRequest : GetRankingsRequest<GetSpotlightRankingsResponse>
    {
        private readonly int spotlight;
        private readonly RankingsSortCriteria sort;

        public GetSpotlightRankingsRequest(RulesetInfo ruleset, int spotlight, RankingsSortCriteria sort)
            : base(ruleset)
        {
            this.spotlight = spotlight;
            this.sort = sort;
        }

        protected override WebRequest CreateWebRequest()
        {
            var req = base.CreateWebRequest();

            req.AddParameter("spotlight", spotlight.ToString());
            req.AddParameter("filter", sort.ToString().ToLowerInvariant());

            return req;
        }

        protected override string TargetPostfix() => "charts";
    }
}
