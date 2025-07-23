// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using sus.Game.Online.Rooms;
using sus.Game.Overlays.Dialog;

namespace sus.Game.Screens.OnlinePlay.Playlists
{
    public partial class ClosePlaylistDialog : DeletionDialog
    {
        public ClosePlaylistDialog(Room room, Action closeAction)
        {
            HeaderText = "Are you sure you want to close the following playlist:";
            BodyText = room.Name;
            DangerousAction = closeAction;
        }
    }
}
