// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.ComponentModel;
using sus.Framework.Input.Bindings;
using sus.Game.Rulesets.UI;

namespace sus.Game.Rulesets.EmptyScrolling
{
    public partial class EmptyScrollingInputManager : RulesetInputManager<EmptyScrollingAction>
    {
        public EmptyScrollingInputManager(RulesetInfo ruleset)
            : base(ruleset, 0, SimultaneousBindingMode.Unique)
        {
        }
    }

    public enum EmptyScrollingAction
    {
        [Description("Button 1")]
        Button1,

        [Description("Button 2")]
        Button2,
    }
}
