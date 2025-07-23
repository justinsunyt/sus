// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using sus.Game.Configuration;
using sus.Game.Localisation;

namespace sus.Game.Screens.Play.PlayerSettings
{
    public partial class InputSettings : PlayerSettingsGroup
    {
        public InputSettings()
            : base("Input Settings")
        {
        }

        [BackgroundDependencyLoader]
        private void load(OsuConfigManager config)
        {
            Children = new Drawable[]
            {
                new PlayerCheckbox
                {
                    // TODO: change to touchscreen detection once https://github.com/ppy/sus/pull/25348 makes it in
                    LabelText = RuntimeInfo.IsDesktop ? MouseSettingsStrings.DisableClicksDuringGameplay : TouchSettingsStrings.DisableTapsDuringGameplay,
                    Current = config.GetBindable<bool>(RuntimeInfo.IsDesktop ? OsuSetting.MouseDisableButtons : OsuSetting.TouchDisableGameplayTaps)
                }
            };
        }
    }
}
