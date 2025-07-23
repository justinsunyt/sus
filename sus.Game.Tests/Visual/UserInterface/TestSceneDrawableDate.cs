// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Containers;
using sus.Framework.Graphics.Shapes;
using sus.Game.Graphics;
using susTK;
using susTK.Graphics;

namespace sus.Game.Tests.Visual.UserInterface
{
    public partial class TestSceneDrawableDate : OsuTestScene
    {
        public TestSceneDrawableDate()
        {
            Child = new FillFlowContainer
            {
                Direction = FillDirection.Vertical,
                AutoSizeAxes = Axes.Both,
                Origin = Anchor.Centre,
                Anchor = Anchor.Centre,
                Children = new Drawable[]
                {
                    new PokeyDrawableDate(DateTimeOffset.Now.Subtract(TimeSpan.FromSeconds(60))),
                    new PokeyDrawableDate(DateTimeOffset.Now.Subtract(TimeSpan.FromSeconds(55))),
                    new PokeyDrawableDate(DateTimeOffset.Now.Subtract(TimeSpan.FromSeconds(50))),
                    new PokeyDrawableDate(DateTimeOffset.Now),
                    new PokeyDrawableDate(DateTimeOffset.Now.Add(TimeSpan.FromSeconds(60))),
                    new PokeyDrawableDate(DateTimeOffset.Now.Add(TimeSpan.FromSeconds(65))),
                    new PokeyDrawableDate(DateTimeOffset.Now.Add(TimeSpan.FromSeconds(70))),
                }
            };
        }

        private partial class PokeyDrawableDate : CompositeDrawable
        {
            public PokeyDrawableDate(DateTimeOffset date)
            {
                const float box_size = 10;

                DrawableDate drawableDate;
                Box flash;

                AutoSizeAxes = Axes.Both;
                InternalChildren = new Drawable[]
                {
                    flash = new Box
                    {
                        Colour = Color4.Yellow,
                        Size = new Vector2(box_size),
                        Anchor = Anchor.CentreLeft,
                        Origin = Anchor.CentreLeft,
                        Alpha = 0
                    },
                    drawableDate = new DrawableDate(date)
                    {
                        X = box_size + 2,
                    }
                };

                drawableDate.Current.ValueChanged += _ => flash.FadeOutFromOne(500);
            }
        }
    }
}
