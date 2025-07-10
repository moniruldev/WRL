<%@ Page Title="Report View" Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="ReportView.aspx.cs" Inherits="PG.Web.Report.ReportView" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
 
 </style>
    <script language="javascript" type="text/javascript">
// <!CDATA[
        var isPageResize = true;
        var hdnIsPrint = '<%=hdnIsPrint.ClientID%>';
       // resizeReport();

        function resizeReport() {
            $(document.body).css('background-color', 'white');
            return;
//            if ($('#dvReportViewer').length > 0) {
//                var pHt = $('#dvReportViewer').parent().height();
//                var pWd = $('#dvReportViewer').parent().width();

//                $('#dvReportViewer').height(pHt);
//               // $('#dvReportViewer').height(pHt);
////                $('#dvReportViewer').width(pWd);


//                $('#dvReportViewer').find('table:first').css('display', '');
//                if (JSUtility.isIE()) {
//                    $('#dvReportViewer').find('table:first').height(pHt - 35);
//                }
//                else {
//                    $('#dvReportViewer').find('table:first').height(pHt);
//                }
//                //
//                //$('#dvReportViewer').find('table:first').height(pHt);
                
//            }
        }

        jQuery(document).ready(function() {
            resizeReport();
        });

        $(window).resize(function() {
            resizeReport();
        });

        function PageResizeCompleted(pageHeight) {
            //alert('r');
            resizeReport();
        }



        //
        jQuery(document).ready(function () {
            var isPrint = parseInt($('#' + hdnIsPrint).val());
            if (isPrint) {
                //alert('Printing...');
            }

        });

// ]]>
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="dvPageContent" style="width:100%; height:100%;"> 
    <div id="dvReportViewer" style="height:auto; width:100%; background-color:White;">
        <rsweb:ReportViewer ID="ReportViewer1" runat="server"  Width="100%" BackColor="White" Height="100%">
        </rsweb:ReportViewer>
    </div>
     <asp:HiddenField ID="hdnIsPrint" runat="server" Value = "0" />
  </div>
  <div ="dvPrintIFrame" style="height:0px; width:0px; display:none;"  >
      <asp:Literal ID="IFrameLiteral" runat="server" Text=""></asp:Literal>
  </div>
  
</asp:Content>
