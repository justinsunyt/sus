// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using NUnit.Framework;
using sus.Game.Beatmaps;
using sus.Game.Rulesets.Catch.Difficulty;
using sus.Game.Rulesets.Catch.Mods;
using sus.Game.Rulesets.Difficulty;
using sus.Game.Tests.Beatmaps;

namespace sus.Game.Rulesets.Catch.Tests
{
    public class CatchDifficultyCalculatorTest : DifficultyCalculatorTest
    {
        protected override string ResourceAssembly => "sus.Game.Rulesets.Catch.Tests";

        [TestCase(4.0505463516206195d, 127, "diffcalc-test")]
        public void Test(double expectedStarRating, int expectedMaxCombo, string name)
            => base.Test(expectedStarRating, expectedMaxCombo, name);

        [TestCase(5.1696411260785498d, 127, "diffcalc-test")]
        public void TestClockRateAdjusted(double expectedStarRating, int expectedMaxCombo, string name)
            => Test(expectedStarRating, expectedMaxCombo, name, new CatchModDoubleTime());

        protected override DifficultyCalculator CreateDifficultyCalculator(IWorkingBeatmap beatmap) => new CatchDifficultyCalculator(new CatchRuleset().RulesetInfo, beatmap);

        protected override Ruleset CreateRuleset() => new CatchRuleset();
    }
}
