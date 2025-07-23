// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Bindables;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Framework.Input.Events;
using sus.Game.Graphics;
using sus.Game.Graphics.Containers;
using sus.Game.Graphics.Sprites;
using sus.Framework.Extensions.LocalisationExtensions;
using sus.Game.Resources.Localisation.Web;
using sus.Framework.Localisation;
using sus.Game.Online.Chat;

namespace sus.Game.Overlays.Profile.Sections.Kudsus
{
    public partial class KudsusInfo : Container
    {
        private readonly Bindable<UserProfileData?> user = new Bindable<UserProfileData?>();

        public KudsusInfo(Bindable<UserProfileData?> user)
        {
            this.user.BindTo(user);
            CountSection total;
            RelativeSizeAxes = Axes.X;
            AutoSizeAxes = Axes.Y;
            Masking = true;
            CornerRadius = 3;
            Child = total = new CountTotal();

            this.user.ValueChanged += u => total.Count = u.NewValue?.User.Kudsus.Total ?? 0;
        }

        protected override bool OnClick(ClickEvent e) => true;

        private partial class CountTotal : CountSection
        {
            public CountTotal()
                : base(UsersStrings.ShowExtraKudsusTotal)
            {
                DescriptionText.AddText("Based on how much of a contribution the user has made to beatmap moderation. See ");
                DescriptionText.AddLink("this page", LinkAction.OpenWiki, @"Modding/Kudsus");
                DescriptionText.AddText(" for more information.");
            }
        }

        private partial class CountSection : Container
        {
            private readonly OsuSpriteText valueText;
            protected readonly LinkFlowContainer DescriptionText;

            public new int Count
            {
                set => valueText.Text = value.ToLocalisableString("N0");
            }

            protected CountSection(LocalisableString header)
            {
                RelativeSizeAxes = Axes.X;
                AutoSizeAxes = Axes.Y;
                Padding = new MarginPadding { Bottom = 20 };
                Child = new FillFlowContainer
                {
                    AutoSizeAxes = Axes.Y,
                    RelativeSizeAxes = Axes.X,
                    Direction = FillDirection.Vertical,
                    Children = new Drawable[]
                    {
                        new OsuSpriteText
                        {
                            Text = header,
                            Font = OsuFont.GetFont(size: 12, weight: FontWeight.Bold)
                        },
                        valueText = new OsuSpriteText
                        {
                            Text = "0",
                            Font = OsuFont.GetFont(size: 40, weight: FontWeight.Light),
                        },
                        DescriptionText = new LinkFlowContainer(t => t.Font = t.Font.With(size: 14))
                        {
                            RelativeSizeAxes = Axes.X,
                            AutoSizeAxes = Axes.Y,
                        }
                    }
                };
            }
        }
    }
}
