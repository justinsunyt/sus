// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

#nullable disable

using System.Linq;
using NUnit.Framework;
using sus.Framework.Allocation;
using sus.Framework.Extensions;
using sus.Framework.Input.Bindings;
using sus.Framework.Testing;
using sus.Game.Database;
using sus.Game.Graphics.UserInterface;
using sus.Game.Input.Bindings;
using sus.Game.Overlays.Settings.Sections.Input;
using sus.Game.Screens.Play;
using sus.Game.Screens.Play.HUD;
using sus.Game.Screens.SelectV2;
using sus.Game.Tests.Beatmaps.IO;
using susTK.Input;

namespace sus.Game.Tests.Visual.Navigation
{
    public partial class TestSceneChangeAndUseGameplayBindings : OsuGameTestScene
    {
        [Test]
        public void TestGameplayKeyBindings()
        {
            AddAssert("databased key is default", () => firstOsuRulesetKeyBindings.KeyCombination.Keys.SequenceEqual(new[] { InputKey.Z }));

            AddStep("open settings", () => { Game.Settings.Show(); });

            // Until step requires as settings has a delayed load.
            AddUntilStep("wait for button", () => configureBindingsButton?.Enabled.Value == true);
            AddStep("scroll to section", () => Game.Settings.SectionsContainer.ScrollTo(configureBindingsButton));
            AddStep("press button", () => configureBindingsButton.TriggerClick());
            AddUntilStep("wait for panel", () => keyBindingPanel?.IsLoaded == true);
            AddUntilStep("wait for sus subsection", () => susBindingSubsection?.IsLoaded == true);
            AddStep("scroll to section", () => keyBindingPanel.SectionsContainer.ScrollTo(susBindingSubsection));
            AddWaitStep("wait for scroll to end", 3);
            AddStep("start rebinding first sus! key", () =>
            {
                var button = susBindingSubsection.ChildrenOfType<KeyBindingRow>().First();

                InputManager.MoveMouseTo(button);
                InputManager.Click(MouseButton.Left);
            });

            AddStep("Press 's'", () => InputManager.Key(Key.S));

            AddUntilStep("wait for database updated", () => firstOsuRulesetKeyBindings.KeyCombination.Keys.SequenceEqual(new[] { InputKey.S }));

            AddStep("close settings", () => Game.Settings.Hide());

            AddStep("import beatmap", () => BeatmapImportHelper.LoadQuickOszIntoOsu(Game).WaitSafely());

            PushAndConfirm(() => new SoloSongSelect());

            AddUntilStep("wait for selection", () => !Game.Beatmap.IsDefault);
            AddUntilStep("wait for carousel load", () => songSelect.CarouselItemsPresented);

            AddStep("enter gameplay", () => InputManager.Key(Key.Enter));

            AddUntilStep("wait for player", () =>
            {
                DismissAnyNotifications();
                return player != null;
            });

            AddUntilStep("wait for gameplay", () => player?.IsBreakTime.Value == false);

            AddStep("press 'z'", () => InputManager.Key(Key.Z));
            AddAssert("key counter didn't increase", () => keyCounter.CountPresses.Value == 0);

            AddStep("press 's'", () => InputManager.Key(Key.S));
            AddAssert("key counter did increase", () => keyCounter.CountPresses.Value == 1);
        }

        private KeyBindingsSubsection susBindingSubsection => keyBindingPanel
                                                              .ChildrenOfType<VariantBindingsSubsection>()
                                                              .FirstOrDefault(s => s.Ruleset!.ShortName == "sus");

        private OsuButton configureBindingsButton => Game.Settings
                                                         .ChildrenOfType<BindingSettings>().SingleOrDefault()?
                                                         .ChildrenOfType<OsuButton>()
                                                         .First(b => b.Text.ToString() == "Configure");

        private KeyBindingPanel keyBindingPanel => Game.Settings
                                                       .ChildrenOfType<KeyBindingPanel>().SingleOrDefault();

        private RealmKeyBinding firstOsuRulesetKeyBindings => Game.Dependencies
                                                                  .Get<RealmAccess>().Realm
                                                                  .All<RealmKeyBinding>()
                                                                  .AsEnumerable()
                                                                  .First(k => k.RulesetName == "sus" && k.ActionInt == 0);

        private SoloSongSelect songSelect => Game.ScreenStack.CurrentScreen as SoloSongSelect;

        private Player player => Game.ScreenStack.CurrentScreen as Player;

        private KeyCounter keyCounter => player.ChildrenOfType<KeyCounter>().First();
    }
}
