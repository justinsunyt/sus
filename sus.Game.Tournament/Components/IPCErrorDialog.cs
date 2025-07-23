// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Graphics.Sprites;
using sus.Game.Overlays.Dialog;

namespace sus.Game.Tournament.Components
{
    public partial class IPCErrorDialog : PopupDialog
    {
        public IPCErrorDialog(string headerText, string bodyText)
        {
            Icon = FontAwesome.Regular.SadTear;
            HeaderText = headerText;
            BodyText = bodyText;
            Buttons = new PopupDialogButton[]
            {
                new PopupDialogOkButton
                {
                    Text = @"Alright.",
                    Action = () => Expire()
                }
            };
        }
    }
}
