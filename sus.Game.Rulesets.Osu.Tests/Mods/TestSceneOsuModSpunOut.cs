// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using sus.Framework.Testing;
using sus.Framework.Utils;
using sus.Game.Beatmaps;
using sus.Game.Rulesets.Judgements;
using sus.Game.Rulesets.Mods;
using sus.Game.Rulesets.Objects;
using sus.Game.Rulesets.Osu.Judgements;
using sus.Game.Rulesets.Osu.Mods;
using sus.Game.Rulesets.Osu.Objects;
using sus.Game.Rulesets.Osu.Objects.Drawables;
using sus.Game.Rulesets.Osu.Skinning.Default;
using sus.Game.Rulesets.Scoring;
using susTK;

namespace sus.Game.Rulesets.Osu.Tests.Mods
{
    public partial class TestSceneOsuModSpunOut : OsuModTestScene
    {
        protected override bool AllowFail => true;

        [Test]
        public void TestSpinnerAutoCompleted()
        {
            DrawableSpinner? spinner = null;
            JudgementResult? lastResult = null;

            CreateModTest(new ModTestData
            {
                Mod = new OsuModSpunOut(),
                Autoplay = false,
                CreateBeatmap = singleSpinnerBeatmap,
                PassCondition = () =>
                {
                    // Bind to the first spinner's results for further tracking.
                    if (spinner == null)
                    {
                        // We only care about the first spinner we encounter for this test.
                        var nextSpinner = Player.ChildrenOfType<DrawableSpinner>().SingleOrDefault();

                        if (nextSpinner == null)
                            return false;

                        lastResult = null;

                        spinner = nextSpinner;
                        spinner.OnNewResult += (_, result) => lastResult = result;
                    }

                    return lastResult?.Type == HitResult.Great;
                }
            });
        }

        [TestCase(null)]
        [TestCase(typeof(OsuModDoubleTime))]
        [TestCase(typeof(OsuModHalfTime))]
        public void TestSpinRateUnaffectedByMods(Type? additionalModType)
        {
            var mods = new List<Mod> { new OsuModSpunOut() };
            if (additionalModType != null)
                mods.Add((Mod)Activator.CreateInstance(additionalModType)!);

            CreateModTest(new ModTestData
            {
                Mods = mods,
                Autoplay = false,
                CreateBeatmap = singleSpinnerBeatmap,
                PassCondition = () =>
                {
                    var counter = Player.ChildrenOfType<SpinnerSpmCalculator>().SingleOrDefault();
                    var spinner = Player.ChildrenOfType<DrawableSpinner>().FirstOrDefault();

                    if (counter == null || spinner == null)
                        return false;

                    // ignore cases where the spinner hasn't started as these lead to false-positives
                    if (Precision.AlmostEquals(counter.Result.Value, 0, 1))
                        return false;

                    float rotationSpeed = (float)(1.01 * spinner.HitObject.SpinsRequired / spinner.HitObject.Duration);

                    return Precision.AlmostEquals(counter.Result.Value, rotationSpeed * 1000 * 60, 1);
                }
            });
        }

        [Test]
        public void TestSpinnerGetsNoBonusScore()
        {
            DrawableSpinner? spinner = null;
            List<JudgementResult> results = new List<JudgementResult>();

            CreateModTest(new ModTestData
            {
                Mod = new OsuModSpunOut(),
                Autoplay = false,
                CreateBeatmap = singleSpinnerBeatmap,
                PassCondition = () =>
                {
                    // Bind to the first spinner's results for further tracking.
                    if (spinner == null)
                    {
                        // We only care about the first spinner we encounter for this test.
                        var nextSpinner = Player.ChildrenOfType<DrawableSpinner>().SingleOrDefault();

                        if (nextSpinner == null)
                            return false;

                        spinner = nextSpinner;
                        spinner.OnNewResult += (_, result) => results.Add(result);

                        results.Clear();
                    }

                    // we should only be checking the bonus/progress after the spinner has fully completed.
                    if (results.OfType<OsuSpinnerJudgementResult>().All(r => r.TimeCompleted == null))
                        return false;

                    return
                        results.Any(r => r.Type == HitResult.SmallBonus)
                        && results.All(r => r.Type != HitResult.LargeBonus);
                }
            });
        }

        private Beatmap singleSpinnerBeatmap() => new Beatmap
        {
            HitObjects = new List<HitObject>
            {
                new Spinner
                {
                    Position = new Vector2(256, 192),
                    StartTime = 500,
                    Duration = 2000
                }
            }
        };
    }
}
