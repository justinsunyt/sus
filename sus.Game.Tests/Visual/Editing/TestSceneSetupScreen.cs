// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using NUnit.Framework;
using sus.Framework.Allocation;
using sus.Framework.Graphics.Containers;
using sus.Game.Overlays;
using sus.Game.Rulesets;
using sus.Game.Rulesets.Catch;
using sus.Game.Rulesets.Edit;
using sus.Game.Rulesets.Mania;
using sus.Game.Rulesets.Osu;
using sus.Game.Rulesets.Osu.Beatmaps;
using sus.Game.Rulesets.Taiko;
using sus.Game.Screens.Edit;
using sus.Game.Screens.Edit.Setup;

namespace sus.Game.Tests.Visual.Editing
{
    [TestFixture]
    public partial class TestSceneSetupScreen : EditorClockTestScene
    {
        [Cached(typeof(EditorBeatmap))]
        [Cached(typeof(IBeatSnapProvider))]
        private readonly EditorBeatmap editorBeatmap;

        [Cached]
        private readonly OverlayColourProvider colourProvider = new OverlayColourProvider(OverlayColourScheme.Blue);

        public TestSceneSetupScreen()
        {
            editorBeatmap = new EditorBeatmap(new OsuBeatmap
            {
                BeatmapInfo =
                {
                    Ruleset = new OsuRuleset().RulesetInfo
                }
            });
        }

        [Test]
        public void TestOsu() => runForRuleset(new OsuRuleset().RulesetInfo);

        [Test]
        public void TestTaiko() => runForRuleset(new TaikoRuleset().RulesetInfo);

        [Test]
        public void TestCatch() => runForRuleset(new CatchRuleset().RulesetInfo);

        [Test]
        public void TestMania() => runForRuleset(new ManiaRuleset().RulesetInfo);

        private void runForRuleset(RulesetInfo rulesetInfo)
        {
            AddStep("create screen", () =>
            {
                editorBeatmap.BeatmapInfo.Ruleset = rulesetInfo;

                Beatmap.Value = CreateWorkingBeatmap(editorBeatmap.PlayableBeatmap);

                Child = new SetupScreen
                {
                    State = { Value = Visibility.Visible },
                };
            });
        }
    }
}
