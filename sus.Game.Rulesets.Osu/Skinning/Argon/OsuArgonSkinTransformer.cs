// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Graphics;
using sus.Game.Rulesets.Scoring;
using sus.Game.Skinning;

namespace sus.Game.Rulesets.Osu.Skinning.Argon
{
    public class OsuArgonSkinTransformer : SkinTransformer
    {
        public OsuArgonSkinTransformer(ISkin skin)
            : base(skin)
        {
        }

        public override Drawable? GetDrawableComponent(ISkinComponentLookup lookup)
        {
            bool isPro = Skin is ArgonProSkin;

            switch (lookup)
            {
                case SkinComponentLookup<HitResult> resultComponent:
                    HitResult result = resultComponent.Component;

                    // This should eventually be moved to a skin setting, when supported.
                    if (isPro && (result == HitResult.Great || result == HitResult.Perfect))
                        return Drawable.Empty();

                    switch (result)
                    {
                        case HitResult.IgnoreMiss:
                        case HitResult.LargeTickMiss:
                            return new ArgonJudgementPieceSliderTickMiss(result);

                        default:
                            return new ArgonJudgementPiece(result);
                    }

                case OsuSkinComponentLookup susComponent:
                    // TODO: Once everything is finalised, consider throwing UnsupportedSkinComponentException on missing entries.
                    switch (susComponent.Component)
                    {
                        case OsuSkinComponents.HitCircle:
                            return new ArgonMainCirclePiece(true);

                        case OsuSkinComponents.SliderHeadHitCircle:
                            return new ArgonMainCirclePiece(false);

                        case OsuSkinComponents.SliderBody:
                            return new ArgonSliderBody
                            {
                                BodyAlpha = isPro ? 0.92f : 0.98f
                            };

                        case OsuSkinComponents.SliderBall:
                            return new ArgonSliderBall();

                        case OsuSkinComponents.SliderFollowCircle:
                            return new ArgonFollowCircle();

                        case OsuSkinComponents.SliderScorePoint:
                            return new ArgonSliderScorePoint();

                        case OsuSkinComponents.SpinnerBody:
                            return new ArgonSpinner();

                        case OsuSkinComponents.ReverseArrow:
                            return new ArgonReverseArrow();

                        case OsuSkinComponents.FollowPoint:
                            return new ArgonFollowPoint();

                        case OsuSkinComponents.Cursor:
                            return new ArgonCursor();

                        case OsuSkinComponents.CursorTrail:
                            return new ArgonCursorTrail();
                    }

                    break;
            }

            return base.GetDrawableComponent(lookup);
        }
    }
}
