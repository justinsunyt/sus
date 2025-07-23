// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Extensions.IEnumerableExtensions;
using sus.Framework.Testing;
using sus.Framework.Timing;
using sus.Game.Rulesets.Judgements;
using sus.Game.Rulesets.Objects;
using sus.Game.Rulesets.Scoring;
using sus.Game.Rulesets.Taiko.Skinning.Legacy;
using sus.Game.Skinning;

namespace sus.Game.Rulesets.Taiko.Tests.Skinning
{
    public partial class TestSceneTaikoScroller : TaikoSkinnableTestScene
    {
        private readonly ManualClock clock = new ManualClock();

        private bool reversed;

        public TestSceneTaikoScroller()
        {
            AddStep("Load scroller", () => SetContents(_ =>
                new SkinnableDrawable(new TaikoSkinComponentLookup(TaikoSkinComponents.Scroller), _ => Empty())
                {
                    Clock = new FramedClock(clock),
                    Height = 0.4f,
                }));

            AddToggleStep("Toggle passing", passing => this.ChildrenOfType<LegacyTaikoScroller>().ForEach(s => s.LastResult.Value =
                new JudgementResult(new HitObject(), new Judgement()) { Type = passing ? HitResult.Great : HitResult.Miss }));

            AddToggleStep("toggle playback direction", reversed => this.reversed = reversed);
        }

        protected override void Update()
        {
            base.Update();

            clock.CurrentTime += (reversed ? -1 : 1) * Clock.ElapsedFrameTime;
        }
    }
}
