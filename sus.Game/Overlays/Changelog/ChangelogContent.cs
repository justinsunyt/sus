// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Game.Online.API.Requests.Responses;
using System;

namespace sus.Game.Overlays.Changelog
{
    public partial class ChangelogContent : FillFlowContainer
    {
        public Action<APIChangelogBuild>? BuildSelected;

        public void SelectBuild(APIChangelogBuild build) => BuildSelected?.Invoke(build);

        public ChangelogContent()
        {
            RelativeSizeAxes = Axes.X;
            AutoSizeAxes = Axes.Y;
            Direction = FillDirection.Vertical;
        }
    }
}
