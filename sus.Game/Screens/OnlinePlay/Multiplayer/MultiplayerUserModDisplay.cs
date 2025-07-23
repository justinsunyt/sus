// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Linq;
using sus.Framework.Allocation;
using sus.Framework.Extensions.ObjectExtensions;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Game.Online.Multiplayer;
using sus.Game.Online.Rooms;
using sus.Game.Rulesets;
using sus.Game.Rulesets.Mods;
using sus.Game.Screens.Play.HUD;

namespace sus.Game.Screens.OnlinePlay.Multiplayer
{
    public partial class MultiplayerUserModDisplay : CompositeDrawable
    {
        [Resolved]
        private MultiplayerClient client { get; set; } = null!;

        [Resolved]
        private RulesetStore rulesets { get; set; } = null!;

        private ModDisplay modDisplay = null!;

        public MultiplayerUserModDisplay()
        {
            AutoSizeAxes = Axes.Both;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            InternalChild = modDisplay = new ModDisplay();
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            client.RoomUpdated += onRoomUpdated;
            onRoomUpdated();
        }

        private void onRoomUpdated() => Scheduler.AddOnce(() =>
        {
            if (client.Room == null || client.LocalUser == null)
                return;

            MultiplayerPlaylistItem currentItem = client.Room.CurrentPlaylistItem;
            Ruleset ruleset = rulesets.GetRuleset(client.LocalUser.RulesetId ?? currentItem.RulesetID)!.CreateInstance();
            Mod[] userMods = client.LocalUser.Mods.Select(m => m.ToMod(ruleset)).ToArray();

            if (!userMods.SequenceEqual(modDisplay.Current.Value))
                modDisplay.Current.Value = userMods;
        });

        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);

            if (client.IsNotNull())
                client.RoomUpdated -= onRoomUpdated;
        }
    }
}
