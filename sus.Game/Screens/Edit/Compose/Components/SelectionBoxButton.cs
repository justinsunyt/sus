// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using sus.Framework.Allocation;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Cursor;
using sus.Framework.Graphics.Sprites;
using sus.Framework.Input.Events;
using sus.Framework.Localisation;
using susTK;
using susTK.Graphics;

namespace sus.Game.Screens.Edit.Compose.Components
{
    public sealed partial class SelectionBoxButton : SelectionBoxControl, IHasTooltip
    {
        private SpriteIcon icon = null!;

        private readonly IconUsage iconUsage;

        public Action? Action;

        public event Action? Clicked;

        public event Action? HoverLost;

        public SelectionBoxButton(IconUsage iconUsage, string tooltip)
        {
            this.iconUsage = iconUsage;

            TooltipText = tooltip;

            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            Size = new Vector2(20);
            AddInternal(icon = new SpriteIcon
            {
                RelativeSizeAxes = Axes.Both,
                Size = new Vector2(0.5f),
                Icon = iconUsage,
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
            });
        }

        protected override bool OnClick(ClickEvent e)
        {
            Clicked?.Invoke();

            TriggerAction();

            return true;
        }

        protected override void UpdateHoverState()
        {
            base.UpdateHoverState();
            icon.FadeColour(!IsHeld && IsHovered ? Color4.White : Color4.Black, TRANSFORM_DURATION, Easing.OutQuint);
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            base.OnHoverLost(e);

            HoverLost?.Invoke();
        }

        public LocalisableString TooltipText { get; }

        public void TriggerAction()
        {
            Circle.FlashColour(Colours.GrayF, 300);

            TriggerOperationStarted();
            Action?.Invoke();
            TriggerOperationEnded();
        }
    }
}
