// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using System.Linq;
using sus.Framework;
using sus.Framework.Allocation;
using sus.Framework.Input.Handlers;
using sus.Framework.Localisation;
using sus.Game.Configuration;
using sus.Game.Localisation;

namespace sus.Game.Overlays.Settings.Sections.Input
{
    /// <summary>
    /// Touch input settings subsection common to all touch handlers (even on different platforms).
    /// </summary>
    public partial class TouchSettings : SettingsSubsection
    {
        private readonly InputHandler handler;

        protected override LocalisableString Header => TouchSettingsStrings.Touch;

        public TouchSettings(InputHandler handler)
        {
            this.handler = handler;
        }

        [BackgroundDependencyLoader]
        private void load(OsuConfigManager susConfig)
        {
            if (!RuntimeInfo.IsMobile) // don't allow disabling the only input method (touch) on mobile.
            {
                Add(new SettingsCheckbox
                {
                    LabelText = CommonStrings.Enabled,
                    Current = handler.Enabled
                });
            }

            Add(new SettingsCheckbox
            {
                LabelText = TouchSettingsStrings.DisableTapsDuringGameplay,
                Current = susConfig.GetBindable<bool>(OsuSetting.TouchDisableGameplayTaps)
            });
        }

        public override IEnumerable<LocalisableString> FilterTerms => base.FilterTerms.Concat(new LocalisableString[] { @"touchscreen" });
    }
}
