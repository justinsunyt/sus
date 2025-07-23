// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Allocation;
using sus.Game.Tournament.Screens.Editors;

namespace sus.Game.Tournament.Tests.Screens
{
    public partial class TestSceneRoundEditorScreen : TournamentScreenTestScene
    {
        [BackgroundDependencyLoader]
        private void load()
        {
            Add(new RoundEditorScreen
            {
                Width = 0.85f // create room for control panel
            });
        }
    }
}
