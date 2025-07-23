// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Audio.Track;
using sus.Framework.Graphics.Textures;
using sus.Game.IO;

namespace sus.Game.Beatmaps
{
    internal interface IBeatmapResourceProvider : IStorageResourceProvider
    {
        /// <summary>
        /// Retrieve a global large texture store, used for loading beatmap backgrounds.
        /// </summary>
        TextureStore LargeTextureStore { get; }

        /// <summary>
        /// Retrieve a global large texture store, used specifically for retrieving cropped beatmap panel backgrounds.
        /// </summary>
        TextureStore BeatmapPanelTextureStore { get; }

        /// <summary>
        /// Access a global track store for retrieving beatmap tracks from.
        /// </summary>
        ITrackStore Tracks { get; }
    }
}
