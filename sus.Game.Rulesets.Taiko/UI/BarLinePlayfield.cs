// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using sus.Game.Rulesets.Taiko.Objects;
using sus.Game.Rulesets.Taiko.Objects.Drawables;
using sus.Game.Rulesets.UI.Scrolling;

namespace sus.Game.Rulesets.Taiko.UI
{
    public partial class BarLinePlayfield : ScrollingPlayfield
    {
        [BackgroundDependencyLoader]
        private void load()
        {
            RegisterPool<BarLine, DrawableBarLine>(15);
        }
    }
}
