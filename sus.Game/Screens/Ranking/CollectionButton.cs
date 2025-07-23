// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Linq;
using sus.Framework.Allocation;
using sus.Framework.Bindables;
using sus.Framework.Extensions;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Cursor;
using sus.Framework.Graphics.Sprites;
using sus.Framework.Graphics.UserInterface;
using sus.Game.Beatmaps;
using sus.Game.Collections;
using sus.Game.Database;
using sus.Game.Graphics;
using sus.Game.Graphics.UserInterface;
using susTK;
using Realms;

namespace sus.Game.Screens.Ranking
{
    public partial class CollectionButton : GrayButton, IHasPopover
    {
        private readonly BeatmapInfo beatmapInfo;
        private readonly Bindable<bool> isInAnyCollection;

        [Resolved]
        private RealmAccess realmAccess { get; set; } = null!;

        private IDisposable? collectionSubscription;

        [Resolved]
        private OsuColour colours { get; set; } = null!;

        public CollectionButton(BeatmapInfo beatmapInfo)
            : base(FontAwesome.Solid.Book)
        {
            this.beatmapInfo = beatmapInfo;
            isInAnyCollection = new Bindable<bool>(false);

            Size = new Vector2(75, 30);

            TooltipText = "collections";
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            Action = this.ShowPopover;
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            collectionSubscription = realmAccess.RegisterForNotifications(r => r.All<BeatmapCollection>(), collectionsChanged);

            isInAnyCollection.BindValueChanged(_ => updateState(), true);
        }

        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);

            collectionSubscription?.Dispose();
        }

        private void collectionsChanged(IRealmCollection<BeatmapCollection> sender, ChangeSet? changes)
        {
            isInAnyCollection.Value = sender.AsEnumerable().Any(c => c.BeatmapMD5Hashes.Contains(beatmapInfo.MD5Hash));
        }

        private void updateState()
        {
            Background.FadeColour(isInAnyCollection.Value ? colours.Green : colours.Gray4, 500, Easing.InOutExpo);
        }

        public Popover GetPopover() => new CollectionPopover(beatmapInfo);
    }
}
