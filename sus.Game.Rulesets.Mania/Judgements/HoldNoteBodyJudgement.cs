// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Game.Rulesets.Scoring;

namespace sus.Game.Rulesets.Mania.Judgements
{
    public class HoldNoteBodyJudgement : ManiaJudgement
    {
        public override HitResult MaxResult => HitResult.IgnoreHit;
        public override HitResult MinResult => HitResult.ComboBreak;
    }
}
