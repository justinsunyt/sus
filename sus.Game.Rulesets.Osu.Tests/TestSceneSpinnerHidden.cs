// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using NUnit.Framework;
using sus.Game.Rulesets.Osu.Mods;

namespace sus.Game.Rulesets.Osu.Tests
{
    [TestFixture]
    public partial class TestSceneSpinnerHidden : TestSceneSpinner
    {
        [SetUp]
        public void SetUp() => Schedule(() =>
        {
            SelectedMods.Value = new[] { new OsuModHidden() };
        });
    }
}
