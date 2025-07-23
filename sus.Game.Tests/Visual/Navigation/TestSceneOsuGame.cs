// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

#nullable disable

using System;
using System.Collections.Generic;
using NUnit.Framework;
using sus.Framework.Allocation;
using sus.Framework.Bindables;
using sus.Framework.Configuration;
using sus.Framework.Graphics.Textures;
using sus.Framework.Platform;
using sus.Game.Audio;
using sus.Game.Beatmaps;
using sus.Game.Configuration;
using sus.Game.Graphics;
using sus.Game.Input;
using sus.Game.Input.Bindings;
using sus.Game.Online.API;
using sus.Game.Online.Chat;
using sus.Game.Overlays;
using sus.Game.Rulesets;
using sus.Game.Rulesets.Mods;
using sus.Game.Scoring;
using sus.Game.Screens.Menu;
using sus.Game.Skinning;
using susTK;
using susTK.Input;

namespace sus.Game.Tests.Visual.Navigation
{
    [TestFixture]
    public partial class TestSceneOsuGame : OsuGameTestScene
    {
        private IReadOnlyList<Type> requiredGameDependencies => new[]
        {
            typeof(OsuGame),
            typeof(OsuLogo),
            typeof(IdleTracker),
            typeof(OnScreenDisplay),
            typeof(INotificationOverlay),
            typeof(BeatmapListingOverlay),
            typeof(DashboardOverlay),
            typeof(NewsOverlay),
            typeof(ChannelManager),
            typeof(ChatOverlay),
            typeof(SettingsOverlay),
            typeof(UserProfileOverlay),
            typeof(BeatmapSetOverlay),
            typeof(LoginOverlay),
            typeof(MusicController),
            typeof(AccountCreationOverlay),
            typeof(IDialogOverlay),
            typeof(ScreenshotManager)
        };

        private IReadOnlyList<Type> requiredGameBaseDependencies => new[]
        {
            typeof(OsuGameBase),
            typeof(Bindable<RulesetInfo>),
            typeof(IBindable<RulesetInfo>),
            typeof(Bindable<IReadOnlyList<Mod>>),
            typeof(IBindable<IReadOnlyList<Mod>>),
            typeof(LargeTextureStore),
            typeof(OsuConfigManager),
            typeof(SkinManager),
            typeof(ISkinSource),
            typeof(IAPIProvider),
            typeof(RulesetStore),
            typeof(ScoreManager),
            typeof(BeatmapManager),
            typeof(IRulesetConfigCache),
            typeof(OsuColour),
            typeof(IBindable<WorkingBeatmap>),
            typeof(Bindable<WorkingBeatmap>),
            typeof(GlobalActionContainer),
            typeof(PreviewTrackManager),
        };

        [Resolved]
        private OsuGameBase gameBase { get; set; }

        [Test]
        public void TestCursorHidesWhenIdle()
        {
            AddStep("move mouse inside game bounds", () => InputManager.MoveMouseTo(Game.ScreenSpaceDrawQuad.TopLeft + new Vector2(20)));
            AddStep("click mouse", () => InputManager.Click(MouseButton.Left));
            AddUntilStep("wait until idle", () => Game.IsIdle.Value);
            AddUntilStep("menu cursor hidden", () => Game.GlobalCursorDisplay.MenuCursor.ActiveCursor.Alpha == 0);
            AddStep("click mouse", () => InputManager.Click(MouseButton.Left));
            AddUntilStep("menu cursor shown", () => Game.GlobalCursorDisplay.MenuCursor.ActiveCursor.Alpha == 1);
        }

        [Test]
        public void TestNullRulesetHandled()
        {
            RulesetInfo ruleset = null;

            AddStep("store current ruleset", () => ruleset = Ruleset.Value);
            AddStep("set global ruleset to null value", () => Ruleset.Value = null);

            AddAssert("ruleset still valid", () => Ruleset.Value.Available);
            AddAssert("ruleset unchanged", () => ReferenceEquals(Ruleset.Value, ruleset));
        }

        [Test]
        public void TestSwitchThreadExecutionMode()
        {
            AddStep("Change thread mode to multi threaded", () => { Game.Dependencies.Get<FrameworkConfigManager>().SetValue(FrameworkSetting.ExecutionMode, ExecutionMode.MultiThreaded); });
            AddStep("Change thread mode to single thread", () => { Game.Dependencies.Get<FrameworkConfigManager>().SetValue(FrameworkSetting.ExecutionMode, ExecutionMode.SingleThread); });
        }

        [Test]
        public void TestUnavailableRulesetHandled()
        {
            RulesetInfo ruleset = null;

            AddStep("store current ruleset", () => ruleset = Ruleset.Value);
            AddStep("set global ruleset to invalid value", () => Ruleset.Value = new RulesetInfo
            {
                Name = "unavailable",
                Available = false,
            });

            AddAssert("ruleset still valid", () => Ruleset.Value.Available);
            AddAssert("ruleset unchanged", () => ReferenceEquals(Ruleset.Value, ruleset));
        }

        [Test]
        public void TestAvailableDependencies()
        {
            AddAssert("check OsuGame DI members", () =>
            {
                foreach (var type in requiredGameDependencies)
                {
                    if (Game.Dependencies.Get(type) == null)
                        throw new InvalidOperationException($"{type} has not been cached");
                }

                return true;
            });

            AddAssert("check OsuGameBase DI members", () =>
            {
                foreach (var type in requiredGameBaseDependencies)
                {
                    if (gameBase.Dependencies.Get(type) == null)
                        throw new InvalidOperationException($"{type} has not been cached");
                }

                return true;
            });
        }
    }
}
