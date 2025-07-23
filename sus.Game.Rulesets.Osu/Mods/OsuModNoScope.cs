// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Diagnostics;
using System.Linq;
using sus.Framework.Bindables;
using sus.Framework.Localisation;
using sus.Framework.Utils;
using sus.Game.Beatmaps;
using sus.Game.Rulesets.Mods;
using sus.Game.Rulesets.Osu.Objects;
using sus.Game.Rulesets.Osu.UI;
using sus.Game.Rulesets.UI;
using sus.Game.Utils;

namespace sus.Game.Rulesets.Osu.Mods
{
    public class OsuModNoScope : ModNoScope, IUpdatableByPlayfield, IApplicableToBeatmap
    {
        public override LocalisableString Description => "Where's the cursor?";

        public override Type[] IncompatibleMods => new[] { typeof(OsuModBloom) };

        private PeriodTracker spinnerPeriods = null!;

        public override BindableInt HiddenComboCount { get; } = new BindableInt(10)
        {
            MinValue = 0,
            MaxValue = 50,
        };

        public void ApplyToBeatmap(IBeatmap beatmap)
        {
            spinnerPeriods = new PeriodTracker(beatmap.HitObjects.OfType<Spinner>().Select(b => new Period(b.StartTime - TRANSITION_DURATION, b.EndTime)));
        }

        public void Update(Playfield playfield)
        {
            var susPlayfield = (OsuPlayfield)playfield;
            Debug.Assert(susPlayfield.Cursor != null);

            bool shouldAlwaysShowCursor = IsBreakTime.Value || spinnerPeriods.IsInAny(susPlayfield.Clock.CurrentTime);
            float targetAlpha = shouldAlwaysShowCursor ? 1 : ComboBasedAlpha;
            float currentAlpha = (float)Interpolation.Lerp(susPlayfield.Cursor.Alpha, targetAlpha, Math.Clamp(susPlayfield.Time.Elapsed / TRANSITION_DURATION, 0, 1));

            susPlayfield.Cursor.Alpha = currentAlpha;
            susPlayfield.Smoke.Alpha = currentAlpha;
        }
    }
}
