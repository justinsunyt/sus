// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using sus.Game.Rulesets.Catch.Objects;
using sus.Game.Rulesets.Edit;
using sus.Game.Rulesets.Objects;

namespace sus.Game.Rulesets.Catch.Edit
{
    public partial class CatchDistanceSnapProvider : ComposerDistanceSnapProvider
    {
        public override double ReadCurrentDistanceSnap(HitObject before, HitObject after)
        {
            // sus!catch's distance snap implementation is limited, in that a custom spacing cannot be specified.
            // Therefore this functionality is not currently used.
            //
            // The implementation below is probably correct but should be checked if/when exposed via controls.

            float expectedDistance = DurationToDistance(after.StartTime - before.GetEndTime(), before.StartTime);

            float previousEndX = (before as JuiceStream)?.EndX ?? ((CatchHitObject)before).EffectiveX;
            float actualDistance = Math.Abs(previousEndX - ((CatchHitObject)after).EffectiveX);

            return actualDistance / expectedDistance;
        }
    }
}
