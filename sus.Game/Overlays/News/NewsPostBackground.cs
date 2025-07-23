// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Allocation;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Sprites;
using sus.Framework.Graphics.Textures;

namespace sus.Game.Overlays.News
{
    [LongRunningLoad]
    public partial class NewsPostBackground : Sprite
    {
        private readonly string sourceUrl;

        public NewsPostBackground(string sourceUrl)
        {
            this.sourceUrl = sourceUrl;
        }

        [BackgroundDependencyLoader]
        private void load(LargeTextureStore store)
        {
            Texture = store.Get(createUrl(sourceUrl));
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            this.FadeInFromZero(500, Easing.OutQuint);
        }

        private string createUrl(string source)
        {
            if (string.IsNullOrEmpty(source))
                return "Headers/news";

            if (source.StartsWith('/'))
                return "https://sus.ppy.sh" + source;

            return source;
        }
    }
}
