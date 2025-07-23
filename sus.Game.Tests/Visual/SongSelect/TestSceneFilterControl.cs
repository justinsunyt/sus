// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using NUnit.Framework;
using sus.Framework.Graphics;
using sus.Game.Screens.Select;

namespace sus.Game.Tests.Visual.SongSelect
{
    public partial class TestSceneFilterControl : OsuManualInputManagerTestScene
    {
        [SetUp]
        public void SetUp() => Schedule(() =>
        {
            Child = new FilterControl
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                RelativeSizeAxes = Axes.X,
                Height = FilterControl.HEIGHT,
            };
        });
    }
}
