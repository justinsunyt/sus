// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Allocation;
using sus.Framework.Graphics;
using sus.Framework.Localisation;
using sus.Game.Graphics;
using sus.Game.Graphics.Sprites;
using sus.Game.Rulesets.Scoring;

namespace sus.Game.Screens.Play.HUD
{
    public partial class DefaultComboCounter : ComboCounter
    {
        [BackgroundDependencyLoader]
        private void load(OsuColour colours, ScoreProcessor scoreProcessor)
        {
            Colour = colours.BlueLighter;
            Current.BindTo(scoreProcessor.Combo);
        }

        protected override OsuSpriteText CreateSpriteText()
            => base.CreateSpriteText().With(s => s.Font = s.Font.With(size: 20f));

        protected override LocalisableString FormatCount(int count)
        {
            return $@"{count}x";
        }
    }
}
