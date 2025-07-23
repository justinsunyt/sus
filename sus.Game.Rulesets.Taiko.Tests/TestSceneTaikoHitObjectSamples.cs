// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Reflection;
using NUnit.Framework;
using osu.Framework.IO.Stores;
using sus.Game.Tests.Beatmaps;

namespace sus.Game.Rulesets.Taiko.Tests
{
    public partial class TestSceneTaikoHitObjectSamples : HitObjectSampleTest
    {
        protected override Ruleset CreatePlayerRuleset() => new TaikoRuleset();

        protected override IResourceStore<byte[]> RulesetResources => new DllResourceStore(Assembly.GetAssembly(typeof(TestSceneTaikoHitObjectSamples)));

        [TestCase("taiko-normal-hitnormal2", "taiko-normal-hitnormal")]
        [TestCase("hitnormal", "hitnormal")]
        public void TestDefaultCustomSampleFromBeatmap(string beatmapSkinSampleName, string userSkinSampleName)
        {
            SetupSkins(beatmapSkinSampleName, userSkinSampleName);

            CreateTestWithBeatmap("taiko-hitobject-beatmap-custom-sample-bank.sus");

            AssertBeatmapLookup(beatmapSkinSampleName);
        }

        [TestCase("", "taiko-normal-hitnormal")]
        [TestCase("taiko-normal-hitnormal", "taiko-normal-hitnormal")]
        [TestCase("", "hitnormal")]
        public void TestDefaultCustomSampleFromUserSkinFallback(string beatmapSkinSampleName, string userSkinSampleName)
        {
            SetupSkins(beatmapSkinSampleName, userSkinSampleName);

            CreateTestWithBeatmap("taiko-hitobject-beatmap-custom-sample-bank.sus");

            AssertUserLookup(userSkinSampleName);
        }

        [TestCase("taiko-normal-hitnormal2")]
        public void TestUserSkinLookupIgnoresSampleBank(string unwantedSample)
        {
            SetupSkins(string.Empty, unwantedSample);

            CreateTestWithBeatmap("taiko-hitobject-beatmap-custom-sample-bank.sus");

            AssertNoLookup(unwantedSample);
        }
    }
}
