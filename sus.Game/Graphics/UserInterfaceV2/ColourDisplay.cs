// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

#nullable disable

using System;
using sus.Framework.Allocation;
using sus.Framework.Bindables;
using sus.Framework.Extensions;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Framework.Graphics.Cursor;
using sus.Framework.Graphics.Shapes;
using sus.Framework.Graphics.UserInterface;
using sus.Framework.Localisation;
using sus.Game.Graphics.Containers;
using sus.Game.Graphics.Sprites;
using sus.Game.Graphics.UserInterface;
using sus.Game.Resources.Localisation.Web;
using susTK;

namespace sus.Game.Graphics.UserInterfaceV2
{
    /// <summary>
    /// A component which displays a colour along with related description text.
    /// </summary>
    public partial class ColourDisplay : CompositeDrawable, IHasCurrentValue<Colour4>
    {
        /// <summary>
        /// Invoked when the user has requested the colour corresponding to this <see cref="ColourDisplay"/>
        /// to be removed from its palette.
        /// </summary>
        public event Action<ColourDisplay> DeleteRequested;

        private readonly BindableWithCurrent<Colour4> current = new BindableWithCurrent<Colour4>();

        private OsuSpriteText colourName;

        public Bindable<Colour4> Current
        {
            get => current.Current;
            set => current.Current = value;
        }

        private LocalisableString name;

        public LocalisableString ColourName
        {
            get => name;
            set
            {
                if (name == value)
                    return;

                name = value;

                colourName.Text = name;
            }
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            AutoSizeAxes = Axes.Y;
            Width = 100;

            InternalChild = new FillFlowContainer
            {
                RelativeSizeAxes = Axes.X,
                AutoSizeAxes = Axes.Y,
                Direction = FillDirection.Vertical,
                Spacing = new Vector2(0, 10),
                Children = new Drawable[]
                {
                    new ColourCircle
                    {
                        Current = { BindTarget = Current },
                        DeleteRequested = () => DeleteRequested?.Invoke(this)
                    },
                    colourName = new OsuSpriteText
                    {
                        Anchor = Anchor.TopCentre,
                        Origin = Anchor.TopCentre
                    }
                }
            };
        }

        private partial class ColourCircle : OsuClickableContainer, IHasPopover, IHasContextMenu
        {
            public Bindable<Colour4> Current { get; } = new Bindable<Colour4>();

            public Action DeleteRequested { get; set; }

            private readonly Box fill;
            private readonly OsuSpriteText colourHexCode;

            public ColourCircle()
            {
                RelativeSizeAxes = Axes.X;
                Height = 100;
                CornerRadius = 50;
                Masking = true;
                Action = this.ShowPopover;

                Children = new Drawable[]
                {
                    fill = new Box
                    {
                        RelativeSizeAxes = Axes.Both
                    },
                    colourHexCode = new OsuSpriteText
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Font = OsuFont.Default.With(size: 12)
                    }
                };
            }

            protected override void LoadComplete()
            {
                base.LoadComplete();

                Current.BindValueChanged(_ => updateColour(), true);
            }

            private void updateColour()
            {
                fill.Colour = Current.Value;
                colourHexCode.Text = Current.Value.ToHex();
                colourHexCode.Colour = OsuColour.ForegroundTextColourFor(Current.Value);
            }

            public Popover GetPopover() => new OsuPopover(false)
            {
                Child = new OsuColourPicker
                {
                    Current = { BindTarget = Current }
                }
            };

            public MenuItem[] ContextMenuItems => new MenuItem[]
            {
                new OsuMenuItem(CommonStrings.ButtonsDelete, MenuItemType.Destructive, () => DeleteRequested?.Invoke())
            };
        }
    }
}
