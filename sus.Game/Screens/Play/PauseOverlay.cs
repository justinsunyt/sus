// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Linq;
using sus.Framework.Allocation;
using sus.Framework.Audio;
using sus.Framework.Bindables;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Framework.Input.Events;
using sus.Framework.Localisation;
using sus.Framework.Platform;
using sus.Game.Audio;
using sus.Game.Input.Bindings;
using sus.Game.Localisation;
using sus.Game.Skinning;

namespace sus.Game.Screens.Play
{
    public partial class PauseOverlay : GameplayMenuOverlay
    {
        public override bool IsPresent => base.IsPresent || pauseLoop.IsPlaying;

        public override LocalisableString Header => GameplayMenuOverlayStrings.PausedHeader;

        private SkinnableSound pauseLoop = null!;

        protected override Action BackAction => () =>
        {
            if (Buttons.Any())
                Buttons.First().TriggerClick();
            else
                OnResume?.Invoke();
        };

        private readonly IBindable<bool> windowActive = new Bindable<bool>(true);

        private float targetVolume => windowActive.Value && State.Value == Visibility.Visible ? 1.0f : 0;

        [BackgroundDependencyLoader]
        private void load(GameHost? host)
        {
            AddInternal(pauseLoop = new SkinnableSound(new SampleInfo("Gameplay/pause-loop"))
            {
                Looping = true,
                Volume = { Value = 0 }
            });

            if (host != null)
                windowActive.BindTo(host.IsActive);
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            // Schedule required because host.IsActive doesn't seem to always run on the update thread.
            windowActive.BindValueChanged(_ => Schedule(() => pauseLoop.VolumeTo(targetVolume, 1000, Easing.Out)));
        }

        public void StopAllSamples()
        {
            if (!IsLoaded)
                return;

            pauseLoop.Stop();
        }

        protected override void PopIn()
        {
            base.PopIn();

            pauseLoop.VolumeTo(targetVolume, TRANSITION_DURATION, Easing.InQuint);
            pauseLoop.Play();
        }

        protected override void PopOut()
        {
            base.PopOut();

            pauseLoop.VolumeTo(targetVolume, TRANSITION_DURATION, Easing.OutQuad).Finally(_ => pauseLoop.Stop());
        }

        public override bool OnPressed(KeyBindingPressEvent<GlobalAction> e)
        {
            switch (e.Action)
            {
                case GlobalAction.PauseGameplay:
                    InternalButtons.First().TriggerClick();
                    return true;
            }

            return base.OnPressed(e);
        }
    }
}
