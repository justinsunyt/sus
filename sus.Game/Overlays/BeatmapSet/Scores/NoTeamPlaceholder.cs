// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Game.Graphics.Sprites;
using sus.Game.Localisation;
using susTK;

namespace sus.Game.Overlays.BeatmapSet.Scores
{
    public partial class NoTeamPlaceholder : Container
    {
        public NoTeamPlaceholder()
        {
            AutoSizeAxes = Axes.Both;
            Child = new FillFlowContainer
            {
                AutoSizeAxes = Axes.Both,
                Direction = FillDirection.Vertical,
                Spacing = new Vector2(0, 20),
                Children = new Drawable[]
                {
                    new OsuSpriteText
                    {
                        Anchor = Anchor.TopCentre,
                        Origin = Anchor.TopCentre,
                        Text = LeaderboardStrings.NoTeam,
                    },
                }
            };
        }
    }
}
