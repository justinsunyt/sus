// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Linq;
using NUnit.Framework;
using sus.Framework.Graphics.Containers;
using sus.Framework.Testing;
using sus.Game.Online.API;
using sus.Game.Online.API.Requests;
using sus.Game.Online.API.Requests.Responses;
using sus.Game.Overlays;

namespace sus.Game.Tests.Visual.Navigation
{
    public partial class TestSceneStartupBeatmapSetDisplay : OsuGameTestScene
    {
        private const int requested_beatmap_set_id = 1;

        protected override TestOsuGame CreateTestGame() => new TestOsuGame(LocalStorage, API, new[] { $"sus://s/{requested_beatmap_set_id}" });

        [SetUp]
        public void Setup() => Schedule(() =>
        {
            ((DummyAPIAccess)API).HandleRequest = request =>
            {
                switch (request)
                {
                    case GetBeatmapSetRequest gbr:

                        var apiBeatmapSet = CreateAPIBeatmapSet();
                        apiBeatmapSet.OnlineID = requested_beatmap_set_id;
                        apiBeatmapSet.Beatmaps = apiBeatmapSet.Beatmaps.Append(new APIBeatmap
                        {
                            DifficultyName = "Target difficulty",
                            OnlineID = 75,
                        }).ToArray();
                        gbr.TriggerSuccess(apiBeatmapSet);
                        return true;
                }

                return false;
            };
        });

        [Test]
        public void TestBeatmapSetLink()
        {
            AddUntilStep("Beatmap overlay displayed", () => Game.ChildrenOfType<BeatmapSetOverlay>().FirstOrDefault()?.State.Value == Visibility.Visible);
            AddUntilStep("Beatmap overlay showing content", () => Game.ChildrenOfType<BeatmapSetOverlay>().FirstOrDefault()?.Header.BeatmapSet.Value.OnlineID == requested_beatmap_set_id);
        }
    }
}
