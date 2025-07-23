// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Allocation;
using sus.Framework.Extensions.ObjectExtensions;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;

namespace sus.Game.Rulesets.UI
{
    public partial class DrawableRulesetDependenciesProvidingContainer : Container
    {
        private readonly Ruleset ruleset;

        private DrawableRulesetDependencies rulesetDependencies = null!;

        public DrawableRulesetDependenciesProvidingContainer(Ruleset ruleset)
        {
            this.ruleset = ruleset;
            RelativeSizeAxes = Axes.Both;
        }

        protected override IReadOnlyDependencyContainer CreateChildDependencies(IReadOnlyDependencyContainer parent)
        {
            return rulesetDependencies = new DrawableRulesetDependencies(ruleset, base.CreateChildDependencies(parent));
        }

        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);

            if (rulesetDependencies.IsNotNull())
                rulesetDependencies.Dispose();
        }
    }
}
