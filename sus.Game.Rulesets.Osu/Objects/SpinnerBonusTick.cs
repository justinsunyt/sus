// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Game.Rulesets.Judgements;
using sus.Game.Rulesets.Scoring;

namespace sus.Game.Rulesets.Osu.Objects
{
    public class SpinnerBonusTick : SpinnerTick
    {
        public override Judgement CreateJudgement() => new OsuSpinnerBonusTickJudgement();

        public class OsuSpinnerBonusTickJudgement : OsuSpinnerTickJudgement
        {
            public override HitResult MaxResult => HitResult.LargeBonus;
        }
    }
}
