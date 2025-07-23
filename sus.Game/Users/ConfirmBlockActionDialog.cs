// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using sus.Framework.Allocation;
using sus.Framework.Graphics.Sprites;
using sus.Framework.Localisation;
using sus.Game.Localisation;
using sus.Game.Online.API;
using sus.Game.Online.API.Requests;
using sus.Game.Online.API.Requests.Responses;
using sus.Game.Overlays;
using sus.Game.Overlays.Dialog;
using sus.Game.Overlays.Notifications;

namespace sus.Game.Users
{
    public partial class ConfirmBlockActionDialog : DangerousActionDialog
    {
        private readonly APIUser user;

        [Resolved]
        private IAPIProvider api { get; set; } = null!;

        [Resolved]
        private NotificationOverlay? notifications { get; set; }

        private ConfirmBlockActionDialog(APIUser user, LocalisableString text, Action<ConfirmBlockActionDialog> action)
        {
            this.user = user;
            BodyText = text;
            DangerousAction = () => action(this);
        }

        public static ConfirmBlockActionDialog Block(APIUser user) => new ConfirmBlockActionDialog(user, ContextMenuStrings.ConfirmBlockUser(user.Username), d => d.toggleBlock(true));
        public static ConfirmBlockActionDialog Unblock(APIUser user) => new ConfirmBlockActionDialog(user, ContextMenuStrings.ConfirmUnblockUser(user.Username), d => d.toggleBlock(false));

        private void toggleBlock(bool block)
        {
            APIRequest req = block ? new BlockUserRequest(user.OnlineID) : new UnblockUserRequest(user.OnlineID);

            req.Success += () =>
            {
                api.UpdateLocalBlocks();
            };

            req.Failure += e =>
            {
                notifications?.Post(new SimpleNotification
                {
                    Text = e.Message,
                    Icon = FontAwesome.Solid.Times,
                });
            };

            api.Queue(req);
        }
    }
}
