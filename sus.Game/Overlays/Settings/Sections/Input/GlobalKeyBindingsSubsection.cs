// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using System.Linq;
using sus.Framework.Localisation;
using sus.Game.Database;
using sus.Game.Input.Bindings;
using Realms;

namespace sus.Game.Overlays.Settings.Sections.Input
{
    public partial class GlobalKeyBindingsSubsection : KeyBindingsSubsection
    {
        protected override LocalisableString Header { get; }

        private readonly GlobalActionCategory category;

        public GlobalKeyBindingsSubsection(LocalisableString header, GlobalActionCategory category)
        {
            Header = header;
            this.category = category;
            Defaults = GlobalActionContainer.GetDefaultBindingsFor(category);
        }

        protected override IEnumerable<RealmKeyBinding> GetKeyBindings(Realm realm)
        {
            var bindings = realm.All<RealmKeyBinding>()
                                .Where(b => b.RulesetName == null && b.Variant == null)
                                .Detach();

            var actionsInSection = GlobalActionContainer.GetGlobalActionsFor(category).Cast<int>().ToHashSet();
            return bindings.Where(kb => actionsInSection.Contains(kb.ActionInt));
        }
    }
}
