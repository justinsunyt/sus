// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using sus.Framework.Graphics.Sprites;
using sus.Framework.Localisation;

namespace sus.Game.Skinning
{
    public partial class SkinnableSpriteText : SkinnableDrawable, IHasText
    {
        public SkinnableSpriteText(ISkinComponentLookup lookup, Func<ISkinComponentLookup, SpriteText> defaultImplementation, ConfineMode confineMode = ConfineMode.NoScaling)
            : base(lookup, defaultImplementation, confineMode)
        {
        }

        protected override void SkinChanged(ISkinSource skin)
        {
            base.SkinChanged(skin);

            if (Drawable is IHasText textDrawable)
                textDrawable.Text = Text;
        }

        private LocalisableString text;

        public LocalisableString Text
        {
            get => text;
            set
            {
                if (text == value)
                    return;

                text = value;

                if (Drawable is IHasText textDrawable)
                    textDrawable.Text = value;
            }
        }
    }
}
