// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Allocation;
using sus.Framework.Graphics;
using sus.Game.Beatmaps;
using sus.Game.Overlays.Settings;

namespace sus.Game.Screens.Edit.Verify
{
    internal partial class InterpretationSection : EditorRoundedScreenSettingsSection
    {
        protected override string HeaderText => "Interpretation";

        [BackgroundDependencyLoader]
        private void load(VerifyScreen verify)
        {
            Flow.Add(new SettingsEnumDropdown<DifficultyRating>
            {
                Anchor = Anchor.CentreLeft,
                Origin = Anchor.CentreLeft,
                TooltipText = "Affects checks that depend on difficulty level",
                Current = verify.InterpretedDifficulty.GetBoundCopy()
            });
        }
    }
}
