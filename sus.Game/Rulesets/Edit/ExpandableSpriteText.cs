// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Allocation;
using sus.Framework.Bindables;
using sus.Framework.Graphics;
using sus.Game.Graphics.Containers;
using sus.Game.Graphics.Sprites;

namespace sus.Game.Rulesets.Edit
{
    internal partial class ExpandableSpriteText : OsuSpriteText, IExpandable
    {
        public BindableBool Expanded { get; } = new BindableBool();

        [Resolved(canBeNull: true)]
        private IExpandingContainer? expandingContainer { get; set; }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            expandingContainer?.Expanded.BindValueChanged(containerExpanded =>
            {
                Expanded.Value = containerExpanded.NewValue;
            }, true);

            Expanded.BindValueChanged(expanded =>
            {
                this.FadeTo(expanded.NewValue ? 1 : 0, 150, Easing.OutQuint);
            }, true);
        }
    }
}
