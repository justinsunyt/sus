// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Allocation;
using sus.Framework.Graphics.Cursor;
using sus.Framework.Graphics.UserInterface;
using sus.Game.Graphics.UserInterface;

namespace sus.Game.Graphics.Cursor
{
    [Cached(typeof(OsuContextMenuContainer))]
    public partial class OsuContextMenuContainer : ContextMenuContainer
    {
        private OsuContextMenu menu = null!;

        protected override Menu CreateMenu() => menu = new OsuContextMenu(true);

        public void CloseMenu()
        {
            menu.Close();
        }
    }
}
