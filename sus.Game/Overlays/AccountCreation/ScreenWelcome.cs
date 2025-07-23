// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Allocation;
using sus.Framework.Extensions.LocalisationExtensions;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Framework.Screens;
using sus.Game.Graphics;
using sus.Game.Graphics.Sprites;
using sus.Game.Overlays.Settings;
using sus.Game.Screens.Menu;
using susTK;
using sus.Game.Localisation;

namespace sus.Game.Overlays.AccountCreation
{
    public partial class ScreenWelcome : AccountCreationScreen
    {
        [BackgroundDependencyLoader]
        private void load()
        {
            InternalChild = new FillFlowContainer
            {
                RelativeSizeAxes = Axes.Both,
                Direction = FillDirection.Vertical,
                Anchor = Anchor.TopCentre,
                Origin = Anchor.TopCentre,
                Padding = new MarginPadding(20),
                Spacing = new Vector2(0, 5),
                Children = new Drawable[]
                {
                    new Container
                    {
                        RelativeSizeAxes = Axes.X,
                        Height = 150,
                        Child = new OsuLogo
                        {
                            Scale = new Vector2(0.1f),
                            Anchor = Anchor.Centre,
                            Triangles = false,
                        },
                    },
                    new OsuSpriteText
                    {
                        Anchor = Anchor.TopCentre,
                        Origin = Anchor.TopCentre,
                        Font = OsuFont.GetFont(size: 24, weight: FontWeight.Light),
                        Text = AccountCreationStrings.NewPlayerRegistration.ToTitle(),
                    },
                    new OsuSpriteText
                    {
                        Anchor = Anchor.TopCentre,
                        Origin = Anchor.TopCentre,
                        Font = OsuFont.GetFont(size: 12),
                        Text = AccountCreationStrings.LetsGetYouStarted.ToLower(),
                    },
                    new SettingsButton
                    {
                        Text = AccountCreationStrings.LetsCreateAnAccount,
                        Margin = new MarginPadding { Vertical = 120 },
                        Action = () => this.Push(new ScreenWarning())
                    }
                }
            };
        }
    }
}
