// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using NUnit.Framework;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Game.Rulesets.Taiko.Objects;
using sus.Game.Rulesets.Taiko.UI;

namespace sus.Game.Rulesets.Taiko.Tests.Skinning
{
    [TestFixture]
    public partial class TestSceneKiaiHitExplosion : TaikoSkinnableTestScene
    {
        [Test]
        public void TestKiaiHits()
        {
            AddStep("rim hit", () => SetContents(_ => getContentFor(createHit(HitType.Rim))));
            AddStep("centre hit", () => SetContents(_ => getContentFor(createHit(HitType.Centre))));
        }

        private Drawable getContentFor(DrawableTestHit hit)
        {
            return new Container
            {
                RelativeSizeAxes = Axes.Both,
                Child = new KiaiHitExplosion(hit, hit.HitObject.Type)
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                }
            };
        }

        private DrawableTestHit createHit(HitType type) => new DrawableTestHit(new Hit { StartTime = Time.Current, Type = type });
    }
}
