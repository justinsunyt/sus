// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Allocation;
using sus.Framework.Graphics;
using sus.Framework.Localisation;
using sus.Game.Configuration;
using sus.Game.Localisation;

namespace sus.Game.Overlays.Settings.Sections.Online
{
    public partial class IntegrationSettings : SettingsSubsection
    {
        protected override LocalisableString Header => OnlineSettingsStrings.IntegrationsHeader;

        [BackgroundDependencyLoader]
        private void load(OsuConfigManager config)
        {
            Children = new Drawable[]
            {
                new SettingsEnumDropdown<DiscordRichPresenceMode>
                {
                    LabelText = OnlineSettingsStrings.DiscordRichPresence,
                    Current = config.GetBindable<DiscordRichPresenceMode>(OsuSetting.DiscordRichPresence)
                }
            };
        }
    }
}
