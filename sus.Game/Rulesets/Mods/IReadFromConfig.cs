// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Game.Configuration;

namespace sus.Game.Rulesets.Mods
{
    /// <summary>
    /// An interface for mods that require reading access to the sus! configuration.
    /// </summary>
    public interface IReadFromConfig
    {
        void ReadFromConfig(OsuConfigManager config);
    }
}
