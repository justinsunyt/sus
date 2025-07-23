// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using sus.Framework.Bindables;
using sus.Framework.Localisation;
using sus.Game.Rulesets.Mods;
using sus.Framework.Utils;
using sus.Game.Rulesets.Catch.UI;
using sus.Game.Rulesets.UI;

namespace sus.Game.Rulesets.Catch.Mods
{
    public class CatchModNoScope : ModNoScope, IUpdatableByPlayfield
    {
        public override LocalisableString Description => "Where's the catcher?";

        public override BindableInt HiddenComboCount { get; } = new BindableInt(10)
        {
            MinValue = 0,
            MaxValue = 50,
        };

        public void Update(Playfield playfield)
        {
            var catchPlayfield = (CatchPlayfield)playfield;
            bool shouldAlwaysShowCatcher = IsBreakTime.Value;
            float targetAlpha = shouldAlwaysShowCatcher ? 1 : ComboBasedAlpha;

            // AlwaysPresent required for catcher to still act on input when fully hidden.
            catchPlayfield.CatcherArea.AlwaysPresent = true;
            catchPlayfield.CatcherArea.Alpha = (float)Interpolation.Lerp(catchPlayfield.CatcherArea.Alpha, targetAlpha, Math.Clamp(catchPlayfield.Time.Elapsed / TRANSITION_DURATION, 0, 1));
        }
    }
}
