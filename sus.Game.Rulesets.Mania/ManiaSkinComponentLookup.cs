// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Game.Skinning;

namespace sus.Game.Rulesets.Mania
{
    public class ManiaSkinComponentLookup : SkinComponentLookup<ManiaSkinComponents>
    {
        /// <summary>
        /// Creates a new <see cref="ManiaSkinComponentLookup"/>.
        /// </summary>
        /// <param name="component">The component.</param>
        public ManiaSkinComponentLookup(ManiaSkinComponents component)
            : base(component)
        {
        }
    }

    public enum ManiaSkinComponents
    {
        ColumnBackground,
        HitTarget,
        KeyArea,
        Note,
        HoldNoteHead,
        HoldNoteTail,
        HoldNoteBody,
        HitExplosion,
        StageBackground,
        StageForeground,
        BarLine
    }
}
