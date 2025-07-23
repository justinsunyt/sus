// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Allocation;
using sus.Framework.Extensions.Color4Extensions;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Framework.Graphics.Sprites;
using sus.Game.Graphics;
using sus.Game.Graphics.Sprites;
using sus.Game.Graphics.UserInterface;
using sus.Game.Localisation;
using susTK;
using susTK.Graphics;

namespace sus.Game.Screens.Footer
{
    public partial class ScreenBackButton : ShearedButton
    {
        public const float BUTTON_WIDTH = 240;

        public sealed override bool ReceivePositionalInputAt(Vector2 screenSpacePos)
        {
            // Ensure clicks in the corner of the screen still trigger the back button.
            // Need to apply more than 1x inflation due to shear.
            var inputRectangle = DrawRectangle.Inflate(new MarginPadding
            {
                Left = OsuGame.SCREEN_EDGE_MARGIN * 2,
                Bottom = OsuGame.SCREEN_EDGE_MARGIN * 2,
            });

            return inputRectangle.Contains(ToLocalSpace(screenSpacePos));
        }

        public ScreenBackButton()
            : base(BUTTON_WIDTH)
        {
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            ButtonContent.Child = new FillFlowContainer
            {
                X = -10f,
                RelativeSizeAxes = Axes.Both,
                Direction = FillDirection.Horizontal,
                Spacing = new Vector2(20f, 0f),
                Children = new Drawable[]
                {
                    new SpriteIcon
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Size = new Vector2(17f),
                        Icon = FontAwesome.Solid.ChevronLeft,
                    },
                    new OsuSpriteText
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Font = OsuFont.TorusAlternate.With(size: 17),
                        Text = CommonStrings.Back,
                        UseFullGlyphHeight = false,
                    }
                }
            };

            DarkerColour = Color4Extensions.FromHex("#DE31AE");
            LighterColour = Color4Extensions.FromHex("#FF86DD");
            TextColour = Color4.White;
        }
    }
}
