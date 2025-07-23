// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using NUnit.Framework;
using sus.Game.Beatmaps.ControlPoints;
using sus.Game.Beatmaps;
using sus.Game.Rulesets.Taiko.Skinning.Legacy;

namespace sus.Game.Rulesets.Taiko.Tests.Skinning
{
    public partial class TestSceneTaikoKiaiGlow : TaikoSkinnableTestScene
    {
        [Test]
        public void TestKiaiGlow()
        {
            AddStep("Create kiai glow", () => SetContents(_ => new LegacyKiaiGlow()));
            AddToggleStep("Toggle kiai mode", setUpBeatmap);
        }

        private void setUpBeatmap(bool withKiai)
        {
            var controlPointInfo = new ControlPointInfo();

            controlPointInfo.Add(0, new TimingControlPoint { BeatLength = 500 });

            if (withKiai)
                controlPointInfo.Add(0, new EffectControlPoint { KiaiMode = true });

            Beatmap.Value = CreateWorkingBeatmap(new Beatmap
            {
                ControlPointInfo = controlPointInfo
            });

            Beatmap.Value.Track.Start();
        }
    }
}
