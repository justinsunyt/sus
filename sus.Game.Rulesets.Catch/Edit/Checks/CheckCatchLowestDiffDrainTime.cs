// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Collections.Generic;
using sus.Game.Beatmaps;
using sus.Game.Rulesets.Edit.Checks;

namespace sus.Game.Rulesets.Catch.Edit.Checks
{
    public class CheckCatchLowestDiffDrainTime : CheckLowestDiffDrainTime
    {
        protected override IEnumerable<(DifficultyRating rating, double thresholdMs, string name)> GetThresholds()
        {
            // See lowest difficulty requirements in https://sus.ppy.sh/wiki/en/Ranking_criteria/sus%21catch#general
            yield return (DifficultyRating.Hard, new TimeSpan(0, 2, 30).TotalMilliseconds, "Platter");
            yield return (DifficultyRating.Insane, new TimeSpan(0, 3, 15).TotalMilliseconds, "Rain");
            yield return (DifficultyRating.Expert, new TimeSpan(0, 4, 0).TotalMilliseconds, "Overdose");
        }
    }
}
