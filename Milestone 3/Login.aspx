<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Milestone_3.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <h1>Login Page</h1>
        <div>
            Username<br />
            <asp:TextBox ID="username_input" runat="server" ToolTip="enter you username" ></asp:TextBox>
            <br />
            Password<br />
            <asp:TextBox ID="password_input" runat="server" TextMode ="Password"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="log_in" runat="server" Text="Log in" OnClick="login" Width="122px" />
            <br />
            <br />
            <asp:Panel ID="Panel1" runat="server">
            </asp:Panel>
            <br />
            Do not have an account ?<br />
            <br />
            <asp:Button ID="Register" runat="server" Text="Register" Width="122px" OnClick="Register_Click" />

        </div>
    </form>
</body>
</html>
