// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Allocation;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Game.Tournament.Models;

namespace sus.Game.Tournament.Screens
{
    public abstract partial class TournamentScreen : CompositeDrawable
    {
        public const double FADE_DELAY = 200;

        [Resolved]
        protected LadderInfo LadderInfo { get; private set; } = null!;

        protected TournamentScreen()
        {
            RelativeSizeAxes = Axes.Both;

            FillMode = FillMode.Fit;
            FillAspectRatio = 16 / 9f;
        }

        public override void Hide() => this.FadeOut(FADE_DELAY);

        public override void Show() => this.FadeIn(FADE_DELAY);
    }
}
