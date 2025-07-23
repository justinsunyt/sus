// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using sus.Framework.Allocation;
using sus.Framework.Audio.Track;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Sprites;
using sus.Game.Beatmaps.ControlPoints;
using sus.Game.Graphics.Containers;
using sus.Game.Rulesets.Judgements;
using sus.Game.Rulesets.Scoring;
using sus.Game.Skinning;
using susTK;

namespace sus.Game.Rulesets.Taiko.Skinning.Legacy
{
    internal partial class LegacyKiaiGlow : BeatSyncedContainer
    {
        [Resolved]
        private HealthProcessor? healthProcessor { get; set; }

        private bool isKiaiActive;
        private Sprite sprite = null!;

        [BackgroundDependencyLoader]
        private void load(ISkinSource skin)
        {
            Child = sprite = new Sprite
            {
                Texture = skin.GetTexture("taiko-glow"),
                Origin = Anchor.Centre,
                Anchor = Anchor.Centre,
                Alpha = 0,
                Scale = new Vector2(TaikoLegacyHitTarget.SCALE),
                Colour = new Colour4(255, 228, 0, 255),
            };
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            if (healthProcessor != null)
                healthProcessor.NewJudgement += onNewJudgement;
        }

        protected override void Update()
        {
            base.Update();

            if (isKiaiActive)
                sprite.Alpha = (float)Math.Min(1, sprite.Alpha + Math.Abs(Clock.ElapsedFrameTime) / 100f);
            else
                sprite.Alpha = (float)Math.Max(0, sprite.Alpha - Math.Abs(Clock.ElapsedFrameTime) / 600f);
        }

        protected override void OnNewBeat(int beatIndex, TimingControlPoint timingPoint, EffectControlPoint effectPoint, ChannelAmplitudes amplitudes)
        {
            isKiaiActive = effectPoint.KiaiMode;
        }

        private void onNewJudgement(JudgementResult result)
        {
            if (!result.IsHit || !isKiaiActive)
                return;

            sprite.ScaleTo(TaikoLegacyHitTarget.SCALE + 0.15f).Then()
                  .ScaleTo(TaikoLegacyHitTarget.SCALE, 80, Easing.OutQuad);
        }

        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);

            if (healthProcessor != null)
                healthProcessor.NewJudgement -= onNewJudgement;
        }
    }
}
