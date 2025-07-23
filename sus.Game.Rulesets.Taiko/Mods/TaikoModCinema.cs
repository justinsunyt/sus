// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Collections.Generic;
using System.Linq;
using sus.Game.Beatmaps;
using sus.Game.Rulesets.Mods;
using sus.Game.Rulesets.Taiko.Objects;
using sus.Game.Rulesets.Taiko.Replays;

namespace sus.Game.Rulesets.Taiko.Mods
{
    public class TaikoModCinema : ModCinema<TaikoHitObject>
    {
        public override ModReplayData CreateReplayData(IBeatmap beatmap, IReadOnlyList<Mod> mods)
            => new ModReplayData(new TaikoAutoGenerator(beatmap).Generate(), new ModCreatedUser { Username = "mekkadsus!" });

        public override Type[] IncompatibleMods => base.IncompatibleMods.Concat(new[] { typeof(TaikoModSingleTap) }).ToArray();
    }
}
