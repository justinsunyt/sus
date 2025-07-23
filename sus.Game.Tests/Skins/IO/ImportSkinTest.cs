// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using NUnit.Framework;
using sus.Framework.Allocation;
using sus.Framework.Platform;
using sus.Game.Database;
using sus.Game.Extensions;
using sus.Game.IO;
using sus.Game.Skinning;
using sus.Game.Tests.Resources;
using SharpCompress.Archives.Zip;

namespace sus.Game.Tests.Skins.IO
{
    public class ImportSkinTest : ImportTest
    {
        #region Testing filename metadata inclusion

        [TestCase("Archives/modified-classic-20220723.osk")]
        [TestCase("Archives/modified-default-20230117.osk")]
        [TestCase("Archives/modified-argon-20231106.osk")]
        public Task TestImportModifiedSkinHasResources(string archive) => runSkinTest(async sus =>
        {
            using (var stream = TestResources.OpenResource(archive))
            {
                var imported = await loadSkinIntoOsu(sus, new ImportTask(stream, "skin.osk"));

                // When the import filename doesn't match, it should be appended (and update the skin.ini).

                var skinManager = sus.Dependencies.Get<SkinManager>();

                skinManager.CurrentSkinInfo.Value = imported;

                Assert.That(skinManager.CurrentSkin.Value.LayoutInfos.Count, Is.EqualTo(2));
            }
        });

        [Test]
        public Task TestSingleImportDifferentFilename() => runSkinTest(async sus =>
        {
            var import1 = await loadSkinIntoOsu(sus, new ImportTask(createOskWithIni("test skin", "skinner"), "skin.osk"));

            // When the import filename doesn't match, it should be appended (and update the skin.ini).
            assertCorrectMetadata(import1, "test skin [skin]", "skinner", 1.0m, sus);
        });

        [Test]
        public Task TestSingleImportWeirdIniFileCase() => runSkinTest(async sus =>
        {
            var import1 = await loadSkinIntoOsu(sus, new ImportTask(createOskWithIni("test skin", "skinner", iniFilename: "Skin.InI"), "skin.osk"));

            // When the import filename doesn't match, it should be appended (and update the skin.ini).
            assertCorrectMetadata(import1, "test skin [skin]", "skinner", 1.0m, sus);
        });

        [Test]
        public Task TestSingleImportMissingSectionHeader() => runSkinTest(async sus =>
        {
            var import1 = await loadSkinIntoOsu(sus, new ImportTask(createOskWithIni("test skin", "skinner", includeSectionHeader: false), "skin.osk"));

            // When the import filename doesn't match, it should be appended (and update the skin.ini).
            assertCorrectMetadata(import1, "test skin [skin]", "skinner", 1.0m, sus);
        });

        [Test]
        public Task TestSingleImportMatchingFilename() => runSkinTest(async sus =>
        {
            var import1 = await loadSkinIntoOsu(sus, new ImportTask(createOskWithIni("test skin", "skinner"), "test skin.osk"));

            // When the import filename matches it shouldn't be appended.
            assertCorrectMetadata(import1, "test skin", "skinner", 1.0m, sus);
        });

        [Test]
        public Task TestSingleImportNoIniFile() => runSkinTest(async sus =>
        {
            var import1 = await loadSkinIntoOsu(sus, new ImportTask(createOskWithNonIniFile(), "test skin.osk"));

            // When the import filename matches it shouldn't be appended.
            assertCorrectMetadata(import1, "test skin", "Unknown", SkinConfiguration.LATEST_VERSION, sus);
        });

        [Test]
        public Task TestEmptyImportImportsWithFilename() => runSkinTest(async sus =>
        {
            var import1 = await loadSkinIntoOsu(sus, new ImportTask(createEmptyOsk(), "test skin.osk"));

            // When the import filename matches it shouldn't be appended.
            assertCorrectMetadata(import1, "test skin", "Unknown", SkinConfiguration.LATEST_VERSION, sus);
        });

        #endregion

        #region Cases where imports should match existing

        [Test]
        public Task TestImportTwiceWithSameMetadataAndFilename([Values] bool batchImport) => runSkinTest(async sus =>
        {
            var import1 = await loadSkinIntoOsu(sus, new ImportTask(createOskWithIni("test skin", "skinner"), "skin.osk"), batchImport);
            var import2 = await loadSkinIntoOsu(sus, new ImportTask(createOskWithIni("test skin", "skinner"), "skin.osk"), batchImport);

            assertImportedOnce(import1, import2);
        });

