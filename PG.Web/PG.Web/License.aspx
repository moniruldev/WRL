<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="License.aspx.cs" Inherits="PayRoll.License" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
       <table>
          <tr>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
          </tr>
          <tr>
            <td>
            </td>
            <td>
               <asp:Label ID="Label1" runat="server" Text="Machine ID:"></asp:Label>
            </td>
            <td>
               <asp:TextBox ID="txtMachineID" runat="server"></asp:TextBox>
            </td>
          </tr>
          <tr>
            <td>
            </td>
            <td>
               <asp:Label ID="Label2" runat="server" Text="IP Address:"></asp:Label>
            </td>
            <td>
              <asp:TextBox ID="txtIPAddress" runat="server"></asp:TextBox>
            </td>
          </tr>
          <tr>
            <td>
            </td>
            <td>
            </td>
            <td>
                <asp:Button ID="btnReset" runat="server" Text="Reset License" 
                    onclick="btnReset_Click" />
            </td>
           </tr> 
           <tr>
            <td>
            </td>
            <td>
            </td>
            <td>
              <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
            </td>
          </tr>
       </table>
    </div>
    </form>
</body>
</html>
