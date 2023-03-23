<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Stadium Manager Registeration.aspx.cs" Inherits="Milestone_3.Stadium_Manager_Registeration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <h1>Stadium Manager Registeration Page</h1>
        <div>
            Name<br />
            <br />
            <asp:TextBox ID="txt_name" runat="server"></asp:TextBox>
            <br />
            <br />
            Username<br />
            <br />
            <asp:TextBox ID="txt_username" runat="server"></asp:TextBox>
            <br />
            <br />
            Password<br />
            <br />
            <asp:TextBox ID="txt_password" runat="server" TextMode="Password"></asp:TextBox>
            <br />
            <br />
            Name of the Stadium<br />
            <br />
            <asp:TextBox ID="txt_stadiumname" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="Register" runat="server" Text="Register" Width="126px" OnClick="Register_Click" />
            <br />
            <br />
            <asp:Panel ID="Panel1" runat="server">
            </asp:Panel>
            <br />
        </div>
    </form>
</body>
</html>
