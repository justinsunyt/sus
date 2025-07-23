// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Extensions;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Cursor;
using sus.Framework.Testing;
using sus.Game.Online.API;
using sus.Game.Online.API.Requests;
using sus.Game.Online.API.Requests.Responses;
using sus.Game.Overlays.Comments;
using sus.Game.Tests.Visual.UserInterface;
using susTK;

namespace sus.Game.Tests.Visual.Online
{
    public partial class TestSceneCommentReportButton : ThemeComparisonTestScene
    {
        [SetUpSteps]
        public void SetUpSteps()
        {
            AddStep("setup API", () => ((DummyAPIAccess)API).HandleRequest += req =>
            {
                switch (req)
                {
                    case CommentReportRequest report:
                        Scheduler.AddDelayed(report.TriggerSuccess, 1000);
                        return true;
                }

                return false;
            });
        }

        protected override Drawable CreateContent() => new PopoverContainer
        {
            RelativeSizeAxes = Axes.Both,
            Child = new CommentReportButton(new Comment { User = new APIUser { Username = "Someone" } })
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Scale = new Vector2(2f),
            }.With(b => Schedule(b.ShowPopover)),
        };
    }
}
