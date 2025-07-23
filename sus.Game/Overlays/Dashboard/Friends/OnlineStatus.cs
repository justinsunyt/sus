// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Localisation;
using sus.Game.Resources.Localisation.Web;

namespace sus.Game.Overlays.Dashboard.Friends
{
    public enum OnlineStatus
    {
        [LocalisableDescription(typeof(UsersStrings), nameof(UsersStrings.StatusAll))]
        All,

        [LocalisableDescription(typeof(UsersStrings), nameof(UsersStrings.StatusOnline))]
        Online,

        [LocalisableDescription(typeof(UsersStrings), nameof(UsersStrings.StatusOffline))]
        Offline
    }
}
