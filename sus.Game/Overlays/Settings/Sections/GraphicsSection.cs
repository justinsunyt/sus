// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Graphics;
using sus.Framework.Graphics.Sprites;
using sus.Framework.Localisation;
using sus.Game.Graphics;
using sus.Game.Localisation;
using sus.Game.Overlays.Settings.Sections.Graphics;

namespace sus.Game.Overlays.Settings.Sections
{
    public partial class GraphicsSection : SettingsSection
    {
        public override LocalisableString Header => GraphicsSettingsStrings.GraphicsSectionHeader;

        public override Drawable CreateIcon() => new SpriteIcon
        {
            Icon = OsuIcon.Graphics
        };

        public GraphicsSection()
        {
            Children = new Drawable[]
            {
                new LayoutSettings(),
                new RendererSettings(),
                new VideoSettings(),
                new ScreenshotSettings(),
            };
        }
    }
}
