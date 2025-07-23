// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Diagnostics;
using sus.Game.Rulesets.Mods;
using sus.Game.Rulesets.Objects.Drawables;
using sus.Game.Rulesets.Osu.Objects;
using sus.Game.Rulesets.Osu.Objects.Drawables;
using sus.Game.Rulesets.Osu.UI;
using sus.Game.Rulesets.UI;

namespace sus.Game.Rulesets.Osu.Mods
{
    public class OsuModBarrelRoll : ModBarrelRoll<OsuHitObject>, IApplicableToDrawableHitObject
    {
        public override Type[] IncompatibleMods => new[] { typeof(OsuModBubbles) };

        public void ApplyToDrawableHitObject(DrawableHitObject d)
        {
            d.OnUpdate += _ =>
            {
                switch (d)
                {
                    case DrawableHitCircle circle:
                        circle.CirclePiece.Rotation = -CurrentRotation;
                        break;
                }
            };
        }

        public override void Update(Playfield playfield)
        {
            base.Update(playfield);
            OsuPlayfield susPlayfield = (OsuPlayfield)playfield;
            Debug.Assert(susPlayfield.Cursor != null);

            susPlayfield.Cursor.ActiveCursor.Rotation = -CurrentRotation;
        }
    }
}
