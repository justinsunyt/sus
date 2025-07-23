// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using System.Linq;
using osu.Framework.Graphics.Containers;
using sus.Game.Rulesets.Edit;
using sus.Game.Rulesets.Mania.UI;
using sus.Game.Screens.Edit.Compose.Components;

namespace sus.Game.Rulesets.Mania.Edit
{
    public partial class ManiaBeatSnapGrid : BeatSnapGrid
    {
        protected override IEnumerable<Container> GetTargetContainers(HitObjectComposer composer)
        {
            return ((ManiaPlayfield)composer.Playfield)
                   .Stages
                   .SelectMany(stage => stage.Columns)
                   .Select(column => column.UnderlayElements);
        }
    }
}
