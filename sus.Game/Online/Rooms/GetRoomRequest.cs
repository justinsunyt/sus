// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Game.Online.API;

namespace sus.Game.Online.Rooms
{
    public class GetRoomRequest : APIRequest<Room>
    {
        public readonly long RoomId;

        public GetRoomRequest(long roomId)
        {
            RoomId = roomId;
        }

        protected override string Target => $"rooms/{RoomId}";
    }
}
