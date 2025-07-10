<%@ Page Title="Print Report" Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="ReportPrint.aspx.cs" Inherits="PG.Web.Report.ReportPrint" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <style type="text/css">
 
 </style>
    <script language="javascript" type="text/javascript">
// <!CDATA[
        var isPageResize = true;

// ]]>
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <div id="dvPageContent" style="width:100%; height:100%;"> 
  
  </div>
</asp:Content>
