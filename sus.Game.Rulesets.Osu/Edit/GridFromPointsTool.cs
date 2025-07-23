// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using sus.Game.Rulesets.Edit;
using sus.Game.Rulesets.Edit.Tools;
using sus.Game.Rulesets.Osu.Edit.Blueprints;

namespace sus.Game.Rulesets.Osu.Edit
{
    public partial class GridFromPointsTool : CompositionTool
    {
        public GridFromPointsTool()
            : base("Grid")
        {
            TooltipText = """
                          Left click to set the origin.
                          Left click again to set the spacing and rotation.
                          Right click to reset to default.
                          Click and drag to set the origin, spacing and rotation.
                          """;
        }

        public override Drawable CreateIcon() => new SpriteIcon { Icon = FontAwesome.Solid.DraftingCompass };

        public override PlacementBlueprint CreatePlacementBlueprint() => new GridPlacementBlueprint();
    }
}
