// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Game.Beatmaps;
using sus.Game.Rulesets.Mania.Objects;
using sus.Game.Rulesets.Mania.Scoring;
using sus.Game.Rulesets.Mods;

namespace sus.Game.Rulesets.Mania.Mods
{
    public class ManiaModClassic : ModClassic, IApplicableToBeatmap
    {
        public void ApplyToBeatmap(IBeatmap beatmap)
        {
            bool isConvert = !beatmap.BeatmapInfo.Ruleset.Equals(new ManiaRuleset().RulesetInfo);

            foreach (var ho in beatmap.HitObjects)
            {
                switch (ho)
                {
                    case Note note:
                    {
                        var hitWindows = (ManiaHitWindows)note.HitWindows;
                        hitWindows.IsConvert = isConvert;
                        hitWindows.ClassicModActive = true;
                        break;
                    }

                    case HoldNote hold:
                    {
                        var headWindows = (ManiaHitWindows)hold.Head.HitWindows;
                        var tailWindows = (ManiaHitWindows)hold.Tail.HitWindows;
                        headWindows.IsConvert = tailWindows.IsConvert = isConvert;
                        headWindows.ClassicModActive = tailWindows.ClassicModActive = true;
                        break;
                    }
                }
            }
        }
    }
}
