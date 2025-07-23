// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Linq;
using sus.Framework.Bindables;
using sus.Framework.Graphics;
using sus.Game.Configuration;
using sus.Game.Rulesets.Mods;
using sus.Game.Rulesets.Objects;
using sus.Game.Rulesets.Objects.Drawables;
using sus.Game.Rulesets.Osu.Objects;
using sus.Game.Rulesets.Osu.Objects.Drawables;
using sus.Game.Rulesets.Osu.Scoring;
using sus.Game.Rulesets.Osu.UI;
using sus.Game.Rulesets.Scoring;
using sus.Game.Rulesets.UI;

namespace sus.Game.Rulesets.Osu.Mods
{
    public class OsuModClassic : ModClassic, IApplicableToHitObject, IApplicableToDrawableHitObject, IApplicableToDrawableRuleset<OsuHitObject>, IApplicableHealthProcessor
    {
        public override Type[] IncompatibleMods => base.IncompatibleMods.Append(typeof(OsuModStrictTracking)).ToArray();

        [SettingSource("No slider head accuracy requirement", "Scores sliders proportionally to the number of ticks hit.")]
        public Bindable<bool> NoSliderHeadAccuracy { get; } = new BindableBool(true);

        [SettingSource("Apply classic note lock", "Applies note lock to the full hit window.")]
        public Bindable<bool> ClassicNoteLock { get; } = new BindableBool(true);

        [SettingSource("Always play a slider's tail sample", "Always plays a slider's tail sample regardless of whether it was hit or not.")]
        public Bindable<bool> AlwaysPlayTailSample { get; } = new BindableBool(true);

        [SettingSource("Fade out hit circles earlier", "Make hit circles fade out into a miss, rather than after it.")]
        public Bindable<bool> FadeHitCircleEarly { get; } = new Bindable<bool>(true);

        [SettingSource("Classic health", "More closely resembles the original HP drain mechanics.")]
        public Bindable<bool> ClassicHealth { get; } = new Bindable<bool>(true);

        private bool usingHiddenFading;

        public void ApplyToHitObject(HitObject hitObject)
        {
            switch (hitObject)
            {
                case Slider slider:
                    slider.ClassicSliderBehaviour = NoSliderHeadAccuracy.Value;
                    break;
            }
        }

        public void ApplyToDrawableRuleset(DrawableRuleset<OsuHitObject> drawableRuleset)
        {
            var susRuleset = (DrawableOsuRuleset)drawableRuleset;

            if (ClassicNoteLock.Value)
            {
                double hittableRange = OsuHitWindows.MISS_WINDOW - (drawableRuleset.Mods.OfType<OsuModAutopilot>().Any() ? 200 : 0);
                susRuleset.Playfield.HitPolicy = new LegacyHitPolicy(hittableRange);
            }

            usingHiddenFading = drawableRuleset.Mods.OfType<OsuModHidden>().SingleOrDefault()?.OnlyFadeApproachCircles.Value == false;
        }

        public void ApplyToDrawableHitObject(DrawableHitObject obj)
        {
            switch (obj)
            {
                case DrawableSliderHead head:
                    if (FadeHitCircleEarly.Value && !usingHiddenFading)
                        applyEarlyFading(head);

                    if (ClassicNoteLock.Value)
                        blockInputToObjectsUnderSliderHead(head);

                    break;

                case DrawableSliderTail tail:
                    tail.SamplePlaysOnlyOnHit = !AlwaysPlayTailSample.Value;
                    break;

                case DrawableHitCircle circle:
                    if (FadeHitCircleEarly.Value && !usingHiddenFading)
                        applyEarlyFading(circle);

                    break;
            }
        }

        /// <summary>
        /// On stable, slider heads that have already been hit block input from reaching objects that may be underneath them
        /// until the sliders they're part of have been fully judged.
        /// The purpose of this method is to restore that behaviour.
        /// In order to avoid introducing yet another confusing config option, this behaviour is roped into the general notion of "note lock".
        /// </summary>
        private static void blockInputToObjectsUnderSliderHead(DrawableSliderHead slider)
        {
            slider.HitArea.CanBeHit = () => !slider.DrawableSlider.AllJudged;
        }

        private void applyEarlyFading(DrawableHitCircle circle)
        {
            circle.ApplyCustomUpdateState += (dho, state) =>
            {
                using (dho.BeginAbsoluteSequence(dho.StateUpdateTime))
                {
                    if (state != ArmedState.Hit)
                    {
                        double okWindow = dho.HitObject.HitWindows.WindowFor(HitResult.Ok);
                        double lateMissFadeTime = dho.HitObject.HitWindows.WindowFor(HitResult.Meh) - okWindow;
                        dho.Delay(okWindow).FadeOut(lateMissFadeTime);
                    }
                }
            };
        }

        public HealthProcessor? CreateHealthProcessor(double drainStartTime) => ClassicHealth.Value ? new OsuLegacyHealthProcessor(drainStartTime) : null;
    }
}
