// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using sus.Framework.Allocation;
using sus.Framework.Bindables;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Cursor;
using sus.Framework.Localisation;
using sus.Game.Extensions;
using sus.Game.Graphics;
using sus.Game.Screens.Edit.Components.Timelines.Summary.Visualisations;

namespace sus.Game.Screens.Edit.Components.Timelines.Summary.Parts
{
    public partial class PreviewTimePart : TimelinePart
    {
        private readonly BindableInt previewTime = new BindableInt();

        protected override void LoadBeatmap(EditorBeatmap beatmap)
        {
            base.LoadBeatmap(beatmap);

            previewTime.UnbindAll();
            previewTime.BindTo(beatmap.PreviewTime);
            previewTime.BindValueChanged(t =>
            {
                Clear();

                if (t.NewValue >= 0)
                    Add(new PreviewTimeVisualisation(t.NewValue));
            }, true);
        }

        private partial class PreviewTimeVisualisation : PointVisualisation, IHasTooltip
        {
            public PreviewTimeVisualisation(double time)
                : base(time)
            {
                Alpha = 0.8f;

                // Display as a small circle on the middle line as to not clash with other displays.
                RelativeSizeAxes = Axes.None;
                Height = Width = 5;
            }

            [BackgroundDependencyLoader]
            private void load(OsuColour colours) => Colour = colours.Green1;

            public LocalisableString TooltipText => $"{StartTime.ToEditorFormattedString()} preview time";
        }
    }
}
