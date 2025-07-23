// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Localisation;

namespace sus.Game.Graphics.UserInterface
{
    /// <summary>
    /// A slider bar which displays a millisecond time value.
    /// </summary>
    public partial class TimeSlider : RoundedSliderBar<double>
    {
        public override LocalisableString TooltipText => $"{base.TooltipText} ms";
    }
}
