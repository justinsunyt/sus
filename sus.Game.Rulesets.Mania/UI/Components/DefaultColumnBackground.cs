// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

#nullable disable

using sus.Framework.Allocation;
using sus.Framework.Bindables;
using sus.Framework.Extensions.Color4Extensions;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Colour;
using sus.Framework.Graphics.Containers;
using sus.Framework.Graphics.Shapes;
using sus.Framework.Input.Bindings;
using sus.Framework.Input.Events;
using sus.Game.Rulesets.UI.Scrolling;
using susTK.Graphics;

namespace sus.Game.Rulesets.Mania.UI.Components
{
    public partial class DefaultColumnBackground : CompositeDrawable, IKeyBindingHandler<ManiaAction>
    {
        private readonly IBindable<ScrollingDirection> direction = new Bindable<ScrollingDirection>();

        private Color4 brightColour;
        private Color4 dimColour;

        private Box background;
        private Box backgroundOverlay;

        [Resolved]
        private Column column { get; set; }

        private Bindable<Color4> accentColour;

        public DefaultColumnBackground()
        {
            RelativeSizeAxes = Axes.Both;
        }

        [BackgroundDependencyLoader]
        private void load(IScrollingInfo scrollingInfo)
        {
            InternalChildren = new[]
            {
                background = new Box
                {
                    Name = "Background",
                    RelativeSizeAxes = Axes.Both,
                },
                backgroundOverlay = new Box
                {
                    Name = "Background Gradient Overlay",
                    RelativeSizeAxes = Axes.Both,
                    Height = 0.5f,
                    Blending = BlendingParameters.Additive,
                    Alpha = 0
                }
            };

            accentColour = column.AccentColour.GetBoundCopy();
            accentColour.BindValueChanged(colour =>
            {
                background.Colour = colour.NewValue.Darken(5);
                brightColour = colour.NewValue.Opacity(0.6f);
                dimColour = colour.NewValue.Opacity(0);
            }, true);

            direction.BindTo(scrollingInfo.Direction);
            direction.BindValueChanged(onDirectionChanged, true);
        }

        private void onDirectionChanged(ValueChangedEvent<ScrollingDirection> direction)
        {
            if (direction.NewValue == ScrollingDirection.Up)
            {
                backgroundOverlay.Anchor = backgroundOverlay.Origin = Anchor.TopLeft;
                backgroundOverlay.Colour = ColourInfo.GradientVertical(brightColour, dimColour);
            }
            else
            {
                backgroundOverlay.Anchor = backgroundOverlay.Origin = Anchor.BottomLeft;
                backgroundOverlay.Colour = ColourInfo.GradientVertical(dimColour, brightColour);
            }
        }

        public bool OnPressed(KeyBindingPressEvent<ManiaAction> e)
        {
            if (e.Action == column.Action.Value)
                backgroundOverlay.FadeTo(1, 50, Easing.OutQuint).Then().FadeTo(0.5f, 250, Easing.OutQuint);
            return false;
        }

        public void OnReleased(KeyBindingReleaseEvent<ManiaAction> e)
        {
            if (e.Action == column.Action.Value)
                backgroundOverlay.FadeTo(0, 250, Easing.OutQuint);
        }
    }
}
