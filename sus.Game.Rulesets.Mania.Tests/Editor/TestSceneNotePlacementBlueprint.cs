// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

#nullable disable

using System.Linq;
using NUnit.Framework;
using osu.Framework.Extensions.IEnumerableExtensions;
using osu.Framework.Testing;
using sus.Game.Rulesets.Edit;
using sus.Game.Rulesets.Mania.Edit.Blueprints;
using sus.Game.Rulesets.Mania.Objects;
using sus.Game.Rulesets.Mania.Objects.Drawables;
using sus.Game.Rulesets.Mania.UI;
using sus.Game.Rulesets.Objects;
using sus.Game.Rulesets.Objects.Drawables;
using sus.Game.Rulesets.UI;
using sus.Game.Rulesets.UI.Scrolling;
using sus.Game.Tests.Visual;
using osuTK.Input;

namespace sus.Game.Rulesets.Mania.Tests.Editor
{
    public partial class TestSceneNotePlacementBlueprint : ManiaPlacementBlueprintTestScene
    {
        [SetUp]
        public void Setup() => Schedule(() =>
        {
            this.ChildrenOfType<HitObjectContainer>().ForEach(c => c.Clear());

            ResetPlacement();

            ((ScrollingTestContainer)HitObjectContainer).Direction = ScrollingDirection.Down;
        });

        [Test]
        public void TestPlaceBeforeCurrentTimeDownwards()
        {
            AddStep("move mouse before current time", () =>
            {
                var column = this.ChildrenOfType<Column>().Single();
                InputManager.MoveMouseTo(column.ScreenSpacePositionAtTime(-100));
            });

            AddStep("click", () => InputManager.Click(MouseButton.Left));

            AddAssert("note start time < 0", () => getNote().StartTime < 0);
        }

        [Test]
        public void TestPlaceAfterCurrentTimeDownwards()
        {
            AddStep("move mouse after current time", () =>
            {
                var column = this.ChildrenOfType<Column>().Single();
                InputManager.MoveMouseTo(column.ScreenSpacePositionAtTime(100));
            });

            AddStep("click", () => InputManager.Click(MouseButton.Left));

            AddAssert("note start time > 0", () => getNote().StartTime > 0);
        }

        private Note getNote() => this.ChildrenOfType<DrawableNote>().FirstOrDefault()?.HitObject;

        protected override DrawableHitObject CreateHitObject(HitObject hitObject) => new DrawableNote((Note)hitObject);
        protected override HitObjectPlacementBlueprint CreateBlueprint() => new NotePlacementBlueprint();
    }
}
