// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Localisation;
using sus.Game.Localisation;
using sus.Game.Rulesets.UI.Scrolling;

namespace sus.Game.Rulesets.Mania.UI
{
    public enum ManiaScrollingDirection
    {
        [LocalisableDescription(typeof(RulesetSettingsStrings), nameof(RulesetSettingsStrings.ScrollingDirectionUp))]
        Up = ScrollingDirection.Up,

        [LocalisableDescription(typeof(RulesetSettingsStrings), nameof(RulesetSettingsStrings.ScrollingDirectionDown))]
        Down = ScrollingDirection.Down
    }
}
