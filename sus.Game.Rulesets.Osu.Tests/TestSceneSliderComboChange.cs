// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Bindables;
using sus.Game.Rulesets.Osu.Objects;
using sus.Game.Rulesets.Osu.Objects.Drawables;

namespace sus.Game.Rulesets.Osu.Tests
{
    public partial class TestSceneSliderComboChange : TestSceneSlider
    {
        private readonly Bindable<int> comboIndex = new Bindable<int>();

        protected override void LoadComplete()
        {
            base.LoadComplete();
            Scheduler.AddDelayed(() => comboIndex.Value++, 250, true);
        }

        protected override DrawableSlider CreateDrawableSlider(Slider slider)
        {
            slider.ComboIndexBindable.BindTo(comboIndex);
            slider.IndexInCurrentComboBindable.BindTo(comboIndex);

            return base.CreateDrawableSlider(slider);
        }
    }
}
