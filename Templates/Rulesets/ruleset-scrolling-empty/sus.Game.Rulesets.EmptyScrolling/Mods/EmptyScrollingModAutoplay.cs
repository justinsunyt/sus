// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using sus.Game.Beatmaps;
using sus.Game.Rulesets.EmptyScrolling.Replays;
using sus.Game.Rulesets.Mods;

namespace sus.Game.Rulesets.EmptyScrolling.Mods
{
    public class EmptyScrollingModAutoplay : ModAutoplay
    {
        public override ModReplayData CreateReplayData(IBeatmap beatmap, IReadOnlyList<Mod> mods)
            => new ModReplayData(new EmptyScrollingAutoGenerator(beatmap).Generate(), new ModCreatedUser { Username = "sample" });
    }
}
