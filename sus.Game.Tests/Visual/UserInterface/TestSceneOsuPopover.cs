// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Allocation;
using sus.Framework.Extensions;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Framework.Graphics.Cursor;
using sus.Framework.Graphics.UserInterface;
using sus.Game.Graphics;
using sus.Game.Graphics.Sprites;
using sus.Game.Graphics.UserInterface;
using sus.Game.Graphics.UserInterfaceV2;
using sus.Game.Overlays;
using susTK;

namespace sus.Game.Tests.Visual.UserInterface
{
    public partial class TestSceneOsuPopover : OsuGridTestScene
    {
        public TestSceneOsuPopover()
            : base(1, 2)
        {
            Cell(0, 0).Child = new PopoverContainer
            {
                RelativeSizeAxes = Axes.Both,
                Children = new Drawable[]
                {
                    new OsuSpriteText
                    {
                        Text = @"No OverlayColourProvider",
                        Font = OsuFont.Default.With(size: 40)
                    },
                    new RoundedButtonWithPopover()
                }
            };

            Cell(0, 1).Child = new ColourProvidingContainer(OverlayColourScheme.Orange)
            {
                RelativeSizeAxes = Axes.Both,
                Child = new PopoverContainer
                {
                    RelativeSizeAxes = Axes.Both,
                    Children = new Drawable[]
                    {
                        new OsuSpriteText
                        {
                            Text = @"With OverlayColourProvider (orange)",
                            Font = OsuFont.Default.With(size: 40)
                        },
                        new RoundedButtonWithPopover()
                    }
                }
            };
        }

        private partial class RoundedButtonWithPopover : RoundedButton, IHasPopover
        {
            public RoundedButtonWithPopover()
            {
                Width = 100;
                Height = 30;
                Anchor = Anchor.Centre;
                Origin = Anchor.Centre;
                Text = @"open";
                Action = this.ShowPopover;
            }

            public Popover GetPopover() => new OsuPopover
            {
                Child = new FillFlowContainer
                {
                    AutoSizeAxes = Axes.Both,
                    Direction = FillDirection.Vertical,
                    Spacing = new Vector2(10),
                    Children = new Drawable[]
                    {
                        new OsuSpriteText
                        {
                            Text = @"sample text"
                        },
                        new OsuTextBox
                        {
                            Width = 150,
                            Height = 30
                        }
                    }
                }
            };
        }

        private partial class ColourProvidingContainer : Container
        {
            [Cached]
            private OverlayColourProvider provider { get; }

            public ColourProvidingContainer(OverlayColourScheme colourScheme)
            {
                provider = new OverlayColourProvider(colourScheme);
            }
        }
    }
}
