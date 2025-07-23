// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Allocation;
using sus.Framework.Graphics;
using sus.Framework.Localisation;
using sus.Game.Graphics;
using sus.Game.Graphics.Containers;
using sus.Game.Graphics.UserInterfaceV2;
using sus.Game.Localisation;
using sus.Game.Overlays;

namespace sus.Game.Screens.Edit.Submission
{
    [LocalisableDescription(typeof(BeatmapSubmissionStrings), nameof(BeatmapSubmissionStrings.ContentPermissions))]
    public partial class ScreenContentPermissions : WizardScreen
    {
        [BackgroundDependencyLoader]
        private void load(OsuGame? game)
        {
            Content.AddRange(new Drawable[]
            {
                new OsuTextFlowContainer(cp => cp.Font = OsuFont.Default.With(size: CONTENT_FONT_SIZE))
                {
                    RelativeSizeAxes = Axes.X,
                    AutoSizeAxes = Axes.Y,
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                    Text = BeatmapSubmissionStrings.ContentPermissionsDisclaimer,
                },
                new RoundedButton
                {
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                    Width = 450,
                    Text = BeatmapSubmissionStrings.CheckContentUsageGuidelines,
                    Action = () => game?.ShowWiki(@"Rules/Content_usage_permissions"),
                },
            });
        }

        public override LocalisableString? NextStepText => BeatmapSubmissionStrings.ContentPermissionsAcknowledgement;
    }
}
