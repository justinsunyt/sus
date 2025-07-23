// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Allocation;
using sus.Framework.Bindables;
using sus.Framework.Graphics;
using sus.Framework.Platform;
using sus.Game.Configuration;
using sus.Game.Screens.Play;

namespace sus.Desktop.Windows
{
    public partial class GameplayWinKeyBlocker : Component
    {
        private Bindable<bool> disableWinKey = null!;
        private IBindable<LocalUserPlayingState> localUserPlaying = null!;
        private IBindable<bool> isActive = null!;

        [Resolved]
        private GameHost host { get; set; } = null!;

        [BackgroundDependencyLoader]
        private void load(ILocalUserPlayInfo localUserInfo, OsuConfigManager config)
        {
            localUserPlaying = localUserInfo.PlayingState.GetBoundCopy();
            localUserPlaying.BindValueChanged(_ => updateBlocking());

            isActive = host.IsActive.GetBoundCopy();
            isActive.BindValueChanged(_ => updateBlocking());

            disableWinKey = config.GetBindable<bool>(OsuSetting.GameplayDisableWinKey);
            disableWinKey.BindValueChanged(_ => updateBlocking(), true);
        }

        private void updateBlocking()
        {
            bool shouldDisable = isActive.Value && disableWinKey.Value && localUserPlaying.Value == LocalUserPlayingState.Playing;

            if (shouldDisable)
                host.InputThread.Scheduler.Add(WindowsKey.Disable);
            else
                host.InputThread.Scheduler.Add(WindowsKey.Enable);
        }
    }
}
