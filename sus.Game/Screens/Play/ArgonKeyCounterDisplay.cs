// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Game.Screens.Play.HUD;
using susTK;

namespace sus.Game.Screens.Play
{
    public partial class ArgonKeyCounterDisplay : KeyCounterDisplay
    {
        protected override FillFlowContainer<KeyCounter> KeyFlow { get; }

        public ArgonKeyCounterDisplay()
        {
            Child = KeyFlow = new FillFlowContainer<KeyCounter>
            {
                Direction = FillDirection.Horizontal,
                AutoSizeAxes = Axes.Both,
                Spacing = new Vector2(2),
            };
        }

        protected override KeyCounter CreateCounter(InputTrigger trigger) => new ArgonKeyCounter(trigger);
    }
}
