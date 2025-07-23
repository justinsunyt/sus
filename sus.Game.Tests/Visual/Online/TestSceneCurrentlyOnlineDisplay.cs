// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using osu.Framework.Graphics;
using osu.Framework.Testing;
using sus.Game.Database;
using sus.Game.Online.API.Requests.Responses;
using sus.Game.Online.Metadata;
using sus.Game.Online.Spectator;
using sus.Game.Overlays;
using sus.Game.Overlays.Dashboard;
using sus.Game.Screens.OnlinePlay.Match.Components;
using sus.Game.Tests.Visual.Metadata;
using sus.Game.Tests.Visual.Spectator;
using sus.Game.Users;

namespace sus.Game.Tests.Visual.Online
{
    public partial class TestSceneCurrentlyOnlineDisplay : OsuTestScene
    {
        private readonly APIUser streamingUser = new APIUser { Id = 2, Username = "Test user" };

        private TestSpectatorClient spectatorClient = null!;
        private TestMetadataClient metadataClient = null!;
        private CurrentlyOnlineDisplay currentlyOnline = null!;

        [SetUpSteps]
        public void SetUpSteps()
        {
            AddStep("set up components", () =>
            {
                spectatorClient = new TestSpectatorClient();
                metadataClient = new TestMetadataClient();
                var lookupCache = new TestUserLookupCache();

                Children = new Drawable[]
                {
                    lookupCache,
                    spectatorClient,
                    metadataClient,
                    new DependencyProvidingContainer
                    {
                        RelativeSizeAxes = Axes.Both,
                        CachedDependencies = new (Type, object)[]
                        {
                            (typeof(SpectatorClient), spectatorClient),
                            (typeof(MetadataClient), metadataClient),
                            (typeof(UserLookupCache), lookupCache),
                            (typeof(OverlayColourProvider), new OverlayColourProvider(OverlayColourScheme.Purple)),
                        },
                        Child = currentlyOnline = new CurrentlyOnlineDisplay
                        {
                            RelativeSizeAxes = Axes.Both,
                        }
                    },
                };
            });
        }

        [Test]
        public void TestBasicDisplay()
        {
            IDisposable token = null!;

            AddStep("Begin watching user presence", () => token = metadataClient.BeginWatchingUserPresence());
            AddStep("Add online user", () => metadataClient.UserPresenceUpdated(streamingUser.Id, new UserPresence { Status = UserStatus.Online, Activity = new UserActivity.ChoosingBeatmap() }));
            AddUntilStep("Panel loaded", () => currentlyOnline.ChildrenOfType<UserGridPanel>().FirstOrDefault()?.User.Id == 2);
            AddAssert("Spectate button disabled", () => currentlyOnline.ChildrenOfType<PurpleRoundedButton>().First().Enabled.Value, () => Is.False);

            AddStep("User began playing", () => metadataClient.UserPresenceUpdated(streamingUser.Id, new UserPresence { Status = UserStatus.Online, Activity = new UserActivity.InSoloGame() }));
            AddAssert("Spectate button enabled", () => currentlyOnline.ChildrenOfType<PurpleRoundedButton>().First().Enabled.Value, () => Is.True);

            AddStep("User finished playing", () => metadataClient.UserPresenceUpdated(streamingUser.Id, new UserPresence { Status = UserStatus.Online, Activity = new UserActivity.ChoosingBeatmap() }));
            AddAssert("Spectate button disabled", () => currentlyOnline.ChildrenOfType<PurpleRoundedButton>().First().Enabled.Value, () => Is.False);

            AddStep("Remove playing user", () => metadataClient.UserPresenceUpdated(streamingUser.Id, null));
            AddUntilStep("Panel no longer present", () => !currentlyOnline.ChildrenOfType<UserGridPanel>().Any());
            AddStep("End watching user presence", () => token.Dispose());
        }

        [Test]
        public void TestUserWasPlayingBeforeWatchingUserPresence()
        {
            IDisposable token = null!;

            AddStep("Begin watching user presence", () => token = metadataClient.BeginWatchingUserPresence());
            AddStep("Add online user", () => metadataClient.UserPresenceUpdated(streamingUser.Id, new UserPresence { Status = UserStatus.Online, Activity = new UserActivity.InSoloGame() }));
            AddUntilStep("Panel loaded", () => currentlyOnline.ChildrenOfType<UserGridPanel>().FirstOrDefault()?.User.Id == streamingUser.Id);
            AddAssert("Spectate button enabled", () => currentlyOnline.ChildrenOfType<PurpleRoundedButton>().First().Enabled.Value, () => Is.True);

            AddStep("User finished playing", () => metadataClient.UserPresenceUpdated(streamingUser.Id, new UserPresence { Status = UserStatus.Online, Activity = new UserActivity.ChoosingBeatmap() }));
            AddAssert("Spectate button disabled", () => currentlyOnline.ChildrenOfType<PurpleRoundedButton>().First().Enabled.Value, () => Is.False);
            AddStep("Remove playing user", () => metadataClient.UserPresenceUpdated(streamingUser.Id, null));
            AddStep("End watching user presence", () => token.Dispose());
        }

        internal partial class TestUserLookupCache : UserLookupCache
        {
            private static readonly string[] usernames =
            {
                "fieryrage",
                "Kerensa",
                "MillhioreF",
                "Player01",
                "smoogipoo",
                "Ephemeral",
                "BTMC",
                "Cilvery",
                "m980",
                "HappyStick",
                "LittleEndu",
                "frenzibyte",
                "Zallius",
                "BanchoBot",
                "rocketminer210",
                "pishifat"
            };

            protected override Task<APIUser?> ComputeValueAsync(int lookup, CancellationToken token = default)
            {
                // tests against failed lookups
                if (lookup == 13)
                    return Task.FromResult<APIUser?>(null);

                return Task.FromResult<APIUser?>(new APIUser
                {
                    Id = lookup,
                    Username = usernames[lookup % usernames.Length],
                });
            }
        }
    }
}
