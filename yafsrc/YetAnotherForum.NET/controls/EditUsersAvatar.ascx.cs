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
namespace YAF.Controls
{
  using System;
  using System.Data;
  using System.Drawing;
  using System.Drawing.Imaging;
  using System.IO;
  using System.Security.Cryptography;
  using System.Text;
  using YAF.Classes;
  using YAF.Classes.Core;
  using YAF.Classes.Data;
  using YAF.Classes.Utils;

  /// <summary>
  /// The edit users avatar.
  /// </summary>
  public partial class EditUsersAvatar : BaseUserControl
  {
    /// <summary>
    /// The admin edit mode.
    /// </summary>
    private bool _adminEditMode = false;

    /// <summary>
    /// The current user id.
    /// </summary>
    private int _currentUserID;

    /// <summary>
    /// Gets or sets a value indicating whether InAdminPages.
    /// </summary>
    public bool InAdminPages
    {
      get
      {
        return this._adminEditMode;
      }

      set
      {
        this._adminEditMode = value;
      }
    }

    /// <summary>
    /// The page_ load.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void Page_Load(object sender, EventArgs e)
    {
      PageContext.QueryIDs = new QueryStringIDHelper("u");

      if (this._adminEditMode && PageContext.IsAdmin && PageContext.QueryIDs.ContainsKey("u"))
      {
        this._currentUserID = (int) PageContext.QueryIDs["u"];
      }
      else
      {
        this._currentUserID = PageContext.PageUserID;
      }

      if (!IsPostBack)
      {
        // check if it's a link from the avatar picker
        if (Request.QueryString["av"] != null)
        {
          // save the avatar right now...
          DB.user_saveavatar(
            this._currentUserID, string.Format("{0}{1}/{2}", YafForumInfo.ForumBaseUrl, YafBoardFolders.Current.Avatars, Request.QueryString["av"]), null, null);

          // clear the cache for this user...
          UserMembershipHelper.ClearCacheForUserId(this._currentUserID);
        }

        this.UpdateRemote.Text = PageContext.Localization.GetText("COMMON", "UPDATE");
        this.UpdateUpload.Text = PageContext.Localization.GetText("COMMON", "UPDATE");
        this.Back.Text = PageContext.Localization.GetText("COMMON", "BACK");

        this.NoAvatar.Text = PageContext.Localization.GetText("CP_EDITAVATAR", "NOAVATAR");

        this.DeleteAvatar.Text = PageContext.Localization.GetText("CP_EDITAVATAR", "AVATARDELETE");
        this.DeleteAvatar.Attributes["onclick"] = string.Format("return confirm('{0}?')", PageContext.Localization.GetText("CP_EDITAVATAR", "AVATARDELETE"));

        string addAdminParam = string.Empty;
        if (this._adminEditMode)
        {
          addAdminParam = "u=" + this._currentUserID.ToString();
        }

        this.OurAvatar.NavigateUrl = YafBuildLink.GetLinkNotEscaped(ForumPages.avatar, addAdminParam);
        this.OurAvatar.Text = PageContext.Localization.GetText("CP_EDITAVATAR", "OURAVATAR_SELECT");
      }

      BindData();
    }

    /// <summary>
    /// The bind data.
    /// </summary>
    private void BindData()
    {
      DataRow row;

      using (DataTable dt = DB.user_list(PageContext.PageBoardID, this._currentUserID, null))
      {
        row = dt.Rows[0];
      }

      this.AvatarImg.Visible = true;
      this.Avatar.Text = string.Empty;
      this.DeleteAvatar.Visible = false;
      this.NoAvatar.Visible = false;

      if (PageContext.BoardSettings.AvatarUpload && row["HasAvatarImage"] != null && long.Parse(row["HasAvatarImage"].ToString()) > 0)
      {
        this.AvatarImg.ImageUrl = String.Format("{0}resource.ashx?u={1}", YafForumInfo.ForumRoot, this._currentUserID);
        this.Avatar.Text = string.Empty;
        this.DeleteAvatar.Visible = true;
      }
      else if (row["Avatar"].ToString().Length > 0)
      {
        // Took out PageContext.BoardSettings.AvatarRemote
        this.AvatarImg.ImageUrl = String.Format(
          "{3}resource.ashx?url={0}&width={1}&height={2}", 
          Server.UrlEncode(row["Avatar"].ToString()), 
          PageContext.BoardSettings.AvatarWidth, 
          PageContext.BoardSettings.AvatarHeight, 
          YafForumInfo.ForumRoot);

        this.Avatar.Text = row["Avatar"].ToString();
        this.DeleteAvatar.Visible = true;
      }
      else if (PageContext.BoardSettings.AvatarGravatar)
      {
        var x = new MD5CryptoServiceProvider();
        byte[] bs = Encoding.UTF8.GetBytes(PageContext.User.Email);
        bs = x.ComputeHash(bs);
        var s = new StringBuilder();
        foreach (byte b in bs)
        {
          s.Append(b.ToString("x2").ToLower());
        }

        string emailHash = s.ToString();

        string gravatarUrl = "http://www.gravatar.com/avatar/" + emailHash + ".jpg?r=" + PageContext.BoardSettings.GravatarRating;

        this.AvatarImg.ImageUrl = String.Format(
          "{3}resource.ashx?url={0}&width={1}&height={2}", 
          Server.UrlEncode(gravatarUrl), 
          PageContext.BoardSettings.AvatarWidth, 
          PageContext.BoardSettings.AvatarHeight, 
          YafForumInfo.ForumRoot);

        this.NoAvatar.Text = "Gravatar Image";
        this.NoAvatar.Visible = true;
      }
      else
      {
        this.AvatarImg.ImageUrl = "../images/noavatar.gif";
        this.NoAvatar.Visible = true;
      }

      int rowSpan = 2;

      this.AvatarUploadRow.Visible = this._adminEditMode ? true : PageContext.BoardSettings.AvatarUpload;
      this.AvatarRemoteRow.Visible = this._adminEditMode ? true : PageContext.BoardSettings.AvatarRemote;

      if (this._adminEditMode || PageContext.BoardSettings.AvatarUpload)
      {
        rowSpan++;
      }

      if (this._adminEditMode || PageContext.BoardSettings.AvatarRemote)
      {
        rowSpan++;
      }

      this.avatarImageTD.RowSpan = rowSpan;
    }

