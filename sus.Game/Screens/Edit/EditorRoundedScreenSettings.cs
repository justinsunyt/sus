// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using sus.Framework.Allocation;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Framework.Graphics.Shapes;
using sus.Game.Graphics.Containers;
using sus.Game.Overlays;
using susTK;

namespace sus.Game.Screens.Edit
{
    public abstract partial class EditorRoundedScreenSettings : CompositeDrawable
    {
        [BackgroundDependencyLoader]
        private void load(OverlayColourProvider colours)
        {
            RelativeSizeAxes = Axes.Both;

            InternalChildren = new Drawable[]
            {
                new Box
                {
                    Colour = colours.Background6,
                    RelativeSizeAxes = Axes.Both,
                },
                new OsuScrollContainer
                {
                    RelativeSizeAxes = Axes.Both,
                    Child = new FillFlowContainer
                    {
                        RelativeSizeAxes = Axes.X,
                        AutoSizeAxes = Axes.Y,
                        Direction = FillDirection.Vertical,
                        Padding = new MarginPadding(10),
                        Spacing = new Vector2(10),
                        Children = CreateSections()
                    },
                }
            };
        }

        protected abstract IReadOnlyList<Drawable> CreateSections();
    }
}
