// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Allocation;
using sus.Framework.Extensions.Color4Extensions;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Framework.Graphics.Effects;
using sus.Framework.Graphics.Shapes;
using sus.Framework.Input.Events;
using sus.Game.Graphics.Containers;
using susTK.Graphics;

namespace sus.Game.Graphics.UserInterface
{
    /// <summary>
    /// Highlight on hover, bounce on click.
    /// </summary>
    public partial class OsuAnimatedButton : OsuClickableContainer
    {
        /// <summary>
        /// The colour that should be flashed when the <see cref="OsuAnimatedButton"/> is clicked.
        /// </summary>
        protected Color4 FlashColour = Color4.White.Opacity(0.3f);

        private Color4 hoverColour = Color4.White.Opacity(0.1f);

        protected float ScaleOnMouseDown { get; init; } = 0.75f;

        /// <summary>
        /// The background colour of the <see cref="OsuAnimatedButton"/> while it is hovered.
        /// </summary>
        protected Color4 HoverColour
        {
            get => hoverColour;
            set
            {
                hoverColour = value;
                hover.Colour = value;
            }
        }

        [Resolved]
        private OsuColour colours { get; set; } = null!;

        protected override Container<Drawable> Content => content;

        private readonly Container content;
        private readonly Box hover;

        public OsuAnimatedButton(HoverSampleSet sampleSet = HoverSampleSet.Button)
            : base(sampleSet)
        {
            base.Content.Add(content = new Container
            {
                Origin = Anchor.Centre,
                Anchor = Anchor.Centre,
                RelativeSizeAxes = Axes.Both,
                CornerRadius = 5,
                Masking = true,
                EdgeEffect = new EdgeEffectParameters
                {
                    Colour = Color4.Black.Opacity(0.04f),
                    Type = EdgeEffectType.Shadow,
                    Radius = 5,
                },
                Children = new Drawable[]
                {
                    hover = new Box
                    {
                        RelativeSizeAxes = Axes.Both,
                        Colour = HoverColour,
                        Blending = BlendingParameters.Additive,
                        Alpha = 0,
                    },
                }
            });
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            if (AutoSizeAxes != Axes.None)
            {
                content.RelativeSizeAxes = (Axes.Both & ~AutoSizeAxes);
                content.AutoSizeAxes = AutoSizeAxes;
            }
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            Colour = dimColour;
            Enabled.BindValueChanged(_ => this.FadeColour(dimColour, 200, Easing.OutQuint));
        }

        private Color4 dimColour => Enabled.Value ? Color4.White : colours.Gray9;

        protected override bool OnHover(HoverEvent e)
        {
            hover.FadeIn(500, Easing.OutQuint);
            return base.OnHover(e);
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            hover.FadeOut(500, Easing.OutQuint);
            base.OnHoverLost(e);
        }

        protected override bool OnClick(ClickEvent e)
        {
            // Handle case where a click is triggered via TriggerClick().
            if (!IsHovered)
                hover.FadeOutFromOne(1600);

            hover.FlashColour(FlashColour, 800, Easing.OutQuint);
            return base.OnClick(e);
        }

        protected override bool OnMouseDown(MouseDownEvent e)
        {
            Content.ScaleTo(ScaleOnMouseDown, 2000, Easing.OutQuint);
            return base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseUpEvent e)
        {
            Content.ScaleTo(1, 1000, Easing.OutElastic);
            base.OnMouseUp(e);
        }
    }
}
