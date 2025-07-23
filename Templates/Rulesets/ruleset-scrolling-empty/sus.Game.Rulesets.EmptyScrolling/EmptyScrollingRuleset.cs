// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Collections.Generic;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Sprites;
using sus.Framework.Input.Bindings;
using sus.Game.Beatmaps;
using sus.Game.Graphics;
using sus.Game.Rulesets.Difficulty;
using sus.Game.Rulesets.Mods;
using sus.Game.Rulesets.EmptyScrolling.Beatmaps;
using sus.Game.Rulesets.EmptyScrolling.Mods;
using sus.Game.Rulesets.EmptyScrolling.UI;
using sus.Game.Rulesets.UI;

namespace sus.Game.Rulesets.EmptyScrolling
{
    public class EmptyScrollingRuleset : Ruleset
    {
        public override string Description => "a very emptyscrolling ruleset";

        public override DrawableRuleset CreateDrawableRulesetWith(IBeatmap beatmap, IReadOnlyList<Mod> mods = null) => new DrawableEmptyScrollingRuleset(this, beatmap, mods);

        public override IBeatmapConverter CreateBeatmapConverter(IBeatmap beatmap) => new EmptyScrollingBeatmapConverter(beatmap, this);

        public override DifficultyCalculator CreateDifficultyCalculator(IWorkingBeatmap beatmap) => new EmptyScrollingDifficultyCalculator(RulesetInfo, beatmap);

        public override IEnumerable<Mod> GetModsFor(ModType type)
        {
            switch (type)
            {
                case ModType.Automation:
                    return new[] { new EmptyScrollingModAutoplay() };

                default:
                    return Array.Empty<Mod>();
            }
        }

        public override string ShortName => "emptyscrolling";

        public override IEnumerable<KeyBinding> GetDefaultKeyBindings(int variant = 0) => new[]
        {
            new KeyBinding(InputKey.Z, EmptyScrollingAction.Button1),
            new KeyBinding(InputKey.X, EmptyScrollingAction.Button2),
        };

        public override Drawable CreateIcon() => new SpriteText
        {
            Anchor = Anchor.Centre,
            Origin = Anchor.Centre,
            Text = ShortName[0].ToString(),
            Font = OsuFont.Default.With(size: 18),
        };

        // Leave this line intact. It will bake the correct version into the ruleset on each build/release.
        public override string RulesetAPIVersionSupported => CURRENT_RULESET_API_VERSION;
    }
}
