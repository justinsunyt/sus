// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

#nullable disable

using sus.Framework.Allocation;
using sus.Framework.Bindables;
using sus.Framework.Graphics;
using sus.Game.Overlays.Settings;
using sus.Game.Rulesets.Edit.Checks.Components;

namespace sus.Game.Screens.Edit.Verify
{
    internal partial class VisibilitySection : EditorRoundedScreenSettingsSection
    {
        private readonly IssueType[] configurableIssueTypes =
        {
            IssueType.Warning,
            IssueType.Error,
            IssueType.Negligible
        };

        private BindableList<IssueType> hiddenIssueTypes;

        protected override string HeaderText => "Visibility";

        [BackgroundDependencyLoader]
        private void load(VerifyScreen verify)
        {
            hiddenIssueTypes = verify.HiddenIssueTypes.GetBoundCopy();

            foreach (IssueType issueType in configurableIssueTypes)
            {
                var checkbox = new SettingsCheckbox
                {
                    Anchor = Anchor.CentreLeft,
                    Origin = Anchor.CentreLeft,
                    LabelText = issueType.ToString(),
                    Current = { Default = !hiddenIssueTypes.Contains(issueType) }
                };

                checkbox.Current.SetDefault();
                checkbox.Current.BindValueChanged(state =>
                {
                    if (!state.NewValue)
                        hiddenIssueTypes.Add(issueType);
                    else
                        hiddenIssueTypes.Remove(issueType);
                });

                Flow.Add(checkbox);
            }
        }
    }
}
