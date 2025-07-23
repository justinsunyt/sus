// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Allocation;
using sus.Framework.Screens;
using sus.Game.Overlays;
using sus.Game.Overlays.FirstRunSetup;

namespace sus.Game.Tests.Visual.UserInterface
{
    public partial class TestSceneFirstRunScreenBundledBeatmaps : OsuManualInputManagerTestScene
    {
        [Cached]
        private OverlayColourProvider colourProvider = new OverlayColourProvider(OverlayColourScheme.Purple);

        public TestSceneFirstRunScreenBundledBeatmaps()
        {
            AddStep("load screen", () =>
            {
                Child = new ScreenStack(new ScreenBeatmaps());
            });
        }
    }
}
