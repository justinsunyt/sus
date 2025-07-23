// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using NUnit.Framework;
using sus.Framework.Allocation;
using sus.Framework.Graphics;
using sus.Game.Screens.Edit.Components;
using susTK;

namespace sus.Game.Tests.Visual.Editing
{
    [TestFixture]
    public partial class TestScenePlaybackControl : EditorClockTestScene
    {
        [BackgroundDependencyLoader]
        private void load()
        {
            Child = new PlaybackControl
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Size = new Vector2(200, 100)
            };
        }
    }
}
