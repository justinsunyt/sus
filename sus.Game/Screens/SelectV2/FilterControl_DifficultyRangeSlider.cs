// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Linq;
using sus.Framework.Allocation;
using sus.Framework.Extensions.Color4Extensions;
using sus.Framework.Extensions.LocalisationExtensions;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Colour;
using sus.Framework.Graphics.Containers;
using sus.Framework.Graphics.Shapes;
using sus.Framework.Layout;
using sus.Framework.Localisation;
using sus.Game.Graphics;
using sus.Game.Graphics.UserInterface;
using sus.Game.Localisation;
using sus.Game.Overlays;
using sus.Game.Resources.Localisation.Web;
using sus.Game.Utils;
using susTK.Graphics;

namespace sus.Game.Screens.SelectV2
{
    public partial class FilterControl
    {
        public partial class DifficultyRangeSlider : ShearedRangeSlider
        {
            private Container borderContainer = null!;

            private readonly LayoutValue drawSizeLayout = new LayoutValue(Invalidation.DrawSize);

            private static readonly (float, Color4)[] spectrum = OsuColour.STAR_DIFFICULTY_SPECTRUM
                                                                          .Skip(1)
                                                                          .Prepend((0.0f, OsuColour.STAR_DIFFICULTY_SPECTRUM.ElementAt(1).Item2)).ToArray();

            public DifficultyRangeSlider()
                : base(BeatmapsetsStrings.ShowStatsStars)
            {
                NubWidth = ShearedNub.HEIGHT * 1.16f;
                DefaultStringUpperBound = "∞";

                AddLayout(drawSizeLayout);
            }

            [BackgroundDependencyLoader]
            private void load(OverlayColourProvider colourProvider, OsuColour colours)
            {
                SliderContainer.AddRange(new Drawable[]
                {
                    new Container
                    {
                        Depth = 1,
                        RelativeSizeAxes = Axes.Both,
                        Shear = OsuGame.SHEAR,
                        CornerRadius = 5f,
                        Masking = true,
                        ChildrenEnumerable = spectrum.Zip(spectrum.Skip(1))
                                                     .Select(p => new Box
                                                     {
                                                         RelativePositionAxes = Axes.X,
                                                         X = p.First.Item1 / 10f,
                                                         RelativeSizeAxes = Axes.Both,
                                                         Width = (p.Second.Item1 - p.First.Item1) / 10f,
                                                         Colour = ColourInfo.GradientHorizontal(p.First.Item2, p.Second.Item2),
                                                     }),
                    },
                    borderContainer = new Container
                    {
                        Depth = -1,
                        RelativePositionAxes = Axes.X,
                        RelativeSizeAxes = Axes.Both,
                        Child = new Container
                        {
                            RelativeSizeAxes = Axes.Both,
                            BorderColour = colourProvider.Highlight1,
                            BorderThickness = 2,
                            Masking = true,
                            Shear = OsuGame.SHEAR,
                            CornerRadius = 5f,
                            Child = new Box
                            {
                                Colour = Color4.Transparent,
                                RelativeSizeAxes = Axes.Both,
                            }
                        },
                    }
                });
            }

            protected override void LoadComplete()
            {
                base.LoadComplete();

                LowerBoundSlider.Current.ValueChanged += _ => updateBorderDisplay(false);
                UpperBoundSlider.Current.ValueChanged += _ => updateBorderDisplay(false);
            }

            protected override void UpdateAfterChildren()
            {
                base.UpdateAfterChildren();

                if (!drawSizeLayout.IsValid)
                {
                    updateBorderDisplay(true);
                    drawSizeLayout.Validate();
                }
            }

            private void updateBorderDisplay(bool instant)
            {
                float borderStart = LowerBoundSlider.NormalizedValue * LowerBoundSlider.UsableWidth / LowerBoundSlider.DrawWidth;
                float borderEnd = UpperBoundSlider.NormalizedValue * UpperBoundSlider.UsableWidth / UpperBoundSlider.DrawWidth;
                borderEnd += UpperBoundSlider.NubWidth / UpperBoundSlider.DrawWidth;

                borderContainer.MoveToX(borderStart, instant ? 0 : 250, Easing.OutQuint);
                borderContainer.ResizeWidthTo(borderEnd - borderStart, instant ? 0 : 250, Easing.OutQuint);
            }

            protected override BoundSliderBar CreateBoundSlider(bool isUpper) => new DifficultyBoundSliderBar(this, isUpper);

            private partial class DifficultyBoundSliderBar : BoundSliderBar
            {
                private readonly bool isUpper;

                protected override bool FocusIndicator => false;

                public override LocalisableString TooltipText
                {
                    get
                    {
                        if (Current.IsDefault && isUpper)
                            return UserInterfaceStrings.NoLimit;

                        return SongSelectStrings.Stars(Current.Value.ToLocalisableString(@"0.##"));
                    }
                }

                public DifficultyBoundSliderBar(ShearedRangeSlider slider, bool isUpper)
                    : base(slider, isUpper)
                {
                    this.isUpper = isUpper;
                }

                [Resolved]
                private OsuColour colours { get; set; } = null!;

                protected override void LoadComplete()
                {
                    base.LoadComplete();

                    if (isUpper)
                    {
                        LeftBox.Colour = OsuColour.Gray(0.4f).Opacity(0.2f);
                        RightBox.Colour = OsuColour.Gray(0.05f).Opacity(0.7f);
                    }
                    else
                    {
                        LeftBox.Colour = OsuColour.Gray(0.05f).Opacity(0.7f);
                        RightBox.Colour = OsuColour.Gray(0.4f).Opacity(0.2f);
                    }
                }

                protected override void UpdateDisplay(double value)
                {
                    Colour4 nubColour = ColourUtils.SampleFromLinearGradient(spectrum, (float)Math.Round(value, 2, MidpointRounding.AwayFromZero));
                    nubColour = nubColour.Lighten(0.4f);

                    if (value >= 8.0)
                        nubColour = colours.Gray4;

                    Nub.AccentColour = nubColour;
                    Nub.GlowingAccentColour = nubColour.Lighten(0.2f);
                    Nub.ShadowColour = Color4.Black.Opacity(0.2f);
                    NubText.Colour = OsuColour.ForegroundTextColourFor(nubColour);

                    base.UpdateDisplay(value);
                }
            }
        }
    }
}
