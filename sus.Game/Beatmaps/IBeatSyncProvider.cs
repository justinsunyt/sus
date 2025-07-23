// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Allocation;
using sus.Framework.Audio;
using sus.Framework.Timing;
using sus.Game.Beatmaps.ControlPoints;
using sus.Game.Graphics.Containers;

namespace sus.Game.Beatmaps
{
    /// <summary>
    /// Provides various data sources which allow for synchronising visuals to a known beat.
    /// Primarily intended for use with <see cref="BeatSyncedContainer"/>.
    /// </summary>
    [Cached]
    public interface IBeatSyncProvider : IHasAmplitudes
    {
        /// <summary>
        /// Access any available control points from a beatmap providing beat sync. If <c>null</c>, no current provider is available.
        /// </summary>
        ControlPointInfo? ControlPoints { get; }

        /// <summary>
        /// Access a clock currently responsible for providing beat sync.
        /// </summary>
        IClock Clock { get; }
    }
}
