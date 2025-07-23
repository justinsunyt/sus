// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Allocation;
using sus.Framework.Bindables;
using sus.Framework.Extensions.LocalisationExtensions;
using sus.Framework.Graphics;
using sus.Framework.Input.Bindings;
using sus.Framework.Input.Events;
using sus.Framework.Localisation;
using sus.Game.Beatmaps;
using sus.Game.Input;
using sus.Game.Input.Bindings;
using sus.Game.Localisation;
using sus.Game.Overlays.OSD;

namespace sus.Game.Overlays.Music
{
    /// <summary>
    /// Handles <see cref="GlobalAction"/>s related to music playback, and displays <see cref="Toast"/>s via the global <see cref="OnScreenDisplay"/> accordingly.
    /// </summary>
    public partial class MusicKeyBindingHandler : Component, IKeyBindingHandler<GlobalAction>
    {
        [Resolved]
        private IBindable<WorkingBeatmap> beatmap { get; set; } = null!;

        [Resolved]
        private MusicController musicController { get; set; } = null!;

        [Resolved]
        private OnScreenDisplay? onScreenDisplay { get; set; }

        public bool OnPressed(KeyBindingPressEvent<GlobalAction> e)
        {
            if (e.Repeat || !musicController.AllowTrackControl.Value)
                return false;

            switch (e.Action)
            {
                case GlobalAction.MusicPlay:
                    // use previous state as TogglePause may not update the track's state immediately (state update is run on the audio thread see https://github.com/ppy/sus/issues/9880#issuecomment-674668842)
                    bool wasPlaying = musicController.IsPlaying;

                    if (musicController.TogglePause())
                        onScreenDisplay?.Display(new MusicActionToast(wasPlaying ? ToastStrings.PauseTrack : ToastStrings.PlayTrack, e.Action));
                    return true;

                case GlobalAction.MusicNext:
                    if (beatmap.Disabled)
                        return false;

                    musicController.NextTrack(() => onScreenDisplay?.Display(new MusicActionToast(GlobalActionKeyBindingStrings.MusicNext, e.Action)));

                    return true;

                case GlobalAction.MusicPrev:
                    if (beatmap.Disabled)
                        return false;

                    musicController.PreviousTrack(res =>
                    {
                        switch (res)
                        {
                            case PreviousTrackResult.Restart:
                                onScreenDisplay?.Display(new MusicActionToast(ToastStrings.RestartTrack, e.Action));
                                break;

                            case PreviousTrackResult.Previous:
                                onScreenDisplay?.Display(new MusicActionToast(GlobalActionKeyBindingStrings.MusicPrev, e.Action));
                                break;
                        }
                    });

                    return true;
            }

            return false;
        }

        public void OnReleased(KeyBindingReleaseEvent<GlobalAction> e)
        {
        }

        private partial class MusicActionToast : Toast
        {
            private readonly GlobalAction action;

            public MusicActionToast(LocalisableString value, GlobalAction action)
                : base(ToastStrings.MusicPlayback, value, string.Empty)
            {
                this.action = action;
            }

            [BackgroundDependencyLoader]
            private void load(RealmKeyBindingStore keyBindingStore)
            {
                ShortcutText.Text = keyBindingStore.GetBindingsStringFor(action).ToUpper();
            }
        }
    }
}
