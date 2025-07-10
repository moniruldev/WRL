<%@ Page Title="" Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="IBCuttingReceive.aspx.cs" Inherits="PG.Web.Production.IBCuttingReceive" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="../javascript/jquery.ui.combogrid.js" type="text/javascript"></script>
    <script src="../javascript/jquery.attributeobserver.js" type="text/javascript"></script>
    <link href="../css/jquery.ui.combogrid.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">

        var isPageResize = true;

        ContentForm.CalendarImageURL = "../image/calendar.png";
        var ItemListServiceLinkd = '<%=this.Get_LinkStockTransferableItem_List%>';
        var txtTotQty = '<%=this.txtTotQty.ClientID%>';

        var gridUpdatePanelIDDet = '<%=UpdatePanel1.ClientID%>';
        var gridViewIDDet = '<%=grdTransferList.ClientID%>';
        var updateProgressID = '<%=UpdateProgress2.ClientID%>';

        var ddlDEPT_ID = '<%=ddlDEPT_ID.ClientID%>';
        var ddlITEM_NAME = '<%=ddlITEM_NAME.ClientID%>';

        function onlyNos(e, t) {
            try {
                if (window.event) {
                    var charCode = window.event.keyCode;
                }
                else if (e) {
                    var charCode = e.which;
                }
                else { return true; }
                if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                    return false;
                }
                return true;
            }
            catch (err) {
                alert(err.Description);
            }
        }
        $(document).on('blur', '.txtQty', function () {
            //var weight = $(this).closest('tr').find('.txtWeightQty').val();
            //alert(weight);
            // TestOnTextChange(this);
            sumGrandQty();

        });

        function sumGrandQty() {

            var totAdded = GetTotalSumAddedQty();
            $("#" + txtTotQty).val(JSUtility.FormatCurrency(totAdded));
        }

        function GetTotalSumAddedQty() {
            debugger;
            var totAdd = 0;
            var totalAmount = 0;
            var itemTotalWt = 0;


            $(document).find('.txtQty').each(function (index, elem) {
                // var weight = $(elem).closest('tr').find('.txtPcQty').val();             

                var addQty = parseFloat(JSUtility.GetNumber($(elem).val()));
                if (!isNaN(addQty)) {
                    totAdd += Number(addQty);

                    //   itemTotalWt = itemTotalWt + (weight * addQty);
                }
            });
            //$("#" + txtPanelWeight).val(JSUtility.FormatCurrency(itemTotalWt));
            return totAdd;
        }





        function showOverlay() {
            document.getElementById("overlay").style.display = "block";
        }

        function hideOverlay() {
            document.getElementById("overlay").style.display = "none";
        }

        function ShowProgress() {
            $('#' + updateProgressID).show();
        }

        function UserDeleteConfirmation() {
            return confirm("Are you sure you want to Save and Authorized?");
        }


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
        }





        function fromParent(val1) {
            alert('this is called from parent: ' + val1);
        }

        // alert('OK');
        $(document).ready(function () {

            var pageInstance = Sys.WebForms.PageRequestManager.getInstance();

            pageInstance.add_pageLoaded(function (sender, args) {
                var panels = args.get_panelsUpdated();
                for (i = 0; i < panels.length; i++) {
                    //alert(panels[i].id);
                    //ContentForm.InitDefualtFeatureInScope(panels[i].id);

                    if (panels[i].id == gridUpdatePanelIDDet) {
                        bindItemList(gridViewIDDet);
                        // bindBatchList(gridViewIDDet);
                    }

                }
                //$('#' + dvGridDetailsPopup).parent().appendTo(jQuery("form:first"));
                //gridTaskAfter();

            });


            // alert('OK 1');

            //if ($('#' + txtCompanyName).is(':visible')) {

            //    bindCompanyList();
            //}

            //if ($('#' + txtCustomerName).is(':visible')) {

            //    bindCustomerList();
            //}
            ////alert('OK 1');
            //bindItemGroupList(gridViewIDDet);
            //alert('OK 2');

            bindItemList(gridViewIDDet);
            // bindBatchList(gridViewIDDet);
        });



        function bindItemList(gridViewID) {
            var cgColumns = [
                               { 'columnName': 'itemname', 'width': '200', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                             , { 'columnName': 'uomname', 'width': '70', 'align': 'left', 'highlight': 4, 'label': 'Uom Name' }
                             , { 'columnName': 'bomname', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'BOM Name' }
            ];


            //var companyid = $('#' + hdnCompanyID).val();
            //var depthead = $('#' + hdnEmpCode).val();
            var DEPT_ID = $('#' + ddlDEPT_ID).val();
            var itemid = $('#' + ddlITEM_NAME).val();
            //alert(DEPT_ID);
            //var serviceURL = GLAccountServiceLink + "?isterm=1&includeempty=1&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            var serviceURL = ItemListServiceLinkd + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            //serviceURL += "&companyid=" + companyid;&isFinished=Y
            serviceURL += "&ispaging=1&deptid=" + DEPT_ID + "&itemid=" + itemid;
            //serviceURL += "&locationid=" + locationid;
            // serviceURL += "&empstatus=" + "A";
            //serviceURL += "&acctypefilter=" + Enums.GLAccountTypeFilter.AllAccount;
            // serviceURL += "&namecomptype=" + Enums.DataCompareType.Contains;
            var gridSelector = "#" + gridViewID;


            $(gridSelector).find('input[id$="txtSTOCK_TRANSFER_ITEM_NAME"]').each(function (index, elem) {
                ///list click
                var elemRow = $(elem).closest('tr.gridRow');

                var hdnItemIDElem = $(elemRow).find('input[id$="txtSTOCK_TRANSFER_ITEM_NAME"]');

                //var prevGLCode = '';

                $(elem).closest('tr').find('input[id$="btnITEM_NAME"]').click(function (e) {
                    elmID = $(elem).attr('id');
                    //$(elem).combogrid("show");
                    $(elem).combogrid("dropdownClick");
                });



                //var compNameElem = $('#' + txtCompanyName);

                //$('#' + btnCompanyID).click(function (e) {
                //    //elmID = $(elem).attr('id');
                //    //$(elem).combogrid("show");
                //    $(compNameElem).combogrid("dropdownClick");
                //});


                $(elem).combogrid({
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
                    url: serviceURL,
                    search: function (event, ui) {
                        var elemRowCur = $(elem).closest('tr.gridRow');
                        //var vgroupid = $(elemRowCur).find('input[id$="hdngroupId"]').val();
                        //var vgroupid = $('#' + hdngroupId).val();

                        //var elemRowCur = $(elem).closest('tr.gridRow');
                        //var vbatchNo = $(elemRowCur).find('input[id$="hdnBatchNo"]').val();
                        //  $(detRow).find('input[id$="hdnBatchNo"]').val(data.batchNO);
                        //var batchNo = $('#' + hdnBatchNo).val();
                        var newServiceURL = serviceURL; //+ "&batchno=" + vbatchNo
                        //var newServiceURL = serviceURL + "&companycode=" + companyCode + "&branchcode=" + branchCode + "&deptcode=" + deptCode
                        newServiceURL = JSUtility.AddTimeToQueryString(newServiceURL);
                        $(this).combogrid("option", "url", newServiceURL);


                    },

                    select: function (event, ui) {
                        //alert(ui.item.typename);
                        //$(".txtComboGrid").val(ui.item.code);
                        elemID = $(elem).attr('id');
                        //                    if (!validateGLAccount(elemID, ui.item)) {
                        //                        $(elem).val(prevGLCode);
                        //                        return false;
                        //                    }
                        if (!ui.item) {
                            event.preventDefault();
                            ClearItemData(elemID);
                            return false;
                            //ClearGLAccountData(elemID);
                        }
                        if (ui.item.id == 0) {
                            event.preventDefault();
                            return false;
                            //ClearGLAccountData(elemID);
                        }
                        else {
                            var DEPT_ID = $('#' + ddlDEPT_ID).val();
                            var itemid = $('#' + ddlITEM_NAME).val();
                            if (DEPT_ID == "") {
                                alert("Please Select Department.!");
                                return;
                            }
                            if (itemid == "") {
                                alert("Please Select To Be Item.!");
                                return;
                            }

                            SetItemData(elemID, ui.item);
                        }
                        // setDetInstrument(hdnIsInstrumentElem, txtInstrumentElem, btnInstrumentElem);
                        return false;
                    }


                    // lc: ''
                });

                $(elem).blur(function () {
                    var self = this;
                    elemID = $(elem).attr('id');
                    eCode = $(elem).val();
                    isComboGridOpen = $(self).combogrid('isOpened');
                    if (eCode == '') {
                        ClearItemData(elemID);
                    }
                    else {
                        var serviceURL = ItemListServiceLinkd + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
                        serviceURL += "&ispaging=1";

                        var prcNo = GetItemNo(eCode, serviceURL);

                        if (prcNo == null) {
                            ClearItemData(elemID);
                        }
                        else {
                            SetItemData(elemID, grp);
                        }

                    }
                });

            });

        }

        function GetItemNo(eCode, serviceURL) {
            var prcNo = null;
            var isError = false;
            var isComplete = false;
            //ajax call

            var newServiceURL = serviceURL + " &selectedId=" + eCode;
            var dummyVar = $.ajax({
                type: "GET",
                cache: false,
                async: false,
                dataType: "json",
                url: newServiceURL,

                success: function (prddata) {
                    //            if (accdata.menu[0].count > 0) {
                    //                menu = menudata.menu[0];
                    //            }
                    if (prddata.rows.length > 0) {
                        prcNo = prddata.rows[0];
                    }
                },
                complete: function () {
                    if (!isError) {
                        //return;
                        //alert (menu.name);
                    }
                    isComplete = true;
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    isError = true;
                    alert(textStatus);
                }
            });
            return prcNo;
        }

        function ClearItemData(txtItemID) {
            //$('#' + txtGLAccCodeID).val('');
            var detRow = $('#' + txtItemID).closest('tr.gridRow');
            $(detRow).find('input[id$="hdnSTOCK_TRANSFER_ITEM_ID"]').val('0');
            $(detRow).find('input[id$="txtSTOCK_TRANSFER_ITEM_NAME"]').val('');
            $(detRow).find('input[id$="hdnUOM_ID"]').val('0');
            $(detRow).find('input[id$="txtUOM_NAME"]').val('');
            $(detRow).find('input[id$="txtSTOCK_QTY"]').val('0');
            $(detRow).find('input[id$="txtTRANSFER_QTY"]').val('0');
        }
        function SetItemData(txtItemCodeID, data) {
            $('#' + txtItemCodeID).val(data.itemid);

            var detRow = $('#' + txtItemCodeID).closest('tr.gridRow');
            $(detRow).find('input[id$="STOCK_TRANSFER_ITEM_ID"]').val(data.itemid);
            $(detRow).find('input[id$="txtSTOCK_TRANSFER_ITEM_NAME"]').val(data.itemname);
            //$(detRow).find('input[id$="hdngroupId"]').val(data.itemgroupid);
            //$(detRow).find('input[id$="txtGroupName"]').val(data.itemgroupdesc);
            $(detRow).find('input[id$="hdnUOM_ID"]').val(data.uomid);
            $(detRow).find('input[id$="txtUOM_NAME"]').val(data.uomname);
            $(detRow).find('input[id$="txtSTOCK_QTY"]').val(data.currentstock);
            $(detRow).find('input[id$="txtTRANSFER_QTY"]').val(data.currentstock);


            //$("[id*=btngrdSum]").click();
            var currentqty = $('#' + txtTotQty).val();
            var totalqty = Number(currentqty) + Number(data.currentstock);
            $('#' + txtTotQty).val(totalqty);

        }


        //Set Batch List

        function bindBatchList(gridViewID) {
            var cgColumns = [
                               { 'columnName': 'batchNO', 'width': '200', 'align': 'left', 'highlight': 4, 'label': 'Batch No' }
                             //, { 'columnName': 'productiondate', 'width': '70', 'align': 'left', 'highlight': 4, 'label': 'Prod Date' }

            ];


            //var companyid = $('#' + hdnCompanyID).val();
            //var depthead = $('#' + hdnEmpCode).val();
            var DEPT_ID = $('#' + ddlDEPT_ID).val();
            var proddate = $('#' + txtProductionDate).val();
            // alert(DEPT_ID);
            //var serviceURL = GLAccountServiceLink + "?isterm=1&includeempty=1&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            var serviceURL = ProductionDateWiseBatch_List + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            //serviceURL += "&companyid=" + companyid;
            serviceURL += "&ispaging=1&isFinished=Y&deptid=" + DEPT_ID + "&proddate=" + proddate;
            //serviceURL += "&locationid=" + locationid;
            // serviceURL += "&empstatus=" + "A";
            //serviceURL += "&acctypefilter=" + Enums.GLAccountTypeFilter.AllAccount;
            // serviceURL += "&namecomptype=" + Enums.DataCompareType.Contains;

            var gridSelector = "#" + gridViewID;


            $(gridSelector).find('input[id$="txtBatchNo"]').each(function (index, elem) {
                ///list click
                var elemRow = $(elem).closest('tr.gridRow');

                var hdnItemIDElem = $(elemRow).find('input[id$="txtBatchNo"]');

                //var prevGLCode = '';

                $(elem).closest('tr').find('input[id$="btnBatchNo"]').click(function (e) {
                    elmID = $(elem).attr('id');
                    //$(elem).combogrid("show");
                    $(elem).combogrid("dropdownClick");
                });



                //var compNameElem = $('#' + txtCompanyName);

                //$('#' + btnCompanyID).click(function (e) {
                //    //elmID = $(elem).attr('id');
                //    //$(elem).combogrid("show");
                //    $(compNameElem).combogrid("dropdownClick");
                //});


                $(elem).combogrid({
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
                    url: serviceURL,
                    search: function (event, ui) {
                        var elemRowCur = $(elem).closest('tr.gridRow');

                        var newServiceURL = serviceURL //+ "&groupid=" + vgroupid
                        //var newServiceURL = serviceURL + "&companycode=" + companyCode + "&branchcode=" + branchCode + "&deptcode=" + deptCode
                        newServiceURL = JSUtility.AddTimeToQueryString(newServiceURL);
                        $(this).combogrid("option", "url", newServiceURL);


                    },

                    select: function (event, ui) {
                        //alert(ui.item.typename);
                        //$(".txtComboGrid").val(ui.item.code);
                        elemID = $(elem).attr('id');
                        //                    if (!validateGLAccount(elemID, ui.item)) {
                        //                        $(elem).val(prevGLCode);
                        //                        return false;
                        //                    }
                        if (!ui.item) {
                            event.preventDefault();
                            ClearBatchData(elemID);
                            return false;
                            //ClearGLAccountData(elemID);
                        }
                        if (ui.item.id == 0) {
                            event.preventDefault();
                            return false;
                            //ClearGLAccountData(elemID);
                        }
                        else {
                            SetBatchData(elemID, ui.item);
                        }
                        // setDetInstrument(hdnIsInstrumentElem, txtInstrumentElem, btnInstrumentElem);
                        return false;
                    }


                    // lc: ''
                });

                $(elem).blur(function () {
                    var self = this;
                    elemID = $(elem).attr('id');
                    eCode = $(elem).val();
                    isComboGridOpen = $(self).combogrid('isOpened');
                    if (eCode == '') {
                        ClearItemData(elemID);
                    }
                    else {
                        var serviceURL = ProductionDateWiseBatch_List + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
                        serviceURL += "&ispaging=1";

                        var prcNo = GetItemNo(eCode, serviceURL);

                        if (prcNo == null) {
                            ClearBatchData(elemID);
                        }
                        else {
                            SetBatchData(elemID, grp);
                        }

                    }
                });

            });

        }

        function GetItemNo(eCode, serviceURL) {
            var prcNo = null;
            var isError = false;
            var isComplete = false;
            //ajax call

            var newServiceURL = serviceURL + " &selectedId=" + eCode;
            var dummyVar = $.ajax({
                type: "GET",
                cache: false,
                async: false,
                dataType: "json",
                url: newServiceURL,

                success: function (prddata) {
                    //            if (accdata.menu[0].count > 0) {
                    //                menu = menudata.menu[0];
                    //            }
                    if (prddata.rows.length > 0) {
                        prcNo = prddata.rows[0];
                    }
                },
                complete: function () {
                    if (!isError) {
                        //return;
                        //alert (menu.name);
                    }
                    isComplete = true;
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    isError = true;
                    alert(textStatus);
                }
            });
            return prcNo;
        }

        function ClearBatchData(txtBatch) {
            //$('#' + txtGLAccCodeID).val('');
            var detRow = $('#' + txtBatch).closest('tr.gridRow');
            $(detRow).find('input[id$="hdnBatchNo"]').val('');
            $(detRow).find('input[id$="txtBatchNo"]').val('');

        }
        function SetBatchData(txtBatch, data) {
            $('#' + txtBatch).val(data.itemid);

            var detRow = $('#' + txtBatch).closest('tr.gridRow');

            $(detRow).find('input[id$="txtBatchNo"]').val(data.batchNO);
            $(detRow).find('input[id$="hdnBatchNo"]').val(data.batchNO);






        }


    </script>

    <style type="text/css">
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




        .hidden {
            /*visibility:hidden;*/
            display: none;
        }
    </style>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hdnDEPT_ID" runat="server" />
    <asp:HiddenField ID="hdnLoggedInUser" runat="server" />
    <asp:HiddenField ID="hdnSTOCK_TRANSFER_ID" runat="server" />
    <asp:Button ID="btngrdSum" runat="server" OnClick="btngrdSum_Click" />

    <div id="dvPageContent" style="width: 100%; height: auto;">
        <div id="dvContentHeader" class="dvContentHeader">
            <div id="dvHeader" class="dvHeader_Prod">
                <asp:Label ID="lblHeader" CssClass="lblHeader" runat="server" Text="IB Cutting Lead Receive"></asp:Label>
            </div>
            <!--Message Div -->
            <div id="dvMsg" runat="server" class="dvMessage" style="">
                <asp:Label ID="lblMessage" runat="server" Font-Size="Small" Width="100%"> </asp:Label>
            </div>
            <div id="dvHeaderControl" class="dvHeaderControl">
            </div>
        </div>

        <div id="dvContentMain" class="dvContentMain">
            <div id="dvControlsHead" style="height: auto; width: 100%;">
                <table>
                    <tr>


                        <td align="right" class="auto-style2">
                            <asp:Label ID="lblRejectionNo" runat="server" Text="Transfer NO :" Style="font-weight: 700"></asp:Label>
                        </td>
                        <td class="auto-style2">
                            <asp:TextBox ID="txtSTOCK_TRANSFER_NO" runat="server" Style="text-align: left; font-weight: 700;" CssClass="textBox" Enabled="False" ForeColor="Red"></asp:TextBox>
                        </td>
                        <td></td>
                        <td>

                            <asp:HiddenField ID="hdnUOM_ID" runat="server" />

                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblDEPT_ID" runat="server" Text="Department : "></asp:Label>
                        </td>
                        <td style="text-align: left">

                            <asp:DropDownList ID="ddlDEPT_ID" runat="server" CssClass="dropDownList required" Width="190px" OnSelectedIndexChanged="ddlDEPT_ID_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                        </td>
                        <td style="text-align: right">
                            <asp:Label ID="lblStatus" runat="server" Text="Status :"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAUTHO_STATUS" runat="server"></asp:TextBox>
                        </td>
                        <%-- <td>
                            <asp:HiddenField ID="hdnFC_ID" runat="server" />
                        </td>
                        <td style="text-align: right">
                            <asp:Label ID="Label1" runat="server" Text="FG Type :"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlFGFC_TYPE" runat="server" CssClass="dropDownList required" Width="150px" ViewStateMode="Enabled">
                                <asp:ListItem Selected="True" Value="1">Primary</asp:ListItem>
                                <asp:ListItem Value="2">Tender</asp:ListItem>
                                <asp:ListItem Value="3">Others</asp:ListItem>
                            </asp:DropDownList>

                        </td>--%>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblRejectionItem" runat="server" Text="To Be Item : "></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <%--   <asp:TextBox ID="txtRejectionItem" runat="server" CssClass="textBox" Style="text-align: left;" ></asp:TextBox>
                                  
                                  <asp:HiddenField ID="hdnRejectITEMID" runat="server" Value="0" />--%>
                            <asp:DropDownList ID="ddlITEM_NAME" runat="server" CssClass="dropDownList required" OnSelectedIndexChanged="ddlITEM_NAME_SelectedIndexChanged" AutoPostBack="true" Width="190px"></asp:DropDownList>
                        </td>
                        <td style="text-align: right">
                            <asp:Label ID="lbl1" runat="server" Text="UOM :"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtUOM_NAME" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="auto-style2">
                            <asp:Label ID="lblTRANSFER_REASON" runat="server" Text="Reason :"></asp:Label>
                        </td>
                        <td class="auto-style2" colspan="4">
                            <asp:TextBox ID="txtTRANSFER_REASON" runat="server" CssClass="textBox" Style="text-align: left;" Width="500px"></asp:TextBox>
                        </td>

                    </tr>
                    <%-- <tr>
                        <td>&nbsp;</td>
                        <td>
                            <asp:CheckBox ID="chkAUTH_STATUS" runat="server" Text="  Authorize   " Checked="True" TextAlign="right"></asp:CheckBox>
                        </td>
                        <td>
                            <asp:HiddenField ID="hdnCompanyID" runat="server" Value="0" />
                        </td>

                        <td>
                            <asp:HiddenField ID="hdnAUTH_STATUS" runat="server" />
                        </td>
                        <td>
                                <asp:Button ID="btnUpload" CssClass="buttonSearch" runat="server" OnClick="btnUpload_Click" OnClientClick="showOverlay();" Text="Show Data" Style="padding-left: 22px;" />
                        </td>
                    </tr>--%>
                </table>
            </div>
            <div id="dvControls" style="width: 100%; height: 400px;">
                <div id="Div3" runat="server" class="" style="width: 100%; text-align: left;">
                    <span style="font-weight: bold; font-size: 15px; color: #ff3b00;">Transferable Item Details: </span>
                </div>

                <div id="Div1" runat="server" class="" style="width: auto; height: auto; text-align: left">
                    <div id="dvGridHeaderClosing" style="width: 660px; height: 25px; font-size: smaller;" class="subHeader_Prod">
                        <table style="height: 80%; color: #FFFFFF; font-weight: bold; text-align: left;"
                            class="defFont" cellspacing="1" cellpadding="1">
                            <tr class="headerRow_Prod">
                                <td width="65px" class="headerColCenter_prod">SL#
                                </td>
                                <%--  <td width="135px" class="headerColCenter_prod">
                                    Batch No
                                </td>
                                <td width="15px" class="headerColLeft">

                                </td>--%>
                                <td width="153px" class="headerColCenter_prod">Item
                                </td>
                                <td width="65px" class="headerColCenter_prod">UOM
                                </td>
                                <td width="90px" class="headerColCenter_prod">Cutting Date
                                </td>
                              <%--  <td width="80px" class="headerColCenter_prod">Stock Qty
                                </td>--%>
                                <td width="78px" class="headerColCenter_prod">Qty
                                </td>
                                <td width="150px" class="headerColCenter_prod">Remarks
                                </td>
                                <td width="16px" class="headerColCenter">Delete
                                </td>

                            </tr>
                        </table>
                    </div>
                    <div id="dvGridClosing" style="width: 900px; height: 250px; overflow: auto;" class="dvGrid">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:GridView ID="grdTransferList" runat="server" AutoGenerateColumns="False" ShowHeader="False"
                                    CellPadding="1" CellSpacing="1" ForeColor="#333333" GridLines="Both" DataKeyNames="ITEM_ID"
                                    EnableModelValidation="True" ClientIDMode="AutoID" OnRowCommand="grdTransferList_RowCommand" OnRowCreated="grdTransferList_RowCreated" OnRowDataBound="grdTransferList_RowDataBound" OnRowDeleting="grdTransferList_RowDeleting">
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL#">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSLNO" runat="server" Text='<%# Bind("SLNO") %>' Style="text-align: center;"
                                                    Width="60px">
                                                </asp:Label>
                                                <asp:HiddenField ID="hdnRecordStateInt" runat="server" Value='<%# Bind("_RecordStateInt") %>' />
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText=" Item Type" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div>
                                                    <table border="0" cellpadding="1" cellspacing="1">
                                                        <tbody>
                                                            <tr>
                                                                <td></td>
                                                                <td>
                                                                    <asp:HiddenField ID="hdnSTOCK_TRANSFER_DTL_ID" runat="server" Value='<%# Bind("CUTTING_DTL_ID") %>' />
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtSTOCK_TRANSFER_ITEM_NAME" runat="server" CssClass="textBox textAutoSelect" Width="135px" Text='<%# Bind("ITEM_NAME") %>'></asp:TextBox>
                                                                    <asp:HiddenField ID="hdnSTOCK_TRANSFER_ITEM_ID" runat="server" Value='<%# Bind("ITEM_ID") %>' />
                                                                </td>
                                                                <td>
                                                                    <input id="btnITEM_NAME" type="button" value="" runat="server" class="buttonDropdown" tabindex="-1" />
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtUOM_NAME" runat="server"  CssClass="textBox textAutoSelect" Width="65px" Text='<%# Bind("UOM_NAME") %>'></asp:TextBox>
                                                                    <asp:HiddenField ID="hdnUOM_ID" runat="server" Value='<%# Bind("UOM_ID") %>' />
                                                                </td>
                                                                <td style="white-space:nowrap">
                                                                    <asp:TextBox ID="txtCuttingDate" runat="server" CssClass="textDate dateParse textBox textAutoSelect" Width="70px" Text='<%# Bind("CUTTING_DATE","{0:dd-MMM-yyyy}") %>'></asp:TextBox>
                                                                   
                                                                </td>

                                                                <%--<td>
                                                                    <asp:TextBox ID="txtSTOCK_QTY" runat="server" CssClass="textBox textAutoSelect" Width="75px" align="right" Style="text-align: right;" Text='<%# Bind("STOCK_QTY") %>'></asp:TextBox>
                                                                </td>--%>
                                                                <td style="text-align: right;">
                                                                    <asp:TextBox ID="txtTRANSFER_QTY" runat="server" CssClass="txtQty textBox textAutoSelect" Width="75px" align="right" Style="text-align: right;" Text='<%# Bind("ITEM_QTY") %>' onchange="calcIssueStock(this)"></asp:TextBox>
                                                                </td>

                                                                <td>
                                                                    <asp:TextBox ID="txtREMARKS" runat="server" CssClass="textBox textAutoSelect" Width="150px" Text='<%# Bind("REMARKS") %>' Style=""></asp:TextBox>
                                                                </td>

                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                                <div style="overflow: visible;">
                                                </div>
                                            </ItemTemplate>
                                            <ItemStyle Width="10" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete" ShowHeader="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnDeleteRow" CssClass="buttonDeleteIcon" Height="16px" Width="18px"
                                                    CommandName="delete" runat="server">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <div style="width: 10px;">
                                                    <div>
                                                        <div style="background-position: right center; height: 25px; cursor: pointer; background-image: url('../image/more.png'); background-repeat: no-repeat; text-align: left; vertical-align: middle;"
                                                            onclick="togglePannelStatus(this)"
                                                            title="More..">
                                                            ...
                                                        </div>
                                                        <div style="display: none;">
                                                            <div class="gridPanel" style="float: right; width: 0px; height: 0px;">
                                                                <div style="position: relative; height: 100%; width: 100%;">
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Font-Size="Smaller" />
                                    <EditRowStyle BackColor="#999999" />
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                </asp:GridView>
                                <input id="Hidden1" type="hidden" runat="server" value="[]" />
                                <input id="Hidden2" type="hidden" runat="server" value="[]" />
                            </ContentTemplate>
                            <%--  <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnNewRow" EventName="Click" />
                            </Triggers>--%>
                        </asp:UpdatePanel>


                    </div>
                    <div id="divGridControls2" style="width: 100%; height: 25px; border-top: solid 1px #C0C0C0;">
                        <table style="width: auto; height: 100%; text-align: center;" cellspacing="1" cellpadding="1"
                            border="0">
                            <tr>
                                <td style="width: 2px"></td>
                                <td style="width: 90px" align="left">
                                    <asp:Button ID="btnNewRow2" runat="server" CssClass="buttonNewRow" Text="" OnClientClick="ShowProgress()" OnClick="btnNewRow2_Click" />
                                </td>
                                <td style="width: 20px;">
                                    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                                        DisplayAfter="300">
                                        <ProgressTemplate>
                                            <asp:Image ID="imgProgress2" runat="server" ImageUrl="~/image/loader.gif" />
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </td>
                                <td style="width: 100px"></td>
                                <td style="width: 50px"></td>
                                <td align="right">&nbsp;
                                </td>
                                <td align="right">
                                    <asp:Label ID="lbltotalSalesAmount" runat="server" Text="Total Qnty:" Font-Bold="True" Visible="true"></asp:Label>
                                </td>
                                <td align="right">
                                    <asp:TextBox ID="txtTotQty" runat="server" CssClass="textBox" Style="text-align: right;"
                                        Width="100" TabIndex="-1" Font-Bold="True" Visible="true"></asp:TextBox>
                                </td>
                                <td align="right">&nbsp;
                                </td>

                            </tr>
                        </table>
                    </div>
                </div>



            </div>
            <div id="dvContentFooter" class="dvContentFooter">
                <table>
                    <tr>
                        <td></td>
                        <td>
                            <asp:Button ID="btnAddNew" runat="server" Text="New" CssClass="buttonNew" OnClick="btnAddNew_Click" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonCancel checkIsDirty" />
                        </td>
                        <td>
                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonSave checkRequired" AccessKey="s" OnClick="btnSave_Click" OnClientClick="if ( ! UserDeleteConfirmation()) return false;" />
                            <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="buttonEdit" OnClick="btnEdit_Click" />
                        </td>


                        <td>
                            <asp:Button ID="btnAuthorize" runat="server" Text="Authorize" CssClass="buttoncommon" OnClientClick="if ( ! UserAuthorizeConfirmation()) return false;" OnClick="btnAuthorize_Click" />
                        </td>
                        <td>
                            <%--<uc:PrintButton runat="server" ID="ucPrintButton" DefaultPrintAction="Preview" AutoPrint="False" />--%>
                            <asp:Button ID="btnRefresh" runat="server" Text="Refresh" CssClass="buttonRefresh checkIsDirty" OnClick="btnRefresh_Click" />

                        </td>
                        <td>
                            <input id="btnClose" type="button" runat="server" class="buttonClose" value="Close" onclick="if (ContentForm) { ContentForm.CloseForm(); }" />
                        </td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td>
                            <asp:Button ID="btnPopupTrigger" runat="server" Text="Button" CssClass="buttonHidden" />
                            <asp:HiddenField ID="hdnPopupTriggerID" runat="server" Value="" />
                            <asp:HiddenField ID="hdnPopupCommand" runat="server" Value="" />
                        </td>
                    </tr>
                </table>
            </div>

        </div>


        <div id="overlay" class="overlay">
            <div style="margin: auto; width: 200px; height: 400px; background-color: black; border: solid 1px black; text-align: center; vertical-align: middle;">
                <span style="color: white; font-size: medium;">Please Wait...</span>
                <br />
                <img alt="" src="../../image/progress.gif" />
            </div>
        </div>

        <div id="overlayReport" class="overlay" style="opacity: 0.8;">
            <div style="margin: auto; width: 450px; height: 80px; position: relative; background-color: blue; text-align: center; vertical-align: middle; cursor: auto; z-index: 9999999;">
                <table width="100%">
                    <tr>
                        <td>
                            <span style="color: white; font-size: medium;">Click Open Report to view Report.</span>
                        </td>
                    </tr>
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
