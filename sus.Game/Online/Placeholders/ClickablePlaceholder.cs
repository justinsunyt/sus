// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

#nullable disable

using System;
using sus.Framework.Graphics.Sprites;
using sus.Framework.Localisation;
using sus.Game.Graphics.Containers;
using sus.Game.Graphics.UserInterface;

namespace sus.Game.Online.Placeholders
{
    public partial class ClickablePlaceholder : Placeholder
    {
        public Action Action;

        public ClickablePlaceholder(LocalisableString actionMessage, IconUsage icon)
        {
            OsuAnimatedButton button;
            OsuTextFlowContainer textFlow;

            AddArbitraryDrawable(button = new OsuAnimatedButton
            {
                AutoSizeAxes = Framework.Graphics.Axes.Both,
                Action = () => Action?.Invoke()
            });

            button.Add(textFlow = new OsuTextFlowContainer(cp => cp.Font = cp.Font.With(size: TEXT_SIZE))
            {
                AutoSizeAxes = Framework.Graphics.Axes.Both,
                Margin = new Framework.Graphics.MarginPadding(5)
            });

            textFlow.AddIcon(icon, i =>
            {
                i.Padding = new Framework.Graphics.MarginPadding { Right = 10 };
            });

            textFlow.AddText(actionMessage);
        }
    }
}
