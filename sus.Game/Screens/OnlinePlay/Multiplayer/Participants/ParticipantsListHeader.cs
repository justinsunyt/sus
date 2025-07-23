// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Allocation;
using sus.Game.Online.Multiplayer;
using sus.Game.Resources.Localisation.Web;
using sus.Game.Screens.OnlinePlay.Components;

namespace sus.Game.Screens.OnlinePlay.Multiplayer.Participants
{
    public partial class ParticipantsListHeader : OverlinedHeader
    {
        [Resolved]
        private MultiplayerClient client { get; set; } = null!;

        public ParticipantsListHeader()
            : base(RankingsStrings.SpotlightParticipants)
        {
        }

        protected override void Update()
        {
            base.Update();

            var room = client.Room;
            if (room == null)
                return;

            Details.Value = room.Users.Count.ToString();
        }
    }
}
