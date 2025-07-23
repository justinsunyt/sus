// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Allocation;
using sus.Game.Graphics.UserInterface;
using sus.Game.Overlays;
using sus.Game.Screens.Edit.Timing;

namespace sus.Game.Screens.Edit.Compose.Components.Timeline
{
    public partial class TimelineButton : IconButton
    {
        [BackgroundDependencyLoader]
        private void load(OverlayColourProvider colourProvider)
        {
            // These are using colourProvider but don't match the design.
            // Just something to fit until someone implements the updated design.
            IconColour = colourProvider.Background1;
            IconHoverColour = colourProvider.Content2;

            HoverColour = colourProvider.Background1;
            FlashColour = colourProvider.Content2;

            Add(new RepeatingButtonBehaviour(this));
        }

        protected override HoverSounds CreateHoverSounds(HoverSampleSet sampleSet) => new HoverSounds(sampleSet);
    }
}
