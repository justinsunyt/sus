// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.IO;
using System.Linq;
using NUnit.Framework;
using sus.Game.Beatmaps;
using sus.Game.Tests.Resources;
using sus.Game.Beatmaps.Formats;
using sus.Game.IO;
using sus.Game.IO.Archives;

namespace sus.Game.Tests.Beatmaps.IO
{
    [TestFixture]
    public class OszArchiveReaderTest
    {
        [Test]
        public void TestReadBeatmaps()
        {
            using (var osz = TestResources.GetTestBeatmapStream())
            {
                var reader = new ZipArchiveReader(osz);
                string[] expected =
                {
                    "Soleily - Renatus (Deif) [Platter].sus",
                    "Soleily - Renatus (Deif) [Rain].sus",
                    "Soleily - Renatus (Deif) [Salad].sus",
                    "Soleily - Renatus (ExPew) [Another].sus",
                    "Soleily - Renatus (ExPew) [Hyper].sus",
                    "Soleily - Renatus (ExPew) [Normal].sus",
                    "Soleily - Renatus (Gamu) [Hard].sus",
                    "Soleily - Renatus (Gamu) [Insane].sus",
                    "Soleily - Renatus (Gamu) [Normal].sus",
                    "Soleily - Renatus (MMzz) [Futsuu].sus",
                    "Soleily - Renatus (MMzz) [Muzukashii].sus",
                    "Soleily - Renatus (MMzz) [Oni].sus"
                };
                string[] maps = reader.Filenames.ToArray();
                foreach (string map in expected)
                    Assert.Contains(map, maps);
            }
        }

        [Test]
        public void TestReadMetadata()
        {
            using (var osz = TestResources.GetTestBeatmapStream())
            {
                var reader = new ZipArchiveReader(osz);

                Beatmap beatmap;

                using (var stream = new LineBufferedReader(reader.GetStream("Soleily - Renatus (Deif) [Platter].sus")))
                    beatmap = Decoder.GetDecoder<Beatmap>(stream).Decode(stream);

                var meta = beatmap.Metadata;

                Assert.AreEqual(241526, beatmap.BeatmapInfo.BeatmapSet?.OnlineID);
                Assert.AreEqual("Soleily", meta.Artist);
                Assert.AreEqual("Soleily", meta.ArtistUnicode);
                Assert.AreEqual("03. Renatus - Soleily 192kbps.mp3", meta.AudioFile);
                Assert.AreEqual("Deif", meta.Author.Username);
                Assert.AreEqual("machinetop_background.jpg", meta.BackgroundFile);
                Assert.AreEqual(164471, meta.PreviewTime);
                Assert.AreEqual(string.Empty, meta.Source);
                Assert.AreEqual("MBC7 Unisphere 地球ヤバイEP Chikyu Yabai", meta.Tags);
                Assert.AreEqual("Renatus", meta.Title);
                Assert.AreEqual("Renatus", meta.TitleUnicode);
            }
        }

        [Test]
        public void TestReadFile()
        {
            using (var osz = TestResources.GetTestBeatmapStream())
            {
                var reader = new ZipArchiveReader(osz);

                using (var stream = new StreamReader(reader.GetStream("Soleily - Renatus (Deif) [Platter].sus")))
                {
                    Assert.AreEqual("sus file format v13", stream.ReadLine()?.Trim());
                }
            }
        }
    }
}
