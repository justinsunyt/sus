// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using sus.Framework.Graphics.Sprites;
using sus.Game.Localisation;
using sus.Game.Overlays.Dialog;

namespace sus.Game.Screens.Edit
{
    public partial class DiscardUnsavedChangesDialog : PopupDialog
    {
        public DiscardUnsavedChangesDialog(Action exit)
        {
            HeaderText = EditorDialogsStrings.DiscardUnsavedChangesDialogHeader;

            Icon = FontAwesome.Solid.Trash;

            Buttons = new PopupDialogButton[]
            {
                new PopupDialogDangerousButton
                {
                    Text = EditorDialogsStrings.ForgetAllChanges,
                    Action = exit
                },
                new PopupDialogCancelButton
                {
                    Text = EditorDialogsStrings.ContinueEditing,
                },
            };
        }
    }
}
