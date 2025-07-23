// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using NUnit.Framework;
using sus.Framework.Allocation;
using sus.Framework.Graphics;
using sus.Game.Beatmaps.ControlPoints;
using sus.Game.Rulesets.Osu;
using sus.Game.Screens.Edit;
using sus.Game.Screens.Edit.Components.Timelines.Summary;
using susTK;

namespace sus.Game.Tests.Visual.Editing
{
    [TestFixture]
    public partial class TestSceneEditorSummaryTimeline : EditorClockTestScene
    {
        [Cached(typeof(EditorBeatmap))]
        private readonly EditorBeatmap editorBeatmap;

        public TestSceneEditorSummaryTimeline()
        {
            var beatmap = CreateBeatmap(new OsuRuleset().RulesetInfo);

            beatmap.ControlPointInfo.Add(100000, new TimingControlPoint { BeatLength = 100 });
            beatmap.ControlPointInfo.Add(50000, new DifficultyControlPoint { SliderVelocity = 2 });
            beatmap.ControlPointInfo.Add(80000, new EffectControlPoint { KiaiMode = true });
            beatmap.ControlPointInfo.Add(110000, new EffectControlPoint { KiaiMode = false });
            beatmap.Bookmarks = new[] { 75000, 125000 };
            beatmap.Breaks.Add(new ManualBreakPeriod(90000, 120000));

            editorBeatmap = new EditorBeatmap(beatmap);
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            AddStep("create timeline", () =>
            {
                // required for track
                Beatmap.Value = CreateWorkingBeatmap(editorBeatmap.PlayableBeatmap);

                Add(new SummaryTimeline
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Size = new Vector2(500, 50)
                });
            });
        }
    }
}
