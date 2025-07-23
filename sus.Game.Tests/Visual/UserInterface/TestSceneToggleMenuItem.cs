// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Graphics;
using sus.Game.Graphics.UserInterface;

namespace sus.Game.Tests.Visual.UserInterface
{
    public partial class TestSceneToggleMenuItem : OsuTestScene
    {
        public TestSceneToggleMenuItem()
        {
            Add(new OsuMenu(Direction.Vertical, true)
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Items = new[]
                {
                    new ToggleMenuItem("First"),
                    new ToggleMenuItem("Second") { State = { Value = true } }
                }
            });
        }
    }
}
