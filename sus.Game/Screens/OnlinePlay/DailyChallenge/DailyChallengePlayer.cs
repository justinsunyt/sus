// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Game.Online.Rooms;
using sus.Game.Screens.OnlinePlay.Playlists;
using sus.Game.Users;

namespace sus.Game.Screens.OnlinePlay.DailyChallenge
{
    public partial class DailyChallengePlayer : PlaylistsPlayer
    {
        protected override UserActivity InitialActivity => new UserActivity.PlayingDailyChallenge(Beatmap.Value.BeatmapInfo, Ruleset.Value);

        public DailyChallengePlayer(Room room, PlaylistItem playlistItem)
            : base(room, playlistItem)
        {
        }
    }
}
