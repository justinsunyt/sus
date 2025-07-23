// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.IO;
using System.Linq;
using NUnit.Framework;
using sus.Framework.Allocation;
using sus.Framework.Platform;
using sus.Framework.Testing;
using sus.Game.Tournament.IO;
using sus.Game.Tournament.IPC;

namespace sus.Game.Tournament.Tests.NonVisual
{
    [TestFixture]
    public class IPCLocationTest : TournamentHostTest
    {
        [Test]
        public void CheckIPCLocation()
        {
            // don't use clean run because files are being written before sus! launches.
            using (var host = new TestRunHeadlessGameHost(nameof(CheckIPCLocation)))
            {
                string basePath = Path.Combine(host.UserStoragePaths.First(), nameof(CheckIPCLocation));

                // Set up a fake IPC client for the IPC Storage to switch to.
                string testStableInstallDirectory = Path.Combine(basePath, "stable-ce");
                Directory.CreateDirectory(testStableInstallDirectory);

                string ipcFile = Path.Combine(testStableInstallDirectory, "ipc.txt");
                File.WriteAllText(ipcFile, string.Empty);

                try
                {
                    var sus = LoadTournament(host);
                    TournamentStorage storage = (TournamentStorage)sus.Dependencies.Get<Storage>();
                    FileBasedIPC? ipc = null;

                    WaitForOrAssert(() => (ipc = sus.Dependencies.Get<MatchIPCInfo>() as FileBasedIPC)?.IsLoaded == true, @"ipc could not be populated in a reasonable amount of time");

                    Assert.True(ipc!.SetIPCLocation(testStableInstallDirectory));
                    Assert.True(storage.AllTournaments.Exists("stable.json"));
                }
                finally
                {
                    host.Exit();
                }
            }
        }
    }
}
