// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using NUnit.Framework;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using sus.Game.Overlays;
using sus.Game.Overlays.SkinEditor;
using sus.Game.Rulesets;
using sus.Game.Rulesets.Osu;
using sus.Game.Skinning;

namespace sus.Game.Tests.Visual.Gameplay
{
    public partial class TestSceneSkinEditorComponentsList : SkinnableTestScene
    {
        [Cached]
        private readonly OverlayColourProvider colourProvider = new OverlayColourProvider(OverlayColourScheme.Blue);

        [Test]
        public void TestToggleEditor()
        {
            var skinComponentsContainer = new SkinnableContainer(new GlobalSkinnableContainerLookup(GlobalSkinnableContainers.SongSelect));

            AddStep("show available components", () => SetContents(_ => new SkinComponentToolbox(skinComponentsContainer, null)
            {
                Anchor = Anchor.TopRight,
                Origin = Anchor.TopRight,
                Width = 0.6f,
            }));
        }

        protected override Ruleset CreateRulesetForSkinProvider() => new OsuRuleset();
    }
}
