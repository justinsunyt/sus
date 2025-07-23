// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Allocation;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Game.Graphics.Sprites;
using sus.Game.Overlays;
using sus.Game.Overlays.Dashboard.Friends;
using susTK;

namespace sus.Game.Tests.Visual.UserInterface
{
    public partial class TestSceneUserListToolbar : OsuTestScene
    {
        [Cached]
        private readonly OverlayColourProvider colourProvider = new OverlayColourProvider(OverlayColourScheme.Purple);

        public TestSceneUserListToolbar()
        {
            UserListToolbar toolbar;
            OsuSpriteText sort;
            OsuSpriteText displayStyle;

            Add(toolbar = new UserListToolbar
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
            });

            Add(new FillFlowContainer
            {
                AutoSizeAxes = Axes.Both,
                Direction = FillDirection.Vertical,
                Spacing = new Vector2(0, 5),
                Children = new Drawable[]
                {
                    sort = new OsuSpriteText(),
                    displayStyle = new OsuSpriteText()
                }
            });

            toolbar.SortCriteria.BindValueChanged(criteria => sort.Text = $"Criteria: {criteria.NewValue}", true);
            toolbar.DisplayStyle.BindValueChanged(style => displayStyle.Text = $"Style: {style.NewValue}", true);
        }
    }
}
