// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using sus.Framework.Allocation;
using sus.Framework.Bindables;
using sus.Framework.Extensions.ObjectExtensions;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Sprites;
using sus.Framework.Screens;
using sus.Game.Online.API;
using sus.Game.Online.Metadata;
using sus.Game.Online.Multiplayer;
using sus.Game.Online.Notifications.WebSocket;
using sus.Game.Online.Spectator;
using sus.Game.Overlays;
using sus.Game.Overlays.Notifications;
using sus.Game.Screens.OnlinePlay;

namespace sus.Game.Online
{
    /// <summary>
    /// Handles various scenarios where connection is lost and we need to let the user know what and why.
    /// </summary>
    public partial class OnlineStatusNotifier : Component
    {
        private readonly Func<IScreen> getCurrentScreen;

        private INotificationsClient notificationsClient = null!;

        [Resolved]
        private MultiplayerClient multiplayerClient { get; set; } = null!;

        [Resolved]
        private SpectatorClient spectatorClient { get; set; } = null!;

        [Resolved]
        private MetadataClient metadataClient { get; set; } = null!;

        [Resolved]
        private INotificationOverlay? notificationOverlay { get; set; }

        private IBindable<APIState> apiState = null!;
        private IBindable<bool> multiplayerState = null!;
        private IBindable<bool> spectatorState = null!;

        /// <summary>
        /// This flag will be set to <c>true</c> when the user has been notified so we don't show more than one notification.
        /// </summary>
        private bool userNotified;

        public OnlineStatusNotifier(Func<IScreen> getCurrentScreen)
        {
            this.getCurrentScreen = getCurrentScreen;
        }

        [BackgroundDependencyLoader]
        private void load(IAPIProvider api)
        {
            apiState = api.State.GetBoundCopy();
            notificationsClient = api.NotificationsClient;
            multiplayerState = multiplayerClient.IsConnected.GetBoundCopy();
            spectatorState = spectatorClient.IsConnected.GetBoundCopy();

            notificationsClient.MessageReceived += notifyAboutForcedDisconnection;
            multiplayerClient.Disconnecting += notifyAboutForcedDisconnection;
            spectatorClient.Disconnecting += notifyAboutForcedDisconnection;
            metadataClient.Disconnecting += notifyAboutForcedDisconnection;
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            apiState.BindValueChanged(state =>
            {
                if (state.NewValue == APIState.Online)
                {
                    userNotified = false;
                    return;
                }

                if (userNotified) return;

                if (state.NewValue == APIState.Offline && getCurrentScreen() is OnlinePlayScreen)
                {
                    userNotified = true;
                    notificationOverlay?.Post(new SimpleErrorNotification
                    {
                        Icon = FontAwesome.Solid.ExclamationCircle,
                        Text = "Connection to API was lost. Can't continue with online play."
                    });
                }
            });

            multiplayerState.BindValueChanged(connected => Schedule(() =>
            {
                if (connected.NewValue)
                {
                    userNotified = false;
                    return;
                }

                if (userNotified) return;

                if (multiplayerClient.Room != null)
                {
                    userNotified = true;
                    notificationOverlay?.Post(new SimpleErrorNotification
                    {
                        Icon = FontAwesome.Solid.ExclamationCircle,
                        Text = "Connection to the multiplayer server was lost. Exiting multiplayer."
                    });
                }
            }));

            spectatorState.BindValueChanged(_ =>
            {
                // TODO: handle spectator server failure somehow?
            });
        }

        private void notifyAboutForcedDisconnection()
        {
            if (userNotified) return;

            userNotified = true;
            notificationOverlay?.Post(new SimpleErrorNotification
            {
                Icon = FontAwesome.Solid.ExclamationCircle,
                Text = "You have been logged out on this device due to a login to your account on another device."
            });
        }

        private void notifyAboutForcedDisconnection(SocketMessage obj)
        {
            if (obj.Event != @"logout") return;

            if (userNotified) return;

            userNotified = true;
            notificationOverlay?.Post(new SimpleErrorNotification
            {
                Icon = FontAwesome.Solid.ExclamationCircle,
                Text = "You have been logged out due to a change to your account. Please log in again."
            });
        }

        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);

            if (notificationsClient.IsNotNull())
                notificationsClient.MessageReceived -= notifyAboutForcedDisconnection;

            if (spectatorClient.IsNotNull())
                spectatorClient.Disconnecting -= notifyAboutForcedDisconnection;

            if (multiplayerClient.IsNotNull())
                multiplayerClient.Disconnecting -= notifyAboutForcedDisconnection;

            if (metadataClient.IsNotNull())
                metadataClient.Disconnecting -= notifyAboutForcedDisconnection;
        }
    }
}
