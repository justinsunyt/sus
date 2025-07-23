// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Linq;
using NUnit.Framework;
using sus.Framework.Extensions.IEnumerableExtensions;
using sus.Framework.Screens;
using sus.Framework.Testing;
using sus.Game.Skinning;
using sus.Game.Tests.Visual;
using susTK;

namespace sus.Game.Rulesets.Catch.Tests
{
    [TestFixture]
    public partial class TestSceneCatchPlayerLegacySkin : LegacySkinPlayerTestScene
    {
        protected override Ruleset CreatePlayerRuleset() => new CatchRuleset();

        [Test]
        public void TestLegacyHUDComboCounterNotExistent([Values] bool withModifiedSkin)
        {
            if (withModifiedSkin)
            {
                AddStep("change component scale", () => Player.ChildrenOfType<LegacyScoreCounter>().First().Scale = new Vector2(2f));
                AddStep("update target", () => Player.ChildrenOfType<SkinnableContainer>().ForEach(LegacySkin.UpdateDrawableTarget));
                AddStep("exit player", () => Player.Exit());
                CreateTest();
            }

            AddAssert("legacy HUD combo counter not added", () => !Player.ChildrenOfType<LegacyDefaultComboCounter>().Any());
        }
    }
}
