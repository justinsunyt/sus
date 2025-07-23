// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Screens;
using sus.Game.Database;
using sus.Game.Overlays;
using sus.Game.Overlays.FirstRunSetup;
using sus.Game.Tests.Beatmaps;

namespace sus.Game.Tests.Visual.UserInterface
{
    public partial class TestSceneFirstRunScreenUIScale : OsuManualInputManagerTestScene
    {
        [Cached]
        private OverlayColourProvider colourProvider = new OverlayColourProvider(OverlayColourScheme.Purple);

        [Cached(typeof(BeatmapStore))]
        private BeatmapStore beatmapStore = new TestBeatmapStore();

        public TestSceneFirstRunScreenUIScale()
        {
            AddStep("load screen", () =>
            {
                Child = new ScreenStack(new ScreenUIScale());
            });
        }
    }
}
