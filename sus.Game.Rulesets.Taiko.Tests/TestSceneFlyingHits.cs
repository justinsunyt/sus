// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

#nullable disable

using System.Linq;
using NUnit.Framework;
using sus.Framework.Testing;
using sus.Game.Rulesets.Judgements;
using sus.Game.Rulesets.Scoring;
using sus.Game.Rulesets.Taiko.Judgements;
using sus.Game.Rulesets.Taiko.Objects;
using sus.Game.Rulesets.Taiko.Objects.Drawables;
using sus.Game.Rulesets.Taiko.UI;

namespace sus.Game.Rulesets.Taiko.Tests
{
    [TestFixture]
    public partial class TestSceneFlyingHits : DrawableTaikoRulesetTestScene
    {
        [TestCase(HitType.Centre)]
        [TestCase(HitType.Rim)]
        public void TestFlyingHits(HitType hitType)
        {
            DrawableFlyingHit flyingHit = null;

            AddStep("add flying hit", () =>
            {
                addFlyingHit(hitType);

                // flying hits all land in one common scrolling container (and stay there for rewind purposes),
                // so we need to manually get the latest one.
                flyingHit = this.ChildrenOfType<DrawableFlyingHit>().MaxBy(h => h.HitObject.StartTime);
            });

            AddAssert("hit type is correct", () => flyingHit.HitObject.Type == hitType);
        }

        private void addFlyingHit(HitType hitType)
        {
            var tick = new DrumRollTick(new DrumRoll()) { HitWindows = HitWindows.Empty, StartTime = DrawableRuleset.Playfield.Time.Current };

            DrawableDrumRollTick h;
            DrawableRuleset.Playfield.Add(h = new DrawableDrumRollTick(tick) { JudgementType = hitType });
            ((TaikoPlayfield)DrawableRuleset.Playfield).OnNewResult(h, new JudgementResult(tick, new TaikoDrumRollTickJudgement()) { Type = HitResult.Great });
        }
    }
}
