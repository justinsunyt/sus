// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Graphics;
using sus.Game.Graphics;
using sus.Game.Graphics.Sprites;
using sus.Game.Graphics.UserInterface;
using sus.Game.Screens.Ranking.Expanded.Accuracy;
using osuTK;

namespace sus.Game.Screens.Ranking.Expanded.Statistics
{
    public partial class StatisticCounter : RollingCounter<int>
    {
        protected override double RollingDuration => AccuracyCircle.ACCURACY_TRANSFORM_DURATION;

        protected override Easing RollingEasing => AccuracyCircle.ACCURACY_TRANSFORM_EASING;

        protected override OsuSpriteText CreateSpriteText() => base.CreateSpriteText().With(s =>
        {
            s.Font = OsuFont.Torus.With(size: 20, fixedWidth: true);
            s.Spacing = new Vector2(-2, 0);
        });
    }
}
