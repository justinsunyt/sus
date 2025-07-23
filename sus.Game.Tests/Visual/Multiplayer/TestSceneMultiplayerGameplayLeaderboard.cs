// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using sus.Framework.Graphics;
using sus.Game.Online.API;
using sus.Game.Online.Multiplayer;
using sus.Game.Rulesets.Mods;
using sus.Game.Rulesets.Osu.Mods;
using sus.Game.Screens.Select.Leaderboards;

namespace sus.Game.Tests.Visual.Multiplayer
{
    public partial class TestSceneMultiplayerGameplayLeaderboard : MultiplayerGameplayLeaderboardTestScene
    {
        protected override MultiplayerRoomUser CreateUser(int userId)
        {
            var user = base.CreateUser(userId);

            if (userId == TOTAL_USERS - 1)
                user.Mods = new[] { new APIMod(new OsuModNoFail()) };

            return user;
        }

        protected override MultiplayerLeaderboardProvider CreateLeaderboardProvider() =>
            new TestLeaderboard(MultiplayerUsers.ToArray())
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
            };

        [Test]
        public void TestPerUserMods()
        {
            AddStep("first user has no mods", () => Assert.That(((TestLeaderboard)LeaderboardProvider!).UserMods[0], Is.Empty));
            AddStep("last user has NF mod", () =>
            {
                Assert.That(((TestLeaderboard)LeaderboardProvider!).UserMods[TOTAL_USERS - 1], Has.One.Items);
                Assert.That(((TestLeaderboard)LeaderboardProvider).UserMods[TOTAL_USERS - 1].Single(), Is.TypeOf<OsuModNoFail>());
            });
        }

        private partial class TestLeaderboard : MultiplayerLeaderboardProvider
        {
            public Dictionary<int, IReadOnlyList<Mod>> UserMods => UserScores.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ScoreProcessor.Mods);

            public TestLeaderboard(MultiplayerRoomUser[] users)
                : base(users)
            {
            }
        }
    }
}
