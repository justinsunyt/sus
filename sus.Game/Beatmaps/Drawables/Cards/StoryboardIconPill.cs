// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Graphics.Sprites;
using sus.Framework.Localisation;
using sus.Game.Resources.Localisation.Web;

namespace sus.Game.Beatmaps.Drawables.Cards
{
    public partial class StoryboardIconPill : IconPill
    {
        public StoryboardIconPill()
            : base(FontAwesome.Solid.Image)
        {
        }

        public override LocalisableString TooltipText => BeatmapsetsStrings.ShowInfoStoryboard;
    }
}
