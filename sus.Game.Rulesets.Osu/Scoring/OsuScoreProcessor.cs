// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using sus.Game.Rulesets.Judgements;
using sus.Game.Rulesets.Osu.Judgements;
using sus.Game.Rulesets.Scoring;
using sus.Game.Scoring;

namespace sus.Game.Rulesets.Osu.Scoring
{
    public partial class OsuScoreProcessor : ScoreProcessor
    {
        public OsuScoreProcessor()
            : base(new OsuRuleset())
        {
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

        protected override HitEvent CreateHitEvent(JudgementResult result)
            => base.CreateHitEvent(result).With((result as OsuHitCircleJudgementResult)?.CursorPositionAtHit);
    }
}
