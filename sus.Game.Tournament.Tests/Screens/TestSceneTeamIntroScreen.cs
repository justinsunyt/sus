// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Linq;
using sus.Framework.Allocation;
using sus.Framework.Graphics;
using sus.Game.Tournament.Models;
using sus.Game.Tournament.Screens.TeamIntro;

namespace sus.Game.Tournament.Tests.Screens
{
    public partial class TestSceneTeamIntroScreen : TournamentScreenTestScene
    {
        [Cached]
        private readonly LadderInfo ladder = new LadderInfo();

        [BackgroundDependencyLoader]
        private void load()
        {
            ladder.CurrentMatch.Value = new TournamentMatch
            {
                Team1 = { Value = Ladder.Teams.FirstOrDefault(t => t.Acronym.Value == "USA") },
                Team2 = { Value = Ladder.Teams.FirstOrDefault(t => t.Acronym.Value == "JPN") },
                Round = { Value = Ladder.Rounds.FirstOrDefault(g => g.Name.Value == "Finals") }
            };

            Add(new TeamIntroScreen
            {
                FillMode = FillMode.Fit,
                FillAspectRatio = 16 / 9f
            });
        }
    }
}
