// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using sus.Game.Localisation;
using sus.Game.Rulesets;

namespace sus.Game.Overlays.Settings.Sections.Input
{
    public partial class KeyBindingPanel : SettingsSubPanel
    {
        protected override Drawable CreateHeader() => new SettingsHeader(InputSettingsStrings.KeyBindingPanelHeader, InputSettingsStrings.KeyBindingPanelDescription);

        [BackgroundDependencyLoader(permitNulls: true)]
        private void load(RulesetStore rulesets)
        {
            AddSection(new GlobalKeyBindingsSection());

            foreach (var ruleset in rulesets.AvailableRulesets)
                AddSection(new RulesetBindingsSection(ruleset));
        }
    }
}
