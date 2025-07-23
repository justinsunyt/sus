// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Extensions.Color4Extensions;
using sus.Framework.Graphics.Sprites;
using sus.Game.Graphics.UserInterface;
using susTK;
using susTK.Graphics;

namespace sus.Game.Tests.Visual.UserInterface
{
    public partial class TestSceneTwoLayerButton : OsuTestScene
    {
        public TestSceneTwoLayerButton()
        {
            Add(new TwoLayerButton
            {
                Position = new Vector2(100),
                Text = "button",
                Icon = FontAwesome.Solid.Check,
                BackgroundColour = Color4.SlateGray,
                HoverColour = Color4.SlateGray.Darken(0.2f)
            });
        }
    }
}
