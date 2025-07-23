// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Allocation;
using sus.Framework.Graphics;
using sus.Game.Graphics.Cursor;
using sus.Game.Tournament.Screens.Ladder;

namespace sus.Game.Tournament.Tests.Screens
{
    public partial class TestSceneLadderScreen : TournamentScreenTestScene
    {
        [BackgroundDependencyLoader]
        private void load()
        {
            Add(new OsuContextMenuContainer
            {
                RelativeSizeAxes = Axes.Both,
                Child = new LadderScreen()
            });
        }
    }
}
