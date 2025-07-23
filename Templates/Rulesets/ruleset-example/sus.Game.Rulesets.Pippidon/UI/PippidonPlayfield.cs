// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Allocation;
using sus.Framework.Graphics;
using sus.Game.Rulesets.UI;

namespace sus.Game.Rulesets.Pippidon.UI
{
    [Cached]
    public partial class PippidonPlayfield : Playfield
    {
        protected override GameplayCursorContainer CreateCursor() => new PippidonCursorContainer();

        [BackgroundDependencyLoader]
        private void load()
        {
            AddRangeInternal(new Drawable[]
            {
                HitObjectContainer,
            });
        }
    }
}
