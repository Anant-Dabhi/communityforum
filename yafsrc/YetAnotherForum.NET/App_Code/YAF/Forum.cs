/* YetAnotherForum.NET
 * Copyright (C) 2006-2010 Jaben Cargman
 * http://www.yetanotherforum.net/
 * 
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation; either version 2
 * of the License, or (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
 */

namespace YAF
{
  using System;
  using System.IO;
  using System.Web.UI;
  using YAF.Classes;
  using YAF.Classes.Core;
  using YAF.Classes.Utils;
  using YAF.Controls;

  /// <summary>
  /// EventArgs class for the PageTitleSet event
  /// </summary>
  public class ForumPageTitleArgs : EventArgs
  {
    /// <summary>
    /// The _title.
    /// </summary>
    private string _title;

    /// <summary>
    /// Initializes a new instance of the <see cref="ForumPageTitleArgs"/> class.
    /// </summary>
    /// <param name="title">
    /// The title.
    /// </param>
    public ForumPageTitleArgs(string title)
    {
      this._title = title;
    }

    /// <summary>
    /// Gets Title.
    /// </summary>
    public string Title
    {
      get
      {
        return this._title;
      }
    }
  }

  /// <summary>
  /// EventArgs class for the YafBeforeForumPageLoad event
  /// </summary>
  public class YafBeforeForumPageLoad : EventArgs
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="YafBeforeForumPageLoad"/> class.
    /// </summary>
    public YafBeforeForumPageLoad()
    {
    }
  }

  /// <summary>
  /// EventArgs class for the YafForumPageReady event -- created for future options
  /// </summary>
  public class YafAfterForumPageLoad : EventArgs
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="YafAfterForumPageLoad"/> class.
    /// </summary>
    public YafAfterForumPageLoad()
    {
    }
  }

  /// <summary>
  /// Summary description for Forum.
  /// </summary>
  [ToolboxData("<{0}:Forum runat=\"server\"></{0}:Forum>")]
  public class Forum : UserControl
  {
    /// <summary>
    /// The _current forum page.
    /// </summary>
    private ForumPage _currentForumPage;

    /// <summary>
    /// The _footer.
    /// </summary>
    private Footer _footer;

    /// <summary>
    /// The _header.
    /// </summary>
    private Header _header;

    /// <summary>
    /// The _orig footer client id.
    /// </summary>
    private string _origFooterClientID;

    /// <summary>
    /// The _orig header client id.
    /// </summary>
    private string _origHeaderClientID;

    /// <summary>
    /// The _page.
    /// </summary>
    private ForumPages _page;

    /// <summary>
    /// Initializes a new instance of the <see cref="Forum"/> class.
    /// </summary>
    public Forum()
    {
      Load += new EventHandler(Forum_Load);
      Init += new EventHandler(Forum_Init);
      Unload += new EventHandler(Forum_Unload);

      // setup header/footer
      this._header = new Header();
      this._footer = new Footer();
      this._origHeaderClientID = this._header.ClientID;
      this._origFooterClientID = this._footer.ClientID;

      // init the modules and run them immediately...
      YafContext.Current.BaseModuleManager.Load();
      YafContext.Current.BaseModuleManager.CallInitModules(this);
    }

    /// <summary>
    /// The forum header control
    /// </summary>
    public Header Header
    {
      get
      {
        return this._header;
      }

      set
      {
        this._header = value;
      }
    }

    /// <summary>
    /// The forum footer control
    /// </summary>
    public Footer Footer
    {
      get
      {
        return this._footer;
      }

      set
      {
        this._footer = value;
      }
    }

    /// <summary>
    /// UserID for the current User (Read Only)
    /// </summary>
    public int PageUserID
    {
      get
      {
        return YafContext.Current.PageUserID;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether Popup.
    /// </summary>
    public bool Popup
    {
      get
      {
        return YafControlSettings.Current.Popup;
      }

      set
      {
        YafControlSettings.Current.Popup = value;
      }
    }

    /// <summary>
    /// UserName for the current User (Read Only)
    /// </summary>
    public string PageUserName
    {
      get
      {
        if (YafContext.Current.User == null)
        {
          return "Guest";
        }

        return YafContext.Current.User.UserName;
      }
    }

    /// <summary>
    /// Get or sets the Board ID for this instance of the forum control, overriding the value defined in app.config.
    /// </summary>
    public int BoardID
    {
      get
      {
        return YafControlSettings.Current.BoardID;
      }

      set
      {
        YafControlSettings.Current.BoardID = value;
      }
    }

    /// <summary>
    /// Gets or sets the CategoryID for this instance of the forum control
    /// </summary>
    public int CategoryID
    {
      get
      {
        return YafControlSettings.Current.CategoryID;
      }

      set
      {
        YafControlSettings.Current.CategoryID = value;
      }
    }

    /// <summary>
    /// Gets or sets LockedForum.
    /// </summary>
    public int LockedForum
    {
      get
      {
        return YafControlSettings.Current.LockedForum;
      }

      set
      {
        YafControlSettings.Current.LockedForum = value;
      }
    }

    /// <summary>
    /// The page title set.
    /// </summary>
    public event EventHandler<ForumPageTitleArgs> PageTitleSet;

    /// <summary>
    /// The before forum page load.
    /// </summary>
    public event EventHandler<YafBeforeForumPageLoad> BeforeForumPageLoad;

    /// <summary>
    /// The after forum page load.
    /// </summary>
    public event EventHandler<YafAfterForumPageLoad> AfterForumPageLoad;

    /// <summary>
    /// The forum_ init.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    private void Forum_Init(object sender, EventArgs e)
    {
      // handle script manager first...
      if (ScriptManager.GetCurrent(Page) == null)
      {
        // add a script manager since one doesn't exist...
        var yafScriptManager = new ScriptManager();
        yafScriptManager.ID = "YafScriptManager";
        yafScriptManager.EnablePartialRendering = true;
        Controls.Add(yafScriptManager);
      }
    }

    /// <summary>
    /// The forum_ unload.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    private void Forum_Unload(object sender, EventArgs e)
    {
      // make sure the YafContext is disposed of...
      YafContext.Current.Dispose();
    }

    /// <summary>
    /// The forum_ load.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    /// <exception cref="ApplicationException">
    /// </exception>
    private void Forum_Load(object sender, EventArgs e)
    {
      // context is ready to be loaded, call the before page load event...
      if (BeforeForumPageLoad != null)
      {
        BeforeForumPageLoad(this, new YafBeforeForumPageLoad());
      }

      // "forum load" should be done by now, load the user and page...
      int userId = YafContext.Current.PageUserID;

      // get the current page...
      string src = GetPageSource();

      try
      {
        this._currentForumPage = (ForumPage) LoadControl(src);
        this._currentForumPage.ForumFooter = this._footer;
        this._currentForumPage.ForumHeader = this._header;

        // don't allow as a popup if it's not allowed by the page...
        if (!this._currentForumPage.AllowAsPopup && Popup)
        {
          Popup = false;
        }

        // set the YafContext ForumPage...
        YafContext.Current.CurrentForumPage = this._currentForumPage;

        // add the header control before the page rendering...
        if (!Popup && YafContext.Current.Settings.LockedForum == 0 && this._origHeaderClientID == this._header.ClientID)
        {
          Controls.AddAt(0, this._header);
        }

        Controls.Add(this._currentForumPage);

        // add the footer control after the page...
        if (!Popup && YafContext.Current.Settings.LockedForum == 0 && this._origFooterClientID == this._footer.ClientID)
        {
          Controls.Add(this._footer);
        }

        // load plugins/functionality modules
        if (AfterForumPageLoad != null)
        {
          AfterForumPageLoad(this, new YafAfterForumPageLoad());
        }
      }
      catch (FileNotFoundException)
      {
        throw new ApplicationException("Failed to load " + src + ".");
      }
    }

    /// <summary>
    /// The get page source.
    /// </summary>
    /// <returns>
    /// The get page source.
    /// </returns>
    private string GetPageSource()
    {
      string m_baseDir = YafForumInfo.ForumFileRoot;

      if (Request.QueryString["g"] != null)
      {
        try
        {
          this._page = (ForumPages) Enum.Parse(typeof (ForumPages), Request.QueryString["g"], true);
        }
        catch (Exception)
        {
          this._page = ForumPages.forum;
        }
      }
      else
      {
        this._page = ForumPages.forum;
      }

      if (!ValidPage(this._page))
      {
        YafBuildLink.Redirect(ForumPages.topics, "f={0}", LockedForum);
      }

      string src = string.Format("{0}pages/{1}.ascx", m_baseDir, this._page);

      string controlOverride = YafContext.Current.Theme.GetItem("PAGE_OVERRIDE", this._page.ToString(), null);

      if (!String.IsNullOrEmpty(controlOverride))
      {
        src = controlOverride;
      }

      if (src.IndexOf("/moderate_") >= 0)
      {
        src = src.Replace("/moderate_", "/moderate/");
      }

      if (src.IndexOf("/admin_") >= 0)
      {
        src = src.Replace("/admin_", "/admin/");
      }

      if (src.IndexOf("/help_") >= 0)
      {
        src = src.Replace("/help_", "/help/");
      }

      return src;
    }

    /// <summary>
    /// The render.
    /// </summary>
    /// <param name="writer">
    /// The writer.
    /// </param>
    protected override void Render(HtmlTextWriter writer)
    {
      // wrap the forum in one main div and then a page div for better CSS selection
      writer.WriteLine();
      writer.Write(String.Format(@"<div class=""yafnet"" id=""{0}"">", ClientID));
      writer.Write(String.Format(@"<div id=""yafpage_{0}"">", this._page.ToString()));

      // render the forum
      base.Render(writer);

      writer.WriteLine("</div></div>");
    }

    /// <summary>
    /// Called when the forum control sets it's Page Title
    /// </summary>
    /// <param name="sender">
    /// </param>
    /// <param name="e">
    /// </param>
    public void FirePageTitleSet(object sender, ForumPageTitleArgs e)
    {
      if (PageTitleSet != null)
      {
        PageTitleSet(this, e);
      }
    }

    /// <summary>
    /// The valid page.
    /// </summary>
    /// <param name="forumPage">
    /// The forum page.
    /// </param>
    /// <returns>
    /// The valid page.
    /// </returns>
    private bool ValidPage(ForumPages forumPage)
    {
      if (LockedForum == 0)
      {
        return true;
      }

      if (forumPage == ForumPages.forum || forumPage == ForumPages.mytopics || forumPage == ForumPages.activeusers)
      {
        return false;
      }

      if (forumPage == ForumPages.cp_editprofile || forumPage == ForumPages.cp_pm || forumPage == ForumPages.cp_message || forumPage == ForumPages.cp_profile ||
          forumPage == ForumPages.cp_signature || forumPage == ForumPages.cp_subscriptions)
      {
        return false;
      }

      if (forumPage == ForumPages.pmessage)
      {
        return false;
      }

      return true;
    }
  }
}