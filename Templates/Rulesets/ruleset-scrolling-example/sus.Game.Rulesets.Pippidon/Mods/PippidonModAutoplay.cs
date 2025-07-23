// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using sus.Game.Beatmaps;
using sus.Game.Rulesets.Mods;
using sus.Game.Rulesets.Pippidon.Replays;

namespace sus.Game.Rulesets.Pippidon.Mods
{
    public class PippidonModAutoplay : ModAutoplay
    {
        public override ModReplayData CreateReplayData(IBeatmap beatmap, IReadOnlyList<Mod> mods)
            => new ModReplayData(new PippidonAutoGenerator(beatmap).Generate(), new ModCreatedUser { Username = "sample" });
    }
}
