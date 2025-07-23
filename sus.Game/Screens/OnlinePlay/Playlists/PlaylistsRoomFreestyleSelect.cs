// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Bindables;
using sus.Framework.Screens;
using sus.Game.Beatmaps;
using sus.Game.Online.Rooms;
using sus.Game.Rulesets;

namespace sus.Game.Screens.OnlinePlay.Playlists
{
    public partial class PlaylistsRoomFreestyleSelect : OnlinePlayFreestyleSelect
    {
        public new readonly Bindable<BeatmapInfo?> Beatmap = new Bindable<BeatmapInfo?>();
        public new readonly Bindable<RulesetInfo?> Ruleset = new Bindable<RulesetInfo?>();

        public PlaylistsRoomFreestyleSelect(Room room, PlaylistItem item)
            : base(room, item)
        {
        }

        protected override bool OnStart()
        {
            if (!base.OnStart())
                return false;

            Beatmap.Value = base.Beatmap.Value.BeatmapInfo;
            Ruleset.Value = base.Ruleset.Value;

            this.Exit();
            return true;
        }
    }
}
