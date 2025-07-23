// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

#nullable disable

using sus.Framework.Allocation;
using sus.Framework.Audio.Track;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Framework.Testing;
using sus.Game.Overlays.SkinEditor;
using sus.Game.Rulesets;
using sus.Game.Rulesets.Osu;
using sus.Game.Rulesets.Scoring;
using sus.Game.Screens.Edit;
using sus.Game.Screens.Play;
using sus.Game.Screens.Play.HUD;
using sus.Game.Screens.Select.Leaderboards;
using sus.Game.Tests.Gameplay;
using susTK.Input;

namespace sus.Game.Tests.Visual.Gameplay
{
    public partial class TestSceneSkinEditorMultipleSkins : SkinnableTestScene
    {
        [Cached(typeof(ScoreProcessor))]
        private ScoreProcessor scoreProcessor { get; set; }

        [Cached(typeof(HealthProcessor))]
        private HealthProcessor healthProcessor = new DrainingHealthProcessor(0);

        [Cached]
        private GameplayState gameplayState = TestGameplayState.Create(new OsuRuleset());

        [Cached(typeof(IGameplayClock))]
        private readonly IGameplayClock gameplayClock = new GameplayClockContainer(new TrackVirtual(60000), false, false);

        [Cached]
        public readonly EditorClipboard Clipboard = new EditorClipboard();

        [Cached(typeof(IGameplayLeaderboardProvider))]
        private EmptyGameplayLeaderboardProvider leaderboardProvider = new EmptyGameplayLeaderboardProvider();

        public TestSceneSkinEditorMultipleSkins()
        {
            scoreProcessor = gameplayState.ScoreProcessor;
        }

        [SetUpSteps]
        public void SetUpSteps()
        {
            AddStep("create editor overlay", () =>
            {
                SetContents(_ =>
                {
                    var ruleset = new OsuRuleset();
                    var mods = new[] { ruleset.GetAutoplayMod() };
                    var working = CreateWorkingBeatmap(ruleset.RulesetInfo);
                    var beatmap = working.GetPlayableBeatmap(ruleset.RulesetInfo, mods);

                    var drawableRuleset = ruleset.CreateDrawableRulesetWith(beatmap, mods);

                    var hudOverlay = new HUDOverlay(drawableRuleset, mods)
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                    };

                    // Add any key just to display the key counter visually.
                    hudOverlay.InputCountController.Add(new KeyCounterKeyboardTrigger(Key.Space));
                    scoreProcessor.Combo.Value = 1;

                    return new Container
                    {
                        RelativeSizeAxes = Axes.Both,
                        Children = new Drawable[]
                        {
                            drawableRuleset,
                            hudOverlay,
                            new SkinEditor(hudOverlay),
                        }
                    };
                });
            });
        }

        protected override Ruleset CreateRulesetForSkinProvider() => new OsuRuleset();
    }
}
