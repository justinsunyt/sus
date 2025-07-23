// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.IO;
using System.Linq;
using NUnit.Framework;
using osu.Framework.Allocation;
using osu.Framework.Platform;
using osu.Framework.Testing;
using sus.Game.Tests;
using sus.Game.Tournament.Configuration;

namespace sus.Game.Tournament.Tests.NonVisual
{
    [TestFixture]
    public class CustomTourneyDirectoryTest : TournamentHostTest
    {
        [Test]
        public void TestDefaultDirectory()
        {
            using (HeadlessGameHost host = new CleanRunHeadlessGameHost())
            {
                try
                {
                    var sus = LoadTournament(host);
                    var storage = sus.Dependencies.Get<Storage>();

                    Assert.That(storage.GetFullPath("."), Is.EqualTo(Path.Combine(host.Storage.GetFullPath("."), "tournaments", "default")));
                }
                finally
                {
                    host.Exit();
                }
            }
        }

        [Test]
        public void TestCustomDirectory()
        {
            using (HeadlessGameHost host = new TestRunHeadlessGameHost(nameof(TestCustomDirectory))) // don't use clean run as we are writing a config file.
            {
                string susDesktopStorage = Path.Combine(host.UserStoragePaths.First(), nameof(TestCustomDirectory));
                const string custom_tournament = "custom";

                // need access before the game has constructed its own storage yet.
                Storage storage = new DesktopStorage(susDesktopStorage, host);
                // manual cleaning so we can prepare a config file.
                storage.DeleteDirectory(string.Empty);

                using (var storageConfig = new TournamentConfigManager(storage))
                    storageConfig.SetValue(StorageConfig.CurrentTournament, custom_tournament);

                try
                {
                    var sus = LoadTournament(host);

                    storage = sus.Dependencies.Get<Storage>();

                    Assert.That(storage.GetFullPath("."), Is.EqualTo(Path.Combine(host.Storage.GetFullPath("."), "tournaments", custom_tournament)));
                }
                finally
                {
                    host.Exit();
                }
            }
        }
    }
}
