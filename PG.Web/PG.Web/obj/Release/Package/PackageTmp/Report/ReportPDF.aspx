<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportPDF.aspx.cs" Inherits="PG.Web.Report.ReportPDF" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    

</head>
<body>
    <form id="form1" runat="server">
   <div style="width:0px;height:0px;">
    </div>
    </form>

     
     <object 
       id="pdfDocument" 
       type="application/pdf" 
       data="<%=this.GetReportPDFPageLink%>"
       width="0px" 
       height="0px">
       </object>
</body>
</html>
