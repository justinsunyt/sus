// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Allocation;
using sus.Framework.Graphics;
using sus.Game.Configuration;
using sus.Game.Graphics.UserInterface;

namespace sus.Game.Screens.Play.PlayerSettings
{
    public partial class DiscussionSettings : PlayerSettingsGroup
    {
        public DiscussionSettings()
            : base("discussions")
        {
        }

        [BackgroundDependencyLoader]
        private void load(OsuConfigManager config)
        {
            Children = new Drawable[]
            {
                new PlayerCheckbox
                {
                    LabelText = "Show floating comments",
                    Current = config.GetBindable<bool>(OsuSetting.FloatingComments)
                },
                new FocusedTextBox
                {
                    RelativeSizeAxes = Axes.X,
                    Height = 30,
                    PlaceholderText = "Add Comment",
                    HoldFocus = false,
                },
            };
        }
    }
}
