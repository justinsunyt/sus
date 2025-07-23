// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Graphics;
using sus.Game.Beatmaps;
using sus.Game.Rulesets.Edit;
using sus.Game.Rulesets.Edit.Tools;
using sus.Game.Rulesets.Osu.Edit.Blueprints.HitCircles;
using sus.Game.Rulesets.Osu.Objects;

namespace sus.Game.Rulesets.Osu.Edit
{
    public class HitCircleCompositionTool : CompositionTool
    {
        public HitCircleCompositionTool()
            : base(nameof(HitCircle))
        {
        }

        public override Drawable CreateIcon() => new BeatmapStatisticIcon(BeatmapStatisticsIconType.Circles);

        public override HitObjectPlacementBlueprint CreatePlacementBlueprint() => new HitCirclePlacementBlueprint();
    }
}
