// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Linq;
using NUnit.Framework;
using sus.Framework.Allocation;
using sus.Framework.Extensions.ObjectExtensions;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Framework.Testing;
using sus.Game.Beatmaps.ControlPoints;
using sus.Game.Overlays;
using sus.Game.Rulesets.Edit;
using sus.Game.Rulesets.Osu;
using sus.Game.Rulesets.Osu.Beatmaps;
using sus.Game.Screens.Edit;
using sus.Game.Screens.Edit.Compose;
using sus.Game.Skinning;

namespace sus.Game.Tests.Visual.Editing
{
    [TestFixture]
    public partial class TestSceneComposeScreen : EditorClockTestScene
    {
        private EditorBeatmap editorBeatmap = null!;

        [Cached]
        private EditorClipboard clipboard = new EditorClipboard();

        [SetUpSteps]
        public void SetUpSteps()
        {
            AddStep("setup compose screen", () =>
            {
                var beatmap = new OsuBeatmap
                {
                    BeatmapInfo = { Ruleset = new OsuRuleset().RulesetInfo },
                };

                beatmap.ControlPointInfo.Add(0, new TimingControlPoint());

                editorBeatmap = new EditorBeatmap(beatmap, new LegacyBeatmapSkin(beatmap.BeatmapInfo, null));

                Beatmap.Value = CreateWorkingBeatmap(editorBeatmap.PlayableBeatmap);

                Child = new DependencyProvidingContainer
                {
                    RelativeSizeAxes = Axes.Both,
                    CachedDependencies = new (Type, object)[]
                    {
                        (typeof(EditorBeatmap), editorBeatmap),
                        (typeof(IBeatSnapProvider), editorBeatmap),
                        (typeof(OverlayColourProvider), new OverlayColourProvider(OverlayColourScheme.Green)),
                    },
                    Children = new Drawable[]
                    {
                        editorBeatmap,
                        new ComposeScreen { State = { Value = Visibility.Visible } },
                    }
                };
            });

            AddUntilStep("wait for composer", () => this.ChildrenOfType<HitObjectComposer>().SingleOrDefault()?.IsLoaded == true);
        }

        /// <summary>
        /// Ensures that the skin of the edited beatmap is properly wrapped in a <see cref="LegacySkinTransformer"/>.
        /// </summary>
        [Test]
        public void TestLegacyBeatmapSkinHasTransformer()
        {
            AddAssert("legacy beatmap skin has transformer", () =>
            {
                var sources = this.ChildrenOfType<BeatmapSkinProvidingContainer>().First().AllSources;
                return sources.OfType<LegacySkinTransformer>().Count(t => t.Skin == editorBeatmap.BeatmapSkin.AsNonNull().Skin) == 1;
            });
        }
    }
}
