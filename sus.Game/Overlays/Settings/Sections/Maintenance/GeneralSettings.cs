// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Linq;
using System.Threading.Tasks;
using sus.Framework.Allocation;
using sus.Framework.Graphics;
using sus.Framework.Localisation;
using sus.Framework.Platform;
using sus.Framework.Screens;
using sus.Game.Localisation;
using sus.Game.Screens;
using sus.Game.Screens.Import;
using sus.Game.Screens.Utility;

namespace sus.Game.Overlays.Settings.Sections.Maintenance
{
    public partial class GeneralSettings : SettingsSubsection
    {
        protected override LocalisableString Header => CommonStrings.General;

        private ISystemFileSelector? selector;

        [BackgroundDependencyLoader]
        private void load(OsuGameBase game, GameHost host, IPerformFromScreenRunner? performer)
        {
            if ((selector = host.CreateSystemFileSelector(game.HandledExtensions.ToArray())) != null)
                selector.Selected += f => Task.Run(() => game.Import(f.FullName));

            AddRange(new Drawable[]
            {
                new SettingsButton
                {
                    Text = DebugSettingsStrings.ImportFiles,
                    Action = () =>
                    {
                        if (selector != null)
                            selector.Present();
                        else
                            performer?.PerformFromScreen(menu => menu.Push(new FileImportScreen()));
                    },
                },
                new SettingsButton
                {
                    Text = DebugSettingsStrings.RunLatencyCertifier,
                    Action = () => performer?.PerformFromScreen(menu => menu.Push(new LatencyCertifierScreen()))
                }
            });
        }
    }
}
