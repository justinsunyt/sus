// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using sus.Game.Rulesets.Replays;
using sus.Game.Rulesets.Taiko.Replays;
using sus.Game.Rulesets.UI;
using sus.Game.Scoring;
using osuTK;

namespace sus.Game.Rulesets.Taiko.UI
{
    public partial class TaikoReplayRecorder : ReplayRecorder<TaikoAction>
    {
        public TaikoReplayRecorder(Score score)
            : base(score)
        {
        }

        protected override ReplayFrame HandleFrame(Vector2 mousePosition, List<TaikoAction> actions, ReplayFrame previousFrame) =>
            new TaikoReplayFrame(Time.Current, actions.ToArray());
    }
}
