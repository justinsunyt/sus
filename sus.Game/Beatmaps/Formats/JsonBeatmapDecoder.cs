// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Game.IO;
using sus.Game.IO.Serialization;

namespace sus.Game.Beatmaps.Formats
{
    public class JsonBeatmapDecoder : Decoder<Beatmap>
    {
        public static void Register()
        {
            AddDecoder<Beatmap>("{", _ => new JsonBeatmapDecoder());
        }

        protected override void ParseStreamInto(LineBufferedReader stream, Beatmap output)
        {
            stream.ReadToEnd().DeserializeInto(output);

            foreach (var hitObject in output.HitObjects)
                hitObject.ApplyDefaults(output.ControlPointInfo, output.Difficulty);
        }
    }
}
