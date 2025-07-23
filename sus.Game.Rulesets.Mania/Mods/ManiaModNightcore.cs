// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Game.Rulesets.Mania.Objects;
using sus.Game.Rulesets.Mods;

namespace sus.Game.Rulesets.Mania.Mods
{
    public class ManiaModNightcore : ModNightcore<ManiaHitObject>, IManiaRateAdjustmentMod
    {
        // For now, all rate-increasing mods should be given a 1x multiplier in mania because it doesn't always
        // make the map any harder and is more of a personal preference.
        // In the future, we can consider adjusting this by experimenting with not applying the hitwindow leniency.
        public override double ScoreMultiplier => 1;
    }
}
