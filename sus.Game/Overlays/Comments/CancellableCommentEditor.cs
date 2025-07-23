// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using sus.Framework.Allocation;
using sus.Game.Resources.Localisation.Web;

namespace sus.Game.Overlays.Comments
{
    public abstract partial class CancellableCommentEditor : CommentEditor
    {
        public Action? OnCancel;

        [BackgroundDependencyLoader]
        private void load()
        {
            ButtonsContainer.Add(new EditorButton
            {
                Action = () => OnCancel?.Invoke(),
                Text = CommonStrings.ButtonsCancel,
            });
        }
    }
}
