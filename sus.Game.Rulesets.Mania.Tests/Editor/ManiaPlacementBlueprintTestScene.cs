// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

#nullable disable

using System;
using System.Collections.Generic;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Timing;
using sus.Game.Rulesets.Mania.Beatmaps;
using sus.Game.Rulesets.Mania.Objects.Drawables;
using sus.Game.Rulesets.Mania.UI;
using sus.Game.Rulesets.Mods;
using sus.Game.Rulesets.Objects.Drawables;
using sus.Game.Rulesets.UI.Scrolling;
using sus.Game.Tests.Visual;
using osuTK.Graphics;

namespace sus.Game.Rulesets.Mania.Tests.Editor
{
    public abstract partial class ManiaPlacementBlueprintTestScene : PlacementBlueprintTestScene
    {
        protected sealed override Ruleset CreateRuleset() => new ManiaRuleset();

        private readonly Column column;

        [Cached(typeof(IReadOnlyList<Mod>))]
        private IReadOnlyList<Mod> mods { get; set; } = Array.Empty<Mod>();

        [Cached(typeof(IScrollingInfo))]
        private IScrollingInfo scrollingInfo;

        [Cached]
        private readonly StageDefinition stage = new StageDefinition(5);

        protected ManiaPlacementBlueprintTestScene()
        {
            scrollingInfo = ((ScrollingTestContainer)HitObjectContainer).ScrollingInfo;

            Add(column = new Column(0, false)
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                AccentColour = { Value = Color4.OrangeRed },
                Clock = new FramedClock(new StopwatchClock()), // No scroll
            });
        }

        protected override void UpdatePlacementTimeAndPosition()
        {
            double time = column.TimeAtScreenSpacePosition(InputManager.CurrentState.Mouse.Position);
            var pos = column.ScreenSpacePositionAtTime(time);
            CurrentBlueprint.UpdateTimeAndPosition(pos, time);
        }

        protected override Container CreateHitObjectContainer() => new ScrollingTestContainer(ScrollingDirection.Down) { RelativeSizeAxes = Axes.Both };

        protected override void AddHitObject(DrawableHitObject hitObject) => column.Add((DrawableManiaHitObject)hitObject);

        public ManiaPlayfield Playfield => null;
    }
}
