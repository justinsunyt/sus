// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Framework.Graphics.Cursor;
using sus.Game.Screens.OnlinePlay;
using sus.Game.Screens.OnlinePlay.Multiplayer;
using sus.Game.Screens.OnlinePlay.Multiplayer.Match;

namespace sus.Game.Tests.Visual.Multiplayer
{
    public partial class TestSceneMultiplayerMatchFooter : MultiplayerTestScene
    {
        public override void SetUpSteps()
        {
            base.SetUpSteps();

            AddStep("create footer", () =>
            {
                MultiplayerBeatmapAvailabilityTracker tracker = new MultiplayerBeatmapAvailabilityTracker();

                Child = new DependencyProvidingContainer
                {
                    RelativeSizeAxes = Axes.Both,
                    CachedDependencies =
                    [
                        (typeof(OnlinePlayBeatmapAvailabilityTracker), tracker)
                    ],
                    Children =
                    [
                        tracker,
                        new PopoverContainer
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            RelativeSizeAxes = Axes.Both,
                            Child = new Container
                            {
                                Anchor = Anchor.Centre,
                                Origin = Anchor.Centre,
                                RelativeSizeAxes = Axes.X,
                                Height = 50,
                                Child = new MultiplayerMatchFooter()
                            }
                        }
                    ]
                };
            });
        }
    }
}
