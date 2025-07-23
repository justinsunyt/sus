// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Allocation;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Shapes;
using sus.Game.Tests.Visual;
using susTK.Graphics;

namespace sus.Game.Rulesets.EmptyFreeform.Tests
{
    public partial class TestSceneOsuGame : OsuTestScene
    {
        [BackgroundDependencyLoader]
        private void load()
        {
            Children = new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = Color4.Black,
                },
            };

            AddGame(new OsuGame());
        }
    }
}
