// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Graphics.Performance;
using sus.Game.Rulesets.Catch.Objects;
using sus.Game.Rulesets.Judgements;
using susTK.Graphics;

namespace sus.Game.Rulesets.Catch.UI
{
    public class HitExplosionEntry : LifetimeEntry
    {
        /// <summary>
        /// The judgement result that triggered this explosion.
        /// </summary>
        public JudgementResult JudgementResult { get; }

        /// <summary>
        /// The hitobject which triggered this explosion.
        /// </summary>
        public CatchHitObject HitObject => (CatchHitObject)JudgementResult.HitObject;

        /// <summary>
        /// The accent colour of the object caught.
        /// </summary>
        public Color4 ObjectColour { get; }

        /// <summary>
        /// The position at which the object was caught.
        /// </summary>
        public float Position { get; }

        public HitExplosionEntry(double startTime, JudgementResult judgementResult, Color4 objectColour, float position)
        {
            LifetimeStart = startTime;
            Position = position;
            JudgementResult = judgementResult;
            ObjectColour = objectColour;
        }
    }
}
