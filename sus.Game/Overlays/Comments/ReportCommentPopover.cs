// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Game.Graphics.UserInterfaceV2;
using sus.Game.Online.API.Requests.Responses;
using osu.Game.Resources.Localisation.Web;

namespace sus.Game.Overlays.Comments
{
    public partial class ReportCommentPopover : ReportPopover<CommentReportReason>
    {
        public ReportCommentPopover(Comment? comment)
            : base(ReportStrings.CommentTitle(comment?.User?.Username ?? comment?.LegacyName ?? @"Someone"))
        {
        }
    }
}