        [Test]
        public Task TestImportTwiceWithNoMetadataSameDownloadFilename([Values] bool batchImport) => runSkinTest(async sus =>
        {
            // if a user downloads two skins that do have skin.ini files but don't have any creator metadata in the skin.ini, they should both import separately just for safety.
            var import1 = await loadSkinIntoOsu(sus, new ImportTask(createOskWithIni(string.Empty, string.Empty), "download.osk"), batchImport);
            var import2 = await loadSkinIntoOsu(sus, new ImportTask(createOskWithIni(string.Empty, string.Empty), "download.osk"), batchImport);

            assertImportedOnce(import1, import2);
        });

        [Test]
        public Task TestImportUpperCasedOskArchive() => runSkinTest(async sus =>
        {
            var import1 = await loadSkinIntoOsu(sus, new ImportTask(createOskWithIni("name 1", "author 1"), "name 1.OsK"));
            assertCorrectMetadata(import1, "name 1", "author 1", 1.0m, sus);

            var import2 = await loadSkinIntoOsu(sus, new ImportTask(createOskWithIni("name 1", "author 1"), "name 1.oSK"));

            assertImportedOnce(import1, import2);
        });

        [Test]
        public Task TestImportExportedSkinFilename() => runSkinTest(async sus =>
        {
            MemoryStream exportStream = new MemoryStream();

            var import1 = await loadSkinIntoOsu(sus, new ImportTask(createOskWithIni("name 1", "author 1"), "custom.osk"));
            assertCorrectMetadata(import1, "name 1 [custom]", "author 1", 1.0m, sus);

            await new LegacySkinExporter(sus.Dependencies.Get<Storage>()).ExportToStreamAsync(import1, exportStream);

            string exportFilename = import1.GetDisplayString();

            var import2 = await loadSkinIntoOsu(sus, new ImportTask(exportStream, $"{exportFilename}.osk"));
            assertCorrectMetadata(import2, "name 1 [custom]", "author 1", 1.0m, sus);

            assertImportedOnce(import1, import2);
        });

        [Test]
        public Task TestImportExportedNonAsciiSkinFilename() => runSkinTest(async sus =>
        {
            MemoryStream exportStream = new MemoryStream();

            var import1 = await loadSkinIntoOsu(sus, new ImportTask(createOskWithIni("name 『1』", "author 1"), "custom.osk"));
            assertCorrectMetadata(import1, "name 『1』 [custom]", "author 1", 1.0m, sus);

            await new LegacySkinExporter(sus.Dependencies.Get<Storage>()).ExportToStreamAsync(import1, exportStream);

            string exportFilename = import1.GetDisplayString().GetValidFilename();

            var import2 = await loadSkinIntoOsu(sus, new ImportTask(exportStream, $"{exportFilename}.osk"));
            assertCorrectMetadata(import2, "name 『1』 [custom]", "author 1", 1.0m, sus);
        });

        [Test]
        public Task TestSameMetadataNameSameFolderName([Values] bool batchImport) => runSkinTest(async sus =>
        {
            var import1 = await loadSkinIntoOsu(sus, new ImportTask(createOskWithIni("name 1", "author 1"), "my custom skin 1"), batchImport);
            var import2 = await loadSkinIntoOsu(sus, new ImportTask(createOskWithIni("name 1", "author 1"), "my custom skin 1"), batchImport);

            assertImportedOnce(import1, import2);
            assertCorrectMetadata(import1, "name 1 [my custom skin 1]", "author 1", 1.0m, sus);
        });

        [Test]
        public Task TestImportWithSubfolder() => runSkinTest(async sus =>
        {
            const string filename = "Archives/skin-with-subfolder-zip-entries.osk";
            var import = await loadSkinIntoOsu(sus, new ImportTask(TestResources.OpenResource(filename), filename));

            assertCorrectMetadata(import, $"Totally fully features skin [Real Skin with Real Features] [{filename[..^4]}]", "Unknown", 2.7m, sus);
            Assert.That(import.PerformRead(r => r.Files.Count), Is.EqualTo(3));
        });

        #endregion

        #region Cases where imports should be uniquely imported

