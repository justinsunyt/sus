// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using MessagePack;
using sus.Game.Online.Multiplayer.Countdown;
using sus.Game.Online.Multiplayer.MatchTypes.TeamVersus;

namespace sus.Game.Online.Multiplayer
{
    /// <summary>
    /// A request from a user to perform an action specific to the current match type.
    /// </summary>
    [Serializable]
    [MessagePackObject]
    // IMPORTANT: Add rules to SignalRUnionWorkaroundResolver for new derived types.
    [Union(0, typeof(ChangeTeamRequest))]
    [Union(1, typeof(StartMatchCountdownRequest))]
    [Union(2, typeof(StopCountdownRequest))]
    public abstract class MatchUserRequest
    {
    }
}
