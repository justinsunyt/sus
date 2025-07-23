// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Game.Rulesets.Mods;
using sus.Game.Rulesets.Objects.Drawables;
using sus.Game.Rulesets.Taiko.Objects;
using sus.Game.Rulesets.Taiko.Objects.Drawables;
using sus.Game.Rulesets.Taiko.UI;
using sus.Game.Rulesets.UI;

namespace sus.Game.Rulesets.Taiko.Mods
{
    public class TaikoModClassic : ModClassic, IApplicableToDrawableRuleset<TaikoHitObject>, IApplicableToDrawableHitObject
    {
        public void ApplyToDrawableRuleset(DrawableRuleset<TaikoHitObject> drawableRuleset)
        {
            var drawableTaikoRuleset = (DrawableTaikoRuleset)drawableRuleset;
            drawableTaikoRuleset.LockPlayfieldAspectRange.Value = false;
        }

        public void ApplyToDrawableHitObject(DrawableHitObject drawable)
        {
            if (drawable is DrawableTaikoHitObject hit)
                hit.SnapJudgementLocation = true;
        }
    }
}
