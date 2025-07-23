// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using NUnit.Framework;
using sus.Game.Online.Rooms;
using sus.Game.Rulesets.Osu;
using sus.Game.Screens.OnlinePlay.Multiplayer;
using sus.Game.Tests.Resources;

namespace sus.Game.Tests.Visual.Multiplayer
{
    public partial class TestSceneMultiplayerResults : ScreenTestScene
    {
        [Test]
        public void TestDisplayResults()
        {
            MultiplayerResultsScreen screen = null!;

            AddStep("show results screen", () =>
            {
                var rulesetInfo = new OsuRuleset().RulesetInfo;
                var beatmapInfo = CreateBeatmap(rulesetInfo).BeatmapInfo;
                var score = TestResources.CreateTestScoreInfo(beatmapInfo);

                Stack.Push(screen = new MultiplayerResultsScreen(score, 1, new PlaylistItem(beatmapInfo)));
            });

            AddUntilStep("wait for loaded", () => screen.IsLoaded);
        }
    }
}
