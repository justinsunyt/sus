// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using sus.Framework.Configuration.Tracking;

namespace sus.Game.Rulesets.Configuration
{
    public interface IRulesetConfigManager : ITrackableConfigManager, IDisposable
    {
    }
}
