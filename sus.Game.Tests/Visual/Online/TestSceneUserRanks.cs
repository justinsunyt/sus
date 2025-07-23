// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using NUnit.Framework;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using sus.Game.Graphics;
using sus.Game.Graphics.Containers;
using sus.Game.Online.API.Requests.Responses;
using sus.Game.Overlays;
using sus.Game.Overlays.Profile;
using sus.Game.Overlays.Profile.Sections;
using sus.Game.Rulesets.Osu;

namespace sus.Game.Tests.Visual.Online
{
    [TestFixture]
    public partial class TestSceneUserRanks : OsuTestScene
    {
        protected override bool UseOnlineAPI => true;

        [Cached]
        private readonly OverlayColourProvider colourProvider = new OverlayColourProvider(OverlayColourScheme.Green);

        public TestSceneUserRanks()
        {
            RanksSection ranks;

            Add(new Container
            {
                RelativeSizeAxes = Axes.Both,
                Children = new Drawable[]
                {
                    new Box
                    {
                        RelativeSizeAxes = Axes.Both,
                        Colour = OsuColour.Gray(0.2f)
                    },
                    new OsuScrollContainer
                    {
                        RelativeSizeAxes = Axes.Both,
                        Child = ranks = new RanksSection(),
                    },
                }
            });

            AddStep("Show cookiezi", () => ranks.User.Value = new UserProfileData(new APIUser { Id = 124493 }, new OsuRuleset().RulesetInfo));
        }
    }
}
