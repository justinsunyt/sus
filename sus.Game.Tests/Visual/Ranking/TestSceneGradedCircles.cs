// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Graphics;
using sus.Game.Rulesets.Osu;
using sus.Game.Rulesets.Scoring;
using sus.Game.Scoring;
using sus.Game.Screens.Ranking.Expanded.Accuracy;
using susTK;

namespace sus.Game.Tests.Visual.Ranking
{
    public partial class TestSceneGradedCircles : OsuTestScene
    {
        private readonly GradedCircles ring;

        public TestSceneGradedCircles()
        {
            ScoreProcessor scoreProcessor = new OsuRuleset().CreateScoreProcessor();
            double accuracyX = scoreProcessor.AccuracyCutoffFromRank(ScoreRank.X);
            double accuracyS = scoreProcessor.AccuracyCutoffFromRank(ScoreRank.S);

            double accuracyA = scoreProcessor.AccuracyCutoffFromRank(ScoreRank.A);
            double accuracyB = scoreProcessor.AccuracyCutoffFromRank(ScoreRank.B);
            double accuracyC = scoreProcessor.AccuracyCutoffFromRank(ScoreRank.C);

            Add(ring = new GradedCircles(accuracyC, accuracyB, accuracyA, accuracyS, accuracyX)
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Size = new Vector2(400)
            });
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            AddSliderStep("Progress", 0.0, 1.0, 1.0, p => ring.Progress = p);
        }
    }
}
