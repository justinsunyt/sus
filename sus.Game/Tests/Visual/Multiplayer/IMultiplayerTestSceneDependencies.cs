// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Game.Tests.Visual.OnlinePlay;
using sus.Game.Tests.Visual.Spectator;

namespace sus.Game.Tests.Visual.Multiplayer
{
    /// <summary>
    /// Interface that defines the dependencies required for multiplayer test scenes.
    /// </summary>
    public interface IMultiplayerTestSceneDependencies : IOnlinePlayTestSceneDependencies
    {
        /// <summary>
        /// The cached <see cref="Online.Multiplayer.MultiplayerClient"/>.
        /// </summary>
        TestMultiplayerClient MultiplayerClient { get; }

        /// <summary>
        /// The cached <see cref="sus.Game.Online.Spectator.SpectatorClient"/>.
        /// </summary>
        TestSpectatorClient SpectatorClient { get; }
    }
}
