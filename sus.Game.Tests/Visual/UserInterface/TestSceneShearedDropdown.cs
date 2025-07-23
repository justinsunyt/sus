// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Bindables;
using sus.Framework.Extensions.Color4Extensions;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Framework.Graphics.Shapes;
using sus.Game.Graphics.UserInterfaceV2;
using susTK.Graphics;

namespace sus.Game.Tests.Visual.UserInterface
{
    public partial class TestSceneShearedDropdown : ThemeComparisonTestScene
    {
        public TestSceneShearedDropdown()
            : base(false)
        {
        }

        protected override Drawable CreateContent() => new Container
        {
            RelativeSizeAxes = Axes.Both,
            Children = new Drawable[]
            {
                new Box
                {
                    Colour = Color4.Black.Opacity(0.75f),
                    RelativeSizeAxes = Axes.Both,
                },
                new ShearedDropdown<string>("Test")
                {
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                    Y = 300f,
                    Width = 140,
                    Current = new Bindable<string>(),
                    Items = new[] { "Global", "Friends", "Local", "Really lonnnnnnng option" },
                }
            }
        };
    }
}
