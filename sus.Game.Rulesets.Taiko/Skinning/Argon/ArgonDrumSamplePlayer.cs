// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Allocation;
using sus.Game.Audio;
using sus.Game.Rulesets.Taiko.Objects;
using sus.Game.Rulesets.Taiko.UI;
using sus.Game.Rulesets.UI;
using sus.Game.Skinning;

namespace sus.Game.Rulesets.Taiko.Skinning.Argon
{
    internal partial class ArgonDrumSamplePlayer : DrumSamplePlayer
    {
        private ArgonFlourishTriggerSource argonFlourishTrigger = null!;

        [BackgroundDependencyLoader]
        private void load(Playfield playfield, IPooledSampleProvider sampleProvider)
        {
            var hitObjectContainer = playfield.HitObjectContainer;

            // Warm up pools for non-standard samples.
            sampleProvider.GetPooledSample(new VolumeAwareHitSampleInfo(new HitSampleInfo(HitSampleInfo.HIT_NORMAL), true));
            sampleProvider.GetPooledSample(new VolumeAwareHitSampleInfo(new HitSampleInfo(HitSampleInfo.HIT_CLAP), true));
            sampleProvider.GetPooledSample(new VolumeAwareHitSampleInfo(new HitSampleInfo(HitSampleInfo.HIT_FLOURISH), true));

            // We want to play back flourishes in an isolated source as to not have them cancelled.
            AddInternal(argonFlourishTrigger = new ArgonFlourishTriggerSource(hitObjectContainer));
        }

        protected override DrumSampleTriggerSource CreateTriggerSource(HitObjectContainer hitObjectContainer, SampleBalance balance) =>
            new ArgonDrumSampleTriggerSource(hitObjectContainer, balance);

        protected override void Play(DrumSampleTriggerSource triggerSource, HitType hitType, bool strong)
        {
            base.Play(triggerSource, hitType, strong);

            // This won't always play something, but the logic for flourish playback is contained within.
            argonFlourishTrigger.Play(hitType, strong);
        }
    }
}
