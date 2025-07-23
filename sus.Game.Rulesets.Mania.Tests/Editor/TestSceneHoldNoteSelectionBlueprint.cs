// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Game.Beatmaps;
using sus.Game.Beatmaps.ControlPoints;
using sus.Game.Rulesets.Mania.Edit.Blueprints;
using sus.Game.Rulesets.Mania.Objects;
using sus.Game.Rulesets.Mania.Objects.Drawables;

namespace sus.Game.Rulesets.Mania.Tests.Editor
{
    public partial class TestSceneHoldNoteSelectionBlueprint : ManiaSelectionBlueprintTestScene
    {
        public TestSceneHoldNoteSelectionBlueprint()
            : base(4)
        {
            for (int i = 0; i < 4; i++)
            {
                var holdNote = new HoldNote
                {
                    Column = i,
                    StartTime = i * 100,
                    Duration = 500
                };
                holdNote.ApplyDefaults(new ControlPointInfo(), new BeatmapDifficulty());

                var drawableHitObject = new DrawableHoldNote(holdNote);
                Playfield.Add(drawableHitObject);
                AddBlueprint(new HoldNoteSelectionBlueprint(holdNote), drawableHitObject);
            }
        }
    }
}
