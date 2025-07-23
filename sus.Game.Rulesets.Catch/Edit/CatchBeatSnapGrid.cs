// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using sus.Framework.Graphics.Containers;
using sus.Game.Rulesets.Catch.UI;
using sus.Game.Rulesets.Edit;
using sus.Game.Screens.Edit.Compose.Components;

namespace sus.Game.Rulesets.Catch.Edit
{
    public partial class CatchBeatSnapGrid : BeatSnapGrid
    {
        protected override IEnumerable<Container> GetTargetContainers(HitObjectComposer composer) => new[]
        {
            ((CatchPlayfield)composer.Playfield).UnderlayElements
        };
    }
}
