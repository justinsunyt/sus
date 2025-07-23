// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Allocation;
using sus.Framework.Bindables;
using sus.Framework.Graphics.Sprites;
using sus.Framework.Localisation;
using sus.Game.Resources.Localisation.Web;

namespace sus.Game.Overlays.Profile.Header.Components
{
    public partial class MappingSubscribersButton : ProfileHeaderStatisticsButton
    {
        public readonly Bindable<UserProfileData?> User = new Bindable<UserProfileData?>();

        public override LocalisableString TooltipText => FollowsStrings.MappingFollowers;

        protected override IconUsage Icon => FontAwesome.Solid.Bell;

        [BackgroundDependencyLoader]
        private void load()
        {
            User.BindValueChanged(user => SetValue(user.NewValue?.User.MappingFollowerCount ?? 0), true);
        }
    }
}
