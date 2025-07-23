// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Allocation;

namespace sus.Game.Tournament.Tests
{
    public partial class TestSceneTournamentSceneManager : TournamentTestScene
    {
        [BackgroundDependencyLoader]
        private void load()
        {
            Add(new TournamentSceneManager());
        }
    }
}
