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
    public partial class TestSceneSkinnableComboCounter : SkinnableHUDComponentTestScene
    {
        [Cached]
        private ScoreProcessor scoreProcessor = new ScoreProcessor(new OsuRuleset());

        protected override Drawable CreateArgonImplementation() => new ArgonComboCounter();
        protected override Drawable CreateDefaultImplementation() => new DefaultComboCounter();
        protected override Drawable CreateLegacyImplementation() => new LegacyDefaultComboCounter();

        [Test]
        public void TestComboCounterIncrementing()
        {
            AddRepeatStep("increase combo", () => scoreProcessor.Combo.Value++, 10);

            AddStep("reset combo", () => scoreProcessor.Combo.Value = 0);
        }
    }
}
