// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Game.Online.Multiplayer;
using sus.Game.Online.Spectator;
using sus.Game.Tests.Visual.OnlinePlay;
using sus.Game.Tests.Visual.Spectator;

namespace sus.Game.Tests.Visual.Multiplayer
{
    /// <summary>
    /// Contains the basic dependencies of multiplayer test scenes.
    /// </summary>
    public class MultiplayerTestSceneDependencies : OnlinePlayTestSceneDependencies, IMultiplayerTestSceneDependencies
    {
        public TestMultiplayerClient MultiplayerClient { get; }
        public TestSpectatorClient SpectatorClient { get; }

        public MultiplayerTestSceneDependencies()
        {
            MultiplayerClient = new TestMultiplayerClient(RequestsHandler);
            SpectatorClient = CreateSpectatorClient();

            CacheAs<MultiplayerClient>(MultiplayerClient);
            CacheAs<SpectatorClient>(SpectatorClient);
        }

        protected virtual TestSpectatorClient CreateSpectatorClient() => new TestSpectatorClient();
    }
}
