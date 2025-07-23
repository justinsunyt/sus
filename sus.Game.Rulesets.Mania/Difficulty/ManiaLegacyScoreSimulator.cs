// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using System.Linq;
using sus.Game.Beatmaps;
using sus.Game.Rulesets.Mania.Beatmaps;
using sus.Game.Rulesets.Mania.Mods;
using sus.Game.Rulesets.Mods;
using sus.Game.Rulesets.Scoring.Legacy;

namespace sus.Game.Rulesets.Mania.Difficulty
{
    internal class ManiaLegacyScoreSimulator : ILegacyScoreSimulator
    {
        public LegacyScoreAttributes Simulate(IWorkingBeatmap workingBeatmap, IBeatmap playableBeatmap)
        {
            return new LegacyScoreAttributes
            {
                ComboScore = 1000000,
                MaxCombo = 0 // Max combo is mod-dependent, so any value here is insufficient.
            };
        }

        public double GetLegacyScoreMultiplier(IReadOnlyList<Mod> mods, LegacyBeatmapConversionDifficultyInfo difficulty)
        {
            bool scoreV2 = mods.Any(m => m is ModScoreV2);

            double multiplier = 1.0;

            foreach (var mod in mods)
            {
                switch (mod)
                {
                    case ManiaModNoFail:
                        multiplier *= scoreV2 ? 1.0 : 0.5;
                        break;

                    case ManiaModEasy:
                        multiplier *= 0.5;
                        break;

                    case ManiaModHalfTime:
                    case ManiaModDaycore:
                        multiplier *= 0.5;
                        break;
                }
            }

            if (new ManiaRuleset().RulesetInfo.Equals(difficulty.SourceRuleset))
                return multiplier;

            // Apply key mod multipliers.
            int originalColumns = ManiaBeatmapConverter.GetColumnCount(difficulty);
            int actualColumns = ManiaBeatmapConverter.GetColumnCount(difficulty, mods);

            if (actualColumns > originalColumns)
                multiplier *= 0.9;
            else if (actualColumns < originalColumns)
                multiplier *= 0.9 - 0.04 * (originalColumns - actualColumns);

            return multiplier;
        }
    }
}
