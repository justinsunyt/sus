// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using sus.Framework.Bindables;
using sus.Framework.Graphics.Sprites;
using sus.Framework.Localisation;
using sus.Game.Configuration;
using sus.Game.Graphics;
using sus.Game.Screens.Play;

namespace sus.Game.Rulesets.Mods
{
    public abstract class ModNoFail : Mod, IApplicableFailOverride, IApplicableToHUD, IReadFromConfig
    {
        public override string Name => "No Fail";
        public override string Acronym => "NF";
        public override IconUsage? Icon => OsuIcon.ModNoFail;
        public override ModType Type => ModType.DifficultyReduction;
        public override LocalisableString Description => "You can't fail, no matter what.";
        public override double ScoreMultiplier => 0.5;
        public override Type[] IncompatibleMods => new[] { typeof(ModFailCondition), typeof(ModCinema) };
        public override bool Ranked => UsesDefaultConfiguration;
        public override bool ValidForFreestyleAsRequiredMod => true;

        private readonly Bindable<bool> showHealthBar = new Bindable<bool>();

        /// <summary>
        /// We never fail, 'yo.
        /// </summary>
        public bool PerformFail() => false;

        public bool RestartOnFail => false;

        public void ReadFromConfig(OsuConfigManager config)
        {
            config.BindWith(OsuSetting.ShowHealthDisplayWhenCantFail, showHealthBar);
        }

        public void ApplyToHUD(HUDOverlay overlay)
        {
            overlay.ShowHealthBar.BindTo(showHealthBar);
        }
    }
}
