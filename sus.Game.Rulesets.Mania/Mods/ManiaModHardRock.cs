// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Game.Rulesets.Mania.Objects;
using sus.Game.Rulesets.Mania.Scoring;
using sus.Game.Rulesets.Mods;
using sus.Game.Rulesets.Objects;

namespace sus.Game.Rulesets.Mania.Mods
{
    public class ManiaModHardRock : ModHardRock, IApplicableToHitObject
    {
        public override double ScoreMultiplier => 1;
        public override bool Ranked => false;

        public const double HIT_WINDOW_DIFFICULTY_MULTIPLIER = 1.4;

        void IApplicableToHitObject.ApplyToHitObject(HitObject hitObject)
        {
            switch (hitObject)
            {
                case Note:
                    ((ManiaHitWindows)hitObject.HitWindows).DifficultyMultiplier = HIT_WINDOW_DIFFICULTY_MULTIPLIER;
                    break;

                case HoldNote hold:
                    ((ManiaHitWindows)hold.Head.HitWindows).DifficultyMultiplier = HIT_WINDOW_DIFFICULTY_MULTIPLIER;
                    ((ManiaHitWindows)hold.Tail.HitWindows).DifficultyMultiplier = HIT_WINDOW_DIFFICULTY_MULTIPLIER;
                    break;
            }
        }
    }
}
