// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using sus.Framework.Graphics.Sprites;
using sus.Game.Overlays.Dialog;
using sus.Game.Localisation;

namespace sus.Game.Screens.Select.Carousel
{
    public partial class UpdateLocalConfirmationDialog : DangerousActionDialog
    {
        public UpdateLocalConfirmationDialog(Action onConfirm)
        {
            HeaderText = PopupDialogStrings.UpdateLocallyModifiedText;
            BodyText = PopupDialogStrings.UpdateLocallyModifiedDescription;
            Icon = FontAwesome.Solid.ExclamationTriangle;
            DangerousAction = onConfirm;
        }
    }
}
