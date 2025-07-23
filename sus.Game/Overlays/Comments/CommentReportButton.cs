// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Linq;
using sus.Framework.Allocation;
using sus.Framework.Extensions;
using sus.Framework.Extensions.LocalisationExtensions;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Framework.Graphics.Cursor;
using sus.Framework.Graphics.Sprites;
using sus.Framework.Graphics.UserInterface;
using sus.Framework.Testing;
using sus.Game.Graphics;
using sus.Game.Graphics.Containers;
using sus.Game.Graphics.UserInterface;
using sus.Game.Online.API;
using sus.Game.Online.API.Requests;
using sus.Game.Online.API.Requests.Responses;
using sus.Game.Resources.Localisation.Web;
using susTK;

namespace sus.Game.Overlays.Comments
{
    public partial class CommentReportButton : CompositeDrawable, IHasPopover, IHasLineBaseHeight
    {
        private readonly Comment comment;

        private LinkFlowContainer link = null!;
        private LoadingSpinner loading = null!;

        [Resolved]
        private IAPIProvider api { get; set; } = null!;

        [Resolved]
        private OverlayColourProvider? colourProvider { get; set; }

        public CommentReportButton(Comment comment)
        {
            this.comment = comment;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            AutoSizeAxes = Axes.Both;

            InternalChildren = new Drawable[]
            {
                link = new LinkFlowContainer(s => s.Font = OsuFont.GetFont(size: 12, weight: FontWeight.Bold))
                {
                    AutoSizeAxes = Axes.Both,
                },
                loading = new LoadingSpinner
                {
                    Size = new Vector2(12f),
                }
            };

            link.AddLink(ReportStrings.CommentButton.ToLower(), this.ShowPopover);
        }

        public Popover GetPopover() => new ReportCommentPopover(comment)
        {
            Action = report
        };

        private void report(CommentReportReason reason, string comments)
        {
            var request = new CommentReportRequest(comment.Id, reason, comments);

            link.Hide();
            loading.Show();

            request.Success += () => Schedule(() =>
            {
                loading.Hide();

                link.Clear(true);
                link.AddText(UsersStrings.ReportThanks, s => s.Colour = colourProvider?.Content2 ?? Colour4.White);
                link.Show();

                this.FadeOut(2000, Easing.InQuint).Expire();
            });

            request.Failure += _ => Schedule(() =>
            {
                loading.Hide();
                link.Show();
            });

            api.Queue(request);
        }

        public float LineBaseHeight => link.ChildrenOfType<IHasLineBaseHeight>().FirstOrDefault()?.LineBaseHeight ?? DrawHeight;
    }
}
