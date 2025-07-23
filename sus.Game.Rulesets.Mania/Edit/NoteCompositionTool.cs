// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Graphics;
using sus.Game.Beatmaps;
using sus.Game.Rulesets.Edit;
using sus.Game.Rulesets.Edit.Tools;
using sus.Game.Rulesets.Mania.Edit.Blueprints;
using sus.Game.Rulesets.Mania.Objects;

namespace sus.Game.Rulesets.Mania.Edit
{
    public class NoteCompositionTool : CompositionTool
    {
        public NoteCompositionTool()
            : base(nameof(Note))
        {
        }

        public override Drawable CreateIcon() => new BeatmapStatisticIcon(BeatmapStatisticsIconType.Circles);

        public override HitObjectPlacementBlueprint CreatePlacementBlueprint() => new NotePlacementBlueprint();
    }
}
