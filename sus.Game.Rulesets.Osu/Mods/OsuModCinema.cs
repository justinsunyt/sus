// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Collections.Generic;
using System.Linq;
using sus.Game.Beatmaps;
using sus.Game.Rulesets.Mods;
using sus.Game.Rulesets.Osu.Objects;
using sus.Game.Rulesets.Osu.Replays;

namespace sus.Game.Rulesets.Osu.Mods
{
    public class OsuModCinema : ModCinema<OsuHitObject>
    {
        public override Type[] IncompatibleMods => base.IncompatibleMods.Concat(new[] { typeof(OsuModMagnetised), typeof(OsuModAutopilot), typeof(OsuModSpunOut), typeof(OsuModAlternate), typeof(OsuModSingleTap), typeof(OsuModRepel) }).ToArray();

        public override ModReplayData CreateReplayData(IBeatmap beatmap, IReadOnlyList<Mod> mods)
            => new ModReplayData(new OsuAutoGenerator(beatmap, mods).Generate(), new ModCreatedUser { Username = "Autoplay" });
    }
}
