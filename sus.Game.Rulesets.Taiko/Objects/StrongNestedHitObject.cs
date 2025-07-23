// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Game.Rulesets.Judgements;
using sus.Game.Rulesets.Scoring;
using sus.Game.Rulesets.Taiko.Judgements;

namespace sus.Game.Rulesets.Taiko.Objects
{
    /// <summary>
    /// Base type for nested strong hits.
    /// Used by <see cref="TaikoStrongableHitObject"/>s to represent their strong bonus scoring portions.
    /// </summary>
    public abstract class StrongNestedHitObject : TaikoHitObject
    {
        public readonly TaikoHitObject Parent;

        protected StrongNestedHitObject(TaikoHitObject parent)
        {
            Parent = parent;
        }

        public override Judgement CreateJudgement() => new TaikoStrongJudgement();

        protected override HitWindows CreateHitWindows() => HitWindows.Empty;
    }
}
