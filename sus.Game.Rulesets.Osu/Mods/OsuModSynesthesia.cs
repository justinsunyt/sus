// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Game.Beatmaps;
using sus.Game.Graphics;
using sus.Game.Rulesets.Mods;
using sus.Game.Rulesets.Objects;
using sus.Game.Rulesets.Objects.Drawables;
using sus.Game.Rulesets.Osu.Objects.Drawables;
using sus.Game.Screens.Edit;
using osuTK.Graphics;

namespace sus.Game.Rulesets.Osu.Mods
{
    /// <summary>
    /// Mod that colours <see cref="HitObject"/>s based on the musical division they are on
    /// </summary>
    public class OsuModSynesthesia : ModSynesthesia, IApplicableToBeatmap, IApplicableToDrawableHitObject
    {
        private readonly OsuColour colours = new OsuColour();

        private IBeatmap? currentBeatmap { get; set; }

        public void ApplyToBeatmap(IBeatmap beatmap)
        {
            //Store a reference to the current beatmap to look up the beat divisor when notes are drawn
            if (currentBeatmap != beatmap)
                currentBeatmap = beatmap;
        }

        public void ApplyToDrawableHitObject(DrawableHitObject d)
        {
            if (currentBeatmap == null) return;

            Color4? timingBasedColour = null;

            d.HitObjectApplied += _ =>
            {
                // slider tails are a painful edge case, as their start time is offset 36ms back (see `LastTick`).
                // to work around this, look up the slider tail's parenting slider's end time instead to ensure proper snap.
                double snapTime = d is DrawableSliderTail tail
                    ? tail.Slider.GetEndTime()
                    : d.HitObject.StartTime;
                timingBasedColour = BindableBeatDivisor.GetColourFor(currentBeatmap.ControlPointInfo.GetClosestBeatDivisor(snapTime), colours);
            };

            // Need to set this every update to ensure it doesn't get overwritten by DrawableHitObject.OnApply() -> UpdateComboColour().
            d.OnUpdate += _ =>
            {
                if (timingBasedColour != null)
                    d.AccentColour.Value = timingBasedColour.Value;
            };
        }
    }
}
