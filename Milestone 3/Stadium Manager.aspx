<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Stadium Manager.aspx.cs" Inherits="Milestone_3.Stadium_Manager" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <h1>Stadium Manager Page</h1>
        <div>
            View Stadium Information<br />
            <br />
            <asp:Button ID="stadiumInfo" runat="server" Text="View" Width="128px" OnClick="stadiumInformation" />
            <br />
            <br />
            <asp:Panel ID="Panel1" runat="server">
            </asp:Panel>
            <br />
            View All Request Received<br />
            <br />
            <asp:Button ID="allRequestReceive" runat="server" Text="View" Width="128px" OnClick="allRequestReceived" />
            <br />
            <br />
            <asp:Panel ID="Panel2" runat="server">
            </asp:Panel>
            <br />
            Accept Request<br />
            <br />
            Club Host Name<br />
            <br />
            <asp:TextBox ID="acceptHostName" runat="server"></asp:TextBox>
            <br />
            <br />
            Club Guest Name<br />
            <br />
&nbsp;<asp:TextBox ID="acceptGuestName" runat="server"></asp:TextBox>
            <br />
            <br />
            Start Time<br />
            <br />
            <asp:TextBox ID="acceptStarttime" runat="server" TextMode="DateTimeLocal"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="AcceptRequests" runat="server" Text="Accept Request" Width="128px" OnClick="AcceptRequest" />
            <br />
            <br />
            <asp:Panel ID="Panel3" runat="server">
            </asp:Panel>
            <br />
            Reject Request<br />
            <br />
            Club Host Name<br />
            <br />
            <asp:TextBox ID="rejectHostName" runat="server"></asp:TextBox>
            <br />
            <br />
            Club Guest Name<br />
            <br />
            <asp:TextBox ID="rejectGuestName" runat="server"></asp:TextBox>
            <br />
            <br />
            Start Time<br />
            <br />
            <asp:TextBox ID="rejectStarttime" runat="server" TextMode="DateTimeLocal"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="RejectRequests" runat="server" Text="Reject Request" Width="128px" OnClick="RejectRequest" />
            <br />
            <br />
            <asp:Panel ID="Panel4" runat="server">
            </asp:Panel>
            <br />
        </div>
    </form>
</body>
</html>
