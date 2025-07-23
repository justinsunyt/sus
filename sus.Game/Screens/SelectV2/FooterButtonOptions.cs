// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Allocation;
using sus.Framework.Bindables;
using sus.Framework.Extensions;
using sus.Framework.Graphics.Cursor;
using sus.Framework.Graphics.Sprites;
using sus.Game.Beatmaps;
using sus.Game.Graphics;
using sus.Game.Input.Bindings;
using sus.Game.Overlays;
using sus.Game.Screens.Footer;

namespace sus.Game.Screens.SelectV2
{
    public partial class FooterButtonOptions : ScreenFooterButton, IHasPopover
    {
        [Resolved]
        private OverlayColourProvider colourProvider { get; set; } = null!;

        [Resolved]
        private IBindable<WorkingBeatmap> beatmap { get; set; } = null!;

        [Resolved]
        private ISongSelect? songSelect { get; set; }

        [BackgroundDependencyLoader]
        private void load(OsuColour colour)
        {
            Text = "Options";
            Icon = FontAwesome.Solid.Cog;
            AccentColour = colour.Purple1;
            Hotkey = GlobalAction.ToggleBeatmapOptions;

            Action = this.ShowPopover;
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            beatmap.BindValueChanged(_ => beatmapChanged(), true);
        }

        private void beatmapChanged()
        {
            this.HidePopover();
            Enabled.Value = !beatmap.IsDefault;
        }

        public Framework.Graphics.UserInterface.Popover GetPopover() => new Popover(this, beatmap.Value)
        {
            ColourProvider = colourProvider,
            SongSelect = songSelect
        };
    }
}
