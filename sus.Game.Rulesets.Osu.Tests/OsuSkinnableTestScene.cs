// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

#nullable disable

using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Game.Tests.Visual;

namespace sus.Game.Rulesets.Osu.Tests
{
    public abstract partial class OsuSkinnableTestScene : SkinnableTestScene
    {
        private Container content;

        protected override Container<Drawable> Content
        {
            get
            {
                if (content == null)
                    base.Content.Add(content = new OsuInputManager(new OsuRuleset().RulesetInfo));

                return content;
            }
        }

        protected override Ruleset CreateRulesetForSkinProvider() => new OsuRuleset();
    }
}
