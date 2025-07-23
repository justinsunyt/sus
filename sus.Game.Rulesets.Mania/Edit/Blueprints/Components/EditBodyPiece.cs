// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Allocation;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Framework.Graphics.Shapes;
using sus.Game.Graphics;

namespace sus.Game.Rulesets.Mania.Edit.Blueprints.Components
{
    public partial class EditBodyPiece : CompositeDrawable
    {
        private readonly Container border;

        public EditBodyPiece()
        {
            InternalChildren = new Drawable[]
            {
                border = new Container
                {
                    RelativeSizeAxes = Axes.Both,
                    Masking = true,
                    BorderThickness = 3,
                    Child = new Box
                    {
                        RelativeSizeAxes = Axes.Both,
                        Alpha = 0,
                        AlwaysPresent = true,
                    },
                },
            };
        }

        [BackgroundDependencyLoader]
        private void load(OsuColour colours)
        {
            border.BorderColour = colours.YellowDarker;
        }
    }
}
