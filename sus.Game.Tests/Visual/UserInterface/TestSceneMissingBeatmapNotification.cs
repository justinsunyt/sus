// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Linq;
using NUnit.Framework;
using sus.Framework.Allocation;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Game.Database;
using sus.Game.Overlays;
using sus.Game.Tests.Scores.IO;

namespace sus.Game.Tests.Visual.UserInterface
{
    [TestFixture]
    public partial class TestSceneMissingBeatmapNotification : OsuTestScene
    {
        [Cached]
        private OverlayColourProvider colourProvider = new OverlayColourProvider(OverlayColourScheme.Purple);

        [BackgroundDependencyLoader]
        private void load()
        {
            Child = new Container
            {
                Width = 280,
                AutoSizeAxes = Axes.Y,
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Child = new MissingBeatmapNotification(CreateAPIBeatmapSet(Ruleset.Value).Beatmaps.First(), "deadbeef", new ImportScoreTest.TestArchiveReader())
            };
        }
    }
}
