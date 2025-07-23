// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Threading.Tasks;
using sus.Framework.Allocation;
using sus.Framework.Localisation;
using sus.Game.Localisation;
using sus.Game.Skinning;

namespace sus.Game.Overlays.Settings.Sections.Maintenance
{
    public partial class SkinSettings : SettingsSubsection
    {
        protected override LocalisableString Header => CommonStrings.Skins;

        private SettingsButton deleteSkinsButton = null!;

        [BackgroundDependencyLoader]
        private void load(SkinManager skins, IDialogOverlay? dialogOverlay)
        {
            Add(deleteSkinsButton = new DangerousSettingsButton
            {
                Text = MaintenanceSettingsStrings.DeleteAllSkins,
                Action = () =>
                {
                    dialogOverlay?.Push(new MassDeleteConfirmationDialog(() =>
                    {
                        deleteSkinsButton.Enabled.Value = false;
                        Task.Run(() => skins.Delete()).ContinueWith(_ => Schedule(() => deleteSkinsButton.Enabled.Value = true));
                    }, DeleteConfirmationContentStrings.Skins));
                }
            });
        }
    }
}
