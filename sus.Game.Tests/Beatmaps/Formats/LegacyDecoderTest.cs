// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using NUnit.Framework;
using sus.Game.Beatmaps.Formats;
using sus.Game.IO;
using sus.Game.Tests.Resources;

namespace sus.Game.Tests.Beatmaps.Formats
{
    [TestFixture]
    public class LegacyDecoderTest
    {
        [Test]
        public void TestDecodeComments()
        {
            var decoder = new LineLoggingDecoder(14);

            using (var resStream = TestResources.OpenResource("comments.sus"))
            using (var stream = new LineBufferedReader(resStream))
            {
                decoder.Decode(stream);

                Assert.That(decoder.ParsedLines, Has.None.EqualTo("// Combo1: 0, 0, 0"));
                Assert.That(decoder.ParsedLines, Has.None.EqualTo("//Combo2: 0, 0, 0"));
                Assert.That(decoder.ParsedLines, Has.None.EqualTo(" // Combo3: 0, 0, 0"));
                Assert.That(decoder.ParsedLines, Has.One.EqualTo("Combo1: 100, 100, 100 // Comment at end of line"));
            }
        }

        private class LineLoggingDecoder : LegacyDecoder<TestModel>
        {
            public readonly List<string> ParsedLines = new List<string>();

            public LineLoggingDecoder(int version)
                : base(version)
            {
            }

            protected override bool ShouldSkipLine(string line)
            {
                bool result = base.ShouldSkipLine(line);

                if (!result)
                    ParsedLines.Add(line);

                return result;
            }
        }

        private class TestModel
        {
        }
    }
}
