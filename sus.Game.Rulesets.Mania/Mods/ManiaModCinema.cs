// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using sus.Game.Beatmaps;
using sus.Game.Rulesets.Mania.Beatmaps;
using sus.Game.Rulesets.Mania.Objects;
using sus.Game.Rulesets.Mania.Replays;
using sus.Game.Rulesets.Mods;

namespace sus.Game.Rulesets.Mania.Mods
{
    public class ManiaModCinema : ModCinema<ManiaHitObject>
    {
        public override ModReplayData CreateReplayData(IBeatmap beatmap, IReadOnlyList<Mod> mods)
            => new ModReplayData(new ManiaAutoGenerator((ManiaBeatmap)beatmap).Generate(), new ModCreatedUser { Username = "sus!topus" });
    }
}
