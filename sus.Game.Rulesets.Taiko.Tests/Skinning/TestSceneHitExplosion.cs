// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using NUnit.Framework;
using sus.Framework.Extensions.IEnumerableExtensions;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Framework.Testing;
using sus.Game.Rulesets.Judgements;
using sus.Game.Rulesets.Objects;
using sus.Game.Rulesets.Scoring;
using sus.Game.Rulesets.Taiko.Objects;
using sus.Game.Rulesets.Taiko.UI;
using sus.Game.Screens.Ranking;

namespace sus.Game.Rulesets.Taiko.Tests.Skinning
{
    [TestFixture]
    public partial class TestSceneHitExplosion : TaikoSkinnableTestScene
    {
        protected override double TimePerAction => 100;

        [Test]
        public void TestNormalHit()
        {
            AddStep("Great", () => SetContents(_ => getContentFor(createHit(HitResult.Great))));
            AddStep("Ok", () => SetContents(_ => getContentFor(createHit(HitResult.Ok))));
            AddStep("Miss", () => SetContents(_ => getContentFor(createHit(HitResult.Miss))));
        }

        [TestCase(HitResult.Great)]
        [TestCase(HitResult.Ok)]
        public void TestStrongHit(HitResult type)
        {
            AddStep("create hit", () => SetContents(_ => getContentFor(createStrongHit(type))));
            AddStep("visualise second hit",
                () => this.ChildrenOfType<HitExplosion>()
                          .ForEach(e => e.VisualiseSecondHit(new JudgementResult(new HitObject { StartTime = Time.Current }, new Judgement()))));
        }

        private Drawable getContentFor(DrawableTestHit hit)
        {
            return new Container
            {
                RelativeSizeAxes = Axes.Both,
                Children = new Drawable[]
                {
                    // the hit needs to be added to hierarchy in order for nested objects to be created correctly.
                    // setting zero alpha is supposed to prevent the test from looking broken.
                    hit.With(h => h.Alpha = 0),

                    new AspectContainer
                    {
                        RelativeSizeAxes = Axes.X,
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Child =
                            new HitExplosion(hit.Type)
                            {
                                Anchor = Anchor.Centre,
                                Origin = Anchor.Centre,
                            }.With(explosion => explosion.Apply(hit))
                    }
                }
            };
        }

        private DrawableTestHit createHit(HitResult type) => new DrawableTestHit(new Hit { StartTime = Time.Current }, type);

        private DrawableTestHit createStrongHit(HitResult type) => new DrawableTestStrongHit(Time.Current, type);
    }
}
