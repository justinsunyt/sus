// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using sus.Game.Rulesets.Catch.Replays;
using sus.Game.Rulesets.Replays;
using sus.Game.Rulesets.UI;
using sus.Game.Scoring;
using osuTK;

namespace sus.Game.Rulesets.Catch.UI
{
    public partial class CatchReplayRecorder : ReplayRecorder<CatchAction>
    {
        private readonly CatchPlayfield playfield;

        public CatchReplayRecorder(Score target, CatchPlayfield playfield)
            : base(target)
        {
            this.playfield = playfield;
        }

        protected override ReplayFrame HandleFrame(Vector2 mousePosition, List<CatchAction> actions, ReplayFrame previousFrame)
            => new CatchReplayFrame(Time.Current, playfield.Catcher.X, actions.Contains(CatchAction.Dash), previousFrame as CatchReplayFrame);
    }
}
