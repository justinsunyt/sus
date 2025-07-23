// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Collections.Generic;
using sus.Game.Rulesets.Judgements;
using sus.Game.Rulesets.Scoring;
using sus.Game.Rulesets.Taiko.Objects;
using sus.Game.Scoring;

namespace sus.Game.Rulesets.Taiko.Scoring
{
    public partial class TaikoScoreProcessor : ScoreProcessor
    {
        private const double combo_base = 4;

        public TaikoScoreProcessor()
            : base(new TaikoRuleset())
        {
        }

        protected override double ComputeTotalScore(double comboProgress, double accuracyProgress, double bonusPortion)
        {
            return 250000 * comboProgress
                   + 750000 * Math.Pow(Accuracy.Value, 3.6) * accuracyProgress
                   + bonusPortion;
        }

        protected override double GetBonusScoreChange(JudgementResult result) => base.GetBonusScoreChange(result) * strongScaleValue(result);

        protected override double GetComboScoreChange(JudgementResult result)
        {
            return GetBaseScoreForResult(result.Type)
                   * Math.Min(Math.Max(0.5, Math.Log(result.ComboAfterJudgement, combo_base)), Math.Log(400, combo_base))
                   * strongScaleValue(result);
        }

        public override ScoreRank RankFromScore(double accuracy, IReadOnlyDictionary<HitResult, int> results)
        {
            ScoreRank rank = base.RankFromScore(accuracy, results);

            switch (rank)
            {
                case ScoreRank.S:
                case ScoreRank.X:
                    if (results.GetValueOrDefault(HitResult.Miss) > 0)
                        rank = ScoreRank.A;
                    break;
            }

            return rank;
        }

        public override int GetBaseScoreForResult(HitResult result)
        {
            switch (result)
            {
                case HitResult.Ok:
                    return 150;
            }

            return base.GetBaseScoreForResult(result);
        }

        private double strongScaleValue(JudgementResult result)
        {
            if (result.HitObject is StrongNestedHitObject strong)
                return strong.Parent is DrumRollTick ? 3 : 7;

            return 1;
        }
    }
}
