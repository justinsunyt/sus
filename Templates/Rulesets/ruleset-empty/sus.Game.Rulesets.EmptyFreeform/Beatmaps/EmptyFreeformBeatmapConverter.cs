// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using System.Threading;
using sus.Game.Beatmaps;
using sus.Game.Rulesets.EmptyFreeform.Objects;
using sus.Game.Rulesets.Objects;
using sus.Game.Rulesets.Objects.Types;
using osuTK;

namespace sus.Game.Rulesets.EmptyFreeform.Beatmaps
{
    public class EmptyFreeformBeatmapConverter : BeatmapConverter<EmptyFreeformHitObject>
    {
        public EmptyFreeformBeatmapConverter(IBeatmap beatmap, Ruleset ruleset)
            : base(beatmap, ruleset)
        {
        }

        // todo: Check for conversion types that should be supported (ie. Beatmap.HitObjects.Any(h => h is IHasXPosition))
        // https://github.com/ppy/sus/tree/master/sus.Game/Rulesets/Objects/Types
        public override bool CanConvert() => true;

        protected override IEnumerable<EmptyFreeformHitObject> ConvertHitObject(HitObject original, IBeatmap beatmap, CancellationToken cancellationToken)
        {
            yield return new EmptyFreeformHitObject
            {
                Samples = original.Samples,
                StartTime = original.StartTime,
                Position = (original as IHasPosition)?.Position ?? Vector2.Zero,
            };
        }
    }
}
