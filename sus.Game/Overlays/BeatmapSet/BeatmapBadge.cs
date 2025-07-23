// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Allocation;
using sus.Framework.Extensions.LocalisationExtensions;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Framework.Graphics.Shapes;
using sus.Framework.Localisation;
using sus.Game.Graphics;
using sus.Game.Graphics.Sprites;

namespace sus.Game.Overlays.BeatmapSet
{
    public abstract partial class BeatmapBadge : CompositeDrawable
    {
        /// <summary>
        /// The text displayed on the badge's label.
        /// </summary>
        public LocalisableString BadgeText
        {
            set => badgeLabel.Text = value.ToUpper();
        }

        /// <summary>
        /// The colour of the badge's label.
        /// </summary>
        public Colour4 BadgeColour
        {
            set => badgeLabel.Colour = value;
        }

        private readonly Box background;
        private readonly OsuSpriteText badgeLabel;

        protected BeatmapBadge()
        {
            AutoSizeAxes = Axes.Both;

            InternalChild = new CircularContainer
            {
                Masking = true,
                AutoSizeAxes = Axes.Both,
                Children = new Drawable[]
                {
                    background = new Box
                    {
                        RelativeSizeAxes = Axes.Both,
                    },
                    badgeLabel = new OsuSpriteText
                    {
                        Font = OsuFont.GetFont(size: 10, weight: FontWeight.SemiBold),
                        Margin = new MarginPadding { Horizontal = 10, Vertical = 2 },
                    }
                }
            };
        }

        [BackgroundDependencyLoader(true)]
        private void load(OsuColour colours, OverlayColourProvider? colourProvider)
        {
            background.Colour = colourProvider?.Background5 ?? colours.Gray2;
        }
    }
}
