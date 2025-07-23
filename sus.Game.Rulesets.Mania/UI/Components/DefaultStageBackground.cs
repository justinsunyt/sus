// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Allocation;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Framework.Graphics.Shapes;
using susTK.Graphics;

namespace sus.Game.Rulesets.Mania.UI.Components
{
    public partial class DefaultStageBackground : CompositeDrawable
    {
        public DefaultStageBackground()
        {
            RelativeSizeAxes = Axes.Both;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            InternalChild = new Box
            {
                Name = "Background",
                RelativeSizeAxes = Axes.Both,
                Colour = Color4.Black
            };
        }
    }
}
