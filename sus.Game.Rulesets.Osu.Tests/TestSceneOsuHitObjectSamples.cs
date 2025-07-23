// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Reflection;
using NUnit.Framework;
using sus.Framework.IO.Stores;
using sus.Game.Tests.Beatmaps;

namespace sus.Game.Rulesets.Osu.Tests
{
    public partial class TestSceneOsuHitObjectSamples : HitObjectSampleTest
    {
        protected override Ruleset CreatePlayerRuleset() => new OsuRuleset();

        protected override IResourceStore<byte[]> RulesetResources => new DllResourceStore(Assembly.GetAssembly(typeof(TestSceneOsuHitObjectSamples)));

        [TestCase("normal-hitnormal2", "normal-hitnormal")]
        [TestCase("hitnormal", "hitnormal")]
        public void TestDefaultCustomSampleFromBeatmap(string beatmapSkinSampleName, string userSkinSampleName)
        {
            SetupSkins(beatmapSkinSampleName, userSkinSampleName);

            CreateTestWithBeatmap("sus-hitobject-beatmap-custom-sample-bank.sus");

            AssertBeatmapLookup(beatmapSkinSampleName);
        }

        [TestCase("", "normal-hitnormal")]
        [TestCase("normal-hitnormal", "normal-hitnormal")]
        [TestCase("", "hitnormal")]
        public void TestDefaultCustomSampleFromUserSkinFallback(string beatmapSkinSampleName, string userSkinSampleName)
        {
            SetupSkins(beatmapSkinSampleName, userSkinSampleName);

            CreateTestWithBeatmap("sus-hitobject-beatmap-custom-sample-bank.sus");

            AssertUserLookup(userSkinSampleName);
        }

        [TestCase("normal-hitnormal2")]
        public void TestUserSkinLookupIgnoresSampleBank(string unwantedSample)
        {
            SetupSkins(string.Empty, unwantedSample);

            CreateTestWithBeatmap("sus-hitobject-beatmap-custom-sample-bank.sus");

            AssertNoLookup(unwantedSample);
        }
    }
}
