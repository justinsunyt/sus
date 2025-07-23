// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Linq;
using sus.Framework.Graphics;
using sus.Game.Online.Multiplayer;
using sus.Game.Online.Multiplayer.MatchTypes.TeamVersus;
using sus.Game.Screens.OnlinePlay.Multiplayer;
using sus.Game.Screens.Play.HUD;
using sus.Game.Screens.Select.Leaderboards;

namespace sus.Game.Tests.Visual.Multiplayer
{
    public partial class TestSceneMultiplayerGameplayLeaderboardTeams : MultiplayerGameplayLeaderboardTestScene
    {
        private int team;

        protected override MultiplayerRoomUser CreateUser(int userId)
        {
            var user = base.CreateUser(userId);
            user.MatchState = new TeamVersusUserState
            {
                TeamID = team++ % 2
            };
            return user;
        }

        protected override MultiplayerLeaderboardProvider CreateLeaderboardProvider() =>
            new MultiplayerLeaderboardProvider(MultiplayerUsers.ToArray())
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
            };

        public override void SetUpSteps()
        {
            base.SetUpSteps();

            AddStep("Add external display components", () =>
            {
                LoadComponentAsync(new MatchScoreDisplay
                {
                    Team1Score = { BindTarget = LeaderboardProvider!.TeamScores[0] },
                    Team2Score = { BindTarget = LeaderboardProvider.TeamScores[1] }
                }, Add);

                GameplayMatchScoreDisplay matchScoreDisplay;
                LoadComponentAsync(matchScoreDisplay = new GameplayMatchScoreDisplay
                {
                    Anchor = Anchor.BottomCentre,
                    Origin = Anchor.BottomCentre,
                    Team1Score = { BindTarget = LeaderboardProvider.TeamScores[0] },
                    Team2Score = { BindTarget = LeaderboardProvider.TeamScores[1] },
                }, Add);

                Leaderboard!.CollapseDuringGameplay.BindValueChanged(_ => matchScoreDisplay.Expanded.Value = !Leaderboard.CollapseDuringGameplay.Value);
            });
        }
    }
}
