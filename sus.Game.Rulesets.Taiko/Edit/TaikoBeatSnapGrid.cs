// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using osu.Framework.Graphics.Containers;
using sus.Game.Rulesets.Edit;
using sus.Game.Rulesets.Taiko.UI;
using sus.Game.Screens.Edit.Compose.Components;

namespace sus.Game.Rulesets.Taiko.Edit
{
    public partial class TaikoBeatSnapGrid : BeatSnapGrid
    {
        protected override IEnumerable<Container> GetTargetContainers(HitObjectComposer composer) => new[]
        {
            ((TaikoPlayfield)composer.Playfield).UnderlayElements
        };
    }
}
