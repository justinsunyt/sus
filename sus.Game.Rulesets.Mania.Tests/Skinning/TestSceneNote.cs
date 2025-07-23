// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Game.Beatmaps;
using sus.Game.Beatmaps.ControlPoints;
using sus.Game.Rulesets.Mania.Objects;
using sus.Game.Rulesets.Mania.Objects.Drawables;

namespace sus.Game.Rulesets.Mania.Tests.Skinning
{
    public partial class TestSceneNote : ManiaHitObjectTestScene
    {
        protected override DrawableManiaHitObject CreateHitObject()
        {
            var note = new Note();
            note.ApplyDefaults(new ControlPointInfo(), new BeatmapDifficulty());

            return new DrawableNote(note);
        }
    }
}
