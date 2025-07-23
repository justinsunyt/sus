// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using NUnit.Framework;
using sus.Game.Beatmaps;
using sus.Game.Rulesets.Difficulty;
using sus.Game.Rulesets.Mania.Difficulty;
using sus.Game.Rulesets.Mania.Mods;
using sus.Game.Tests.Beatmaps;

namespace sus.Game.Rulesets.Mania.Tests
{
    public class ManiaDifficultyCalculatorTest : DifficultyCalculatorTest
    {
        protected override string ResourceAssembly => "sus.Game.Rulesets.Mania.Tests";

        [TestCase(2.3493769750220914d, 242, "diffcalc-test")]
        public void Test(double expectedStarRating, int expectedMaxCombo, string name)
            => base.Test(expectedStarRating, expectedMaxCombo, name);

        [TestCase(2.797245912537965d, 242, "diffcalc-test")]
        public void TestClockRateAdjusted(double expectedStarRating, int expectedMaxCombo, string name)
            => Test(expectedStarRating, expectedMaxCombo, name, new ManiaModDoubleTime());

        protected override DifficultyCalculator CreateDifficultyCalculator(IWorkingBeatmap beatmap) => new ManiaDifficultyCalculator(new ManiaRuleset().RulesetInfo, beatmap);

        protected override Ruleset CreateRuleset() => new ManiaRuleset();
    }
}
