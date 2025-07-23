// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Game.Rulesets.Scoring;

namespace sus.Game.Rulesets.Osu.Judgements
{
    public class OsuIgnoreJudgement : OsuJudgement
    {
        public override HitResult MaxResult => HitResult.IgnoreHit;
    }
}