    /// <summary>
    /// The delete avatar_ click.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void DeleteAvatar_Click(object sender, EventArgs e)
    {
      DB.user_deleteavatar(this._currentUserID);

      // clear the cache for this user...
      UserMembershipHelper.ClearCacheForUserId(this._currentUserID);
      BindData();
    }

    /// <summary>
    /// The back_ click.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void Back_Click(object sender, EventArgs e)
    {
      if (this._adminEditMode)
      {
        YafBuildLink.Redirect(ForumPages.admin_users);
      }
      else
      {
        YafBuildLink.Redirect(ForumPages.cp_profile);
      }
    }

    /// <summary>
    /// The remote update_ click.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void RemoteUpdate_Click(object sender, EventArgs e)
    {
      if (this.Avatar.Text.Length > 0 && !this.Avatar.Text.StartsWith("http://"))
      {
        this.Avatar.Text = "http://" + this.Avatar.Text;
      }

      // update
      DB.user_saveavatar(this._currentUserID, this.Avatar.Text.Trim(), null, null);

      // clear the cache for this user...
      UserMembershipHelper.ClearCacheForUserId(this._currentUserID);

      // clear the URL out...
      this.Avatar.Text = string.Empty;

      BindData();
    }

    /// <summary>
    /// The upload update_ click.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void UploadUpdate_Click(object sender, EventArgs e)
    {
      if (this.File.PostedFile != null && this.File.PostedFile.FileName.Trim().Length > 0 && this.File.PostedFile.ContentLength > 0)
      {
        long x = PageContext.BoardSettings.AvatarWidth;
        long y = PageContext.BoardSettings.AvatarHeight;
        int nAvatarSize = PageContext.BoardSettings.AvatarSize;

        Stream resized = null;

        try
        {
          using (Image img = Image.FromStream(this.File.PostedFile.InputStream))
          {
            if (img.Width > x || img.Height > y)
            {
              PageContext.AddLoadMessage(String.Format(PageContext.Localization.GetText("CP_EDITAVATAR", "WARN_TOOBIG"), x, y));
              PageContext.AddLoadMessage(String.Format(PageContext.Localization.GetText("CP_EDITAVATAR", "WARN_SIZE"), img.Width, img.Height));
              PageContext.AddLoadMessage(PageContext.Localization.GetText("CP_EDITAVATAR", "WARN_RESIZED"));

              double newWidth = img.Width;
              double newHeight = img.Height;
              if (newWidth > x)
              {
                newHeight = newHeight * x / newWidth;
                newWidth = x;
              }

              if (newHeight > y)
              {
                newWidth = newWidth * y / newHeight;
                newHeight = y;
              }

              using (var bitmap = new Bitmap(img, new Size((int) newWidth, (int) newHeight)))
              {
                resized = new MemoryStream();
                bitmap.Save(resized, ImageFormat.Jpeg);
              }
            }

            if (nAvatarSize > 0 && this.File.PostedFile.ContentLength >= nAvatarSize && resized == null)
            {
              PageContext.AddLoadMessage(String.Format(PageContext.Localization.GetText("CP_EDITAVATAR", "WARN_BIGFILE"), nAvatarSize));
              PageContext.AddLoadMessage(String.Format(PageContext.Localization.GetText("CP_EDITAVATAR", "WARN_FILESIZE"), this.File.PostedFile.ContentLength));
              return;
            }

            if (resized == null)
            {
              DB.user_saveavatar(this._currentUserID, null, this.File.PostedFile.InputStream, this.File.PostedFile.ContentType);
            }
            else
            {
              DB.user_saveavatar(this._currentUserID, null, resized, "image/jpeg");
            }

            // clear the cache for this user...
            UserMembershipHelper.ClearCacheForUserId(this._currentUserID);
          }
        }
        catch
        {
          // image is probably invalid...
          PageContext.AddLoadMessage(PageContext.Localization.GetText("CP_EDITAVATAR", "INVALID_FILE"));
        }

        BindData();
      }
    }
  }
}