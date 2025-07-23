// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Localisation;
using sus.Game.Resources.Localisation.Web;

namespace sus.Game.Overlays.BeatmapListing
{
    public enum SearchPlayed
    {
        [LocalisableDescription(typeof(BeatmapsStrings), nameof(BeatmapsStrings.PlayedAny))]
        Any,

        [LocalisableDescription(typeof(BeatmapsStrings), nameof(BeatmapsStrings.PlayedPlayed))]
        Played,

        [LocalisableDescription(typeof(BeatmapsStrings), nameof(BeatmapsStrings.PlayedUnplayed))]
        Unplayed
    }
}
