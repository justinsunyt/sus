// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Localisation;
using sus.Game.Configuration;
using sus.Game.Graphics.UserInterface;
using sus.Game.Localisation;

namespace sus.Game.Overlays.Settings.Sections.UserInterface
{
    public partial class GeneralSettings : SettingsSubsection
    {
        protected override LocalisableString Header => CommonStrings.General;

        [BackgroundDependencyLoader]
        private void load(OsuConfigManager config)
        {
            Children = new Drawable[]
            {
                new SettingsCheckbox
                {
                    LabelText = UserInterfaceStrings.CursorRotation,
                    Current = config.GetBindable<bool>(OsuSetting.CursorRotation)
                },
                new SettingsSlider<float, SizeSlider<float>>
                {
                    LabelText = UserInterfaceStrings.MenuCursorSize,
                    Current = config.GetBindable<float>(OsuSetting.MenuCursorSize),
                    KeyboardStep = 0.01f
                },
                new SettingsCheckbox
                {
                    LabelText = UserInterfaceStrings.Parallax,
                    Current = config.GetBindable<bool>(OsuSetting.MenuParallax)
                },
                new SettingsSlider<double, TimeSlider>
                {
                    ClassicDefault = 0,
                    LabelText = UserInterfaceStrings.HoldToConfirmActivationTime,
                    Current = config.GetBindable<double>(OsuSetting.UIHoldActivationDelay),
                    Keywords = new[] { @"delay" },
                    KeyboardStep = 50
                },
            };
        }
    }
}
