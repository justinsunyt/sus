// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Game.Overlays.Notifications;

namespace sus.Game.Database
{
    public partial class ImportProgressNotification : ProgressNotification
    {
        public ImportProgressNotification()
        {
            State = ProgressNotificationState.Active;
        }
    }
}
