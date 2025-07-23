// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using NUnit.Framework;
using osu.Framework.Graphics;
using sus.Game.Rulesets.Mods;
using sus.Game.Rulesets.Osu.Mods;
using sus.Game.Screens.Play.HUD;

namespace sus.Game.Tests.Visual.UserInterface
{
    public partial class TestSceneModDisplay : OsuTestScene
    {
        [Test]
        public void TestMode([Values] ExpansionMode mode)
        {
            AddStep("create mod display", () =>
            {
                Child = new ModDisplay
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    ExpansionMode = mode,
                    Current =
                    {
                        Value = new Mod[]
                        {
                            new OsuModHardRock(),
                            new OsuModDoubleTime(),
                            new OsuModDifficultyAdjust(),
                            new OsuModEasy(),
                        }
                    }
                };
            });
        }
    }
}
