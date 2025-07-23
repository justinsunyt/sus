// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using NUnit.Framework;
using sus.Game.Rulesets.Catch.Mods;
using sus.Game.Tests.Visual;

namespace sus.Game.Rulesets.Catch.Tests.Mods
{
    public partial class TestSceneCatchModFloatingFruits : ModTestScene
    {
        protected override Ruleset CreatePlayerRuleset() => new CatchRuleset();

        [Test]
        public void TestFloating() => CreateModTest(new ModTestData
        {
            Mod = new CatchModFloatingFruits(),
            PassCondition = () => true
        });
    }
}
