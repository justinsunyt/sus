// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input;
using osu.Framework.Input.Bindings;
using sus.Game.Graphics.UserInterface;
using sus.Game.Input.Bindings;

namespace sus.Game.Tests.Visual.UserInterface
{
    public partial class TestSceneHotkeyDisplay : ThemeComparisonTestScene
    {
        protected override Drawable CreateContent() => new FillFlowContainer
        {
            Anchor = Anchor.Centre,
            Origin = Anchor.Centre,
            Direction = FillDirection.Vertical,
            Children = new[]
            {
                new HotkeyDisplay { Hotkey = new Hotkey(new KeyCombination(InputKey.MouseLeft)) },
                new HotkeyDisplay { Hotkey = new Hotkey(GlobalAction.EditorDecreaseDistanceSpacing) },
                new HotkeyDisplay { Hotkey = new Hotkey(PlatformAction.Save) },
            }
        };
    }
}
