// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using NUnit.Framework;
using sus.Framework.Allocation;
using sus.Framework.Graphics;
using sus.Game.Rulesets.Osu;
using sus.Game.Rulesets.Scoring;
using sus.Game.Screens.Play.HUD;
using sus.Game.Skinning;
using sus.Game.Tests.Gameplay;

namespace sus.Game.Tests.Visual.Gameplay
{
    public partial class TestSceneSkinnableScoreCounter : SkinnableHUDComponentTestScene
    {
        [Cached(typeof(ScoreProcessor))]
        private ScoreProcessor scoreProcessor = TestGameplayState.Create(new OsuRuleset()).ScoreProcessor;

        protected override Drawable CreateArgonImplementation() => new ArgonScoreCounter();
        protected override Drawable CreateDefaultImplementation() => new DefaultScoreCounter();
        protected override Drawable CreateLegacyImplementation() => new LegacyScoreCounter();

        [Test]
        public void TestScoreCounterIncrementing()
        {
            AddStep(@"Reset all", () => scoreProcessor.TotalScore.Value = 0);

            AddStep(@"Hit! :D", () => scoreProcessor.TotalScore.Value += 300);
        }

        [Test]
        public void TestVeryLargeScore()
        {
            AddStep("set large score", () => scoreProcessor.TotalScore.Value = 1_000_000_000);
        }
    }
}
