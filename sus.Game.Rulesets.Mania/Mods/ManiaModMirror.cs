// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Extensions.IEnumerableExtensions;
using sus.Game.Rulesets.Mania.Objects;
using sus.Game.Rulesets.Mods;
using System.Linq;
using sus.Framework.Localisation;
using sus.Game.Beatmaps;
using sus.Game.Rulesets.Mania.Beatmaps;

namespace sus.Game.Rulesets.Mania.Mods
{
    public class ManiaModMirror : ModMirror, IApplicableToBeatmap
    {
        public override LocalisableString Description => "Notes are flipped horizontally.";
        public override bool Ranked => UsesDefaultConfiguration;

        public void ApplyToBeatmap(IBeatmap beatmap)
        {
            int availableColumns = ((ManiaBeatmap)beatmap).TotalColumns;

            beatmap.HitObjects.OfType<ManiaHitObject>().ForEach(h => h.Column = availableColumns - 1 - h.Column);
        }
    }
}
