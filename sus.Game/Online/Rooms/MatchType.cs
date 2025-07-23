// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Localisation;
using sus.Game.Resources.Localisation.Web;

namespace sus.Game.Online.Rooms
{
    public enum MatchType
    {
        // used for sus-web deserialization so names shouldn't be changed.

        Playlists,

        [LocalisableDescription(typeof(MatchesStrings), nameof(MatchesStrings.MatchTeamTypesHeadToHead))]
        HeadToHead,

        [LocalisableDescription(typeof(MatchesStrings), nameof(MatchesStrings.MatchTeamTypesTeamVersus))]
        TeamVersus,
    }
}
