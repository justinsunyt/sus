// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Game.Rulesets.Edit;
using sus.Game.Rulesets.Mania.Edit.Blueprints;
using sus.Game.Rulesets.Mania.Objects;
using sus.Game.Rulesets.Mania.Objects.Drawables;
using sus.Game.Rulesets.Objects;
using sus.Game.Rulesets.Objects.Drawables;

namespace sus.Game.Rulesets.Mania.Tests.Editor
{
    public partial class TestSceneHoldNotePlacementBlueprint : ManiaPlacementBlueprintTestScene
    {
        protected override DrawableHitObject CreateHitObject(HitObject hitObject) => new DrawableHoldNote((HoldNote)hitObject);
        protected override HitObjectPlacementBlueprint CreateBlueprint() => new HoldNotePlacementBlueprint();
    }
}
