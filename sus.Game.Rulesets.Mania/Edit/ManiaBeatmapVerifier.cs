// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using System.Linq;
using sus.Game.Rulesets.Edit;
using sus.Game.Rulesets.Edit.Checks.Components;
using sus.Game.Rulesets.Mania.Edit.Checks;

namespace sus.Game.Rulesets.Mania.Edit
{
    public class ManiaBeatmapVerifier : IBeatmapVerifier
    {
        private readonly List<ICheck> checks = new List<ICheck>
        {
            // Compose
            new CheckManiaConcurrentObjects(),

            // Spread
            new CheckManiaLowestDiffDrainTime(),

            // Settings
            new CheckKeyCount(),
            new CheckManiaAbnormalDifficultySettings(),
        };

        public IEnumerable<Issue> Run(BeatmapVerifierContext context)
        {
            return checks.SelectMany(check => check.Run(context));
        }
    }
}
