// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using sus.Game.Rulesets.Difficulty.Preprocessing;
using sus.Game.Rulesets.Difficulty.Utils;
using sus.Game.Rulesets.Osu.Difficulty.Preprocessing;
using sus.Game.Rulesets.Osu.Objects;

namespace sus.Game.Rulesets.Osu.Difficulty.Evaluators
{
    public static class AimEvaluator
    {
        private const double wide_angle_multiplier = 1.5;
        private const double acute_angle_multiplier = 2.6;
        private const double slider_multiplier = 1.35;
        private const double velocity_change_multiplier = 0.75;
        private const double wiggle_multiplier = 1.02;

        /// <summary>
        /// Evaluates the difficulty of aiming the current object, based on:
        /// <list type="bullet">
        /// <item><description>cursor velocity to the current object,</description></item>
        /// <item><description>angle difficulty,</description></item>
        /// <item><description>sharp velocity increases,</description></item>
        /// <item><description>and slider difficulty.</description></item>
        /// </list>
        /// </summary>
        public static double EvaluateDifficultyOf(DifficultyHitObject current, bool withSliderTravelDistance)
        {
            if (current.BaseObject is Spinner || current.Index <= 1 || current.Previous(0).BaseObject is Spinner)
                return 0;

            var susCurrObj = (OsuDifficultyHitObject)current;
            var susLastObj = (OsuDifficultyHitObject)current.Previous(0);
            var susLastLastObj = (OsuDifficultyHitObject)current.Previous(1);

            const int radius = OsuDifficultyHitObject.NORMALISED_RADIUS;
            const int diameter = OsuDifficultyHitObject.NORMALISED_DIAMETER;

            // Calculate the velocity to the current hitobject, which starts with a base distance / time assuming the last object is a hitcircle.
            double currVelocity = susCurrObj.LazyJumpDistance / susCurrObj.StrainTime;

            // But if the last object is a slider, then we extend the travel velocity through the slider into the current object.
            if (susLastObj.BaseObject is Slider && withSliderTravelDistance)
            {
                double travelVelocity = susLastObj.TravelDistance / susLastObj.TravelTime; // calculate the slider velocity from slider head to slider end.
                double movementVelocity = susCurrObj.MinimumJumpDistance / susCurrObj.MinimumJumpTime; // calculate the movement velocity from slider end to current object

                currVelocity = Math.Max(currVelocity, movementVelocity + travelVelocity); // take the larger total combined velocity.
            }

            // As above, do the same for the previous hitobject.
            double prevVelocity = susLastObj.LazyJumpDistance / susLastObj.StrainTime;

            if (susLastLastObj.BaseObject is Slider && withSliderTravelDistance)
            {
                double travelVelocity = susLastLastObj.TravelDistance / susLastLastObj.TravelTime;
                double movementVelocity = susLastObj.MinimumJumpDistance / susLastObj.MinimumJumpTime;

                prevVelocity = Math.Max(prevVelocity, movementVelocity + travelVelocity);
            }

            double wideAngleBonus = 0;
            double acuteAngleBonus = 0;
            double sliderBonus = 0;
            double velocityChangeBonus = 0;
            double wiggleBonus = 0;

            double aimStrain = currVelocity; // Start strain with regular velocity.

            if (Math.Max(susCurrObj.StrainTime, susLastObj.StrainTime) < 1.25 * Math.Min(susCurrObj.StrainTime, susLastObj.StrainTime)) // If rhythms are the same.
            {
                if (susCurrObj.Angle != null && susLastObj.Angle != null)
                {
                    double currAngle = susCurrObj.Angle.Value;
                    double lastAngle = susLastObj.Angle.Value;

                    // Rewarding angles, take the smaller velocity as base.
                    double angleBonus = Math.Min(currVelocity, prevVelocity);

                    wideAngleBonus = calcWideAngleBonus(currAngle);
                    acuteAngleBonus = calcAcuteAngleBonus(currAngle);

                    // Penalize angle repetition.
                    wideAngleBonus *= 1 - Math.Min(wideAngleBonus, Math.Pow(calcWideAngleBonus(lastAngle), 3));
                    acuteAngleBonus *= 0.08 + 0.92 * (1 - Math.Min(acuteAngleBonus, Math.Pow(calcAcuteAngleBonus(lastAngle), 3)));

                    // Apply full wide angle bonus for distance more than one diameter
                    wideAngleBonus *= angleBonus * DifficultyCalculationUtils.Smootherstep(susCurrObj.LazyJumpDistance, 0, diameter);

                    // Apply acute angle bonus for BPM above 300 1/2 and distance more than one diameter
                    acuteAngleBonus *= angleBonus *
                                       DifficultyCalculationUtils.Smootherstep(DifficultyCalculationUtils.MillisecondsToBPM(susCurrObj.StrainTime, 2), 300, 400) *
                                       DifficultyCalculationUtils.Smootherstep(susCurrObj.LazyJumpDistance, diameter, diameter * 2);

                    // Apply wiggle bonus for jumps that are [radius, 3*diameter] in distance, with < 110 angle
                    // https://www.desmos.com/calculator/dp0v0nvowc
                    wiggleBonus = angleBonus
                                  * DifficultyCalculationUtils.Smootherstep(susCurrObj.LazyJumpDistance, radius, diameter)
                                  * Math.Pow(DifficultyCalculationUtils.ReverseLerp(susCurrObj.LazyJumpDistance, diameter * 3, diameter), 1.8)
                                  * DifficultyCalculationUtils.Smootherstep(currAngle, double.DegreesToRadians(110), double.DegreesToRadians(60))
                                  * DifficultyCalculationUtils.Smootherstep(susLastObj.LazyJumpDistance, radius, diameter)
                                  * Math.Pow(DifficultyCalculationUtils.ReverseLerp(susLastObj.LazyJumpDistance, diameter * 3, diameter), 1.8)
                                  * DifficultyCalculationUtils.Smootherstep(lastAngle, double.DegreesToRadians(110), double.DegreesToRadians(60));
                }
            }

            if (Math.Max(prevVelocity, currVelocity) != 0)
            {
                // We want to use the average velocity over the whole object when awarding differences, not the individual jump and slider path velocities.
                prevVelocity = (susLastObj.LazyJumpDistance + susLastLastObj.TravelDistance) / susLastObj.StrainTime;
                currVelocity = (susCurrObj.LazyJumpDistance + susLastObj.TravelDistance) / susCurrObj.StrainTime;

                // Scale with ratio of difference compared to 0.5 * max dist.
                double distRatio = Math.Pow(Math.Sin(Math.PI / 2 * Math.Abs(prevVelocity - currVelocity) / Math.Max(prevVelocity, currVelocity)), 2);

                // Reward for % distance up to 125 / strainTime for overlaps where velocity is still changing.
                double overlapVelocityBuff = Math.Min(diameter * 1.25 / Math.Min(susCurrObj.StrainTime, susLastObj.StrainTime), Math.Abs(prevVelocity - currVelocity));

                velocityChangeBonus = overlapVelocityBuff * distRatio;

                // Penalize for rhythm changes.
                velocityChangeBonus *= Math.Pow(Math.Min(susCurrObj.StrainTime, susLastObj.StrainTime) / Math.Max(susCurrObj.StrainTime, susLastObj.StrainTime), 2);
            }

            if (susLastObj.BaseObject is Slider)
            {
                // Reward sliders based on velocity.
                sliderBonus = susLastObj.TravelDistance / susLastObj.TravelTime;
            }

            aimStrain += wiggleBonus * wiggle_multiplier;

            // Add in acute angle bonus or wide angle bonus + velocity change bonus, whichever is larger.
            aimStrain += Math.Max(acuteAngleBonus * acute_angle_multiplier, wideAngleBonus * wide_angle_multiplier + velocityChangeBonus * velocity_change_multiplier);

            // Add in additional slider velocity bonus.
            if (withSliderTravelDistance)
                aimStrain += sliderBonus * slider_multiplier;

            return aimStrain;
        }

        private static double calcWideAngleBonus(double angle) => DifficultyCalculationUtils.Smoothstep(angle, double.DegreesToRadians(40), double.DegreesToRadians(140));

        private static double calcAcuteAngleBonus(double angle) => DifficultyCalculationUtils.Smoothstep(angle, double.DegreesToRadians(140), double.DegreesToRadians(40));
    }
}
