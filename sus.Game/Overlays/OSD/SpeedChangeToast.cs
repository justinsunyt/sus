// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Game.Input;
using sus.Game.Input.Bindings;
using sus.Game.Localisation;

namespace sus.Game.Overlays.OSD
{
    public partial class SpeedChangeToast : Toast
    {
        public SpeedChangeToast(RealmKeyBindingStore keyBindingStore, double newSpeed)
            : base(ModSelectOverlayStrings.ModCustomisation, ToastStrings.SpeedChangedTo(newSpeed), keyBindingStore.GetBindingsStringFor(GlobalAction.IncreaseModSpeed) + " / " + keyBindingStore.GetBindingsStringFor(GlobalAction.DecreaseModSpeed))
        {
        }
    }
}
