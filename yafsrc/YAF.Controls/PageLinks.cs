/* Yet Another Forum.NET
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
using System;
using System.Data;
using System.Web.UI;
using YAF.Classes;
using YAF.Classes.Data;
using YAF.Classes.Utils;

namespace YAF.Controls
{
  /// <summary>
  /// Summary description for PageLinks.
  /// </summary>
  public class PageLinks : BaseControl
  {
    /// <summary>
    /// Gets or sets LinkedPageLinkID.
    /// </summary>
    public string LinkedPageLinkID
    {
      get
      {
        if (ViewState["LinkedPageLinkID"] != null)
        {
          return ViewState["LinkedPageLinkID"].ToString();
        }

        return null;
      }

      set
      {
        ViewState["LinkedPageLinkID"] = value;
      }
    }

    /// <summary>
    /// Gets or sets PageLinkDT.
    /// </summary>
    protected DataTable PageLinkDT
    {
      get
      {
        if (ViewState["PageLinkDT"] != null)
        {
          return ViewState["PageLinkDT"] as DataTable;
        }

        return null;
      }

      set
      {
        ViewState["PageLinkDT"] = value;
      }
    }

    /// <summary>
    /// The add link.
    /// </summary>
    /// <param name="title">
    /// The title.
    /// </param>
    public void AddLink(string title)
    {
      AddLink(title, string.Empty);
    }

    /// <summary>
    /// The add link.
    /// </summary>
    /// <param name="title">
    /// The title.
    /// </param>
    /// <param name="url">
    /// The url.
    /// </param>
    public void AddLink(string title, string url)
    {
      DataTable dt = PageLinkDT;

      if (dt == null)
      {
        dt = new DataTable();
        dt.Columns.Add("Title", typeof (string));
        dt.Columns.Add("URL", typeof (string));
        PageLinkDT = dt;
      }

      DataRow dr = dt.NewRow();
      dr["Title"] = title;
      dr["URL"] = url;
      dt.Rows.Add(dr);
    }

    /// <summary>
    /// Clear all Links
    /// </summary>
    public void Clear()
    {
      if (PageLinkDT != null)
      {
        PageLinkDT = null;
      }
    }

    /// <summary>
    /// The add forum links.
    /// </summary>
    /// <param name="forumID">
    /// The forum id.
    /// </param>
    public void AddForumLinks(int forumID)
    {
      AddForumLinks(forumID, false);
    }

    /// <summary>
    /// The add forum links.
    /// </summary>
    /// <param name="forumID">
    /// The forum id.
    /// </param>
    /// <param name="noForumLink">
    /// The no forum link.
    /// </param>
    public void AddForumLinks(int forumID, bool noForumLink)
    {
      using (DataTable dtLinks = DB.forum_listpath(forumID))
      {
        foreach (DataRow row in dtLinks.Rows)
        {
          if (noForumLink && Convert.ToInt32(row["ForumID"]) == forumID)
          {
            AddLink(row["Name"].ToString(), string.Empty);
          }
          else
          {
            AddLink(row["Name"].ToString(), YafBuildLink.GetLink(ForumPages.topics, "f={0}", row["ForumID"]));
          }
        }
      }
    }

    /// <summary>
    /// The render.
    /// </summary>
    /// <param name="writer">
    /// The writer.
    /// </param>
    protected override void Render(HtmlTextWriter writer)
    {
      DataTable linkDataTable = null;

      if (!String.IsNullOrEmpty(LinkedPageLinkID))
      {
        // attempt to get access to the other control...
        var plControl = Parent.FindControl(LinkedPageLinkID) as PageLinks;

        if (plControl != null)
        {
          // use the other data stream...
          linkDataTable = plControl.PageLinkDT;
        }
      }
      else
      {
        // use the data table from this control...
        linkDataTable = PageLinkDT;
      }

      if (linkDataTable == null || linkDataTable.Rows.Count == 0)
      {
        return;
      }

      writer.WriteLine(String.Format(@"<div id=""{0}"" class=""yafPageLink"">", ClientID));

      bool bFirst = true;
      foreach (DataRow row in linkDataTable.Rows)
      {
        if (!bFirst)
        {
          writer.WriteLine(@"<span class=""linkSeperator"">&#187;</span>");
        }
        else
        {
          bFirst = false;
        }

        string title = HtmlEncode(row["Title"].ToString().Trim());
        string url = row["URL"].ToString().Trim();

        if (String.IsNullOrEmpty(url))
        {
          writer.WriteLine(String.Format(@"<span class=""currentPageLink"">{0}</span>", title));
        }
        else
        {
          writer.WriteLine(String.Format(@"<a href=""{0}"">{1}</a>", url, title));
        }
      }

      writer.WriteLine("</div>");
    }
  }
}