// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using sus.Game.Rulesets.Catch.Objects;
using sus.Game.Rulesets.Edit;
using sus.Game.Rulesets.UI;
using sus.Game.Rulesets.UI.Scrolling;

namespace sus.Game.Rulesets.Catch.Edit.Blueprints
{
    public abstract partial class CatchPlacementBlueprint<THitObject> : HitObjectPlacementBlueprint
        where THitObject : CatchHitObject, new()
    {
        protected new THitObject HitObject => (THitObject)base.HitObject;

        protected ScrollingHitObjectContainer HitObjectContainer => (ScrollingHitObjectContainer)playfield.HitObjectContainer;

        [Resolved]
        private Playfield playfield { get; set; } = null!;

        [Resolved]
        protected CatchHitObjectComposer? Composer { get; private set; }

        protected CatchPlacementBlueprint()
            : base(new THitObject())
        {
        }
    }
}
