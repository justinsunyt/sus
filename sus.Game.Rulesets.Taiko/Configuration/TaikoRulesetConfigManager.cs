// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Game.Configuration;
using sus.Game.Rulesets.Configuration;

namespace sus.Game.Rulesets.Taiko.Configuration
{
    public class TaikoRulesetConfigManager : RulesetConfigManager<TaikoRulesetSetting>
    {
        public TaikoRulesetConfigManager(SettingsStore? settings, RulesetInfo ruleset, int? variant = null)
            : base(settings, ruleset, variant)
        {
        }

        protected override void InitialiseDefaults()
        {
            base.InitialiseDefaults();

            SetDefault(TaikoRulesetSetting.TouchControlScheme, TaikoTouchControlScheme.KDDK);
        }
    }

    public enum TaikoRulesetSetting
    {
        TouchControlScheme
    }
}
