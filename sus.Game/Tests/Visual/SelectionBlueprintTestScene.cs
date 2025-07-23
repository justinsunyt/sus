// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Allocation;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Framework.Timing;
using sus.Game.Rulesets.Edit;
using sus.Game.Rulesets.Objects.Drawables;
using sus.Game.Screens.Edit;

namespace sus.Game.Tests.Visual
{
    public abstract partial class SelectionBlueprintTestScene : OsuManualInputManagerTestScene
    {
        [Cached]
        private readonly EditorClock editorClock = new EditorClock();

        protected override Container<Drawable> Content => content;
        private readonly Container content;

        protected SelectionBlueprintTestScene()
        {
            base.Content.AddRange(new Drawable[]
            {
                editorClock,
                content = new Container
                {
                    Clock = new FramedClock(new StopwatchClock()),
                    RelativeSizeAxes = Axes.Both
                }
            });
        }

        protected void AddBlueprint(HitObjectSelectionBlueprint blueprint, DrawableHitObject? drawableObject = null)
        {
            Add(blueprint.With(d =>
            {
                d.DrawableObject = drawableObject;
                d.Depth = float.MinValue;
                d.Select();
            }));
        }
    }
}
