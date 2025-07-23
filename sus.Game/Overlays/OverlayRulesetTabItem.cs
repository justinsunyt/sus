// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Framework.Graphics.UserInterface;
using sus.Framework.Input.Events;
using sus.Game.Graphics.UserInterface;
using sus.Game.Rulesets;
using susTK.Graphics;
using susTK;
using sus.Framework.Allocation;
using sus.Framework.Audio;
using sus.Framework.Audio.Sample;
using sus.Framework.Graphics.Cursor;
using sus.Framework.Localisation;
using sus.Game.Graphics.Containers;

namespace sus.Game.Overlays
{
    public partial class OverlayRulesetTabItem : TabItem<RulesetInfo>, IHasTooltip
    {
        private Color4 accentColour;

        protected virtual Color4 AccentColour
        {
            get => accentColour;
            set
            {
                accentColour = value;
                icon.FadeColour(value, 120, Easing.OutQuint);
            }
        }

        protected override Container<Drawable> Content { get; }

        [Resolved]
        private OverlayColourProvider colourProvider { get; set; } = null!;

        private readonly Drawable icon;

        public LocalisableString TooltipText => Value.Name;

        private Sample selectSample = null!;

        public OverlayRulesetTabItem(RulesetInfo value)
            : base(value)
        {
            AutoSizeAxes = Axes.Both;

            AddRangeInternal(new Drawable[]
            {
                Content = new FillFlowContainer
                {
                    AutoSizeAxes = Axes.Both,
                    Direction = FillDirection.Horizontal,
                    Spacing = new Vector2(4, 0),
                    Child = icon = new ConstrainedIconContainer
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Size = new Vector2(20f),
                        Icon = value.CreateInstance().CreateIcon(),
                    },
                },
                new HoverSounds(HoverSampleSet.TabSelect)
            });

            Enabled.Value = true;
        }

        [BackgroundDependencyLoader]
        private void load(AudioManager audio)
        {
            selectSample = audio.Samples.Get(@"UI/tabselect-select");
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            Enabled.BindValueChanged(_ => updateState(), true);
        }

        public override bool PropagatePositionalInputSubTree => Enabled.Value && base.PropagatePositionalInputSubTree;

        protected override bool OnHover(HoverEvent e)
        {
            base.OnHover(e);
            updateState();
            return true;
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            base.OnHoverLost(e);
            updateState();
        }

        protected override void OnActivated() => updateState();

        protected override void OnDeactivated() => updateState();

        protected override void OnActivatedByUser() => selectSample.Play();

        private void updateState()
        {
            AccentColour = Enabled.Value ? getActiveColour() : colourProvider.Foreground1;
        }

        private Color4 getActiveColour() => IsHovered || Active.Value ? Color4.White : colourProvider.Highlight1;
    }
}
