// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Linq;
using sus.Framework.Graphics;
using sus.Game.Online.API;
using sus.Game.Online.Rooms;
using sus.Game.Rulesets.Osu;
using sus.Game.Rulesets.Osu.Mods;
using sus.Game.Screens.OnlinePlay.Components;
using sus.Game.Tests.Beatmaps;
using sus.Game.Tests.Visual.OnlinePlay;
using susTK;

namespace sus.Game.Tests.Visual.Multiplayer
{
    public partial class TestSceneMatchBeatmapDetailArea : OnlinePlayTestScene
    {
        private Room room = null!;

        public override void SetUpSteps()
        {
            base.SetUpSteps();

            AddStep("create area", () =>
            {
                Child = new MatchBeatmapDetailArea(room = new Room())
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Size = new Vector2(500),
                    CreateNewItem = createNewItem
                };
            });
        }

        private void createNewItem()
        {
            room.Playlist = room.Playlist.Append(new PlaylistItem(new TestBeatmap(new OsuRuleset().RulesetInfo).BeatmapInfo)
            {
                ID = room.Playlist.Count,
                RulesetID = new OsuRuleset().RulesetInfo.OnlineID,
                RequiredMods = new[]
                {
                    new APIMod(new OsuModHardRock()),
                    new APIMod(new OsuModDoubleTime()),
                    new APIMod(new OsuModAutoplay())
                }
            }).ToArray();
        }
    }
}
