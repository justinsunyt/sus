// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using NUnit.Framework;
using sus.Game.Beatmaps;
using sus.Game.Rulesets.Judgements;
using sus.Game.Rulesets.Objects;
using sus.Game.Rulesets.Scoring;
using sus.Game.Rulesets.Taiko.Judgements;
using sus.Game.Rulesets.Taiko.Objects;
using sus.Game.Rulesets.Taiko.Scoring;

namespace sus.Game.Rulesets.Taiko.Tests
{
    [TestFixture]
    public class TaikoScoreProcessorTest
    {
        [Test]
        public void TestInaccurateHitScore()
        {
            var beatmap = new Beatmap<HitObject>
            {
                HitObjects =
                {
                    new Hit(),
                    new Hit { StartTime = 1000 }
                }
            };

            var scoreProcessor = new TaikoScoreProcessor();
            scoreProcessor.ApplyBeatmap(beatmap);

            // Apply a miss judgement
            scoreProcessor.ApplyResult(new JudgementResult(beatmap.HitObjects[0], new TaikoJudgement()) { Type = HitResult.Great });
            scoreProcessor.ApplyResult(new JudgementResult(beatmap.HitObjects[1], new TaikoJudgement()) { Type = HitResult.Ok });

            Assert.That(scoreProcessor.TotalScore.Value, Is.EqualTo(453745));
            Assert.That(scoreProcessor.Accuracy.Value, Is.EqualTo(0.75).Within(0.0001));
        }
    }
}
