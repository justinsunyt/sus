// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using sus.Game.Skinning;

namespace sus.Game.Rulesets.Osu.Skinning.Legacy
{
    public partial class LegacySmokeSegment : SmokeSegment
    {
        [BackgroundDependencyLoader]
        private void load(ISkinSource skin)
        {
            base.LoadComplete();

            Texture = skin.GetTexture("cursor-smoke");
        }
    }
}
