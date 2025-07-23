// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using sus.Game.Rulesets.Osu.Replays;
using sus.Game.Rulesets.Replays;
using sus.Game.Rulesets.UI;
using sus.Game.Scoring;
using osuTK;

namespace sus.Game.Rulesets.Osu.UI
{
    public partial class OsuReplayRecorder : ReplayRecorder<OsuAction>
    {
        public OsuReplayRecorder(Score score)
            : base(score)
        {
        }

        protected override ReplayFrame HandleFrame(Vector2 mousePosition, List<OsuAction> actions, ReplayFrame previousFrame)
            => new OsuReplayFrame(Time.Current, mousePosition, actions.ToArray());
    }
}
