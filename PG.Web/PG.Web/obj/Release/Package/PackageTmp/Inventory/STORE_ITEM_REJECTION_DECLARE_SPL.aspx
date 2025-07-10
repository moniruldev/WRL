<%@ Page Title="" Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="STORE_ITEM_REJECTION_DECLARE_SPL.aspx.cs" Inherits="PG.Web.Inventory.STORE_ITEM_REJECTION_DECLARE_SPL" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="../javascript/jquery.ui.combogrid.js" type="text/javascript"></script>
    <script src="../javascript/jquery.attributeobserver.js" type="text/javascript"></script>
    <link href="../css/jquery.ui.combogrid.css" rel="stylesheet" type="text/css" />

      <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js" type="text/javascript"></script> 
     <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css"/> 
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap-theme.min.css"/>  

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
        
     <%--   var txtProductionDate = '<%=txtProductionDate.ClientID%>';
        var ddlREJECT_ITEM_TYPE = '<%=ddlREJECT_ITEM_TYPE.ClientID%>';--%>

    <%--    var txtTotQty = '<%=txtTotQty.ClientID%>';
        var txtTotWt = '<%=txtTotWt.ClientID%>';--%>
        
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
        //    var qty = $(detRow).find('input[id$="txtRejection_QTY"]').val();
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

        function ValidateBatchStock() {
            var grid = document.getElementById("<%= grdItemDetails.ClientID%>");
            var totalqty = 0;
            var sumUsed = 0;
            for (var i = 0; i < grid.rows.length - 1; i++) {
                var pending = 0;
              
                var chkSelect = $("input[id*=chkSelect]");
                if (chkSelect[i].checked) {
                    if (parseFloat($("input[id*=txtusedQty]")[i].value) == 0) {
                        alert('Please input used quantity or deselect row');
                        return false;
                    }

                    if (parseFloat($("input[id*=txtSysOpening]")[i].value) < parseFloat($("input[id*=txtusedQty]")[i].value)) {
                        alert("Item Quantity Can not be greater than Stock Qty");
                        return false;
                    }


                }
                else {
                    $("input[id*=txtusedQty]")[i].value = 0;

                }


            }
       

        }


        function CalculateBatchStock(txtbox) {


            var grid = document.getElementById("<%= grdItemDetails.ClientID%>");
            var sumUsed = 0;
            for (var i = 0; i < grid.rows.length - 1; i++) {
                var pending = 0;

                var chkSelect = $("input[id*=chkSelect]");
                if (chkSelect[i].checked) {
                    var txtSysOpening = $("input[id*=txtSysOpening]");
                    //var txtTotIssueQty = $("input[id*=txtTotIssueQty]");

                    //if (parseFloat(txtSysOpening[i].value) < parseFloat(txtTotIssueQty[i].value)) {
                    //    pending = parseFloat(txtTotIssueQty[i].value) - sumUsed;
                    //    if ((sumUsed + parseFloat(txtSysOpening[i].value)) > parseFloat(txtTotIssueQty[i].value)) {
                    //        $("input[id*=txtusedQty]")[i].value = pending;
                    //    }
                    //    else {
                    //        $("input[id*=txtusedQty]")[i].value = parseFloat(txtSysOpening[i].value);//-sumUsed;
                    //    }

                    //    sumUsed += parseFloat($("input[id*=txtusedQty]")[i].value);

                    //}

                    //if (parseFloat(txtSysOpening[i].value) > parseFloat(txtTotIssueQty[i].value)) {
                    //    $("input[id*=txtusedQty]")[i].value = parseFloat(txtTotIssueQty[i].value) - sumUsed;
                    //    sumUsed += parseFloat($("input[id*=txtusedQty]")[i].value);
                    //    //pending = parseFloat(txtTotIssueQty[i].value) - parseFloat($("input[id*=txtusedQty]")[i].value);
                    //}



                }
                else {
                    $("input[id*=txtusedQty]")[i].value = 0;

                }


            }

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
                        //bindFinishedItem(gridViewIDDet);
                        bindRejectItemList(gridViewIDDet);
                        //bindItemList(gridViewIDDet);
                        //bindBatchList(gridViewIDDet);
                        //sumGrandQty();
                        //sumGrandWt();
                    }

                }

            });


           
            //bindFinishedItem(gridViewIDDet);
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


            $(gridSelector).find('input[id$="txtFINISH_ITEM_NAME"]').each(function (index, elem) {

                var elemRow = $(elem).closest('tr.gridRow');

                var hdnItemIDElem = $(elemRow).find('input[id$="txtFINISH_ITEM_NAME"]');



                $(elem).closest('tr').find('input[id$="btnFinishItem"]').click(function (e) {
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
            $(detRow).find('input[id$="hdnFINISHED_ITEM_ID"]').val(data.itemid);
            $(detRow).find('input[id$="txtFINISH_ITEM_NAME"]').val(data.itemname);
            $(detRow).find('input[id$="hdnUOM_ID"]').val(data.uomid);
            $(detRow).find('input[id$="txtUOM_NAME"]').val(data.uomname);
            $(detRow).find('input[id$="hdnIsBatch"]').val(data.isbatch);
            $('#' + hdnFGItemId).val(data.itemid)
            $("[id*=btnAddRejectItemId]").click();
        }

        function ClearFinishItemData(txtItemID) {
            var detRow = $('#' + txtItemID).closest('tr.gridRow');
            $(detRow).find('input[id$="hdnFINISHED_ITEM_ID"]').val('0');
            $(detRow).find('input[id$="txtFINISH_ITEM_NAME"]').val('');
            $(detRow).find('input[id$="hdnIsBatch"]').val('');
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
            serviceURL += "&ispaging=1&forproduction=Y";
            var gridSelector = "#" + gridViewIDD;
            $(gridSelector).find('input[id$="txtRejectItemName"]').each(function (index, elem) {
                ///list click
                var elemRow = $(elem).closest('tr.gridRow');
                var hdnItemIDElem = $(elemRow).find('input[id$="txtRejectItemName"]');
                //var prevGLCode = '';
                $(elem).closest('tr').find('input[id$="btnRejectItemName"]').click(function (e) {
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
                        var chkValue = document.getElementById("<%= chkIsRM.ClientID %>").checked;
                        
                     
                        var newServiceURL = serviceURL + "&deptid=" + DeptID; //+ "&groupid=62";
                        if (chkValue == true) {
                            newServiceURL = newServiceURL + "&isFinished=N"  + "&stlmid=0";
                        }
                        else {
                            newServiceURL = newServiceURL + "&isFinished=Y"  + "&stlmid=" + stlmid;

                        }
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
                            SetClosingItemData(elemID, ui.item);
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
                        ClearClosingItemData(elemID);
                    }
                    else {
                        var DeptID = $('[id*=ddlDEPT_ID] option:selected').val();
                        var stlmid = $('[id*=ddlStorageLocation] option:selected').val();
                        var chkValue = document.getElementById("<%= chkIsRM.ClientID %>").checked;
                        var serviceURL = BOMNameListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
                        serviceURL += "&ispaging=1&isFinished=Y&forproduction=Y" + "&deptid=" + DeptID ;// + "&groupid=62";
                        if (chkValue == true) {
                            newServiceURL = newServiceURL + "&isFinished=N"  + "&stlmid=0";
                        }
                        else {
                            newServiceURL = newServiceURL + "&isFinished=Y"  + "&stlmid=" + stlmid;

                        }
                        var prcNo = GetItemNo(eCode, serviceURL);

                        if (prcNo == null) {
                            ClearClosingItemData(elemID);
                        }
                        else {
                            SetClosingItemData(elemID, grp);
                        }


                        //if (grp == null) {
                        //    ClearClosingItemData(elemID);
                        //}
                        //else {
                        //    SetClosingItemData(elemID, grp);
                        //}
                    }
                });

            });

        }

        function ClearClosingItemData(txtRejectItemName) {
            //$('#' + txtGLAccCodeID).val('');
            var detRow = $('#' + txtRejectItemName).closest('tr.gridRow');
            $(detRow).find('input[id$="hdnRejectItemId"]').val('0');
            $(detRow).find('input[id$="txtRejectItemName"]').val('');
            $(detRow).find('input[id$="hdnUOM_ID"]').val('0');
            $(detRow).find('input[id$="txtUOM_NAME"]').val('');
            $(detRow).find('input[id$="txtSYSTEM_OPENING_STOCK"]').val('');
            $(detRow).find('input[id$="txtMANUAL_OPENING_STOCK"]').val('');
            $(detRow).find('input[id$="hdnIsBatch"]').val('');

        }
        function SetClosingItemData(txtRejectItemName, data) {
            //$('#' + hdnRejectItemId).val(data.itemid);
            var detRow = $('#' + txtRejectItemName).closest('tr.gridRow');
            $(detRow).find('input[id$="hdnRejectItemId"]').val(data.itemid);
            $(detRow).find('input[id$="txtRejectItemName"]').val(data.itemname);
            $(detRow).find('input[id$="hdnUOM_ID"]').val(data.uomid);
            $(detRow).find('input[id$="txtUOM_NAME"]').val(data.uomname);
            $(detRow).find('input[id$="txtSYSTEM_OPENING_STOCK"]').val(data.closing_qty);
            $(detRow).find('input[id$="txtMANUAL_OPENING_STOCK"]').val(data.closing_qty);
            $(detRow).find('input[id$="hdnIsBatch"]').val(data.isbatch);
            $("[id*=btnAddRejectItemId]").click();
          
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
        <div id="dvContentHeader" class="dvContentHeader">
            <div id="dvHeader" class="dvHeader_Prod">
                <asp:Label ID="lblHeader" CssClass="lblHeader" runat="server" Text="Reject Declaration"></asp:Label>
            </div>
            <!--Message Div -->
            <div id="dvMsg" runat="server" class="dvMessage" style="">
                            <asp:HiddenField ID="hdnProd_Rejection_ID" runat="server" />
                             <asp:HiddenField ID="hdnDeptID" runat="server" Value ="0" />
                             <asp:HiddenField ID="hdnREJECT_ITEM_UOM_ID" runat="server"></asp:HiddenField>
                           <asp:HiddenField ID="hdnFGItemId" runat="server"></asp:HiddenField>
                <asp:Label ID="lblMessage" runat="server" Font-Size="Small" Width="100%"> </asp:Label>
            </div>
            <div id="dvHeaderControl" class="dvHeaderControl">
            </div>
        </div>

        <div id="dvContentMain" class="dvContentMain">
            <div id="dvControlsHead" style="height: auto; width: 100%;">
                <table cellpadding="4" cellspacing="4" border="0" >
                   <tr style="height:5px;">
                       <td></td>
                   </tr>
                     <tr>
                       
                         <td style="width:100px"></td>
                        <td align="right" class="auto-style2">
                            <asp:Label ID="lblRejectionNo" runat="server" Text="Rejection NO :" style=""></asp:Label>
                        </td>
                        <td class="auto-style2">
                            <asp:TextBox ID="txtRejectionNo" runat="server" Style="text-align: left;" CssClass="textBox" ForeColor="Red" Width="190px"  Enabled="False"></asp:TextBox>
                        </td>
                        <td style="width:100px"></td>
                        <td align="right">
                              <asp:Label ID="lblRejectionDate" runat="server" Text="Date :" style=""></asp:Label>
                        </td>
                        <td>
                              <asp:TextBox ID="txtRejectionDate" runat="server" CssClass="textBox textDate dateParse" Style="text-align: left;" Width="100px"></asp:TextBox>
                        </td>
                    </tr>
                     <tr style="height:5px;">
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
                          
                             <asp:Label ID="Label1" runat="server" Text="Shift : " style=""></asp:Label>
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
                     <tr style="height:5px;">
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
                        <td align="right" class="auto-style2">
                            <asp:Label ID="lblRejectionReason" runat="server" Text="Rejection Reason :" style=""></asp:Label>
                        </td>
                        <td class="auto-style2" >
                            <asp:TextBox ID="txtRejectionReason" runat="server" CssClass="textBox" Style="text-align: left;" ></asp:TextBox>
                        </td>
                          <td class="auto-style2" style="padding-left:10px;" >
                           <asp:CheckBox runat="server" ID="chkIsRM" />
                              <asp:Label runat="server" ID="lblIsRm" Text="Is RM" Style="color:green;font-size:12px;font-weight:bold;"></asp:Label>
                        </td>
                    </tr>
                     <tr style="height:5px;">
                       <td></td>
                   </tr>
                                      
                </table>
            </div>
            <div id="dvControls" style="width: 100%; height: 400px;">
                <div id="Div3" runat="server" class="" style="width: 100%; text-align: left;border-top: solid 1px #0b07f5;">
                    <span style="font-weight: bold;font-size : 15px;color :#ff3b00;">Rejection Details: </span>
                </div>

                <div id="Div1" runat="server" class="" style="width: auto; height: auto; text-align: left">
                    <div id="dvGridHeaderClosing" style="width: 1170px; height: 25px; font-size: smaller;" class="subHeader_Prod">
                        <table style="height: 100%; color: #FFFFFF; font-weight: bold; text-align: left;"
                            class="defFont" cellspacing="1" cellpadding="1">
                            <tr class="headerRow_Prod">
                                <td width="65px" class="headerColCenter_prod">SL#
                                </td>
                               <%--  <td width="135px" class="headerColCenter_prod" style="display:none;">
                                     Date
                                </td>
                                <td width="100px" class="headerColCenter_prod">
                                    Batch No
                                </td>--%>
                             <%--   <td width="15px" class="headerColLeft">

                                </td>--%>
                              <%--  <td width="180px" class="headerColCenter_prod">Item
                                </td>
                                <td width="15px" class="headerColLeft"></td>--%>
                                 <td width="200px" class="headerColCenter_prod"> Reject Item
                                 </td>
                                 <td width="15px" class="headerColCenter_prod"></td>
                                 <td width="65px" class="headerColCenter_prod">UOM
                                </td>
                                 <td width="80px" class="headerColCenter_prod">Reject Qty
                                </td>
                                <%--<td width="52px" class="headerColCenter_prod">Batch
                                </td>--%>
                              
                                <td width="150px" class="headerColCenter_prod">Remarks
                                </td>
                                <td width="16px" class="headerColCenter">Delete
                                </td>
                                
                            </tr>
                        </table>
                    </div>
                    <div id="dvGridClosing" style="width: 1170px; height: 250px; overflow: auto;" class="dvGrid">
                      <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" ShowHeader="False" 
                                    CellPadding="1" CellSpacing="1" ForeColor="#333333" GridLines="Both" DataKeyNames="ITEM_ID" 
                                    EnableModelValidation="True" ClientIDMode="AutoID" OnRowCommand="GridView1_RowCommand" OnRowCreated="GridView1_RowCreated" OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="GridView1_RowDeleting"  >
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <Columns>
                                       

                                        <asp:TemplateField HeaderText="SL#">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSLNO" runat="server" Text='<%# Bind("PROD_REJ_DET_SLNO") %>' Style="text-align: center;"
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
                                                                    <asp:HiddenField ID="hdnPROD_REJECTION_ID" runat="server" Value='<%# Bind("PROD_REJECTION_ID") %>' />
                                                                    <asp:HiddenField ID="hdnIsBatch" runat="server" Value='<%# Bind("IS_BATCH") %>' />
                                                                </td>
                                                          
                                                                 <%--  <td>
                                                                        <asp:TextBox ID="txtFINISH_ITEM_NAME" runat="server" CssClass="textBox textAutoSelect" Width="180px" Text='<%# Bind("ITEM_NAME") %>'></asp:TextBox>
                                                                        <asp:HiddenField ID="hdnFINISHED_ITEM_ID" runat="server" Value='<%# Bind("ITEM_ID") %>' />
                                                                    </td>
                                                                    <td>
                                                                        <input id="btnFinishItem" type="button" value="" runat="server" class="buttonDropdown" tabindex="-1" />
                                                                    </td>--%>
                                                                    <td>
                                                                     <asp:TextBox ID="txtRejectItemName" runat="server" CssClass="textBox textAutoSelect" Width="200px" Text='<%# Bind("TO_REJ_ITEM_NAME") %>'></asp:TextBox>
                                                                      <asp:HiddenField ID="hdnRejectItemId" runat="server" Value='<%# Bind("TO_REJ_ITEM_ID") %>' />
                                                                    </td>
                                                                    <td>
                                                                        <input id="btnRejectItemName" type="button" value="" runat="server" class="buttonDropdown" tabindex="-1" />
                                                                    </td>

                                                                  <td>
                                                                    <asp:TextBox ID="txtUOM_NAME" runat="server" CssClass="textBox textAutoSelect"  Width="65px" Text='<%# Bind("UOM_NAME") %>'></asp:TextBox>
                                                                    <asp:HiddenField ID="hdnUOM_ID" runat="server" Value='<%# Bind("UOM_ID") %>' />
                                                                  </td>
                                                                  <td>
                                                                    <asp:TextBox ID="txtRejection_QTY" runat="server" style="text-align: right;font-size:14px;color:black;" CssClass="textBox textAutoSelect txtQty"  Width="80px" align="right" Text='<%# Bind("REJECTION_QTY") %>'  onkeypress=" return isNumberKey(event,this);"></asp:TextBox>
                                                                 </td>
                                                                
                                                                 <%-- <td> 
                                                                    <asp:LinkButton ID="lnkItemdetail"  CommandName="itemdtl"  CssClass="buttoncommon"  CommandArgument='<%# Bind("ITEM_ID") %>' runat="server" CausesValidation="false" Width="52px" Height="17" >Batch</asp:LinkButton>  
                                                                  </td>--%>

                                                                <td>
                                                                    <asp:TextBox ID="txtREJECTION_DET_REMARKS" runat="server" CssClass="textBox textAutoSelect" Width="150px" Text='<%# Bind("REJECTION_DET_REMARKS") %>' Style=""></asp:TextBox>
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

          <div id="myModalItemDetails" class="modal fade" >  
            <div class="modal-dialog" style="max-width:600px; align-content:space-around;"> <%-- --%>
                <div class="modal-content" style="max-width:600px;">  
                    <div class="modal-header" style="max-width:600px;">  
                        
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>  
                        <h4 class="modal-title">Item Details</h4>  
                    </div> 
                    <div class="col-md-12">
                        <asp:Label ID="lblItemName" runat="server" Text="Item Name : " Visible="true" ></asp:Label>
                        <asp:Label ID="lblItemNameText" runat="server" Visible="true"  Font-Bold="true" ></asp:Label>
                    </div> 
                    <div class="modal-body" style="overflow-y:scroll; overflow-x:scroll; max-height:300px; max-width:600px; margin-top: 10px; margin-bottom: 10px;"> <%-- --%>
                        <asp:Label ID="Label8" runat="server" ClientIDMode="Static" Visible="false"></asp:Label>  
                    
                        <asp:HiddenField ID="hdnBatchItemId" runat="server" />
                         <asp:HiddenField ID="hdnBatchMachineId" runat="server" />
                         <asp:HiddenField ID="hdnIssueStock" runat="server" />
                         <asp:HiddenField ID="hdnEditMode" runat="server" />
                 
                       <asp:GridView ID="grdItemDetails" runat="server" HeaderStyle-BackColor="#3AC0F2" HeaderStyle-ForeColor="Black"
            RowStyle-BackColor="White" AlternatingRowStyle-BackColor="#A1DCF2" AlternatingRowStyle-ForeColor="#000"
            BorderStyle="None" BorderWidth="5px" CellPadding="10"  GridLines="Vertical" CssClass="gridView"
            AutoGenerateColumns="false">
            <Columns>
                 <asp:TemplateField HeaderText="SELECT">  
                    <ItemTemplate> 
                       <asp:CheckBox ID="chkSelect" runat="server" Width="52px" onclick="CalculateBatchStock(this)" Checked="false"  />  
                     </ItemTemplate> 
                   <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" /> 
                 </asp:TemplateField>  
                 <asp:TemplateField HeaderText="SL" ItemStyle-Width="30px">   
                     <ItemTemplate>
                         <asp:Label runat="server" ID="lblBatchSL" Text=" <%# Container.DataItemIndex + 1 %> "></asp:Label>
                              
                          <asp:HiddenField ID="hdnBatchNo" runat="server" Value='<%# Bind("PROD_BATCH_NO") %>' /> 
                          <asp:HiddenField ID="hdnOpeningQty" runat="server" Value='<%# Bind("SYSTEM_OPENING_STOCK") %>' /> 
                     </ItemTemplate>
                 </asp:TemplateField>
                <asp:TemplateField HeaderText="Batch">
                    <ItemTemplate>
                        <asp:Label ID="txtProdBatchNo" runat="server" CssClass="" Text='<%# Bind("PROD_BATCH_NO") %>' Width="80px" ReadOnly="true"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="Stock">
                    <ItemTemplate>
                       <asp:TextBox ID="txtSysOpening" runat="server" CssClass="form-control" Text='<%# Bind("SYSTEM_OPENING_STOCK") %>' Width="80px" ReadOnly="true" ></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                      <asp:TemplateField HeaderText="UOM">
                    <ItemTemplate>
                        <asp:Label ID="txtClsUom" runat="server" CssClass="" Text='<%# Bind("CLOSING_UOM_NAME") %>' Width="50px" ></asp:Label>
                        <asp:HiddenField runat="server" ID="hdnClsUomId" Value='<%# Bind("CLOSING_UOM_ID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Item Qty">
                    <ItemTemplate>
                        <asp:TextBox ID="txtusedQty" runat="server" CssClass="form-control" Text='<%# Bind("USED_QTY") %>' Width="100px" onBlur=" return ValidateBatchStock()" onkeypress=" return isNumberKey(event,this);"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>

               <%--     <asp:TemplateField HeaderText="Total Qty">
                    <ItemTemplate>
                        <asp:TextBox ID="txtTotIssueQty" runat="server" CssClass="form-control" Text='<%# Bind("ISSUE_STOCK") %>' Width="100px" ReadOnly="true"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                
          
            </Columns>
        </asp:GridView>
 
                    </div>  
                
                    <div class="modal-footer"> 
                        <table>
                            <tr>
                        <td>
                         <asp:Button ID="btnStkAdd" runat="server" Text="Add" CssClass="buttonSave" OnClientClick="return ValidateBatchStock();"   OnClick="btnStkAdd_Click" Visible="true" />
                         <button type="button"  Class="buttonClose"  data-dismiss="modal">Close</button> 
                          
                             </td>
                                </tr>
                          </table> 
                    </div>  
                </div>  
            </div>  
        </div>  


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
                                                   <%-- <td align="right">
                                                        <asp:Label ID="lbltotalSalesAmount" runat="server" Text="Total Qnty:" Font-Bold="True" Visible="true"></asp:Label>
                                                    </td>
                                                    <td align="right">
                                                        <asp:TextBox ID="txtTotQty" runat="server"   style="text-align: right;" CssClass="textBox"
                                                            Width="100" TabIndex="-1" Font-Bold="True" Visible="true"></asp:TextBox>
                                                    </td>
                                                     <td align="right"  Width="5px">
                                                         &nbsp;</td>
                                                    <td align="right">&nbsp;
                                                        <asp:TextBox ID="txtTotWt" runat="server" CssClass="textBox"  style="text-align: right;"
                                                            Width="100" TabIndex="-1" Font-Bold="True" Visible="true"></asp:TextBox>
                                                    </td>--%>

                                                </tr>
                                            </table>
                                        </div>
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
                       <asp:Button ID="btnAuthorize" runat="server" Text="Authorize" CssClass="buttoncommon" OnClientClick="if ( ! UserAuthorizeConfirmation()) return false;" OnClick="btnAuthorize_Click" Visible="true" />
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
