<%@ Page Title="" Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="ProductionCrushingPPEntryWithBOM.aspx.cs" Inherits="PG.Web.Production.ProductionCrushingPPEntryWithBOM" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="../javascript/jquery.ui.combogrid.js" type="text/javascript"></script>
    <script src="../javascript/jquery.attributeobserver.js" type="text/javascript"></script>
    <link href="../css/jquery.ui.combogrid.css" rel="stylesheet" type="text/css" />

 <%--     <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js" type="text/javascript"></script> 
     <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css"/> 
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap-theme.min.css"/>  --%>

    <script language="javascript" type="text/javascript">
        var isPageResize = true;
        ContentForm.CalendarImageURL = "../image/calendar.png";

        var ItemListServiceLinkd = '<%=this.Get_LinkBatch_WiseItem_List%>';
        var ProductionDateWiseBatch_List = '<%=this.ProductionDateWiseBatch_List%>';
        var BOMNameListServiceLink = '<%=this.BOMItemNameListServiceLink%>';
        var gridUpdatePanelIDDet = '<%=UpdatePanel1.ClientID%>';
        var gridViewIDDet = '<%=GridView1.ClientID%>';
        var updateProgressID = '<%=UpdateProgress2.ClientID%>';

        var ddlDEPT_ID = '<%=ddlDEPT_ID.ClientID%>';
        var hdnFGItemId = '<%=hdnFGItemId.ClientID%>';

        var hdnFinishItemIdForRm = '<%=hdnFinishItemIdForRm.ClientID%>';
        var hdnFinishItemNameForRm = '<%=hdnFinishItemNameForRm.ClientID%>';
        var hdnBOMIDForFn = '<%=hdnBOMIDForFn.ClientID%>';
        var hdnFinishedQty = '<%=hdnFinishedQty.ClientID%>';
        var hndMACHINE_ID_CLOSING = '<%=hndMACHINE_ID_CLOSING.ClientID%>';

        var SupporvisorListServiceLink = '<%= this.SupporvisorListServiceLink%>';
        var txtSUPERVISOR_NAME = '<%= txtSUPERVISOR_NAME.ClientID%>';
        var btnSUPERVISOR_ID = '<%=btnSUPERVISOR_ID.ClientID%>';
        var hdnSUPERVISOR_ID = '<%=hdnSUPERVISOR_ID.ClientID%>';
    
        
     <%--   var txtProductionDate = '<%=txtProductionDate.ClientID%>';
        var ddlREJECT_ITEM_TYPE = '<%=ddlREJECT_ITEM_TYPE.ClientID%>';--%>

    <%--    var txtTotQty = '<%=txtTotQty.ClientID%>';
        var txtTotWt = '<%=txtTotWt.ClientID%>';--%>
        $(function () {
            $("#tabs").tabs();
        });

        $(document).on('keyup', '.txtWt', function () {
            sumGrandWt();
        });

        $(document).on('keyup,onblur', '.txtQty', function () {
            sumGrandQty();
            sumGrandWt();
        });

        $(document).on('blur', '.txtQty', function () {
            sumGrandQty();
            sumGrandWt();
        });
        var addQty = 0;
        var addAmt = 0;
        var addWt = 0;


        function TestOnTextChange(texbx) {
            var detRow = $(texbx).closest('tr.gridRow');
            //var pcqty = $(detRow).find('input[id$="hdnPANEL_PC"]').val();
            var panelqty = $(detRow).find('input[id$="txtCrushingItemQty"]').val();
            var tpcqty = panelqty;
            $(detRow).find('input[id$="txtCrushingItemQty"]').val(tpcqty);
            //var standardweight = $(detRow).find('input[id$="hdnITEM_STANDARD_WEIGHT_KG"]').val();
            //var tweight = tpcqty * standardweight;
            //$(detRow).find('input[id$="txtITEM_WEIGHT"]').val(tweight);
            $('#' + hdnFinishedQty).val(tpcqty);

            var itemid = $(detRow).find('input[id$="hdnCrusingItemID"]').val();
            $('#' + hdnFinishItemIdForRm).val(itemid);

            var itemname = $(detRow).find('input[id$="txtCrushingItemName"]').val();
            $('#' + hdnFinishItemNameForRm).val(itemname);

            var bomid = $(detRow).find('input[id$="hdnITEM_BOM_ID"]').val();
            $('#' + hdnBOMIDForFn).val(bomid);

            //var useditemname = $(detRow).find('input[id$="txtUSED_ITEM_NAME"]').val();
            //$("[id*=btnAddAutoUsedItem]").click();
            //if (useditemname != "")
            //{
            //    $("[id*=btnAddAutoUsedItem]").click();
            //    $("[id*=btnRefreshClosingGrid]").click();
            //}
            //if (MachineID == "0") {
            //    alert('Please Select Machine ');
            //    $(detRow).find('input[id$="txtItem_qty"]').val('0');
            //    //panelqty.value = "0";
            //    return;
            //}
            //else {
                $("[id*=btnAddAutoUsedItem]").click();
            //}
        }

        function UserDeleteConfirmation() {
            return confirm("Are you sure you want to Save ?");
        }
        function UserSaveConfirmation() {
            return confirm("Are you sure you want to Save?");
        }

        function UserAuthorizeConfirmation() {
            return confirm("Are you sure you want to Authorized?");
        }

        function isNumberKey(evt, obj) {

            var charCode = (evt.which) ? evt.which : event.keyCode
            var value = obj.value;
            var dotcontains = value.indexOf(".") != -1;
            if (dotcontains)
                if (charCode == 46) return false;
            if (charCode == 46) return true;
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }

        //function calcWeight(texbx) {
        //    var detRow = $(texbx).closest('tr.gridRow');
        //    var pcqty = $(detRow).find('input[id$="hdnPANEL_PC"]').val();
        //    var standardweight = $(detRow).find('input[id$="txtITEM_STANDARD_WEIGHT_KG"]').val();
        //    var qty = $(detRow).find('input[id$="txtCrushingItemQty"]').val();
        //    var productionqty = $(detRow).find('input[id$="txtPRODUCTION_QTY"]').val();
        //    var tgoodqty = Number(productionqty) - Number(qty);
        //    $(detRow).find('input[id$="txtGOOD_QTY"]').val(tgoodqty);
        //    var tweight = qty * standardweight;
        //    $(detRow).find('input[id$="txtREJECTION_WEIGHT"]').val(tweight);
        //}
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

       


        //function GetTotalSumAddedQty() {
        //    debugger;
        //    var totAdd = 0;
        //    $(document).find('.txtQty').each(function (index, elem) {
        //        addQty = parseFloat(JSUtility.GetNumber($(elem).val()));

        //        if (!isNaN(addQty)) {
        //            totAdd += addQty;
        //        }
        //    });
        //    return totAdd;
        //}

        //function GetTotalSumAddedWt() {
        //    debugger;
        //    var totAdd = 0;
        //    $(document).find('.txtWt').each(function (index, elem) {
        //        addWt = parseFloat(JSUtility.GetNumber($(elem).val()));
        //        if (!isNaN(addWt)) {
        //            totAdd += addWt;
        //        }
        //    });
        //    return totAdd;
        //}

        //function sumGrandQty() {
        //    var totAdded = GetTotalSumAddedQty();
        //    $("#" + txtTotQty).val(JSUtility.FormatCurrency(totAdded));
        //}

        //function sumGrandWt() {
        //    var totAdded = GetTotalSumAddedWt();
        //    $("#" + txtTotWt).val(JSUtility.FormatCurrency(totAdded));
        //}


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

                    if (panels[i].id == gridUpdatePanelIDDet) {
                        bindFinishedItem(gridViewIDDet);
                        bindRejectItemList(gridViewIDDet);
                        //bindItemList(gridViewIDDet);
                        //bindBatchList(gridViewIDDet);
                        //sumGrandQty();
                        //sumGrandWt();
                    }

                }

            });


            bindSupporvisorList();
            bindFinishedItem(gridViewIDDet);
            bindRejectItemList(gridViewIDDet);
            //bindItemList(gridViewIDDet);
            bindBatchList(gridViewIDDet);

        });

      
        function bindFinishedItem(gridViewID) {
            var cgColumns = [
                               { 'columnName': 'itemname', 'width': '200', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                             , { 'columnName': 'uomname', 'width': '70', 'align': 'center', 'highlight': 4, 'label': 'Uom ' }
                              , { 'columnName': 'itemcode', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Code' }
                              , { 'columnName': 'itemgroupdesc', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Group Name' }
                             //, { 'columnName': 'bomname', 'width': '150', 'align': 'center', 'highlight': 4, 'label': 'BOM Name' }
                             //, { 'columnName': 'bomno', 'width': '160', 'align': 'center', 'highlight': 4, 'label': 'BOM NO' }
            ];

            var serviceURL = BOMNameListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            serviceURL += "&ispaging=1&isFinished=Y&for_production=Y";


            var gridSelector = "#" + gridViewID;


            $(gridSelector).find('input[id$="txtClosingRejectItemName"]').each(function (index, elem) {

                var elemRow = $(elem).closest('tr.gridRow');

                var hdnItemIDElem = $(elemRow).find('input[id$="txtClosingRejectItemName"]');



                $(elem).closest('tr').find('input[id$="btnClosingRejectItem"]').click(function (e) {
                    elmID = $(elem).attr('id');

                    $(elem).combogrid("dropdownClick");
                });

                //var DeptID = $('[id*=ddlDEPT_ID] option:selected').val();
                //var stlmid = $('[id*=ddlStorageLocation] option:selected').val();
                //var stlmid = $('#' + hdnStoragelocationID).val();


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
                        var vgroupid = $(elemRowCur).find('input[id$="hdngroupId"]').val();
                        //var vgroupid = $('#' + hdngroupId).val();+ "&groupid=" + vgroupid
                        var DeptID = $('[id*=ddlDEPT_ID] option:selected').val();
                        var stlmid = $('[id*=ddlStorageLocation] option:selected').val();

                        var newServiceURL = serviceURL + "&deptid=" + DeptID + "&stlmid=" + stlmid;
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
                            ClearFinishItemData(elemID);
                            return false;
                            //ClearGLAccountData(elemID);
                        }
                        if (ui.item.id == 0) {
                            event.preventDefault();
                            return false;
                            //ClearGLAccountData(elemID);
                        }
                        else {
                            SetFinishItemData(elemID, ui.item);
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
                        ClearFinishItemData(elemID);
                    }
                    else {
                        var DeptID = $('[id*=ddlDEPT_ID] option:selected').val();
                        var stlmid = $('[id*=ddlStorageLocation] option:selected').val();

                        var serviceURL = BOMNameListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
                        serviceURL += "&ispaging=1" + "&deptid=" + DeptID + "&stlmid=" + stlmid;

                        var prcNo = GetItemNo(eCode, serviceURL);

                        if (prcNo == null) {
                            ClearFinishItemData(elemID);
                        }
                        else {
                            SetFinishItemData(elemID, grp);
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

            var newServiceURL = serviceURL + "&selectedId=" + eCode + "&deptid=" + DeptID + "&stlmid=" + stlmid;
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

        function SetFinishItemData(txtItemCodeID, data) {
            $('#' + txtItemCodeID).val(data.itemid);
            var detRow = $('#' + txtItemCodeID).closest('tr.gridRow');
            $(detRow).find('input[id$="hdnClosingRejectItemName"]').val(data.itemid);
            $(detRow).find('input[id$="txtClosingRejectItemName"]').val(data.itemname);
            $(detRow).find('input[id$="hdnUOM_ID"]').val(data.uomid);
            $(detRow).find('input[id$="txtUOM_NAME"]').val(data.uomname);
            $('#' + hdnFGItemId).val(data.itemid)
            $("[id*=btnAddRejectItemId]").click();
        }

        function ClearFinishItemData(txtItemID) {
            var detRow = $('#' + txtItemID).closest('tr.gridRow');
            $(detRow).find('input[id$="hdnClosingRejectItemName"]').val('0');
            $(detRow).find('input[id$="txtClosingRejectItemName"]').val('');
            $('#' + hdnFGItemId).val('0')
            //sumGrandQty();
        }

        function bindRejectItemList(gridViewIDD) {
            var cgColumns = [{ 'columnName': 'itemname', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                             , { 'columnName': 'uomname', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Uom Name' }
                             , { 'columnName': 'itemcode', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Code' }
                             , { 'columnName': 'closing_qty', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Cls Qty' }
                             , { 'columnName': 'itemgroupdesc', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Group Name' }
                             , { 'columnName': 'class_name', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Class Name' }
                             //, { 'columnName': 'item_type', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Item Type' }
            ];

            var serviceURL = BOMNameListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            //serviceURL += "&companyid=" + companyid;
            serviceURL += "&ispaging=1&isFinished=Y&forproduction=Y";
            var gridSelector = "#" + gridViewIDD;
            $(gridSelector).find('input[id$="txtCrushingItemName"]').each(function (index, elem) {
                ///list click
                var elemRow = $(elem).closest('tr.gridRow');
                var hdnItemIDElem = $(elemRow).find('input[id$="txtCrushingItemName"]');
                //var prevGLCode = '';
                $(elem).closest('tr').find('input[id$="btnCrushingItemName"]').click(function (e) {
                    elmID = $(elem).attr('id');
                    //$(elem).combogrid("show");
                    $(elem).combogrid("dropdownClick");
                });

                //var DeptID = $('[id*=ddlDEPT_ID] option:selected').val();
                //var stlmid = $('#' + hdnStoragelocationID).val();


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
                    width: 650,
                    url: serviceURL,
                    search: function (event, ui) {
                        var elemRowCur = $(elem).closest('tr.gridRow');
                        //var vgroupid = $(elemRowCur).find('input[id$="hdngroupId"]').val();
                        //var vgroupid = $('#' + hdngroupId).val();
                        var DeptID = $('[id*=ddlDEPT_ID] option:selected').val();
                        var stlmid = $('[id*=ddlStorageLocation] option:selected').val();
                        var newServiceURL = serviceURL + "&deptid=" + DeptID + "&stlmid=" + stlmid + "&groupid=64";
                        //var newServiceURL = serviceURL + "&companycode=" + companyCode + "&branchcode=" + branchCode + "&deptcode=" + deptCode
                        newServiceURL = JSUtility.AddTimeToQueryString(newServiceURL);
                        $(this).combogrid("option", "url", newServiceURL);
                    },

                    select: function (event, ui) {
                        elemID = $(elem).attr('id');
                        if (!ui.item) {
                            event.preventDefault();
                            ClearClosignItemData(elemID);
                            return false;
                        }
                        if (ui.item.id == 0) {
                            event.preventDefault();
                            return false;
                        }
                        else {
                            SetRejectItemData(elemID, ui.item);
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
                        //                    if (!validateGLAccount(elemID, null)) {
                        //                        $(elem).val(prevGLCode);
                        //                        return false;
                        //                    }
                        ClearRejectItemData(elemID);
                    }
                    else {
                        var DeptID = $('[id*=ddlDEPT_ID] option:selected').val();
                        var stlmid = $('[id*=ddlStorageLocation] option:selected').val();
                        var serviceURL = BOMNameListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
                        serviceURL += "&ispaging=1&isFinished=Y&forproduction=Y" + "&deptid=" + DeptID + "&stlmid=" + stlmid + "&groupid=64";

                        var prcNo = GetItemNo(eCode, serviceURL);

                        if (prcNo == null) {
                            ClearRejectItemData(elemID);
                        }
                        else {
                            SetRejectItemData(elemID, grp);
                        }


                        //if (grp == null) {
                        //    ClearRejectItemData(elemID);
                        //}
                        //else {
                        //    SetRejectItemData(elemID, grp);
                        //}
                    }
                });

            });

        }

        function ClearRejectItemData(txtCrushingItemName) {
            //$('#' + txtGLAccCodeID).val('');
            var detRow = $('#' + txtCrushingItemName).closest('tr.gridRow');
            $(detRow).find('input[id$="hdnCrusingItemID"]').val('0');
            $(detRow).find('input[id$="txtCrushingItemName"]').val('');
            $(detRow).find('input[id$="hdnCrushingUOMID"]').val('0');
            $(detRow).find('input[id$="txtCrushingUOMName"]').val('');
            $(detRow).find('input[id$="txtSYSTEM_OPENING_STOCK"]').val('');

        }
        function SetRejectItemData(txtCrushingItemName, data) {
            //$('#' + hdnCrusingItemID).val(data.itemid);
            var detRow = $('#' + txtCrushingItemName).closest('tr.gridRow');
            $(detRow).find('input[id$="hdnCrusingItemID"]').val(data.itemid);
            $(detRow).find('input[id$="txtCrushingItemName"]').val(data.itemname);
            $(detRow).find('input[id$="hdnCrushingUOMID"]').val(data.uomid);
            $(detRow).find('input[id$="hdnITEM_BOM_ID"]').val(data.bomid);
            $(detRow).find('input[id$="txtCrushingUOMName"]').val(data.uomname);
            $(detRow).find('input[id$="txtSYSTEM_OPENING_STOCK"]').val(data.closing_qty);
          
        }

        function bindSupporvisorList() {

            var cgColumns = [{ 'columnName': 'empid', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Emp ID' }
                             , { 'columnName': 'fullname', 'width': '200', 'align': 'left', 'highlight': 4, 'label': 'Emp Name' }
                             , { 'columnName': 'designationName', 'width': '100', 'align': 'left', 'highlight': 4, 'label': ' Designation' }

            ];
            var serviceURL = SupporvisorListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            serviceURL += "&ispaging=0";
            var groupIDElem = $('#' + txtSUPERVISOR_NAME);

            $('#' + btnSUPERVISOR_ID).click(function (e) {
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
                width: 450,
                url: serviceURL,
                search: function (event, ui) {
                    var e = document.getElementById("<%=ddlDEPT_ID.ClientID%>");
                    var dept_id = e.options[e.selectedIndex].value;
                    var name = $('#' + txtSUPERVISOR_NAME).val();
                    var newServiceURL = serviceURL + "&deptid=" + dept_id + "&name=" + name;

                    $(this).combogrid("option", "url", newServiceURL);
                },
                select: function (event, ui) {
                    if (!ui.item) {
                        event.preventDefault();
                        return false;
                    }


                    if (ui.item.dealerid == '') {
                        event.preventDefault();
                        return false;
                    }
                    else {
                        // $('#' + hdnDealerID).val(ui.item.dealerid);
                        $('#' + hdnSUPERVISOR_ID).val(ui.item.empid);
                        $('#' + txtSUPERVISOR_NAME).val(ui.item.fullname);


                    }
                    return false;
                },

                lc: ''
            });


            $(groupIDElem).blur(function () {
                var self = this;

                var serviceURL = SupporvisorListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
                var e = document.getElementById("<%=ddlDEPT_ID.ClientID%>");
                var dept_id = e.options[e.selectedIndex].value;
                var eCode = $('#' + txtSUPERVISOR_NAME).val();
                serviceURL += "&ispaging=0&deptid=" + dept_id;

                var prcNo = "A"; //GetItemNo(eCode, serviceURL);

                if (prcNo == null) {
                    $('#' + hdnSUPERVISOR_ID).val('0');
                    $('#' + txtSUPERVISOR_NAME).val('');
                }
                else {
                    $('#' + hdnSUPERVISOR_ID).val(ui.item.empid);
                    $('#' + txtSUPERVISOR_NAME).val(ui.item.fullname);
                }



                var groupID = $(groupIDElem).val();
                if (groupID == '') {
                    // $('#' + hdnDealerID).val('0');

                    $('#' + hdnSUPERVISOR_ID).val('0');
                    $('#' + txtSUPERVISOR_NAME).val('');

                }
            });
        }


    </script>

     <style type="text/css">

     /* Style the tab */
.tab {
    overflow: hidden;
    border: 1px solid #ccc;
    background-color: #f1f1f1;
}

/* Style the buttons inside the tab */
.tab button {
    background-color: inherit;
    float: left;
    border: none;
    outline: none;
    cursor: pointer;
    padding: 14px 16px;
    transition: 0.3s;
    font-size: 17px;
    
}

/* Change background color of buttons on hover */
.tab button:hover {
    background-color: #ddd;
}

/* Create an active/current tablink class */
.tab button.active {
    background-color: #ccc;
}

/* Style the tab content */
.tabcontent {
    display: none;
    padding: 6px 12px;
    border: 1px solid #ccc;
    border-top: none;
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

  .modal-backdrop {
    z-index: 1020 !important;
    display:none;
}

      
      
        .hidden
        {
            /*visibility:hidden;*/
            display:none;
        }
 </style>

        

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="dvPageContent" style="width: 100%; height: auto;">
          <asp:Button ID="btnAddAutoUsedItem" runat="server" Style="display: none;" OnClick="btnAddAutoUsedItem_Click" />
         <asp:Button ID="btnRefreshClosingGrid" runat="server" Style="display: none;"  OnClick="btnRefreshClosingGrid_Click" />
          <asp:HiddenField ID="hdnBOMIDForFn" runat="server" />
         <asp:HiddenField ID="hdnFinishedQty" runat="server" />
         <asp:HiddenField ID="hndMACHINE_ID_CLOSING" runat="server" />
          <asp:HiddenField ID="hdnSUPERVISOR_ID" runat="server" />
        <asp:HiddenField ID="hdnFinishItemIdForRm" runat="server" />
        <asp:HiddenField ID="hdnFinishItemNameForRm" runat="server" />
        <div id="dvContentHeader" class="dvContentHeader">
            <div id="dvHeader" class="dvHeader_Prod">
                <asp:Label ID="lblHeader" CssClass="lblHeader" runat="server" Text="Reject Declaration"></asp:Label>
            </div>
            <!--Message Div -->
            <div id="dvMsg" runat="server" class="dvMessage" style="">
                            <asp:HiddenField ID="hdnPROD_CMP_CRUS_ID" runat="server" />
                             <asp:HiddenField ID="hdnDeptID" runat="server" Value ="0" />
                             <asp:HiddenField ID="hdnREJECT_ITEM_UOM_ID" runat="server"></asp:HiddenField>
                           <asp:HiddenField ID="hdnFGItemId" runat="server"></asp:HiddenField>
                <asp:Label ID="lblMessage" runat="server" Font-Size="Small" Width="100%"> </asp:Label>
            </div>
            <div id="dvHeaderControl" class="dvHeaderControl">
            </div>
        </div>

        <div id="dvContentMain" class="dvContentMain">
    <div id="tabs" style="height: 100%; background : url(../image/bg_greendot.gif) !important;border:none;font-size:12px;">
  <ul>
    <li><a href="#tabs-1">Reject Item </a></li>
    <li><a href="#tabs-2">Used Item </a></li>
    
  </ul>   
            <div id="dvControlsHead" style="height: auto; width: 100%;">
                <table cellpadding="2" cellspacing="2" border="0" >
                   <tr style="height:2px;">
                       <td></td>
                   </tr>
                     <tr>
                       
                         <td style="width:100px"></td>
                        <td align="right" class="auto-style2">
                            <asp:Label ID="lblProductionNo" runat="server" Text="Production No :" style=""></asp:Label>
                        </td>
                        <td class="auto-style2">
                            <asp:TextBox ID="txtProductionNo" runat="server" Style="text-align: left;" CssClass="textBox" ForeColor="Red" Width="190px"  Enabled="False"></asp:TextBox>
                        </td>
                        <td style="width:100px"></td>
                        <td align="right">
                              <asp:Label ID="lblProductionDate" runat="server" Text="Date :" style=""></asp:Label>
                        </td>
                        <td>
                              <asp:TextBox ID="txtProductionDate" runat="server" CssClass="textBox textDate dateParse" Style="text-align: left;" Width="100px"></asp:TextBox>
                        </td>
                        <td style="text-align: right">
                            <asp:Label ID="lblBatchNo" runat="server" Text="Batch NO :" Width="150px"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtPROD_BATCH_NO" runat="server" CssClass="textBox textAutoSelect textNumberOnly" Width="150px" TabIndex="2"></asp:TextBox>
                        </td>
                    </tr>
                     <tr style="height:2px;">
                       <td></td>
                   </tr>
                    <tr>
                         <td style="width:100px"></td>
                        <td style="text-align: right">
                                  <asp:Label ID="lblDEPT_ID" runat="server" Text="Department : " style=""></asp:Label>
                              </td>
                              <td style="text-align: left">

                                  <asp:DropDownList ID="ddlDEPT_ID" runat="server" CssClass="dropDownList required" Width="190px" ></asp:DropDownList>
                              </td>
                         <td style="width:100px"></td>
                        <td align="right">
                          
                             <asp:Label ID="lblShift" runat="server" Text="Shift : " style=""></asp:Label>
                        </td>
                        <td >
                             <asp:DropDownList ID="ddlSHIFT_ID" runat="server" CssClass="dropDownList" Width="155px" TabIndex="3" ></asp:DropDownList>
                           
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
                     <tr style="height:2px;">
                       <td></td>
                   </tr>
                    <tr>
                         <td style="width:100px"></td>
                         <td align="right">
                             <asp:Label ID="Label3" runat="server" Text="Storage Loc:" style=""></asp:Label>
                        </td>
                        <td>
                             <asp:DropDownList ID="ddlStorageLocation" runat="server" CssClass="dropDownList"></asp:DropDownList>
                        </td>
                          <td style="width:100px"></td>
                         <td style="text-align: right">
                            <asp:Label ID="lblSUPERVISOR_ID" runat="server" Text="Shift Incharge : "></asp:Label>
                        </td>
                        <td style="text-align: left; width:200px;" >
                            <asp:TextBox ID="txtSUPERVISOR_NAME" runat="server" CssClass="textBox textAutoSelect" Width="150px"  TabIndex="1"  autofocus></asp:TextBox>
                            <input id="btnSUPERVISOR_ID" type="button" value="" runat="server" class="buttonDropdown" style="width: 15px" tabindex="-1" />
                        </td>
                        <td align="right" class="auto-style2">
                            <asp:Label ID="lblRemarks" runat="server" Text="Remarks :" style=""></asp:Label>
                        </td>
                        <td class="auto-style2" >
                            <asp:TextBox ID="txtRemarks" runat="server" CssClass="textBox" Style="text-align: left;" ></asp:TextBox>
                        </td>
                    </tr>
                     <tr style="height:2px;">
                       <td></td>
                   </tr>
                                      
                </table>
            </div>
       <div id="tabs-1">
            <div id="dvControls" style="width: 100%; height: 300px;">
                <div id="Div3" runat="server" class="" style="width: 100%; text-align: left;border-top: solid 1px #0b07f5;">
                    <span style="font-weight: bold;font-size : 15px;color :#ff3b00;">Items Details: </span>
                </div>

                <div id="Div1" runat="server" class="" style="width: auto; height: auto; text-align: left">
                    <div id="dvGridHeader" style="width: 1170px; height: 25px; font-size: smaller;" class="subHeader_Prod">
                        <table style="height: 100%; color: #FFFFFF; font-weight: bold; text-align: left;"
                            class="defFont" cellspacing="1" cellpadding="1">
                            <tr class="headerRow_Prod">
                                <td width="65px" class="headerColCenter_prod">SL#
                                </td>
                                 <td width="200px" class="headerColCenter_prod"> Crushing Item
                                 </td>
                                 <td width="15px" class="headerColCenter_prod"></td>
                                 <td width="65px" class="headerColCenter_prod">UOM
                                </td>
                                 <td width="80px" class="headerColCenter_prod">Qty
                                </td>
                              
                                <td width="150px" class="headerColCenter_prod">Remarks
                                </td>
                                <td width="16px" class="headerColCenter">Delete
                                </td>
                                
                            </tr>
                        </table>
                    </div>
                    <div id="dvGrid" style="width: 1170px; height: 220px; overflow: auto;" class="dvGrid">
                      <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" ShowHeader="False" 
                                    CellPadding="1" CellSpacing="1" ForeColor="#333333" GridLines="Both" DataKeyNames="ITEM_ID" 
                                    EnableModelValidation="True" ClientIDMode="AutoID" OnRowCommand="GridView1_RowCommand" OnRowCreated="GridView1_RowCreated" OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="GridView1_RowDeleting"  >
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <Columns>
                                       

                                        <asp:TemplateField HeaderText="SL#">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSLNO" runat="server" Text='<%# Bind("CMP_CRUS_DET_SLNO") %>' Style="text-align: center;"
                                                    Width="65px">
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
                                                                <td>
                                                                    <asp:HiddenField ID="hdnPROD_DTL_ID" runat="server" Value='<%# Bind("PROD_CMP_CRUS_DTL_ID") %>' />
                                                                </td>
                                                                  
                                                                    <td>
                                                                     <asp:TextBox ID="txtCrushingItemName" runat="server" CssClass="textBox textAutoSelect" Width="200px" Text='<%# Bind("ITEM_NAME") %>'></asp:TextBox>
                                                                      <asp:HiddenField ID="hdnCrusingItemID" runat="server" Value='<%# Bind("ITEM_ID") %>' />
                                                                       <asp:HiddenField ID="hdnITEM_BOM_ID" runat="server" Value='<%# Bind("BOM_ID") %>' />
                                                                    </td>
                                                                    <td>
                                                                        <input id="btnCrushingItemName" type="button" value="" runat="server" class="buttonDropdown" tabindex="-1" />
                                                                    </td>

                                                                  <td>
                                                                    <asp:TextBox ID="txtCrushingUOMName" runat="server" CssClass="textBox textAutoSelect"  Width="65px" Text='<%# Bind("UOM_NAME") %>'></asp:TextBox>
                                                                    <asp:HiddenField ID="hdnCrushingUOMID" runat="server" Value='<%# Bind("UOM_ID") %>' />
                                                                  </td>
                                                                  <td>
                                                                    <asp:TextBox ID="txtCrushingItemQty" runat="server" style="text-align: right;font-size:14px;color:black;" CssClass="textBox textAutoSelect txtQty"  Width="80px" align="right" Text='<%# Bind("ITEM_QTY") %>' onblur="TestOnTextChange(this)"  onkeypress=" return isNumberKey(event,this);"></asp:TextBox>
                                                                 </td>
                                                                

                                                                <td>
                                                                    <asp:TextBox ID="txtRemarks" runat="server" CssClass="textBox textAutoSelect" Width="150px" Text='<%# Bind("CMP_CRUS_DET_REMARKS") %>' Style=""></asp:TextBox>
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
                              <asp:AsyncPostBackTrigger ControlID="btnAddRejectItemId" EventName="Click" />
                            </Triggers>
                    </asp:UpdatePanel>


                    </div>
                    <div id="divGridControls2" style="width: 100%; height: 25px; border-top: solid 1px #0b07f5;">
                                            <table style="width: auto; height: 100%; text-align: center;" cellspacing="1" cellpadding="1"
                                                border="0">
                                                <tr>
                                                    <td style="width: 2px"></td>
                                                    <td style="width: 90px" align="left">
                                                        <asp:Button ID="btnNewRow2" runat="server" CssClass="buttonNewRow" Text="" OnClientClick="ShowProgress()" OnClick="btnNewRow2_Click" />
                                                        <asp:Button ID="btnAddRejectItemId" runat="server" OnClick="btnAddRejectItemId_Click" Style="display:none;" />
                                                    </td>
                                                    <td style="width: 20px;">
                                                        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                                                            DisplayAfter="300">
                                                            <ProgressTemplate>
                                                                <asp:Image ID="imgProgress2" runat="server" ImageUrl="~/image/loader.gif" />
                                                            </ProgressTemplate>
                                                        </asp:UpdateProgress>
                                                    </td>
                                                    <td style="width: 270px"></td>
                                                    <td align="right">&nbsp;
                                                    </td>
                                                

                                                </tr>
                                            </table>
                                        </div>
                </div>
                

            
        </div>
       </div>
      
     <div id="tabs-2">
                    <%-- Start Closing Details --%>

           
                    <div id="Div2" runat="server" class="" style="float:left; width: 100%; text-align: left;  border-top: solid 1px #0b07f5;">
                        <span style="font-weight: bold; font-size: 15; color: #ff3b00;">Used Raw Materials Details: </span>
                    </div>
         <div id="groupBoxContainer1 boxShadow" style="height: auto; ">
                <div id="groupDataDetails1" style="width: 100%; height: auto;">
                    <div id="Div4" runat="server" class="" style="width: auto; height: auto; text-align: left; width: 100%">
                        <div id="dvGridHeaderClosing" style="float:left; width: 1200px; height: 25px; font-size: smaller;" class="subHeader_Prod">
                            <table style=" height: 100%; color: #FFFFFF; font-weight: bold; text-align: left;"
                                class="defFont" cellspacing="1" cellpadding="1">
                                <tr class="headerRow_Prod">
                                    <td width="35px" class="headerColCenter_prod">SL#
                                    </td>
                                    <td width="180px" class="headerColCenter_prod">Crushing Item
                                    </td>
                                    <td width="15px" class="headerColCenter_prod"></td>
                                    <td width="200px" class="headerColCenter_prod"> Used Item
                                    </td>
                                    <td width="15px" class="headerColCenter_prod"></td>

                                    <td width="65px" class="headerColCenter_prod">UOM
                                    </td>
                                    <td width="115px" class="headerColCenter_prod">Op Stock
                                    </td>
                                  
                                     <td width="75px" class="headerColCenter_prod">Std Used Qty
                                    </td>
                                    <td width="110px" class="headerColCenter_prod">Remarks
                                    </td>
                                    <td width="16px" class="headerColCenter_prod">Delete
                                    </td>

                                </tr>
                            </table>
                        </div>
                        <div id="dvGridClosing" style=" float:left; width: 1200px; height: 200px; overflow: auto;" class="dvGrid">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:GridView ID="grdClosingRowMaterial" runat="server" width="1125px" AutoGenerateColumns="False" ShowHeader="False"
                                        CellPadding="1" CellSpacing="1" ForeColor="#333333" GridLines="Both" DataKeyNames="CLOSING_ITEM_ID"
                                        EnableModelValidation="True" ClientIDMode="AutoID" OnRowCommand="grdClosingRowMaterial_RowCommand" OnRowCreated="grdClosingRowMaterial_RowCreated" OnRowDataBound="grdClosingRowMaterial_RowDataBound" OnRowDeleting="grdClosingRowMaterial_RowDeleting">
                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                        <Columns>


                                            <asp:TemplateField HeaderText="SL#" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCLOSING_SI" runat="server" Text= <%# Container.DataItemIndex + 1 %>   Style="text-align: center;" Width="32px">
                                                    </asp:Label>

                                                </ItemTemplate>
                                                <ItemStyle VerticalAlign="Top" Width="50px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText=" Item Type" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <div>
                                                        <table border="0" cellpadding="1" cellspacing="1">
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <asp:HiddenField ID="hdnCLOSING_ID" runat="server" Value='<%# Bind("CLOSING_ID") %>' />
                                                                    </td>
                                                                    <td>
                                                                        <asp:HiddenField ID="hdnPROD_REJECTION_ID" runat="server" Value='<%# Bind("REJ_MST_ID") %>' />
                                                                        <%--<asp:HiddenField ID="hdnISMANUAL" runat="server" Value='<%# Bind("ISMANUAL") %>' />--%>
                                                                         <asp:HiddenField ID="hdnMachineIDgridcloding" runat="server" Value='<%# Bind("MACHINE_ID") %>' />
                                                                    </td>

                                                                    <td>
                                                                        <asp:TextBox ID="txtClosingRejectItemName" runat="server" CssClass="textBox textAutoSelect" Width="180px" Text='<%# Bind("REJECT_ITEM_NAME") %>'></asp:TextBox>
                                                                        <asp:HiddenField ID="hdnClosingRejectItemName" runat="server" Value='<%# Bind("REJECT_ITEM_ID") %>' />
                                                                    </td>
                                                                    <td>
                                                                        <input id="btnClosingRejectItem" type="button" value="" runat="server" class="buttonDropdown" tabindex="-1" />
                                                                    </td>




                                                                    <td>
                                                                        <asp:TextBox ID="txtCLOSINGITEM_NAME" runat="server" CssClass="textBox textAutoSelect" Width="198px" Text='<%# Bind("CLOSING_ITEM_NAME") %>'></asp:TextBox>
                                                                        <asp:HiddenField ID="hdnCLOSING_ITEM_ID" runat="server" Value='<%# Bind("CLOSING_ITEM_ID") %>' />
                                                                    </td>
                                                                    <td>
                                                                        <input id="btnCLOSINGITEM_NAME" type="button" value="" runat="server" class="buttonDropdown" tabindex="-1" />
                                                                    </td>
                                                                    <td>

                                                                        <asp:TextBox ID="txtCLOSING_UOM_NAME" runat="server" CssClass="textBox textAutoSelect" Width="62px" Text='<%# Bind("CLOSING_UOM_NAME") %>'></asp:TextBox>
                                                                        <asp:HiddenField ID="hdnCLOSING_UOM_ID" runat="server" Value='<%# Bind("CLOSING_UOM_ID") %>' />
                                                                    </td>
                                                                    <td align="right">
                                                                        <asp:TextBox ID="txtSYSTEM_OPENING_STOCK" runat="server" CssClass="textBox textAutoSelect"  Width="110px" style="text-align:right;" Text='<%# Eval("SYSTEM_OPENING_STOCK", "{0:#,##0.00}") %>' onkeypress="return isNumberKey(event,this);"></asp:TextBox>
                                                                    </td>

                                                                    <td align="right">
                                                                        <asp:TextBox ID="txtSTD_USED_QTY" runat="server" CssClass="textBox textAutoSelect" Width="72"  align="right" style="text-align:right;" Text='<%# Eval( "STD_USED_QTY", "{0:#,##0.00}") %>'  onkeypress="return isNumberKey(event,this);"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtCLOSING_REMARKS" runat="server" CssClass="textBox textAutoSelect" Width="110px" Text='<%# Bind("CLOSING_REMARKS") %>' Style=""></asp:TextBox>
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
                                    <input id="Hidden3" type="hidden" runat="server" value="[]" />
                                    <input id="Hidden4" type="hidden" runat="server" value="[]" />
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnNewRowClosing" EventName="Click" />
                                     <asp:AsyncPostBackTrigger ControlID="btnRefreshClosingGrid" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>

                        <div id="divGridControlsClosing">
                            <table style="width: 70%; height: 100%; text-align: center;" cellspacing="1" cellpadding="1"
                                border="0">
                                <tr>
                                    <td style="width: 2px"></td>

                                    <td style="width: 160px; text-align: right;"></td>
                                    <td align="right" style="text-align: left">&nbsp;
                                    </td>
                                    <td align="right">&nbsp;</td>
                                    <td align="right">&nbsp;</td>
                                    <td align="right">&nbsp;
                                    </td>
                                    <td style="width: 90px" align="left">
                                        <asp:Button ID="btnNewRowClosing" runat="server" CssClass="buttonNewRow" Text="" OnClientClick="ShowClosingProgress()" OnClick="btnNewRowClosing_Click" Visible="false" />
                                    </td>
                                    <td style="width: 20px;">
                                        <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="UpdatePanel2"
                                            DisplayAfter="300">
                                            <ProgressTemplate>
                                                <asp:Image ID="imgProgressClosing" runat="server" ImageUrl="~/image/loader.gif" />
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </td>
                                </tr>


                            </table>
                        </div>
                    </div>

                     </div>
            </div>
                    <%-- End  Closing Details --%>
    
                     <%-- Start  Closing Summary --%>
                   <div id="groupBoxContainer2 boxShadow" style="height: auto; width: 90%;">
                            <div id="groupDataDetails2" style="float: left;width: 90%; height: auto;">
                                <div id="Div6" runat="server" class="" style="float:left; width: 90%; text-align: left;  border-top: solid 1px #0b07f5;">
                                    <span style="font-weight: bold; font-size: 15; color: #ff3b00;">Used Raw Materials Summary: </span>
                                </div>

                                <div id="Div7" runat="server" class="" style=" float:left; width: auto; height: auto; text-align: left; width: 100%">
                                    <div id="dvGridHeaderClosingSummary" style="width: 500px; height: 25px; font-size: smaller;" class="subHeader_Prod">
                                        <table style="height: 100%; color: #FFFFFF; font-weight: bold; text-align: left;"
                                            class="defFont" cellspacing="1" cellpadding="1">
                                            <tr class="headerRow_Prod">
                                                <td width="40px" class="headerColCenter_prod">SL#
                                                </td>
                                     
                                                <td width="250px" class="headerColCenter_prod">RM Item
                                                </td>
                                    

                                                <td width="70px" class="headerColCenter_prod">UOM
                                                </td>
                                             
                                                <td width="80px" class="headerColCenter_prod">Used Qty
                                                </td>
                                             

                                            </tr>
                                        </table>
                                    </div>
                                    <div id="dvGridClosingSummary" style="width: auto; height: 150px; overflow: auto;" class="dvGrid">
                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:GridView ID="grdClosingRowMaterialSummary" runat="server" AutoGenerateColumns="False" ShowHeader="False"
                                                    CellPadding="1" CellSpacing="1" ForeColor="#333333" GridLines="Both" DataKeyNames="CLOSING_ITEM_ID"
                                                    EnableModelValidation="True" ClientIDMode="AutoID"   OnRowCreated="grdClosingRowMaterialSummary_RowCreated">
                                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="SL#">
                                                            <ItemTemplate>

                                                   
                                                                <asp:Label ID="lblCLOSING_SI" runat="server" Text= <%# Container.DataItemIndex + 1 %>    Style="text-align: center;" Width="40px">
                                                                </asp:Label>

                                                            </ItemTemplate>
                                                            <ItemStyle VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText=" Item Type" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <div>
                                                                    <table border="0" cellpadding="1" cellspacing="1">
                                                                        <tbody>
                                                                            <tr>
                                                                             
                                                                                <td>
                                                                                    <asp:TextBox ID="txtCLOSINGITEM_NAME" runat="server" CssClass="textBox textAutoSelect" Width="250px" Text='<%# Bind("CLOSING_ITEM_NAME") %>'></asp:TextBox>
                                                                                    <asp:HiddenField ID="hdnCLOSING_ITEM_ID" runat="server" Value='<%# Bind("CLOSING_ITEM_ID") %>' />
                                                                                </td>
                                                                             
                                                                                <td>

                                                                                    <asp:TextBox ID="txtCLOSING_UOM_NAME" runat="server" CssClass="textBox textAutoSelect" Width="65px" Text='<%# Bind("CLOSING_UOM_NAME") %>'></asp:TextBox>
                                                                                    <asp:HiddenField ID="hdnCLOSING_UOM_ID" runat="server" Value='<%# Bind("CLOSING_UOM_ID") %>' />
                                                                                </td>
                                                                             
                                                                                <td>
                                                                                    <asp:TextBox ID="txtISSUE_STOCK" runat="server" CssClass="textBox textAutoSelect" Width="75px" BackColor="Khaki" align="right" style="text-align:right;" Text ='<%# Bind("ISSUE_STOCK") %>' onchange="calcCloseingStock(this)" onkeypress="return isNumberKey(event,this);"></asp:TextBox>
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
                                                        <asp:TemplateField HeaderText="Delete" ShowHeader="false" Visible="false">
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
                                                <input id="Hidden5" type="hidden" runat="server" value="[]" />
                                                <input id="Hidden6" type="hidden" runat="server" value="[]" />
                                            </ContentTemplate>
                                 
                                        </asp:UpdatePanel>
                                    </div>
                         
                                </div>

                     </div>
                        </div>
                    <%-- ENd Closing Summary --%>
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
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonSave checkRequired" AccessKey="s" OnClick="btnSave_Click" OnClientClick="if ( ! UserSaveConfirmation()) return false;" />
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="buttonEdit" OnClick="btnEdit_Click" Visible="true" />
                    </td>


                    <td>
                        <asp:Button ID="btnRefresh" runat="server" Text="Refresh" CssClass="buttonRefresh checkIsDirty" OnClick="btnRefresh_Click" />
                    </td>
                    <td>
                       <asp:Button ID="btnAuthorize" runat="server" Text="Authorize" CssClass="buttonAthorize" OnClientClick="if ( ! UserAuthorizeConfirmation()) return false;" OnClick="btnAuthorize_Click" Visible="true" />
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

    <div id="overlay" class="overlay" >
         <div style="margin:auto;width:200px;height:400px;background-color:black;border:solid 1px black;
                  text-align:center; vertical-align:middle;"> 
           <span style="color:white; font-size:medium;" >Please Wait...</span>
             <br />
             <img alt="" src="../../image/progress.gif" />
         </div>
    </div>

    <div id="overlayReport" class="overlay" style="opacity: 0.8;">
         <div style="margin:auto;width:450px;height:80px; position: relative;background-color: blue;
                  text-align:center; vertical-align:middle; cursor:auto; z-index: 9999999;">
           <table width="100%">
           <tr>
               <td>
                   <span style="color:white; font-size:medium;" >Click Open Report to view Report.</span>
               </td>
           </tr>
           <tr>
             <td>         
                <input id="btnOpenReportWindow" type="button" value="Open Report" class="buttoncommon" />
                <input id="btnCacnelReportWindow" type="button" value="Cancel" class="buttoncommon"  />  
             </td>
           </tr>            
            </table>
         </div>
    </div>
</asp:Content>
