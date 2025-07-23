// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

#nullable disable

using System;
using System.Linq;
using sus.Framework.Bindables;
using sus.Framework.Graphics;
using sus.Framework.Localisation;
using sus.Game.Graphics;
using sus.Game.Localisation;
using sus.Game.Online.API.Requests.Responses;
using sus.Game.Resources.Localisation.Web;

namespace sus.Game.Overlays.Wiki
{
    public partial class WikiHeader : BreadcrumbControlOverlayHeader
    {
        public static LocalisableString IndexPageString => LayoutStrings.HeaderHelpIndex;

        public readonly Bindable<APIWikiPage> WikiPageData = new Bindable<APIWikiPage>();

        public Action ShowIndexPage;
        public Action ShowParentPage;

        public WikiHeader()
        {
            TabControl.AddItem(IndexPageString);
            Current.Value = IndexPageString;

            WikiPageData.BindValueChanged(onWikiPageChange);
            Current.BindValueChanged(onCurrentChange);
        }

        private void onWikiPageChange(ValueChangedEvent<APIWikiPage> e)
        {
            if (e.NewValue == null)
                return;

            TabControl.Clear();
            Current.Value = null;

            TabControl.AddItem(IndexPageString);

            if (e.NewValue.Path == WikiOverlay.INDEX_PATH)
            {
                Current.Value = IndexPageString;
                return;
            }

            if (e.NewValue.Subtitle != null)
                TabControl.AddItem(e.NewValue.Subtitle);

            TabControl.AddItem(e.NewValue.Title);
            Current.Value = e.NewValue.Title;
        }

        private void onCurrentChange(ValueChangedEvent<LocalisableString?> e)
        {
            if (e.NewValue == TabControl.Items.LastOrDefault())
                return;

            if (e.NewValue == IndexPageString)
            {
                ShowIndexPage?.Invoke();
                return;
            }

            ShowParentPage?.Invoke();
        }

        protected override Drawable CreateBackground() => new OverlayHeaderBackground(@"Headers/wiki");

        protected override OverlayTitle CreateTitle() => new WikiHeaderTitle();

        private partial class WikiHeaderTitle : OverlayTitle
        {
            public WikiHeaderTitle()
            {
                Title = PageTitleStrings.MainWikiControllerDefault;
                Description = NamedOverlayComponentStrings.WikiDescription;
                Icon = OsuIcon.Wiki;
            }
        }
    }
}
