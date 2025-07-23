// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Game.Overlays.BeatmapSet;
using System.Collections.Specialized;
using System.Linq;
using sus.Framework.Graphics;
using sus.Game.Rulesets.Osu;
using sus.Game.Rulesets.Mania;
using sus.Game.Rulesets.Taiko;
using sus.Game.Rulesets.Catch;
using sus.Framework.Graphics.Containers;
using sus.Framework.Graphics.Sprites;
using sus.Framework.Extensions.IEnumerableExtensions;
using sus.Framework.Bindables;
using sus.Framework.Extensions.ObjectExtensions;
using sus.Game.Graphics.Sprites;
using sus.Game.Rulesets;
using sus.Game.Rulesets.Mods;

namespace sus.Game.Tests.Visual.Online
{
    public partial class TestSceneLeaderboardModSelector : OsuTestScene
    {
        public TestSceneLeaderboardModSelector()
        {
            LeaderboardModSelector modSelector;
            FillFlowContainer<SpriteText> selectedMods;

            var ruleset = new Bindable<IRulesetInfo?>();

            Add(selectedMods = new FillFlowContainer<SpriteText>
            {
                Anchor = Anchor.TopLeft,
                Origin = Anchor.TopLeft,
            });

            Add(modSelector = new LeaderboardModSelector
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Ruleset = { BindTarget = ruleset }
            });

            modSelector.SelectedMods.CollectionChanged += (_, args) =>
            {
                switch (args.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        args.NewItems.AsNonNull().Cast<Mod>().ForEach(mod => selectedMods.Add(new OsuSpriteText
                        {
                            Text = mod.Acronym,
                        }));
                        break;

                    case NotifyCollectionChangedAction.Remove:
                        args.OldItems.AsNonNull().Cast<Mod>().ForEach(mod =>
                        {
                            foreach (var selected in selectedMods)
                            {
                                if (selected.Text == mod.Acronym)
                                {
                                    selectedMods.Remove(selected, true);
                                    break;
                                }
                            }
                        });
                        break;
                }
            };

            AddStep("sus ruleset", () => ruleset.Value = new OsuRuleset().RulesetInfo);
            AddStep("mania ruleset", () => ruleset.Value = new ManiaRuleset().RulesetInfo);
            AddStep("taiko ruleset", () => ruleset.Value = new TaikoRuleset().RulesetInfo);
            AddStep("catch ruleset", () => ruleset.Value = new CatchRuleset().RulesetInfo);
            AddStep("Deselect all", () => modSelector.DeselectAll());
            AddStep("null ruleset", () => ruleset.Value = null);
        }
    }
}
