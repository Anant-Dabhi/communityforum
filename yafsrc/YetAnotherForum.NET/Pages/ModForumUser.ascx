<%@ Control Language="c#" AutoEventWireup="True"
	Inherits="YAF.Pages.ModForumUser" Codebehind="ModForumUser.ascx.cs" %>


<YAF:PageLinks runat="server" ID="PageLinks" />

<div class="row">
    <div class="col-xl-12">
        <h2><YAF:LocalizedLabel ID="LocalizedLabel6" runat="server" LocalizedTag="TITLE" /></h2>
    </div>
</div>

<div class="row">
    <div class="col">
        <div class="card mb-3">
            <div class="card-header">
                <YAF:IconHeader runat="server"
                                IconName="user-secret"/>
            </div>
            <div class="card-body text-center">
                <div class="input-group mb-3">
                    <asp:TextBox runat="server" ID="UserName" CssClass="form-control mb-1" PlaceHolder='<%# this.GetText("USER") %>' />
                    <YAF:ThemeButton runat="server" ID="FindUsers" 
                                     TextLocalizedTag="FIND"
                                     OnClick="FindUsers_Click" 
                                     Type="Secondary"
                                     Icon="search" />
                    </div>
                    <div class="mb-3">
                        <asp:DropDownList runat="server" ID="ToList"
                                          Visible="false" 
                                          CssClass="select2-select" />
                    </div>
                    <div class="mb-3">
                        <asp:Label runat="server" AssociatedControlID="AccessMaskID">
                            <YAF:LocalizedLabel ID="LocalizedLabel3" runat="server" LocalizedTag="ACCESSMASK" />
                        </asp:Label>
                        <asp:DropDownList runat="server" ID="AccessMaskID" CssClass="select2-select" />
                    </div>
            </div>
            <div class="card-footer text-center">
                <YAF:ThemeButton runat="server" ID="Update"
                                 OnClick="Update_Click"
                                 TextLocalizedTag="UPDATE"
                                 Type="Primary"
                                 Icon="save"/>
                <YAF:ThemeButton runat="server" ID="Cancel"
                                 OnClick="Cancel_Click"
                                 TextLocalizedTag="CANCEL"
                                 Type="Secondary"
                                 Icon="times"/>

            </div>
        </div>
    </div>
</div>