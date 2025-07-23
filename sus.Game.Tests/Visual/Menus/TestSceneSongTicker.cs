// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using sus.Game.Overlays;
using sus.Game.Screens.Menu;

namespace sus.Game.Tests.Visual.Menus
{
    public partial class TestSceneSongTicker : OsuTestScene
    {
        public TestSceneSongTicker()
        {
            AddRange(new Drawable[]
            {
                new SongTicker
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                },
                new NowPlayingOverlay
                {
                    Origin = Anchor.TopRight,
                    Anchor = Anchor.TopRight,
                    State = { Value = Visibility.Visible }
                }
            });
        }
    }
}
