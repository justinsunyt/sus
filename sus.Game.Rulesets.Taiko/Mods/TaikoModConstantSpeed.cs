// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Graphics.Sprites;
using sus.Framework.Localisation;
using sus.Game.Configuration;
using sus.Game.Rulesets.Taiko.Objects;
using sus.Game.Rulesets.Taiko.UI;
using sus.Game.Rulesets.Mods;
using sus.Game.Rulesets.UI;

namespace sus.Game.Rulesets.Taiko.Mods
{
    public class TaikoModConstantSpeed : Mod, IApplicableToDrawableRuleset<TaikoHitObject>
    {
        public override string Name => "Constant Speed";
        public override string Acronym => "CS";
        public override double ScoreMultiplier => 0.9;
        public override LocalisableString Description => "No more tricky speed changes!";
        public override IconUsage? Icon => FontAwesome.Solid.Equals;
        public override ModType Type => ModType.Conversion;

        public void ApplyToDrawableRuleset(DrawableRuleset<TaikoHitObject> drawableRuleset)
        {
            var taikoRuleset = (DrawableTaikoRuleset)drawableRuleset;
            taikoRuleset.VisualisationMethod = ScrollVisualisationMethod.Constant;
        }
    }
}
