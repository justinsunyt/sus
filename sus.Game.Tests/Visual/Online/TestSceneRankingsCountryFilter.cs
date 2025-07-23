// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Bindables;
using sus.Framework.Graphics.Containers;
using sus.Game.Overlays.Rankings;
using sus.Game.Users;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Shapes;
using susTK.Graphics;
using sus.Game.Graphics.Sprites;
using sus.Game.Overlays;
using sus.Framework.Allocation;

namespace sus.Game.Tests.Visual.Online
{
    public partial class TestSceneRankingsCountryFilter : OsuTestScene
    {
        [Cached]
        private readonly OverlayColourProvider colourProvider = new OverlayColourProvider(OverlayColourScheme.Green);

        public TestSceneRankingsCountryFilter()
        {
            var countryBindable = new Bindable<CountryCode>();

            AddRange(new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = Color4.Gray,
                },
                new FillFlowContainer
                {
                    RelativeSizeAxes = Axes.X,
                    AutoSizeAxes = Axes.Y,
                    Direction = FillDirection.Vertical,
                    Children = new Drawable[]
                    {
                        new CountryFilter
                        {
                            Anchor = Anchor.TopCentre,
                            Origin = Anchor.TopCentre,
                            Current = { BindTarget = countryBindable }
                        },
                        new OsuSpriteText
                        {
                            Anchor = Anchor.TopCentre,
                            Origin = Anchor.TopCentre,
                            Text = "Some content",
                            Margin = new MarginPadding { Vertical = 20 }
                        }
                    }
                }
            });

            const CountryCode country = CountryCode.BY;
            const CountryCode unknown_country = CountryCode.CK;

            AddStep("Set country", () => countryBindable.Value = country);
            AddStep("Set default country", () => countryBindable.Value = default);
            AddStep("Set country with no flag", () => countryBindable.Value = unknown_country);
        }
    }
}
