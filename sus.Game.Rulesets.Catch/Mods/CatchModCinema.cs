// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using sus.Game.Beatmaps;
using sus.Game.Rulesets.Catch.Objects;
using sus.Game.Rulesets.Catch.Replays;
using sus.Game.Rulesets.Mods;

namespace sus.Game.Rulesets.Catch.Mods
{
    public class CatchModCinema : ModCinema<CatchHitObject>
    {
        public override ModReplayData CreateReplayData(IBeatmap beatmap, IReadOnlyList<Mod> mods)
            => new ModReplayData(new CatchAutoGenerator(beatmap).Generate(), new ModCreatedUser { Username = "sus!salad" });
    }
}
