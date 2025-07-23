// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using NUnit.Framework;
using sus.Framework.Audio;
using sus.Framework.Audio.Track;
using sus.Framework.Bindables;
using sus.Framework.Timing;
using sus.Game.Screens.Play;

namespace sus.Game.Tests.NonVisual
{
    [TestFixture]
    public partial class GameplayClockContainerTest
    {
        [TestCase(0)]
        [TestCase(1)]
        public void TestTrueGameplayRateWithGameplayAdjustment(double underlyingClockRate)
        {
            var trackVirtual = new TrackVirtual(60000) { Frequency = { Value = underlyingClockRate } };
            var gameplayClock = new TestGameplayClockContainer(trackVirtual);

            Assert.That(gameplayClock.GetTrueGameplayRate(), Is.EqualTo(2));
        }

        private partial class TestGameplayClockContainer : GameplayClockContainer
        {
            public TestGameplayClockContainer(IClock underlyingClock)
                : base(underlyingClock, false, false)
            {
                AdjustmentsFromMods.AddAdjustment(AdjustableProperty.Frequency, new BindableDouble(2.0));
            }
        }
    }
}
