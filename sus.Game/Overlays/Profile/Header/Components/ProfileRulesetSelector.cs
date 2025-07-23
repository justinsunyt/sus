// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Linq;
using sus.Framework.Allocation;
using sus.Framework.Bindables;
using sus.Framework.Extensions.ObjectExtensions;
using sus.Framework.Graphics.UserInterface;
using sus.Game.Extensions;
using sus.Game.Rulesets;

namespace sus.Game.Overlays.Profile.Header.Components
{
    public partial class ProfileRulesetSelector : OverlayRulesetSelector
    {
        [Resolved]
        private UserProfileOverlay? profileOverlay { get; set; }

        public readonly Bindable<UserProfileData?> User = new Bindable<UserProfileData?>();

        protected override void LoadComplete()
        {
            base.LoadComplete();

            User.BindValueChanged(user => updateState(user.NewValue), true);
            Current.BindValueChanged(ruleset =>
            {
                if (User.Value != null && !ruleset.NewValue.Equals(User.Value.Ruleset))
                    profileOverlay?.ShowUser(User.Value.User, ruleset.NewValue);
            });
        }

        private void updateState(UserProfileData? user)
        {
            Current.Value = Items.SingleOrDefault(ruleset => user?.Ruleset.MatchesOnlineID(ruleset) == true);
            SetDefaultRuleset(Rulesets.GetRuleset(user?.User.PlayMode ?? @"sus").AsNonNull());
        }

        public void SetDefaultRuleset(RulesetInfo ruleset)
        {
            foreach (var tabItem in TabContainer)
                ((ProfileRulesetTabItem)tabItem).IsDefault = ((ProfileRulesetTabItem)tabItem).Value.Equals(ruleset);
        }

        protected override TabItem<RulesetInfo> CreateTabItem(RulesetInfo value) => new ProfileRulesetTabItem(value);
    }
}
