// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Graphics;
using sus.Game.Overlays.Profile.Sections.Kudsus;
using sus.Framework.Localisation;
using sus.Game.Resources.Localisation.Web;

namespace sus.Game.Overlays.Profile.Sections
{
    public partial class KudsusSection : ProfileSection
    {
        public override LocalisableString Title => UsersStrings.ShowExtraKudsusTitle;

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
