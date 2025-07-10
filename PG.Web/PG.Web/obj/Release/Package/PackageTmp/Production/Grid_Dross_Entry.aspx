<%@ Page Title="" Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="Grid_Dross_Entry.aspx.cs" Inherits="PG.Web.Production.Grid_Dross_Entry" %>

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
        var ItemListServiceLink = '<%=this.ItemListServiceLink%>';
        var txtTotQty = '<%=this.txtTotQty.ClientID%>';

        var gridUpdatePanelIDDet = '<%=UpdatePanel1.ClientID%>';
        var gridViewIDDet = '<%=grdTransferList.ClientID%>';
        var updateProgressID = '<%=UpdateProgress2.ClientID%>';

        var ddlDEPT_ID = '<%=ddlDEPT_ID.ClientID%>';
        var ddlStlmId = '<%=ddlStlmId.ClientID%>';

        var ProductionNoListServiceLink = '<%=this.ProductionNoListServiceLink%>';
        var txtProdNo = '<%=txtProdNo.ClientID%>';
        var btnProdNo = '<%=btnProdNo.ClientID%>';
        var hdnProdId = '<%=hdnProdId.ClientID%>';
      

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

        function bindUsedItemList(gridViewIDD) {
            var cgColumns = [{ 'columnName': 'itemname', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                             , { 'columnName': 'uomname', 'width': '50', 'align': 'left', 'highlight': 4, 'label': 'Uom' }
                             , { 'columnName': 'itemcode', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Code' }
                             //, { 'columnName': 'closing_qty', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Cls Qty' }
                             , { 'columnName': 'itemgroupdesc', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Group Name' }
                             , { 'columnName': 'class_name', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Class Name' }
                             //, { 'columnName': 'item_type', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Item Type' }
            ];
            var DEPT_ID = $('#' + ddlDEPT_ID).val();
            var serviceURL = ItemListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            serviceURL += "&ispaging=1&isFinished=N&for_production=Y";// &deptid=+ DEPT_ID;
            var gridSelector = "#" + gridViewIDD;
            $(gridSelector).find('input[id$="txtUSED_ITEM_NAME"]').each(function (index, elem) {
                ///list click
                var elemRow = $(elem).closest('tr.gridRow');
                var hdnItemIDElem = $(elemRow).find('input[id$="txtUSED_ITEM_NAME"]');
                //var prevGLCode = '';
                $(elem).closest('tr').find('input[id$="btnUSED_ITEM"]').click(function (e) {
                    elmID = $(elem).attr('id');
                    //$(elem).combogrid("show");
                    $(elem).combogrid("dropdownClick");
                });

                var DeptID = $('[id*=ddlDEPT_ID] option:selected').val();
                var stlmid = $('[id*=ddlStlmId] option:selected').val();

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
                    width: 600,
                    url: serviceURL,
                    search: function (event, ui) {
                        var elemRowCur = $(elem).closest('tr.gridRow');
                        var newServiceURL = serviceURL + "&groupid=5" + "&deptid=" + DeptID + "&stlmid=" + stlmid;//+ "&groupid=" + vgroupid
                        newServiceURL = JSUtility.AddTimeToQueryString(newServiceURL);
                        $(this).combogrid("option", "url", newServiceURL);
                    },

                    select: function (event, ui) {
                        elemID = $(elem).attr('id');
                        if (!ui.item) {
                            event.preventDefault();
                            ClearUsedItemData(elemID);
                            return false;
                        }
                        if (ui.item.id == 0) {
                            event.preventDefault();
                            return false;
                        }
                        else {
                            SetUsedItemData(elemID, ui.item);
                        }
                        return false;
                    }
                });

                $(elem).blur(function () {
                    var self = this;
                    elemID = $(elem).attr('id');
                    eCode = $(elem).val();
                    isComboGridOpen = $(self).combogrid('isOpened');
                    if (eCode == '') {
                        ClearUsedItemData(elemID);
                    }
                    else {
                        var serviceURL = ItemListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
                        serviceURL += "&ispaging=1&isFinished=N&for_production=Y" + "&groupid=5" + "&deptid=" + DeptID + "&stlmid=" + stlmid;
                        var prcNo = GetItemNo(eCode, serviceURL);
                        if (prcNo == null) {
                            ClearUsedItemData(elemID);
                        }
                        else {
                            SetUsedItemData(elemID, grp);
                        }
                    }
                });

            });

        }

        function ClearUsedItemData(txtCLOSINGITEM_NAME) {
            var detRow = $('#' + txtCLOSINGITEM_NAME).closest('tr.gridRow');
            $(detRow).find('input[id$="hdnUSED_ITEM_ID"]').val('0');
            $(detRow).find('input[id$="txtUSED_ITEM_NAME"]').val('');
        }
        function SetUsedItemData(txtCLOSINGITEM_NAME, data) {
            var detRow = $('#' + txtCLOSINGITEM_NAME).closest('tr.gridRow');
            $(detRow).find('input[id$="hdnUSED_ITEM_ID"]').val(data.itemid);
            $(detRow).find('input[id$="txtUSED_ITEM_NAME"]').val(data.itemname);
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
                        bindDrossItemList(gridViewIDDet);
                        bindUsedItemList(gridViewIDDet);
                        // bindBatchList(gridViewIDDet);
                    }

                }
                //$('#' + dvGridDetailsPopup).parent().appendTo(jQuery("form:first"));
                //gridTaskAfter();

            });


            // alert('OK 1');

        

            //if ($('#' + txtCustomerName).is(':visible')) {

            //    bindCustomerList();
            //}
            ////alert('OK 1');
            //bindItemGroupList(gridViewIDDet);
            //alert('OK 2');

            bindDrossItemList(gridViewIDDet);
            bindUsedItemList(gridViewIDDet);
            // bindBatchList(gridViewIDDet);

            //if ($('#' + txtProdNo).is(':visible')) {

            bindProductionNoList();
            //}
        });

        function bindDrossItemList(gridViewIDD) {
            var cgColumns = [{ 'columnName': 'itemname', 'width': '160', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                             , { 'columnName': 'uomname', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Uom Name' }
                             , { 'columnName': 'itemcode', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Code' }
                             //, { 'columnName': 'closing_qty', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Cls Qty' }
                             //, { 'columnName': 'itemgroupdesc', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Group Name' }
                             , { 'columnName': 'class_name', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Class Name' }
                             //, { 'columnName': 'item_type', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Item Type' }
            ];

            var serviceURL = ItemListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            serviceURL += "&ispaging=1&isFinished=Y&for_production=Y&groupid=47";
            var gridSelector = "#" + gridViewIDD;
            $(gridSelector).find('input[id$="txtITEM_NAME"]').each(function (index, elem) {
                ///list click
                var elemRow = $(elem).closest('tr.gridRow');
                var hdnItemIDElem = $(elemRow).find('input[id$="txtITEM_NAME"]');
                //var prevGLCode = '';
                $(elem).closest('tr').find('input[id$="btnITEM_NAME"]').click(function (e) {
                    elmID = $(elem).attr('id');
                    //$(elem).combogrid("show");
                    $(elem).combogrid("dropdownClick");
                });

                var DeptID = $('[id*=ddlDEPT_ID] option:selected').val();
                var stlmid = $('[id*=ddlStlmId] option:selected').val();
                //var stlmid = $('#' + ddlStlmId).val();

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
                    width: 700,
                    url: serviceURL,
                    search: function (event, ui) {
                        var elemRowCur = $(elem).closest('tr.gridRow');
                        var newServiceURL = serviceURL + "&deptid=" + DeptID + "&stlmid=" + stlmid;
                        newServiceURL = JSUtility.AddTimeToQueryString(newServiceURL);
                        $(this).combogrid("option", "url", newServiceURL);
                    },

                    select: function (event, ui) {
                        elemID = $(elem).attr('id');
                        if (!ui.item) {
                            event.preventDefault();
                            ClearDrossItemData(elemID);
                            return false;
                        }
                        if (ui.item.id == 0) {
                            event.preventDefault();
                            return false;
                        }
                        else {
                            SetDrossItemData(elemID, ui.item);
                        }
                        return false;
                    }
                });

                $(elem).blur(function () {
                    var self = this;
                    elemID = $(elem).attr('id');
                    eCode = $(elem).val();
                    isComboGridOpen = $(self).combogrid('isOpened');
                    if (eCode == '') {
                        ClearDrossItemData(elemID);
                    }
                    else {
                        var serviceURL = ItemListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
                        serviceURL += "&ispaging=1&isFinished=Y&for_production=Y&groupid=47" + "&deptid=" + DeptID + "&stlmid=" + stlmid;
                        var prcNo = GetItemNo(eCode, serviceURL);
                        if (prcNo == null) {
                            ClearDrossItemData(elemID);
                        }
                        else {
                            SetDrossItemData(elemID, grp);
                        }
                    }
                });

            });

        }

        function ClearDrossItemData(txtCLOSINGITEM_NAME) {
            //$('#' + txtGLAccCodeID).val('');
            var detRow = $('#' + txtCLOSINGITEM_NAME).closest('tr.gridRow');
                $(detRow).find('input[id$="hdnITEM_ID"]').val('0');
                $(detRow).find('input[id$="txtITEM_NAME"]').val('');
                $(detRow).find('input[id$="hdnUOM_ID"]').val('0');
                $(detRow).find('input[id$="txtUOM_NAME"]').val('');
                $(detRow).find('input[id$="txtITEM_QTY"]').val('0');

        }

        function SetDrossItemData(txtCLOSINGITEM_NAME, data) {
            var detRow = $('#' + txtCLOSINGITEM_NAME).closest('tr.gridRow');
                $(detRow).find('input[id$="ITEM_ID"]').val(data.itemid);
                $(detRow).find('input[id$="txtITEM_NAME"]').val(data.itemname);
                $(detRow).find('input[id$="hdnUOM_ID"]').val(data.uomid);
                $(detRow).find('input[id$="txtUOM_NAME"]').val(data.uomname);



        }

        function bindProductionNoList() {
            var cgColumns = [{ 'columnName': 'prodno', 'width': '150', 'align': 'left', 'highlight': 2, 'label': 'Prod No' }
                             , { 'columnName': 'proddate', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Date' }
                             , { 'columnName': 'shift', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Shift' }
                             //, { 'columnName': 'supphone', 'width': '80', 'align': 'left', 'highlight': 0, 'label': 'phone' }



            ];


            var serviceURL = ProductionNoListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            serviceURL += "&ispaging=1";

            var supplierIDElem = $('#' + txtProdNo);
            

            $('#' + btnProdNo).click(function (e) {
                $(supplierIDElem).combogrid("dropdownClick");
            });

            var DeptID = $('[id*=ddlDEPT_ID] option:selected').val();

            $(supplierIDElem).combogrid({
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
                width: 450,
                url: serviceURL,
                search: function (event, ui) {
               
                    var newServiceURL = serviceURL + "&deptid=" + DeptID;
                    $(this).combogrid("option", "url", newServiceURL);


                },
                select: function (event, ui) {
                    if (!ui.item) {
                        event.preventDefault();

                        // $('#' + hdnDealerID).val('0');
                        $('#' + txtProdNo).val('');
                        return false;
                        //ClearGLAccountData(elemID);
                    }


                    if (ui.item.supid == '') {
                        event.preventDefault();
                        return false;
                        //ClearGLAccountData(elemID);
                    }
                    else {
                        // $('#' + hdnDealerID).val(ui.item.dealerid);
                        $('#' + hdnProdId).val(ui.item.prodid);
                        $('#' + txtProdNo).val(ui.item.prodno);


                    }
                    return false;
                },

                lc: ''
            });


            $(supplierIDElem).blur(function () {
                var self = this;

                var customerID = $(supplierIDElem).val();
                if (customerID == '') {
                    $('#' + hdnProdId).val('0');
                    $('#' + txtProdNo).val('');

                }
            });
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
    <asp:HiddenField ID="hdnDROSS_MST_ID" runat="server" />
    <asp:Button ID="btngrdSum" runat="server" OnClick="btngrdSum_Click" />

    <div id="dvPageContent" style="width: 100%; height: auto;">
        <div id="dvContentHeader" class="dvContentHeader">
            <div id="dvHeader" class="dvHeader_Prod">
                <asp:Label ID="lblHeader" CssClass="lblHeader" runat="server" Text="Dross Entry"></asp:Label>
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
                            <asp:Label ID="lblRejectionNo" runat="server" Text="Dross NO :" Style="font-weight: 700"></asp:Label>
                        </td>
                        <td class="auto-style2">
                            <asp:TextBox ID="txtDROSS_NO" runat="server" Style="text-align: left; font-weight: 700;" CssClass="textBox" Enabled="False" ForeColor="Red"></asp:TextBox>
                        </td >
                        <td align="right">  <asp:Label ID="Label1" runat="server" Text="Dross Date : "></asp:Label> </td>
                        <td>
                                   <asp:TextBox ID="txtRECEIVE_DATE" runat="server" CssClass="textBox textDate dateParse" Style="text-align: left;" Width="100px"></asp:TextBox>
                            

                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblDEPT_ID" runat="server" Text="Department : "></asp:Label>
                        </td>
                        <td style="text-align: left">

                            <asp:DropDownList ID="ddlDEPT_ID" runat="server" CssClass="dropDownList required" Width="190px" OnSelectedIndexChanged="ddlDEPT_ID_SelectedIndexChanged" AutoPostBack="true" Enabled="false"></asp:DropDownList>
                        </td>
                     <%--   <td style="text-align: right">
                            <asp:Label ID="lblStatus" runat="server" Text="Status :"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAUTHO_STATUS" Width="100px" runat="server"></asp:TextBox>
                        </td>--%>
                          <td style="text-align: right">
                            <asp:Label ID="lblstlm" runat="server" Text="Storage Location :"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlStlmId" runat="server" CssClass="dropDownList" AutoPostBack="true"></asp:DropDownList>
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
                        <td align="right" class="auto-style2">
                            <asp:Label ID="lblTRANSFER_REASON" runat="server" Text="Reason :"></asp:Label>
                        </td>
                        <td class="auto-style2" colspan="">
                            <asp:TextBox ID="txtTRANSFER_REASON" runat="server" CssClass="textBox" Style="text-align: left;" Width="200px"></asp:TextBox>
                        </td>

                        <td align="right">
                            <asp:Label ID="lblProdNo" runat="server" CssClass="label" Text="Production No :" ></asp:Label>

                        </td>
                       <td>
                            <table>
                                <tr>
                                    <td> <asp:TextBox ID="txtProdNo" runat="server" CssClass="textBox" Width="200px"></asp:TextBox></td>
                                    <td> <input id="btnProdNo" type="button" value="" runat="server" class="buttonDropdown" tabindex="-1" /></td>
                                </tr>
                            </table>
                           
                           
                        </td>
                       <td><asp:HiddenField ID="hdnProdId" runat="server" Value="0" /></td>

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
            <div id="dvControls" style="width: 100%; height: 400px;" >
                <div id="Div3" runat="server" class="" style="width: 100%; text-align: left;">
                    <span style="font-weight: bold; font-size: 15px; color: #ff3b00;">Dross Item Details: </span>
                </div>

                <div id="Div1" runat="server" class="" style="width: auto; height: auto; text-align: left">
                    <div id="dvGridHeaderClosing" style="width: 900px; height: 25px; font-size: smaller;" class="subHeader_Prod">
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
                                <%--<td width="90px" class="headerColCenter_prod"> Date
                                </td>--%>
                              <%--  <td width="80px" class="headerColCenter_prod">Stock Qty
                                </td>--%>
                                <td width="78px" class="headerColCenter_prod">Qty
                                </td>
                                <td width="150px" class="headerColCenter_prod">Used RM
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
                                                                    <asp:HiddenField ID="hdnDROSS_DTL_ID" runat="server" Value='<%# Bind("DROSS_DTL_ID") %>' />
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtITEM_NAME" runat="server" CssClass="textBox textAutoSelect" Width="135px" Text='<%# Bind("ITEM_NAME") %>'></asp:TextBox>
                                                                    <asp:HiddenField ID="hdnITEM_ID" runat="server" Value='<%# Bind("ITEM_ID") %>' />
                                                                </td>
                                                                <td>
                                                                    <input id="btnITEM_NAME" type="button" value="" runat="server" class="buttonDropdown" tabindex="-1" />
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtUOM_NAME" runat="server"  CssClass="textBox textAutoSelect" Width="65px" Text='<%# Bind("UOM_NAME") %>'></asp:TextBox>
                                                                    <asp:HiddenField ID="hdnUOM_ID" runat="server" Value='<%# Bind("UOM_ID") %>' />
                                                                </td>
                                                     
                                                                <td style="text-align: right;">
                                                                    <asp:TextBox ID="txtITEM_QTY" runat="server" CssClass="txtQty textBox textAutoSelect" Width="75px" align="right" Style="text-align: right;" Text='<%# Bind("ITEM_QTY") %>' onchange="calcIssueStock(this)"></asp:TextBox>
                                                                </td>

                                                                   <td>
                                                                                <asp:TextBox ID="txtUSED_ITEM_NAME" runat="server" CssClass="textBox textAutoSelect" Width="130px" Text='<%# Bind("USED_ITEM_NAME") %>'></asp:TextBox>
                                                                                <asp:HiddenField ID="hdnUSED_ITEM_ID" runat="server" Value='<%# Bind("USED_ITEM_ID") %>' />
                                                                            </td>
                                                                            <td>
                                                                                <input id="btnUSED_ITEM" type="button" value="" runat="server" class="buttonDropdown" tabindex="-1" />
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
                              <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnNewRow2" EventName="Click" />
                            </Triggers>
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
