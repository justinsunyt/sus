// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Reflection;
using Android.App;
using Android.OS;
using sus.Framework.Android;

namespace sus.Game.Tests.Android
{
    [Activity(ConfigurationChanges = DEFAULT_CONFIG_CHANGES, Exported = true, LaunchMode = DEFAULT_LAUNCH_MODE, MainLauncher = true)]
    public class MainActivity : AndroidGameActivity
    {
        protected override Framework.Game CreateGame() => new OsuTestBrowser();

        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // See the comment in OsuGameActivity
            Assembly.Load("sus.Game.Rulesets.Osu");
            Assembly.Load("sus.Game.Rulesets.Taiko");
            Assembly.Load("sus.Game.Rulesets.Catch");
            Assembly.Load("sus.Game.Rulesets.Mania");
        }
    }
}
