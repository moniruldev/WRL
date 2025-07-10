<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportTest.aspx.cs" Inherits="PG.Web.Report.ReportTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            EnableModelValidation="True">
            <Columns>
                <asp:BoundField DataField="GLAccountID" HeaderText="ID" />
                <asp:BoundField DataField="GLAccountName" HeaderText="Name" />
                <asp:BoundField DataField="OpenDebitAmt" HeaderText="OpDebit" DataFormatString="{0:#0.00}" />
                <asp:BoundField DataField="OpenCreditAmt" HeaderText="OpCredit" DataFormatString="{0:#0.00}" />
                <asp:BoundField DataField="DebitAmt" HeaderText="TrDebit" DataFormatString="{0:#0.00}" />
                <asp:BoundField DataField="CreditAmt" HeaderText="TrCredit" DataFormatString="{0:#0.00}" />
                <asp:BoundField DataField="CloseDebitAmt" HeaderText="ClDebit" DataFormatString="{0:#0.00}" />
                <asp:BoundField DataField="CloseCreditAmt" HeaderText="ClCredit" DataFormatString="{0:#0.00}" />
            </Columns>
        </asp:GridView>
      
    </div>
    </form>
</body>
</html>
