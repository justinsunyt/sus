// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using NUnit.Framework;
using sus.Framework.Allocation;
using sus.Framework.Bindables;
using sus.Framework.Graphics;
using sus.Game.Rulesets.Osu;
using sus.Game.Rulesets.Scoring;
using sus.Game.Scoring;
using sus.Game.Screens.Play.HUD;
using sus.Game.Skinning;

namespace sus.Game.Tests.Visual.Gameplay
{
    public partial class TestSceneSkinnableRankDisplay : SkinnableHUDComponentTestScene
    {
        [Cached]
        private ScoreProcessor scoreProcessor = new ScoreProcessor(new OsuRuleset());

        private Bindable<ScoreRank> rank => (Bindable<ScoreRank>)scoreProcessor.Rank;

        protected override Drawable CreateDefaultImplementation() => new DefaultRankDisplay();

        protected override Drawable CreateLegacyImplementation() => new LegacyRankDisplay();

        [Test]
        public void TestChangingRank()
        {
            AddStep("Set rank to SS Hidden", () => rank.Value = ScoreRank.XH);
            AddStep("Set rank to SS", () => rank.Value = ScoreRank.X);
            AddStep("Set rank to S Hidden", () => rank.Value = ScoreRank.SH);
            AddStep("Set rank to S", () => rank.Value = ScoreRank.S);
            AddStep("Set rank to A", () => rank.Value = ScoreRank.A);
            AddStep("Set rank to B", () => rank.Value = ScoreRank.B);
            AddStep("Set rank to C", () => rank.Value = ScoreRank.C);
            AddStep("Set rank to D", () => rank.Value = ScoreRank.D);
            AddStep("Set rank to F", () => rank.Value = ScoreRank.F);
        }
    }
}