        [Test]
        public Task TestImportTwiceWithSameMetadataButDifferentFilename() => runSkinTest(async sus =>
        {
            var import1 = await loadSkinIntoOsu(sus, new ImportTask(createOskWithIni("test skin", "skinner"), "skin.osk"));
            var import2 = await loadSkinIntoOsu(sus, new ImportTask(createOskWithIni("test skin", "skinner"), "skin2.osk"));

            assertImportedBoth(import1, import2);
        });

        [Test]
        public Task TestImportTwiceWithNoMetadataDifferentDownloadFilename() => runSkinTest(async sus =>
        {
            // if a user downloads two skins that do have skin.ini files but don't have any creator metadata in the skin.ini, they should both import separately just for safety.
            var import1 = await loadSkinIntoOsu(sus, new ImportTask(createOskWithIni(string.Empty, string.Empty), "download.osk"));
            var import2 = await loadSkinIntoOsu(sus, new ImportTask(createOskWithIni(string.Empty, string.Empty), "download2.osk"));

            assertImportedBoth(import1, import2);
        });

        [Test]
        public Task TestImportTwiceWithSameFilenameDifferentMetadata() => runSkinTest(async sus =>
        {
            var import1 = await loadSkinIntoOsu(sus, new ImportTask(createOskWithIni("test skin v2", "skinner"), "skin.osk"));
            var import2 = await loadSkinIntoOsu(sus, new ImportTask(createOskWithIni("test skin v2.1", "skinner"), "skin.osk"));

            assertImportedBoth(import1, import2);
            assertCorrectMetadata(import1, "test skin v2 [skin]", "skinner", 1.0m, sus);
            assertCorrectMetadata(import2, "test skin v2.1 [skin]", "skinner", 1.0m, sus);
        });

        [Test]
        public Task TestSameMetadataNameDifferentFolderName() => runSkinTest(async sus =>
        {
            var import1 = await loadSkinIntoOsu(sus, new ImportTask(createOskWithIni("name 1", "author 1"), "my custom skin 1"));
            var import2 = await loadSkinIntoOsu(sus, new ImportTask(createOskWithIni("name 1", "author 1"), "my custom skin 2"));

            assertImportedBoth(import1, import2);
            assertCorrectMetadata(import1, "name 1 [my custom skin 1]", "author 1", 1.0m, sus);
            assertCorrectMetadata(import2, "name 1 [my custom skin 2]", "author 1", 1.0m, sus);
        });

        [Test]
        public Task TestExportThenImportDefaultSkin() => runSkinTest(async sus =>
        {
            var skinManager = sus.Dependencies.Get<SkinManager>();

            skinManager.EnsureMutableSkin();

            MemoryStream exportStream = new MemoryStream();

            Guid originalSkinId = skinManager.CurrentSkinInfo.Value.ID;

            await skinManager.CurrentSkinInfo.Value.PerformRead(async s =>
            {
                Assert.IsFalse(s.Protected);
                Assert.AreEqual(typeof(ArgonSkin), s.CreateInstance(skinManager).GetType());

                await new LegacySkinExporter(sus.Dependencies.Get<Storage>()).ExportToStreamAsync(skinManager.CurrentSkinInfo.Value, exportStream);

                Assert.Greater(exportStream.Length, 0);
            });

            var imported = await skinManager.Import(new ImportTask(exportStream, "exported.osk"));

            imported.PerformRead(s =>
            {
                Assert.IsFalse(s.Protected);
                Assert.AreNotEqual(originalSkinId, s.ID);
                Assert.AreEqual(typeof(ArgonSkin), s.CreateInstance(skinManager).GetType());
            });
        });

        [Test]
        public Task TestExportThenImportClassicSkin() => runSkinTest(async sus =>
        {
            var skinManager = sus.Dependencies.Get<SkinManager>();

            skinManager.CurrentSkinInfo.Value = skinManager.DefaultClassicSkin.SkinInfo;

            skinManager.EnsureMutableSkin();

            MemoryStream exportStream = new MemoryStream();

            Guid originalSkinId = skinManager.CurrentSkinInfo.Value.ID;

            await skinManager.CurrentSkinInfo.Value.PerformRead(async s =>
            {
                Assert.IsFalse(s.Protected);
                Assert.AreEqual(typeof(DefaultLegacySkin), s.CreateInstance(skinManager).GetType());

                await new LegacySkinExporter(sus.Dependencies.Get<Storage>()).ExportToStreamAsync(skinManager.CurrentSkinInfo.Value, exportStream);

                Assert.Greater(exportStream.Length, 0);
            });

            var imported = await skinManager.Import(new ImportTask(exportStream, "exported.osk"));

            imported.PerformRead(s =>
            {
                Assert.IsFalse(s.Protected);
                Assert.AreNotEqual(originalSkinId, s.ID);
                Assert.AreEqual(typeof(DefaultLegacySkin), s.CreateInstance(skinManager).GetType());
            });
        });

