// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Graphics;
using sus.Framework.Testing;
using sus.Game.Rulesets;
using sus.Game.Rulesets.Osu;
using sus.Game.Skinning;

namespace sus.Game.Tests.Visual.Gameplay
{
    public abstract partial class SkinnableHUDComponentTestScene : SkinnableTestScene
    {
        protected override Ruleset CreateRulesetForSkinProvider() => new OsuRuleset();

        [SetUpSteps]
        public virtual void SetUpSteps()
        {
            AddStep("setup components", SetUpComponents);
        }

        public void SetUpComponents()
        {
            SetContents(skin =>
            {
                var implementation = skin is LegacySkin
                    ? CreateLegacyImplementation()
                    : skin is ArgonSkin
                        ? CreateArgonImplementation()
                        : CreateDefaultImplementation();

                implementation.Anchor = Anchor.Centre;
                implementation.Origin = Anchor.Centre;
                return implementation;
            });
        }

        protected abstract Drawable CreateDefaultImplementation();
        protected virtual Drawable CreateArgonImplementation() => CreateDefaultImplementation();
        protected abstract Drawable CreateLegacyImplementation();
    }
}
