// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Graphics;
using sus.Game.Overlays.Profile.Header.Components;
using sus.Game.Rulesets.Catch;
using sus.Game.Rulesets.Mania;
using sus.Game.Rulesets.Osu;
using sus.Game.Rulesets.Taiko;
using sus.Framework.Bindables;
using sus.Game.Overlays;
using sus.Framework.Allocation;
using sus.Game.Online.API.Requests.Responses;
using sus.Game.Overlays.Profile;

namespace sus.Game.Tests.Visual.Online
{
    public partial class TestSceneProfileRulesetSelector : OsuTestScene
    {
        [Cached]
        private readonly OverlayColourProvider colourProvider = new OverlayColourProvider(OverlayColourScheme.Pink);

        public TestSceneProfileRulesetSelector()
        {
            var user = new Bindable<UserProfileData?>();

            Child = new ProfileRulesetSelector
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                User = { BindTarget = user }
            };
            AddStep("User on sus ruleset", () => user.Value = new UserProfileData(new APIUser { Id = 0, PlayMode = "sus" }, new OsuRuleset().RulesetInfo));
            AddStep("User on taiko ruleset", () => user.Value = new UserProfileData(new APIUser { Id = 1, PlayMode = "sus" }, new TaikoRuleset().RulesetInfo));
            AddStep("User on catch ruleset", () => user.Value = new UserProfileData(new APIUser { Id = 2, PlayMode = "sus" }, new CatchRuleset().RulesetInfo));
            AddStep("User on mania ruleset", () => user.Value = new UserProfileData(new APIUser { Id = 3, PlayMode = "sus" }, new ManiaRuleset().RulesetInfo));

            AddStep("User with sus as default", () => user.Value = new UserProfileData(new APIUser { Id = 0, PlayMode = "sus" }, new OsuRuleset().RulesetInfo));
            AddStep("User with taiko as default", () => user.Value = new UserProfileData(new APIUser { Id = 1, PlayMode = "taiko" }, new OsuRuleset().RulesetInfo));
            AddStep("User with catch as default", () => user.Value = new UserProfileData(new APIUser { Id = 2, PlayMode = "fruits" }, new OsuRuleset().RulesetInfo));
            AddStep("User with mania as default", () => user.Value = new UserProfileData(new APIUser { Id = 3, PlayMode = "mania" }, new OsuRuleset().RulesetInfo));

            AddStep("null user", () => user.Value = null);
        }
    }
}
