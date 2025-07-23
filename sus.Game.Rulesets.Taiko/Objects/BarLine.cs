// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Bindables;
using sus.Game.Rulesets.Judgements;
using sus.Game.Rulesets.Objects;

namespace sus.Game.Rulesets.Taiko.Objects
{
    public class BarLine : TaikoHitObject, IBarLine
    {
        private HitObjectProperty<bool> major;

        public Bindable<bool> MajorBindable => major.Bindable;

        public bool Major
        {
            get => major.Value;
            set => major.Value = value;
        }

        public override Judgement CreateJudgement() => new IgnoreJudgement();
    }
}
