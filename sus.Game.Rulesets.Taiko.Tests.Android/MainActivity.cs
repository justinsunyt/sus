// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using Android.App;
using sus.Framework.Android;
using sus.Game.Tests;

namespace sus.Game.Rulesets.Taiko.Tests.Android
{
    [Activity(ConfigurationChanges = DEFAULT_CONFIG_CHANGES, Exported = true, LaunchMode = DEFAULT_LAUNCH_MODE, MainLauncher = true)]
    public class MainActivity : AndroidGameActivity
    {
        protected override Framework.Game CreateGame() => new OsuTestBrowser();
    }
}
