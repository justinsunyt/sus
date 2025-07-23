// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Allocation;
using sus.Framework.Graphics.Sprites;
using sus.Framework.Localisation;
using sus.Game.Overlays;

namespace sus.Game.Online.Placeholders
{
    public sealed partial class LoginPlaceholder : ClickablePlaceholder
    {
        [Resolved]
        private LoginOverlay? login { get; set; }

        public LoginPlaceholder(LocalisableString actionMessage)
            : base(actionMessage, FontAwesome.Solid.UserLock)
        {
            Action = () => login?.Show();
        }
    }
}
