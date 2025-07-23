// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Allocation;
using sus.Framework.Bindables;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Game.Graphics;
using sus.Game.Graphics.Sprites;

namespace sus.Game.Screens.Ranking.Contracted
{
    public partial class ContractedPanelTopContent : CompositeDrawable
    {
        public readonly Bindable<int?> ScorePosition = new Bindable<int?>();

        private OsuSpriteText text = null!;

        public ContractedPanelTopContent()
        {
            RelativeSizeAxes = Axes.Both;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            InternalChild = text = new OsuSpriteText
            {
                Anchor = Anchor.TopCentre,
                Origin = Anchor.TopCentre,
                Y = 6,
                Font = OsuFont.GetFont(size: 18, weight: FontWeight.Bold)
            };
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            ScorePosition.BindValueChanged(pos => text.Text = pos.NewValue != null ? $"#{pos.NewValue}" : string.Empty, true);
        }
    }
}
