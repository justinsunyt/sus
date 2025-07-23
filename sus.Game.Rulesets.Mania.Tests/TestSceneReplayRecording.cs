// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Linq;
using NUnit.Framework;
using sus.Framework.Allocation;
using sus.Framework.Audio;
using sus.Framework.Timing;
using sus.Game.Beatmaps;
using sus.Game.Rulesets.Mania.Beatmaps;
using sus.Game.Rulesets.Mania.Objects;
using sus.Game.Rulesets.Mania.Replays;
using sus.Game.Storyboards;
using sus.Game.Tests.Visual;
using susTK.Input;

namespace sus.Game.Rulesets.Mania.Tests
{
    public partial class TestSceneReplayRecording : PlayerTestScene
    {
        protected override Ruleset CreatePlayerRuleset() => new ManiaRuleset();

        [Resolved]
        private AudioManager audioManager { get; set; } = null!;

        protected override IBeatmap CreateBeatmap(RulesetInfo ruleset) => new ManiaBeatmap(new StageDefinition(1))
        {
            HitObjects =
            {
                new Note { StartTime = 0, },
                new Note { StartTime = 5000, },
                new Note { StartTime = 10000, },
                new Note { StartTime = 15000, }
            },
            Difficulty = { CircleSize = 1 },
            BeatmapInfo =
            {
                Ruleset = ruleset,
            }
        };

        protected override WorkingBeatmap CreateWorkingBeatmap(IBeatmap beatmap, Storyboard? storyboard = null) =>
            new ClockBackedTestWorkingBeatmap(beatmap, storyboard, new FramedClock(new ManualClock { Rate = 1 }), audioManager);

        [Test]
        public void TestRecording()
        {
            seekTo(0);
            AddStep("press space", () => InputManager.PressKey(Key.Space));
            seekTo(15);
            AddStep("release space", () => InputManager.ReleaseKey(Key.Space));
            AddUntilStep("button press recorded to replay", () => Player.Score.Replay.Frames.OfType<ManiaReplayFrame>().Any(f => f.Actions.SequenceEqual([ManiaAction.Key1])));
        }

        private void seekTo(double time)
        {
            AddStep($"seek to {time}ms", () => Player.GameplayClockContainer.Seek(time));
            AddUntilStep("wait for seek to finish", () => Player.DrawableRuleset.FrameStableClock.CurrentTime, () => Is.EqualTo(time).Within(500));
        }
    }
}
