// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Linq;
using NUnit.Framework;
using osu.Framework.Allocation;
using osu.Framework.Testing;
using sus.Game.Rulesets.Mania.Mods;
using sus.Game.Rulesets.Mania.Objects.Drawables;
using sus.Game.Rulesets.UI.Scrolling;
using sus.Game.Rulesets.UI.Scrolling.Algorithms;
using sus.Game.Tests.Visual;

namespace sus.Game.Rulesets.Mania.Tests.Mods
{
    public partial class TestSceneManiaModConstantSpeed : ModTestScene
    {
        protected override Ruleset CreatePlayerRuleset() => new ManiaRuleset();

        [Test]
        public void TestConstantScroll() => CreateModTest(new ModTestData
        {
            Mod = new ManiaModConstantSpeed(),
            PassCondition = () =>
            {
                var hitObject = Player.ChildrenOfType<DrawableManiaHitObject>().FirstOrDefault();
                return hitObject?.Dependencies.Get<IScrollingInfo>().Algorithm.Value is ConstantScrollAlgorithm;
            }
        });
    }
}
