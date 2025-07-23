// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Game.Rulesets.Edit;
using sus.Game.Rulesets.Objects;
using sus.Game.Rulesets.Objects.Drawables;
using sus.Game.Rulesets.Osu.Edit.Blueprints.HitCircles;
using sus.Game.Rulesets.Osu.Objects;
using sus.Game.Rulesets.Osu.Objects.Drawables;
using sus.Game.Tests.Visual;

namespace sus.Game.Rulesets.Osu.Tests.Editor
{
    public partial class TestSceneHitCirclePlacementBlueprint : PlacementBlueprintTestScene
    {
        protected sealed override Ruleset CreateRuleset() => new OsuRuleset();
        protected override DrawableHitObject CreateHitObject(HitObject hitObject) => new DrawableHitCircle((HitCircle)hitObject);
        protected override HitObjectPlacementBlueprint CreateBlueprint() => new HitCirclePlacementBlueprint();
    }
}
