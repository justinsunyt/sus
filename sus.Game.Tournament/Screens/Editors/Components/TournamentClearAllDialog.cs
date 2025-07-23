// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using sus.Framework.Graphics.Sprites;
using sus.Game.Overlays.Dialog;

namespace sus.Game.Tournament.Screens.Editors.Components
{
    public partial class TournamentClearAllDialog : DangerousActionDialog
    {
        public TournamentClearAllDialog(Action action)
        {
            HeaderText = @"Clear all?";
            Icon = FontAwesome.Solid.Trash;
            DangerousAction = action;
        }
    }
}
