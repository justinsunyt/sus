// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Allocation;
using sus.Framework.Audio.Track;
using sus.Framework.Bindables;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Sprites;
using sus.Framework.Graphics.Textures;
using sus.Framework.Input.Bindings;
using sus.Framework.Input.Events;
using sus.Game.Beatmaps.ControlPoints;
using sus.Game.Graphics.Containers;
using susTK;

namespace sus.Game.Rulesets.Pippidon.UI
{
    public partial class PippidonCharacter : BeatSyncedContainer, IKeyBindingHandler<PippidonAction>
    {
        public readonly BindableInt LanePosition = new BindableInt
        {
            Value = 0,
            MinValue = 0,
            MaxValue = PippidonPlayfield.LANE_COUNT - 1,
        };

        [BackgroundDependencyLoader]
        private void load(TextureStore textures)
        {
            Size = new Vector2(PippidonPlayfield.LANE_HEIGHT);

            Child = new Sprite
            {
                FillMode = FillMode.Fit,
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Scale = new Vector2(1.2f),
                RelativeSizeAxes = Axes.Both,
                Texture = textures.Get("character")
            };

            LanePosition.BindValueChanged(e => { this.MoveToY(e.NewValue * PippidonPlayfield.LANE_HEIGHT); });
        }

        protected override void OnNewBeat(int beatIndex, TimingControlPoint timingPoint, EffectControlPoint effectPoint, ChannelAmplitudes amplitudes)
        {
            if (effectPoint.KiaiMode)
            {
                bool direction = beatIndex % 2 == 1;
                double duration = timingPoint.BeatLength / 2;

                Child.RotateTo(direction ? 10 : -10, duration * 2, Easing.InOutSine);

                Child.Animate(i => i.MoveToY(-10, duration, Easing.Out))
                     .Then(i => i.MoveToY(0, duration, Easing.In));
            }
            else
            {
                Child.ClearTransforms();
                Child.RotateTo(0, 500, Easing.Out);
                Child.MoveTo(Vector2.Zero, 500, Easing.Out);
            }
        }

        public bool OnPressed(KeyBindingPressEvent<PippidonAction> e)
        {
            switch (e.Action)
            {
                case PippidonAction.MoveUp:
                    changeLane(-1);
                    return true;

                case PippidonAction.MoveDown:
                    changeLane(1);
                    return true;

                default:
                    return false;
            }
        }

        public void OnReleased(KeyBindingReleaseEvent<PippidonAction> e)
        {
        }

        private void changeLane(int change) => LanePosition.Value = (LanePosition.Value + change + PippidonPlayfield.LANE_COUNT) % PippidonPlayfield.LANE_COUNT;
    }
}
