// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Allocation;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Framework.Input.Events;
using sus.Game.Online.API.Requests.Responses;
using susTK;

namespace sus.Game.Beatmaps.Drawables.Cards
{
    public partial class BeatmapCardExtraInfoRow : CompositeDrawable
    {
        [Resolved(CanBeNull = true)]
        private BeatmapCardContent? content { get; set; }

        public BeatmapCardExtraInfoRow(APIBeatmapSet beatmapSet)
        {
            RelativeSizeAxes = Axes.X;
            AutoSizeAxes = Axes.Y;

            InternalChild = new FillFlowContainer
            {
                RelativeSizeAxes = Axes.X,
                AutoSizeAxes = Axes.Y,
                Direction = FillDirection.Horizontal,
                Spacing = new Vector2(3, 0),
                Children = new Drawable[]
                {
                    new BeatmapSetOnlineStatusPill
                    {
                        Status = beatmapSet.Status,
                        Anchor = Anchor.CentreLeft,
                        Origin = Anchor.CentreLeft,
                        TextSize = 13f
                    },
                    new DifficultySpectrumDisplay
                    {
                        BeatmapSet = beatmapSet,
                        Anchor = Anchor.CentreLeft,
                        Origin = Anchor.CentreLeft,
                    }
                }
            };
        }

        protected override bool OnHover(HoverEvent e)
        {
            content?.ExpandAfterDelay();
            return base.OnHover(e);
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            if (content?.Expanded.Value == false)
                content.CancelExpand();

            base.OnHoverLost(e);
        }
    }
}
