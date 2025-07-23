// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Collections.Generic;
using sus.Framework.Allocation;
using sus.Framework.Graphics.Containers;
using sus.Framework.Graphics.Pooling;
using sus.Game.Rulesets.Judgements;
using sus.Game.Rulesets.Objects;
using sus.Game.Rulesets.Scoring;

namespace sus.Game.Rulesets.UI
{
    /// <summary>
    /// Handles the task of preparing poolable drawable judgements for gameplay usage.
    /// </summary>
    /// <typeparam name="T">The drawable judgement type.</typeparam>
    public partial class JudgementPooler<T> : CompositeComponent
        where T : DrawableJudgement, new()
    {
        private readonly IDictionary<HitResult, DrawablePool<T>> poolDictionary = new Dictionary<HitResult, DrawablePool<T>>();

        private readonly IEnumerable<HitResult> usableHitResults;
        private readonly Action<T>? onJudgementInitialLoad;

        public JudgementPooler(IEnumerable<HitResult> usableHitResults, Action<T>? onJudgementInitialLoad = null)
        {
            this.usableHitResults = usableHitResults;
            this.onJudgementInitialLoad = onJudgementInitialLoad;
        }

        public T? Get(HitResult result, Action<T>? setupAction)
        {
            if (!poolDictionary.TryGetValue(result, out var pool))
                return null;

            return pool.Get(setupAction);
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            foreach (HitResult result in usableHitResults)
            {
                var pool = new DrawableJudgementPool(result, onJudgementInitialLoad);
                poolDictionary.Add(result, pool);
                AddInternal(pool);
            }
        }

        private partial class DrawableJudgementPool : DrawablePool<T>
        {
            private readonly HitResult result;
            private readonly Action<T>? onLoaded;

            public DrawableJudgementPool(HitResult result, Action<T>? onLoaded)
                : base(20)
            {
                this.result = result;
                this.onLoaded = onLoaded;
            }

            protected override T CreateNewDrawable()
            {
                var judgement = base.CreateNewDrawable();

                // just a placeholder to initialise the correct drawable hierarchy for this pool.
                judgement.Apply(new JudgementResult(new HitObject(), new Judgement()) { Type = result }, null);

                onLoaded?.Invoke(judgement);

                return judgement;
            }
        }
    }
}
