// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

#nullable disable

using JetBrains.Annotations;
using sus.Game.Rulesets.Objects;
using sus.Game.Rulesets.Objects.Types;
using sus.Game.Rulesets.Osu.Objects;
using sus.Game.Screens.Edit.Compose.Components;

namespace sus.Game.Rulesets.Osu.Edit
{
    public partial class OsuDistanceSnapGrid : CircularDistanceSnapGrid
    {
        public OsuDistanceSnapGrid(OsuHitObject hitObject, [CanBeNull] OsuHitObject nextHitObject = null, [CanBeNull] IHasSliderVelocity sliderVelocitySource = null)
            : base(hitObject.StackedEndPosition, hitObject.GetEndTime(), nextHitObject?.StartTime - 1, sliderVelocitySource)
        {
            Masking = true;
        }
    }
}
