// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using sus.Framework.Extensions.LocalisationExtensions;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Game.Resources.Localisation.Web;
using sus.Game.Users;

namespace sus.Game.Overlays.Rankings.Tables
{
    public partial class PerformanceTable : UserBasedTable
    {
        public PerformanceTable(int page, IReadOnlyList<UserStatistics> rankings)
            : base(page, rankings)
        {
        }

        protected override RankingsTableColumn[] CreateUniqueHeaders() => new[]
        {
            new RankingsTableColumn(RankingsStrings.StatPerformance, Anchor.Centre, new Dimension(GridSizeMode.AutoSize), true),
        };

        protected override Drawable[] CreateUniqueContent(UserStatistics item) => new Drawable[]
        {
            new RowText { Text = item.PP?.ToLocalisableString(@"N0") ?? default, }
        };
    }
}
