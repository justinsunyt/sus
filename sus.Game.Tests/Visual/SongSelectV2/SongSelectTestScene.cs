// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Linq;
using sus.Framework.Allocation;
using sus.Framework.Audio;
using sus.Framework.Extensions;
using sus.Framework.Extensions.ObjectExtensions;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Framework.Graphics.Cursor;
using sus.Framework.Platform;
using sus.Framework.Screens;
using sus.Framework.Testing;
using sus.Game.Beatmaps;
using sus.Game.Configuration;
using sus.Game.Database;
using sus.Game.Online.Leaderboards;
using sus.Game.Overlays;
using sus.Game.Overlays.Toolbar;
using sus.Game.Rulesets;
using sus.Game.Rulesets.Mods;
using sus.Game.Scoring;
using sus.Game.Screens;
using sus.Game.Screens.Footer;
using sus.Game.Screens.Menu;
using sus.Game.Screens.Select.Filter;
using sus.Game.Screens.SelectV2;
using sus.Game.Tests.Resources;

namespace sus.Game.Tests.Visual.SongSelectV2
{
    public abstract partial class SongSelectTestScene : ScreenTestScene
    {
        protected BeatmapManager Beatmaps { get; private set; } = null!;
        protected RealmRulesetStore Rulesets { get; private set; } = null!;
        protected OsuConfigManager Config { get; private set; } = null!;
        protected ScoreManager ScoreManager { get; private set; } = null!;

        private RealmDetachedBeatmapStore beatmapStore = null!;

        protected Screens.SelectV2.SongSelect SongSelect { get; private set; } = null!;
        protected BeatmapCarousel Carousel => SongSelect.ChildrenOfType<BeatmapCarousel>().Single();

        [Cached]
        protected readonly ScreenFooter Footer;

        [Cached]
        private readonly OsuLogo logo;

        [Cached]
        private readonly VolumeOverlay volume;

        [Cached(typeof(INotificationOverlay))]
        private readonly INotificationOverlay notificationOverlay = new NotificationOverlay();

        [Cached]
        protected readonly LeaderboardManager LeaderboardManager = new LeaderboardManager();

        protected SongSelectTestScene()
        {
            Children = new Drawable[]
            {
                new PopoverContainer
                {
                    RelativeSizeAxes = Axes.Both,
                    Children = new Drawable[]
                    {
                        LeaderboardManager,
                        new Toolbar
                        {
                            State = { Value = Visibility.Visible },
                        },
                        Footer = new ScreenFooter
                        {
                            BackButtonPressed = () => Stack.CurrentScreen.Exit(),
                        },
                        logo = new OsuLogo
                        {
                            Alpha = 0f,
                        },
                        volume = new VolumeOverlay(),
                    },
                },
            };

            Stack.Padding = new MarginPadding { Top = Toolbar.HEIGHT };
        }

        protected override IReadOnlyDependencyContainer CreateChildDependencies(IReadOnlyDependencyContainer parent)
        {
            var dependencies = new DependencyContainer(base.CreateChildDependencies(parent));

            // These DI caches are required to ensure for interactive runs this test scene doesn't nuke all user beatmaps in the local install.
            // At a point we have isolated interactive test runs enough, this can likely be removed.
            dependencies.Cache(Rulesets = new RealmRulesetStore(Realm));
            dependencies.Cache(Realm);
            dependencies.Cache(Beatmaps = new BeatmapManager(LocalStorage, Realm, null, Dependencies.Get<AudioManager>(), Resources, Dependencies.Get<GameHost>(), Beatmap.Default));
            dependencies.Cache(Config = new OsuConfigManager(LocalStorage));
            dependencies.Cache(ScoreManager = new ScoreManager(Rulesets, () => Beatmaps, LocalStorage, Realm, API, Config));

            dependencies.CacheAs<BeatmapStore>(beatmapStore = new RealmDetachedBeatmapStore());

            return dependencies;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            Add(beatmapStore);
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            Stack.ScreenPushed += updateFooter;
            Stack.ScreenExited += updateFooter;
        }

        public override void SetUpSteps()
        {
            base.SetUpSteps();

            AddStep("reset defaults", () =>
            {
                Ruleset.Value = Rulesets.AvailableRulesets.First();

                Beatmap.SetDefault();
                SelectedMods.SetDefault();

                Config.SetValue(OsuSetting.SongSelectSortingMode, SortMode.Title);
                Config.SetValue(OsuSetting.SongSelectGroupMode, GroupMode.None);

                SongSelect = null!;
            });

            AddStep("delete all beatmaps", () => Beatmaps.Delete());
        }

