// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Allocation;
using sus.Framework.Extensions.ObjectExtensions;
using sus.Game.Tournament.Models;
using sus.Game.Tournament.Screens.Editors;

namespace sus.Game.Tournament.Tests.Screens
{
    public partial class TestSceneSeedingEditorScreen : TournamentScreenTestScene
    {
        [Cached]
        private readonly LadderInfo ladder = new LadderInfo();

        [BackgroundDependencyLoader]
        private void load()
        {
            var match = CreateSampleMatch();

            Add(new SeedingEditorScreen(match.Team1.Value.AsNonNull(), new TeamEditorScreen())
            {
                Width = 0.85f // create room for control panel
            });
        }
    }
}
