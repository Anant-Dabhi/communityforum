/* Yet Another Forum.NET
 * Copyright (C) 2003-2005 Bjørnar Henden
 * Copyright (C) 2006-2013 Jaben Cargman
 * Copyright (C) 2014-2021 Ingo Herbote
 * https://www.yetanotherforum.net/
 *
 * Licensed to the Apache Software Foundation (ASF) under one
 * or more contributor license agreements.  See the NOTICE file
 * distributed with this work for additional information
 * regarding copyright ownership.  The ASF licenses this file
 * to you under the Apache License, Version 2.0 (the
 * "License"); you may not use this file except in compliance
 * with the License.  You may obtain a copy of the License at

 * https://www.apache.org/licenses/LICENSE-2.0

 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */
namespace YAF.Modules
{
    #region Using

    using System;
    using System.Linq;
    using System.Text;
    using System.Web.UI.HtmlControls;

    using YAF.Types;
    using YAF.Types.Attributes;
    using YAF.Types.Constants;
    using YAF.Types.Extensions;
    using YAF.Types.Interfaces;
    using YAF.Utils.Helpers;
    using YAF.Web.Controls;
    using YAF.Web.EventsArgs;

    #endregion

    /// <summary>
    /// Page Title Module
    /// </summary>
    [YafModule("Page Title Module", "Tiny Gecko", 1)]
    public class PageTitleForumModule : SimpleBaseForumModule
    {
        #region Public Methods

        /// <summary>
        /// The initialization after page.
        /// </summary>
        public override void InitAfterPage()
        {
            this.CurrentForumPage.Load += this.ForumPage_Load;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Handles the Load event of the ForumPage control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ForumPage_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            this.GeneratePageTitle();
        }

        /// <summary>
        /// Creates this pages title and fires a PageTitleSet event if one is set
        /// </summary>
        private void GeneratePageTitle()
        {
            var head = this.ForumControl.Page.Header
                       ?? this.CurrentForumPage.FindControlRecursiveBothAs<HtmlHead>("YafHead");

            if (head == null)
            {
                return;
            }

            // compute page title..
            var title = new StringBuilder();

            var pageString = string.Empty;

            if (this.ForumPageType is ForumPages.Posts or ForumPages.topics)
            {
                // get current page...
                var currentPager = this.CurrentForumPage.FindControlAs<Pager>("Pager");

                if (currentPager != null && currentPager.CurrentPageIndex != 0)
                {
                    pageString = this.PageContext.BoardSettings.PagingTitleTemplate.Replace(
                        "{paging}",
                        $"{this.GetText("COMMON", "PAGE")} {currentPager.CurrentPageIndex + 1}");
                }
            }

            if (!this.PageContext.CurrentForumPage.IsAdminPage)
            {
                switch (this.ForumPageType)
                {
                    case ForumPages.Posts:
                        if (this.PageContext.PageTopicID != 0)
                        {
                            // Tack on the topic we're viewing
                            title.Append(
                                this.Get<IBadWordReplace>().Replace(this.PageContext.PageTopicName.Truncate(80)));
                        }

                        // Append Current Page
                        title.Append(pageString);

                        break;
                    case ForumPages.topics:
                        if (this.PageContext.PageForumName != string.Empty)
                        {
                            // Tack on the forum we're viewing
                            title.Append(this.CurrentForumPage.HtmlEncode(this.PageContext.PageForumName.Truncate(80)));
                        }

                        // Append Current Page
                        title.Append(pageString);

                        break;
                    case ForumPages.forum:
                        if (this.PageContext.PageCategoryName != string.Empty)
                        {
                            // Tack on the forum we're viewing
                            title.Append(
                                this.CurrentForumPage.HtmlEncode(this.PageContext.PageCategoryName.Truncate(80)));
                        }

                        break;
                    default:
                        var pageLinks = this.CurrentForumPage.FindControlAs<PageLinks>("PageLinks");

                        var activePageLink = pageLinks?.PageLinkList?.FirstOrDefault(link => link.URL.IsNotSet());

                        if (activePageLink != null)
                        {
                            // Tack on the forum we're viewing
                            title.Append(this.CurrentForumPage.HtmlEncode(activePageLink.Title.Truncate(80)));
                        }

                        break;
                }
            }
            else
            {
                var pageLinks = this.CurrentForumPage.FindControlAs<PageLinks>("PageLinks");

                var activePageLink = pageLinks?.PageLinkList?.FirstOrDefault(link => link.URL.IsNotSet());

                if (activePageLink != null)
                {
                    // Tack on the forum we're viewing
                    title.Append(this.CurrentForumPage.HtmlEncode(activePageLink.Title));
                }
            }

            var boardName = this.CurrentForumPage.HtmlEncode(this.PageContext.BoardSettings.Name);

            var forumPageTitle =
                title.Length > 0
                    ? this.PageContext.BoardSettings.TitleTemplate.Replace("{title}", title.ToString())
                        .Replace("{boardName}", boardName)
                    : boardName;

            head.Title = forumPageTitle;

            this.ForumControl.FirePageTitleSet(this, new ForumPageTitleArgs(forumPageTitle));
        }

        #endregion
    }
}