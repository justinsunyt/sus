// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using NUnit.Framework;
using sus.Framework.Allocation;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Game.Rulesets.Edit;
using sus.Game.Rulesets.Taiko.Beatmaps;
using sus.Game.Rulesets.Taiko.Edit;
using sus.Game.Rulesets.Taiko.Objects;
using sus.Game.Screens.Edit;
using sus.Game.Tests.Visual;

namespace sus.Game.Rulesets.Taiko.Tests.Editor
{
    public partial class TestSceneTaikoHitObjectComposer : EditorClockTestScene
    {
        [SetUp]
        public void Setup() => Schedule(() =>
        {
            BeatDivisor.Value = 8;
            EditorClock.Seek(0);

            Child = new TestComposer { RelativeSizeAxes = Axes.Both };
        });

        [Test]
        public void BasicTest()
        {
        }

        private partial class TestComposer : CompositeDrawable
        {
            [Cached(typeof(EditorBeatmap))]
            [Cached(typeof(IBeatSnapProvider))]
            public readonly EditorBeatmap EditorBeatmap;

            public TestComposer()
            {
                InternalChildren = new Drawable[]
                {
                    EditorBeatmap = new EditorBeatmap(new TaikoBeatmap
                    {
                        BeatmapInfo = { Ruleset = new TaikoRuleset().RulesetInfo }
                    }),
                    new TaikoHitObjectComposer(new TaikoRuleset())
                };

                for (int i = 0; i < 10; i++)
                    EditorBeatmap.Add(new Hit { StartTime = 125 * i });
            }
        }
    }
}
