// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Game.Rulesets.Objects;
using sus.Game.Rulesets.Objects.Drawables;
using sus.Game.Rulesets.Scoring;
using sus.Game.Rulesets.UI;

namespace sus.Game.Rulesets.Osu.UI
{
    public interface IHitPolicy
    {
        /// <summary>
        /// The <see cref="IHitObjectContainer"/> containing the <see cref="DrawableHitObject"/>s which this <see cref="IHitPolicy"/> applies to.
        /// </summary>
        IHitObjectContainer HitObjectContainer { set; }

        /// <summary>
        /// Determines whether a <see cref="DrawableHitObject"/> can be hit at a point in time.
        /// </summary>
        /// <param name="hitObject">The <see cref="DrawableHitObject"/> to check.</param>
        /// <param name="time">The time to check.</param>
        /// <param name="result">The result that the object would be judged with if hit.</param>
        /// <returns>Whether <paramref name="hitObject"/> can be hit at the given <paramref name="time"/>.</returns>
        ClickAction CheckHittable(DrawableHitObject hitObject, double time, HitResult result);

        /// <summary>
        /// Handles a <see cref="HitObject"/> being hit.
        /// </summary>
        /// <param name="hitObject">The <see cref="HitObject"/> that was hit.</param>
        void HandleHit(DrawableHitObject hitObject);
    }
}
