// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using NUnit.Framework;
using sus.Game.Beatmaps;
using sus.Game.Rulesets.Mania.Mods;
using sus.Game.Rulesets.Mods;
using sus.Game.Tests.Beatmaps;
using sus.Game.Tests.Visual;

namespace sus.Game.Rulesets.Mania.Tests
{
    public partial class TestSceneManiaPlayerLegacySkin : LegacySkinPlayerTestScene
    {
        protected override Ruleset CreatePlayerRuleset() => new ManiaRuleset();

        // play with a converted beatmap to allow dual stages mod to work.
        protected override IBeatmap CreateBeatmap(RulesetInfo ruleset) => new TestBeatmap(new RulesetInfo());

        protected override bool HasCustomSteps => true;

        [Test]
        public void TestSingleStage()
        {
            AddStep("Load single stage", LoadPlayer);
            AddUntilStep("player loaded", () => Player.IsLoaded && Player.Alpha == 1);
        }

        [Test]
        public void TestDualStage()
        {
            AddStep("Load dual stage", () => LoadPlayer(new Mod[] { new ManiaModDualStages() }));
            AddUntilStep("player loaded", () => Player.IsLoaded && Player.Alpha == 1);
        }
    }
}
