// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Bindables;
using sus.Framework.Graphics;
using sus.Game.Graphics;
using sus.Game.Localisation;
using sus.Game.Resources.Localisation.Web;
using sus.Game.Rulesets;
using sus.Game.Users;

namespace sus.Game.Overlays.Rankings
{
    public partial class RankingsOverlayHeader : TabControlOverlayHeader<RankingsScope>
    {
        public Bindable<RulesetInfo> Ruleset => rulesetSelector.Current;

        public Bindable<CountryCode> Country => countryFilter.Current;

        private OverlayRulesetSelector rulesetSelector = null!;
        private CountryFilter countryFilter = null!;

        protected override OverlayTitle CreateTitle() => new RankingsTitle();

        protected override Drawable CreateTabControlContent() => rulesetSelector = new OverlayRulesetSelector();

        protected override Drawable CreateContent() => countryFilter = new CountryFilter();

        protected override Drawable CreateBackground() => new OverlayHeaderBackground("Headers/rankings");

        private partial class RankingsTitle : OverlayTitle
        {
            public RankingsTitle()
            {
                Title = PageTitleStrings.MainRankingControllerDefault;
                Description = NamedOverlayComponentStrings.RankingsDescription;
                Icon = OsuIcon.Ranking;
            }
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            Current.BindValueChanged(scope =>
            {
                rulesetSelector.FadeTo(showRulesetSelector(scope.NewValue) ? 1 : 0, 200, Easing.OutQuint);
            }, true);

            bool showRulesetSelector(RankingsScope scope)
            {
                switch (scope)
                {
                    case RankingsScope.Performance:
                    case RankingsScope.Score:
                    case RankingsScope.Country:
                    case RankingsScope.Spotlights:
                        return true;

                    default:
                        return false;
                }
            }
        }
    }
}
