// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using NUnit.Framework;
using sus.Framework.Allocation;
using sus.Framework.Graphics;
using sus.Game.Rulesets.Osu;
using sus.Game.Rulesets.Scoring;
using sus.Game.Screens.Play.HUD;
using sus.Game.Skinning;

namespace sus.Game.Tests.Visual.Gameplay
{
    public partial class TestSceneSkinnableAccuracyCounter : SkinnableHUDComponentTestScene
    {
        [Cached]
        private ScoreProcessor scoreProcessor = new ScoreProcessor(new OsuRuleset());

        protected override Drawable CreateArgonImplementation() => new ArgonAccuracyCounter();
        protected override Drawable CreateDefaultImplementation() => new DefaultAccuracyCounter();
        protected override Drawable CreateLegacyImplementation() => new LegacyAccuracyCounter();

        public override void SetUpSteps()
        {
            AddStep("Set initial accuracy", () => scoreProcessor.Accuracy.Value = 1);

            base.SetUpSteps();
        }

        [Test]
        public void TestChangingAccuracy()
        {
            AddStep(@"Reset all", () => scoreProcessor.Accuracy.Value = 1);

            AddStep(@"Miss :(", () => scoreProcessor.Accuracy.Value -= 0.023);
        }
    }
}
