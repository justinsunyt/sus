// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Graphics;
using sus.Game.Beatmaps;
using sus.Game.Rulesets.Edit;
using sus.Game.Rulesets.Edit.Tools;
using sus.Game.Rulesets.Taiko.Edit.Blueprints;
using sus.Game.Rulesets.Taiko.Objects;

namespace sus.Game.Rulesets.Taiko.Edit
{
    public class DrumRollCompositionTool : CompositionTool
    {
        public DrumRollCompositionTool()
            : base(nameof(DrumRoll))
        {
        }

        public override Drawable CreateIcon() => new BeatmapStatisticIcon(BeatmapStatisticsIconType.Sliders);

        public override HitObjectPlacementBlueprint CreatePlacementBlueprint() => new DrumRollPlacementBlueprint();
    }
}
