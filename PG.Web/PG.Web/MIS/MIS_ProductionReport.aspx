<%@ Page Title="" Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="MIS_ProductionReport.aspx.cs" Inherits="PG.Web.MIS.MIS_ProductionReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/GLGroupTree.ascx" TagName="GLGroupTree" TagPrefix="uc1" %>
<%@ Register assembly="DropDownCheckBoxes" namespace="Saplin.Controls" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script src="../javascript/jquery.ui.combogrid.js" type="text/javascript"></script>
    <link href="../css/jquery.ui.combogrid.css" rel="stylesheet" type="text/css" />
      
       


    <style type="text/css">
        

        .dvGroup
        {
            width: 182px;
            height: 20px;
            border: 1px solid lightgrey;
        }
        
        
        .dvGroupListPopup
        {
            display: none;
            height: 0px;
            width: 0px;
        }
        
        
        .textPopup
        {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            border: 1px #1B68B8 solid;
            background-color: #FFFFFF;
            color: #000000;
            font-size: 11px;
            width: 160px;
            height: 16px;
            padding-left: 2px;
        }
        
        .btnPopup
        {
            height: 20px;
            width: 16px;
            background-image: url(/image/dropdown.gif);
            background-repeat: no-repeat;
            background-position: center bottom;
            cursor: pointer;
        }
        
        .btnPopup:hover
        {
            background-image: url(/image/dropdown_over.gif);
        }
        
        .dvSpacer
        {
            height: 10px;
            width: 100%;
        }
        
        
        .dvReportList
        {
            height: 100%;
            width: 100%;
            overflow: auto;
        }
        .dvParam
        {
            height: 100%;
            width: 100%;
            overflow: auto;
        }
        
        
        .tblParam
        {
            /* border-collapse: collapse;    */
            height: auto;
        }
        
        

        
        .tblParam td
        {
            height: auto;
        }
        
        
        .cboYesNo
        {
            width: 50px;
        }
        
        .tdSpacer
        {
            width: 10px;
        }
        
        .rowParam
        {
        }
        
        .rowSpacer
        {
            height:20px;
        }
        
        
        .dvPrintIFrame
        {
            height: 0px;
            width: 0px;
        }
    </style>

     <script language="javascript" type="text/javascript">
         var ReportViewPageLink = '<%=this.ReportViewPageLink%>';
         var ReportViewPDFPageLink = '<%=this.ReportViewPDFPageLink%>';
         var ReportPrintPageLink = '<%=this.ReportPrintPageLink%>';
         var ReportPDFPageLink = '<%=this.ReportPDFPageLink%>';

    var isPageResize = true;
    ContentForm.CalendarImageURL = "../image/calendar.png";
     function PageResizeCompleted(pg, cntMain) {
            resizeContentInner(cntMain);
        }
     function resizeContentInner(cntMain) {
            var contHeight = $("#dvContentMainInner").height();

            var topHeight = $("#dvTop").height();

            var middleHeight = contHeight - topHeight;

            $("#dvMiddle").height(middleHeight);
            $("#tblMiddle").height(middleHeight);

            $("#dvReportList").height(middleHeight);
            $("#dvParam").height(middleHeight);

        }
      
     function showOverlay() {
         document.getElementById("overlay").style.display = "block";
     }

     function hideOverlay() {
         document.getElementById("overlay").style.display = "none";
     }
     function showOverlayReport() {
         document.getElementById("overlayReport").style.display = "block";
     }


     function hideOverlayReport() {
         document.getElementById("overlayReport").style.display = "none";
     }

     function reportInNewWindow(url) {
         var rWin = window.open(url, '_blank');
         if (rWin == null) {
             reportURL = url;
             showOverlayReport();
         }
     }



     function tbopen(key, isPrint, isPDFAutoPrint, showWait) {
         key = key || '';
         isPrint = isPrint || false;
         showWait = showWait || true;
         var now = new Date();
         var strTime = now.getTime().toString();
         var url = ReportViewPageLink + "?rk=" + key + "&_tt=" + strTime;
         //var url = ReportViewPageLink + "?rk=" + key;

         //if (pageInTab == 1)
         if (TabVar.PageMode == Enums.PageMode.InTab) {

             var tdata = new xtabdata();
             tdata.linktype = Enums.LinkType.Direct;
             tdata.id = 7999;
             tdata.name = "Report view";
             //tdata.label = "User: " + userid;
             tdata.label = "Report view";
             tdata.type = 0;
             tdata.url = url;
             tdata.tabaction = Enums.TabAction.InNewTab;
             tdata.selecttab = 1;
             tdata.reload = 0;
             tdata.param = "";
             tdata.showWait = showWait;

             try {
                 //window.parent.OpenMenuByData(tdata);
                 window.parent.TabMenu.OpenMenuByData(tdata);
             }
             catch (err) {
                 alert("error in page");
             }
         }
         else {
             //on new window/tab
             //window.open(url,'_blank');   

             window.location = url;
         }
     }
         
         </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="dvPageContent" style="width: 100%; height: 100%;">
        <div id="dvContentHeader" class="dvContentHeader">
            <div id="dvHeader" class="dvHeader">
                Report -Production MIS
            </div>
            <div id="dvMessage" class="dvMessage">
                <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
            </div>
            <div id="dvHeaderControl" class="dvHeaderControl">
            </div>
        </div>
        <div id="dvContentMain" class="dvContentMain">
            <div id="dvContentMainInner" class="dvContentMainInner" style="height: 100%;">
                <div id="dvTop" style="height: auto; width: 100%;">
                    <table cellspacing="0" cellpadding="0" style="height: auto; width: 100%;">
                        <tr>
                            <td>
                            </td>
                            <td valign="top" style="width: 255px;">
                                
                            </td>
                            <td style="border-left: 1px solid grey;">
                            </td>
                            <td valign="top" style="">
                                <div id="dvParamHeader" class="dvParamHeader" style="height: auto; width: 100%;">
                                    <table cellspacing="0" cellpadding="0" border="0" style="height: auto; width: 100%;">
                                        <tr>
                                            <td style="border-bottom: 1px solid grey;">
                                                <asp:Label ID="lblReportName" runat="server" Text="Production Report" Font-Bold="True"
                                                    Font-Size="10pt"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="dvMiddle" style="height: 489px; width: 100%">
                    <table id="tblMiddle" cellspacing="0" cellpadding="0" style="height: 100%; width: 100%;
                        min-width: 700px;">
                        <tr style="height: 100%">
                            <td>
                            </td>
                            <td valign="top" style="width: 250px;">
                                <div id="dvReportList" class="dvReportList">
                                    <table cellspacing="2" cellpadding="1">
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:TreeView ID="tvwReport" runat="server" OnSelectedNodeChanged="tvwReport_SelectedNodeChanged" style="font-size:medium; "
                                                    NodeIndent="10">
                                                    <HoverNodeStyle BackColor="#CCCCFF" />
                                                    <Nodes>
                                                        <asp:TreeNode Text="Lead Consumption" Value="Lead Consumption" Expanded="True" SelectAction="Expand">
                                                            <asp:TreeNode Selected="True" Text="Summary" Value="2004"></asp:TreeNode>
                                                          <%--  <asp:TreeNode Text="Details" Value="2005"></asp:TreeNode>
                                                            <asp:TreeNode Text="Receipt/Payment" Value="1504"></asp:TreeNode>
                                                            <asp:TreeNode Text="Cash Flow Statment" Value="1505"></asp:TreeNode>
                                                            <asp:TreeNode Text="Income Statement" Value="1502"></asp:TreeNode>
                                                            <asp:TreeNode Text="Balance Sheet" Value="1503"></asp:TreeNode>--%>
                                                        </asp:TreeNode>
                                                      <asp:TreeNode Text="Battery Production" Value="0" Expanded="True" SelectAction="Expand">
                                                            <asp:TreeNode Text="Monthly Item Wise " Value="3002"></asp:TreeNode>
                                                            <asp:TreeNode Text="Monthly Category Wise" Value="3003"></asp:TreeNode>
                                                            <%--  <asp:TreeNode Text="Cash/Bank" Value="Cash/Bank" Expanded="False" SelectAction="Expand">
                                                                <asp:TreeNode Text="Cash Summary" Value="1540"></asp:TreeNode>
                                                                <asp:TreeNode Text="Cash Journal List" Value="1541"></asp:TreeNode>
                                                                <asp:TreeNode Text="Cash Journal Book" Value="1542"></asp:TreeNode>
                                                            </asp:TreeNode>
                                                            <asp:TreeNode Text="Journal" Value="Journal" Expanded="False" SelectAction="Expand">
                                                                <asp:TreeNode Text="Journal List" Value="1530"></asp:TreeNode>
                                                                <asp:TreeNode Text="Journal Book" Value="1531"></asp:TreeNode>
                                                                <asp:TreeNode Text="Journal" Value="1532"></asp:TreeNode>
                                                            </asp:TreeNode>
                                                            <asp:TreeNode Text="Ledger" Value="Ledger" Expanded="False" SelectAction="Expand">
                                                                <asp:TreeNode Text="Ledger" Value="1521"></asp:TreeNode>
                                                                <asp:TreeNode Text="Ledger Summary" Value="1522"></asp:TreeNode>
                                                            </asp:TreeNode>--%>
                                                        </asp:TreeNode>
                                                        <%--  <asp:TreeNode Expanded="False" SelectAction="Expand" 
                                                            Text="Cost Center/Reference" Value="0">
                                                            <asp:TreeNode Text="Cost Center Summary" Value="1551"></asp:TreeNode>
                                                            <asp:TreeNode Text="Cost Center Details" Value="1561"></asp:TreeNode>
                                                            <asp:TreeNode Text="Reference Summary" Value="1552"></asp:TreeNode>
                                                            <asp:TreeNode Text="Reference Details" Value="1562"></asp:TreeNode>
                                                           <%-- <asp:TreeNode Text="Tran. Code Summary" Value="1553"></asp:TreeNode>
                                                            <asp:TreeNode Text="Tran. Code Details" Value="1563"></asp:TreeNode>
                                                        </asp:TreeNode> --%>
                                                    </Nodes>
                                                    <NodeStyle ForeColor="Black" />
                                                    <SelectedNodeStyle BackColor="#CCCCFF" ForeColor="White" Font-Bold="True" />
                                                </asp:TreeView>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                            <td style="border-left: 1px solid grey;">
                            </td>
                            <td valign="top" style="height: 100%;">
                                <div id="dvParam" class="dvParam">
                                    <table id="tblParam" cellspacing="4" cellpadding="2" border="0" class="tblParam">
                                        <tr>
                                            <td style="width: 5px;">
                                            </td>
                                            <td style="width: 200px;">
                                                <div class="dvSpacer">
                                                </div>
                                            </td>
                                            <td>
                                                <asp:HiddenField ID="hdnCompanyID" runat="server" Value="0" />
                                            </td>
                                        </tr>
                                     <tr class="rowParam">
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                <asp:Label ID="lblDeptName" runat="server" Text="Department :"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlDEPT_NAME" runat="server" Width="170px" CssClass="dropDownList">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                            </td>
                                            <td>
                                            </td>
                                        </tr>

                                        <tr class="rowParam">
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                <asp:Label ID="lblCategory" runat="server" Text="Category:"></asp:Label>
                                            </td>
                                            <td>
               <cc1:DropDownCheckBoxes ID="ddlchkTypeCategory" runat="server" AddJQueryReference="false" UseButtons="false"  UseSelectAllNode="True"  AutoPostBack="true" style="top: 0px; left: 0px">
                    <Style2 SelectBoxWidth="150px" DropDownBoxBoxWidth="" DropDownBoxBoxHeight=""></Style2>
                    <Texts OkButton="OK" CancelButton="Cancel" SelectAllNode="All Category" SelectBoxCaption="Select" />
                 
                   </cc1:DropDownCheckBoxes>
                                            </td>
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                         <tr class="rowParam">
                                            <td>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="lblFromDate" runat="server" Text="Date From:"></asp:Label>
                                            </td>
                                            <td>
                                                <table cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txtFromDate" runat="server" Width="80px" CssClass="textBox textDate dateParse"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 4px;">
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblToDate" runat="server" Text="To:"></asp:Label>
                                                        </td>
                                                        <td style="width: 2px;">
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtToDate" runat="server" Width="80px" CssClass="textBox textDate dateParse"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td align="right" class="">
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                   <%--        <tr class="rowParam">
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                <asp:Label ID="lblAccYear" runat="server" Text="Year:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlAccYear" runat="server" CssClass="dropDownList" Width="170px"
                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlAccYear_SelectedIndexChanged">
                                                    <asp:ListItem Selected="True" Text="(select)" Value="0"> </asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                         <tr >
                                            <td>
                                               
                                            </td>
                                            <td align="right">
                                                 <asp:Label ID="lblLocation" runat="server" Text="Location:"></asp:Label>
                                            </td>
                                            <td>
                                                 <asp:DropDownList ID="ddlLocation" runat="server" CssClass="dropDownList" Width="170px">
                                                      <asp:ListItem Selected="True" Text="(All)" Value="0"> </asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                             <td>
                                            </td>
                                            <td style="" align="right">
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 5px;">
                                            </td>
                                            <td style="">
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                       

                                        <tr class="rowParam">
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                <asp:Label ID="lblJournalType" runat="server" Text="Journal Type:" Visible="False"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlJournalType" runat="server" Width="170px" CssClass="dropDownList"
                                                    Visible="False">
                                                    <asp:ListItem Selected="True" Text="(all type)" Value="0"> </asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr class="rowParam">
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                <asp:Label ID="lblGLGroup" runat="server" Text="GL Group:" Visible="False"></asp:Label>
                                            </td>
                                            <td colspan="4">
                                                <table cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txtGLGroup" runat="server" Width="150px" CssClass="textBox"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <input id="btnGLGroup" type="button" value="" runat="server" class="buttonDropdown"
                                                                tabindex="-1" />
                                                        </td>
                                                        <td class="tdSpacer">
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtGLGroupName" runat="server" Width="150px" CssClass="textBox"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:HiddenField ID="hdnGLGroupID" runat="server" Value="0" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr class="rowParam">
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                <asp:Label ID="lblGLAccount" runat="server" Text="Account /Sub Ledger:" Visible="False"></asp:Label>
                                            </td>
                                            <td colspan="4">
                                                <table cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txtGLAccount" runat="server" Width="150px" CssClass="textBox"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <input id="btnGLAccount" type="button" value="" runat="server" class="buttonDropdown"
                                                                tabindex="-1" />
                                                        </td>
                                                        <td class="tdSpacer">
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtGLAccountName" runat="server" Width="150px" CssClass="textBox"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:HiddenField ID="hdnGLAccountID" runat="server" Value="0" />
                                                            <asp:HiddenField ID="hdnGLGroupIDAcc" runat="server" Value="0" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr class="rowParam">
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                <asp:Label ID="lblJournalNo" runat="server" Text="Journal No:"></asp:Label>
                                            </td>
                                            <td colspan="4">
                                                <table cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txtJournalNo" runat="server" Width="150px" CssClass="textBox"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <input id="btnJournalNo" type="button" value="" runat="server" class="buttonDropdown"
                                                                tabindex="-1" />
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                            <asp:HiddenField ID="hdnJournalID" runat="server" Value="0" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>

                                       <tr class="rowParam">
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                <asp:Label ID="lblAccRefCategory" runat="server" Text="Cost Center Category:"></asp:Label>
                                            </td>
                                            <td style="">
                                                <asp:DropDownList ID="ddlAccRefCategory" runat="server" CssClass="dropDownList" Width="170px">
                                                    <asp:ListItem Value="0">All</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                               <asp:HiddenField ID="hdnAccRefTypeID" runat="server" Value="0" />
                                            </td>
                                            <td style="" align="right">
                                                &nbsp;
                                            </td>
                                            <td>
                                            </td>
                                        </tr>

                                        <tr class="rowParam">
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                <asp:Label ID="lblAccRefCode" runat="server" Text="Cost Center:"></asp:Label>
                                            </td>
                                            <td colspan="4">
                                                <table cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txtAccRefCode" runat="server" Width="150px" CssClass="textBox"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <input id="btnAccRefCode" type="button" value="" runat="server" class="buttonDropdown"
                                                                tabindex="-1" />
                                                        </td>
                                                        <td class="tdSpacer">
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtAccRefName" runat="server" Width="150px" CssClass="textBox"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:HiddenField ID="hdnAccRefID" runat="server" Value="0" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>

                                        <tr class="rowSpacer">
                                            <td>
                                            </td>
                                            <td style="">
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr class="rowParam">
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                <asp:Label ID="lblBlock" runat="server" Text="Show:"></asp:Label>
                                            </td>
                                            <td style="">
                                                <asp:DropDownList ID="ddlBlock" runat="server" CssClass="dropDownList">
                                                    <asp:ListItem Value="0">All</asp:ListItem>
                                                    <asp:ListItem Value="1">Open</asp:ListItem>
                                                    <asp:ListItem Value="2">Blocked</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                &nbsp;
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr class="rowParam">
                                            <td>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="lblOrderBy" runat="server" Text="Order By:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlOrderBy" runat="server" CssClass="dropDownList" Width="170px">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                            </td>
                                            <td align="right">
                                                &nbsp;
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr class="rowParam">
                                            <td>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="lblReportFormat" runat="server" Text="Journal Report Format:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlReportFormat" runat="server" CssClass="dropDownList" Width="170px">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                            </td>
                                            <td align="right">
                                                &nbsp;
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr class="rowParam">
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                <asp:Label ID="lblAmountShowType" runat="server" Text="Amount Show Type:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlAmountShowType" runat="server" CssClass="dropDownList"  Width="170px">
                                                    <asp:ListItem Selected="True" Value="1">Closing Balance</asp:ListItem>
                                                    <asp:ListItem Value="2">Opening,Transaction,Closing</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr class="rowParam">
                                            <td>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="lblIncludePostType" runat="server" Text="Include Post Type:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlIncludePostType" runat="server" CssClass="dropDownList" Width="85px">
                                                    <asp:ListItem Value="0">ALL</asp:ListItem>
                                                    <asp:ListItem Selected="True" Value="1">Posted</asp:ListItem>
                                                    <asp:ListItem Value="2">Unposted</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr class="rowParam">
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                <asp:Label ID="lblIncludeOpBal" runat="server" Text="Include Opening Balance:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlIncludeOpBal" runat="server" CssClass="dropDownList"  Width="170px">
                                                    <asp:ListItem Value="0">None</asp:ListItem>
                                                    <asp:ListItem Selected="True" Value="1">Include ALL</asp:ListItem>
                                                    <asp:ListItem Value="2">Include ALL Indv.</asp:ListItem>
                                                    <asp:ListItem Value="3">Include Year</asp:ListItem>
                                                    <asp:ListItem Value="4">Include DateRange</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>

                                        <tr class="rowParam">
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                <asp:Label ID="lblIncludeZero" runat="server" Text="Include Zero:"></asp:Label>
                                            </td>
                                            <td style="">
                                                <asp:DropDownList ID="ddlIncludeZero" runat="server" CssClass="dropDownList cboYesNo">
                                                    <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                &nbsp;
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr class="rowParam">
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                <asp:Label ID="lblHeirarchyLevel" runat="server" Text="Heirarchy Level:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlHeirarchyLevel" runat="server" CssClass="dropDownList cboYesNo" Width="170px">
                                                    <asp:ListItem Value="-1">All</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                            </td>
                                            <td align="right">
                                                &nbsp;
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr class="rowParam">
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                <asp:Label ID="lblIncludeAllAccount" runat="server" Text="Include All Account:"></asp:Label>
                                            </td>
                                            <td style="">
                                                <asp:DropDownList ID="ddlIncludeAllAccount" runat="server" CssClass="dropDownList cboYesNo">
                                                    <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                &nbsp;
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr class="rowParam">
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                <asp:Label ID="lblGroupLedgerShowType" runat="server" Text="Group/Ledger:"></asp:Label>
                                            </td>
                                            <td style="">
                                                <asp:DropDownList ID="ddlGroupLedgerShowType" runat="server" CssClass="dropDownList"  Width="170px">
                                                    <asp:ListItem  Value="1">Groups</asp:ListItem>
                                                    <asp:ListItem Value="2">Ledgers</asp:ListItem>
                                                    <asp:ListItem Selected="True" Value="3">Groups And Ledgers</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                &nbsp;
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr class="rowParam">
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                <asp:Label ID="lblIncludeGLClass" runat="server" Text="Show Root Group:"></asp:Label>
                                            </td>
                                            <td style="">
                                                <asp:DropDownList ID="ddlIncludeGLClass" runat="server" CssClass="dropDownList cboYesNo">
                                                    <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                &nbsp;
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr class="rowParam">
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                <asp:Label ID="lblIncludeGroupParents" runat="server" Text="Include Group Parents:"></asp:Label>
                                            </td>
                                            <td style="">
                                                <asp:DropDownList ID="ddlIncludeGroupParents" runat="server" CssClass="dropDownList cboYesNo">
                                                    <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                &nbsp;
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr class="rowParam">
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                <asp:Label ID="lblIncludeGroupChilds" runat="server" Text="Include Group Childs:"></asp:Label>
                                            </td>
                                            <td style="">
                                                <asp:DropDownList ID="ddlIncludeGroupChilds" runat="server" CssClass="dropDownList cboYesNo">
                                                    <asp:ListItem Value="0">No</asp:ListItem>
                                                    <asp:ListItem Selected="True" Value="1">Yes</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                &nbsp;
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                &nbsp;
                                            </td>
                                            <td style="">
                                                &nbsp;
                                            </td>
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                &nbsp;
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr class="rowParam">
                                            <td>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="lblBalnceSheetShowMethod" runat="server" Text="Show Method:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlBalnceSheetShowMethod" runat="server" CssClass="dropDownList"  Width="170px">
                                                    <asp:ListItem Selected="True" Value="0">Assets/Liabilities</asp:ListItem>
                                                    <asp:ListItem Value="1">Liabilities/Assets</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr class="rowParam">
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                <asp:Label ID="lblShowPercentage" runat="server" Text="Show Percentage:"></asp:Label>
                                            </td>
                                            <td style="">
                                                <asp:DropDownList ID="ddlShowPercentage" runat="server" CssClass="dropDownList cboYesNo">
                                                    <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                &nbsp;
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr class="rowParam">
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                <asp:Label ID="lblControlAccountSummary" runat="server" Text="Control Account Summary:"></asp:Label>
                                            </td>
                                            <td style="">
                                                <asp:DropDownList ID="ddlControlAccountSummary" runat="server" CssClass="dropDownList cboYesNo">
                                                    <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                &nbsp;
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr class="rowParam">
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                <asp:Label ID="lblIncludeSubForControl" runat="server" Text="Show Sub Accounts :"></asp:Label>
                                            </td>
                                            <td style="">
                                                <asp:DropDownList ID="ddlIncludeSubForControl" runat="server" CssClass="dropDownList cboYesNo">
                                                    <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                &nbsp;
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr class="rowParam">
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                <asp:Label ID="lblIncludeContraEntry" runat="server" Text="Show Contra Entries:"></asp:Label>
                                            </td>
                                            <td style="">
                                                <asp:DropDownList ID="ddlIncludeContraEntry" runat="server" CssClass="dropDownList cboYesNo">
                                                    <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                &nbsp;
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr class="rowParam">
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                <asp:Label ID="lblIncludeInstrument" runat="server" Text="Show Instrument:"></asp:Label>
                                            </td>
                                            <td style="">
                                                <asp:DropDownList ID="ddlIncludeInstrument" runat="server" 
                                                    CssClass="dropDownList cboYesNo" Enabled="False">
                                                    <asp:ListItem  Value="0">No</asp:ListItem>
                                                    <asp:ListItem Selected="True" Value="1">Yes</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                &nbsp;
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr class="rowParam">
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                <asp:Label ID="lblIncludeCostCenter" runat="server" Text="Show Cost Center:"></asp:Label>
                                            </td>
                                            <td style="">
                                                <asp:DropDownList ID="ddlIncludeCostCenter" runat="server" CssClass="dropDownList cboYesNo">
                                                    <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                &nbsp;
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr class="rowParam">
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                <asp:Label ID="lblIncludeReference" runat="server" Text="Show Reference:"></asp:Label>
                                            </td>
                                            <td style="">
                                                <asp:DropDownList ID="ddlIncludeReference" runat="server" CssClass="dropDownList cboYesNo">
                                                    <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                &nbsp;
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr class="rowParam">
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                <asp:Label ID="lblIncludeTranCode" runat="server" Text="Show Tran. Code:" Visible="false"></asp:Label>
                                            </td>
                                            <td style="">
                                                <asp:DropDownList ID="ddlIncludeTranCode" runat="server" CssClass="dropDownList cboYesNo" Visible="false">
                                                    <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                &nbsp;
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr class="rowParam">
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                            </td>
                                            <td style="">
                                            </td>
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                &nbsp;
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr class="rowParam">
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                <asp:Label ID="lblIncludeDetOfDetails" runat="server" Text="Show Details For all:"></asp:Label>
                                            </td>
                                            <td style="">
                                                <asp:DropDownList ID="ddlIncludeDetOfDetails" runat="server" CssClass="dropDownList cboYesNo">
                                                    <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                &nbsp;
                                            </td>
                                            <td>
                                            </td>
                                        </tr>--%>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div id="dvContentFooter" class="dvContentFooter">
            <div id="dvContentFooterInner" class="dvContentFooterInner">
                <div style="width: 100%; height: 100%; margin-bottom: 0px;">
                    <div style="width: auto; min-width: 300px; height: auto; text-align: left;">
                        <table border="0">
                            <tr>
                                <td style="width: 100px;">
                                
                                </td>
                                <td>
                                    <asp:Label ID="Label2" runat="server" Text="Report View"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlReportViewMode" runat="server" CssClass="dropDownList">
                                        <asp:ListItem Value="0">In This Tab</asp:ListItem>
                                        <asp:ListItem Value="1">In New Tab</asp:ListItem>
                                        <asp:ListItem Value="2" Selected="True" >In New Window</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <asp:Button ID="btnView" runat="server" Text="View Report" Width="100px" CssClass="buttoncommon buttonPrintPreview"
                                        OnClick="btnView_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="btnPrint" runat="server" Text="Print Report" Width="100px" CssClass="buttoncommon buttonPrint"
                                        OnClick="btnPrint_Click" />
                                </td>
                                <td style="width: 20px;">
                                </td>
                                <td>
                                    <asp:Label ID="Label3" runat="server" Text="Get Report As:"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlExport" runat="server" Width="70" CssClass="dropDownList">
                                        <asp:ListItem Selected="True" Value="0">PDF</asp:ListItem>
                                        <asp:ListItem Value="1">Excel</asp:ListItem>
                                        <asp:ListItem Value="2">Word</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:Button ID="btnExport" runat="server" Text="Get Report" Width="100px" CssClass="buttoncommon buttonExport"
                                        OnClick="btnExport_Click" />
                                </td>
                                <td style="width: 10px;">
                                </td>
                                <td>
                                    <div id="dvPrintIFrame" class="dvPrintIFrame">
                                        <iframe id="ifPrintButton" runat="server" width="0" height="0"></iframe>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
   


     <div id="overlay" class="overlay" style="display:none;">
        <div style="margin: auto; width: 200px; height: 400px; background-color: black; border: solid 1px black; text-align: center; vertical-align: middle;">
            <span style="color: white; font-size: medium;">Please Wait...</span>
            <br />
            <img alt="" src="../../image/progress.gif" />
        </div>
    </div>
   </div>
    <div id="overlayReport" class="overlay" style="opacity: 0.8; display:none;">
        <div style="margin: auto; width: 450px; height: 80px; position: relative; background-color: blue; text-align: center; vertical-align: middle; cursor: auto; z-index: 9999999;">
            <table width="100%">
                <tr>
                    <td>
                        <span style="color: white; font-size: medium;">Click Open Report to view Report.</span>
                    </td>
                </tr>
                <tr></tr>
                <tr>
                    <td>
                        <input id="btnOpenReportWindow" type="button" value="Open Report" class="buttoncommon" />
                        <input id="btnCacnelReportWindow" type="button" value="Cancel" class="buttoncommon" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
      
</asp:Content>
