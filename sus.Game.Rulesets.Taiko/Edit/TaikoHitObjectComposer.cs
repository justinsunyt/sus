// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using osu.Framework.Allocation;
using sus.Game.Beatmaps;
using sus.Game.Rulesets.Edit;
using sus.Game.Rulesets.Edit.Tools;
using sus.Game.Rulesets.Mods;
using sus.Game.Rulesets.Taiko.Objects;
using sus.Game.Rulesets.UI;
using sus.Game.Screens.Edit.Compose.Components;

namespace sus.Game.Rulesets.Taiko.Edit
{
    [Cached]
    public partial class TaikoHitObjectComposer : ScrollingHitObjectComposer<TaikoHitObject>
    {
        protected override bool ApplyHorizontalCentering => false;

        public TaikoHitObjectComposer(TaikoRuleset ruleset)
            : base(ruleset)
        {
        }

        protected override IReadOnlyList<CompositionTool> CompositionTools => new CompositionTool[]
        {
            new HitCompositionTool(),
            new DrumRollCompositionTool(),
            new SwellCompositionTool()
        };

        protected override DrawableRuleset<TaikoHitObject> CreateDrawableRuleset(Ruleset ruleset, IBeatmap beatmap, IReadOnlyList<Mod> mods) =>
            new DrawableTaikoEditorRuleset(ruleset, beatmap, mods);

        protected override ComposeBlueprintContainer CreateBlueprintContainer()
            => new TaikoBlueprintContainer(this);

        protected override BeatSnapGrid CreateBeatSnapGrid() => new TaikoBeatSnapGrid();
    }
}
