// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Graphics;
using sus.Framework.Graphics.Sprites;
using sus.Framework.Localisation;
using sus.Game.Graphics;
using sus.Game.Localisation;
using sus.Game.Overlays.Settings.Sections.UserInterface;

namespace sus.Game.Overlays.Settings.Sections
{
    public partial class UserInterfaceSection : SettingsSection
    {
        public override LocalisableString Header => UserInterfaceStrings.UserInterfaceSectionHeader;

        public override Drawable CreateIcon() => new SpriteIcon
        {
            Icon = OsuIcon.UserInterface
        };

        public UserInterfaceSection()
        {
            Children = new Drawable[]
            {
                new GeneralSettings(),
                new MainMenuSettings(),
                new SongSelectSettings()
            };
        }
    }
}
