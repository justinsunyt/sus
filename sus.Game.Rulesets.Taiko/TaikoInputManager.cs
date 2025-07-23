// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.ComponentModel;
using sus.Framework.Allocation;
using sus.Framework.Input.Bindings;
using sus.Game.Rulesets.UI;

namespace sus.Game.Rulesets.Taiko
{
    [Cached] // Used for touch input, see DrumTouchInputArea.
    public partial class TaikoInputManager : RulesetInputManager<TaikoAction>
    {
        public TaikoInputManager(RulesetInfo ruleset)
            : base(ruleset, 0, SimultaneousBindingMode.Unique)
        {
        }
    }

    public enum TaikoAction
    {
        [Description("Left (rim)")]
        LeftRim,

        [Description("Left (centre)")]
        LeftCentre,

        [Description("Right (centre)")]
        RightCentre,

        [Description("Right (rim)")]
        RightRim
    }
}
