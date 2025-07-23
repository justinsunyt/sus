// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using sus.Framework.Bindables;
using sus.Framework.Graphics;
using sus.Game.Beatmaps;
using sus.Game.Configuration;
using sus.Game.Rulesets.Mania.Configuration;
using sus.Game.Rulesets.Mania.UI;
using sus.Game.Rulesets.Mods;
using sus.Game.Rulesets.UI;
using sus.Game.Rulesets.UI.Scrolling;
using susTK;

namespace sus.Game.Rulesets.Mania.Edit
{
    public partial class DrawableManiaEditorRuleset : DrawableManiaRuleset, ISupportConstantAlgorithmToggle
    {
        public BindableBool ShowSpeedChanges { get; } = new BindableBool();

        public double? TimelineTimeRange { get; set; }

        public new IScrollingInfo ScrollingInfo => base.ScrollingInfo;

        public DrawableManiaEditorRuleset(Ruleset ruleset, IBeatmap beatmap, IReadOnlyList<Mod>? mods)
            : base(ruleset, beatmap, mods)
        {
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            ShowSpeedChanges.BindValueChanged(showChanges => VisualisationMethod = showChanges.NewValue ? ScrollVisualisationMethod.Sequential : ScrollVisualisationMethod.Constant, true);
        }

        protected override Playfield CreatePlayfield() => new ManiaEditorPlayfield(Beatmap.Stages)
        {
            Anchor = Anchor.Centre,
            Origin = Anchor.Centre,
            Size = Vector2.One
        };

        protected override void Update()
        {
            TargetTimeRange = TimelineTimeRange == null || ShowSpeedChanges.Value ? ComputeScrollTime(Config.Get<double>(ManiaRulesetSetting.ScrollSpeed)) : TimelineTimeRange.Value;
            base.Update();
        }
    }
}
