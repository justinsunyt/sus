// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using Foundation;
using sus.Framework.iOS;
using sus.Game.Tests;

namespace sus.Game.Rulesets.Mania.Tests.iOS
{
    [Register("AppDelegate")]
    public class AppDelegate : GameApplicationDelegate
    {
        protected override Framework.Game CreateGame() => new OsuTestBrowser();
    }
}
