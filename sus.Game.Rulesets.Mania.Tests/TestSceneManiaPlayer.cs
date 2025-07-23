// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Extensions.ObjectExtensions;
using sus.Game.Rulesets.Mania.Configuration;
using sus.Game.Rulesets.Mania.UI;
using sus.Game.Tests.Visual;

namespace sus.Game.Rulesets.Mania.Tests
{
    public partial class TestSceneManiaPlayer : PlayerTestScene
    {
        protected override Ruleset CreatePlayerRuleset() => new ManiaRuleset();

        public override void SetUpSteps()
        {
            base.SetUpSteps();

            AddStep("change direction to down", () => changeDirectionTo(ManiaScrollingDirection.Down));
            AddStep("change direction to up", () => changeDirectionTo(ManiaScrollingDirection.Up));
        }

        private void changeDirectionTo(ManiaScrollingDirection direction)
        {
            var rulesetConfig = (ManiaRulesetConfigManager)RulesetConfigs.GetConfigFor(new ManiaRuleset()).AsNonNull();
            rulesetConfig.SetValue(ManiaRulesetSetting.ScrollDirection, direction);
        }
    }
}
