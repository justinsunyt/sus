// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Game.Rulesets;

namespace sus.Game.Screens.OnlinePlay.Lounge.Components
{
    public class FilterCriteria
    {
        public string SearchString = string.Empty;
        public RoomModeFilter Mode;
        public RoomStatusFilter? Status;
        public string Category = string.Empty;
        public RulesetInfo? Ruleset;
        public RoomPermissionsFilter Permissions;
    }
}
