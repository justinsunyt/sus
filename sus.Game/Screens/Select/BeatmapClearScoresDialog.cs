// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Threading.Tasks;
using sus.Framework.Allocation;
using sus.Game.Beatmaps;
using sus.Game.Overlays.Dialog;
using sus.Game.Scoring;

namespace sus.Game.Screens.Select
{
    public partial class BeatmapClearScoresDialog : DangerousActionDialog
    {
        [Resolved]
        private ScoreManager scoreManager { get; set; } = null!;

        public BeatmapClearScoresDialog(BeatmapInfo beatmapInfo, Action? onCompletion = null)
        {
            BodyText = $"All local scores on {beatmapInfo.GetDisplayTitle()}";
            DangerousAction = () =>
            {
                Task.Run(() => scoreManager.Delete(beatmapInfo))
                    .ContinueWith(_ => onCompletion?.Invoke());
            };
        }
    }
}
