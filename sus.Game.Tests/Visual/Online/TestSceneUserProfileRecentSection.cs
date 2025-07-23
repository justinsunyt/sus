// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using sus.Framework.Allocation;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Framework.Graphics.Shapes;
using sus.Game.Graphics;
using sus.Game.Graphics.Containers;
using sus.Game.Online.API.Requests;
using sus.Game.Online.API.Requests.Responses;
using sus.Game.Overlays;
using sus.Game.Overlays.Profile.Sections.Recent;

namespace sus.Game.Tests.Visual.Online
{
    [TestFixture]
    public partial class TestSceneUserProfileRecentSection : OsuTestScene
    {
        [Cached]
        private readonly OverlayColourProvider colourProvider = new OverlayColourProvider(OverlayColourScheme.Green);

        public TestSceneUserProfileRecentSection()
        {
            Children = new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = OsuColour.Gray(0.2f)
                },
                new OsuScrollContainer
                {
                    RelativeSizeAxes = Axes.Both,
                    Child = new FillFlowContainer<DrawableRecentActivity>
                    {
                        RelativeSizeAxes = Axes.X,
                        AutoSizeAxes = Axes.Y,
                        Direction = FillDirection.Vertical,
                        ChildrenEnumerable = createDummyActivities().Select(a => new DrawableRecentActivity(a))
                    },
                }
            };
        }

        private IEnumerable<APIRecentActivity> createDummyActivities()
        {
            var dummyBeatmap = new APIRecentActivity.RecentActivityBeatmap
            {
                Title = @"Dummy beatmap",
                Url = "/b/1337",
            };

            var dummyUser = new APIRecentActivity.RecentActivityUser
            {
                Username = "DummyReborn",
                Url = "/u/666",
                PreviousUsername = "Dummy",
            };

            return new[]
            {
                new APIRecentActivity
                {
                    User = dummyUser,
                    Type = RecentActivityType.Achievement,
                    Achievement = new APIRecentActivity.RecentActivityAchievement
                    {
                        Name = @"Feelin' It",
                        Slug = @"all-secret-feelinit",
                    },
                },
                new APIRecentActivity
                {
                    User = dummyUser,
                    Type = RecentActivityType.BeatmapPlaycount,
                    Count = 1337,
                    Beatmap = dummyBeatmap,
                },
                new APIRecentActivity
                {
                    User = dummyUser,
                    Type = RecentActivityType.BeatmapsetApprove,
                    Approval = BeatmapApproval.Approved,
                    Beatmapset = dummyBeatmap,
                },
                new APIRecentActivity
                {
                    User = dummyUser,
                    Type = RecentActivityType.BeatmapsetApprove,
                    Approval = BeatmapApproval.Loved,
                    Beatmapset = dummyBeatmap,
                },
                new APIRecentActivity
                {
                    User = dummyUser,
                    Type = RecentActivityType.BeatmapsetApprove,
                    Approval = BeatmapApproval.Qualified,
                    Beatmapset = dummyBeatmap,
                },
                new APIRecentActivity
                {
                    User = dummyUser,
                    Type = RecentActivityType.BeatmapsetApprove,
                    Approval = BeatmapApproval.Ranked,
                    Beatmapset = dummyBeatmap,
                },
                new APIRecentActivity
                {
                    User = dummyUser,
                    Type = RecentActivityType.BeatmapsetDelete,
                    Beatmapset = dummyBeatmap,
                },
                new APIRecentActivity
                {
                    User = dummyUser,
                    Type = RecentActivityType.BeatmapsetRevive,
                    Beatmapset = dummyBeatmap,
                },
                new APIRecentActivity
                {
                    User = dummyUser,
                    Type = RecentActivityType.BeatmapsetRevive,
                    Beatmapset = dummyBeatmap,
                },
                new APIRecentActivity
                {
                    User = dummyUser,
                    Type = RecentActivityType.BeatmapsetUpdate,
                    Beatmapset = dummyBeatmap,
                },
                new APIRecentActivity
                {
                    User = dummyUser,
                    Type = RecentActivityType.BeatmapsetUpload,
                    Beatmapset = dummyBeatmap,
                },
                new APIRecentActivity
                {
                    User = dummyUser,
                    Type = RecentActivityType.Rank,
                    Rank = 1,
                    Mode = "sus!",
                    Beatmap = dummyBeatmap,
                },
                new APIRecentActivity
                {
                    User = dummyUser,
                    Type = RecentActivityType.Rank,
                    Rank = 1,
                    Mode = "vitaru",
                    Beatmap = dummyBeatmap,
                },
                new APIRecentActivity
                {
                    User = dummyUser,
                    Type = RecentActivityType.Rank,
                    Rank = 1,
                    Mode = "fruits",
                    Beatmap = dummyBeatmap,
                },
                new APIRecentActivity
                {
                    User = dummyUser,
                    Type = RecentActivityType.RankLost,
                    Mode = "sus!",
                    Beatmap = dummyBeatmap,
                },
                new APIRecentActivity
                {
                    User = dummyUser,
                    Type = RecentActivityType.UsernameChange,
                },
                new APIRecentActivity
                {
                    User = dummyUser,
                    Type = RecentActivityType.UserSupportAgain,
                },
                new APIRecentActivity
                {
                    User = dummyUser,
                    Type = RecentActivityType.UserSupportFirst,
                },
                new APIRecentActivity
                {
                    User = dummyUser,
                    Type = RecentActivityType.UserSupportGift,
                },
            };
        }
    }
}
