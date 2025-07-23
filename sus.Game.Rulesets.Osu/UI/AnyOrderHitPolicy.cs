// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Game.Rulesets.Objects.Drawables;
using sus.Game.Rulesets.Scoring;
using sus.Game.Rulesets.UI;

namespace sus.Game.Rulesets.Osu.UI
{
    /// <summary>
    /// An <see cref="IHitPolicy"/> which allows hitobjects to be hit in any order.
    /// </summary>
    public class AnyOrderHitPolicy : IHitPolicy
    {
        public IHitObjectContainer HitObjectContainer { get; set; } = null!;

        public ClickAction CheckHittable(DrawableHitObject hitObject, double time, HitResult result) => ClickAction.Hit;

        public void HandleHit(DrawableHitObject hitObject)
        {
        }
    }
}
