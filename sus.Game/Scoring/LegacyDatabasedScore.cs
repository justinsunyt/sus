// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

#nullable disable

using System;
using System.Linq;
using sus.Framework.IO.Stores;
using sus.Game.Beatmaps;
using sus.Game.Extensions;
using sus.Game.Rulesets;
using sus.Game.Scoring.Legacy;

namespace sus.Game.Scoring
{
    public class LegacyDatabasedScore : Score
    {
        public LegacyDatabasedScore(ScoreInfo score, RulesetStore rulesets, BeatmapManager beatmaps, IResourceStore<byte[]> store)
        {
            ScoreInfo = score;

            string replayFilename = score.Files.FirstOrDefault(f => f.Filename.EndsWith(".osr", StringComparison.InvariantCultureIgnoreCase))?.File.GetStoragePath();

            if (replayFilename == null)
                return;

            using (var stream = store.GetStream(replayFilename))
            {
                if (stream == null)
                    return;

                Replay = new DatabasedLegacyScoreDecoder(rulesets, beatmaps).Parse(stream).Replay;
            }
        }
    }
}
