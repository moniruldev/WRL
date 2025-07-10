<%@ Page Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="LCSummaryReport.aspx.cs" Inherits="PG.Web.Report.INV.LCSummaryReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="DropDownCheckBoxes" Namespace="Saplin.Controls" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../../javascript/jquery.ui.combogrid.js" type="text/javascript"></script>
    <link href="../../css/jquery.ui.combogrid.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        // <!CDATA[

        var isPageResize = true;
        ContentForm.CalendarImageURL = "../../image/calendar.png";

        var ReportViewPageLink = '<%=this.ReportViewPageLink%>';
        var ReportViewPDFPageLink = '<%=this.ReportViewPDFPageLink%>';
        var ItemListServiceLink = '<%=this.ItemListServiceLink%>';
        var ifPrintButton = '<%=ifPrintButton.ClientID%>';


        var txtFromDateID = '<%=txtFromDate.ClientID%>';
        var txtToDateID = '<%=txtToDate.ClientID%>';


        var btnItemLoad = '<%= btnItemLoad.ClientID%>';
        var hdnItemIdForFilter = '<%= hdnItemIdForFilter.ClientID%>';
        var txtItemName = '<%= txtItemName.ClientID%>';



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

        $(document).ready(function () {

            if ($('#' + txtItemName).is(':visible')) {
                bindItemList();
            }

        });

        function bindItemList() {

            var cgColumns = [{ 'columnName': 'itemname', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                             , { 'columnName': 'uomname', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Uom Name' }
                             , { 'columnName': 'itemcode', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Code' }
                             , { 'columnName': 'itemgroupdesc', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Group Name' }
                             , { 'columnName': 'class_name', 'width': '120', 'align': 'left', 'highlight': 4, 'label': 'Class Name' }
                             , { 'columnName': 'item_type', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Item Type' }



            ];
            var serviceURL = ItemListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;


            serviceURL += "&ispaging=1";
            var groupIDElem = $('#' + txtItemName);

            $('#' + btnItemLoad).click(function (e) {
                $(groupIDElem).combogrid("dropdownClick");
            });

            $(groupIDElem).combogrid({
                debug: true,
                searchButton: false,
                resetButton: false,
                alternate: true,
                munit: 'px',
                scrollBar: true,
                showPager: true,
                colModel: cgColumns,
                autoFocus: true,
                showError: true,
                width: 850,
                url: serviceURL,
                search: function (event, ui) {
                    //var companyCode = $('#' + ddlCompany).val();
                    //var branchCode = $('#' + hdnBranch).val();
                    //var deptCode = $('#' + hdnDepartment).val();
                    //var locationid = $('#' + lblLocationID).val();
                    // var seid = $('#' + txtExecutiveID).val();
                    var vgroupid = 0;

                    //  var vgroupid = $(hdnItemGroupIDParent).val();
                    //var vgroupid = $('#' + hdngroupId).val();
                    //  newServiceURL = JSUtility.AddTimeToQueryString(newServiceURL);
                    var newServiceURL = serviceURL + "&groupid=" + vgroupid;

                    newServiceURL = JSUtility.AddTimeToQueryString(newServiceURL);
                    // var newServiceURL = serviceURL;
                    $(this).combogrid("option", "url", newServiceURL);


                },
                select: function (event, ui) {
                    if (!ui.item) {
                        event.preventDefault();

                        // $('#' + hdnDealerID).val('0');
                        //$('#' + txtDealerID).val('');
                        return false;
                        //ClearGLAccountData(elemID);
                    }


                    if (ui.item.dealerid == '') {
                        event.preventDefault();
                        return false;
                        //ClearGLAccountData(elemID);
                    }
                    else {
                        $('#' + hdnItemIdForFilter).val('0');
                        // $('#' + hdnDealerID).val(ui.item.dealerid);
                        $('#' + hdnItemIdForFilter).val(ui.item.itemid);
                        $('#' + txtItemName).val(ui.item.itemname);
                        //$('#' + txtGroupCode).val(ui.item.itemgroupcode);

                    }
                    return false;
                },

                lc: ''
            });


            $(groupIDElem).blur(function () {
                var self = this;

                var groupID = $(groupIDElem).val();
                if (groupID == '') {
                    // $('#' + hdnDealerID).val('0');
                    $('#' + txtItemName).val('');
                    //$('#' + txtGroupCode).val('');
                }
            });
        }
        // ]]>
    </script>
    <style type="text/css">
        /*.FixedHeader { POSITION: relative; TOP: expression(this.offsetParent.scrollTop); BACKGROUND-COLOR: white } */

        .FixedHeader {
            POSITION: relative;
            BACKGROUND-COLOR: white;
        }

        #dvMessage {
            height: 20px;
        }

        .style1 {
            width: 113px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="dvPageContent" style="width: 100%; height: auto;">
        <div id="dvContentHeader" class="dvContentHeader">
            <div id="dvHeader" class="dvHeader">
                <asp:Label ID="lblHeader" CssClass="lblHeader" runat="server" Text="LC Summary Report"></asp:Label>
            </div>
            <!--Message Div -->
            <div id="dvMsg" runat="server" class="dvMessage">
                <asp:Label ID="lblMessage" runat="server" Font-Size="Small" Width="100%"> </asp:Label>
            </div>
            <div id="dvHeaderControl" class="dvHeaderControl">
            </div>
        </div>

        <div id="dvContentMain" class="dvContentMain">
            <div id="dvControls" style="height: auto; width: 100%">
                <div id="dvControlsInner" class="groupBoxContainer boxShadow">
                    <div id="groupBox">
                        <div id="groupHeader" class="groupHeader">
                            <div style="width: 100%; height: 20px;">
                                <table>
                                    <tr>
                                        <td>
                                            <div id="dvIconEditMode" class="iconView" runat="server"></div>
                                        </td>
                                        <td>
                                            <span>LC Summary Report</span>
                                        </td>
                                    </tr>
                                </table>

                            </div>

                        </div>
                        <div id="groupContent" class="groupContent" style="width: 70%; height: auto; overflow: auto;">
                            <div id="groupContenInner" style="width: 100%; height: auto; text-align: left;">
                                <table style="text-align: left;" border="0" cellspacing="4" cellpadding="2">


                                    <tr class="rowParam">
                                        <td></td>
                                        <td align="right">
                                            <asp:Label ID="lblFromDate" runat="server" Text="Date From:"></asp:Label>
                                        </td>
                                        <td>
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtFromDate" runat="server" Width="70px" CssClass="textBox textDate dateParse"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 4px;"></td>
                                                    <td>
                                                        <asp:Label ID="lblToDate" runat="server" Text="Date To:"></asp:Label>
                                                    </td>
                                                    <td style="width: 2px;"></td>
                                                    <td>
                                                        <asp:TextBox ID="txtToDate" runat="server" Width="70px" CssClass="textBox textDate dateParse"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td align="right" class="">&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                    </tr>
                                      <tr>
                                        <td></td>
                                        <td style="" align="left"></td>
                                        <td style="" align="left">                                      
                                            <asp:Button ID="btnLoadLc" runat="server" CssClass="buttoncommon"  Width="80px"
                                                Text="Load LC" OnClick="btnLoadLc_Click"  />
                                        </td>

                                        <td style="" align="right"></td>
                                        <td style="" align="left"></td>

                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td style="" align="right">
                                            <asp:Label ID="Label3" runat="server" Text="Lc Number"></asp:Label>
                                        </td>
                                        <td style="" align="left">
                                            <asp:DropDownList ID="ddlLCList" runat="server" Width="240" CssClass="dropDownList enableIsDirty"></asp:DropDownList>
                                        </td>
                                        <td style="" align="left"></td>
                                        <td style="" align="left"></td>

                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td style="" align="right">
                                            <asp:Label ID="Label8" runat="server" Text="Purchase No"></asp:Label>
                                        </td>
                                        <td style="" align="left">

                                            <asp:TextBox ID="txtPurchaseNo" runat="server" Width="235px" CssClass="textBox fldRequired enableIsDirty"></asp:TextBox>

                                        </td>
                                        <td style="" align="right"></td>
                                        <td style="" align="left"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td style="" align="right">
                                            <asp:Label ID="Label1" runat="server" Text="LC No"></asp:Label>
                                        </td>
                                        <td style="" align="left">

                                            <asp:TextBox ID="txtLCNo" runat="server" Width="235px" CssClass="textBox fldRequired enableIsDirty"></asp:TextBox>

                                        </td>
                                        <td style="" align="right"></td>
                                        <td style="" align="left"></td>
                                        <td></td>
                                    </tr>

                                    <tr>
                                        <td></td>
                                        <td style="" align="right">
                                            <asp:Label ID="lblDeptName" runat="server" Text="Supplier"></asp:Label>
                                        </td>
                                        <td style="" align="left">
                                            <asp:DropDownList ID="ddlSupplier" runat="server" Width="240" CssClass="dropDownList enableIsDirty"></asp:DropDownList>
                                        </td>
                                        <td style="" align="left"></td>
                                        <td style="" align="left"></td>

                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:HiddenField ID="hdnItemIdForFilter" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td style="" align="right">
                                            <asp:Label ID="Label4" runat="server" Text="Item"></asp:Label>
                                        </td>
                                        <td style="" align="left">
                                            <asp:TextBox ID="txtItemName" Width="235px" runat="server" CssClass="textBox required" Enabled="true"></asp:TextBox><input id="btnItemLoad" type="button" value="" runat="server" class="buttonDropdown" tabindex="-1" />

                                        </td>
                                        <td style="" align="right"></td>
                                        <td style="" align="left"></td>
                                        <td></td>
                                    </tr>



                                    <tr>
                                        <td></td>
                                        <td style="" align="left"></td>
                                        <td style="" align="left">                                      
                                            <asp:Button ID="buttonIRRPreview" runat="server" CssClass="buttoncommon buttonPrint" Style="padding-left: 22px;" Width="100px"
                                                Text="Preview" OnClick="btnIRRPreview_Click" />
                                        </td>

                                        <td style="" align="right"></td>
                                        <td style="" align="left"></td>

                                        <td></td>
                                    </tr>
                                </table>
                            </div>

                        </div>

                    </div>
                </div>
            </div>
            <br />
            <br />
        </div>
        <div id="dvContentFooter" class="dvContentFooter">
            <div id="dvContentFooterInner" class="dvContentFooterInner">
                <div style="width: 100%; height: 100%; margin-bottom: 0px;">
                    <div style="width: auto; min-width: 300px; height: auto; text-align: left;">
                        <table border="0">
                            <tr>
                                <td style="width: 100px;"></td>
                                <td>
                                    <asp:Label ID="Label2" runat="server" Text="Report View"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlReportViewMode" runat="server" CssClass="dropDownList">
                                        <asp:ListItem Value="0">In This Tab</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="1">In New Tab</asp:ListItem>
                                        <asp:ListItem Value="2">In New Window</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlReportViewType" runat="server" CssClass="dropDownList">
                                        <asp:ListItem Value="0">Screen</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="1">PDF</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td></td>

                                <td style="width: 20px;"></td>
                                <td style="width: 10px;"></td>
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

    </div>
</asp:Content>


