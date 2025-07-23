// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Allocation;
using sus.Framework.Graphics;
using sus.Game.Graphics;
using sus.Game.Rulesets.Osu.Objects;
using sus.Game.Rulesets.Osu.Skinning.Default;
using susTK;

namespace sus.Game.Rulesets.Osu.Edit.Blueprints.HitCircles.Components
{
    public partial class HitCirclePiece : BlueprintPiece<HitCircle>
    {
        public HitCirclePiece()
        {
            Origin = Anchor.Centre;

            Size = OsuHitObject.OBJECT_DIMENSIONS;

            CornerRadius = Size.X / 2;
            CornerExponent = 2;

            InternalChild = new RingPiece();
        }

        [BackgroundDependencyLoader]
        private void load(OsuColour colours)
        {
            Colour = colours.Yellow;
        }

        public override void UpdateFrom(HitCircle hitObject)
        {
            base.UpdateFrom(hitObject);

            Scale = new Vector2(hitObject.Scale);
        }
    }
}
