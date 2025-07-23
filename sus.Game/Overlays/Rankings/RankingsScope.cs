// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Localisation;
using sus.Game.Resources.Localisation.Web;

namespace sus.Game.Overlays.Rankings
{
    public enum RankingsScope
    {
        [LocalisableDescription(typeof(RankingsStrings), nameof(RankingsStrings.StatPerformance))]
        Performance,

        [LocalisableDescription(typeof(RankingsStrings), nameof(RankingsStrings.StatRankedScore))]
        Score,

        [LocalisableDescription(typeof(RankingsStrings), nameof(RankingsStrings.TypeCountry))]
        Country,

        [LocalisableDescription(typeof(RankingsStrings), nameof(RankingsStrings.TypeCharts))]
        Spotlights,

        [LocalisableDescription(typeof(RankingsStrings), nameof(RankingsStrings.TypeKudsus))]
        Kudsus,
    }
}
