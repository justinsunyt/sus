// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Platform;
using sus.Game.Skinning;

namespace sus.Game.Database
{
    public class LegacySkinExporter : LegacyArchiveExporter<SkinInfo>
    {
        public LegacySkinExporter(Storage storage)
            : base(storage)
        {
        }

        protected override bool UseFixedEncoding => false;

        protected override string FileExtension => @".osk";
    }
}
