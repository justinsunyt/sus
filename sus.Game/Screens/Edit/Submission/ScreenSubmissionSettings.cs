// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Diagnostics;
using sus.Framework.Allocation;
using sus.Framework.Bindables;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Framework.Localisation;
using sus.Game.Beatmaps;
using sus.Game.Configuration;
using sus.Game.Graphics;
using sus.Game.Graphics.Containers;
using sus.Game.Graphics.UserInterfaceV2;
using sus.Game.Localisation;
using sus.Game.Online.API;
using sus.Game.Online.API.Requests;
using sus.Game.Overlays;
using susTK;

namespace sus.Game.Screens.Edit.Submission
{
    [LocalisableDescription(typeof(BeatmapSubmissionStrings), nameof(BeatmapSubmissionStrings.SubmissionSettings))]
    public partial class ScreenSubmissionSettings : WizardScreen
    {
        private readonly BindableBool notifyOnDiscussionReplies = new BindableBool();
        private readonly BindableBool loadInBrowserAfterSubmission = new BindableBool();

        public override LocalisableString? NextStepText => BeatmapSubmissionStrings.ConfirmSubmission;

        [Resolved]
        private BeatmapSubmissionSettings settings { get; set; } = null!;

        [BackgroundDependencyLoader]
        private void load(OsuConfigManager configManager, OsuColour colours)
        {
            configManager.BindWith(OsuSetting.EditorSubmissionNotifyOnDiscussionReplies, settings.NotifyOnDiscussionReplies);
            configManager.BindWith(OsuSetting.EditorSubmissionLoadInBrowserAfterSubmission, loadInBrowserAfterSubmission);

            Content.Add(new FillFlowContainer
            {
                RelativeSizeAxes = Axes.X,
                AutoSizeAxes = Axes.Y,
                Spacing = new Vector2(5),
                Children = new Drawable[]
                {
                    new FormEnumDropdown<BeatmapSubmissionTarget>
                    {
                        RelativeSizeAxes = Axes.X,
                        Caption = BeatmapSubmissionStrings.BeatmapSubmissionTargetCaption,
                        Current = settings.Target,
                    },
                    new FormCheckBox
                    {
                        Caption = BeatmapSubmissionStrings.NotifyOnDiscussionReplies,
                        Current = settings.NotifyOnDiscussionReplies,
                    },
                    new FormCheckBox
                    {
                        Caption = BeatmapSubmissionStrings.LoadInBrowserAfterSubmission,
                        Current = loadInBrowserAfterSubmission,
                    },
                    new OsuTextFlowContainer(cp => cp.Font = OsuFont.Default.With(size: CONTENT_FONT_SIZE, weight: FontWeight.Bold))
                    {
                        RelativeSizeAxes = Axes.X,
                        Colour = colours.Orange1,
                        Text = BeatmapSubmissionStrings.LegacyExportDisclaimer,
                        Padding = new MarginPadding { Top = 20 }
                    },
                }
            });

            switch (settings.LatestOnlineStateRequest?.CompletionState)
            {
                case APIRequestCompletionState.Completed:
                    setSubmissionTargetFromLatestOnlineState();
                    break;

                case APIRequestCompletionState.Waiting:
                    settings.Target.Disabled = true;
                    settings.LatestOnlineStateRequest.Success += _ => setSubmissionTargetFromLatestOnlineState();
                    break;
            }
        }

        private void setSubmissionTargetFromLatestOnlineState()
        {
            Debug.Assert(settings.LatestOnlineStateRequest != null);
            settings.Target.Disabled = false;
            settings.Target.Value = settings.LatestOnlineStateRequest.Response?.Status >= BeatmapOnlineStatus.Pending ? BeatmapSubmissionTarget.Pending : BeatmapSubmissionTarget.WIP;
        }
    }
}
