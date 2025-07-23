// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using NUnit.Framework;
using sus.Framework.Allocation;
using sus.Framework.Bindables;
using sus.Framework.Extensions.ObjectExtensions;
using sus.Framework.Graphics;
using sus.Game.Rulesets.Taiko.Configuration;
using sus.Game.Rulesets.Taiko.UI;
using sus.Game.Tests.Visual;

namespace sus.Game.Rulesets.Taiko.Tests
{
    [TestFixture]
    public partial class TestSceneDrumTouchInputArea : OsuTestScene
    {
        private DrumTouchInputArea drumTouchInputArea = null!;

        private readonly Bindable<TaikoTouchControlScheme> controlScheme = new Bindable<TaikoTouchControlScheme>();

        [BackgroundDependencyLoader]
        private void load()
        {
            var config = (TaikoRulesetConfigManager)RulesetConfigs.GetConfigFor(Ruleset.Value.CreateInstance()).AsNonNull();
            config.BindWith(TaikoRulesetSetting.TouchControlScheme, controlScheme);
        }

        private void createDrum()
        {
            Child = new TaikoInputManager(new TaikoRuleset().RulesetInfo)
            {
                RelativeSizeAxes = Axes.Both,
                Children = new Drawable[]
                {
                    new InputDrum
                    {
                        Anchor = Anchor.TopCentre,
                        Origin = Anchor.TopCentre,
                        Height = 0.2f,
                    },
                    drumTouchInputArea = new DrumTouchInputArea
                    {
                        Anchor = Anchor.BottomCentre,
                        Origin = Anchor.BottomCentre,
                    }
                }
            };
        }

        [Test]
        public void TestDrum()
        {
            AddStep("create drum", createDrum);
            AddStep("show drum", () => drumTouchInputArea.Show());

            AddStep("change scheme (kddk)", () => controlScheme.Value = TaikoTouchControlScheme.KDDK);
            AddStep("change scheme (kkdd)", () => controlScheme.Value = TaikoTouchControlScheme.KKDD);
            AddStep("change scheme (ddkk)", () => controlScheme.Value = TaikoTouchControlScheme.DDKK);
        }

        protected override Ruleset CreateRuleset() => new TaikoRuleset();
    }
}
