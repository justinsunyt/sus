// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Game.Rulesets.Catch.Judgements;
using sus.Game.Rulesets.Judgements;

namespace sus.Game.Rulesets.Catch.Objects
{
    public class Droplet : PalpableCatchHitObject
    {
        public override Judgement CreateJudgement() => new CatchDropletJudgement();
    }
}
