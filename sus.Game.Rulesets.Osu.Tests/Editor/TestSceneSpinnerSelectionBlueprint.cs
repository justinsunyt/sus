// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Game.Beatmaps;
using sus.Game.Beatmaps.ControlPoints;
using sus.Game.Rulesets.Osu.Edit.Blueprints.Spinners;
using sus.Game.Rulesets.Osu.Objects;
using sus.Game.Rulesets.Osu.Objects.Drawables;
using sus.Game.Tests.Visual;
using susTK;

namespace sus.Game.Rulesets.Osu.Tests.Editor
{
    public partial class TestSceneSpinnerSelectionBlueprint : SelectionBlueprintTestScene
    {
        public TestSceneSpinnerSelectionBlueprint()
        {
            var spinner = new Spinner
            {
                Position = new Vector2(256, 256),
                StartTime = -1000,
                EndTime = 2000
            };

            spinner.ApplyDefaults(new ControlPointInfo(), new BeatmapDifficulty { CircleSize = 2 });

            DrawableSpinner drawableSpinner;

            Add(new Container
            {
                RelativeSizeAxes = Axes.Both,
                Size = new Vector2(0.5f),
                Child = drawableSpinner = new DrawableSpinner(spinner)
            });

            AddBlueprint(new SpinnerSelectionBlueprint(spinner) { Size = new Vector2(0.5f) }, drawableSpinner);
        }
    }
}
