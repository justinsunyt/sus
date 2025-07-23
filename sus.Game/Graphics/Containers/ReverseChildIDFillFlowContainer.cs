// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;

namespace sus.Game.Graphics.Containers
{
    public partial class ReverseChildIDFillFlowContainer<T> : FillFlowContainer<T> where T : Drawable
    {
        protected override int Compare(Drawable x, Drawable y) => CompareReverseChildID(x, y);
    }
}
