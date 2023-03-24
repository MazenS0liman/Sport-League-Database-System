<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Fan Registeration.aspx.cs" Inherits="Milestone_3.Fan_Registeration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <h1>Fan Registeration Page</h1>
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
            <asp:TextBox ID="txt_password" runat="server" TextMode ="Password"></asp:TextBox>
            <br />
            <br />
            National ID Number<br />
            <br />
            <asp:TextBox ID="txt_nationalID" runat="server" TextMode ="Number"></asp:TextBox>
            <br />
            <br />
            Phone Number<br />
            <br />
            <asp:TextBox ID="txt_phonenumber" runat="server" TextMode ="Number"></asp:TextBox>
            <br />
            <br />
            Birth Date<br />
            <br />
            <asp:TextBox ID="txt_birthdate" runat="server" TextMode="DateTimeLocal"></asp:TextBox>
            <br />
            <br />
            Adderss<br />
            <br />
            <asp:TextBox ID="txt_address" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="Register" runat="server" Text="Register" Width="126px" OnClick="Register_Click" />
            <br />
            <br />
            <asp:Panel ID="Panel1" runat="server">
            </asp:Panel>
            <br />
            <br />
        </div>
    </form>
</body>
</html>
