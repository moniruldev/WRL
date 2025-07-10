<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="XMLTest.aspx.cs" Inherits="PG.Web.XMLTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
       XML Data Test
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" Text="Insert" OnClick="Button1_Click" />
        <asp:Button ID="Button2" runat="server" Text="Update" />
        <asp:Button ID="Button3" runat="server" Text="Read" OnClick="Button3_Click" />
        <br />
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    </div>
    </form>
</body>
</html>
