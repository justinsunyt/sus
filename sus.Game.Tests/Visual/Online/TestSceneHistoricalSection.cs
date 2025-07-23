// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using NUnit.Framework;
using sus.Framework.Allocation;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Shapes;
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
    public partial class TestSceneHistoricalSection : OsuTestScene
    {
        protected override bool UseOnlineAPI => true;

        [Cached]
        private readonly OverlayColourProvider colourProvider = new OverlayColourProvider(OverlayColourScheme.Pink);

        public TestSceneHistoricalSection()
        {
            HistoricalSection section;

            Add(new Box
            {
                RelativeSizeAxes = Axes.Both,
                Colour = OsuColour.Gray(0.2f)
            });

            Add(new OsuScrollContainer
            {
                RelativeSizeAxes = Axes.Both,
                Child = section = new HistoricalSection(),
            });

            AddStep("Show peppy", () => section.User.Value = new UserProfileData(new APIUser { Id = 2 }, new OsuRuleset().RulesetInfo));
            AddStep("Show WubWoofWolf", () => section.User.Value = new UserProfileData(new APIUser { Id = 39828 }, new OsuRuleset().RulesetInfo));
        }
    }
}
