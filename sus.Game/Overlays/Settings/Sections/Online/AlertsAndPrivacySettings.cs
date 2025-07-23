// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Allocation;
using sus.Framework.Graphics;
using sus.Framework.Localisation;
using sus.Game.Configuration;
using sus.Game.Localisation;

namespace sus.Game.Overlays.Settings.Sections.Online
{
    public partial class AlertsAndPrivacySettings : SettingsSubsection
    {
        protected override LocalisableString Header => OnlineSettingsStrings.AlertsAndPrivacyHeader;

        [BackgroundDependencyLoader]
        private void load(OsuConfigManager config)
        {
            Children = new Drawable[]
            {
                new SettingsCheckbox
                {
                    LabelText = OnlineSettingsStrings.NotifyOnMentioned,
                    Current = config.GetBindable<bool>(OsuSetting.NotifyOnUsernameMentioned)
                },
                new SettingsCheckbox
                {
                    LabelText = OnlineSettingsStrings.NotifyOnPrivateMessage,
                    Current = config.GetBindable<bool>(OsuSetting.NotifyOnPrivateMessage)
                },
                new SettingsCheckbox
                {
                    LabelText = OnlineSettingsStrings.NotifyOnFriendPresenceChange,
                    TooltipText = OnlineSettingsStrings.NotifyOnFriendPresenceChangeTooltip,
                    Current = config.GetBindable<bool>(OsuSetting.NotifyOnFriendPresenceChange),
                },
                new SettingsCheckbox
                {
                    LabelText = OnlineSettingsStrings.HideCountryFlags,
                    Current = config.GetBindable<bool>(OsuSetting.HideCountryFlags)
                },
            };
        }
    }
}
