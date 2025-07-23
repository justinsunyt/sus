// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

#nullable disable

using System;
using susTK;
using susTK.Graphics;
using sus.Framework.Allocation;
using sus.Framework.Audio;
using sus.Framework.Audio.Sample;
using sus.Framework.Extensions;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Shapes;
using sus.Framework.Graphics.Sprites;
using sus.Framework.Graphics.UserInterface;
using sus.Framework.Input.Events;
using sus.Framework.Localisation;
using sus.Game.Graphics.Sprites;

namespace sus.Game.Graphics.UserInterface
{
    public partial class PageTabControl<T> : OsuTabControl<T>
    {
        protected override TabItem<T> CreateTabItem(T value) => new PageTabItem(value);

        public PageTabControl()
        {
            Height = 30;
        }

        [BackgroundDependencyLoader]
        private void load(OsuColour colours)
        {
            AccentColour = colours.Yellow;
        }

        public partial class PageTabItem : TabItem<T>, IHasAccentColour
        {
            private const float transition_duration = 100;

            private readonly Box box;

            protected readonly SpriteText Text;

            private Color4 accentColour;

            public Color4 AccentColour
            {
                get => accentColour;
                set
                {
                    accentColour = value;
                    box.Colour = accentColour;
                }
            }

            private Sample selectSample = null!;

            public PageTabItem(T value)
                : base(value)
            {
                AutoSizeAxes = Axes.X;
                RelativeSizeAxes = Axes.Y;

                Children = new Drawable[]
                {
                    Text = new OsuSpriteText
                    {
                        Margin = new MarginPadding { Top = 8, Bottom = 8 },
                        Origin = Anchor.BottomLeft,
                        Anchor = Anchor.BottomLeft,
                        Text = CreateText(),
                        Font = OsuFont.GetFont(size: 14)
                    },
                    box = new Box
                    {
                        RelativeSizeAxes = Axes.X,
                        Height = 5,
                        Scale = new Vector2(1f, 0f),
                        Colour = Color4.White,
                        Origin = Anchor.BottomLeft,
                        Anchor = Anchor.BottomLeft,
                    },
                    new HoverSounds(HoverSampleSet.TabSelect)
                };

                Active.BindValueChanged(active => Text.Font = Text.Font.With(Typeface.Torus, weight: active.NewValue ? FontWeight.Bold : FontWeight.Medium), true);
            }

            [BackgroundDependencyLoader]
            private void load(AudioManager audio)
            {
                selectSample = audio.Samples.Get(@"UI/tabselect-select");
            }

            protected virtual LocalisableString CreateText() => (Value as Enum)?.GetLocalisableDescription() ?? Value.ToString();

            protected override bool OnHover(HoverEvent e)
            {
                if (!Active.Value)
                    slideActive();
                return true;
            }

            protected override void OnHoverLost(HoverLostEvent e)
            {
                if (!Active.Value)
                    slideInactive();
            }

            private void slideActive()
            {
                box.ScaleTo(new Vector2(1f), transition_duration);
            }

            private void slideInactive()
            {
                box.ScaleTo(new Vector2(1f, 0f), transition_duration);
            }

            protected override void OnActivated() => slideActive();

            protected override void OnDeactivated() => slideInactive();

            protected override void OnActivatedByUser() => selectSample.Play();
        }
    }
}
