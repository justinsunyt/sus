// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using NUnit.Framework;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Shapes;
using sus.Game.Graphics.UserInterfaceV2;
using sus.Game.Overlays;
using sus.Game.Tests.Visual.UserInterface;

namespace sus.Game.Tests.Visual.Settings
{
    public partial class TestSceneFileSelector : ThemeComparisonTestScene
    {
        public TestSceneFileSelector()
            : base(false)
        {
        }

        [Test]
        public void TestJpgFilesOnly()
        {
            AddStep("create", () =>
            {
                var colourProvider = new OverlayColourProvider(OverlayColourScheme.Aquamarine);

                ContentContainer.Child = new DependencyProvidingContainer
                {
                    RelativeSizeAxes = Axes.Both,
                    CachedDependencies = new (Type, object)[]
                    {
                        (typeof(OverlayColourProvider), colourProvider)
                    },
                    Children = new Drawable[]
                    {
                        new Box
                        {
                            RelativeSizeAxes = Axes.Both,
                            Colour = colourProvider.Background3
                        },
                        new OsuFileSelector(validFileExtensions: new[] { ".jpg" })
                        {
                            RelativeSizeAxes = Axes.Both,
                        },
                    }
                };
            });
        }

        protected override Drawable CreateContent() => new OsuFileSelector
        {
            RelativeSizeAxes = Axes.Both,
        };
    }
}
