// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Localisation;
using sus.Game.Graphics;
using sus.Game.Localisation;
using sus.Game.Overlays.Settings.Sections.Online;

namespace sus.Game.Overlays.Settings.Sections
{
    public partial class OnlineSection : SettingsSection
    {
        public override LocalisableString Header => OnlineSettingsStrings.OnlineSectionHeader;

        public override Drawable CreateIcon() => new SpriteIcon
        {
            Icon = OsuIcon.Online
        };

        public OnlineSection()
        {
            Children = new Drawable[]
            {
                new WebSettings(),
                new AlertsAndPrivacySettings(),
            };

            if (RuntimeInfo.IsDesktop)
                Add(new IntegrationSettings());
        }
    }
}
