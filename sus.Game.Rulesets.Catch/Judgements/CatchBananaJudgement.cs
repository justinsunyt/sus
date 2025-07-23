// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Game.Rulesets.Judgements;
using sus.Game.Rulesets.Scoring;

namespace sus.Game.Rulesets.Catch.Judgements
{
    public class CatchBananaJudgement : CatchJudgement
    {
        public override HitResult MaxResult => HitResult.LargeBonus;

        public override bool ShouldExplodeFor(JudgementResult result) => true;
    }
}
