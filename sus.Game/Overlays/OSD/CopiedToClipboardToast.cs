// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Game.Localisation;

namespace sus.Game.Overlays.OSD
{
    public partial class CopiedToClipboardToast : Toast
    {
        public CopiedToClipboardToast()
            : base(CommonStrings.General, ToastStrings.CopiedToClipboard, "")
        {
        }
    }
}
