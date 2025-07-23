// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

#nullable disable

using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Input;
using sus.Game.Beatmaps;
using sus.Game.Configuration;
using sus.Game.Input.Handlers;
using sus.Game.Replays;
using sus.Game.Rulesets.Mods;
using sus.Game.Rulesets.Objects;
using sus.Game.Rulesets.Objects.Drawables;
using sus.Game.Rulesets.Taiko.Objects;
using sus.Game.Rulesets.Taiko.Replays;
using sus.Game.Rulesets.Timing;
using sus.Game.Rulesets.UI;
using sus.Game.Rulesets.UI.Scrolling;
using sus.Game.Scoring;
using sus.Game.Screens.Play;
using sus.Game.Skinning;
using sus.Game.Storyboards;
using osuTK;

namespace sus.Game.Rulesets.Taiko.UI
{
    public partial class DrawableTaikoRuleset : DrawableScrollingRuleset<TaikoHitObject>
    {
        public new BindableDouble TimeRange => base.TimeRange;

        public readonly BindableBool LockPlayfieldAspectRange = new BindableBool(true);

        public new TaikoInputManager KeyBindingInputManager => (TaikoInputManager)base.KeyBindingInputManager;

        protected new TaikoPlayfieldAdjustmentContainer PlayfieldAdjustmentContainer => (TaikoPlayfieldAdjustmentContainer)base.PlayfieldAdjustmentContainer;

        protected override bool UserScrollSpeedAdjustment => false;

        [CanBeNull]
        private SkinnableDrawable scroller;

        public DrawableTaikoRuleset(Ruleset ruleset, IBeatmap beatmap, IReadOnlyList<Mod> mods = null)
            : base(ruleset, beatmap, mods)
        {
            Direction.Value = ScrollingDirection.Left;
            VisualisationMethod = ScrollVisualisationMethod.Overlapping;
        }

        [BackgroundDependencyLoader(true)]
        private void load([CanBeNull] GameplayState gameplayState)
        {
            new BarLineGenerator<BarLine>(Beatmap).BarLines.ForEach(bar => Playfield.Add(bar));

            var spriteElements = gameplayState?.Storyboard.Layers.Where(l => l.Name != @"Overlay")
                                              .SelectMany(l => l.Elements)
                                              .OfType<StoryboardSprite>()
                                              .DistinctBy(e => e.Path) ?? Enumerable.Empty<StoryboardSprite>();

            if (spriteElements.Count() < 10)
            {
                FrameStableComponents.Add(scroller = new SkinnableDrawable(new TaikoSkinComponentLookup(TaikoSkinComponents.Scroller), _ => Empty())
                {
                    RelativeSizeAxes = Axes.X,
                    Depth = float.MaxValue,
                });
            }

            KeyBindingInputManager.Add(new DrumTouchInputArea());
        }

        protected override void Update()
        {
            base.Update();

            TimeRange.Value = ComputeTimeRange();
        }

        protected virtual double ComputeTimeRange()
        {
            // Using the constant algorithm results in a sluggish scroll speed that's equal to 60 BPM.
            // We need to adjust it to the expected default scroll speed (BPM * base SV multiplier).
            double multiplier = VisualisationMethod == ScrollVisualisationMethod.Constant
                ? (Beatmap.BeatmapInfo.BPM * Beatmap.Difficulty.SliderMultiplier) / 60
                : 1;
            return PlayfieldAdjustmentContainer.ComputeTimeRange() / multiplier;
        }

        protected override void UpdateAfterChildren()
        {
            base.UpdateAfterChildren();

            var playfieldScreen = Playfield.ScreenSpaceDrawQuad;

            if (scroller != null)
                scroller.Height = ToLocalSpace(playfieldScreen.TopLeft + new Vector2(0, playfieldScreen.Height / 20)).Y;
        }

        public MultiplierControlPoint ControlPointAt(double time)
        {
            int result = ControlPoints.BinarySearch(new MultiplierControlPoint(time));
            if (result < 0)
                result = Math.Clamp(~result - 1, 0, ControlPoints.Count);
            return ControlPoints[result];
        }

        public override PlayfieldAdjustmentContainer CreatePlayfieldAdjustmentContainer() => new TaikoPlayfieldAdjustmentContainer
        {
            LockPlayfieldAspectRange = { BindTarget = LockPlayfieldAspectRange }
        };

        protected override PassThroughInputManager CreateInputManager() => new TaikoInputManager(Ruleset.RulesetInfo);

        protected override Playfield CreatePlayfield() => new TaikoPlayfield();

        public override DrawableHitObject<TaikoHitObject> CreateDrawableRepresentation(TaikoHitObject h) => null;

        protected override ReplayInputHandler CreateReplayInputHandler(Replay replay) => new TaikoFramedReplayInputHandler(replay);

        protected override ReplayRecorder CreateReplayRecorder(Score score) => new TaikoReplayRecorder(score);

        protected override ResumeOverlay CreateResumeOverlay() => new DelayedResumeOverlay();
    }
}
