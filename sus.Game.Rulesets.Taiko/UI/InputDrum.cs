// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using sus.Game.Rulesets.Taiko.Skinning.Default;
using sus.Game.Skinning;

namespace sus.Game.Rulesets.Taiko.UI
{
    /// <summary>
    /// A component of the playfield that captures input and displays input as a drum.
    /// </summary>
    internal partial class InputDrum : Container
    {
        [BackgroundDependencyLoader]
        private void load()
        {
            Children = new Drawable[]
            {
                new SkinnableDrawable(new TaikoSkinComponentLookup(TaikoSkinComponents.InputDrum), _ => new DefaultInputDrum())
                {
                    RelativeSizeAxes = Axes.Both,
                },
            };
        }
    }
}
