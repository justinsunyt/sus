// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Graphics;
using sus.Game.Rulesets.Catch.Objects;
using susTK;

namespace sus.Game.Rulesets.Catch.Skinning.Default
{
    public partial class DropletPiece : CatchHitObjectPiece
    {
        protected override Drawable HyperBorderPiece { get; }

        public DropletPiece()
        {
            Size = new Vector2(CatchHitObject.OBJECT_RADIUS / 2);

            InternalChildren = new[]
            {
                new Pulp
                {
                    RelativeSizeAxes = Axes.Both,
                    AccentColour = { BindTarget = AccentColour }
                },
                HyperBorderPiece = new HyperDropletBorderPiece()
            };
        }
    }
}
