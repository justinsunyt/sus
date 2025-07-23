// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Localisation;
using sus.Game.Configuration;
using sus.Game.Localisation;
using sus.Game.Overlays.Mods.Input;

namespace sus.Game.Overlays.Settings.Sections.UserInterface
{
    public partial class SongSelectSettings : SettingsSubsection
    {
        protected override LocalisableString Header => UserInterfaceStrings.SongSelectHeader;

        [BackgroundDependencyLoader]
        private void load(OsuConfigManager config)
        {
            Children = new Drawable[]
            {
                new SettingsCheckbox
                {
                    LabelText = UserInterfaceStrings.ShowConvertedBeatmaps,
                    Current = config.GetBindable<bool>(OsuSetting.ShowConvertedBeatmaps),
                    Keywords = new[] { "converts", "converted" }
                },
                new SettingsEnumDropdown<RandomSelectAlgorithm>
                {
                    LabelText = UserInterfaceStrings.RandomSelectionAlgorithm,
                    Current = config.GetBindable<RandomSelectAlgorithm>(OsuSetting.RandomSelectAlgorithm),
                },
                new SettingsEnumDropdown<ModSelectHotkeyStyle>
                {
                    LabelText = UserInterfaceStrings.ModSelectHotkeyStyle,
                    Current = config.GetBindable<ModSelectHotkeyStyle>(OsuSetting.ModSelectHotkeyStyle),
                    ClassicDefault = ModSelectHotkeyStyle.Classic
                },
                new SettingsCheckbox
                {
                    LabelText = UserInterfaceStrings.ModSelectTextSearchStartsActive,
                    Current = config.GetBindable<bool>(OsuSetting.ModSelectTextSearchStartsActive),
                    ClassicDefault = false
                },
                new SettingsCheckbox
                {
                    LabelText = GameplaySettingsStrings.BackgroundBlur,
                    Current = config.GetBindable<bool>(OsuSetting.SongSelectBackgroundBlur),
                    ClassicDefault = false,
                }
            };
        }
    }
}
