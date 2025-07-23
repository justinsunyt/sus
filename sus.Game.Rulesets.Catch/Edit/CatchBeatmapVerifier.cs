// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using System.Linq;
using sus.Game.Rulesets.Catch.Edit.Checks;
using sus.Game.Rulesets.Edit;
using sus.Game.Rulesets.Edit.Checks;
using sus.Game.Rulesets.Edit.Checks.Components;

namespace sus.Game.Rulesets.Catch.Edit
{
    public class CatchBeatmapVerifier : IBeatmapVerifier
    {
        private readonly List<ICheck> checks = new List<ICheck>
        {
            // Compose
            new CheckBananaShowerGap(),
            new CheckConcurrentObjects(),

            // Spread
            new CheckCatchLowestDiffDrainTime(),

            // Settings
            new CheckCatchAbnormalDifficultySettings(),
        };

        public IEnumerable<Issue> Run(BeatmapVerifierContext context)
        {
            return checks.SelectMany(check => check.Run(context));
        }
    }
}
