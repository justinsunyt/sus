// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Allocation;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Framework.Graphics.Cursor;
using sus.Framework.Testing;
using sus.Game.Graphics.Cursor;
using sus.Game.Overlays;

namespace sus.Game.Tests.Visual.SongSelectV2
{
    public abstract partial class SongSelectComponentsTestScene : OsuManualInputManagerTestScene
    {
        [Cached]
        protected readonly OverlayColourProvider ColourProvider = new OverlayColourProvider(OverlayColourScheme.Aquamarine);

        protected override Container<Drawable> Content { get; } = new OsuContextMenuContainer
        {
            RelativeSizeAxes = Axes.X,
            AutoSizeAxes = Axes.Y,
        };

        private Container? resizeContainer;

        protected virtual Anchor ComponentAnchor => Anchor.TopLeft;
        protected virtual float InitialRelativeWidth => 0.5f;

        [BackgroundDependencyLoader]
        private void load()
        {
            base.Content.Child = new PopoverContainer
            {
                RelativeSizeAxes = Axes.Both,
                Child = resizeContainer = new Container
                {
                    Anchor = ComponentAnchor,
                    Origin = ComponentAnchor,
                    RelativeSizeAxes = Axes.X,
                    AutoSizeAxes = Axes.Y,
                    Width = InitialRelativeWidth,
                    Child = Content
                }
            };

            AddSliderStep("change relative width", 0, 1f, InitialRelativeWidth, v =>
            {
                if (resizeContainer != null)
                    resizeContainer.Width = v;
            });
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            ChangeBackgroundColour(ColourProvider.Background6);
        }

        [SetUpSteps]
        public virtual void SetUpSteps()
        {
            AddStep("reset dependencies", () =>
            {
                Beatmap.SetDefault();
                SelectedMods.SetDefault();
            });
        }
    }
}
