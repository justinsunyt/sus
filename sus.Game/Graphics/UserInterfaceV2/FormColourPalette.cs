// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Collections.Specialized;
using System.Linq;
using sus.Framework.Allocation;
using sus.Framework.Bindables;
using sus.Framework.Extensions;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Framework.Graphics.Cursor;
using sus.Framework.Graphics.Shapes;
using sus.Framework.Graphics.UserInterface;
using sus.Framework.Input.Events;
using sus.Framework.Localisation;
using sus.Game.Graphics.Containers;
using sus.Game.Graphics.Sprites;
using sus.Game.Graphics.UserInterface;
using sus.Game.Overlays;
using sus.Game.Resources.Localisation.Web;
using susTK;
using susTK.Graphics;

namespace sus.Game.Graphics.UserInterfaceV2
{
    public partial class FormColourPalette : CompositeDrawable
    {
        public BindableList<Colour4> Colours { get; } = new BindableList<Colour4>();

        public LocalisableString Caption { get; init; }
        public LocalisableString HintText { get; init; }

        public BindableBool CanAdd { get; } = new BindableBool(true);

        private Box background = null!;
        private FormFieldCaption caption = null!;
        private FillFlowContainer flow = null!;
        private RoundedButton addButton = null!;

        [Resolved]
        private OverlayColourProvider colourProvider { get; set; } = null!;

        [BackgroundDependencyLoader]
        private void load()
        {
            RelativeSizeAxes = Axes.X;
            AutoSizeAxes = Axes.Y;

            Masking = true;
            CornerRadius = 5;

            InternalChildren = new Drawable[]
            {
                background = new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = colourProvider.Background5,
                },
                new FillFlowContainer
                {
                    RelativeSizeAxes = Axes.X,
                    AutoSizeAxes = Axes.Y,
                    Padding = new MarginPadding(9),
                    Spacing = new Vector2(7),
                    Direction = FillDirection.Vertical,
                    Children = new Drawable[]
                    {
                        caption = new FormFieldCaption
                        {
                            Caption = Caption,
                            TooltipText = HintText,
                        },
                        flow = new FillFlowContainer
                        {
                            RelativeSizeAxes = Axes.X,
                            AutoSizeAxes = Axes.Y,
                            Direction = FillDirection.Full,
                            Spacing = new Vector2(5),
                            Child = addButton = new RoundedButton
                            {
                                Action = addNewColour,
                                Size = new Vector2(70),
                                Text = "+",
                            }
                        }
                    },
                },
            };

            flow.SetLayoutPosition(addButton, float.MaxValue);
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            Colours.BindCollectionChanged((_, args) =>
            {
                if (args.Action != NotifyCollectionChangedAction.Replace)
                    updateColours();
            }, true);
            CanAdd.BindValueChanged(canAdd =>
            {
                if (canAdd.NewValue)
                {
                    addButton.Enabled.Value = true;
                    addButton.TooltipText = string.Empty;
                }
                else
                {
                    addButton.Enabled.Value = false;
                    addButton.TooltipText = "Maximum combo colours reached";
                }
            }, true);
            updateState();
        }

        protected override bool OnHover(HoverEvent e)
        {
            updateState();
            return true;
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            base.OnHoverLost(e);
            updateState();
        }

        private void addNewColour()
        {
            Color4 startingColour = Colours.Count > 0
                ? Colours.Last()
                : Colour4.White;

            Colours.Add(startingColour);
            flow.OfType<ColourButton>().Last().TriggerClick();
        }

        private void updateState()
        {
            background.Colour = colourProvider.Background5;
            caption.Colour = colourProvider.Content2;

            BorderThickness = IsHovered ? 2 : 0;

            if (IsHovered)
                BorderColour = colourProvider.Light4;
        }

        private void updateColours()
        {
            flow.RemoveAll(d => d is ColourButton, true);

            for (int i = 0; i < Colours.Count; ++i)
            {
                // copy to avoid accesses to modified clsusre.
                int colourIndex = i;
                var colourButton = new ColourButton { Current = { Value = Colours[colourIndex] } };
                colourButton.Current.BindValueChanged(colour => Colours[colourIndex] = colour.NewValue);
                colourButton.DeleteRequested = () => Colours.RemoveAt(colourIndex);
                flow.Add(colourButton);
            }
        }

        private partial class ColourButton : OsuClickableContainer, IHasPopover, IHasContextMenu
        {
            public Bindable<Colour4> Current { get; } = new Bindable<Colour4>();
            public Action? DeleteRequested { get; set; }

            private Box background = null!;
            private OsuSpriteText hexCode = null!;

            [BackgroundDependencyLoader]
            private void load()
            {
                Size = new Vector2(70);

                Masking = true;
                CornerRadius = 35;
                Action = this.ShowPopover;

                Children = new Drawable[]
                {
                    background = new Box
                    {
                        RelativeSizeAxes = Axes.Both,
                    },
                    hexCode = new OsuSpriteText
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                    },
                };
            }

            protected override void LoadComplete()
            {
                base.LoadComplete();

                Current.BindValueChanged(_ => updateState(), true);
            }

            public Popover GetPopover() => new ColourPickerPopover
            {
                Current = { BindTarget = Current }
            };

            public MenuItem[] ContextMenuItems => new MenuItem[]
            {
                new OsuMenuItem(CommonStrings.ButtonsDelete, MenuItemType.Destructive, () => DeleteRequested?.Invoke())
            };

            private void updateState()
            {
                background.Colour = Current.Value;
                hexCode.Text = Current.Value.ToHex();
                hexCode.Colour = OsuColour.ForegroundTextColourFor(Current.Value);
            }
        }

        private partial class ColourPickerPopover : OsuPopover, IHasCurrentValue<Colour4>
        {
            public Bindable<Colour4> Current
            {
                get => current.Current;
                set => current.Current = value;
            }

            private readonly BindableWithCurrent<Colour4> current = new BindableWithCurrent<Colour4>();

            public ColourPickerPopover()
                : base(false)
            {
            }

            [BackgroundDependencyLoader]
            private void load(OverlayColourProvider colourProvider)
            {
                Child = new OsuColourPicker
                {
                    Current = { BindTarget = Current }
                };

                Body.BorderThickness = 2;
                Body.BorderColour = colourProvider.Highlight1;
                Content.Padding = new MarginPadding(2);
            }
        }
    }
}
