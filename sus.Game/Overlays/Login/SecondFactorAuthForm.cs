// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Threading.Tasks;
using sus.Framework.Allocation;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Framework.Input;
using sus.Framework.Input.Events;
using sus.Framework.Logging;
using sus.Game.Graphics;
using sus.Game.Graphics.Containers;
using sus.Game.Graphics.UserInterface;
using sus.Game.Online.API;
using sus.Game.Online.API.Requests;
using sus.Game.Overlays.Settings;
using sus.Game.Resources.Localisation.Web;
using susTK;

namespace sus.Game.Overlays.Login
{
    public partial class SecondFactorAuthForm : Container
    {
        private OsuTextBox codeTextBox = null!;
        private LinkFlowContainer explainText = null!;
        private ErrorTextFlowContainer errorText = null!;

        private LoadingLayer loading = null!;

        [Resolved]
        private IAPIProvider api { get; set; } = null!;

        [BackgroundDependencyLoader]
        private void load()
        {
            RelativeSizeAxes = Axes.X;
            AutoSizeAxes = Axes.Y;

            Children = new Drawable[]
            {
                new FillFlowContainer
                {
                    RelativeSizeAxes = Axes.X,
                    AutoSizeAxes = Axes.Y,
                    Direction = FillDirection.Vertical,
                    Spacing = new Vector2(0, SettingsSection.ITEM_SPACING),
                    Children = new Drawable[]
                    {
                        new FillFlowContainer
                        {
                            RelativeSizeAxes = Axes.X,
                            AutoSizeAxes = Axes.Y,
                            Padding = new MarginPadding { Horizontal = SettingsPanel.CONTENT_MARGINS },
                            Direction = FillDirection.Vertical,
                            Spacing = new Vector2(0f, SettingsSection.ITEM_SPACING),
                            Children = new Drawable[]
                            {
                                new OsuTextFlowContainer(s => s.Font = OsuFont.GetFont(weight: FontWeight.Regular))
                                {
                                    RelativeSizeAxes = Axes.X,
                                    AutoSizeAxes = Axes.Y,
                                    Text = "An email has been sent to you with a verification code. Enter the code.",
                                },
                                codeTextBox = new OsuTextBox
                                {
                                    InputProperties = new TextInputProperties(TextInputType.Code),
                                    PlaceholderText = "Enter code",
                                    RelativeSizeAxes = Axes.X,
                                    TabbableContentContainer = this,
                                },
                                explainText = new LinkFlowContainer(s => s.Font = OsuFont.GetFont(weight: FontWeight.Regular))
                                {
                                    RelativeSizeAxes = Axes.X,
                                    AutoSizeAxes = Axes.Y,
                                },
                                errorText = new ErrorTextFlowContainer
                                {
                                    RelativeSizeAxes = Axes.X,
                                    AutoSizeAxes = Axes.Y,
                                    Alpha = 0,
                                },
                            },
                        },
                        new LinkFlowContainer
                        {
                            Padding = new MarginPadding { Horizontal = SettingsPanel.CONTENT_MARGINS },
                            RelativeSizeAxes = Axes.X,
                            AutoSizeAxes = Axes.Y,
                        },
                    }
                },
                loading = new LoadingLayer(true)
                {
                    Padding = new MarginPadding { Vertical = -SettingsSection.ITEM_SPACING },
                }
            };

            explainText.AddParagraph(UserVerificationStrings.BoxInfoCheckSpam);
            // We can't support localisable strings with nested links yet. Not sure if we even can (probably need to allow markdown link formatting or something).
            explainText.AddParagraph("If you can't access your email or have forgotten what you used, please follow the ");
            explainText.AddLink(UserVerificationStrings.BoxInfoRecoverLink, $"{api.Endpoints.WebsiteUrl}/home/password-reset");
            explainText.AddText(". You can also ");
            explainText.AddLink(UserVerificationStrings.BoxInfoReissueLink, () =>
            {
                loading.Show();

                var reissueRequest = new ReissueVerificationCodeRequest();
                reissueRequest.Failure += ex =>
                {
                    Logger.Error(ex, @"Failed to retrieve new verification code.");
                    loading.Hide();
                };
                reissueRequest.Success += () =>
                {
                    loading.Hide();
                };

                Task.Run(() => api.Perform(reissueRequest));
            });
            explainText.AddText(" or ");
            explainText.AddLink(UserVerificationStrings.BoxInfoLogoutLink, () => { api.Logout(); });
            explainText.AddText(".");

            codeTextBox.Current.BindValueChanged(code =>
            {
                string trimmedCode = code.NewValue.Trim();

                if (trimmedCode.Length == 8)
                {
                    api.AuthenticateSecondFactor(trimmedCode);
                    codeTextBox.Current.Disabled = true;
                }
            });

            if (api.LastLoginError?.Message is string error)
            {
                errorText.Alpha = 1;
                errorText.AddErrors(new[] { error });
            }
        }

        public override bool AcceptsFocus => true;

        protected override bool OnClick(ClickEvent e) => true;

        protected override void OnFocus(FocusEvent e)
        {
            Schedule(() => { GetContainingFocusManager()!.ChangeFocus(codeTextBox); });
        }
    }
}
