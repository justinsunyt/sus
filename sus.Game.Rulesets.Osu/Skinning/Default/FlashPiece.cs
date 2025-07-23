// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Framework.Graphics.Shapes;
using sus.Game.Rulesets.Osu.Objects;

namespace sus.Game.Rulesets.Osu.Skinning.Default
{
    public partial class FlashPiece : Container
    {
        public FlashPiece()
        {
            Size = OsuHitObject.OBJECT_DIMENSIONS;

            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;

            Blending = BlendingParameters.Additive;
            Alpha = 0;

            Child = new CircularContainer
            {
                Masking = true,
                RelativeSizeAxes = Axes.Both,
                Child = new Box
                {
                    RelativeSizeAxes = Axes.Both
                }
            };
        }
    }
}
