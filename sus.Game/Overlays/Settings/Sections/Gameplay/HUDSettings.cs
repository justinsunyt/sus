// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Allocation;
using sus.Framework.Graphics;
using sus.Framework.Localisation;
using sus.Game.Configuration;
using sus.Game.Localisation;

namespace sus.Game.Overlays.Settings.Sections.Gameplay
{
    public partial class HUDSettings : SettingsSubsection
    {
        protected override LocalisableString Header => GameplaySettingsStrings.HUDHeader;

        [BackgroundDependencyLoader]
        private void load(OsuConfigManager config)
        {
            Children = new Drawable[]
            {
                new SettingsEnumDropdown<HUDVisibilityMode>
                {
                    LabelText = GameplaySettingsStrings.HUDVisibilityMode,
                    Current = config.GetBindable<HUDVisibilityMode>(OsuSetting.HUDVisibilityMode)
                },
                new SettingsCheckbox
                {
                    LabelText = GameplaySettingsStrings.ShowReplaySettingsOverlay,
                    Current = config.GetBindable<bool>(OsuSetting.ReplaySettingsOverlay),
                    Keywords = new[] { "hide" },
                },
                new SettingsCheckbox
                {
                    LabelText = GameplaySettingsStrings.AlwaysShowKeyOverlay,
                    Current = config.GetBindable<bool>(OsuSetting.KeyOverlay),
                    Keywords = new[] { "counter" },
                },
                new SettingsCheckbox
                {
                    LabelText = GameplaySettingsStrings.AlwaysShowGameplayLeaderboard,
                    Current = config.GetBindable<bool>(OsuSetting.GameplayLeaderboard),
                },
                new SettingsCheckbox
                {
                    LabelText = GameplaySettingsStrings.AlwaysRequireHoldForMenu,
                    Current = config.GetBindable<bool>(OsuSetting.AlwaysRequireHoldingForPause),
                },
                new SettingsCheckbox
                {
                    LabelText = GameplaySettingsStrings.AlwaysShowHoldForMenuButton,
                    Current = config.GetBindable<bool>(OsuSetting.AlwaysShowHoldForMenuButton),
                },
                new SettingsCheckbox
                {
                    ClassicDefault = false,
                    LabelText = GameplaySettingsStrings.ShowHealthDisplayWhenCantFail,
                    Current = config.GetBindable<bool>(OsuSetting.ShowHealthDisplayWhenCantFail),
                    Keywords = new[] { "hp", "bar" }
                },
            };
        }
    }
}
