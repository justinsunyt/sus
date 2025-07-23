// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using sus.Framework.Extensions.IEnumerableExtensions;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Framework.Testing;
using sus.Framework.Utils;
using sus.Game.Overlays;
using sus.Game.Rulesets;
using sus.Game.Rulesets.Catch;
using sus.Game.Rulesets.Mania;
using sus.Game.Rulesets.Mods;
using sus.Game.Rulesets.Osu;
using sus.Game.Rulesets.Taiko;
using sus.Game.Rulesets.UI;
using susTK;

namespace sus.Game.Tests.Visual.UserInterface
{
    [TestFixture]
    public partial class TestSceneModSwitchTiny : OsuTestScene
    {
        [Test]
        public void TestOsu() => createSwitchTestFor(new OsuRuleset());

        [Test]
        public void TestTaiko() => createSwitchTestFor(new TaikoRuleset());

        [Test]
        public void TestCatch() => createSwitchTestFor(new CatchRuleset());

        [Test]
        public void TestMania() => createSwitchTestFor(new ManiaRuleset());

        [Test]
        public void TestShowRateAdjusts()
        {
            AddStep("create mod icons", () =>
            {
                Child = new FillFlowContainer
                {
                    RelativeSizeAxes = Axes.Both,
                    Direction = FillDirection.Full,
                    ChildrenEnumerable = Ruleset.Value.CreateInstance().CreateAllMods()
                                                .OfType<ModRateAdjust>()
                                                .SelectMany(m =>
                                                {
                                                    List<TestModSwitchTiny> icons = new List<TestModSwitchTiny> { new TestModSwitchTiny(m) };

                                                    for (double i = m.SpeedChange.MinValue; i < m.SpeedChange.MaxValue; i += m.SpeedChange.Precision * 10)
                                                    {
                                                        m = (ModRateAdjust)m.DeepClone();
                                                        m.SpeedChange.Value = i;
                                                        icons.Add(new TestModSwitchTiny(m, true));
                                                    }

                                                    return icons;
                                                }),
                };
            });

            AddStep("adjust rates", () =>
            {
                foreach (var icon in this.ChildrenOfType<TestModSwitchTiny>())
                {
                    if (icon.Mod is ModRateAdjust rateAdjust)
                    {
                        rateAdjust.SpeedChange.Value = RNG.NextDouble() > 0.9
                            ? rateAdjust.SpeedChange.Default
                            : RNG.NextDouble(rateAdjust.SpeedChange.MinValue, rateAdjust.SpeedChange.MaxValue);
                    }
                }
            });

            AddToggleStep("toggle active", active => this.ChildrenOfType<TestModSwitchTiny>().ForEach(s => s.Active.Value = active));
        }

        private void createSwitchTestFor(Ruleset ruleset)
        {
            AddStep("no colour scheme", () => Child = createContent(ruleset, null));

            foreach (var scheme in Enum.GetValues(typeof(OverlayColourScheme)).Cast<OverlayColourScheme>())
            {
                AddStep($"{scheme} colour scheme", () => Child = createContent(ruleset, scheme));
            }

            AddToggleStep("toggle active", active => this.ChildrenOfType<TestModSwitchTiny>().ForEach(s => s.Active.Value = active));
        }

        private static Drawable createContent(Ruleset ruleset, OverlayColourScheme? colourScheme)
        {
            var switchFlow = new FillFlowContainer
            {
                RelativeSizeAxes = Axes.Both,
                Direction = FillDirection.Vertical,
                Spacing = new Vector2(10),
                Padding = new MarginPadding(20),
                ChildrenEnumerable = ruleset.CreateAllMods()
                                            .GroupBy(mod => mod.Type)
                                            .Select(group => new FillFlowContainer
                                            {
                                                RelativeSizeAxes = Axes.X,
                                                AutoSizeAxes = Axes.Y,
                                                Direction = FillDirection.Full,
                                                Spacing = new Vector2(5),
                                                ChildrenEnumerable = group.Select(mod => new TestModSwitchTiny(mod))
                                            })
            };

            if (colourScheme != null)
            {
                return new DependencyProvidingContainer
                {
                    RelativeSizeAxes = Axes.Both,
                    CachedDependencies = new (Type, object)[]
                    {
                        (typeof(OverlayColourProvider), new OverlayColourProvider(colourScheme.Value))
                    },
                    Child = switchFlow
                };
            }

            return switchFlow;
        }

        private partial class TestModSwitchTiny : ModSwitchTiny
        {
            public new IMod Mod => base.Mod;

            public TestModSwitchTiny(IMod mod, bool showExtendedInformation = false)
                : base(mod, showExtendedInformation)
            {
            }
        }
    }
}