        #endregion

        private void assertCorrectMetadata(Live<SkinInfo> import1, string name, string creator, decimal version, OsuGameBase sus)
        {
            import1.PerformRead(i =>
            {
                Assert.That(i.Name, Is.EqualTo(name));
                Assert.That(i.Creator, Is.EqualTo(creator));

                // for extra safety let's reconstruct the skin, reading from the skin.ini.
                var instance = i.CreateInstance((IStorageResourceProvider)sus.Dependencies.Get(typeof(SkinManager)));

                Assert.That(instance.Configuration.SkinInfo.Name, Is.EqualTo(name));
                Assert.That(instance.Configuration.SkinInfo.Creator, Is.EqualTo(creator));
                Assert.That(instance.Configuration.LegacyVersion, Is.EqualTo(version));
            });
        }

        private void assertImportedBoth(Live<SkinInfo> import1, Live<SkinInfo> import2)
        {
            import1.PerformRead(i1 => import2.PerformRead(i2 =>
            {
                Assert.That(i2.ID, Is.Not.EqualTo(i1.ID));
                Assert.That(i2.Hash, Is.Not.EqualTo(i1.Hash));
                Assert.That(i2.Files.First(), Is.Not.EqualTo(i1.Files.First()));
            }));
        }

        private void assertImportedOnce(Live<SkinInfo> import1, Live<SkinInfo> import2)
        {
            import1.PerformRead(i1 => import2.PerformRead(i2 =>
            {
                Assert.That(i2.ID, Is.EqualTo(i1.ID));
                Assert.That(i2.Hash, Is.EqualTo(i1.Hash));
                Assert.That(i2.Files.First(), Is.EqualTo(i1.Files.First()));
            }));
        }

        private MemoryStream createEmptyOsk()
        {
            var zipStream = new MemoryStream();
            using var zip = ZipArchive.Create();
            zip.SaveTo(zipStream);
            return zipStream;
        }

        private MemoryStream createOskWithNonIniFile()
        {
            var zipStream = new MemoryStream();
            using var zip = ZipArchive.Create();
            zip.AddEntry("hitcircle.png", new MemoryStream(new byte[] { 0, 1, 2, 3 }));
            zip.SaveTo(zipStream);
            return zipStream;
        }

        private MemoryStream createOskWithIni(string name, string author, bool makeUnique = false, string iniFilename = @"skin.ini", bool includeSectionHeader = true)
        {
            var zipStream = new MemoryStream();
            using var zip = ZipArchive.Create();
            zip.AddEntry(iniFilename, generateSkinIni(name, author, makeUnique, includeSectionHeader));
            zip.SaveTo(zipStream);
            return zipStream;
        }

        private MemoryStream generateSkinIni(string name, string author, bool makeUnique = true, bool includeSectionHeader = true)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);

            if (includeSectionHeader)
                writer.WriteLine("[General]");

            writer.WriteLine($"Name: {name}");
            writer.WriteLine($"Author: {author}");

            if (makeUnique)
            {
                writer.WriteLine();
                writer.WriteLine($"# unique {Guid.NewGuid()}");
            }

            writer.Flush();

            return stream;
        }

        private async Task runSkinTest(Func<OsuGameBase, Task> action, [CallerMemberName] string callingMethodName = @"")
        {
            using (HeadlessGameHost host = new CleanRunHeadlessGameHost(callingMethodName: callingMethodName))
            {
                try
                {
                    var sus = LoadOsuIntoHost(host);
                    await action(sus);
                }
                finally
                {
                    host.Exit();
                }
            }
        }

        private async Task<Live<SkinInfo>> loadSkinIntoOsu(OsuGameBase sus, ImportTask import, bool batchImport = false)
        {
            var skinManager = sus.Dependencies.Get<SkinManager>();
            return await skinManager.Import(import, new ImportParameters { Batch = batchImport });
        }
    }
}
