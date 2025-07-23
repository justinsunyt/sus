// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Collections.Generic;
using System.Linq;
using sus.Game.Beatmaps;
using sus.Game.Rulesets.Difficulty;
using sus.Game.Rulesets.Difficulty.Preprocessing;
using sus.Game.Rulesets.Difficulty.Skills;
using sus.Game.Rulesets.Mods;

namespace sus.Game.Rulesets.EmptyScrolling
{
    public class EmptyScrollingDifficultyCalculator : DifficultyCalculator
    {
        public EmptyScrollingDifficultyCalculator(IRulesetInfo ruleset, IWorkingBeatmap beatmap)
            : base(ruleset, beatmap)
        {
        }

        protected override DifficultyAttributes CreateDifficultyAttributes(IBeatmap beatmap, Mod[] mods, Skill[] skills, double clockRate)
        {
            return new DifficultyAttributes(mods, 0);
        }

        protected override IEnumerable<DifficultyHitObject> CreateDifficultyHitObjects(IBeatmap beatmap, double clockRate) => Enumerable.Empty<DifficultyHitObject>();

        protected override Skill[] CreateSkills(IBeatmap beatmap, Mod[] mods, double clockRate) => Array.Empty<Skill>();
    }
}
