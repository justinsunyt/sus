// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Game.Database;
using sus.Game.Screens.OnlinePlay;

namespace sus.Game.Tests.Visual.OnlinePlay
{
    /// <summary>
    /// Interface that defines the dependencies required for online play test scenes.
    /// </summary>
    public interface IOnlinePlayTestSceneDependencies
    {
        /// <summary>
        /// The cached <see cref="OngoingOperationTracker"/>.
        /// </summary>
        OngoingOperationTracker OngoingOperationTracker { get; }

        /// <summary>
        /// The cached <see cref="UserLookupCache"/>.
        /// </summary>
        TestUserLookupCache UserLookupCache { get; }

        /// <summary>
        /// The cached <see cref="BeatmapLookupCache"/>.
        /// </summary>
        BeatmapLookupCache BeatmapLookupCache { get; }
    }
}
