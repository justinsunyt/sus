// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Graphics;
using sus.Game.Overlays.Profile.Sections.Kudsus;
using osu.Framework.Localisation;
using osu.Game.Resources.Localisation.Web;

namespace sus.Game.Overlays.Profile.Sections
{
    public partial class KudsusSection : ProfileSection
    {
        public override LocalisableString Title => UsersStrings.ShowExtraKudosuTitle;

        public override string Identifier => @"kudsus";

        public KudsusSection()
        {
            Children = new Drawable[]
            {
                new KudsusInfo(User),
                new PaginatedKudsusHistoryContainer(User),
            };
        }
    }
}
