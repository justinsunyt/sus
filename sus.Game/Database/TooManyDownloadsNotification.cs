// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Allocation;
using sus.Framework.Graphics.Sprites;
using sus.Game.Graphics;
using sus.Game.Overlays.Notifications;
using sus.Game.Resources.Localisation.Web;

namespace sus.Game.Database
{
    public partial class TooManyDownloadsNotification : SimpleNotification
    {
        public TooManyDownloadsNotification()
        {
            Text = BeatmapsetsStrings.DownloadLimitExceeded;
            Icon = FontAwesome.Solid.ExclamationCircle;
        }

        [BackgroundDependencyLoader]
        private void load(OsuColour colours)
        {
            IconContent.Colour = colours.RedDark;
        }
    }
}
