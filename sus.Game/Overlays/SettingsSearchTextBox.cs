// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

#nullable disable
using sus.Framework.Input.Events;
using sus.Game.Graphics.UserInterface;

namespace sus.Game.Overlays
{
    public partial class SettingsSearchTextBox : SeekLimitedSearchTextBox
    {
        protected override void OnFocus(FocusEvent e)
        {
            base.OnFocus(e);

            // on mobile platforms, focus is not held by the search text box, and the select all feature
            // will not make sense on it, and might annoy the user when they try to focus manually.
            if (HoldFocus)
                SelectAll();
        }
    }
}
