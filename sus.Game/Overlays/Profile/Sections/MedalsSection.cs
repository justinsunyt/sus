// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Localisation;
using sus.Game.Resources.Localisation.Web;

namespace sus.Game.Overlays.Profile.Sections
{
    public partial class MedalsSection : ProfileSection
    {
        public override LocalisableString Title => UsersStrings.ShowExtraMedalsTitle;

        public override string Identifier => @"medals";
    }
}
