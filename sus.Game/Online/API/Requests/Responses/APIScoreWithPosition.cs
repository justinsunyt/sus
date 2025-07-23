// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

#nullable disable

using Newtonsoft.Json;
using sus.Game.Beatmaps;
using sus.Game.Rulesets;
using sus.Game.Scoring;

namespace sus.Game.Online.API.Requests.Responses
{
    public class APIScoreWithPosition
    {
        [JsonProperty(@"position")]
        public int? Position;

        [JsonProperty(@"score")]
        public SoloScoreInfo Score;

        public ScoreInfo CreateScoreInfo(RulesetStore rulesets, BeatmapInfo beatmap = null)
        {
            var score = Score.ToScoreInfo(rulesets, beatmap);
            score.Position = Position;
            return score;
        }
    }
}
