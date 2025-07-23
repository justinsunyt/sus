// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Graphics;
using sus.Game.Beatmaps;
using sus.Game.Rulesets.Edit;
using sus.Game.Rulesets.Edit.Tools;
using sus.Game.Rulesets.Mania.Edit.Blueprints;

namespace sus.Game.Rulesets.Mania.Edit
{
    public class HoldNoteCompositionTool : CompositionTool
    {
        public HoldNoteCompositionTool()
            : base("Hold")
        {
        }

        public override Drawable CreateIcon() => new BeatmapStatisticIcon(BeatmapStatisticsIconType.Sliders);

        public override HitObjectPlacementBlueprint CreatePlacementBlueprint() => new HoldNotePlacementBlueprint();
    }
}
