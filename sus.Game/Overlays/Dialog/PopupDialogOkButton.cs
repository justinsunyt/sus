// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Allocation;
using sus.Game.Graphics;
using sus.Game.Graphics.UserInterface;

namespace sus.Game.Overlays.Dialog
{
    public partial class PopupDialogOkButton : PopupDialogButton
    {
        [BackgroundDependencyLoader]
        private void load(OsuColour colours)
        {
            ButtonColour = colours.Pink;
        }

        public PopupDialogOkButton()
            : base(HoverSampleSet.DialogOk)
        {
        }
    }
}
