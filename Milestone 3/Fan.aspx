<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Fan.aspx.cs" Inherits="Milestone_3.Fan" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <h1>Fan Page</h1>
        <div>
            View Avaible Matches<br />
            <br />
            Insert Date
            <br />
            <br />
            <asp:TextBox ID="date_entered" runat="server" TextMode="DateTimeLocal"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="viewMatches" runat="server" Text="View" Width="125px" OnClick="viewMatches_Click" />
            <br />
            <br />
            <asp:Panel ID="Panel1" runat="server">
            </asp:Panel>
            <br />
            Purchase a Ticket<br />
            <br />
            Host Name<br />
            <br />
            <asp:TextBox ID="hostname" runat="server"></asp:TextBox>
            <br />
            <br />
            Guest Name<br />
            <br />
            <asp:TextBox ID="guestname" runat="server"></asp:TextBox>
            <br />
            <br />
            Start Time<br />
            <br />
            <asp:TextBox ID="starttime" runat="server" TextMode ="DateTimeLocal"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="purchaseTickeT" runat="server" Text="Purchase" Width="125px" OnClick="purchaseTickeT_Click" />
            <br />
            <br />
            <asp:Panel ID="Panel2" runat="server">
            </asp:Panel>
        </div>
    </form>
</body>
</html>
