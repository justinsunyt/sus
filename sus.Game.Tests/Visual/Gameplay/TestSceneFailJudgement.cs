// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Linq;
using sus.Game.Rulesets;
using sus.Game.Rulesets.Mods;
using sus.Game.Rulesets.Scoring;
using sus.Game.Scoring;
using sus.Game.Screens.Play;

namespace sus.Game.Tests.Visual.Gameplay
{
    public partial class TestSceneFailJudgement : TestSceneAllRulesetPlayers
    {
        protected override Player CreatePlayer(Ruleset ruleset)
        {
            SelectedMods.Value = Array.Empty<Mod>();
            return new FailPlayer();
        }

        protected override void AddCheckSteps()
        {
            AddUntilStep("player is playing", () => Player.LocalUserPlaying.Value);
            AddUntilStep("wait for fail", () => Player.GameplayState.HasFailed);
            AddAssert("player is not playing", () => !Player.LocalUserPlaying.Value);
            AddUntilStep("wait for multiple judgements", () => ((FailPlayer)Player).ScoreProcessor.JudgedHits > 1);
            AddAssert("total number of results == 1", () =>
            {
                var score = new ScoreInfo { Ruleset = Ruleset.Value };

                ((FailPlayer)Player).ScoreProcessor.PopulateScore(score);

                return score.Statistics.Values.Sum() == 1;
            });
        }

        private partial class FailPlayer : TestPlayer
        {
            public new HealthProcessor HealthProcessor => base.HealthProcessor;

            public FailPlayer()
                : base(false, false)
            {
            }

            protected override void LoadComplete()
            {
                base.LoadComplete();
                HealthProcessor.FailConditions += (_, _) => true;
            }
        }
    }
}
