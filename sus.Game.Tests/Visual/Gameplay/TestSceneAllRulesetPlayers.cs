// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

#nullable disable

using NUnit.Framework;
using sus.Framework.Allocation;
using sus.Game.Configuration;
using sus.Game.Rulesets;
using sus.Game.Rulesets.Catch;
using sus.Game.Rulesets.Mania;
using sus.Game.Rulesets.Mods;
using sus.Game.Rulesets.Osu;
using sus.Game.Rulesets.Taiko;
using sus.Game.Screens.Play;

namespace sus.Game.Tests.Visual.Gameplay
{
    /// <summary>
    /// A base class which runs <see cref="Player"/> test for all available rulesets.
    /// Steps to be run for each ruleset should be added via <see cref="AddCheckSteps"/>.
    /// </summary>
    public abstract partial class TestSceneAllRulesetPlayers : RateAdjustedBeatmapTestScene
    {
        protected Player Player { get; private set; }

        protected OsuConfigManager Config { get; private set; }

        [BackgroundDependencyLoader]
        private void load()
        {
            Dependencies.Cache(Config = new OsuConfigManager(LocalStorage));
            Config.GetBindable<double>(OsuSetting.DimLevel).Value = 1.0;
        }

        [Test]
        public void TestOsu() => runForRuleset(new OsuRuleset().RulesetInfo);

        [Test]
        public void TestTaiko() => runForRuleset(new TaikoRuleset().RulesetInfo);

        [Test]
        public void TestCatch() => runForRuleset(new CatchRuleset().RulesetInfo);

        [Test]
        public void TestMania() => runForRuleset(new ManiaRuleset().RulesetInfo);

        private void runForRuleset(RulesetInfo ruleset)
        {
            Player p = null;
            AddStep($"load {ruleset.Name} player", () => p = loadPlayerFor(ruleset));
            AddUntilStep("player loaded", () =>
            {
                if (p?.IsLoaded == true)
                {
                    p = null;
                    return true;
                }

                return false;
            });

            AddCheckSteps();
        }

        protected abstract void AddCheckSteps();

        private Player loadPlayerFor(RulesetInfo rulesetInfo)
        {
            // if a player screen is present already, we must exit that before loading another one,
            // otherwise it'll crash on SpectatorClient.BeginPlaying being called while client is in "playing" state already.
            if (Stack.CurrentScreen is Player)
                Stack.Exit();

            Ruleset.Value = rulesetInfo;
            var ruleset = rulesetInfo.CreateInstance();

            var working = CreateWorkingBeatmap(rulesetInfo);

            Beatmap.Value = working;
            SelectedMods.Value = new[] { ruleset.CreateMod<ModNoFail>() };

            Player = CreatePlayer(ruleset);

            LoadScreen(Player);

            return Player;
        }

        protected virtual Player CreatePlayer(Ruleset ruleset) => new TestPlayer(false, false);
    }
}
