// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Allocation;
using sus.Framework.Bindables;
using sus.Framework.Graphics.Containers;
using sus.Framework.Input;
using sus.Framework.Input.States;
using sus.Game.Overlays;

namespace sus.Game.Screens.Utility.SampleComponents
{
    public abstract partial class LatencySampleComponent : CompositeDrawable
    {
        protected readonly BindableDouble SampleBPM = new BindableDouble();
        protected readonly BindableDouble SampleApproachRate = new BindableDouble();
        protected readonly BindableFloat SampleVisualSpacing = new BindableFloat();

        protected readonly BindableBool IsActive = new BindableBool();

        private InputManager inputManager = null!;

        [Resolved]
        private LatencyArea latencyArea { get; set; } = null!;

        [Resolved]
        protected OverlayColourProvider OverlayColourProvider { get; private set; } = null!;

        [BackgroundDependencyLoader]
        private void load(LatencyCertifierScreen latencyCertifierScreen)
        {
            SampleBPM.BindTo(latencyCertifierScreen.SampleBPM);
            SampleApproachRate.BindTo(latencyCertifierScreen.SampleApproachRate);
            SampleVisualSpacing.BindTo(latencyCertifierScreen.SampleVisualSpacing);
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            inputManager = GetContainingInputManager()!;
            IsActive.BindTo(latencyArea.IsActiveArea);
        }

        protected sealed override void Update()
        {
            base.Update();
            UpdateAtLimitedRate(inputManager.CurrentState);
        }

        protected abstract void UpdateAtLimitedRate(InputState inputState);
    }
}
