// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Allocation;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Framework.Input.Events;
using sus.Game.Graphics.Containers;
using susTK;

namespace sus.Game.Graphics.UserInterface
{
    public abstract partial class LoadingButton : OsuHoverContainer
    {
        private bool isLoading;

        public bool IsLoading
        {
            get => isLoading;
            set
            {
                isLoading = value;

                Enabled.Value = !isLoading;

                if (value)
                    loading.Show();
                else
                    loading.Hide();
            }
        }

        public Vector2 LoadingAnimationSize
        {
            get => loading.Size;
            set => loading.Size = value;
        }

        private readonly LoadingSpinner loading;

        protected LoadingButton()
            : base(HoverSampleSet.Button)
        {
            Add(loading = new LoadingSpinner
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Size = new Vector2(12),
                Depth = -1,
            });
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            Add(CreateContent());
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            loading.State.BindValueChanged(s =>
            {
                if (s.NewValue == Visibility.Visible)
                    OnLoadStarted();
                else
                    OnLoadFinished();
            }, true);
        }

        protected override bool OnClick(ClickEvent e)
        {
            if (!Enabled.Value)
                return false;

            try
            {
                return base.OnClick(e);
            }
            finally
            {
                // run afterwards as this will disable this button.
                IsLoading = true;
            }
        }

        protected virtual void OnLoadStarted()
        {
        }

        protected virtual void OnLoadFinished()
        {
        }

        protected abstract Drawable CreateContent();
    }
}
