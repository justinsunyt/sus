// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

#nullable disable

using System.Linq;
using NUnit.Framework;
using sus.Framework.Allocation;
using sus.Framework.Bindables;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Framework.Testing;
using sus.Game.Graphics.UserInterface;
using sus.Game.Online.API;
using sus.Game.Online.API.Requests.Responses;
using sus.Game.Overlays;
using sus.Game.Overlays.AccountCreation;
using sus.Game.Overlays.Settings;
using sus.Game.Users;

namespace sus.Game.Tests.Visual.Online
{
    public partial class TestSceneAccountCreationOverlay : OsuTestScene
    {
        private readonly Container userPanelArea;
        private readonly AccountCreationOverlay accountCreation;

        private IBindable<APIUser> localUser;

        public TestSceneAccountCreationOverlay()
        {
            Children = new Drawable[]
            {
                accountCreation = new AccountCreationOverlay(),
                userPanelArea = new Container
                {
                    Padding = new MarginPadding(10),
                    AutoSizeAxes = Axes.Both,
                    Anchor = Anchor.TopRight,
                    Origin = Anchor.TopRight,
                },
            };
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            localUser = API.LocalUser.GetBoundCopy();
            localUser.BindValueChanged(user => { userPanelArea.Child = new UserGridPanel(user.NewValue) { Width = 200 }; }, true);
        }

        [Test]
        public void TestOverlayVisibility()
        {
            AddStep("start hidden", () => accountCreation.Hide());
            AddStep("log out", () => API.Logout());

            AddStep("show manually", () => accountCreation.Show());
            AddUntilStep("overlay is visible", () => accountCreation.State.Value == Visibility.Visible);

            AddStep("click button", () => accountCreation.ChildrenOfType<SettingsButton>().Single().TriggerClick());
            AddUntilStep("warning screen is present", () => accountCreation.ChildrenOfType<ScreenWarning>().SingleOrDefault()?.IsPresent == true);

            AddStep("log back in", () =>
            {
                API.Login("dummy", "password");
                ((DummyAPIAccess)API).AuthenticateSecondFactor("abcdefgh");
            });
            AddUntilStep("overlay is hidden", () => accountCreation.State.Value == Visibility.Hidden);
        }

        [Test]
        public void TestFullFlow()
        {
            AddStep("log out", () => API.Logout());

            AddStep("show manually", () => accountCreation.Show());
            AddUntilStep("overlay is visible", () => accountCreation.State.Value == Visibility.Visible);

            AddStep("click button", () => accountCreation.ChildrenOfType<SettingsButton>().Single().TriggerClick());
            AddUntilStep("warning screen is present", () => accountCreation.ChildrenOfType<ScreenWarning>().SingleOrDefault()?.IsPresent == true);

            AddStep("proceed", () => accountCreation.ChildrenOfType<DangerousSettingsButton>().Single().TriggerClick());
            AddUntilStep("entry screen is present", () => accountCreation.ChildrenOfType<ScreenEntry>().SingleOrDefault()?.IsPresent == true);

            AddStep("input details", () =>
            {
                var entryScreen = accountCreation.ChildrenOfType<ScreenEntry>().Single();
                entryScreen.ChildrenOfType<OsuTextBox>().ElementAt(0).Text = "new_user";
                entryScreen.ChildrenOfType<OsuTextBox>().ElementAt(1).Text = "new.user@fake.mail";
                entryScreen.ChildrenOfType<OsuTextBox>().ElementAt(2).Text = "password";
            });
            AddStep("click button", () => accountCreation.ChildrenOfType<ScreenEntry>().Single()
                                                         .ChildrenOfType<SettingsButton>().Single().TriggerClick());
            AddUntilStep("verification screen is present", () => accountCreation.ChildrenOfType<ScreenEmailVerification>().SingleOrDefault()?.IsPresent == true);

            AddStep("verify", () => ((DummyAPIAccess)API).AuthenticateSecondFactor("abcdefgh"));
            AddUntilStep("overlay is hidden", () => accountCreation.State.Value == Visibility.Hidden);
        }
    }
}
