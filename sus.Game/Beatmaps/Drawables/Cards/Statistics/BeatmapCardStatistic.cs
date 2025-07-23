// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

#nullable disable

using sus.Framework.Allocation;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Framework.Graphics.Cursor;
using sus.Framework.Graphics.Sprites;
using sus.Framework.Localisation;
using sus.Game.Graphics;
using sus.Game.Graphics.Sprites;
using sus.Game.Overlays;
using susTK;

namespace sus.Game.Beatmaps.Drawables.Cards.Statistics
{
    /// <summary>
    /// A single statistic shown on a beatmap card.
    /// </summary>
    public abstract partial class BeatmapCardStatistic : CompositeDrawable, IHasTooltip, IHasCustomTooltip
    {
        protected IconUsage Icon
        {
            get => spriteIcon.Icon;
            set => spriteIcon.Icon = value;
        }

        protected LocalisableString Text
        {
            get => spriteText.Text;
            set => spriteText.Text = value;
        }

        public LocalisableString TooltipText { get; protected set; }

        private readonly SpriteIcon spriteIcon;
        private readonly OsuSpriteText spriteText;

        protected BeatmapCardStatistic()
        {
            AutoSizeAxes = Axes.Both;

            InternalChild = new FillFlowContainer
            {
                AutoSizeAxes = Axes.Both,
                Direction = FillDirection.Horizontal,
                Spacing = new Vector2(4, 0),
                Children = new Drawable[]
                {
                    spriteIcon = new SpriteIcon
                    {
                        Anchor = Anchor.CentreLeft,
                        Origin = Anchor.CentreLeft,
                        Size = new Vector2(8),
                        Margin = new MarginPadding { Top = 1 }
                    },
                    spriteText = new OsuSpriteText
                    {
                        Anchor = Anchor.CentreLeft,
                        Origin = Anchor.CentreLeft,
                        Font = OsuFont.Default.With(size: 11)
                    }
                }
            };
        }

        [BackgroundDependencyLoader]
        private void load(OverlayColourProvider colourProvider)
        {
            spriteIcon.Colour = colourProvider.Content2;
        }

        #region Tooltip implementation

        public virtual ITooltip GetCustomTooltip() => null!;
        public virtual object TooltipContent => null;

        #endregion
    }
}
