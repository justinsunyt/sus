// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Linq;
using System.Threading;
using sus.Framework.Localisation;
using sus.Game.Beatmaps;
using sus.Game.Rulesets.Judgements;
using sus.Game.Rulesets.Mods;
using sus.Game.Rulesets.Objects;
using sus.Game.Rulesets.Objects.Drawables;
using sus.Game.Rulesets.Objects.Types;
using sus.Game.Rulesets.Osu.Beatmaps;
using sus.Game.Rulesets.Osu.Objects;
using sus.Game.Rulesets.Osu.Objects.Drawables;
using sus.Game.Rulesets.Scoring;
using sus.Game.Rulesets.UI;
using sus.Game.Screens.Play;

namespace sus.Game.Rulesets.Osu.Mods
{
    public partial class OsuModStrictTracking : Mod, IApplicableAfterBeatmapConversion, IApplicableToDrawableHitObject, IApplicableToDrawableRuleset<OsuHitObject>
    {
        public override string Name => @"Strict Tracking";
        public override string Acronym => @"ST";
        public override ModType Type => ModType.DifficultyIncrease;
        public override LocalisableString Description => @"Once you start a slider, follow precisely or get a miss.";
        public override double ScoreMultiplier => 1.0;
        public override Type[] IncompatibleMods => new[] { typeof(ModClassic), typeof(OsuModTargetPractice) };

        public void ApplyToDrawableHitObject(DrawableHitObject drawable)
        {
            if (drawable is DrawableSlider slider)
            {
                slider.Tracking.ValueChanged += e =>
                {
                    if (e.NewValue || slider.Judged) return;

                    if (slider.Time.Current < slider.HitObject.StartTime)
                        return;

                    if ((slider.Clock as IGameplayClock)?.IsRewinding == true)
                        return;

                    var tail = slider.NestedHitObjects.OfType<StrictTrackingDrawableSliderTail>().First();

                    if (!tail.Judged)
                        tail.MissForcefully();
                };
            }
        }

        public void ApplyToBeatmap(IBeatmap beatmap)
        {
            var susBeatmap = (OsuBeatmap)beatmap;

            if (susBeatmap.HitObjects.Count == 0) return;

            var hitObjects = susBeatmap.HitObjects.Select(ho =>
            {
                if (ho is Slider slider)
                {
                    var newSlider = new StrictTrackingSlider(slider);
                    return newSlider;
                }

                return ho;
            }).ToList();

            susBeatmap.HitObjects = hitObjects;
        }

        public void ApplyToDrawableRuleset(DrawableRuleset<OsuHitObject> drawableRuleset)
        {
            drawableRuleset.Playfield.RegisterPool<StrictTrackingSliderTailCircle, StrictTrackingDrawableSliderTail>(10, 100);
        }

        private class StrictTrackingSliderTailCircle : SliderTailCircle
        {
            public StrictTrackingSliderTailCircle(Slider slider)
                : base(slider)
            {
            }

            public override Judgement CreateJudgement() => new StrictTrackingTailJudgement();
        }

        public class StrictTrackingTailJudgement : SliderTailCircle.TailJudgement
        {
            public override HitResult MinResult => HitResult.LargeTickMiss;
        }

        private partial class StrictTrackingDrawableSliderTail : DrawableSliderTail
        {
            public override bool DisplayResult => true;
        }

        private class StrictTrackingSlider : Slider
        {
            public StrictTrackingSlider(Slider original)
            {
                StartTime = original.StartTime;
                Samples = original.Samples;
                Path = original.Path;
                NodeSamples = original.NodeSamples;
                RepeatCount = original.RepeatCount;
                Position = original.Position;
                NewCombo = original.NewCombo;
                ComboOffset = original.ComboOffset;
                TickDistanceMultiplier = original.TickDistanceMultiplier;
                SliderVelocityMultiplier = original.SliderVelocityMultiplier;
            }

            protected override void CreateNestedHitObjects(CancellationToken cancellationToken)
            {
                var sliderEvents = SliderEventGenerator.Generate(StartTime, SpanDuration, Velocity, TickDistance, Path.Distance, this.SpanCount(), cancellationToken);

                foreach (var e in sliderEvents)
                {
                    switch (e.Type)
                    {
                        case SliderEventType.Tick:
                            AddNested(new SliderTick
                            {
                                SpanIndex = e.SpanIndex,
                                SpanStartTime = e.SpanStartTime,
                                StartTime = e.Time,
                                Position = Position + Path.PositionAt(e.PathProgress),
                                StackHeight = StackHeight,
                                Scale = Scale,
                                PathProgress = e.PathProgress,
                            });
                            break;

                        case SliderEventType.Head:
                            AddNested(HeadCircle = new SliderHeadCircle
                            {
                                StartTime = e.Time,
                                Position = Position,
                                StackHeight = StackHeight,
                            });
                            break;

                        case SliderEventType.Tail:
                            AddNested(TailCircle = new StrictTrackingSliderTailCircle(this)
                            {
                                RepeatIndex = e.SpanIndex,
                                StartTime = e.Time,
                                Position = EndPosition,
                                StackHeight = StackHeight
                            });
                            break;

                        case SliderEventType.Repeat:
                            AddNested(new SliderRepeat(this)
                            {
                                RepeatIndex = e.SpanIndex,
                                StartTime = StartTime + (e.SpanIndex + 1) * SpanDuration,
                                Position = Position + Path.PositionAt(e.PathProgress),
                                StackHeight = StackHeight,
                                Scale = Scale,
                                PathProgress = e.PathProgress,
                            });
                            break;
                    }
                }

                UpdateNestedSamples();
            }
        }
    }
}
