// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using sus.Game.Graphics;
using sus.Game.Rulesets.Catch.Objects;
using sus.Game.Rulesets.Catch.Skinning.Default;
using osuTK;

namespace sus.Game.Rulesets.Catch.Edit.Blueprints.Components
{
    public partial class FruitOutline : CompositeDrawable
    {
        public FruitOutline()
        {
            Anchor = Anchor.BottomLeft;
            Origin = Anchor.Centre;
            InternalChild = new BorderPiece();
        }

        [BackgroundDependencyLoader]
        private void load(OsuColour susColour)
        {
            Colour = susColour.Yellow;
        }

        public void UpdateFrom(CatchHitObject hitObject)
        {
            Scale = new Vector2(hitObject.Scale);
        }
    }
}
