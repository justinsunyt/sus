// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Extensions.Color4Extensions;
using sus.Game.Graphics.Containers;

namespace sus.Game.Screens.OnlinePlay
{
    public partial class OnlinePlayScreenWaveContainer : WaveContainer
    {
        protected override bool StartHidden => true;

        public OnlinePlayScreenWaveContainer()
        {
            FirstWaveColour = Color4Extensions.FromHex(@"654d8c");
            SecondWaveColour = Color4Extensions.FromHex(@"554075");
            ThirdWaveColour = Color4Extensions.FromHex(@"44325e");
            FourthWaveColour = Color4Extensions.FromHex(@"392850");
        }
    }
}
