// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Graphics;
using sus.Game.Beatmaps;
using sus.Game.Rulesets.Catch.Edit.Blueprints;
using sus.Game.Rulesets.Catch.Objects;
using sus.Game.Rulesets.Edit;
using sus.Game.Rulesets.Edit.Tools;

namespace sus.Game.Rulesets.Catch.Edit
{
    public class BananaShowerCompositionTool : CompositionTool
    {
        public BananaShowerCompositionTool()
            : base(nameof(BananaShower))
        {
        }

        public override Drawable CreateIcon() => new BeatmapStatisticIcon(BeatmapStatisticsIconType.Spinners);

        public override HitObjectPlacementBlueprint CreatePlacementBlueprint() => new BananaShowerPlacementBlueprint();
    }
}
