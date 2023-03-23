<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registeration.aspx.cs" Inherits="Milestone_3.Registeration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <h1>Registeration Page</h1>
    <h2>Choose Type of User</h2>
    <form id="form1" runat="server">
        <div>
            Sports Association Manager<br />
            <br />
            <asp:Button ID="RegisterSPA" runat="server" Text="Register" Width="111px" OnClick="RegisterSPA_Click" />
            <br />
            <br />
            Club Representative<br />
            <br />
            <asp:Button ID="RegisterRepresentative" runat="server" Text="Register" Width="111px" OnClick="RegisterRepresentative_Click" />
            <br />
            <br />
            Stadium Manager<br />
            <br />
            <asp:Button ID="RegisterStadiumManager" runat="server" Text="Register" Width="111px" OnClick="RegisterStadiumManager_Click" />
            <br />
            <br />
            Fan<br />
            <br />
            <asp:Button ID="RegisterFan" runat="server" Text="Register" Width="111px" OnClick="RegisterFan_Click" />
            <br />
        </div>
    </form>
</body>
</html>
