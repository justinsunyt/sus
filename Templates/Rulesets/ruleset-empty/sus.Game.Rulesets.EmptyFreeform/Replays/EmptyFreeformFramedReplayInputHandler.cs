// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using System.Linq;
using sus.Framework.Input.StateChanges;
using sus.Framework.Utils;
using sus.Game.Replays;
using sus.Game.Rulesets.Replays;

namespace sus.Game.Rulesets.EmptyFreeform.Replays
{
    public class EmptyFreeformFramedReplayInputHandler : FramedReplayInputHandler<EmptyFreeformReplayFrame>
    {
        public EmptyFreeformFramedReplayInputHandler(Replay replay)
            : base(replay)
        {
        }

        protected override bool IsImportant(EmptyFreeformReplayFrame frame) => frame.Actions.Any();

        protected override void CollectReplayInputs(List<IInput> inputs)
        {
            var position = Interpolation.ValueAt(CurrentTime, StartFrame.Position, EndFrame.Position, StartFrame.Time, EndFrame.Time);

            inputs.Add(new MousePositionAbsoluteInput
            {
                Position = GamefieldToScreenSpace(position),
            });
            inputs.Add(new ReplayState<EmptyFreeformAction>
            {
                PressedActions = CurrentFrame?.Actions ?? new List<EmptyFreeformAction>(),
            });
        }
    }
}
