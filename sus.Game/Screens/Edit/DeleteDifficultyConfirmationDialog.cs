// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using sus.Game.Beatmaps;
using sus.Game.Overlays.Dialog;

namespace sus.Game.Screens.Edit
{
    public partial class DeleteDifficultyConfirmationDialog : DeletionDialog
    {
        public DeleteDifficultyConfirmationDialog(BeatmapInfo beatmapInfo, Action deleteAction)
        {
            BodyText = $"\"{beatmapInfo.DifficultyName}\" difficulty";
            DangerousAction = deleteAction;
        }
    }
}
