// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using NUnit.Framework;
using sus.Framework.Allocation;
using sus.Framework.Input.Bindings;
using sus.Framework.Testing;
using sus.Game.Input.Bindings;
using sus.Game.Rulesets;
using sus.Game.Rulesets.Catch;
using sus.Game.Rulesets.Mania;
using sus.Game.Rulesets.Osu;
using sus.Game.Rulesets.Taiko;
using sus.Game.Tests.Visual;
using susTK.Input;

namespace sus.Game.Tests.Input
{
    [HeadlessTest]
    public partial class RealmKeyBindingTest : OsuTestScene
    {
        [Resolved]
        private RulesetStore rulesets { get; set; } = null!;

        [Test]
        public void TestUnmapGlobalAction()
        {
            var keyBinding = new RealmKeyBinding(GlobalAction.ToggleReplaySettings, KeyCombination.FromKey(Key.Z));

            AddAssert("action is integer", () => keyBinding.Action, () => Is.EqualTo((int)GlobalAction.ToggleReplaySettings));
            AddAssert("action unmaps correctly", () => keyBinding.GetAction(rulesets), () => Is.EqualTo(GlobalAction.ToggleReplaySettings));
        }

        [TestCase(typeof(OsuRuleset), OsuAction.Smoke, null)]
        [TestCase(typeof(TaikoRuleset), TaikoAction.LeftCentre, null)]
        [TestCase(typeof(CatchRuleset), CatchAction.MoveRight, null)]
        [TestCase(typeof(ManiaRuleset), ManiaAction.Key7, 7)]
        public void TestUnmapRulesetActions(Type rulesetType, object action, int? variant)
        {
            string rulesetName = ((Ruleset)Activator.CreateInstance(rulesetType)!).ShortName;
            var keyBinding = new RealmKeyBinding(action, KeyCombination.FromKey(Key.Z), rulesetName, variant);

            AddAssert("action is integer", () => keyBinding.Action, () => Is.EqualTo((int)action));
            AddAssert("action unmaps correctly", () => keyBinding.GetAction(rulesets), () => Is.EqualTo(action));
        }
    }
}
