// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Allocation;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Framework.Localisation;
using sus.Game.Graphics.UserInterfaceV2;
using sus.Game.Localisation;
using sus.Game.Online.API;
using sus.Game.Overlays;
using susTK;

namespace sus.Game.Screens.Edit.Submission
{
    [LocalisableDescription(typeof(BeatmapSubmissionStrings), nameof(BeatmapSubmissionStrings.FrequentlyAskedQuestions))]
    public partial class ScreenFrequentlyAskedQuestions : WizardScreen
    {
        [BackgroundDependencyLoader]
        private void load(OsuGame? game, IAPIProvider api)
        {
            Content.Add(new FillFlowContainer
            {
                RelativeSizeAxes = Axes.X,
                AutoSizeAxes = Axes.Y,
                Direction = FillDirection.Vertical,
                Spacing = new Vector2(5),
                Children = new Drawable[]
                {
                    new FormButton
                    {
                        RelativeSizeAxes = Axes.X,
                        Caption = BeatmapSubmissionStrings.BeatmapRankingCriteriaDescription,
                        ButtonText = BeatmapSubmissionStrings.BeatmapRankingCriteria,
                        Action = () => game?.ShowWiki(@"Ranking_Criteria"),
                    },
                    new FormButton
                    {
                        RelativeSizeAxes = Axes.X,
                        Caption = BeatmapSubmissionStrings.SubmissionProcessDescription,
                        ButtonText = BeatmapSubmissionStrings.SubmissionProcess,
                        Action = () => game?.ShowWiki(@"Beatmap_ranking_procedure"),
                    },
                    new FormButton
                    {
                        RelativeSizeAxes = Axes.X,
                        Caption = BeatmapSubmissionStrings.MappingHelpForumDescription,
                        ButtonText = BeatmapSubmissionStrings.MappingHelpForum,
                        Action = () => game?.OpenUrlExternally($@"{api.Endpoints.WebsiteUrl}/community/forums/56"),
                    },
                    new FormButton
                    {
                        RelativeSizeAxes = Axes.X,
                        Caption = BeatmapSubmissionStrings.ModdingQueuesForumDescription,
                        ButtonText = BeatmapSubmissionStrings.ModdingQueuesForum,
                        Action = () => game?.OpenUrlExternally($@"{api.Endpoints.WebsiteUrl}/community/forums/60"),
                    },
                },
            });
        }
    }
}
