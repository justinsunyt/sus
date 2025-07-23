// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Allocation;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Sprites;
using sus.Framework.Graphics.Textures;
using sus.Game.Rulesets.UI;
using susTK;

namespace sus.Game.Rulesets.Pippidon.UI
{
    public partial class PippidonCursorContainer : GameplayCursorContainer
    {
        private Sprite cursorSprite;
        private Texture cursorTexture;

        protected override Drawable CreateCursor() => cursorSprite = new Sprite
        {
            Scale = new Vector2(0.5f),
            Origin = Anchor.Centre,
            Texture = cursorTexture,
        };

        [BackgroundDependencyLoader]
        private void load(TextureStore textures)
        {
            cursorTexture = textures.Get("character");

            if (cursorSprite != null)
                cursorSprite.Texture = cursorTexture;
        }
    }
}
