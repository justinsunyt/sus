// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using NUnit.Framework;
using sus.Game.Rulesets;
using sus.Game.Rulesets.Mods;
using sus.Game.Rulesets.Osu;

namespace sus.Game.Tests.Visual.Gameplay
{
    public partial class TestSceneUnknownMod : ModTestScene
    {
        protected override Ruleset CreatePlayerRuleset() => new OsuRuleset();

        /// <summary>
        /// This test also covers the scenario of exiting Player after an unsuccessful beatmap load.
        /// </summary>
        [Test]
        public void TestUnknownModDoesntEnterGameplay()
        {
            CreateModTest(new ModTestData
            {
                Mod = new UnknownMod("WNG"),
                PassCondition = () => Player.IsLoaded && !Player.LoadedBeatmapSuccessfully
            });
        }
    }
}
