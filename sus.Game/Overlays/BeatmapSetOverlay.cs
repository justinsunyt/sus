// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

#nullable disable

using System.Linq;
using sus.Framework.Allocation;
using sus.Framework.Bindables;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Game.Online.API;
using sus.Game.Online.API.Requests;
using sus.Game.Online.API.Requests.Responses;
using sus.Game.Overlays.BeatmapSet;
using sus.Game.Overlays.BeatmapSet.Scores;
using sus.Game.Overlays.Comments;
using susTK;
using susTK.Graphics;

namespace sus.Game.Overlays
{
    public partial class BeatmapSetOverlay : OnlineOverlay<BeatmapSetHeader>
    {
        public const float Y_PADDING = 25;
        public const float RIGHT_WIDTH = 275;

        private readonly Bindable<APIBeatmapSet> beatmapSet = new Bindable<APIBeatmapSet>();

        [Resolved]
        private IAPIProvider api { get; set; }

        private IBindable<APIUser> apiUser;

        private (BeatmapSetLookupType type, int id)? lastLookup;

        public BeatmapSetOverlay()
            : base(OverlayColourScheme.Blue)
        {
            Info info;
            CommentsSection comments;

            Child = new FillFlowContainer
            {
                RelativeSizeAxes = Axes.X,
                AutoSizeAxes = Axes.Y,
                Direction = FillDirection.Vertical,
                Spacing = new Vector2(0, 20),
                Children = new Drawable[]
                {
                    info = new Info
                    {
                        Beatmap = { BindTarget = Header.HeaderContent.Picker.Beatmap }
                    },
                    new ScoresContainer
                    {
                        Beatmap = { BindTarget = Header.HeaderContent.Picker.Beatmap }
                    },
                    comments = new CommentsSection()
                }
            };

            Header.BeatmapSet.BindTo(beatmapSet);
            info.BeatmapSet.BindTo(beatmapSet);
            comments.BeatmapSet.BindTo(beatmapSet);

            Header.HeaderContent.Picker.Beatmap.ValueChanged += b => ScrollFlow.ScrollToStart();
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            apiUser = api.LocalUser.GetBoundCopy();
            apiUser.BindValueChanged(_ => Schedule(() =>
            {
                if (api.IsLoggedIn)
                    performFetch();
            }));
        }

        protected override BeatmapSetHeader CreateHeader() => new BeatmapSetHeader();

        protected override Color4 BackgroundColour => ColourProvider.Background6;

        protected override void PopOutComplete()
        {
            base.PopOutComplete();
            beatmapSet.Value = null;
        }

        public void FetchAndShowBeatmap(int beatmapId)
        {
            lastLookup = (BeatmapSetLookupType.BeatmapId, beatmapId);
            beatmapSet.Value = null;

            performFetch();
            Show();
        }

        public void FetchAndShowBeatmapSet(int beatmapSetId)
        {
            lastLookup = (BeatmapSetLookupType.SetId, beatmapSetId);

            beatmapSet.Value = null;

            performFetch();
            Show();
        }

        /// <summary>
        /// Show an already fully-populated beatmap set.
        /// </summary>
        /// <param name="set">The set to show.</param>
        public void ShowBeatmapSet(APIBeatmapSet set)
        {
            beatmapSet.Value = set;
            Show();
        }

        private void performFetch()
        {
            if (!api.IsLoggedIn)
                return;

            if (lastLookup == null)
                return;

            var req = new GetBeatmapSetRequest(lastLookup.Value.id, lastLookup.Value.type);
            req.Success += res =>
            {
                beatmapSet.Value = res;
                if (lastLookup.Value.type == BeatmapSetLookupType.BeatmapId)
                    Header.HeaderContent.Picker.Beatmap.Value = Header.BeatmapSet.Value.Beatmaps.First(b => b.OnlineID == lastLookup.Value.id);
            };
            API.Queue(req);
        }

        private partial class CommentsSection : BeatmapSetLayoutSection
        {
            public readonly Bindable<APIBeatmapSet> BeatmapSet = new Bindable<APIBeatmapSet>();

            public CommentsSection()
            {
                CommentsContainer comments;

                Add(comments = new CommentsContainer());

                BeatmapSet.BindValueChanged(beatmapSet =>
                {
                    if (beatmapSet.NewValue?.OnlineID > 0)
                    {
                        Show();
                        comments.ShowComments(CommentableType.Beatmapset, beatmapSet.NewValue.OnlineID);
                    }
                    else
                    {
                        Hide();
                    }
                }, true);
            }
        }
    }
}
