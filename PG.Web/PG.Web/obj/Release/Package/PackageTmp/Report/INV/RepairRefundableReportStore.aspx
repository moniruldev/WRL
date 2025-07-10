<%@ Page Title="" Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="RepairRefundableReportStore.aspx.cs" Inherits="PG.Web.Report.INV.RepairRefundableReportStore" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="DropDownCheckBoxes" Namespace="Saplin.Controls" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../../javascript/jquery.ui.combogrid.js" type="text/javascript"></script>
    <script src="../../javascript/jquery.attributeobserver.js" type="text/javascript"></script>
    <link href="../../css/jquery.ui.combogrid.css" rel="stylesheet" type="text/css" />


    <script language="javascript" type="text/javascript">
        // <!CDATA[

        var isPageResize = true;
        ContentForm.CalendarImageURL = "../../image/calendar.png";

       <%-- var hdnCompanyID = '<%=hdnCompanyID.ClientID%>';--%>

        var ReportViewPageLink = '<%=this.ReportViewPageLink%>';
        var ReportViewPDFPageLink = '<%=this.ReportViewPDFPageLink%>';
        var ReportPrintPageLink = '<%=this.ReportPrintPageLink%>';
        var ReportPDFPageLink = '<%=this.ReportPDFPageLink%>';

        var ItemGroupListServiceLink = '<%=this.ItemGroupListServiceLink%>';
        var ItemListServiceLink = '<%=this.ItemListServiceLink%>';
        var CustomerListServiceLink = '<%=this.CustomerListServiceLink%>';

        var hdnGroupID = '<%= hdnGroupID.ClientID%>';
        var txtGroupName = '<%= txtGroupName.ClientID%>';
        var btnGroupID = '<%= btnGroupID.ClientID%>';

        var hdnItemID = '<%=hdnItemID.ClientID %>';
        var txtItemName = '<%=txtItemName.ClientID %>';
        var btnItemID = '<%= btnItemID.ClientID%>';

        var txtCustomerName = '<%=txtCustomerName.ClientID%>';
        var btnCustomerID = '<%=btnCustomerID.ClientID%>';

        <%--  var txtCustomerID = '<%=txtCustomerID.ClientID%>';--%>
        var hdnCustomerID = '<%=hdnCustomerID.ClientID%>';
        var txtCustomerAddress = '<%=txtCustomerAddress.ClientID%>';


        var ifPrintButton = '<%=ifPrintButton.ClientID%>';


        var txtFromDateID = '<%=txtFromDate.ClientID%>';
        var txtToDateID = '<%=txtToDate.ClientID%>';


       <%-- var ddlLocation = '<%=ddlLocation.ClientID%>';--%>

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


        function tbopen(key, pdfView, isPrint, isPDFAutoPrint, showWait) {
            hideOverlay();


            key = key || '';
            isPrint = isPrint || false;
            showWait = showWait || true;

            if (isPrint) {
                if (key != '') {
                    ReportPrint(key, isPDFAutoPrint);
                    return;
                }
            }

            //var url = "/Report/ReportView.aspx?rk=" + key

            var now = new Date();
            var strTime = now.getTime().toString();
            var url = ReportViewPageLink + "?rk=" + key + "&_tt=" + strTime;
            if (pdfView) {
                url = ReportViewPDFPageLink + "?rk=" + key + "&_tt=" + strTime;
            }
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

        function ReportPrint(key, isPDFAutoPrint) {
            var rptPageLink = ReportViewPageLink;
            if (isPDFAutoPrint) {
                //rptPageLink = ReportPDFPageLink;
                rptPageLink = ReportViewPDFPageLink;
            }

            //var url = "./Report/ReportView.aspx?rk=" + key
            var now = new Date();
            var strTime = now.getTime().toString();
            var url = ReportViewPageLink + "?rk=" + key + "&_tt=" + strTime;

            //var url = rptPageLink + "?rk=" + key;

            iframe = document.getElementById(ifPrintButton);
            if (iframe === null) {
                iframe = document.createElement('iframe');
                iframe.id = hiddenIFrameID;
                //        iframe.style.display = 'none';
                //        iframe.style = 'none';
                document.body.appendChild(iframe);
            }
            iframe.src = url;

            hideOverlay();
        }

        function fromParent(val1) {
            alert('this is called from parent: ' + val1);
        }

        $(document).ready(function () {
            str = document.body.innerHTML


            //    $('#tblParam tr').each(function () {
            //        if ($(this).find('td:empty').length) $(this).remove();
            //    });

            $("#tblParam tr.rowParam").each(function () {
                var cell = $.trim($(this).find('td').text());
                if (cell.length == 0) {
                    //console.log('empty');
                    //$(this).addClass('nodisplay');
                    $(this).hide();
                }
            });

            hideOverlay();

        });



        $(document).ready(function () {


            //alert("error in page");
            if ($('#' + txtGroupName).is(':visible')) {
                //alert(1);
                bindGroupList();
                //alert(2);
            }
            //alert(3);
            if ($('#' + txtItemName).is(':visible')) {
                bindItemList();
            }
            //alert('OK 5');
            if ($('#' + txtCustomerName).is(':visible')) {
                //alert('OK 2');
                bindCustomerList();
                //alert('OK 3');
            }



        });


        function getCheckBoxes() {
            var checkValues = '';
            var comma = '';
            $('#' + ddlLocation + ' input:checked').each(function () {
                // selected.push($(this).attr('name'));
                checkValues = checkValues + comma + $(this).val();
                comma = ',';
            });

            /// alert(checkValues);
            return checkValues;

        }

        //this is for group dropdown
        function bindGroupList() {
            var cgColumns = [
                             //{ 'columnName': 'itemgroupid', 'width': '80', 'align': 'left', 'highlight': 2, 'label': 'Group ID' }
                             { 'columnName': 'itemgroupcode', 'width': '50', 'align': 'left', 'highlight': 4, 'label': 'Code' }
                            , { 'columnName': 'itemgroupdesc', 'width': '160', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                            , { 'columnName': 'itemgroupnameparent', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Group Parent' }


            ];
            var serviceURL = ItemGroupListServiceLink + "?isterm=1&includeempty=0&hasitem=1&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;

            serviceURL += "&ispaging=1";
            var groupIDElem = $('#' + txtGroupName);

            $('#' + btnGroupID).click(function (e) {
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
                width: 360,
                url: serviceURL,
                search: function (event, ui) {
                    //var companyCode = $('#' + ddlCompany).val();
                    //var branchCode = $('#' + hdnBranch).val();
                    //var deptCode = $('#' + hdnDepartment).val();
                    //var locationid = $('#' + lblLocationID).val();
                    // var seid = $('#' + txtExecutiveID).val();
                    var newServiceURL = serviceURL;
                    $(this).combogrid("option", "url", newServiceURL);


                },
                select: function (event, ui) {
                    if (!ui.item) {
                        event.preventDefault();

                        $('#' + hdnGroupID).val('0');
                        //$('#' + txtDealerID).val('');
                        return false;
                        //ClearGLAccountData(elemID);
                    }


                    if (ui.item.itemgroupid == '') {
                        event.preventDefault();
                        return false;
                        //ClearGLAccountData(elemID);
                    }
                    else {
                        // $('#' + hdnDealerID).val(ui.item.dealerid);
                        $('#' + hdnGroupID).val(ui.item.itemgroupid);
                        $('#' + txtGroupName).val(ui.item.itemgroupdesc);
                        //$('#' + txtGroupCode).val(ui.item.itemgroupcode);

                    }
                    return false;
                },

                lc: ''
            });

            $(groupIDElem).blur(function () {
                var self = this;
                elemID = $(groupIDElem).attr('id');
                eCode = $(groupIDElem).val();
                isComboGridOpen = $(self).combogrid('isOpened');
                if (eCode == '') {
                    //                    if (!validateGLAccount(elemID, null)) {
                    //                        $(elem).val(prevGLCode);
                    //                        return false;
                    //                    }
                    $('#' + hdnGroupID).val('0');
                }
                else {
                    //grp = GetGLGroup(eCode);
                    ////                    if (!validateGLAccount(elemID, grp)) {
                    ////                        $(elem).val(prevGLCode);
                    ////                        return false;
                    ////                    }

                    if (grp == null) {
                        $('#' + hdnGroupID).val('0');
                    }
                    else {
                        //SetItemGroupData(elemID, grp);
                    }
                }
                //setDetInstrument(hdnIsInstrumentElem, txtInstrumentElem, btnInstrumentElem);
                grpID = $(self).closest('tr').find('input[id$="hdnGroupID"]').val();
                if (grpID == '0' | grpID == '') {
                    $(self).addClass('textError');
                }

            });


            $(groupIDElem).blur(function () {
                var self = this;

                var groupID = $(groupIDElem).val();
                if (groupID == '') {
                    // $('#' + hdnDealerID).val('0');
                    $('#' + txtGroupName).val('');
                    //$('#' + txtGroupCode).val('');
                }
            });
        }

        //this is for item dropdown
        function bindItemList() {

            var cgColumns = [
                { 'columnName': 'itemcode', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Code' }
                             , { 'columnName': 'itemname', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                             , { 'columnName': 'itemgroupdesc', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Group' }
                             , { 'columnName': 'uomname', 'width': '40', 'align': 'left', 'highlight': 4, 'label': 'UOM' }
                              //, { 'columnName': 'class_name', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Class Name' }
                             , { 'columnName': 'item_type', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Item Type' }
            ];

            var itemServiceURL = ItemListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;

            itemServiceURL += "&ispaging=1";
            var itemIDElem = $('#' + txtItemName);

            $('#' + btnItemID).click(function (e) {
                $(itemIDElem).combogrid("dropdownClick");
            });

            $(itemIDElem).combogrid({
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
                width: 500,
                url: itemServiceURL,
                search: function (event, ui) {
                    //var companyCode = $('#' + ddlCompany).val();
                    //var branchCode = $('#' + hdnBranch).val();
                    //var deptCode = $('#' + hdnDepartment).val();
                    //var locationid = $('#' + lblLocationID).val();
                    // var seid = $('#' + txtExecutiveID).val();
                    var vgroupid = $('#' + hdnGroupID).val();
                    var newServiceURL = itemServiceURL + "&groupid=" + vgroupid;
                    $(this).combogrid("option", "url", newServiceURL);


                },
                select: function (event, ui) {
                    if (!ui.item) {
                        event.preventDefault();

                        $('#' + hdnGroupID).val('0');
                        $('#' + hdnItemID).val('0');
                        return false;
                        //ClearGLAccountData(elemID);
                    }


                    if (ui.item.itemid == '') {
                        event.preventDefault();
                        return false;
                        //ClearGLAccountData(elemID);
                    }
                    else {
                        // $('#' + hdnDealerID).val(ui.item.dealerid);
                        $('#' + hdnItemID).val(ui.item.itemid);
                        $('#' + txtItemName).val(ui.item.itemname);

                        //$('#' + hdnUomID).val(ui.item.itemid);
                        //$('#' + hdnUomName).val(ui.item.uomname);

                        //$('#' + txtGroupCode).val(ui.item.itemgroupcode);

                    }
                    return false;
                },

                lc: ''
            });


            $(itemIDElem).blur(function () {
                var self = this;
                var groupID = $(itemIDElem).val();
                if (groupID == '') {
                    // $('#' + hdnDealerID).val('0');
                    $('#' + txtItemName).val('');
                    $('#' + hdnItemID).val('0');
                    //$('#' + txtGroupCode).val('');
                }
            });
        }

        function bindCustomerList() {
            var cgColumns = [{ 'columnName': 'custname', 'width': '180', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                             , { 'columnName': 'custcode', 'width': '50', 'align': 'left', 'highlight': 4, 'label': 'Code' }
                             , { 'columnName': 'custaddress', 'width': '100', 'align': 'left', 'highlight': 0, 'label': 'address' }
                             , { 'columnName': 'custphone', 'width': '150', 'align': 'left', 'highlight': 0, 'label': 'Phone' }


            ];


            //var companyid = $('#' + hdnCompanyID).val();
            //var depthead = $('#' + hdnEmpCode).val();
            //var locationid = $('#' + ddlLocation).val();
            // var seid = $('#' + txtExecutiveID).val();

            //var serviceURL = GLAccountServiceLink + "?isterm=1&includeempty=1&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            var serviceURL = CustomerListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            //serviceURL += "&companyid=" + companyid;
            serviceURL += "&ispaging=1";
            serviceURL += "&isRefundable=" + "Y";
            //serviceURL += "&isRotary=" + "N";
            //serviceURL += "&isService_Center=" + "N";
            //serviceURL += "&acctypefilter=" + Enums.GLAccountTypeFilter.AllAccount;
            // serviceURL += "&namecomptype=" + Enums.DataCompareType.Contains;



            var customerIDElem = $('#' + txtCustomerName);

            $('#' + btnCustomerID).click(function (e) {
                //elmID = $(elem).attr('id');
                //$(elem).combogrid("show");
                $(customerIDElem).combogrid("dropdownClick");
            });


            $(customerIDElem).combogrid({
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
                width: 700,
                url: serviceURL,
                search: function (event, ui) {
                    //var companyCode = $('#' + ddlCompany).val();
                    //var branchCode = $('#' + hdnBranch).val();
                    //var deptCode = $('#' + hdnDepartment).val();
                    //var locationid = $('#' + lblLocationID).val();
                    // var seid = $('#' + txtExecutiveID).val();
                    var newServiceURL = serviceURL;
                    $(this).combogrid("option", "url", newServiceURL);


                },
                select: function (event, ui) {
                    if (!ui.item) {
                        event.preventDefault();

                        // $('#' + hdnDealerID).val('0');
                        $('#' + txtDealerID).val('');
                        return false;
                        //ClearGLAccountData(elemID);
                    }


                    if (ui.item.dealerid == '') {
                        event.preventDefault();
                        return false;
                        //ClearGLAccountData(elemID);
                    }
                    else {
                        // $('#' + hdnDealerID).val(ui.item.dealerid);
                        $('#' + hdnCustomerID).val(ui.item.customerid);
                        $('#' + txtCustomerName).val(ui.item.custname);
                        $('#' + txtCustomerAddress).val(ui.item.custaddress);

                    }
                    return false;
                },

                lc: ''
            });


            $(customerIDElem).blur(function () {
                var self = this;

                var customerID = $(customerIDElem).val();
                if (customerID == '') {
                    // $('#' + hdnDealerID).val('0');
                    $('#' + txtCustomerName).val('');
                    $('#' + txtCustomerAddress).val('');
                }
            });
        }

        // ]]>
    </script>
    <style type="text/css">
        .dvGroup {
            width: 182px;
            height: 20px;
            border: 1px solid lightgrey;
        }


        .dvGroupListPopup {
            display: none;
            height: 0px;
            width: 0px;
        }


        .textPopup {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            border: 1px #1B68B8 solid;
            background-color: #FFFFFF;
            color: #000000;
            font-size: 11px;
            width: 160px;
            height: 16px;
            padding-left: 2px;
        }

        .btnPopup {
            height: 20px;
            width: 16px;
            background-image: url(/image/dropdown.gif);
            background-repeat: no-repeat;
            background-position: center bottom;
            cursor: pointer;
        }

            .btnPopup:hover {
                background-image: url(/image/dropdown_over.gif);
            }

        .dvSpacer {
            height: 10px;
            width: 100%;
        }


        .dvReportList {
            height: 100%;
            width: 100%;
            overflow: auto;
        }

        .dvParam {
            height: 100%;
            width: 100%;
            overflow: auto;
        }


        .tblParam {
            /* border-collapse: collapse;    */
            height: auto;
        }




            .tblParam td {
                height: auto;
            }


        .cboYesNo {
            width: 50px;
        }

        .tdSpacer {
            width: 10px;
        }

        .rowParam {
        }

        .rowSpacer {
            height: 20px;
        }


        .dvPrintIFrame {
            height: 0px;
            width: 0px;
        }

        .overlay {
            background-color: #000;
            cursor: wait;
            display: none;
            height: 100%;
            left: 0;
            opacity: 0.4;
            position: fixed;
            top: 0;
            width: 100%;
            z-index: 9999998;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="dvPageContent" style="width: 100%; height: 100%;">
        <div id="dvContentHeader" class="dvContentHeader">
            <div id="dvHeader" class="dvHeader">
                Report - MIS
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


                            <td valign="top" style="">
                                <div id="dvParamHeader" class="dvParamHeader" style="height: auto; width: 100%;">
                                    <table cellspacing="0" cellpadding="0" border="0" style="height: auto; width: 100%;">
                                        <tr>
                                            <td style="border-bottom: 1px solid grey;">
                                                <asp:Label ID="lblReportName" runat="server" Text="Repair and Refundable Reports" Font-Bold="True"
                                                    Font-Size="10pt"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="groupBox" style="height: auto; width: 100%">
                    <table id="tblMiddle" cellspacing="0" cellpadding="0" style="height: 100%; width: 100%; min-width: 700px;">
                        <tr style="height: 100%">
                            <td></td>
                            <td valign="top" style="width: 0px; display: none">
                                <div id="dvReportList" class="dvReportList" style="display: none">
                                    <table cellspacing="2" cellpadding="1">
                                        <tr>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td>
                                                <asp:TreeView ID="tvwReport" runat="server" OnSelectedNodeChanged="tvwReport_SelectedNodeChanged"
                                                    NodeIndent="10">
                                                    <HoverNodeStyle BackColor="#CCCCFF" />
                                                    <Nodes>
                                                        <asp:TreeNode Text="Sales Return Report" Value="Stock_Reports" Expanded="True" SelectAction="Expand">
                                                            <asp:TreeNode Text="Refundable Report" Value="1756" Selected="True"></asp:TreeNode>
                                                            <%-- <asp:TreeNode Text="Comparision Report" Value="1621" Selected="false"></asp:TreeNode>
                                                            <asp:TreeNode Text="Stock Ageing Report" Value="1622" Selected="false"></asp:TreeNode>--%>
                                                        </asp:TreeNode>
                                                    </Nodes>
                                                    <NodeStyle ForeColor="Black" />
                                                    <SelectedNodeStyle BackColor="#CCCCFF" ForeColor="White" Font-Bold="True" />
                                                </asp:TreeView>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                            <td style="border-left: 1px solid grey;"></td>
                            <td valign="top" style="height: 100%;">
                                <div id="dvParam" class="dvParam">
                                    <table id="tblParam" cellspacing="4" cellpadding="2" border="0" class="tblParam">
                                       

                                        <tr class="rowParam">
                                            <td></td>
                                            <td style="" align="right">&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td></td>
                                            <td style="" align="right"></td>
                                            <td></td>
                                        </tr>


                                        <tr class="rowParam">
                                            <td></td>
                                            <td style="" align="right">
                                                <asp:Label ID="lblItemType" runat="server" Text="Item Type:" Visible="True"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlItemType" runat="server" Width="180px" CssClass="dropDownList" Visible="True">
                                                    <%--<asp:ListItem Selected="True" Text="List" Value="0"> </asp:ListItem>--%>
                                                </asp:DropDownList>
                                            </td>
                                            <td></td>
                                            <td style="" align="right"></td>
                                            <td></td>
                                        </tr>

                                        <tr class="rowParam">
                                            <td></td>
                                            <td style="" align="right">&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td></td>
                                            <td style="" align="right"></td>
                                            <td></td>
                                        </tr>

                                        <tr class="rowParam">
                                            <td></td>
                                            <td style="" align="right">
                                                <asp:Label ID="lblItemClass" runat="server" Text="Item Class:" Visible="True"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlItemClass" runat="server" Width="180px" CssClass="dropDownList" Visible="True">
                                                    <%--<asp:ListItem Selected="True" Text="List" Value="0"> </asp:ListItem>--%>
                                                </asp:DropDownList>

                                            </td>
                                            <td></td>
                                            <td style="" align="right"></td>
                                            <td></td>
                                        </tr>


                                        <tr class="rowParam">
                                            <td></td>
                                            <td style="" align="right">&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td></td>
                                            <td style="" align="right"></td>
                                            <td></td>
                                        </tr>


                                        <tr class="rowParam">
                                            <td></td>
                                            <td style="" align="right">
                                                <asp:Label ID="lblItemGroup" runat="server" Text="Item Group:" Visible="True"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtGroupName" runat="server" CssClass="textBox required" Enabled="true"></asp:TextBox>
                                                <input id="btnGroupID" type="button" value="" runat="server" class="buttonDropdown" tabindex="-1" />

                                            </td>
                                            <td>
                                                <asp:HiddenField ID="hdnGroupID" runat="server" Value="0" />
                                            </td>
                                            <td style="" align="right"></td>
                                            <td></td>
                                        </tr>
                                        <tr class="rowParam">
                                            <td></td>
                                            <td style="" align="right">&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td></td>
                                            <td style="" align="right"></td>
                                            <td></td>
                                        </tr>

                                        <tr class="rowParam">
                                            <td></td>
                                            <td style="" align="right">
                                                <asp:Label ID="lblItemName" runat="server" Text="Item Name:" Visible="True"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtItemName" runat="server" CssClass="textBox required" Enabled="true"></asp:TextBox>
                                                <input id="btnItemID" type="button" value="" runat="server" class="buttonDropdown" tabindex="-1" />

                                            </td>
                                            <td>
                                                <asp:HiddenField ID="hdnItemID" runat="server" Value="0" />
                                            </td>
                                            <td style="" align="right"></td>
                                            <td></td>
                                        </tr>
                                        <tr class="rowParam">
                                            <td></td>
                                            <td style="" align="right">&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td></td>
                                            <td style="" align="right"></td>
                                            <td></td>
                                        </tr>

                                       
                     <tr>
               <td></td>
                                                        <td align="right" class="auto-style2">
                                                            <asp:Label ID="lblCustomer" runat="server" Text="Customer:" Font-Bold="true"></asp:Label>
                                                        </td>

                                                        <td class="auto-style2">

                                                            <asp:TextBox ID="txtCustomerName" runat="server" CssClass="textBox" Enabled="true"></asp:TextBox>
                                                             <input id="btnCustomerID" type="button" value="" runat="server" class="buttonDropdown"
                                                                tabindex="-1" />
                                                        </td>
                                                        
                                                        <td colspan="4"  align="left">

                                                            <asp:TextBox ID="txtCustomerAddress" runat="server" CssClass="textBox" Enabled="false" Width="200px"></asp:TextBox>
                                                            <%--<asp:TextBox ID="txtCustomerID" runat="server" CssClass="textBox" Visible="true" Width="10px"></asp:TextBox>--%>
                                                            
                                                        </td>

             

                                                    </tr>

                                        <tr class="rowParam">
                                            <td></td>
                                            <td align="right">
                                                <asp:Label ID="lblFromDate" runat="server" Text="Date From:"></asp:Label>
                                            </td>
                                            <td>
                                                <table cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txtFromDate" runat="server" Width="85px" CssClass="textBox textDate dateParse"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 4px;"></td>
                                                        <td>
                                                            <asp:Label ID="lblToDate" runat="server" Text="To:"></asp:Label>
                                                        </td>
                                                        <td style="width: 2px;"></td>
                                                        <td>
                                                            <asp:TextBox ID="txtToDate" runat="server" Width="85px" CssClass="textBox textDate dateParse"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td align="right" class="">&nbsp;
                                                <asp:HiddenField ID="hdnCustomerID" runat="server" Value="0"  />
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                        </tr>

                                        <tr class="rowParam">
                                            <td></td>
                                            <td style="" align="right">&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td></td>
                                            <td style="" align="right"></td>
                                            <td></td>
                                        </tr>


                                        <%-- <tr >
                                            <td>
                                               
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="lblLocation" runat="server" Text="Location:"></asp:Label>
                                             </td>
                                            <td>
                                                <cc1:DropDownCheckBoxes ID="ddlLocation" runat="server" AddJQueryReference="false" UseButtons="false"  UseSelectAllNode="True" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged" AutoPostBack="true">
                                                <Style2 SelectBoxWidth="220px" DropDownBoxBoxWidth="" DropDownBoxBoxHeight=""></Style2>
                                                <Texts OkButton="OK" CancelButton="Cancel" SelectAllNode="All Types" SelectBoxCaption="Select" />
                                                </cc1:DropDownCheckBoxes>
                                               <%-- <asp:DropDownList ID="ddlLocation" runat="server" Width="170px" CssClass="dropDownList" AutoPostBack="True" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged" ></asp:DropDownList>--%>
                                        <%-- </td>
                                             <td>
                                            </td>
                                            <td style="" align="right">
                                            </td>
                                            <td>
                                            </td>
                                        </tr>--%>
                                        <tr class="rowParam">
                                            <td></td>
                                            <td style="" align="right">&nbsp;</td>
                                            <td>&nbsp;

                                            </td>
                                            <td></td>
                                            <td style="" align="right"></td>
                                            <td></td>
                                        </tr>

                                        <tr>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                            <td></td>
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
                                <td style="width: 100px;"></td>
                                <td>
                                    <asp:Label ID="Label2" runat="server" Text="Report View"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlReportViewMode" runat="server" CssClass="dropDownList">
                                        <asp:ListItem Value="0">In This Tab</asp:ListItem>
                                        <asp:ListItem Value="1">In New Tab</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="2">In New Window</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td></td>
                                <td>
                                    <asp:Label ID="Label1" runat="server" Text="Type:"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlReportViewType" runat="server" CssClass="dropDownList">
                                        <asp:ListItem Value="0">Screen</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="1">PDF</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td></td>
                                <td>
                                    <asp:Button ID="btnView" runat="server" Text="View Report" Width="100px" CssClass="buttoncommon buttonPrintPreview"
                                        OnClick="btnView_Click" OnClientClick="showOverlay();" />
                                </td>
                                <td>
                                    <asp:Button ID="btnPrint" runat="server" Text="Print Report" Width="100px" CssClass="buttoncommon buttonPrint"
                                        OnClick="btnPrint_Click" OnClientClick="showOverlay();" Visible="False" />
                                </td>
                                <td style="width: 20px;"></td>
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
                                        OnClick="btnExport_Click" OnClientClick="showOverlay();" />
                                </td>
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


    <div id="overlay" class="overlay">
        <div style="margin: auto; width: 200px; height: 400px; background-color: black; border: solid 1px black; text-align: center; vertical-align: middle;">
            <span style="color: white; font-size: medium;">Please Wait...</span>
            <br />
            <img alt="" src="../../image/progress.gif" />
        </div>
    </div>
</asp:Content>

