// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Bindables;
using sus.Game.Screens.Play;

namespace sus.Game.Rulesets.UI
{
    public interface IFrameStableClock : IGameplayClock
    {
        IBindable<bool> IsCatchingUp { get; }

        /// <summary>
        /// Whether the frame stable clock is waiting on new frames to arrive to be able to progress time.
        /// </summary>
        IBindable<bool> WaitingOnFrames { get; }
    }
}
