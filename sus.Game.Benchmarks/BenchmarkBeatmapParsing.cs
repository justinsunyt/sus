// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.IO;
using BenchmarkDotNet.Attributes;
using sus.Framework.IO.Stores;
using sus.Game.Beatmaps;
using sus.Game.Beatmaps.Formats;
using sus.Game.IO;
using sus.Game.IO.Archives;
using sus.Game.Tests.Resources;

namespace sus.Game.Benchmarks
{
    public class BenchmarkBeatmapParsing : BenchmarkTest
    {
        private readonly MemoryStream beatmapStream = new MemoryStream();

        public override void SetUp()
        {
            using (var resources = new DllResourceStore(typeof(TestResources).Assembly))
            using (var archive = resources.GetStream("Resources/Archives/241526 Soleily - Renatus.osz"))
            using (var reader = new ZipArchiveReader(archive))
                reader.GetStream("Soleily - Renatus (Gamu) [Insane].sus").CopyTo(beatmapStream);
        }

        [Benchmark]
        public Beatmap BenchmarkBundledBeatmap()
        {
            beatmapStream.Seek(0, SeekOrigin.Begin);
            var reader = new LineBufferedReader(beatmapStream); // no disposal

            var decoder = Decoder.GetDecoder<Beatmap>(reader);
            return decoder.Decode(reader);
        }
    }
}
