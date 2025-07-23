// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using sus.Framework.Localisation;
using sus.Game.Rulesets.Mods;
using sus.Game.Rulesets.Objects.Types;
using sus.Game.Rulesets.Osu.Objects;
using sus.Game.Rulesets.Osu.Objects.Drawables;
using sus.Game.Rulesets.Osu.UI;
using sus.Game.Rulesets.Replays;
using sus.Game.Rulesets.UI;
using sus.Game.Screens.Play;
using static sus.Game.Input.Handlers.ReplayInputHandler;

namespace sus.Game.Rulesets.Osu.Mods
{
    public class OsuModRelax : ModRelax, IUpdatableByPlayfield, IApplicableToDrawableRuleset<OsuHitObject>, IApplicableToPlayer, IHasNoTimedInputs
    {
        public override LocalisableString Description => @"You don't need to click. Give your clicking/tapping fingers a break from the heat of things.";

        public override Type[] IncompatibleMods =>
            base.IncompatibleMods.Concat(new[] { typeof(OsuModAutopilot), typeof(OsuModMagnetised), typeof(OsuModAlternate), typeof(OsuModSingleTap) }).ToArray();

        /// <summary>
        /// How early before a hitobject's start time to trigger a hit.
        /// </summary>
        public const float RELAX_LENIENCY = 12;

        private bool isDownState;
        private bool wasLeft;

        private OsuInputManager susInputManager = null!;

        private ReplayState<OsuAction> state = null!;
        private double lastStateChangeTime;

        private DrawableOsuRuleset ruleset = null!;
        private IPressHandler pressHandler = null!;

        private bool hasReplay;
        private bool legacyReplay;

        public void ApplyToDrawableRuleset(DrawableRuleset<OsuHitObject> drawableRuleset)
        {
            ruleset = (DrawableOsuRuleset)drawableRuleset;

            // grab the input manager for future use.
            susInputManager = ruleset.KeyBindingInputManager;
        }

        public void ApplyToPlayer(Player player)
        {
            if (susInputManager.ReplayInputHandler != null)
            {
                hasReplay = true;

                Debug.Assert(ruleset.ReplayScore != null);
                legacyReplay = ruleset.ReplayScore.ScoreInfo.IsLegacyScore;

                pressHandler = legacyReplay ? new LegacyReplayPressHandler(this) : new PressHandler(this);

                return;
            }

            pressHandler = new PressHandler(this);
            susInputManager.AllowGameplayInputs = false;
        }

        public void Update(Playfield playfield)
        {
            if (hasReplay && !legacyReplay)
                return;

            bool requiresHold = false;
            bool requiresHit = false;

            double time = playfield.Clock.CurrentTime;

            foreach (var h in playfield.HitObjectContainer.AliveObjects.OfType<DrawableOsuHitObject>())
            {
                // we are not yet close enough to the object.
                if (time < h.HitObject.StartTime - RELAX_LENIENCY)
                    break;

                // already hit or beyond the hittable end time.
                if (h.IsHit || (h.HitObject is IHasDuration hasEnd && time > hasEnd.EndTime))
                    continue;

                switch (h)
                {
                    case DrawableHitCircle circle:
                        handleHitCircle(circle);
                        break;

                    case DrawableSlider slider:
                        // Handles cases like "2B" beatmaps, where sliders may be overlapping and simply holding is not enough.
                        if (!slider.HeadCircle.IsHit)
                            handleHitCircle(slider.HeadCircle);

                        requiresHold |= slider.SliderInputManager.IsMouseInFollowArea(slider.Tracking.Value);
                        break;

                    case DrawableSpinner spinner:
                        requiresHold |= spinner.HitObject.SpinsRequired > 0;
                        break;
                }
            }

            if (requiresHit)
            {
                changeState(false);
                changeState(true);
            }

            if (requiresHold)
                changeState(true);
            else if (isDownState && time - lastStateChangeTime > AutoGenerator.KEY_UP_DELAY)
                changeState(false);

            void handleHitCircle(DrawableHitCircle circle)
            {
                if (!circle.HitArea.IsHovered)
                    return;

                Debug.Assert(circle.HitObject.HitWindows != null);
                requiresHit |= circle.HitObject.HitWindows.CanBeHit(time - circle.HitObject.StartTime);
            }

            void changeState(bool down)
            {
                if (isDownState == down)
                    return;

                isDownState = down;
                lastStateChangeTime = time;

                state = new ReplayState<OsuAction>
                {
                    PressedActions = new List<OsuAction>()
                };

                if (down)
                {
                    pressHandler.HandlePress(wasLeft);
                    wasLeft = !wasLeft;
                }
                else
                {
                    pressHandler.HandleRelease(wasLeft);
                }
            }
        }

        private interface IPressHandler
        {
            void HandlePress(bool wasLeft);
            void HandleRelease(bool wasLeft);
        }

        private class PressHandler : IPressHandler
        {
            private readonly OsuModRelax mod;

            public PressHandler(OsuModRelax mod)
            {
                this.mod = mod;
            }

            public void HandlePress(bool wasLeft)
            {
                mod.state.PressedActions.Add(wasLeft ? OsuAction.LeftButton : OsuAction.RightButton);
                mod.state.Apply(mod.susInputManager.CurrentState, mod.susInputManager);
            }

            public void HandleRelease(bool wasLeft)
            {
                mod.state.Apply(mod.susInputManager.CurrentState, mod.susInputManager);
            }
        }

        // legacy replays do not contain key-presses with Relax mod, so they need to be triggered by themselves.
        private class LegacyReplayPressHandler : IPressHandler
        {
            private readonly OsuModRelax mod;

            public LegacyReplayPressHandler(OsuModRelax mod)
            {
                this.mod = mod;
            }

            public void HandlePress(bool wasLeft)
            {
                mod.susInputManager.KeyBindingContainer.TriggerPressed(wasLeft ? OsuAction.LeftButton : OsuAction.RightButton);
            }

            public void HandleRelease(bool wasLeft)
            {
                // this intentionally releases right when `wasLeft` is true because `wasLeft` is set at point of press and not at point of release
                mod.susInputManager.KeyBindingContainer.TriggerReleased(wasLeft ? OsuAction.RightButton : OsuAction.LeftButton);
            }
        }
    }
}
