// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Framework.Graphics.Cursor;
using sus.Framework.Graphics.Shapes;
using sus.Framework.Graphics.Sprites;
using sus.Framework.Localisation;
using susTK;
using susTK.Graphics;

namespace sus.Game.Beatmaps.Drawables.Cards
{
    public abstract partial class IconPill : CircularContainer, IHasTooltip
    {
        public Vector2 IconSize
        {
            get => iconContainer.Size;
            set => iconContainer.Size = value;
        }

        public MarginPadding IconPadding
        {
            get => iconContainer.Padding;
            set => iconContainer.Padding = value;
        }

        private readonly Container iconContainer;

        protected IconPill(IconUsage icon)
        {
            AutoSizeAxes = Axes.Both;
            Masking = true;

            Children = new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = Color4.Black,
                    Alpha = 0.5f,
                },
                iconContainer = new Container
                {
                    Size = new Vector2(22),
                    Padding = new MarginPadding(5),
                    Child = new SpriteIcon
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        RelativeSizeAxes = Axes.Both,
                        Icon = icon,
                    },
                },
            };
        }

        public abstract LocalisableString TooltipText { get; }
    }
}
