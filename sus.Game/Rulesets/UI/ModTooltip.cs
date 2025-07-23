// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Linq;
using sus.Framework.Allocation;
using sus.Framework.Extensions.Color4Extensions;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Framework.Graphics.Cursor;
using sus.Framework.Graphics.Effects;
using sus.Framework.Graphics.Shapes;
using sus.Framework.Localisation;
using sus.Game.Graphics;
using sus.Game.Graphics.Sprites;
using sus.Game.Overlays;
using sus.Game.Rulesets.Mods;
using susTK;
using susTK.Graphics;

namespace sus.Game.Rulesets.UI
{
    public partial class ModTooltip : VisibilityContainer, ITooltip<Mod>
    {
        private readonly OverlayColourProvider colourProvider;

        private OsuSpriteText nameText = null!;
        private TextFlowContainer settingsLabelsFlow = null!;
        private TextFlowContainer settingsValuesFlow = null!;

        public ModTooltip(OverlayColourProvider? colourProvider = null)
        {
            this.colourProvider = colourProvider ?? new OverlayColourProvider(OverlayColourScheme.Aquamarine);
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            AutoSizeAxes = Axes.Both;
            CornerRadius = 7;
            Masking = true;

            EdgeEffect = new EdgeEffectParameters
            {
                Type = EdgeEffectType.Shadow,
                Colour = Color4.Black.Opacity(0.2f),
                Radius = 10f,
            };

            InternalChildren = new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = colourProvider.Background6,
                },
                new FillFlowContainer
                {
                    AutoSizeAxes = Axes.Both,
                    Direction = FillDirection.Horizontal,
                    Padding = new MarginPadding(10f),
                    Spacing = new Vector2(20f, 0f),
                    Children = new Drawable[]
                    {
                        new FillFlowContainer
                        {
                            Anchor = Anchor.BottomLeft,
                            Origin = Anchor.BottomLeft,
                            AutoSizeAxes = Axes.Both,
                            Direction = FillDirection.Vertical,
                            Spacing = new Vector2(0f, 5f),
                            Children = new Drawable[]
                            {
                                nameText = new OsuSpriteText
                                {
                                    Font = OsuFont.Torus.With(size: 16f, weight: FontWeight.SemiBold),
                                    Colour = colourProvider.Content1,
                                    UseFullGlyphHeight = false,
                                },
                                settingsLabelsFlow = new TextFlowContainer(t =>
                                {
                                    t.Font = OsuFont.Torus.With(size: 12f, weight: FontWeight.SemiBold);
                                })
                                {
                                    AutoSizeAxes = Axes.Both,
                                    Colour = colourProvider.Content2,
                                },
                            },
                        },
                        settingsValuesFlow = new TextFlowContainer(t =>
                        {
                            t.Font = OsuFont.Torus.With(size: 12f, weight: FontWeight.SemiBold);
                        })
                        {
                            Anchor = Anchor.BottomLeft,
                            Origin = Anchor.BottomLeft,
                            AutoSizeAxes = Axes.Both,
                            Colour = colourProvider.Content1,
                            TextAnchor = Anchor.TopRight,
                        },
                    },
                }
            };
        }

        private (LocalisableString setting, LocalisableString value)[]? displayedSettings;

        public void SetContent(Mod content)
        {
            nameText.Text = content.Name;

            if (displayedSettings == null || !displayedSettings.SequenceEqual(content.SettingDescription))
            {
                displayedSettings = content.SettingDescription.ToArray();

                settingsLabelsFlow.Clear();
                settingsValuesFlow.Clear();

                if (displayedSettings.Any())
                {
                    settingsLabelsFlow.Show();
                    settingsValuesFlow.Show();

                    foreach (var part in displayedSettings)
                    {
                        settingsLabelsFlow.AddText(part.setting);
                        settingsLabelsFlow.NewLine();

                        settingsValuesFlow.AddText(part.value);
                        settingsValuesFlow.NewLine();
                    }
                }
                else
                {
                    settingsLabelsFlow.Hide();
                    settingsValuesFlow.Hide();
                }
            }
        }

        protected override void PopIn() => this.FadeIn(300, Easing.OutQuint);
        protected override void PopOut() => this.FadeOut(300, Easing.OutQuint);
        public void Move(Vector2 pos) => Position = pos;
    }
}
