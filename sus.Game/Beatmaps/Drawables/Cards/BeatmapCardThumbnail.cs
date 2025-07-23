// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Allocation;
using sus.Framework.Bindables;
using sus.Framework.Extensions.Color4Extensions;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Framework.Graphics.Shapes;
using sus.Game.Beatmaps.Drawables.Cards.Buttons;
using sus.Game.Overlays;
using sus.Framework.Graphics.UserInterface;
using susTK;

namespace sus.Game.Beatmaps.Drawables.Cards
{
    public partial class BeatmapCardThumbnail : Container
    {
        public BindableBool Dimmed { get; } = new BindableBool();

        public new MarginPadding Padding
        {
            get => foreground.Padding;
            set => foreground.Padding = value;
        }

        private readonly Box background;
        private readonly Container foreground;
        private readonly PlayButton playButton;
        private readonly CircularProgress progress;
        private readonly Container content;

        protected override Container<Drawable> Content => content;

        [Resolved]
        private OverlayColourProvider colourProvider { get; set; } = null!;

        public BeatmapCardThumbnail(IBeatmapSetInfo beatmapSetInfo, IBeatmapSetOnlineInfo onlineInfo)
        {
            InternalChildren = new Drawable[]
            {
                new UpdateableOnlineBeatmapSetCover(BeatmapSetCoverType.List)
                {
                    RelativeSizeAxes = Axes.Both,
                    OnlineInfo = onlineInfo
                },
                background = new Box
                {
                    RelativeSizeAxes = Axes.Both
                },
                foreground = new Container
                {
                    RelativeSizeAxes = Axes.Both,
                    Children = new Drawable[]
                    {
                        playButton = new PlayButton(beatmapSetInfo)
                        {
                            RelativeSizeAxes = Axes.Both
                        },
                        progress = new CircularProgress
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            InnerRadius = 0.2f
                        },
                        content = new Container
                        {
                            RelativeSizeAxes = Axes.Both
                        }
                    }
                }
            };
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            progress.Colour = colourProvider.Highlight1;
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            Dimmed.BindValueChanged(_ => updateState());

            playButton.Playing.BindValueChanged(_ => updateState(), true);
            FinishTransforms(true);
        }

        protected override void Update()
        {
            base.Update();

            progress.Progress = playButton.Progress.Value;
            progress.Size = new Vector2(50 * playButton.DrawWidth / (BeatmapCardNormal.HEIGHT - BeatmapCard.CORNER_RADIUS));
        }

        private void updateState()
        {
            bool shouldDim = Dimmed.Value || playButton.Playing.Value;

            playButton.FadeTo(shouldDim ? 1 : 0, BeatmapCard.TRANSITION_DURATION, Easing.OutQuint);
            background.FadeColour(colourProvider.Background6.Opacity(shouldDim ? 0.6f : 0f), BeatmapCard.TRANSITION_DURATION, Easing.OutQuint);
        }
    }
}
