// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Allocation;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Framework.Input.Events;
using sus.Game.Input.Bindings;

namespace sus.Game.Overlays.Volume
{
    /// <summary>
    /// Add to a container or screen to make scrolling anywhere in the container cause the global game volume to be adjusted.
    /// </summary>
    /// <remarks>
    /// This is generally expected behaviour in many locations in sus!stable.
    /// </remarks>
    public partial class GlobalScrollAdjustsVolume : Container
    {
        [Resolved]
        private VolumeOverlay? volumeOverlay { get; set; }

        public GlobalScrollAdjustsVolume()
        {
            RelativeSizeAxes = Axes.Both;
        }

        protected override bool OnScroll(ScrollEvent e)
        {
            if (e.ScrollDelta.Y == 0)
                return false;

            // forward any unhandled mouse scroll events to the volume control.
            return volumeOverlay?.Adjust(GlobalAction.IncreaseVolume, e.ScrollDelta.Y, e.IsPrecise) ?? false;
        }
    }
}
