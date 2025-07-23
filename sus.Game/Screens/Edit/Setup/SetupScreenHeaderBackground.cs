// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Allocation;
using sus.Framework.Bindables;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Framework.Graphics.Shapes;
using sus.Game.Beatmaps;
using sus.Game.Beatmaps.Drawables;
using sus.Game.Graphics;
using sus.Game.Graphics.Containers;
using sus.Game.Localisation;

namespace sus.Game.Screens.Edit.Setup
{
    public partial class SetupScreenHeaderBackground : CompositeDrawable
    {
        [Resolved]
        private OsuColour colours { get; set; } = null!;

        [Resolved]
        private IBindable<WorkingBeatmap> working { get; set; } = null!;

        private readonly Container content;

        public SetupScreenHeaderBackground()
        {
            InternalChild = content = new Container
            {
                RelativeSizeAxes = Axes.Both,
                Masking = true,
                CornerRadius = 3.5f,
            };
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            UpdateBackground();
        }

        public void UpdateBackground()
        {
            LoadComponentAsync(new BeatmapBackgroundSprite(working.Value)
            {
                RelativeSizeAxes = Axes.Both,
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                FillMode = FillMode.Fill,
            }, background =>
            {
                if (background.Texture != null)
                    content.Child = background;
                else
                {
                    content.Children = new Drawable[]
                    {
                        new Box
                        {
                            Colour = colours.GreySeaFoamDarker,
                            RelativeSizeAxes = Axes.Both,
                        },
                        new OsuTextFlowContainer(t => t.Font = OsuFont.Default.With(size: 24))
                        {
                            Text = EditorSetupStrings.DragToSetBackground,
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            AutoSizeAxes = Axes.Both
                        }
                    };
                }

                background.FadeInFromZero(500);
            });
        }
    }
}
