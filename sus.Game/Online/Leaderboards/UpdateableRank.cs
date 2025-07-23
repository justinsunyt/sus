// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Framework.Graphics.Transforms;
using sus.Game.Scoring;

namespace sus.Game.Online.Leaderboards
{
    public partial class UpdateableRank : ModelBackedDrawable<ScoreRank?>
    {
        protected override double TransformDuration => 600;
        protected override bool TransformImmediately => true;

        public ScoreRank? Rank
        {
            get => Model;
            set => Model = value;
        }

        public UpdateableRank(ScoreRank? rank = null)
        {
            Rank = rank;
        }

        protected override DelayedLoadWrapper CreateDelayedLoadWrapper(Func<Drawable> createContentFunc, double timeBeforeLoad)
        {
            return base.CreateDelayedLoadWrapper(createContentFunc, timeBeforeLoad)
                       .With(w =>
                       {
                           w.Anchor = Anchor.Centre;
                           w.Origin = Anchor.Centre;
                       });
        }

        protected override Drawable? CreateDrawable(ScoreRank? rank)
        {
            if (rank.HasValue)
            {
                return new DrawableRank(rank.Value)
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                };
            }

            return null;
        }

        protected override TransformSequence<Drawable> ApplyShowTransforms(Drawable drawable)
        {
            drawable.ScaleTo(1);
            return base.ApplyShowTransforms(drawable);
        }

        protected override TransformSequence<Drawable> ApplyHideTransforms(Drawable drawable)
        {
            drawable.ScaleTo(1.8f, TransformDuration, Easing.Out);

            return base.ApplyHideTransforms(drawable);
        }
    }
}
