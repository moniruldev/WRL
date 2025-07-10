<%@ Page Title="" Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="ProductionPastingEntry.aspx.cs" Inherits="PG.Web.Production.ProductionPastingEntry" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../javascript/jquery.ui.combogrid.js" type="text/javascript"></script>
    <script src="../javascript/jquery.attributeobserver.js" type="text/javascript"></script>
    <link href="../css/jquery.ui.combogrid.css" rel="stylesheet" type="text/css" />
     <script language="javascript" type="text/javascript">
         var isPageResize = true;
         ContentForm.CalendarImageURL = "../image/calendar.png";

         var hdnCompanyID = '<%=hdnCompanyID.ClientID%>';
         var ddlDEPT_ID = '<%=ddlDEPT_ID.ClientID%>';
         var ItemGroupListServiceLink = '<%=this.ItemGroupListServiceLink%>';


         var ItemListServiceLink = '<%=this.ItemListServiceLink%>';
         var BOMItemListServiceLink = '<%=this.BOMItemListServiceLink%>';

         var BOMNameListServiceLink = '<%=this.BOMItemNameListServiceLink%>';

         var BOMListServiceLink = '<%= this.BOMListServiceLink%>';
         var PanelUOMServiceLink = '<%= this.PanelUOMServiceLink %>';
         var MachineListServiceLink = '<%= this.MachineListServiceLink%>';
         var SupporvisorListServiceLink = '<%= this.SupporvisorListServiceLink%>';
         

         var gridUpdatePanelIDDet = '<%=UpdatePanel1.ClientID%>';
         var gridViewIDDet = '<%=GRDDTLITEMLIST.ClientID%>';
         var updateProgressID = '<%=UpdateProgress2.ClientID%>';


         var gridClosingUpdatePanelIDDet = '<%=UpdatePanel2.ClientID%>';
         var gridViewClosingIDDet = '<%=grdClosingRowMaterial.ClientID%>';
         var updateClosingProgressID = '<%=UpdateProgress3.ClientID%>';


         var txtSUPERVISOR_NAME = '<%= txtSUPERVISOR_NAME.ClientID%>';

         var btnSUPERVISOR_ID = '<%=btnSUPERVISOR_ID.ClientID%>';
         var hdnSUPERVISOR_ID = '<%=hdnSUPERVISOR_ID.ClientID%>';  
         var hdnUsedGRID_ID = '<%=hdnUsedGRID_ID.ClientID%>'; 
         
         var txtTotalPanelQty = '<%=txtTotalPanelQty.ClientID%>';
         var txtPanelWeight = '<%=txtPanelWeight.ClientID%>';

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

         //$(document).on('blur', '.txtQty', function () {
         //    //var weight = $(this).closest('tr').find('.txtWeightQty').val();
         //    //alert(weight);
         //   // TestOnTextChange(this);
         //    sumGrandQty();

         //});

         //function sumGrandQty() {

         //    var totAdded = GetTotalSumAddedQty();
         //    $("#" + txtTotalPanelQty).val(JSUtility.FormatCurrency(totAdded));

         //    var totWeightAdded = GetTotalWeightQty();
         //    $("#" + txtPanelWeight).val(JSUtility.FormatCurrency(totWeightAdded));
         //}

         //function GetTotalSumAddedQty() {
         //    debugger;
         //    var totAdd = 0;
         //    var totalAmount = 0;
         //    var itemTotalWt = 0;


         //    $(document).find('.txtQty').each(function (index, elem) {
         //        // var weight = $(elem).closest('tr').find('.txtPcQty').val();             

         //        var addQty = parseFloat(JSUtility.GetNumber($(elem).val()));
         //        if (!isNaN(addQty)) {
         //            totAdd += addQty;

         //            //   itemTotalWt = itemTotalWt + (weight * addQty);
         //        }
         //    });
         //    $("#" + txtPanelWeight).val(JSUtility.FormatCurrency(itemTotalWt));
         //    return totAdd;
         //}

         //function GetTotalWeightQty() {
         //    debugger;
         //    var totAdd = 0;
         //    $(document).find('.txtWeightQty').each(function (index, elem) {
         //        var addQty = parseFloat(JSUtility.GetNumber($(elem).val()));
         //        if (!isNaN(addQty)) {
         //            totAdd += addQty;
         //        }
         //    });

         //    return totAdd;
         //}

         function ShowProgress() {
             $('#' + updateProgressID).show();
         }

         function ShowClosingProgress() {
             $('#' + updateClosingProgressID).show();
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



         $(document).ready(function () {

             var pageInstance = Sys.WebForms.PageRequestManager.getInstance();

             pageInstance.add_pageLoaded(function (sender, args) {
                 var panels = args.get_panelsUpdated();
                 for (i = 0; i < panels.length; i++) {
                     if (panels[i].id == gridUpdatePanelIDDet) {
                         bindItemList(gridViewIDDet);
                      //   bindItemGroupList(gridViewIDDet);
                         bindBOMItemList(gridViewIDDet);
                         bindPanelUOMList(gridViewIDDet);
                         bindWeightUOMList(gridViewIDDet);
                         bindMachineList(gridViewIDDet);
                         bindOperatorList(gridViewIDDet);
                         bindBARTYPEList(gridViewIDDet);

                     }


                     if (panels[i].id == gridClosingUpdatePanelIDDet) {
                         bindClosingItemList(gridViewClosingIDDet);
                     }
                 }
             });

             
           //  bindItemGroupList(gridViewIDDet);
             bindItemList(gridViewIDDet);
             bindBOMItemList(gridViewIDDet);
             bindPanelUOMList(gridViewIDDet);
             bindWeightUOMList(gridViewIDDet);
             bindMachineList(gridViewIDDet);
             bindOperatorList(gridViewIDDet);
             bindBARTYPEList(gridViewIDDet);
             bindSupporvisorList();


             bindClosingItemList(gridViewClosingIDDet);

         });



         function bindItemGroupList(gridViewID) {
             var cgColumns = [{ 'columnName': 'itemgroupdesc', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                              , { 'columnName': 'itemgroupcode', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Code' }
             ];

             var serviceURL = ItemGroupListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
             serviceURL += "&ispaging=1";
             var gridSelector = "#" + gridViewID;


             $(gridSelector).find('input[id$="txtGroupName"]').each(function (index, elem) {
                 var elemRow = $(elem).closest('tr.gridRow');

                 $(elem).closest('tr').find('input[id$="btnItemGroup"]').click(function (e) {
                     elmID = $(elem).attr('id');
                     $(elem).combogrid("dropdownClick");
                 });


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

                         var newServiceURL = JSUtility.AddTimeToQueryString(serviceURL);
                         $(this).combogrid("option", "url", newServiceURL);


                     },

                     select: function (event, ui) {
                         elemID = $(elem).attr('id');
                         if (!ui.item) {
                             event.preventDefault();
                             ClearItemGroupData(elemID);
                             return false;
                         }



                         if (ui.item.id == 0) {
                             event.preventDefault();
                             return false;
                         }
                         else {
                             SetItemGroupData(elemID, ui.item);
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
                         ClearItemGroupData(elemID);
                     }
                     else {

                         if (grp == null) {
                             ClearItemGroupData(elemID);
                         }
                         else {
                             SetItemGroupData(elemID, grp);
                         }
                     }
                     //setDetInstrument(hdnIsInstrumentElem, txtInstrumentElem, btnInstrumentElem);
                     grpID = $(self).closest('tr').find('input[id$="hdngroupId"]').val();
                     if (grpID == '0' | grpID == '') {
                         $(self).addClass('textError');
                     }

                 });

                 $(elem).blur(function () {
                     var self = this;

                     var seID = $(elem).val();
                     if (seID == '') {
                         $('#' + hdngroupId).val('0');
                         //  $('#' + txtExecutiveName).val('');
                     }
                 });
             });
         }

         function bindItemList(gridViewID) {
             var cgColumns = [
                                { 'columnName': 'itemname', 'width': '200', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                              , { 'columnName': 'uomname', 'width': '70', 'align': 'left', 'highlight': 4, 'label': 'Uom ' }
                            //  , { 'columnName': 'itemcode', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Code' }
                             // , { 'columnName': 'itemgroupdesc', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Group Name' }
                             //  , { 'columnName': 'item_type', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Item Type' }
                              //, { 'columnName': 'closing_qty', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Cls Qty' }
                              //, { 'columnName': 'class_name', 'width': '120', 'align': 'left', 'highlight': 4, 'label': 'Class Name' }
                              , { 'columnName': 'bomname', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'BOM Name' }
                              , { 'columnName': 'bomno', 'width': '160', 'align': 'left', 'highlight': 4, 'label': 'BOM NO' }
                              //, { 'columnName': 'uomid', 'width': '50', 'align': 'left', 'highlight': 4, 'label': 'Uom ID' }
                              // , { 'columnName': 'bomid', 'width': '50', 'align': 'left', 'highlight': 4, 'label': 'BOM ID' }


             ];
             var serviceURL = BOMNameListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
             //serviceURL += "&companyid=" + companyid;&deptid=135
             serviceURL += "&ispaging=1&isFinished=Y&for_production=Y&deptid=7";

             var gridSelector = "#" + gridViewID;
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
                         var vgroupid = $(elemRowCur).find('input[id$="hdngroupId"]').val();
                         var e = document.getElementById("<%=ddlDEPT_ID.ClientID%>");
                         var deptid = e.options[e.selectedIndex].value;
                         var newServiceURL = serviceURL;
                         //+ "&groupid=" + vgroupid + "&deptid=" + deptid;
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
                         var serviceURL = BOMNameListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
                         serviceURL += "&ispaging=1&isFinished=Y&for_production=Y";

                         var prcNo = GetItemNo(eCode, serviceURL);

                         if (prcNo == null) {
                             ClearItemData(elemID);
                         }
                         else {
                             SetItemData(elemID, grp);
                         }
                     }
                     //setDetInstrument(hdnIsInstrumentElem, txtInstrumentElem, btnInstrumentElem);
                     //grpID = $(self).closest('tr').find('input[id$="hdngroupId"]').val();
                     //if (grpID == '0' | grpID == '') {
                     //    $(self).addClass('textError');
                     //}

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

         function bindClosingItemList(gridViewIDD) {
             var cgColumns = [{ 'columnName': 'itemname', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                              , { 'columnName': 'uomname', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Uom Name' }
                              , { 'columnName': 'itemcode', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Code' }
                              , { 'columnName': 'closing_qty', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Cls Qty' }
                              , { 'columnName': 'itemgroupdesc', 'width': '120', 'align': 'left', 'highlight': 4, 'label': 'Group Name' }
                              , { 'columnName': 'class_name', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Class Name' }
                              //, { 'columnName': 'item_type', 'width': '50', 'align': 'left', 'highlight': 4, 'label': 'Item Type' }
             ];

             
             //var companyid = $('#' + hdnCompanyID).val();
             //var depthead = $('#' + hdnEmpCode).val();
             //var locationid = $('#' + ddlLocation).val();

             var serviceURL = ItemListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
             serviceURL += "&ispaging=1&forProduction=Y&deptid=7&isFinished=N";
             var gridSelector = "#" + gridViewIDD;
             $(gridSelector).find('input[id$="txtCLOSINGITEM_NAME"]').each(function (index, elem) {
                 ///list click
                 var elemRow = $(elem).closest('tr.gridRow');
                 var hdnItemIDElem = $(elemRow).find('input[id$="txtCLOSINGITEM_NAME"]');
                 //var prevGLCode = '';
                 $(elem).closest('tr').find('input[id$="btnCLOSINGITEM_NAME"]').click(function (e) {
                     elmID = $(elem).attr('id');
                     //$(elem).combogrid("show");
                     $(elem).combogrid("dropdownClick");
                 });

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
                     width: 800,
                     url: serviceURL,
                     search: function (event, ui) {
                         var elemRowCur = $(elem).closest('tr.gridRow');
                         var e = document.getElementById("<%=ddlDEPT_ID.ClientID%>");
                         var deptid = e.options[e.selectedIndex].value;
                         <%-- //var vgroupid =--%>
                         var newServiceURL = serviceURL ;//+ "&groupid=" + vgroupid+ "&deptid=" + deptid
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
                             ClearClosignItemData(elemID);
                             return false;
                             //ClearGLAccountData(elemID);
                         }
                         if (ui.item.id == 0) {
                             event.preventDefault();
                             return false;
                             //ClearGLAccountData(elemID);
                         }
                         else {
                             SetClosingItemData(elemID, ui.item);
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
                         ClearClosingItemData(elemID);
                     }
                     else {
                         var serviceURL = ItemListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
                         serviceURL += "&ispaging=1&for_production=Y";

                         var prcNo = GetItemNo(eCode, serviceURL);

                         if (prcNo == null) {
                             ClearClosingItemData(elemID);
                         }
                         else {
                             SetClosingItemData(elemID, grp);
                         }

                         if (grp == null) {
                             ClearClosingItemData(elemID);
                         }
                         else {
                             SetClosingItemData(elemID, grp);
                         }
                     }
                 });

             });

         }

         function ClearClosingItemData(txtCLOSINGITEM_NAME) {
             //$('#' + txtGLAccCodeID).val('');
             var detRow = $('#' + txtCLOSINGITEM_NAME).closest('tr.gridRow');
             $(detRow).find('input[id$="hdnCLOSING_ITEM_ID"]').val('0');
             $(detRow).find('input[id$="txtCLOSINGITEM_NAME"]').val('');
             $(detRow).find('input[id$="hdnCLOSING_UOM_ID"]').val('0');
             $(detRow).find('input[id$="txtCLOSING_UOM_NAME"]').val('');
             $(detRow).find('input[id$="txtSYSTEM_OPENING_STOCK"]').val('');
             $(detRow).find('input[id$="txtMANUAL_OPENING_STOCK"]').val('');

         }
         function SetClosingItemData(txtCLOSINGITEM_NAME, data) {
             //$('#' + hdnCLOSING_ITEM_ID).val(data.itemid);
             var detRow = $('#' + txtCLOSINGITEM_NAME).closest('tr.gridRow');
             $(detRow).find('input[id$="hdnCLOSING_ITEM_ID"]').val(data.itemid);
             $(detRow).find('input[id$="txtCLOSINGITEM_NAME"]').val(data.itemname);
             $(detRow).find('input[id$="hdnCLOSING_UOM_ID"]').val(data.uomid);
             $(detRow).find('input[id$="txtCLOSING_UOM_NAME"]').val(data.uomname);
             $(detRow).find('input[id$="txtSYSTEM_OPENING_STOCK"]').val(data.closing_qty);
             $(detRow).find('input[id$="txtMANUAL_OPENING_STOCK"]').val(data.closing_qty);
         }


         function ClearItemGroupData(txtItemGroupID) {
             //$('#' + txtGLAccCodeID).val('');
             var detRow = $('#' + txtItemGroupID).closest('tr.gridRow');
             $(detRow).find('input[id$="hdngroupId"]').val('0');
             //$(detRow).find('input[id$="txtGLGroupName"]').val('');

         }

         function SetItemGroupData(txtItemGroupCodeID, data) {
             $('#' + txtItemGroupCodeID).val(data.itemgroupid);

             var detRow = $('#' + txtItemGroupCodeID).closest('tr.gridRow');
             $(detRow).find('input[id$="hdngroupId"]').val(data.itemgroupid);
             $(detRow).find('input[id$="txtGroupName"]').val(data.itemgroupdesc);

         }


         function bindPanelUOMList(gridViewID) {
             var cgColumns = [
                                { 'columnName': 'uomcodeshort', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'UOM Short Name' }
                              , { 'columnName': 'uomname', 'width': '50', 'align': 'left', 'highlight': 4, 'label': 'UOM Name' }
                              , { 'columnName': 'pcqty', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Pc Qty' }

             ];
             var serviceURL = PanelUOMServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
             serviceURL += "&ispaging=1&isPanelUOM=10";

             var gridSelector = "#" + gridViewID;


             $(gridSelector).find('input[id$="txtPANEL_UOM_ID"]').each(function (index, elem) {
                 ///list click
                 var elemRow = $(elem).closest('tr.gridRow');

                 var hdnItemIDElem = $(elemRow).find('input[id$="txtPANEL_UOM_ID"]');

                 //var prevGLCode = '';

                 $(elem).closest('tr').find('input[id$="btnPANEL_UOM_ID"]').click(function (e) {
                     elmID = $(elem).attr('id');
                     //$(elem).combogrid("show");
                     $(elem).combogrid("dropdownClick");
                 });


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
                     width: 400,
                     url: serviceURL,
                     search: function (event, ui) {
                         //var elemRowCur = $(elem).closest('tr.gridRow');
                         //var ItemID = $(elemRowCur).find('input[id$="hdnItemID"]').val();
                         ////var vgroupid = $('#' + hdngroupId).val();+ "&groupid=" + vgroupid "&itemid=" + ItemID;
                         var newServiceURL = serviceURL;
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
                             ClearPanelUOMData(elemID);
                             return false;
                             //ClearGLAccountData(elemID);
                         }



                         if (ui.item.id == 0) {
                             event.preventDefault();
                             return false;
                             //ClearGLAccountData(elemID);
                         }
                         else {
                             SetPanelUOMData(elemID, ui.item);
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
                         ClearPanelUOMData(elemID);
                     }
                     else {
                         var serviceURL = PanelUOMServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
                         serviceURL += "&ispaging=1&isPanelUOM=10";

                         var prcNo = GetItemNo(eCode, serviceURL);

                         if (prcNo == null) {
                             ClearPanelUOMData(elemID);
                         }
                         else {
                             SetPanelUOMData(elemID, grp);
                         }
                     }
                     //setDetInstrument(hdnIsInstrumentElem, txtInstrumentElem, btnInstrumentElem);
                     //grpID = $(self).closest('tr').find('input[id$="hdngroupId"]').val();
                     //if (grpID == '0' | grpID == '') {
                     //    $(self).addClass('textError');
                     //}

                 });

             });

         }


         function bindWeightUOMList(gridViewID) {
             var cgColumns = [
                 //{ 'columnName': 'uomid', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'UOM ID' },
                               { 'columnName': 'uomcodeshort', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'UOM Short Name' }
                              , { 'columnName': 'uomname', 'width': '50', 'align': 'left', 'highlight': 4, 'label': 'UOM Name' }
                             // , { 'columnName': 'pcqty', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Pc Qty' }

             ];
             var serviceURL = PanelUOMServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
             serviceURL += "&ispaging=1&isPanelUOM=";

             var gridSelector = "#" + gridViewID;


             $(gridSelector).find('input[id$="txtWEIGHT_UOM_NAME"]').each(function (index, elem) {
                 ///list click
                 var elemRow = $(elem).closest('tr.gridRow');

                 var hdnItemIDElem = $(elemRow).find('input[id$="txtWEIGHT_UOM_NAME"]');

                 //var prevGLCode = '';

                 $(elem).closest('tr').find('input[id$="btnWEIGHT_UOM_ID"]').click(function (e) {
                     elmID = $(elem).attr('id');
                     //$(elem).combogrid("show");
                     $(elem).combogrid("dropdownClick");
                 });


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
                     width: 400,
                     url: serviceURL,
                     search: function (event, ui) {
                         //var elemRowCur = $(elem).closest('tr.gridRow');
                         //var ItemID = $(elemRowCur).find('input[id$="hdnItemID"]').val();
                         ////var vgroupid = $('#' + hdngroupId).val();+ "&groupid=" + vgroupid "&itemid=" + ItemID;
                         var newServiceURL = serviceURL;
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
                             ClearWeightUOMData(elemID);
                             return false;
                             //ClearGLAccountData(elemID);
                         }



                         if (ui.item.id == 0) {
                             event.preventDefault();
                             return false;
                             //ClearGLAccountData(elemID);
                         }
                         else {
                             SetWeightUOMData(elemID, ui.item);
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
                         ClearWeightUOMData(elemID);
                     }
                     else {
                         var serviceURL = PanelUOMServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
                         serviceURL += "&ispaging=1&isPanelUOM=";

                         var prcNo = GetItemNo(eCode, serviceURL);

                         if (prcNo == null) {
                             ClearWeightUOMData(elemID);
                         }
                         else {
                             SetWeightUOMData(elemID, grp);
                         }

                     }
                     //setDetInstrument(hdnIsInstrumentElem, txtInstrumentElem, btnInstrumentElem);
                     grpID = $(self).closest('tr').find('input[id$="hdngroupId"]').val();
                     if (grpID == '0' | grpID == '') {
                         $(self).addClass('textError');
                     }

                 });

             });

         }


         function bindBOMItemList(gridViewID) {
             var cgColumns = [{ 'columnName': 'bomid', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'BOM ID' }
                              , { 'columnName': 'bomitemdesc', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'BOM Name' }
                              , { 'columnName': 'bomitemid', 'width': '50', 'align': 'left', 'highlight': 4, 'label': 'Item ID' }
                              , { 'columnName': 'itemname', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Item Name' }
                              , { 'columnName': 'bomver', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'BOM Ver' }


             ];
             var serviceURL = BOMListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
             serviceURL += "&ispaging=1";
             // serviceURL += "&namecomptype=" + Enums.DataCompareType.Contains;


             var gridSelector = "#" + gridViewID;


             $(gridSelector).find('input[id$="txtBOM_Name"]').each(function (index, elem) {
                 ///list click
                 var elemRow = $(elem).closest('tr.gridRow');

                 var hdnItemIDElem = $(elemRow).find('input[id$="txtBOM_Name"]');

                 //var prevGLCode = '';

                 $(elem).closest('tr').find('input[id$="btnITEM_BOM_ID"]').click(function (e) {
                     elmID = $(elem).attr('id');
                     //$(elem).combogrid("show");
                     $(elem).combogrid("dropdownClick");
                 });


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
                         var ItemID = $(elemRowCur).find('input[id$="hdnItemID"]').val();
                         //var vgroupid = $('#' + hdngroupId).val();+ "&groupid=" + vgroupid
                         var newServiceURL = serviceURL + "&itemid=" + ItemID;
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
                             ClearBOMData(elemID);
                             return false;
                             //ClearGLAccountData(elemID);
                         }



                         if (ui.item.id == 0) {
                             event.preventDefault();
                             return false;
                             //ClearGLAccountData(elemID);
                         }
                         else {
                             SetBOMData(elemID, ui.item);
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
                         //                    if (!validateGLAccount(elemID, null)) {
                         //                        $(elem).val(prevGLCode);
                         //                        return false;
                         //                    }
                         //ClearItemGroupData(elemID);
                     }
                     else {

                         if (grp == null) {
                             ClearItemData(elemID);
                         }
                         else {
                             SetItemData(elemID, grp);
                         }
                     }
                     //setDetInstrument(hdnIsInstrumentElem, txtInstrumentElem, btnInstrumentElem);
                     grpID = $(self).closest('tr').find('input[id$="hdngroupId"]').val();
                     if (grpID == '0' | grpID == '') {
                         $(self).addClass('textError');
                     }

                 });

             });

         }

         function ClearItemData(txtItemID) {
             //$('#' + txtGLAccCodeID).val('');
             var detRow = $('#' + txtItemID).closest('tr.gridRow');
             $(detRow).find('input[id$="hdnItemId"]').val('0');
             $(detRow).find('input[id$="txtITEM_NAME"]').val('');
             sumGrandQty();
         }
         function SetItemData(txtItemCodeID, data) {
             $('#' + txtItemCodeID).val(data.itemid);

             var detRow = $('#' + txtItemCodeID).closest('tr.gridRow');
             $(detRow).find('input[id$="hdnItemID"]').val(data.itemid);
             $(detRow).find('input[id$="txtITEM_NAME"]').val(data.itemname);
             $(detRow).find('input[id$="hdnUsedGRIDID"]').val(data.usedgridid);
             $(detRow).find('input[id$="hdnUSEDGRIDNAME"]').val(data.usedgridname);

             $(detRow).find('input[id$="hdngroupId"]').val(data.itemgroupid);
             $(detRow).find('input[id$="txtGroupName"]').val(data.itemgroupdesc);
             $(detRow).find('input[id$="hdnUomID"]').val(data.uomid);
             $(detRow).find('input[id$="txtUOM_NAME"]').val(data.uomname);
             $(detRow).find('input[id$="txtPANEL_UOM_ID"]').val(data.paneluomname);
             $(detRow).find('input[id$="hdnPANEL_UOM_ID"]').val(data.paneluomid);
             $(detRow).find('input[id$="hdnPANEL_PC"]').val(data.panelpc);
             $(detRow).find('input[id$="txtBOM_Name"]').val(data.bomname);
             $(detRow).find('input[id$="hdnITEM_BOM_ID"]').val(data.bomid);

             $(detRow).find('input[id$="txtITEM_STANDARD_WEIGHT_KG"]').val(data.paste_panel_kg);
             $(detRow).find('input[id$="hdnITEM_STANDARD_WEIGHT_KG"]').val(data.paste_panel_kg);

             $(detRow).find('input[id$="txtWEIGHT_UOM_NAME"]').val('kg');
             $(detRow).find('input[id$="hndWEIGHT_UOM_ID"]').val('2');

            // $(detRow).find('input[id$="hdnUomID"]').val(data.isprime);
             // bomno
             var pcqty = $(detRow).find('input[id$="hdnPANEL_PC"]').val();

             var panelqty = $(detRow).find('input[id$="txtITEM_PANEL_QTY"]').val();
             var tpcqty = pcqty * panelqty;
             $(detRow).find('input[id$="txtItem_qty"]').val(tpcqty);


             var standardweight = $(detRow).find('input[id$="hdnITEM_STANDARD_WEIGHT_KG"]').val();
             var tweight = panelqty * standardweight;
             $(detRow).find('input[id$="txtITEM_WEIGHT"]').val(tweight);
             //sumGrandQty();
             if(panelqty>0)
                 $("[id*=btnAddAutoUsedItem]").click();

         }


         function SetBOMData(txtBOM_ITEM_DESC, data) {
             $('#' + txtBOM_ITEM_DESC).val(data.bomitemdesc);

             var detRow = $('#' + txtBOM_ITEM_DESC).closest('tr.gridRow');
             $(detRow).find('input[id$="hdnITEM_BOM_ID"]').val(data.bomid);
         }
         function ClearBOMData(txtBOM_ITEM_DESC) {
             var detRow = $('#' + txtBOM_ITEM_DESC).closest('tr.gridRow');
             $(detRow).find('input[id$="hdnITEM_BOM_ID"]').val('0');
             $(detRow).find('input[id$="txtBOM_ITEM_DESC"]').val('');

         }



         function SetPanelUOMData(txtPANEL_UOM_ID, data) {
             $('#' + txtPANEL_UOM_ID).val(data.uomname);

             var detRow = $('#' + txtPANEL_UOM_ID).closest('tr.gridRow');
             $(detRow).find('input[id$="hdnPANEL_UOM_ID"]').val(data.uomid);
             $(detRow).find('input[id$="hdnPANEL_PC"]').val(data.pcqty);
             var panelqty = $(detRow).find('input[id$="txtITEM_PANEL_QTY"]').val();
             var pcqty = data.pcqty * panelqty;

             $(detRow).find('input[id$="txtItem_qty"]').val(pcqty);
             var useditemname = $(detRow).find('input[id$="txtUSED_ITEM_NAME"]').val();
             if (useditemname != "")
                 $("[id*=btnAddAutoUsedItem]").click();
         }
         function ClearPanelUOMData(txtPANEL_UOM_ID) {
             var detRow = $('#' + txtPANEL_UOM_ID).closest('tr.gridRow');
             $(detRow).find('input[id$="hdnPANEL_UOM_ID"]').val('0');
             $(detRow).find('input[id$="txtPANEL_UOM_ID"]').val('');
             $(detRow).find('input[id$="hdnPANEL_PC"]').val(0);

         }


         function SetWeightUOMData(txtWEIGHT_UOM_NAME, data) {
             $('#' + txtWEIGHT_UOM_NAME).val(data.uomcodeshort);
             var detRow = $('#' + txtWEIGHT_UOM_NAME).closest('tr.gridRow');
             $(detRow).find('input[id$="hndWEIGHT_UOM_ID"]').val(data.uomid);
             
         }
         function ClearWeightUOMData(txtWEIGHT_UOM_NAME) {
             var detRow = $('#' + txtWEIGHT_UOM_NAME).closest('tr.gridRow');
             $(detRow).find('input[id$="hndWEIGHT_UOM_ID"]').val('0');
             $(detRow).find('input[id$="txtWEIGHT_UOM_NAME"]').val('');
             

         }

         

         function TestOnTextChange(texbx) {
             
             
             var detRow = $(texbx).closest('tr.gridRow');
             var pcqty = $(detRow).find('input[id$="hdnPANEL_PC"]').val();

             var panelqty =  $(detRow).find('input[id$="txtITEM_PANEL_QTY"]').val();
             var tpcqty = pcqty * panelqty;
             $(detRow).find('input[id$="txtItem_qty"]').val(tpcqty);

             var usedgridid= $(detRow).find('input[id$="hdnUsedGRIDID"]').val();
             $('#' + hdnUsedGRID_ID).val(usedgridid);

             var standardweight = $(detRow).find('input[id$="hdnITEM_STANDARD_WEIGHT_KG"]').val();
             var tweight = panelqty * standardweight;
             $(detRow).find('input[id$="txtITEM_WEIGHT"]').val(tweight);

             var useditemname = $(detRow).find('input[id$="txtUSED_ITEM_NAME"]').val();
             if (useditemname != "")
                 $("[id*=btnAddAutoUsedItem]").click();

         }


         function calcIssueStock(txtbox)
         {
             var detRow = $(txtbox).closest('tr.gridRow');
             var SYSTEM_OPENING_STOCK = $(detRow).find('input[id$="txtSYSTEM_OPENING_STOCK"]').val();
             var CLOSING_QTY = $(detRow).find('input[id$="txtCLOSING_QTY"]').val();

             var ISSUE_QTY = SYSTEM_OPENING_STOCK - CLOSING_QTY;
             $(detRow).find('input[id$="txtISSUE_STOCK"]').val(ISSUE_QTY);
         }

         function calcCloseingStock(txtbox) {
             var detRow = $(txtbox).closest('tr.gridRow');
             var SYSTEM_OPENING_STOCK = $(detRow).find('input[id$="txtSYSTEM_OPENING_STOCK"]').val();
             var ISSUE_QTY = $(detRow).find('input[id$="txtISSUE_STOCK"]').val();

             var CLOSING_QTY = SYSTEM_OPENING_STOCK - ISSUE_QTY;
           
             $(detRow).find('input[id$="txtCLOSING_QTY"]').val(CLOSING_QTY);
         }


         function CalcBarWeight(texbx) {


             var detRow = $(texbx).closest('tr.gridRow');
             var  weight = $(detRow).find('input[id$="hdnBAR_WEIGHT"]').val();

             var pcqty = $(detRow).find('input[id$="txtUSED_BAR_PC"]').val();
             var tpcqty = pcqty * weight;
             $(detRow).find('input[id$="txtUSED_QTY_KG"]').val(tpcqty);
         }

         function bindMachineList(gridViewID) {
             var cgColumns = [{ 'columnName': 'machineid', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Machine ID' }
                              , { 'columnName': 'machinename', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Machine Name' }
                              , { 'columnName': 'machinedescription', 'width': '200', 'align': 'left', 'highlight': 4, 'label': 'Machine Description' }

             ];
             var serviceURL = MachineListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
             serviceURL += "&ispaging=0";

             var gridSelector = "#" + gridViewID;

             $(gridSelector).find('input[id$="txtMACHINE_NAME"]').each(function (index, elem) {
                 ///list click
                 var elemRow = $(elem).closest('tr.gridRow');

                 var hdnItemIDElem = $(elemRow).find('input[id$="txtMACHINE_NAME"]');

                 //var prevGLCode = '';

                 $(elem).closest('tr').find('input[id$="btnMACHINE_NAME"]').click(function (e) {
                     elmID = $(elem).attr('id');
                     //$(elem).combogrid("show");
                     $(elem).combogrid("dropdownClick");
                 });


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
                     width: 400,
                     url: serviceURL,
                     search: function (event, ui) {
                         var elemRowCur = $(elem).closest('tr.gridRow');
                         var e = document.getElementById("<%=ddlDEPT_ID.ClientID%>");  
                         var dept_id = e.options[e.selectedIndex].value;
                        
                         var newServiceURL = serviceURL + "&deptid=" + dept_id;
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
                             ClearMachineData(elemID);
                             return false;
                             //ClearGLAccountData(elemID);
                         }



                         if (ui.item.id == 0) {
                             event.preventDefault();
                             return false;
                             //ClearGLAccountData(elemID);
                         }
                         else {
                             SetMachineData(elemID, ui.item);
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
                         ClearMachineData(elemID);
                     }
                     else {
                         var e = document.getElementById("<%=ddlDEPT_ID.ClientID%>");
                         var dept_id = e.options[e.selectedIndex].value;
                         var serviceURL = MachineListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
                         serviceURL += "&ispaging=0&deptid=" + dept_id;

                         var prcNo = GetItemNo(eCode, serviceURL);

                         if (prcNo == null) {
                             ClearMachineData(elemID);
                         }
                         else {
                             SetMachineData(elemID, grp);
                         }
                     }
                     //setDetInstrument(hdnIsInstrumentElem, txtInstrumentElem, btnInstrumentElem);
                     grpID = $(self).closest('tr').find('input[id$="hndMACHINE_ID"]').val();
                     if (grpID == '0' | grpID == '') {
                         $(self).addClass('textError');
                     }

                 });

             });

         }


         function SetMachineData(txtMACHINE_NAME, data) {
             $('#' + txtMACHINE_NAME).val(data.machinename);
             var detRow = $('#' + txtMACHINE_NAME).closest('tr.gridRow');
             $(detRow).find('input[id$="hndMACHINE_ID"]').val(data.machineid);

         }
         function ClearMachineData(txtMACHINE_NAME) {
             var detRow = $('#' + txtMACHINE_NAME).closest('tr.gridRow');
             $(detRow).find('input[id$="hndMACHINE_ID"]').val('0');
             $(detRow).find('input[id$="txtMACHINE_NAME"]').val('');


         }



         function bindSupporvisorList() {

             var cgColumns = [{ 'columnName': 'empid', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Emp ID' }
                              , { 'columnName': 'fullname', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Emp Name' }
                              , { 'columnName': 'designationName', 'width': '200', 'align': 'left', 'highlight': 4, 'label': ' Designation Name' }

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
                 width: 400,
                 url: serviceURL,
                 search: function (event, ui) {
                     var e = document.getElementById("<%=ddlDEPT_ID.ClientID%>");
                     var dept_id = e.options[e.selectedIndex].value;
                     var newServiceURL = serviceURL + "&deptid=" + dept_id;
                    
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

                 var prcNo = GetItemNo(eCode, serviceURL);

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



         function bindOperatorList(gridViewID) {
             var cgColumns = [{ 'columnName': 'empid', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Emp ID' }
                              , { 'columnName': 'fullname', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Emp Name' }
                              , { 'columnName': 'designationName', 'width': '200', 'align': 'left', 'highlight': 4, 'label': ' Designation Name' }

             ];
             var serviceURL = SupporvisorListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
             serviceURL += "&ispaging=0&isOperator=1";

             var gridSelector = "#" + gridViewID;
             $(gridSelector).find('input[id$="txtOPERATOR_NAME"]').each(function (index, elem) {
                 ///list click
                 var elemRow = $(elem).closest('tr.gridRow');

                 var hdnItemIDElem = $(elemRow).find('input[id$="txtOPERATOR_NAME"]');

                 //var prevGLCode = '';

                 $(elem).closest('tr').find('input[id$="btnOPERATOR"]').click(function (e) {
                     elmID = $(elem).attr('id');
                     //$(elem).combogrid("show");
                     $(elem).combogrid("dropdownClick");
                 });


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
                     width: 400,
                     url: serviceURL,
                     search: function (event, ui) {
                         var elemRowCur = $(elem).closest('tr.gridRow');
                         var e = document.getElementById("<%=ddlDEPT_ID.ClientID%>");
                         var dept_id = e.options[e.selectedIndex].value;

                         var newServiceURL = serviceURL + "&deptid=" + dept_id;
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
                             ClearOperateData(elemID);
                             return false;
                             //ClearGLAccountData(elemID);
                         }



                         if (ui.item.id == 0) {
                             event.preventDefault();
                             return false;
                             //ClearGLAccountData(elemID);
                         }
                         else {
                             SetOperatorData(elemID, ui.item);
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
                         ClearOperatorData(elemID);
                     }
                     else {
                         var e = document.getElementById("<%=ddlDEPT_ID.ClientID%>");
                         var dept_id = e.options[e.selectedIndex].value;
                         var serviceURL = SupporvisorListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
                         serviceURL += "&ispaging=0&isOperator=1&deptid=" + dept_id;

                         var prcNo = GetItemNo(eCode, serviceURL);

                         if (prcNo == null) {
                             ClearOperatorData(elemID);
                         }
                         else {
                             SetOperatorData(elemID, grp);
                         }
                     }
                     //setDetInstrument(hdnIsInstrumentElem, txtInstrumentElem, btnInstrumentElem);
                     grpID = $(self).closest('tr').find('input[id$="hndMACHINE_ID"]').val();
                     if (grpID == '0' | grpID == '') {
                         $(self).addClass('textError');
                     }

                 });

             });

         }


         function SetOperatorData(txtOPERATOR_NAME, data) {
             $('#' + txtOPERATOR_NAME).val(data.fullname);
             var detRow = $('#' + txtOPERATOR_NAME).closest('tr.gridRow');
             $(detRow).find('input[id$="hdnOPERATOR_ID"]').val(data.empid);

         }
         function ClearOperatorData(txtOPERATOR_NAME) {
             var detRow = $('#' + txtOPERATOR_NAME).closest('tr.gridRow');
             $(detRow).find('input[id$="hdnOPERATOR_ID"]').val('0');
             $(detRow).find('input[id$="txtOPERATOR_NAME"]').val('');


         }




         function bindBARTYPEList(gridViewID) {
             var cgColumns = [{ 'columnName': 'uomid', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'UOM ID' }
                              , { 'columnName': 'uomcodeshort', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'UOM Short Name' }
                              , { 'columnName': 'uomname', 'width': '50', 'align': 'left', 'highlight': 4, 'label': 'UOM Name' }
                              , { 'columnName': 'pcqty', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Weight (Kg)' }

             ];
             var serviceURL = PanelUOMServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
             serviceURL += "&ispaging=1&isPanelUOM=11";

             var gridSelector = "#" + gridViewID;


             $(gridSelector).find('input[id$="txtBAR_TYPE"]').each(function (index, elem) {
                 ///list click
                 var elemRow = $(elem).closest('tr.gridRow');

                 var hdnItemIDElem = $(elemRow).find('input[id$="txtBAR_TYPE"]');

                 //var prevGLCode = '';

                 $(elem).closest('tr').find('input[id$="btnBAR_TYPE"]').click(function (e) {
                     elmID = $(elem).attr('id');
                     //$(elem).combogrid("show");
                     $(elem).combogrid("dropdownClick");
                 });


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
                     width: 400,
                     url: serviceURL,
                     search: function (event, ui) {
                         var newServiceURL = serviceURL;
                         newServiceURL = JSUtility.AddTimeToQueryString(newServiceURL);
                         $(this).combogrid("option", "url", newServiceURL);
                     },

                     select: function (event, ui) {
                         elemID = $(elem).attr('id');
                         //                    if (!validateGLAccount(elemID, ui.item)) {
                         //                        $(elem).val(prevGLCode);
                         //                        return false;
                         //                    }
                         if (!ui.item) {
                             event.preventDefault();
                             ClearBARUOMData(elemID);
                             return false;
                         }
                         if (ui.item.id == 0) {
                             event.preventDefault();
                             return false;
                         }
                         else {
                             SetBARUOMData(elemID, ui.item);
                         }
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
                         //                    if (!validateGLAccount(elemID, null)) {
                         //                        $(elem).val(prevGLCode);
                         //                        return false;
                         //                    }
                         //ClearItemGroupData(elemID);
                     }
                     else {

                         if (grp == null) {
                             ClearBARUOMData(elemID);
                         }
                         else {
                             SetBARUOMData(elemID, grp);
                         }
                     }
                 });

             });

         }

         function SetBARUOMData(txtBAR_TYPE, data) {
             $('#' + txtBAR_TYPE).val(data.uomname);
             var detRow = $('#' + txtBAR_TYPE).closest('tr.gridRow');
             $(detRow).find('input[id$="hdnBAR_TYPE"]').val(data.uomid);
             $(detRow).find('input[id$="hdnBAR_WEIGHT"]').val(data.pcqty);

             var barqty = $(detRow).find('input[id$="txtUSED_BAR_PC"]').val();
             var pcqty = data.pcqty * barqty;
             $(detRow).find('input[id$="txtUSED_QTY_KG"]').val(pcqty);
         }
         function ClearBARUOMData(txtBAR_TYPE) {
             var detRow = $('#' + txtBAR_TYPE).closest('tr.gridRow');
             $(detRow).find('input[id$="hdnBAR_TYPE"]').val('0');
             $(detRow).find('input[id$="txtBAR_TYPE"]').val('');
             $(detRow).find('input[id$="hdnBAR_WEIGHT"]').val('0');

         }
           </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div id="dvPageContent" style="width: 100%; height: 100%;">
         <asp:HiddenField ID="hdnLoggedInUser" runat="server" />
         <asp:HiddenField ID="hdnCompanyID" runat="server" Value="0" />
           <asp:HiddenField ID="hdnUsedGRID_ID" runat="server" Value="0" />
          <asp:Button ID="btnAddAutoUsedItem" runat="server" Style="display : none;" OnClick="btnAddAutoUsedItem_Click"  />
          <div id="dvContentHeader" class="dvContentHeader">
            <div id="dvHeader" class="dvHeader_Prod" align="left">
                <asp:Label ID="lblHeader" runat="server" Text="Production Pasting Entry" CssClass="lblHeader" Font-Bold="true" Font-Size="15px"></asp:Label>
            </div>
            <div id="dvMessage" class="dvMessage">
                <asp:Label ID="lblMessage" runat="server" CssClass="lblMessage" Text="" Width="100%"></asp:Label>
            </div>

            <div id="dvHeaderControl" class="dvHeaderControl">
            </div>

        </div>

           <div id="dvContentMain" class="dvContentMain" align="left">
                 <div id="dvControlsHead" style="height: auto; width: 100%;" align="left">
                      <table border="0" cellspacing="4" cellpadding="2" align="left" style="width: 90%" id="tblProductionMstEntry">
                          <tr>
                              <td style="text-align: right">
                                  <asp:Label ID="lblPROD_CODE" runat="server" Text="Prod. NO : "></asp:Label>
                              </td>
                              <td style="text-align: left">
                                  <asp:TextBox ID="txtPROD_NO" runat="server" CssClass="colourdisabletextBox" Enabled="false" Width="163px"></asp:TextBox>
                              </td>
                              <td style="text-align: right">
                                  <asp:Label ID="lblDEPT_ID" runat="server" Text="Department : "></asp:Label>
                              </td>
                              <td style="text-align: left">

                                  <asp:DropDownList ID="ddlDEPT_ID" runat="server" CssClass="dropDownList required" Width="190px" ></asp:DropDownList>
                              </td>
                              <td style="text-align: right"> 
                                  <asp:Label ID="lblSUPERVISOR_ID" runat="server" Text="Supervisor : "></asp:Label>
                              </td>
                              <td style="text-align: left">
                                  <asp:TextBox ID="txtSUPERVISOR_NAME" runat="server" CssClass="textBox textAutoSelect" Width="150px" autofocus></asp:TextBox>
                                 <input id="btnSUPERVISOR_ID" type="button" value="" runat="server" class="buttonDropdown" style="width: 15px" tabindex="-1" />
                              </td>
                              <td> 
                                  &nbsp;</td>
                              <td style="text-align: left"> 
                                  <asp:HiddenField ID="hdnPROD_ID" runat="server" />
                              </td>


                          </tr>
                          <tr>
                              <td style="text-align: right">
                                  <asp:Label ID="lblREFNOMANUAL" runat="server" Text="Ref. No : "></asp:Label>
                              </td>
                              <td style="text-align: left">
                                  <asp:TextBox ID="txtREF_NO_MANUAL" runat="server" CssClass="textBox" Width="163px"></asp:TextBox>
                              </td>
                              <td style="text-align: right">
                                  <asp:Label ID="lblFORECUSTID" runat="server" Text="Forecust Month : "></asp:Label>
                              </td>
                              <td>
                                  <asp:TextBox ID="txtFORECUSTMONTH" runat="server" Style=" display : none" CssClass="colourdisabletextBox"  Enabled="false" Width="163px" ></asp:TextBox>
                                  <asp:DropDownList ID="ddlFORECUSTMONTH" CssClass="dropDownList required" runat="server"  Width="190px"></asp:DropDownList>
                              </td>
                              <td style="text-align: right">
                                  <asp:Label ID="lblSHIFT_ID" runat="server" Text="Shift : "></asp:Label>
                              </td>
                              <td style="text-align: left">
                                  <asp:DropDownList ID="ddlSHIFT_ID" runat="server" CssClass="dropDownList" Width="155px" AutoPostBack="True" OnSelectedIndexChanged="ddlSHIFT_ID_SelectedIndexChanged"></asp:DropDownList>

                              </td>
                               <td></td>
                              <td>
                                  <asp:HiddenField ID="hdnSUPERVISOR_ID" runat="server"  />
                              </td>
                          </tr>
                          <tr>
                              <td style="text-align: right">Batch Start Time : </td>
                              <td><asp:TextBox ID="txtBATCH_STARTTIME" runat="server" CssClass="textBox textDate dateParse" Style="text-align: left;" Width="75px"></asp:TextBox>
                                <asp:TextBox  ID="txtBATCH_STARTTIMEs"  runat="server" CssClass="textBox" Width="60px">  </asp:TextBox>
                                <cc1:MaskedEditExtender id="MaskedEditExtender2" runat="server" acceptampm="true"  mask="99:99" masktype="Time"   messagevalidatortip="true"  targetcontrolid="txtBATCH_STARTTIMEs" autocompletevalue="00:00 AM"></cc1:MaskedEditExtender>
                              </td>
                              <td style="text-align: right">Batch End Time : </td>
                              <td><asp:TextBox ID="txtBATCH_ENDTIME" runat="server" CssClass="textBox textDate dateParse" Style="text-align: left;" Width="100px"></asp:TextBox>
                                  <asp:TextBox  ID="txtBATCH_ENDTIMEs"   runat="server" CssClass="textBox" Width="60px">  </asp:TextBox>
                                 <cc1:MaskedEditExtender id="MaskedEditExtender1" runat="server" acceptampm="true"  mask="99:99" masktype="Time"   messagevalidatortip="true"  targetcontrolid="txtBATCH_ENDTIMEs" autocompletevalue="00:00 AM"></cc1:MaskedEditExtender>

                              </td>
                              <td style="text-align: right">
                                  <asp:Label ID="lblREJECTED_QTY" runat="server" Visible="false"  Text="Rejected Qty :"></asp:Label>
                              </td>
                              <td>
                                  <asp:TextBox ID="txtREJECTED_QTY" runat="server" CssClass="textBox textNumberOnly" Width="150px" Visible="false" onkeypress=" return isNumberKey(event,this);"></asp:TextBox>
                              </td>
                              <td></td>
                              <td>
                                  <asp:HiddenField ID="hndFORECUSTID" runat="server" />
                              </td>

                          </tr>
                          <tr>
                              <td style="text-align: right"> 
                                  <asp:Label ID="lblProcessCode" runat="server"  Text="Batch NO :"></asp:Label>
                              </td>
                              <td>
                                  <asp:TextBox ID="txtPROD_BATCH_NO" runat="server" style="text-transform: uppercase" CssClass="textBox textNumberOnly" Width="163px"></asp:TextBox>
                                 <%-- <asp:TextBox ID="txtPROCESS_CODE" runat="server" CssClass="textBox textNumberOnly" Width="163px" onkeypress=" return isNumberKey(event,this);"></asp:TextBox>--%>
                              </td>
                              <td style="text-align: right">
                                  <asp:Label ID="lblPRODUCTION_DATE" runat="server"  Text="Production Date :"></asp:Label>
                              </td>
                              <td>
                                  <asp:TextBox ID="txtPRODUCTION_DATE" runat="server" CssClass="textBox textDate dateParse" Style="text-align: left;" Width="100px"></asp:TextBox>
                              </td>
                              <td style="text-align: right">
                                  <asp:Label ID="lblBatch" runat="server" Text="Sys. Batch : " Visible="false" ></asp:Label>
                              </td>
                              <td>
                                   <asp:TextBox ID="txtBATCH_ID" runat="server" CssClass="textBox" Style="text-align: left; display :none" Width="150px"></asp:TextBox>
                              </td>
                              <td></td>
                              <td></td>
                          </tr>
                          </table>
                     </div>

               <div id="dvControls" style="height: auto; width: 100%; ">
 
            <div id="groupDataDetails" style="width: 90%; height: auto;">
                <div id="dvGridSeparator" runat="server" style="width: 100%;">
                </div>
                <div id="groupDataHeaderCredit" runat="server" class="" style="float: right;width: 100%; text-align: left; border-top : solid 1px #0b07f5;">
                    <span style="font-weight: bold; font-size : 15px;color :#ff3b00;">Production Items Details: </span>
                </div>
                <div id="dvGridContainer2" runat="server" class="" style="width: auto; height: auto; text-align: left">
                    <div id="dvGridHeader2" style="float:left; width:1100px;   font-size: smaller;" class="subHeader_Prod">
                        <table style="height: 100%;  color: #FFFFFF; font-weight: bold; text-align: left;"
                            class="defFont" cellspacing="1" cellpadding="1">
                            <tr class="headerRow_Prod">
                                <td width="52px" class="headerColCenter">SL#
                                </td>
                                <%--  <td width="140px" class="headerColCenter">Item Group
                                </td>--%>
                                <td width="80px" class="headerColCenter_prod"> MC. Name
                                </td>
                                <td width="150px" class="headerColCenter_prod">Item
                                </td>
                                 <td width="15px" class="headerColCenter_prod"></td>
                                <td width="55px" class="headerColCenter_prod">Panel Qty
                                </td>
                                 <td width="62px" class="headerColCenter_prod">Panel UOM
                                </td>
                               <%-- <td width="15px" class="headerColLeft"></td>--%>
                                <td width="80px" class="headerColCenter_prod">Item (Pcs) Qty
                                </td>
                                <td width="50px" class="headerColCenter_prod">UOM
                                </td>
                                <td width="70px" class="headerColCenter_prod">STD Paste Wt.
                                            </td>
                                <td width="76px" class="headerColCenter_prod">Tt. Paste Wt.
                                </td>
                                <td width="75px" class="headerColCenter_prod">  Wt. UOM
                                </td>
                                 <td width="10px" class="headerColCenter_prod">
                                </td>
                                
                                <td width="116px" class="headerColCenter_prod"> Operator
                                </td>

                                <%--<td width="70px" class="headerColCenter"> Used Bar(Pcs)
                                </td>
                                 <td width="10px" class="headerColCenter">   
                                </td>--%>
                                  <td width="60px" class="headerColCenter_prod"> Grid Batch
                                </td>

                                   <%-- <td width="150px" class="headerColCenter">ITEM BOM
                                </td>
                               <td width="10px" class="headerColLeft"></td>--%>
                                <td width="140px" class="headerColCenter_prod">Remarks
                                </td>
                                <td width="16px" class="headerColCenter_prod">Delete
                                </td>
                                
                            </tr>
                        </table>
                    </div>
                    <div id="dvGrid" style="width: 1100px; height: 150px; overflow: auto;" class="dvGrid">
                      <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                                <asp:GridView ID="GRDDTLITEMLIST" runat="server" AutoGenerateColumns="False" ShowHeader="False" 
                                    CellPadding="1" CellSpacing="1" ForeColor="#333333" GridLines="Both" DataKeyNames="ITEM_ID" 
                                    EnableModelValidation="True" ClientIDMode="AutoID" OnRowCommand="GRDDTLITEMLIST_RowCommand" OnRowCreated="GRDDTLITEMLIST_RowCreated" OnRowDataBound="GRDDTLITEMLIST_RowDataBound" OnRowDeleting="GRDDTLITEMLIST_RowDeleting"  >
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <Columns>
                                       

                                        <asp:TemplateField HeaderText="SL#">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSLNo" runat="server" Text='<%# Bind("SLNO") %>' Style="text-align: center;"
                                                    Width="50px">
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
                                                                <%--<td>
                                                                    <asp:TextBox ID="txtGroupName" runat="server" CssClass="textBox textAutoSelect" Width="123" Text='<%# Bind("ITEM_GROUP_DESC") %>'></asp:TextBox>
                                                                    <asp:HiddenField ID="hdngroupId" runat="server" Value='<%# Bind("ITEM_GROUP_ID") %>' />
                                                                </td>
                                                                <td>
                                                                    <input id="btnItemGroup" type="button" value="" runat="server" class="buttonDropdown" tabindex="-1" />
                                                                </td>--%>

                                                                  <td>
                                                                     <asp:TextBox ID="txtMACHINE_NAME" runat="server" CssClass="textBox textAutoSelect" Width="60px" Text='<%#Bind("MACHINE_NAME") %>'></asp:TextBox>
                                                                     <asp:HiddenField ID="hndMACHINE_ID" runat="server" Value='<%# Bind("MACHINE_ID") %>' />
                                                                </td>
                                                                <td>
                                                                    <input id="btnMACHINE_NAME" type="button" value="" runat="server"  class="buttonDropdown" tabindex="-1" />
                                                                </td>
                                                                <td>
                                                                    <asp:HiddenField ID="hdnPROD_DTL_ID" runat="server" Value='<%# Bind("PROD_DTL_ID") %>' />
                                                                </td>

                                                                <td>
                                                                    <asp:TextBox ID="txtITEM_NAME" runat="server" CssClass="textBox textAutoSelect" Width="130px" Text='<%# Bind("ITEM_NAME") %>'></asp:TextBox>
                                                                    <asp:HiddenField ID="hdnItemID" runat="server" Value='<%# Bind("ITEM_ID") %>' />
                                                                     <asp:HiddenField ID="hdnUsedGRIDID" runat="server" Value='<%# Bind("USED_GRID_ID") %>' />
                                                                     <asp:HiddenField ID="hdnUSEDGRIDNAME" runat="server" Value='<%# Bind("USED_GRID_NAME") %>' />
                                                                </td>
                                                                <td>
                                                                    <input id="btnITEM_NAME" type="button" value="" runat="server" class="buttonDropdown"
                                                                        tabindex="-1" />
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtITEM_PANEL_QTY" runat="server" CssClass="txtQty textBox textAutoSelect" Width="50px"  BackColor="Khaki" Font-Bold="true" align="left" Text='<%# Bind("ITEM_PANEL_QTY") %>' onchange="TestOnTextChange(this)" onkeypress=" return isNumberKey(event,this);"></asp:TextBox>
                                                                </td>

                                                                 <td>
                                                                    <asp:TextBox ID="txtPANEL_UOM_ID" runat="server" CssClass="textBox textAutoSelect" Width="40px" Text='<%#Bind("PANEL_UOM_NAME") %>'></asp:TextBox>
                                                                     <asp:HiddenField ID="hdnPANEL_UOM_ID" runat="server" Value='<%# Bind("PANEL_UOM_ID") %>' />
                                                                      <asp:HiddenField ID="hdnPANEL_PC" runat="server" Value='<%# Bind("PANEL_PC") %>' />
                                                                </td>
                                                                <td>
                                                                    <input id="btnPANEL_UOM_ID" type="button" value="" runat="server" class="buttonDropdown" tabindex="-1" />
                                                                </td>

                                                                <td>
                                                                    <asp:TextBox ID="txtItem_qty" runat="server" CssClass="textBox textAutoSelect" Width="72" align="right" Text='<%# Bind("ITEM_QTY") %>'  onkeypress="return isNumberKey(event,this);"></asp:TextBox>
                                                                </td>

                                                                 <td>

                                                                    <asp:TextBox ID="txtUOM_NAME" runat="server" CssClass="textBox textAutoSelect"  Width="42px" Text='<%# Bind("UOM_NAME") %>'></asp:TextBox>
                                                                    <asp:HiddenField ID="hdnUomID" runat="server" Value='<%# Bind("UOM_ID") %>' />
                                                                </td>
                                                                 <td>
                                                                    <asp:HiddenField ID="hdnITEM_STANDARD_WEIGHT_KG" runat="server"  value='<%# Bind("PASTE_PANEL_KG") %>'></asp:HiddenField>
                                                                    <asp:TextBox ID="txtITEM_STANDARD_WEIGHT_KG" runat="server" CssClass="txtstdweightQty textBox textAutoSelect" Width="60px" align="left" Text='<%# Bind("PASTE_PANEL_KG") %>'  onkeypress="return isNumberKey(event,this);"></asp:TextBox>
                                                                 </td>
                                                                 <td>
                                                                    <asp:TextBox ID="txtITEM_WEIGHT" runat="server" CssClass="txtWeightQty textBox textAutoSelect" Width="72" align="left" Text='<%# Bind("ITEM_WEIGHT") %>'  onkeypress="return isNumberKey(event,this);"></asp:TextBox>
                                                                </td>
                                                                 <td>
                                                                    <asp:TextBox ID="txtWEIGHT_UOM_NAME" runat="server" CssClass="textBox textAutoSelect" Width="68" Text='<%# Bind("WEIGHT_UOM_NAME") %>' Style=""   ></asp:TextBox>
                                                                    <asp:HiddenField ID="hndWEIGHT_UOM_ID" runat="server" Value='<%# Bind("WEIGHT_UOM_ID") %>' />
                                                                </td>
                                                                <td>
                                                                    <input id="btnWEIGHT_UOM_ID" type="button" value="" runat="server" class="buttonDropdown" tabindex="-1" />
                                                                </td>
                                                                
                                                                 <td>
                                                                     <asp:TextBox ID="txtOPERATOR_NAME" runat="server" CssClass="textBox textAutoSelect" Width="95px" Text='<%#Bind("OPERATOR_NAME") %>'></asp:TextBox>
                                                                     <asp:HiddenField ID="hdnOPERATOR_ID" runat="server" Value='<%# Bind("OPERATOR_ID") %>' />
                                                                </td>
                                                                <td>
                                                                    <input id="btnOPERATOR" type="button" value="" runat="server" class="buttonDropdown" tabindex="-1" />
                                                                </td>
                                                            <%--    
                                                                <td>
                                                                    <asp:TextBox ID="txtUSED_BAR_PC" runat="server" CssClass="textBox textAutoSelect" Width="70px" Text='<%# Bind("USED_BAR_PC") %>' onchange="CalcBarWeight(this)"  onkeypress="return isNumberKey(event,this);" ></asp:TextBox>
                                                                </td>

                                                                <td>
                                                                    <asp:TextBox ID="txtBAR_TYPE" runat="server" CssClass="textBox textAutoSelect" Width="50px" Text='<%# Bind("BAR_TYPE_NAME") %>'></asp:TextBox>
                                                                    <asp:HiddenField ID="hdnBAR_TYPE" runat="server" Value='<%# Bind("BAR_TYPE") %>' />
                                                                    <asp:HiddenField ID="hdnBAR_WEIGHT" runat ="server" Value='<%# Bind("BAR_WEIGHT") %>' />

                                                                </td>
                                                                 <td>
                                                                    <input id="btnBAR_TYPE" type="button" value="" runat="server"  class="buttonDropdown" tabindex="-1" />
                                                                </td> --%>

                                                                <td>
                                                                    <asp:TextBox ID="txtGRID_BATCH"  CssClass="textBox" runat="server"  style="text-transform: uppercase"  Text='<%# Bind("GRID_BATCH") %>'  Width="45px"></asp:TextBox>
                                                                </td>
                                                               
                                                               <%-- <td>
                                                                    <asp:TextBox ID="txtBOM_Name" runat="server" CssClass="textBox textAutoSelect" Width="129px" Text='<%#Bind("BOM_NAME") %>'></asp:TextBox>
                                                                     <asp:HiddenField ID="hdnITEM_BOM_ID" runat="server" Value='<%# Bind("BOM_ID") %>' />
                                                                </td>
                                                                <td>
                                                                    <input id="btnITEM_BOM_ID" type="button" value="" runat="server" class="buttonDropdown" tabindex="-1"/>
                                                                </td>--%>
                                                                <td>
                                                                    <asp:TextBox ID="txtRemarks" runat="server" CssClass="textBox textAutoSelect" Width="140px" Text='<%# Bind("REMARKS") %>' Style=""></asp:TextBox>
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
                                <input id="hdnJournalDetRefJson2" type="hidden" runat="server" value="[]" />
                                <input id="hdnJournalDetInsJson2" type="hidden" runat="server" value="[]" />
                       </ContentTemplate>
                         <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnNewRow" EventName="Click" />
                            </Triggers>
                    </asp:UpdatePanel>


                    </div>
                    <div id="divGridControls2" style="width: 90%; height: 30px; border-top: solid 1px #C0C0C0; border-bottom : solid 1px #0b07f5;">
                        <table style="width: 80%; height: 100%; text-align: center;" cellspacing="1" cellpadding="1"
                            border="0">
                            <tr>
                               <td style="width: 2px"></td>
                              
                                            <td style="width: 160px; text-align: right;">
                                                <asp:Label ID="lbl" runat="server" Text="Total :"></asp:Label>
                                                                             </td>
                                            <td align="right"style=" width: 160px; text-align: right;">
                                              <asp:TextBox ID="txtTotalPanelQty" runat="server" Width="80px" Text="0" CssClass="textBox textBoxReadOnly"></asp:TextBox>
                                            </td>
                                            <td style="width: 160px; text-align: right;">
                                                                             </td>
                                             <td  style="width: 250px; text-align: right;" align="right">
                                                 <asp:TextBox ID="txtPanelWeight" runat="server" Width="90px" Text="0" CssClass="textBox textBoxReadOnly"></asp:TextBox>
                                             </td>
                                            <td align="right">
                                                &nbsp;</td>
                                            <td align="right" style="width: 90px">&nbsp;
                                            </td>
                                 <td style="width: 90px" align="left">
                                    <asp:Button ID="btnNewRow" runat="server" CssClass="buttonNewRow" Text="" OnClientClick="ShowProgress()" OnClick="btnNewRow_Click" />
                                </td>
                                <td style="width: 20px;">
                                    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                                        DisplayAfter="300">
                                        <ProgressTemplate>
                                            <asp:Image ID="imgProgress2" runat="server" ImageUrl="~/image/loader.gif" />
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </td>
                            </tr>
                            
                                
                        </table>
                    </div>
                </div>

                 
                <div id="Div3" runat="server" class="" style="width: 100%; text-align: left;">
                    <span style="font-weight: bold; font-size : 15px;color :#ff3b00;">Used Raw Materials Details: </span>
                </div>

                <div id="Div1" runat="server" class="" style="width: auto; height: auto; text-align: left">
                    <div id="dvGridHeaderClosing" style="width: 990px; height: 25px; font-size: smaller;" class="subHeader_Prod">
                        <table style="height: 80%; color: #FFFFFF; font-weight: bold; text-align: left;"
                            class="defFont" cellspacing="1" cellpadding="1">
                            <tr class="headerRow_Prod">
                                                    <td width="40px" class="headerColCenter_prod">SL#
                                                    </td>
                                                    <td width="250px" class="headerColCenter_prod">Item
                                                    </td>
                                                    <td width="15px" class="headerColCenter_prod"></td>
                                                    <td width="72px" class="headerColCenter_prod">UOM
                                                    </td>
                                                     <td width="115px" class="headerColCenter_prod">Op Stock
                                                    </td>
                                                    <%-- <td width="145px" class="headerColCenter" style="display:none;">Manual Opening Stock
                                                    </td>--%>
                                                    <td width="72px" class="headerColCenter_prod">Closing Qty
                                                    </td>
                                <%--
                                                     <td width="75px" class="headerColCenter_prod"> Wastage Qty
                                                    </td>
                                                     <td width="72px" class="headerColCenter_prod"> Rejected Qty
                                                    </td>
                                                    <td width="72px" class="headerColCenter_prod"> Pos Dev
                                                    </td>
                                                    <td width="73px" class="headerColCenter_prod"> Neg Dev
                                                    </td>--%>
                                                     <td width="77px" class="headerColCenter_prod"> Used Qty
                                                    </td>
                                                    <td width="119px" class="headerColCenter_prod">Remarks
                                                    </td>
                                                    <td width="16px" class="headerColCenter_prod">Delete
                                                    </td>
                                
                                                </tr>
                        </table>
                    </div>
                    <div id="dvGridClosing" style=" width: auto; height: 100px; overflow: auto;" class="dvGrid">
                      <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                                <asp:GridView ID="grdClosingRowMaterial" runat="server" AutoGenerateColumns="False" ShowHeader="False" 
                                    CellPadding="1" CellSpacing="1" ForeColor="#333333" GridLines="Both" DataKeyNames="CLOSING_ITEM_ID" 
                                    EnableModelValidation="True" ClientIDMode="AutoID" OnRowCommand="grdClosingRowMaterial_RowCommand" OnRowCreated="grdClosingRowMaterial_RowCreated" OnRowDataBound="grdClosingRowMaterial_RowDataBound" OnRowDeleting="grdClosingRowMaterial_RowDeleting"  >
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <Columns>
                                       

                                        <asp:TemplateField HeaderText="SL#">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCLOSING_SI" runat="server" Text='<%# Bind("CLOSING_SI") %>' Style="text-align: center;"
                                                    Width="40px">
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
                                                                    <asp:HiddenField ID="hdnCLOSING_ID" runat="server" Value='<%# Bind("CLOSING_ID") %>' />
                                                                </td>
                                                                <td>
                                                                    <asp:HiddenField ID="hdnPROD_MST_ID" runat="server" Value='<%# Bind("PROD_MST_ID") %>' />
                                                                </td>

                                                                <td>
                                                                    <asp:TextBox ID="txtCLOSINGITEM_NAME" runat="server" CssClass="textBox textAutoSelect" Width="250px" Text='<%# Bind("CLOSINGITEM_NAME") %>'></asp:TextBox>
                                                                    <asp:HiddenField ID="hdnCLOSING_ITEM_ID" runat="server" Value='<%# Bind("CLOSING_ITEM_ID") %>' />
                                                                </td>
                                                                <td>
                                                                    <input id="btnCLOSINGITEM_NAME" type="button" value="" runat="server" class="buttonDropdown" tabindex="-1" />
                                                                </td>
                                                                 <td>

                                                                    <asp:TextBox ID="txtCLOSING_UOM_NAME" runat="server" CssClass="textBox textAutoSelect"  Width="65px" Text='<%# Bind("CLOSING_UOM_NAME") %>'></asp:TextBox>
                                                                    <asp:HiddenField ID="hdnCLOSING_UOM_ID" runat="server" Value='<%# Bind("CLOSING_UOM_ID") %>' />
                                                                </td>
                                                                 <td>
                                                                    <asp:TextBox ID="txtSYSTEM_OPENING_STOCK" runat="server" CssClass="textBox textAutoSelect" Width="115px" align="right" Text='<%# Bind("SYSTEM_OPENING_STOCK") %>'  onkeypress="return isNumberKey(event,this);"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtCLOSING_QTY" runat="server" CssClass="textBox textAutoSelect"   Width="70px" align="right" Text='<%# Bind("CLOSING_QTY") %>' onchange="calcIssueStock(this)" ></asp:TextBox>
                                                                </td>
                                                                <%--
                                                                
                                                                <td>
                                                                <asp:TextBox ID="txtWASTAGE_QTY" runat="server" CssClass="textBox textAutoSelect"   Width="70" align="right" Text='<%# Bind("WASTAGE_QTY") %>' onkeypress="return isNumberKey(event,this);"  ></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                <asp:TextBox ID="txtREJECTED_QTY" runat="server" CssClass="textBox textAutoSelect"   Width="70" align="right" Text='<%# Bind("REJECTED_QTY") %>' onkeypress="return isNumberKey(event,this);" ></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                <asp:TextBox ID="txtPOSITIVE_DEV" runat="server" CssClass="textBox textAutoSelect"   Width="70" align="right" Text='<%# Bind("POSITIVE_DEV") %>' onkeypress="return isNumberKey(event,this);" ></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                <asp:TextBox ID="txtNEGATIVE_DEV" runat="server" CssClass="textBox textAutoSelect"   Width="70" align="right" Text='<%# Bind("NEGATIVE_DEV") %>' onkeypress="return isNumberKey(event,this);" ></asp:TextBox>
                                                                </td>--%>
                                                                 <%--<td>
                                                                    <asp:TextBox ID="txtMANUAL_OPENING_STOCK" runat="server" CssClass="textBox textAutoSelect" Width="135px" align="right" onchange="calcIssueStock(this)" Text='<%# Bind("MANUAL_OPENING_STOCK") %>'  onkeypress="return isNumberKey(event,this);"></asp:TextBox>
                                                                </td>--%>
                                                                 <td>
                                                                    <asp:TextBox ID="txtISSUE_STOCK" runat="server" CssClass="textBox textAutoSelect" Width="72" BackColor="Khaki" Font-Bold="true" align="right" Text='<%# Bind("ISSUE_STOCK") %>' onchange="calcCloseingStock(this)"  onkeypress="return isNumberKey(event,this);"></asp:TextBox>
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
                                <input id="Hidden1" type="hidden" runat="server" value="[]" />
                                <input id="Hidden2" type="hidden" runat="server" value="[]" />
                       </ContentTemplate>
                         <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnNewRowClosing" EventName="Click" />
                            </Triggers>
                    </asp:UpdatePanel>


                    </div>
                    <div id="divGridControlsClosing" style="width: 90%; height: 50px; border-top: solid 1px #C0C0C0;">
                        <table style="width: 80%; height: 100%; text-align: center;" cellspacing="1" cellpadding="1"
                            border="0">
                            <tr>
                                <td style="width: 2px"></td>
                               
                                <td style="width: 160px; text-align: right;">
                                    <%-- <asp:TextBox ID="txtTotQty" runat="server" CssClass="textBox" Style="text-align: right;"
                                        Width="100" TabIndex="-1" Font-Bold="True"></asp:TextBox>--%>
                                </td>
                                <td align="right" style="text-align: left">
                                    <%--<asp:Label ID="Label1" runat="server" Text="Total Qty :" Font-Bold="True"></asp:Label>--%>&nbsp;
                                </td>
                                <td align="right">
                                    &nbsp;</td>
                                <td align="right">
                                    &nbsp;</td>
                                <td align="right">&nbsp;
                                </td>
                                 <td style="width: 90px" align="left">
                                    <asp:Button ID="btnNewRowClosing" runat="server" CssClass="buttonNewRow" Text="" OnClientClick="ShowClosingProgress()" OnClick="btnNewRowClosing_Click" />
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
               </div>
        <div id="dvContentFooter" class="dvContentFooter">
            <table>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="btnAddNew" runat="server" Text="New" CssClass="buttonNew" OnClick="btnAddNew_Click"  />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonCancel checkIsDirty" />
                    </td>
                    <td>
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonSave checkRequired" AccessKey="s" OnClientClick="if ( ! UserDeleteConfirmation()) return false;" OnClick="btnSave_Click" />
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="buttonEdit" OnClick="btnEdit_Click" />
                    </td>
                     <td>
                        <asp:Button ID="btnAuthorize" runat="server" Text="Authorize" CssClass="buttoncommon" OnClientClick="if ( ! UserAuthorizeConfirmation()) return false;" OnClick="btnAuthorize_Click" />
                    </td>
                    <td>
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="buttonDelete" />
                    </td>
                    <td>
                        <asp:Button ID="btnRefresh" runat="server" Text="Refresh" CssClass="buttonRefresh checkIsDirty" />
                    </td>
                    <td>
                        <%--<asp:TextBox ID="TextBox1" runat="server" CssClass="textBox" Style="text-align: right;"
                                        Width="100" TabIndex="-1" Font-Bold="True"></asp:TextBox>--%>
                    </td>
                    <td>
                        <input id="btnClose" type="button" runat="server" class="buttonClose" value="Close" onclick="if (ContentForm) { ContentForm.CloseForm(); }" />
                    </td>
                    <td></td>
                    <td>
                      
                    </td>
                    <td>
                       
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </div>

                 

           


         </div>
</asp:Content>
