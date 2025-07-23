// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.ComponentModel;
using sus.Framework.Localisation;
using sus.Game.Resources.Localisation.Web;

namespace sus.Game.Overlays.BeatmapListing
{
    public enum SearchExtra
    {
        [LocalisableDescription(typeof(BeatmapsStrings), nameof(BeatmapsStrings.ExtraVideo))]
        [Description("Has Video")]
        Video,

        [LocalisableDescription(typeof(BeatmapsStrings), nameof(BeatmapsStrings.ExtraStoryboard))]
        [Description("Has Storyboard")]
        Storyboard
    }
}
