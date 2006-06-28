<%@ Control language="c#" Codebehind="groups.ascx.cs" AutoEventWireup="false" Inherits="yaf.pages.admin.groups" %>
<%@ Register TagPrefix="yaf" Namespace="yaf.controls" Assembly="yaf" %>

<yaf:PageLinks runat="server" id="PageLinks"/>

<yaf:adminmenu runat="server">

<table class=content width="100%" cellspacing=1 cellpadding=0>
<tr>
	<td class=header1 colspan="5">Groups</td>
</tr>

<asp:repeater id=GroupList runat="server">
	<HeaderTemplate>
		<tr>
			<td class=header2>Name</td>
			<td class=header2>Is Admin</td>
			<td class=header2>Is Start</td>
			<td class=header2>Is Forum Moderator</td>
			<td class=header2>Command</td>
		</tr>
	</HeaderTemplate>
	<ItemTemplate>
		<tr>
			<td class=post>
				<%# Eval( "Name") %>
			</td>
			<td class=post>
				<%# BitSet(Eval( "Flags"),1) %>
			</td>
			<td class=post>
				<%# BitSet(Eval( "Flags"),4) %>
			</td>
			<td class=post>
				<%# BitSet(Eval( "Flags"),8) %>
			</td>
			<td class=post>
				<asp:linkbutton runat="server" commandname="edit" commandargument='<%# Eval( "GroupID") %>'>Edit</asp:linkbutton>
				|
				<asp:linkbutton runat="server" onload="Delete_Load" commandname="delete" commandargument='<%# Eval( "GroupID") %>'>Delete</asp:linkbutton>
			</td>
		</tr>
	</ItemTemplate>
</asp:repeater>

<tr>
	<td class=footer1 colspan="5"><asp:linkbutton id=NewGroup runat="server">New Group</asp:linkbutton></td>
</tr>
</table>

</yaf:adminmenu>

<yaf:SmartScroller id="SmartScroller1" runat = "server" />
