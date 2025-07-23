// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Allocation;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Framework.Graphics.Shapes;
using sus.Game.Graphics;
using sus.Game.Rulesets.Mania.UI;
using sus.Game.Rulesets.UI.Scrolling;
using susTK;

namespace sus.Game.Rulesets.Mania.Edit.Blueprints.Components
{
    public partial class EditNotePiece : CompositeDrawable
    {
        private readonly Container border;
        private readonly Box box;

        [Resolved]
        private Column? column { get; set; }

        public EditNotePiece()
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
                box = new Box
                {
                    RelativeSizeAxes = Axes.X,
                    Height = 3,
                    Anchor = Anchor.BottomCentre,
                    Origin = Anchor.BottomCentre,
                },
            };
        }

        [BackgroundDependencyLoader]
        private void load(OsuColour colours)
        {
            border.BorderColour = colours.YellowDark;
            box.Colour = colours.YellowLight;
        }

        protected override void Update()
        {
            base.Update();

            if (column != null)
                Scale = new Vector2(1, column.ScrollingInfo.Direction.Value == ScrollingDirection.Down ? 1 : -1);
        }
    }
}