        protected virtual void LoadSongSelect()
        {
            AddStep("load screen", () => Stack.Push(SongSelect = new SoloSongSelect()));
            AddUntilStep("wait for load", () => Stack.CurrentScreen == SongSelect && SongSelect.IsLoaded);
            AddUntilStep("wait for filtering", () => !Carousel.IsFiltering);
        }

        protected void SortBy(SortMode mode) => AddStep($"sort by {mode.GetDescription().ToLowerInvariant()}", () => Config.SetValue(OsuSetting.SongSelectSortingMode, mode));

        protected void GroupBy(GroupMode mode) => AddStep($"group by {mode.GetDescription().ToLowerInvariant()}", () => Config.SetValue(OsuSetting.SongSelectGroupMode, mode));

        protected void SortAndGroupBy(SortMode sort, GroupMode group)
        {
            AddStep($"sort by {sort.GetDescription().ToLowerInvariant()} & group by {group.GetDescription().ToLowerInvariant()}", () =>
            {
                Config.SetValue(OsuSetting.SongSelectSortingMode, sort);
                Config.SetValue(OsuSetting.SongSelectGroupMode, group);
            });
        }

        protected void ImportBeatmapForRuleset(params int[] rulesetIds)
        {
            int beatmapsCount = 0;

            AddStep($"import test map for ruleset {rulesetIds}", () =>
            {
                beatmapsCount = SongSelect.IsNull() ? 0 : Carousel.Filters.OfType<BeatmapCarouselFilterGrouping>().Single().SetItems.Count;
                Beatmaps.Import(TestResources.CreateTestBeatmapSetInfo(3, Rulesets.AvailableRulesets.Where(r => rulesetIds.Contains(r.OnlineID)).ToArray()));
            });

            // This is specifically for cases where the add is happening post song select load.
            // For cases where song select is null, the assertions are provided by the load checks.
            AddUntilStep("wait for imported to arrive in carousel", () => SongSelect.IsNull() || Carousel.Filters.OfType<BeatmapCarouselFilterGrouping>().Single().SetItems.Count > beatmapsCount);
        }

        protected void ChangeMods(params Mod[] mods) => AddStep($"change mods to {string.Join(", ", mods.Select(m => m.Acronym))}", () => SelectedMods.Value = mods);

        protected void ChangeRuleset(int rulesetId)
        {
            AddStep($"change ruleset to {rulesetId}", () => Ruleset.Value = Rulesets.AvailableRulesets.First(r => r.OnlineID == rulesetId));
        }

        /// <summary>
        /// Imports test beatmap sets to show in the carousel.
        /// </summary>
        /// <param name="difficultyCountPerSet">
        /// The exact count of difficulties to create for each beatmap set.
        /// A <see langword="null"/> value causes the count of difficulties to be selected randomly.
        /// </param>
        protected void AddManyTestMaps(int? difficultyCountPerSet = null)
        {
            AddStep("import test maps", () =>
            {
                var usableRulesets = Rulesets.AvailableRulesets.Where(r => r.OnlineID != 2).ToArray();

                for (int i = 0; i < 10; i++)
                    Beatmaps.Import(TestResources.CreateTestBeatmapSetInfo(difficultyCountPerSet, usableRulesets));
            });
        }

        protected void WaitForSuspension() => AddUntilStep("wait for not current", () => !SongSelect.AsNonNull().IsCurrentScreen());

        private void updateFooter(IScreen? _, IScreen? newScreen)
        {
            if (newScreen is OsuScreen susScreen && susScreen.ShowFooter)
            {
                Footer.Show();

                if (susScreen.IsLoaded)
                    updateFooterButtons();
                else
                {
                    // ensure the current buttons are immediately disabled on screen change (so they can't be pressed).
                    Footer.SetButtons(Array.Empty<ScreenFooterButton>());

                    susScreen.OnLoadComplete += _ => updateFooterButtons();
                }

                void updateFooterButtons()
                {
                    var buttons = susScreen.CreateFooterButtons();

                    susScreen.LoadComponentsAgainstScreenDependencies(buttons);

                    Footer.SetButtons(buttons);
                    Footer.Show();
                }
            }
            else
            {
                Footer.Hide();
                Footer.SetButtons(Array.Empty<ScreenFooterButton>());
            }
        }
    }
}
