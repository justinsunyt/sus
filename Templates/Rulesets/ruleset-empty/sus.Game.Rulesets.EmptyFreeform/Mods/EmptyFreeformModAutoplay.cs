// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using sus.Game.Beatmaps;
using sus.Game.Rulesets.EmptyFreeform.Replays;
using sus.Game.Rulesets.Mods;

namespace sus.Game.Rulesets.EmptyFreeform.Mods
{
    public class EmptyFreeformModAutoplay : ModAutoplay
    {
        public override ModReplayData CreateReplayData(IBeatmap beatmap, IReadOnlyList<Mod> mods)
            => new ModReplayData(new EmptyFreeformAutoGenerator(beatmap).Generate(), new ModCreatedUser { Username = "sample" });
    }
}
