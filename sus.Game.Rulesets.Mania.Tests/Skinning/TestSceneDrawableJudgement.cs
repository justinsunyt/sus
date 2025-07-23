// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Linq;
using sus.Framework.Extensions;
using sus.Framework.Graphics;
using sus.Framework.Testing;
using sus.Game.Rulesets.Judgements;
using sus.Game.Rulesets.Mania.Scoring;
using sus.Game.Rulesets.Mania.Skinning.Legacy;
using sus.Game.Rulesets.Mania.UI;
using sus.Game.Rulesets.Objects;
using sus.Game.Rulesets.Scoring;

namespace sus.Game.Rulesets.Mania.Tests.Skinning
{
    public partial class TestSceneDrawableJudgement : ManiaSkinnableTestScene
    {
        public TestSceneDrawableJudgement()
        {
            var hitWindows = new ManiaHitWindows();

            foreach (HitResult result in Enum.GetValues(typeof(HitResult)).OfType<HitResult>().Skip(1))
            {
                if (hitWindows.IsHitResultAllowed(result))
                {
                    AddStep("Show " + result.GetDescription(), () =>
                    {
                        SetContents(_ =>
                        {
                            var drawableManiaJudgement = new DrawableManiaJudgement
                            {
                                Anchor = Anchor.Centre,
                                Origin = Anchor.Centre,
                            };

                            drawableManiaJudgement.Apply(new JudgementResult(new HitObject { StartTime = Time.Current }, new Judgement())
                            {
                                Type = result
                            }, null);

                            return drawableManiaJudgement;
                        });

                        // for test purposes, undo the Y adjustment related to the `ScorePosition` legacy positioning config value
                        // (see `LegacyManiaJudgementPiece.load()`).
                        // this prevents the judgements showing somewhere below or above the bounding box of the judgement.
                        foreach (var legacyPiece in this.ChildrenOfType<LegacyManiaJudgementPiece>())
                            legacyPiece.Y = 0;
                    });
                }
            }
        }
    }
}
