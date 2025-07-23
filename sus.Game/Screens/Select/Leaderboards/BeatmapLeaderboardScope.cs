// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.ComponentModel;
using sus.Framework.Localisation;
using sus.Game.Resources.Localisation.Web;

namespace sus.Game.Screens.Select.Leaderboards
{
    public enum BeatmapLeaderboardScope
    {
        [Description("Local Ranking")]
        Local,

        [LocalisableDescription(typeof(BeatmapsetsStrings), nameof(BeatmapsetsStrings.ShowScoreboardGlobal))]
        Global,

        [LocalisableDescription(typeof(BeatmapsetsStrings), nameof(BeatmapsetsStrings.ShowScoreboardCountry))]
        Country,

        [LocalisableDescription(typeof(BeatmapsetsStrings), nameof(BeatmapsetsStrings.ShowScoreboardFriend))]
        Friend,

        [LocalisableDescription(typeof(BeatmapsetsStrings), nameof(BeatmapsetsStrings.ShowScoreboardTeam))]
        Team,
    }
}
