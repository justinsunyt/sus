// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Allocation;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Framework.Graphics.Sprites;
using sus.Game.Graphics;
using sus.Game.Graphics.Containers;

namespace sus.Game.Screens.OnlinePlay.Lounge.Components
{
    public abstract partial class OnlinePlayPill : CompositeDrawable
    {
        protected PillContainer Pill { get; private set; } = null!;
        protected OsuTextFlowContainer TextFlow { get; private set; } = null!;
        protected virtual FontUsage Font => OsuFont.GetFont(size: 12);

        protected OnlinePlayPill()
        {
            AutoSizeAxes = Axes.Both;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            InternalChild = Pill = new PillContainer
            {
                Child = TextFlow = new OsuTextFlowContainer(s => s.Font = Font)
                {
                    AutoSizeAxes = Axes.Both,
                    Anchor = Anchor.CentreLeft,
                    Origin = Anchor.CentreLeft
                }
            };
        }
    }
}
