// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using NUnit.Framework;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using sus.Game.Overlays;
using sus.Game.Overlays.Comments;

namespace sus.Game.Tests.Visual.Online
{
    [TestFixture]
    public partial class TestSceneCommentsHeader : OsuTestScene
    {
        [Cached]
        private readonly OverlayColourProvider colourProvider = new OverlayColourProvider(OverlayColourScheme.Blue);

        private readonly Bindable<CommentsSortCriteria> sort = new Bindable<CommentsSortCriteria>();
        private readonly BindableBool showDeleted = new BindableBool();

        public TestSceneCommentsHeader()
        {
            Add(new CommentsHeader
            {
                Sort = { BindTarget = sort },
                ShowDeleted = { BindTarget = showDeleted }
            });

            AddStep("Trigger ShowDeleted", () => showDeleted.Value = !showDeleted.Value);
            AddStep("Select old", () => sort.Value = CommentsSortCriteria.Old);
            AddStep("Select new", () => sort.Value = CommentsSortCriteria.New);
            AddStep("Select top", () => sort.Value = CommentsSortCriteria.Top);
        }
    }
}
