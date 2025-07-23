// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Allocation;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Sprites;
using sus.Game.Skinning;
using susTK;

namespace sus.Game.Rulesets.Taiko.Skinning.Legacy
{
    public partial class LegacyBarLine : Sprite
    {
        [BackgroundDependencyLoader]
        private void load(ISkinSource skin)
        {
            Texture = skin.GetTexture("taiko-barline");

            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;

            RelativeSizeAxes = Axes.Both;
            Size = new Vector2(1, 0.88f);
            FillMode = FillMode.Fill;
        }
    }
}
