// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Testing;
using sus.Game.Graphics;
using sus.Game.Graphics.Backgrounds;

namespace sus.Game.Tournament.Tests
{
    public partial class TournamentTestBrowser : TournamentGameBase
    {
        protected override void LoadComplete()
        {
            base.LoadComplete();

            BracketLoadTask.ContinueWith(_ => Schedule(() =>
            {
                LoadComponentAsync(new Background("Menu/menu-background-0")
                {
                    Colour = OsuColour.Gray(0.5f),
                    Depth = 10
                }, Add);

                // Have to construct this here, rather than in the constructor, because
                // we depend on some dependencies to be loaded within OsuGameBase.load().
                Add(new TestBrowser());
            }));
        }
    }
}
