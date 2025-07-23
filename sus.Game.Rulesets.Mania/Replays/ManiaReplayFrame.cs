// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using System.Linq;
using sus.Game.Beatmaps;
using sus.Game.Replays.Legacy;
using sus.Game.Rulesets.Replays;
using sus.Game.Rulesets.Replays.Types;

namespace sus.Game.Rulesets.Mania.Replays
{
    public class ManiaReplayFrame : ReplayFrame, IConvertibleReplayFrame
    {
        public List<ManiaAction> Actions = new List<ManiaAction>();

        public ManiaReplayFrame()
        {
        }

        public ManiaReplayFrame(double time, params ManiaAction[] actions)
            : base(time)
        {
            Actions.AddRange(actions);
        }

        public void FromLegacy(LegacyReplayFrame legacyFrame, IBeatmap beatmap, ReplayFrame? lastFrame = null)
        {
            var action = ManiaAction.Key1;
            int activeColumns = (int)(legacyFrame.MouseX ?? 0);

            while (activeColumns > 0)
            {
                if ((activeColumns & 1) > 0)
                    Actions.Add(action);

                action++;
                activeColumns >>= 1;
            }
        }

        public LegacyReplayFrame ToLegacy(IBeatmap beatmap)
        {
            int keys = 0;

            foreach (var action in Actions)
                keys |= 1 << (int)action;

            return new LegacyReplayFrame(Time, keys, null, ReplayButtonState.None);
        }

        public override bool IsEquivalentTo(ReplayFrame other)
            => other is ManiaReplayFrame maniaFrame && Time == maniaFrame.Time && Actions.SequenceEqual(maniaFrame.Actions);
    }
}
