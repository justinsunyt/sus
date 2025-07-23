// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Graphics;
using sus.Framework.Graphics.Sprites;
using sus.Framework.Localisation;
using sus.Game.Graphics;
using sus.Game.Localisation;
using sus.Game.Overlays.Settings.Sections.Maintenance;

namespace sus.Game.Overlays.Settings.Sections
{
    public partial class MaintenanceSection : SettingsSection
    {
        public override LocalisableString Header => MaintenanceSettingsStrings.MaintenanceSectionHeader;

        public override Drawable CreateIcon() => new SpriteIcon
        {
            Icon = OsuIcon.Maintenance
        };

        public MaintenanceSection()
        {
            Children = new Drawable[]
            {
                new GeneralSettings(),
                new BeatmapSettings(),
                new SkinSettings(),
                new CollectionsSettings(),
                new ScoreSettings(),
                new ModPresetSettings()
            };
        }
    }
}
