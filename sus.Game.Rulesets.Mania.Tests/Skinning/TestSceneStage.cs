// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using sus.Game.Rulesets.Mania.Beatmaps;
using sus.Game.Rulesets.Mania.UI;

namespace sus.Game.Rulesets.Mania.Tests.Skinning
{
    public partial class TestSceneStage : ManiaSkinnableTestScene
    {
        [BackgroundDependencyLoader]
        private void load()
        {
            SetContents(_ =>
            {
                ManiaAction action = ManiaAction.Key1;

                return new ManiaInputManager(new ManiaRuleset().RulesetInfo, 4)
                {
                    Child = new Stage(0, new StageDefinition(4), ref action)
                };
            });
        }
    }
}
