// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Allocation;
using sus.Framework.Graphics;
using sus.Framework.Localisation;
using sus.Game.Localisation;
using sus.Game.Overlays.Settings;
using sus.Game.Rulesets.Taiko.Configuration;

namespace sus.Game.Rulesets.Taiko
{
    public partial class TaikoSettingsSubsection : RulesetSettingsSubsection
    {
        protected override LocalisableString Header => "sus!taiko";

        public TaikoSettingsSubsection(TaikoRuleset ruleset)
            : base(ruleset)
        {
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            var config = (TaikoRulesetConfigManager)Config;

            Children = new Drawable[]
            {
                new SettingsEnumDropdown<TaikoTouchControlScheme>
                {
                    LabelText = RulesetSettingsStrings.TouchControlScheme,
                    Current = config.GetBindable<TaikoTouchControlScheme>(TaikoRulesetSetting.TouchControlScheme)
                }
            };
        }
    }
}
