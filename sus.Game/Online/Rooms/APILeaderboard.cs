// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

#nullable disable

using System.Collections.Generic;
using Newtonsoft.Json;
using sus.Game.Online.API.Requests.Responses;

namespace sus.Game.Online.Rooms
{
    public class APILeaderboard
    {
        [JsonProperty("leaderboard")]
        public List<APIUserScoreAggregate> Leaderboard;

        [JsonProperty("user_score")]
        public APIUserScoreAggregate UserScore;
    }
}
