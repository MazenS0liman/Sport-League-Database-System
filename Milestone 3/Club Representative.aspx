<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Club Representative.aspx.cs" Inherits="Milestone_3.Club_Representative" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="direction: ltr">
            <h1>Club Representative Page</h1>
            <br />
            View Club Information<br />
            <br />
            <asp:Button ID="clubInformation" runat="server" Text="View" Width="145px" OnClick="clubInfo" />
            <br />
            <br />
            <asp:Panel ID="Panel1" runat="server">
            </asp:Panel>
            <br />
            View Upcoming Matches<br />
            <br />
            <asp:Button ID="upComingMatch" runat="server" Text="View" Width="145px" OnClick="upComingMatches" />
            <br />
            <br />
            <asp:Panel ID="Panel2" runat="server">
            </asp:Panel>
            <br />
            View Available Stadiums<br />
            <br />
            Enter Date<br />
            <br />
            <asp:TextBox ID="avaibledate" runat="server" TextMode ="DateTimeLocal"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="avaibleStadium" runat="server" Text="View" Width="145px" OnClick="avaibleStadiums" />
            <br />
            <br />
            <asp:Panel ID="Panel3" runat="server">
            </asp:Panel>
            <br />
            Send a Request<br />
            <br />
            Stadium Name<br />
            <br />
            <asp:TextBox ID="stadiumName" runat="server"></asp:TextBox>
            <br />
            <br />
            Start Time<br />
            <br />
            <asp:TextBox ID="startingtime" runat="server" TextMode="DateTimeLocal"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="sendrequest" runat="server" Text="Send Request" Width="145px" OnClick="sendRequest" />
            <br />
            <br />
            <asp:Panel ID="Panel4" runat="server">
            </asp:Panel>
            <br />
            <br />
        </div>
    </form>
</body>
</html>
