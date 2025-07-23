// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Allocation;
using sus.Framework.Graphics.Rendering;
using sus.Framework.Graphics.Sprites;
using sus.Framework.Graphics.Textures;

namespace sus.Game.Rulesets.Pippidon
{
    public partial class PippidonRulesetIcon : Sprite
    {
        private readonly Ruleset ruleset;

        public PippidonRulesetIcon(Ruleset ruleset)
        {
            this.ruleset = ruleset;
        }

        [BackgroundDependencyLoader]
        private void load(IRenderer renderer)
        {
            Texture = new TextureStore(renderer, new TextureLoaderStore(ruleset.CreateResourceStore()), false).Get("Textures/coin");
        }
    }
}
