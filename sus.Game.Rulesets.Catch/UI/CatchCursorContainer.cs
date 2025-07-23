// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Graphics;
using sus.Game.Rulesets.UI;

namespace sus.Game.Rulesets.Catch.UI
{
    public partial class CatchCursorContainer : GameplayCursorContainer
    {
        // Just hide the cursor.
        // The main goal here is to show that we have a cursor so the game never shows the global one.
        protected override Drawable CreateCursor() => Empty();
    }
}
