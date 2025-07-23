// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

#nullable disable

using sus.Framework.Allocation;
using sus.Framework.Audio;
using sus.Framework.Audio.Sample;
using sus.Framework.Extensions;
using sus.Framework.Graphics;
using sus.Framework.Utils;

namespace sus.Game.Graphics.UserInterface
{
    /// <summary>
    /// Adds hover sounds to a drawable.
    /// Does not draw anything.
    /// </summary>
    public partial class HoverSounds : HoverSampleDebounceComponent
    {
        private Sample sampleHover;

        protected readonly HoverSampleSet SampleSet;

        public HoverSounds(HoverSampleSet sampleSet = HoverSampleSet.Default)
        {
            SampleSet = sampleSet;
            RelativeSizeAxes = Axes.Both;
        }

        [BackgroundDependencyLoader]
        private void load(AudioManager audio)
        {
            sampleHover = audio.Samples.Get($@"UI/{SampleSet.GetDescription()}-hover")
                          ?? audio.Samples.Get($@"UI/{HoverSampleSet.Default.GetDescription()}-hover");
        }

        public override void PlayHoverSample()
        {
            sampleHover.Frequency.Value = 0.98 + RNG.NextDouble(0.04);
            sampleHover.Play();
        }
    }
}
