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
using sus.Game.Overlays.BeatmapSet;

namespace sus.Game.Tests.Visual.Navigation
{
    public partial class TestSceneStartupBeatmapDisplay : OsuGameTestScene
    {
        private const int requested_beatmap_id = 75;
        private const int requested_beatmap_set_id = 1;

        protected override TestOsuGame CreateTestGame() => new TestOsuGame(LocalStorage, API, new[] { $"sus://b/{requested_beatmap_id}" });

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
                            OnlineID = requested_beatmap_id,
                        }).ToArray();

                        gbr.TriggerSuccess(apiBeatmapSet);
                        return true;
                }

                return false;
            };
        });

        [Test]
        public void TestBeatmapLink()
        {
            AddUntilStep("Beatmap overlay displayed", () => Game.ChildrenOfType<BeatmapSetOverlay>().FirstOrDefault()?.State.Value == Visibility.Visible);
            AddUntilStep("Beatmap overlay showing content", () => Game.ChildrenOfType<BeatmapPicker>().FirstOrDefault()?.Beatmap.Value?.OnlineID == requested_beatmap_id);
        }
    }
}
