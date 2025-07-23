// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Linq;
using sus.Framework.Graphics.Sprites;
using sus.Framework.Localisation;
using sus.Game.Graphics;
using sus.Game.Rulesets.Judgements;
using sus.Game.Rulesets.Scoring;

namespace sus.Game.Rulesets.Mods
{
    public abstract class ModSuddenDeath : ModFailCondition
    {
        public override string Name => "Sudden Death";
        public override string Acronym => "SD";
        public override IconUsage? Icon => OsuIcon.ModSuddenDeath;
        public override ModType Type => ModType.DifficultyIncrease;
        public override LocalisableString Description => "Miss and fail.";
        public override double ScoreMultiplier => 1;
        public override bool Ranked => true;
        public override bool ValidForFreestyleAsRequiredMod => true;

        public override Type[] IncompatibleMods => base.IncompatibleMods.Append(typeof(ModPerfect)).ToArray();

        protected override bool FailCondition(HealthProcessor healthProcessor, JudgementResult result)
            => result.Type.AffectsCombo()
               && !result.IsHit;
    }
}
