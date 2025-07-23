// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

#nullable disable

using NUnit.Framework;
using sus.Framework.Allocation;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Framework.Graphics.Primitives;
using sus.Framework.Graphics.Shapes;
using sus.Game.Configuration;
using sus.Game.Graphics.Containers;
using susTK;
using susTK.Graphics;

namespace sus.Game.Tests.Visual.UserInterface
{
    [TestFixture]
    public partial class TestSceneScalingContainer : OsuTestScene
    {
        private OsuConfigManager susConfigManager { get; set; }

        private ScalingContainer scaling1;
        private ScalingContainer scaling2;
        private Box scaleTarget;

        [BackgroundDependencyLoader]
        private void load()
        {
            susConfigManager = new OsuConfigManager(LocalStorage);

            Dependencies.CacheAs(susConfigManager);

            Children = new Drawable[]
            {
                new Container
                {
                    RelativeSizeAxes = Axes.Both,
                    Children = new Drawable[]
                    {
                        scaling1 = new ScalingContainer(ScalingMode.Everything)
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Scale = new Vector2(0.8f),
                            Children = new Drawable[]
                            {
                                scaling2 = new ScalingContainer(ScalingMode.Everything)
                                {
                                    Anchor = Anchor.Centre,
                                    Origin = Anchor.Centre,
                                    Scale = new Vector2(0.8f),
                                    Children = new Drawable[]
                                    {
                                        new Box
                                        {
                                            Colour = Color4.Purple,
                                            RelativeSizeAxes = Axes.Both,
                                        },
                                        scaleTarget = new Box
                                        {
                                            Anchor = Anchor.Centre,
                                            Origin = Anchor.Centre,
                                            Colour = Color4.White,
                                            Size = new Vector2(100),
                                        },
                                    }
                                }
                            }
                        }
                    }
                },
            };
        }

        [Test]
        public void TestScaling()
        {
            AddStep("adjust scale", () => susConfigManager.SetValue(OsuSetting.UIScale, 2f));

            checkForCorrectness();

            AddStep("adjust scale", () => susConfigManager.SetValue(OsuSetting.UIScale, 0.5f));

            checkForCorrectness();
        }

        private void checkForCorrectness()
        {
            Quad? scaling1LastQuad = null;
            Quad? scaling2LastQuad = null;
            Quad? scalingTargetLastQuad = null;

            AddUntilStep("ensure dimensions don't change", () =>
            {
                if (scaling1LastQuad.HasValue && scaling2LastQuad.HasValue)
                {
                    // check inter-frame changes to make sure they match expectations.
                    Assert.That(scaling1.ScreenSpaceDrawQuad.AlmostEquals(scaling1LastQuad.Value), Is.True);
                    Assert.That(scaling2.ScreenSpaceDrawQuad.AlmostEquals(scaling2LastQuad.Value), Is.True);
                }

                scaling1LastQuad = scaling1.ScreenSpaceDrawQuad;
                scaling2LastQuad = scaling2.ScreenSpaceDrawQuad;

                // wait for scaling to stop.
                bool scalingFinished = scalingTargetLastQuad.HasValue && scaleTarget.ScreenSpaceDrawQuad.AlmostEquals(scalingTargetLastQuad.Value);

                scalingTargetLastQuad = scaleTarget.ScreenSpaceDrawQuad;

                return scalingFinished;
            });
        }
    }
}
