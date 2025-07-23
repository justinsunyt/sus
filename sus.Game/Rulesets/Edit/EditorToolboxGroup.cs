// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Graphics;
using sus.Game.Overlays;

namespace sus.Game.Rulesets.Edit
{
    public partial class EditorToolboxGroup : SettingsToolboxGroup
    {
        public EditorToolboxGroup(string title)
            : base(title)
        {
            RelativeSizeAxes = Axes.X;
            Width = 1;
        }
    }
}
