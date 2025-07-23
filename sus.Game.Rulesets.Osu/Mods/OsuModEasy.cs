// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Localisation;
using sus.Game.Beatmaps;
using sus.Game.Rulesets.Mods;

namespace sus.Game.Rulesets.Osu.Mods
{
    public class OsuModEasy : ModEasyWithExtraLives
    {
        public override LocalisableString Description => @"Larger circles, more forgiving HP drain, less accuracy required, and extra lives!";

        public override void ApplyToDifficulty(BeatmapDifficulty difficulty)
        {
            base.ApplyToDifficulty(difficulty);

            difficulty.OverallDifficulty *= ADJUST_RATIO;
        }
    }
}
