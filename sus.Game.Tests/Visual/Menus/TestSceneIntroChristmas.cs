// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using NUnit.Framework;
using sus.Game.Screens.Menu;
using sus.Game.Seasonal;

namespace sus.Game.Tests.Visual.Menus
{
    [TestFixture]
    public partial class TestSceneIntroChristmas : IntroTestScene
    {
        protected override bool IntroReliesOnTrack => true;
        protected override IntroScreen CreateScreen() => new IntroChristmas();
    }
}
