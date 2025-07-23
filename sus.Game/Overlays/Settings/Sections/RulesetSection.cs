// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Linq;
using sus.Framework.Allocation;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Sprites;
using sus.Framework.Localisation;
using sus.Framework.Logging;
using sus.Game.Graphics;
using sus.Game.Localisation;
using sus.Game.Rulesets;

namespace sus.Game.Overlays.Settings.Sections
{
    public partial class RulesetSection : SettingsSection
    {
        public override LocalisableString Header => RulesetSettingsStrings.Rulesets;

        public override Drawable CreateIcon() => new SpriteIcon
        {
            Icon = OsuIcon.Rulesets
        };

        [BackgroundDependencyLoader]
        private void load(RulesetStore rulesets)
        {
            foreach (Ruleset ruleset in rulesets.AvailableRulesets.Select(info => info.CreateInstance()))
            {
                try
                {
                    SettingsSubsection? section = ruleset.CreateSettings();

                    if (section != null)
                        Add(section);
                }
                catch
                {
                    Logger.Log($"Failed to load ruleset settings for {ruleset.RulesetInfo.Name}. Please check for an update from the developer.", level: LogLevel.Error);
                }
            }
        }
    }
}
