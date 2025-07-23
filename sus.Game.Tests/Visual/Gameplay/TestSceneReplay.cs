// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

#nullable disable

using System;
using System.ComponentModel;
using System.Linq;
using sus.Game.Rulesets;
using sus.Game.Rulesets.Mods;
using sus.Game.Rulesets.Scoring;
using sus.Game.Scoring;
using sus.Game.Screens.Play;

namespace sus.Game.Tests.Visual.Gameplay
{
    [Description("Player instantiated with a replay.")]
    public partial class TestSceneReplay : TestSceneAllRulesetPlayers
    {
        protected override Player CreatePlayer(Ruleset ruleset)
        {
            var beatmap = Beatmap.Value.GetPlayableBeatmap(ruleset.RulesetInfo, Array.Empty<Mod>());

            return new ScoreAccessibleReplayPlayer(ruleset.GetAutoplayMod()?.CreateScoreFromReplayData(beatmap, Array.Empty<Mod>()));
        }

        protected override void AddCheckSteps()
        {
            AddUntilStep("score above zero", () => ((ScoreAccessibleReplayPlayer)Player).ScoreProcessor.TotalScore.Value > 0);
            AddUntilStep("key counter counted keys", () => ((ScoreAccessibleReplayPlayer)Player).HUDOverlay.InputCountController.Triggers.Any(kc => kc.ActivationCount.Value > 0));
            AddAssert("cannot fail", () => !((ScoreAccessibleReplayPlayer)Player).AllowFail);
        }

        private partial class ScoreAccessibleReplayPlayer : ReplayPlayer
        {
            public new ScoreProcessor ScoreProcessor => base.ScoreProcessor;
            public new HUDOverlay HUDOverlay => base.HUDOverlay;

            public bool AllowFail => base.CheckModsAllowFailure();

            protected override bool PauseOnFocusLost => false;

            public ScoreAccessibleReplayPlayer(Score score)
                : base(score)
            {
            }
        }
    }
}
