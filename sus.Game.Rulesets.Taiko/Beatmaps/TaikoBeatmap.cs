// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Collections.Generic;
using System.Linq;
using sus.Game.Beatmaps;
using sus.Game.Localisation;
using sus.Game.Rulesets.Taiko.Objects;

namespace sus.Game.Rulesets.Taiko.Beatmaps
{
    public class TaikoBeatmap : Beatmap<TaikoHitObject>
    {
        public override IEnumerable<BeatmapStatistic> GetStatistics()
        {
            int hits = HitObjects.Count(s => s is Hit);
            int drumRolls = HitObjects.Count(s => s is DrumRoll);
            int swells = HitObjects.Count(s => s is Swell);
            int sum = Math.Max(1, hits + drumRolls);

            return new[]
            {
                new BeatmapStatistic
                {
                    Name = BeatmapStatisticStrings.Hits,
                    CreateIcon = () => new BeatmapStatisticIcon(BeatmapStatisticsIconType.Circles),
                    Content = hits.ToString(),
                    BarDisplayLength = hits / (float)sum,
                },
                new BeatmapStatistic
                {
                    Name = BeatmapStatisticStrings.Drumrolls,
                    CreateIcon = () => new BeatmapStatisticIcon(BeatmapStatisticsIconType.Sliders),
                    Content = drumRolls.ToString(),
                    BarDisplayLength = drumRolls / (float)sum,
                },
                new BeatmapStatistic
                {
                    Name = BeatmapStatisticStrings.Swells,
                    CreateIcon = () => new BeatmapStatisticIcon(BeatmapStatisticsIconType.Spinners),
                    Content = swells.ToString(),
                    BarDisplayLength = Math.Min(swells / 10f, 1),
                }
            };
        }
    }
}
