// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using osu.Framework.Bindables;
using sus.Game.Beatmaps;
using sus.Game.Configuration;
using sus.Game.Rulesets.Mods;
using sus.Game.Rulesets.Taiko.UI;
using sus.Game.Rulesets.UI;
using sus.Game.Rulesets.UI.Scrolling;

namespace sus.Game.Rulesets.Taiko.Edit
{
    public partial class DrawableTaikoEditorRuleset : DrawableTaikoRuleset, ISupportConstantAlgorithmToggle
    {
        public BindableBool ShowSpeedChanges { get; } = new BindableBool();

        public DrawableTaikoEditorRuleset(Ruleset ruleset, IBeatmap beatmap, IReadOnlyList<Mod> mods)
            : base(ruleset, beatmap, mods)
        {
        }

        protected override Playfield CreatePlayfield() => new TaikoEditorPlayfield();

        protected override void LoadComplete()
        {
            base.LoadComplete();

            ShowSpeedChanges.BindValueChanged(showChanges => VisualisationMethod = showChanges.NewValue ? ScrollVisualisationMethod.Overlapping : ScrollVisualisationMethod.Constant, true);
        }
    }
}
