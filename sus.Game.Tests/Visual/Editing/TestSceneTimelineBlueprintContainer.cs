// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using NUnit.Framework;
using sus.Framework.Graphics;
using sus.Game.Screens.Edit.Compose.Components.Timeline;

namespace sus.Game.Tests.Visual.Editing
{
    [TestFixture]
    public partial class TestSceneTimelineBlueprintContainer : TimelineTestScene
    {
        public override Drawable CreateTestComponent() => new TimelineBlueprintContainer(Composer);

        protected override void LoadComplete()
        {
            base.LoadComplete();
            EditorClock.Seek(10000);
        }
    }
}
