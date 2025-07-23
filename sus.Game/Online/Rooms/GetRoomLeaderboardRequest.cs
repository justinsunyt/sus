// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Game.Online.API;

namespace sus.Game.Online.Rooms
{
    public class GetRoomLeaderboardRequest : APIRequest<APILeaderboard>
    {
        private readonly long roomId;

        public GetRoomLeaderboardRequest(long roomId)
        {
            this.roomId = roomId;
        }

        protected override string Target => $@"rooms/{roomId}/leaderboard";
    }
}
