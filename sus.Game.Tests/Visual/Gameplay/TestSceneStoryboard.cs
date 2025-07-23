// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Linq;
using NUnit.Framework;
using sus.Framework.Allocation;
using sus.Framework.Bindables;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Framework.Graphics.Shapes;
using sus.Framework.Testing;
using sus.Framework.Timing;
using sus.Framework.Utils;
using sus.Game.Beatmaps;
using sus.Game.Beatmaps.Formats;
using sus.Game.IO;
using sus.Game.Overlays;
using sus.Game.Rulesets.Osu;
using sus.Game.Screens.Play;
using sus.Game.Storyboards;
using sus.Game.Storyboards.Drawables;
using sus.Game.Tests.Gameplay;
using sus.Game.Tests.Resources;
using susTK.Graphics;

namespace sus.Game.Tests.Visual.Gameplay
{
    [TestFixture]
    public partial class TestSceneStoryboard : OsuTestScene
    {
        private Container<DrawableStoryboard> storyboardContainer = null!;

        private DrawableStoryboard? storyboard;

        [Cached]
        private GameplayState testGameplayState = TestGameplayState.Create(new OsuRuleset());

        [Test]
        public void TestStoryboard()
        {
            AddStep("Restart", restart);
            AddToggleStep("Toggle passing state", passing => testGameplayState.HealthProcessor.Health.Value = passing ? 1 : 0);
        }

        [Test]
        public void TestStoryboardMissingVideo()
        {
            AddStep("Load storyboard with missing video", () => loadStoryboard("storyboard_no_video.sus"));
        }

        [Test]
        public void TestVideo()
        {
            AddStep("load storyboard with only video", () =>
            {
                // LegacyStoryboardDecoder doesn't parse WidescreenStoryboard, so it is set manually
                loadStoryboard("storyboard_only_video.sus", s => s.Beatmap.WidescreenStoryboard = false);
            });

            AddAssert("storyboard video present in hierarchy", () => this.ChildrenOfType<DrawableStoryboardVideo>().Any());
            AddAssert("storyboard is correct width", () => Precision.AlmostEquals(storyboard?.Width ?? 0f, 480 * 16 / 9f));
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            Clock = new FramedClock();

            AddRange(new Drawable[]
            {
                new Container
                {
                    RelativeSizeAxes = Axes.Both,
                    Children = new Drawable[]
                    {
                        new Box
                        {
                            RelativeSizeAxes = Axes.Both,
                            Colour = Color4.Black,
                        },
                        storyboardContainer = new Container<DrawableStoryboard>
                        {
                            RelativeSizeAxes = Axes.Both,
                        },
                    },
                },
                new NowPlayingOverlay
                {
                    Origin = Anchor.TopRight,
                    Anchor = Anchor.TopRight,
                    State = { Value = Visibility.Visible },
                }
            });

            Beatmap.BindValueChanged(beatmapChanged, true);
        }

        private void beatmapChanged(ValueChangedEvent<WorkingBeatmap> e) => loadStoryboard(e.NewValue.Storyboard);

        private void restart()
        {
            var track = Beatmap.Value.Track;

            track.Reset();
            loadStoryboard(Beatmap.Value.Storyboard);
            track.Start();
        }

        private void loadStoryboard(Storyboard toLoad)
        {
            if (storyboard != null)
                storyboardContainer.Remove(storyboard, true);

            storyboardContainer.Clock = new FramedClock(Beatmap.Value.Track);

            storyboard = toLoad.CreateDrawable(SelectedMods.Value);

            storyboardContainer.Add(storyboard);
        }

        private void loadStoryboard(string filename, Action<Storyboard>? setUpStoryboard = null)
        {
            Storyboard loaded;

            using (var str = TestResources.OpenResource(filename))
            using (var bfr = new LineBufferedReader(str))
            {
                var decoder = new LegacyStoryboardDecoder();
                loaded = decoder.Decode(bfr);
            }

            setUpStoryboard?.Invoke(loaded);

            loadStoryboard(loaded);
        }
    }
}
