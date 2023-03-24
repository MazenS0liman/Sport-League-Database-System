<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="System Admin Page.aspx.cs" Inherits="Milestone_3.System_Admin_Page" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <h1>System Admin Page</h1>
        <div>
            Add New Club<br />
            <br />
            Insert&nbsp;
            Club&nbsp; Name
            <br />
            <asp:TextBox ID="add_club_name" runat="server"></asp:TextBox>
            <br />
            <br />
            Insert&nbsp;
            Location<br />
            <asp:TextBox ID="add_club_location" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="addClub" runat="server" Text="Add Club" Width="124px" OnClick="addClub_Click" />
            <br />
            <br />
            <asp:Panel ID="Panel1" runat="server">
            </asp:Panel>
            <br />
            Delete Club<br />
            <br />
            Insert&nbsp;
            Club Name<br />
            <asp:TextBox ID="delete_club_name" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="deleteClub" runat="server" Text="Delete Club" Width="124px" OnClick="deleteClub_Click" />
            <br />
            <br />
            <asp:Panel ID="Panel2" runat="server">
            </asp:Panel>
            <br />
            Add Stadium<br />
            <br />
            Insert
            Stadium Name<br />
            <asp:TextBox ID="add_stadium_name" runat="server"></asp:TextBox>
            <br />
            <br />
            Insert&nbsp;
            Stadium Location<br />
            <asp:TextBox ID="add_stadium_location" runat="server"></asp:TextBox>
            <br />
            <br />
            Insert&nbsp;
            Stadium Capacity<br />
            <asp:TextBox ID="add_stadium_capacity" runat="server" TextMode="Number"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="addStadium" runat="server" Text="Add Stadium" Width="124px" OnClick="addStadium_Click" />
            <br />
            <br />
            <asp:Panel ID="Panel3" runat="server">
            </asp:Panel>
            <br />
            Delete Stadium<br />
            <br />
            Insert
            Stadium Name<br />
            <asp:TextBox ID="delete_stadium_name" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="deleteStadium" runat="server" Text="Delete Stadium" Width="124px" OnClick="deleteStadium_Click" />
            <br />
            <br />
            <asp:Panel ID="Panel4" runat="server">
            </asp:Panel>
            <br />
            Block Fan<br />
            <br />
            Insert&nbsp; Fan National ID<br />
            <asp:TextBox ID="fannationalID" runat="server" TextMode="Number"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="blockFan" runat="server" Text="Block Fan" Width="124px" OnClick="blockFan_Click" />
            <br />
            <br />
            <asp:Panel ID="Panel5" runat="server">
            </asp:Panel>
            <br />
        </div>
    </form>
</body>
</html>
