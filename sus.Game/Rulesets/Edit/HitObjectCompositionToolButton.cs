// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using sus.Game.Rulesets.Edit.Tools;
using sus.Game.Screens.Edit.Components.RadioButtons;

namespace sus.Game.Rulesets.Edit
{
    public class HitObjectCompositionToolButton : RadioButton
    {
        public CompositionTool Tool { get; }

        public HitObjectCompositionToolButton(CompositionTool tool, Action? action)
            : base(tool.Name, action, tool.CreateIcon)
        {
            Tool = tool;

            TooltipText = tool.TooltipText;
        }
    }
}
