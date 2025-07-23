// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

#nullable disable

using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using sus.Framework.Allocation;
using sus.Game.Beatmaps;
using sus.Game.Database;
using sus.Game.Tests.Database;
using sus.Game.Tests.Resources;

namespace sus.Game.Tests.Beatmaps.IO
{
    public static class BeatmapImportHelper
    {
        public static async Task<BeatmapSetInfo> LoadQuickOszIntoOsu(OsuGameBase sus)
        {
            string temp = TestResources.GetQuickTestBeatmapForImport();

            var manager = sus.Dependencies.Get<BeatmapManager>();

            var importedSet = await manager.Import(new ImportTask(temp)).ConfigureAwait(false);

            Debug.Assert(importedSet != null);

            ensureLoaded(sus);

            waitForOrAssert(() => !File.Exists(temp), "Temporary file still exists after standard import", 5000);

            return manager.GetAllUsableBeatmapSets().Find(beatmapSet => beatmapSet.ID == importedSet.ID);
        }

        public static async Task<BeatmapSetInfo> LoadOszIntoOsu(OsuGameBase sus, string path = null, bool virtualTrack = false)
        {
            string temp = path ?? TestResources.GetTestBeatmapForImport(virtualTrack);

            var manager = sus.Dependencies.Get<BeatmapManager>();

            var importedSet = await manager.Import(new ImportTask(temp)).ConfigureAwait(false);

            Debug.Assert(importedSet != null);

            ensureLoaded(sus);

            waitForOrAssert(() => !File.Exists(temp), "Temporary file still exists after standard import", 5000);

            return manager.GetAllUsableBeatmapSets().Find(beatmapSet => beatmapSet.ID == importedSet.ID);
        }

        private static void ensureLoaded(OsuGameBase sus, int timeout = 60000)
        {
            var realm = sus.Dependencies.Get<RealmAccess>();

            realm.Run(r => BeatmapImporterTests.EnsureLoaded(r, timeout));

            // TODO: add back some extra checks outside of the realm ones?
            // var set = queryBeatmapSets().First();
            // foreach (BeatmapInfo b in set.Beatmaps)
            //     Assert.IsTrue(set.Beatmaps.Any(c => c.OnlineID == b.OnlineID));
            // Assert.IsTrue(set.Beatmaps.Count > 0);
            // var beatmap = store.GetWorkingBeatmap(set.Beatmaps.First(b => b.RulesetID == 0))?.Beatmap;
            // Assert.IsTrue(beatmap?.HitObjects.Any() == true);
            // beatmap = store.GetWorkingBeatmap(set.Beatmaps.First(b => b.RulesetID == 1))?.Beatmap;
            // Assert.IsTrue(beatmap?.HitObjects.Any() == true);
            // beatmap = store.GetWorkingBeatmap(set.Beatmaps.First(b => b.RulesetID == 2))?.Beatmap;
            // Assert.IsTrue(beatmap?.HitObjects.Any() == true);
            // beatmap = store.GetWorkingBeatmap(set.Beatmaps.First(b => b.RulesetID == 3))?.Beatmap;
            // Assert.IsTrue(beatmap?.HitObjects.Any() == true);
        }

        private static void waitForOrAssert(Func<bool> result, string failureMessage, int timeout = 60000)
        {
            Task task = Task.Run(() =>
            {
                while (!result()) Thread.Sleep(200);
            });

            Assert.IsTrue(task.Wait(timeout), failureMessage);
        }
    }
}
