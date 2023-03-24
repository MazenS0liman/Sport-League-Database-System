<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Sport Association Manager.aspx.cs" Inherits="Milestone_3.Sport_Association_Manager" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <h1>Sports Association Manager Page</h1>
        <div>
            Add Match<br />
            <br />
            Host Club Name<br />
            <asp:TextBox ID="add_match_host_name" runat="server"></asp:TextBox>
            <br />
            <br />
            Guest Club Name<br />
            <asp:TextBox ID="add_match_guest_name" runat="server"></asp:TextBox>
            <br />
            <br />
            Start Time<br />
            <asp:TextBox ID="add_match_start_time" runat="server" TextMode="DateTimeLocal"></asp:TextBox>
            <br />
            <br />
            End Time<br />
            <asp:TextBox ID="add_match_end_time" runat="server" TextMode="DateTimeLocal"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="addMatch" runat="server" Text="Add New Match" Width="128px" OnClick="addNewMatch" />
            <br />
            <br />
            <asp:Panel ID="Panel4" runat="server">
            </asp:Panel>
            <br />
            Delete Match<br />
            <br />
            Host Club Name<br />
            <asp:TextBox ID="delete_match_host_name" runat="server"></asp:TextBox>
            <br />
            <br />
            Guest Club Name<br />
            <asp:TextBox ID="delete_match_guest_name" runat="server"></asp:TextBox>
            <br />
            <br />
            Start Time<br />
            <asp:TextBox ID="delete_match_start_time" runat="server" TextMode="DateTimeLocal"></asp:TextBox>
            <br />
            <br />
            End Time<br />
            <asp:TextBox ID="delete_match_end_time" runat="server" TextMode="DateTimeLocal"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="deleteExistingMatch" runat="server" Text="Delete a Match" Width="128px" OnClick="deleteMatch" />
            <br />
            <br />
            <asp:Panel ID="Panel8" runat="server">
            </asp:Panel>
            <br />
            View All Upcoming Matches<br />
            <br />
            <asp:Button ID="AddView1" runat="server" Text="Click View" Width="128px" OnClick="Onclick_AddView1" />
            <br />
            <br />
            <asp:Button ID="RemoveView1" runat="server" Text="Remove View" Width="128px" OnClick="Onclick_RemoveView1" />
            <br />
            <br />
            <asp:Panel ID="Panel1" runat="server">
            </asp:Panel>
            <br />
            View Already Played Matches<br />
            <br />
            <asp:Button ID="AddView2" runat="server" Text="Click View" Width="128px" OnClick="Onclick_AddView2" />
            <br />
            <br />
            <asp:Button ID="RemoveView2" runat="server" Text="Remove View" Width="128px" OnClick="Onclick_RemoveView2" />
            <br />
            <br />
            <asp:Panel ID="Panel2" runat="server">
            </asp:Panel>
            <br />
            View Clubs Who Never Played With Each Other<br />
            <br />
            <asp:Button ID="AddView3" runat="server" Text="Click View" Width="128px" OnClick="Onclick_AddView3" />
            <br />
            <br />
            <asp:Button ID="RemoveView3" runat="server" Text="Remove View" Width="128px" OnClick="Onclick_RemoveView3" />
            <br />
            <br />
            <asp:Panel ID="Panel3" runat="server">
            </asp:Panel>
            <br />
            <br />
       </div>
    </form>
</body>
</html>
