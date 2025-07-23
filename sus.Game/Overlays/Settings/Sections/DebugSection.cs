// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Development;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Sprites;
using sus.Framework.Localisation;
using sus.Game.Graphics;
using sus.Game.Overlays.Settings.Sections.DebugSettings;

namespace sus.Game.Overlays.Settings.Sections
{
    public partial class DebugSection : SettingsSection
    {
        public override LocalisableString Header => @"Debug";

        public override Drawable CreateIcon() => new SpriteIcon
        {
            Icon = OsuIcon.Debug
        };

        public DebugSection()
        {
            if (DebugUtils.IsDebugBuild)
            {
                Add(new GeneralSettings());
                Add(new BatchImportSettings());
            }

            Add(new MemorySettings());
        }
    }
}
