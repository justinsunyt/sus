// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Graphics;
using sus.Game.Beatmaps;
using sus.Game.Rulesets.Edit;
using sus.Game.Rulesets.Edit.Tools;
using sus.Game.Rulesets.Osu.Edit.Blueprints.Spinners;
using sus.Game.Rulesets.Osu.Objects;

namespace sus.Game.Rulesets.Osu.Edit
{
    public class SpinnerCompositionTool : CompositionTool
    {
        public SpinnerCompositionTool()
            : base(nameof(Spinner))
        {
        }

        public override Drawable CreateIcon() => new BeatmapStatisticIcon(BeatmapStatisticsIconType.Spinners);

        public override HitObjectPlacementBlueprint CreatePlacementBlueprint() => new SpinnerPlacementBlueprint();
    }
}
