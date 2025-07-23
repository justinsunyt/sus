// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Localisation;
using sus.Game.Configuration;
using sus.Game.Graphics.UserInterface;
using sus.Game.Overlays.Settings;
using sus.Game.Rulesets.Scoring;
using sus.Game.Scoring;
using sus.Game.Screens.Play;

namespace sus.Game.Rulesets.Mods
{
    public abstract class ModNoScope : Mod, IApplicableToScoreProcessor, IApplicableToPlayer
    {
        public override string Name => "No Scope";
        public override string Acronym => "NS";
        public override ModType Type => ModType.Fun;
        public override IconUsage? Icon => FontAwesome.Solid.EyeSlash;
        public override double ScoreMultiplier => 1;
        public override bool Ranked => true;

        /// <summary>
        /// Slightly higher than the cutoff for <see cref="Drawable.IsPresent"/>.
        /// </summary>
        protected const float MIN_ALPHA = 0.0002f;

        protected const float TRANSITION_DURATION = 100;

        protected readonly BindableNumber<int> CurrentCombo = new BindableInt();

        protected readonly IBindable<bool> IsBreakTime = new Bindable<bool>();

        protected float ComboBasedAlpha;

        [SettingSource(
            "Hidden at combo",
            "The combo count at which the cursor becomes completely hidden",
            SettingControlType = typeof(SettingsSlider<int, HiddenComboSlider>)
        )]
        public abstract BindableInt HiddenComboCount { get; }

        public ScoreRank AdjustRank(ScoreRank rank, double accuracy) => rank;

        public void ApplyToPlayer(Player player)
        {
            IsBreakTime.BindTo(player.IsBreakTime);
        }

        public void ApplyToScoreProcessor(ScoreProcessor scoreProcessor)
        {
            if (HiddenComboCount.Value == 0) return;

            CurrentCombo.BindTo(scoreProcessor.Combo);
            CurrentCombo.BindValueChanged(combo =>
            {
                ComboBasedAlpha = Math.Max(MIN_ALPHA, 1 - (float)combo.NewValue / HiddenComboCount.Value);
            }, true);
        }
    }

    public partial class HiddenComboSlider : RoundedSliderBar<int>
    {
        public override LocalisableString TooltipText => Current.Value == 0 ? "always hidden" : base.TooltipText;
    }
}
