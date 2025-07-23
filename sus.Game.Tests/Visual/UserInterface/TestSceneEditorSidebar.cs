// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Linq;
using NUnit.Framework;
using sus.Framework.Allocation;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Framework.Graphics.Shapes;
using sus.Game.Overlays;
using sus.Game.Screens.Edit.Components;
using susTK;
using susTK.Graphics;

namespace sus.Game.Tests.Visual.UserInterface
{
    public partial class TestSceneEditorSidebar : OsuTestScene
    {
        [Cached]
        private readonly OverlayColourProvider colourProvider = new OverlayColourProvider(OverlayColourScheme.Blue);

        [Test]
        public void TestSidebars()
        {
            AddStep("Add sidebars", () =>
            {
                Children = new Drawable[]
                {
                    new GridContainer
                    {
                        RelativeSizeAxes = Axes.Both,
                        ColumnDimensions = new[]
                        {
                            new Dimension(GridSizeMode.AutoSize),
                            new Dimension(),
                            new Dimension(GridSizeMode.AutoSize),
                        },
                        Content = new[]
                        {
                            new Drawable[]
                            {
                                new EditorSidebar
                                {
                                    Children = new[]
                                    {
                                        new EditorSidebarSection("Section 1")
                                        {
                                            Child = new FillFlowContainer
                                            {
                                                RelativeSizeAxes = Axes.X,
                                                AutoSizeAxes = Axes.Y,
                                                Direction = FillDirection.Full,
                                                Spacing = new Vector2(3),
                                                ChildrenEnumerable = Enumerable.Range(0, 10).Select(_ => new Box
                                                {
                                                    Colour = Color4.White,
                                                    Size = new Vector2(32),
                                                })
                                            },
                                        },
                                        new EditorSidebarSection("Section with a really long section header")
                                        {
                                            Child = new FillFlowContainer
                                            {
                                                RelativeSizeAxes = Axes.X,
                                                AutoSizeAxes = Axes.Y,
                                                Direction = FillDirection.Full,
                                                Spacing = new Vector2(3),
                                                ChildrenEnumerable = Enumerable.Range(0, 400).Select(_ => new Box
                                                {
                                                    Colour = Color4.Gray,
                                                    Size = new Vector2(32),
                                                })
                                            },
                                        },
                                    },
                                },
                                new Container
                                {
                                    RelativeSizeAxes = Axes.Both,
                                },
                                new EditorSidebar
                                {
                                    Children = new[]
                                    {
                                        new EditorSidebarSection("Section 1"),
                                        new EditorSidebarSection("Section 2"),
                                    },
                                },
                            }
                        }
                    }
                };
            });
        }
    }
}
