// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using System.Linq;
using sus.Framework.Allocation;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Framework.IO.Stores;
using sus.Game.Beatmaps;
using sus.Game.Overlays.Toolbar;
using sus.Game.Rulesets;
using sus.Game.Rulesets.Difficulty;
using sus.Game.Rulesets.Mods;
using sus.Game.Rulesets.UI;

namespace sus.Game.Tests.Visual.Menus
{
    public partial class TestSceneToolbarRulesetSelector : OsuTestScene
    {
        [BackgroundDependencyLoader]
        private void load(RulesetStore rulesets, OsuGameBase game)
        {
            TestRuleset.Resources = new TestResourceStore(game.Resources);

            Dependencies.CacheAs<RulesetStore>(new TestRulesetStore(rulesets));

            Child = new Container
            {
                RelativeSizeAxes = Axes.X,
                Height = Toolbar.HEIGHT,
                Child = new ToolbarRulesetSelector(),
            };
        }

        private class TestRulesetStore : RulesetStore
        {
            public TestRulesetStore(RulesetStore store)
            {
                AvailableRulesets = store.AvailableRulesets.Append(new TestRuleset().RulesetInfo);
            }

            public override IEnumerable<RulesetInfo> AvailableRulesets { get; }
        }

        private class TestRuleset : Ruleset
        {
            public static IResourceStore<byte[]> Resources { get; set; } = null!;

            public override IEnumerable<Mod> GetModsFor(ModType type) => Enumerable.Empty<Mod>();

            public override DrawableRuleset CreateDrawableRulesetWith(IBeatmap beatmap, IReadOnlyList<Mod>? mods = null) => null!;

            public override IBeatmapConverter CreateBeatmapConverter(IBeatmap beatmap) => null!;

            public override DifficultyCalculator CreateDifficultyCalculator(IWorkingBeatmap beatmap) => null!;

            public override IResourceStore<byte[]> CreateResourceStore() => Resources;

            public override string Description => "Test Ruleset";
            public override string ShortName => "test";
        }

        private class TestResourceStore : ResourceStore<byte[]>
        {
            public TestResourceStore(IResourceStore<byte[]> store)
                : base(store)
            {
            }

            protected override IEnumerable<string> GetFilenames(string name) => base.GetFilenames(name)
                                                                                    .Select(s => s.Replace("UI/ruleset-select-test", "Gameplay/failsound"));
        }
    }
}
