// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Allocation;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Framework.Graphics.Shapes;
using sus.Framework.Graphics.UserInterface;
using sus.Game.Graphics.UserInterface;
using sus.Game.Overlays;

namespace sus.Game.Graphics.UserInterfaceV2
{
    public partial class OsuHexColourPicker : HexColourPicker
    {
        public OsuHexColourPicker()
        {
            Padding = new MarginPadding(20);
            Spacing = 20;
        }

        [BackgroundDependencyLoader(true)]
        private void load(OverlayColourProvider? overlayColourProvider, OsuColour susColour)
        {
            Background.Colour = overlayColourProvider?.Dark6 ?? susColour.GreySeaFoamDarker;
        }

        protected override TextBox CreateHexCodeTextBox() => new OsuTextBox();
        protected override ColourPreview CreateColourPreview() => new OsuColourPreview();

        private partial class OsuColourPreview : ColourPreview
        {
            private readonly Box preview;

            public OsuColourPreview()
            {
                InternalChild = new CircularContainer
                {
                    RelativeSizeAxes = Axes.Both,
                    Masking = true,
                    Child = preview = new Box
                    {
                        RelativeSizeAxes = Axes.Both
                    }
                };
            }

            protected override void LoadComplete()
            {
                base.LoadComplete();

                Current.BindValueChanged(colour => preview.Colour = colour.NewValue, true);
            }
        }
    }
}
