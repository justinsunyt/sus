// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework;
using sus.Framework.Allocation;
using sus.Framework.Graphics;
using sus.Framework.Localisation;
using sus.Game.Configuration;
using sus.Game.Localisation;

namespace sus.Game.Overlays.Settings.Sections.Gameplay
{
    public partial class InputSettings : SettingsSubsection
    {
        protected override LocalisableString Header => GameplaySettingsStrings.InputHeader;

        [BackgroundDependencyLoader]
        private void load(OsuConfigManager config)
        {
            Children = new Drawable[]
            {
                new SettingsSlider<float, SizeSlider<float>>
                {
                    LabelText = SkinSettingsStrings.GameplayCursorSize,
                    Current = config.GetBindable<float>(OsuSetting.GameplayCursorSize),
                    KeyboardStep = 0.01f
                },
                new SettingsCheckbox
                {
                    LabelText = SkinSettingsStrings.AutoCursorSize,
                    Current = config.GetBindable<bool>(OsuSetting.AutoCursorSize)
                },
                new SettingsCheckbox
                {
                    LabelText = SkinSettingsStrings.GameplayCursorDuringTouch,
                    Keywords = new[] { @"touchscreen" },
                    Current = config.GetBindable<bool>(OsuSetting.GameplayCursorDuringTouch)
                },
            };

            if (RuntimeInfo.OS == RuntimeInfo.Platform.Windows)
            {
                Add(new SettingsCheckbox
                {
                    LabelText = GameplaySettingsStrings.DisableWinKey,
                    Current = config.GetBindable<bool>(OsuSetting.GameplayDisableWinKey)
                });
            }
        }
    }
}
