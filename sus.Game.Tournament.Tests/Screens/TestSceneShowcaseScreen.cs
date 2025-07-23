// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using sus.Game.Tournament.Screens.Showcase;

namespace sus.Game.Tournament.Tests.Screens
{
    public partial class TestSceneShowcaseScreen : TournamentScreenTestScene
    {
        [BackgroundDependencyLoader]
        private void load()
        {
            Add(new ShowcaseScreen());
        }
    }
}
