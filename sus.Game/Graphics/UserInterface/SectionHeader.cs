// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Allocation;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Framework.Graphics.Shapes;
using sus.Framework.Localisation;
using sus.Game.Graphics.Containers;
using sus.Game.Overlays;
using susTK;

namespace sus.Game.Graphics.UserInterface
{
    public partial class SectionHeader : CompositeDrawable
    {
        private readonly LocalisableString text;

        public SectionHeader(LocalisableString text)
        {
            this.text = text;

            Margin = new MarginPadding { Vertical = 10, Horizontal = 5 };

            RelativeSizeAxes = Axes.X;
            AutoSizeAxes = Axes.Y;
        }

        [BackgroundDependencyLoader]
        private void load(OverlayColourProvider colourProvider)
        {
            InternalChild = new FillFlowContainer
            {
                RelativeSizeAxes = Axes.X,
                AutoSizeAxes = Axes.Y,
                Direction = FillDirection.Vertical,
                Spacing = new Vector2(2),
                Children = new Drawable[]
                {
                    new OsuTextFlowContainer(cp => cp.Font = OsuFont.Default.With(size: 16, weight: FontWeight.SemiBold))
                    {
                        Text = text,
                        RelativeSizeAxes = Axes.X,
                        AutoSizeAxes = Axes.Y,
                    },
                    new Circle
                    {
                        Colour = colourProvider.Highlight1,
                        Size = new Vector2(28, 2),
                    }
                }
            };
        }
    }
}
