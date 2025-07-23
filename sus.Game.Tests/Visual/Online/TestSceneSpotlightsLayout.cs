// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Allocation;
using sus.Framework.Bindables;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Game.Overlays;
using sus.Game.Overlays.Rankings;
using sus.Game.Rulesets;
using sus.Game.Rulesets.Catch;
using sus.Game.Rulesets.Mania;
using sus.Game.Rulesets.Osu;
using sus.Game.Rulesets.Taiko;

namespace sus.Game.Tests.Visual.Online
{
    public partial class TestSceneSpotlightsLayout : OsuTestScene
    {
        protected override bool UseOnlineAPI => true;

        [Cached]
        private readonly OverlayColourProvider overlayColour = new OverlayColourProvider(OverlayColourScheme.Green);

        public TestSceneSpotlightsLayout()
        {
            var ruleset = new Bindable<RulesetInfo>(new OsuRuleset().RulesetInfo);

            Add(new BasicScrollContainer
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                RelativeSizeAxes = Axes.Both,
                Width = 0.8f,
                Child = new SpotlightsLayout
                {
                    Ruleset = { BindTarget = ruleset }
                }
            });

            AddStep("Osu ruleset", () => ruleset.Value = new OsuRuleset().RulesetInfo);
            AddStep("Mania ruleset", () => ruleset.Value = new ManiaRuleset().RulesetInfo);
            AddStep("Taiko ruleset", () => ruleset.Value = new TaikoRuleset().RulesetInfo);
            AddStep("Catch ruleset", () => ruleset.Value = new CatchRuleset().RulesetInfo);
        }
    }
}
