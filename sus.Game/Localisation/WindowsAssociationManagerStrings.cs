// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Localisation;

namespace sus.Game.Localisation
{
    public static class WindowsAssociationManagerStrings
    {
        private const string prefix = @"sus.Game.Resources.Localisation.WindowsAssociationManager";

        /// <summary>
        /// "sus! Beatmap"
        /// </summary>
        public static LocalisableString OsuBeatmap => new TranslatableString(getKey(@"sus_beatmap"), @"sus! Beatmap");

        /// <summary>
        /// "sus! Replay"
        /// </summary>
        public static LocalisableString OsuReplay => new TranslatableString(getKey(@"sus_replay"), @"sus! Replay");

        /// <summary>
        /// "sus! Skin"
        /// </summary>
        public static LocalisableString OsuSkin => new TranslatableString(getKey(@"sus_skin"), @"sus! Skin");

        /// <summary>
        /// "sus!"
        /// </summary>
        public static LocalisableString OsuProtocol => new TranslatableString(getKey(@"sus_protocol"), @"sus!");

        /// <summary>
        /// "sus! Multiplayer"
        /// </summary>
        public static LocalisableString OsuMultiplayer => new TranslatableString(getKey(@"sus_multiplayer"), @"sus! Multiplayer");

        private static string getKey(string key) => $@"{prefix}:{key}";
    }
}