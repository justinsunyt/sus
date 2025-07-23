// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Graphics;
using sus.Game.Graphics.UserInterfaceV2;
using sus.Game.Tests.Visual.UserInterface;

namespace sus.Game.Tests.Visual.Settings
{
    public partial class TestSceneDirectorySelector : ThemeComparisonTestScene
    {
        public TestSceneDirectorySelector()
            : base(false)
        {
        }

        protected override Drawable CreateContent() => new OsuDirectorySelector
        {
            RelativeSizeAxes = Axes.Both
        };
    }
}
