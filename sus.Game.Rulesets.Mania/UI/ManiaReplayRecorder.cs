// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using sus.Game.Rulesets.Mania.Replays;
using sus.Game.Rulesets.Replays;
using sus.Game.Rulesets.UI;
using sus.Game.Scoring;
using susTK;

namespace sus.Game.Rulesets.Mania.UI
{
    public partial class ManiaReplayRecorder : ReplayRecorder<ManiaAction>
    {
        public ManiaReplayRecorder(Score score)
            : base(score)
        {
        }

        protected override ReplayFrame HandleFrame(Vector2 mousePosition, List<ManiaAction> actions, ReplayFrame previousFrame)
            => new ManiaReplayFrame(Time.Current, actions.ToArray());
    }
}
