// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using JetBrains.Annotations;
using sus.Framework.Bindables;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Sprites;
using sus.Game.Configuration;
using sus.Game.Graphics;
using sus.Game.Graphics.Sprites;
using sus.Game.Localisation.SkinComponents;

namespace sus.Game.Skinning.Components
{
    [UsedImplicitly]
    public partial class TextElement : FontAdjustableSkinComponent
    {
        [SettingSource(typeof(SkinnableComponentStrings), nameof(SkinnableComponentStrings.TextElementText))]
        public Bindable<string> Text { get; } = new Bindable<string>("Circles!");

        private readonly OsuSpriteText text;

        public TextElement()
        {
            AutoSizeAxes = Axes.Both;
            InternalChildren = new Drawable[]
            {
                text = new OsuSpriteText
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Font = OsuFont.Default.With(size: 40)
                }
            };
            text.Current.BindTo(Text);
        }

        protected override void SetFont(FontUsage font) => text.Font = font.With(size: 40);

        protected override void SetTextColour(Colour4 textColour) => text.Colour = textColour;
    }
}
