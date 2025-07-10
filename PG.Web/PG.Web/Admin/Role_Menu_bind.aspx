<%@ Page Title="" Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="Role_Menu_bind.aspx.cs" Inherits="PG.Web.Admin.Role_Menu_bind" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../javascript/jquery.ui.combogrid.js" type="text/javascript"></script>
    <script src="../javascript/jquery.attributeobserver.js" type="text/javascript"></script>
    <link href="../css/jquery.ui.combogrid.css" rel="stylesheet" type="text/css" />
     <script language="javascript" type="text/javascript">
         var isPageResize = true;
         ContentForm.CalendarImageURL = "../image/calendar.png";
         var hdnCompanyID = '<%=hdnCompanyID.ClientID%>';

         var MenuListServiceLink = '<%=this.MenuListServiceLink%>';

<%--  var ItemGroupListServiceLink = '<%=this.ItemGroupListServiceLink%>';
         var ItemListServiceLink = '<%=this.ItemListServiceLink%>';
         var BOMItemListServiceLink = '<%=this.BOMItemListServiceLink%>';

         var BOMNameListServiceLink = '<%=this.BOMItemNameListServiceLink%>';

         var BOMListServiceLink = '<%= this.BOMListServiceLink%>';
         var PanelUOMServiceLink = '<%= this.PanelUOMServiceLink %>';
         var MachineListServiceLink = '<%= this.MachineListServiceLink%>';
         var SupporvisorListServiceLink = '<%= this.SupporvisorListServiceLink%>';--%>
         

         var gridUpdatePanelIDDet = '<%=UpdatePanel1.ClientID%>';
         var gridViewIDDet = '<%=GRDDTLITEMLIST.ClientID%>';
         var updateProgressID = '<%=UpdateProgress2.ClientID%>';


      


       

         
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

         function ShowProgress() {
             $('#' + updateProgressID).show();
         }

         function ShowClosingProgress() {
             $('#' + updateClosingProgressID).show();
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
                         //bindPanelUOMList(gridViewIDDet);
                         //bindWeightUOMList(gridViewIDDet);
                         bindMachineList(gridViewIDDet);
                         //bindOperatorList(gridViewIDDet);
                         //bindBARTYPEList(gridViewIDDet);

                     }


                     if (panels[i].id == gridClosingUpdatePanelIDDet) {
                         bindClosingItemList(gridViewClosingIDDet);
                     }
                 }
             });
           //  bindItemGroupList(gridViewIDDet);
             bindItemList(gridViewIDDet);
             //bindBOMItemList(gridViewIDDet);
             //bindPanelUOMList(gridViewIDDet);
             //bindWeightUOMList(gridViewIDDet);
             //bindMachineList(gridViewIDDet);
             //bindOperatorList(gridViewIDDet);
             //bindBARTYPEList(gridViewIDDet);
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
                                { 'columnName': 'itemname', 'width': '250', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                              , { 'columnName': 'uomname', 'width': '70', 'align': 'left', 'highlight': 4, 'label': 'Uom' }
                            //  , { 'columnName': 'itemcode', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Code' }
                             // , { 'columnName': 'itemgroupdesc', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Group Name' }
                             //  , { 'columnName': 'item_type', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Item Type' }
                              //, { 'columnName': 'closing_qty', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Cls Qty' }
                              //, { 'columnName': 'class_name', 'width': '120', 'align': 'left', 'highlight': 4, 'label': 'Class Name' }
                              //, { 'columnName': 'bomname', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'BOM Name' }
                              //, { 'columnName': 'bomno', 'width': '180', 'align': 'left', 'highlight': 4, 'label': 'BOM NO' }
                              //, { 'columnName': 'uomid', 'width': '50', 'align': 'left', 'highlight': 4, 'label': 'Uom ID' }
                              // , { 'columnName': 'bomid', 'width': '50', 'align': 'left', 'highlight': 4, 'label': 'BOM ID' }


             ];


             //var companyid = $('#' + hdnCompanyID).val();
             //var depthead = $('#' + hdnEmpCode).val();
             //var locationid = $('#' + ddlLocation).val();

             //var serviceURL = GLAccountServiceLink + "?isterm=1&includeempty=1&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
             var serviceURL = BOMNameListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
             //serviceURL += "&companyid=" + companyid;
             serviceURL += "&ispaging=1&isFinished=Y&deptid=18";
             //serviceURL += "&locationid=" + locationid;
             // serviceURL += "&empstatus=" + "A";
             //serviceURL += "&acctypefilter=" + Enums.GLAccountTypeFilter.AllAccount;
             // serviceURL += "&namecomptype=" + Enums.DataCompareType.Contains;

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
                     width: 600,
                     url: serviceURL,
                     search: function (event, ui) {
                         var elemRowCur = $(elem).closest('tr.gridRow');
                         var vgroupid = $(elemRowCur).find('input[id$="hdngroupId"]').val();
                         //var vgroupid = $('#' + hdngroupId).val();
                         var newServiceURL = serviceURL + "&groupid=" + vgroupid
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
                         //                    if (!validateGLAccount(elemID, null)) {
                         //                        $(elem).val(prevGLCode);
                         //                        return false;
                         //                    }
                         //ClearItemGroupData(elemID);
                     }
                     else {
                         //grp = GetGLGroup(eCode);
                         ////                    if (!validateGLAccount(elemID, grp)) {
                         ////                        $(elem).val(prevGLCode);
                         ////                        return false;
                         ////                    }

                         if (grp == null) {
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



         function bindClosingItemList(gridViewIDD) {
             var cgColumns = [{ 'columnName': 'itemname', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                              , { 'columnName': 'uomname', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Uom Name' }
                              , { 'columnName': 'itemcode', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Code' }
                              , { 'columnName': 'closing_qty', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Cls Qty' }
                              , { 'columnName': 'itemgroupdesc', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Group Name' }
                              , { 'columnName': 'class_name', 'width': '120', 'align': 'left', 'highlight': 4, 'label': 'Class Name' }
                              , { 'columnName': 'item_type', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Item Type' }
             ];
             
             var serviceURL = ItemListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
             //serviceURL += "&companyid=" + companyid;
             serviceURL += "&ispaging=1&deptid=18&isFinished=N";
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
                         //var vgroupid = $(elemRowCur).find('input[id$="hdngroupId"]').val();
                         //var vgroupid = $('#' + hdngroupId).val();
                         var newServiceURL = serviceURL;//+ "&groupid=" + vgroupid
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
                         //ClearItemGroupData(elemID);
                     }
                     else {
                         //grp = GetGLGroup(eCode);
                         ////                    if (!validateGLAccount(elemID, grp)) {
                         ////                        $(elem).val(prevGLCode);
                         ////                        return false;
                         ////                    }

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
             var cgColumns = [{ 'columnName': 'uomid', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'UOM ID' }
                              , { 'columnName': 'uomcodeshort', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'UOM Short Name' }
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
                     //grpID = $(self).closest('tr').find('input[id$="hdngroupId"]').val();
                     //if (grpID == '0' | grpID == '') {
                     //    $(self).addClass('textError');
                     //}

                 });

             });

         }


         function bindWeightUOMList(gridViewID) {
             var cgColumns = [{ 'columnName': 'uomid', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'UOM ID' }
                              , { 'columnName': 'uomcodeshort', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'UOM Short Name' }
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
                         //                    if (!validateGLAccount(elemID, null)) {
                         //                        $(elem).val(prevGLCode);
                         //                        return false;
                         //                    }
                         //ClearItemGroupData(elemID);
                     }
                     else {

                         if (grp == null) {
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

         }
         function SetItemData(txtItemCodeID, data) {
             $('#' + txtItemCodeID).val(data.itemid);

             var detRow = $('#' + txtItemCodeID).closest('tr.gridRow');
             $(detRow).find('input[id$="hdnItemID"]').val(data.itemid);
             $(detRow).find('input[id$="txtITEM_NAME"]').val(data.itemname);
             $(detRow).find('input[id$="hdngroupId"]').val(data.itemgroupid);
             $(detRow).find('input[id$="txtGroupName"]').val(data.itemgroupdesc);
             $(detRow).find('input[id$="hdnUomID"]').val(data.uomid);
             $(detRow).find('input[id$="txtUOM_NAME"]').val(data.uomname);

             $(detRow).find('input[id$="txtBOM_Name"]').val(data.bomname);
             $(detRow).find('input[id$="hdnITEM_BOM_ID"]').val(data.bomid);

             $(detRow).find('input[id$="txtWEIGHT_UOM_NAME"]').val('kg');
             $(detRow).find('input[id$="hndWEIGHT_UOM_ID"]').val('2');

            // $(detRow).find('input[id$="hdnUomID"]').val(data.isprime);
             // bomno


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
         }


         function calcIssueStock(txtbox)
         {
             var detRow = $(txtbox).closest('tr.gridRow');
             var  SYSTEM_OPENING_STOCK = $(detRow).find('input[id$="txtSYSTEM_OPENING_STOCK"]').val();
             var  CLOSING_QTY= $(detRow).find('input[id$="txtCLOSING_QTY"]').val();
              
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
                         //                    if (!validateGLAccount(elemID, null)) {
                         //                        $(elem).val(prevGLCode);
                         //                        return false;
                         //                    }
                         //ClearItemGroupData(elemID);
                     }
                     else {

                         if (grp == null) {
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
                         //                    if (!validateGLAccount(elemID, null)) {
                         //                        $(elem).val(prevGLCode);
                         //                        return false;
                         //                    }
                         //ClearItemGroupData(elemID);
                     }
                     else {

                         if (grp == null) {
                             ClearOperateData(elemID);
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


         $(':input').keypress(function (e) {
             if (e.which == 13) {
                 ti = $(this).attr('tabindex') + 1;
                 $('input[tabindex=' + ti + ']').focus();
                 //try to use________ e.which = 9; return e.which;
             } else if (e.which == 9) {
                 e.preventDefault(); //or return false;
             }
         });
           </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div id="dvPageContent" style="width: 100%; height: 100%;" onkeydown="if(event.keyCode==13){event.keyCode=9; return event.keyCode;}">
         <asp:HiddenField ID="hdnLoggedInUser" runat="server" />
                                  <asp:HiddenField ID="hdnPROD_ID" runat="server" />
                                  <asp:HiddenField ID="hndFORECUSTID" runat="server" />
                                  <asp:HiddenField ID="hdnSUPERVISOR_ID" runat="server"  />
         <asp:HiddenField ID="hdnCompanyID" runat="server" Value="0" />
          <div id="dvContentHeader" class="dvContentHeader">
            <div id="dvHeader" class="dvHeader" align="left">
                <asp:Label ID="lblHeader" runat="server" Text="Menu Assign to Role " CssClass="lblHeader" Font-Bold="true" Font-Size="15px" ></asp:Label>
            </div>
            <div id="dvMessage" class="dvMessage">
                <asp:Label ID="lblMessage" runat="server" CssClass="lblMessage" Text="" Width="100%"></asp:Label>
            </div>

            <div id="dvHeaderControl" class="dvHeaderControl">
            </div>

        </div>

           <div id="dvContentMain" class="dvContentMain" align="left">
                 <div id="dvControlsHead" style="height: auto; width: 100%;" align="left">
                      <table border="0" cellspacing="4" cellpadding="2" align="left" style="width: 70%" id="tblProductionMstEntry">
                          <tr>
                              <td style="text-align: right; width : 100px;" >
                                   <asp:Label ID="lblDEPT_ID" runat="server" Text="Role : "></asp:Label>
                              </td>
                              <td style="text-align: left">
                                   <asp:DropDownList ID="ddlRole" runat="server" CssClass="dropDownList required" Width="190px" ></asp:DropDownList>
                              </td>
                              <td style="text-align: right">
                                  
                              </td>
                              <td style="text-align: left">

                                
                              </td>
                              <td style="text-align: right"> 
                                   
                              </td>
                              <td style="text-align: left">
                                  
                              </td>
                              <td> 
                                  &nbsp;</td>
                              <td style="text-align: left"> 
                                  &nbsp;</td>


                          </tr>
                          
                          
                        </table>
                     </div>
 
               <div id="dvGridSeparator" runat="server" style="height : 10px; float: left;width: 100%;">
                </div>
                <div id="Div5" runat="server" class="" style="float: right;width: 100%; text-align: left; border-top : solid 1px #0b07f5;">
                    <span style="font-weight: bold;font-size : 15px;color :#ff3b00;">Menu Details: </span>
                </div>
                <div id="dvGridHeader2" style="width: 700px; height: 25px; font-size: smaller;" class="subHeader">
                                    <table style="height: 80%; color: #FFFFFF;  font-weight: bold; text-align: left;"
                                        class="defFont" cellspacing="1" cellpadding="1">
                                        <tr class="headerRow">
                                            <td width="30px" class="headerColCenter_prod">SL#
                                            </td>
                                            
                                       
                                            <td width="450px" class="headerColCenter_prod">Menu
                                            </td> 
                                            <td width="80px" class="headerColCenter_prod">  Show
                                            </td>
                                             <td width="15px" class="headerColCenter_prod"></td>
                                       
                                        
                                            <td width="16px" class="headerColCenter_prod">Delete
                                            </td>
                                
                                        </tr>
                                    </table>
                                </div>
               <div id="groupBoxContainer boxShadow" style="height: auto; width: 100%; ">
                        <div id="groupDataDetails" style=" width: 90%; height: auto;">
                  
                            <div id="dvGridContainer2" class=" " runat="server" style="height: auto; text-align: left; width : 100%">
                               
                                <div id="dvGrid" style="  width: 1100px; height: 150px; overflow: auto;" class="dvGrid">
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
                                                                Width="30px">
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
                                                                                <asp:HiddenField ID="hdnROLEMENUID" runat="server" Value='<%# Bind("ROLEMENUID") %>' />
                                                                            </td>

                                                                            <td>
                                                                                <asp:TextBox ID="txtROLE_NAME" runat="server" CssClass="textBox textAutoSelect" Width="430px" Text='<%# Bind("ROLE_NAME") %>'></asp:TextBox>
                                                                                <asp:HiddenField ID="hdnROLEID" runat="server" Value='<%# Bind("ROLEID") %>' />
                                                                            </td>
                                                                            <td>
                                                                                <input id="btnROLE_NAME" type="button" value="" runat="server" class="buttonDropdown"
                                                                                    tabindex="-1" />
                                                                            </td>
                                                                             <td>
                                                                                 <asp:CheckBox ID="chkSHOWMENU" runat="server" Checked='<%# Bind("SHOWMENU") %>' />
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
                                                                            <div style="position: relative; height: 80%; width: 100%;">
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


                                <div id="divGridControls2" style="width: 100%; height: 30px; border-top: solid 1px #C0C0C0; border-bottom : solid 1px #0b07f5; ">
                                    <table style="width: 70%; height: 100%; text-align: center;" cellspacing="1" cellpadding="1"
                                        border="0">
                                        <tr>
                                          
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
                               
                                  <td style="width: 2px"></td>
                              
                                            <td style="width: 160px; text-align: right;">
                                                                             </td>
                                            <td align="right" style="text-align: left">
                                               &nbsp;
                                            </td>
                                             <td align="right">
                                                &nbsp;</td>
                                            <td align="right">
                                                &nbsp;</td>
                                            <td align="right" style="width: 90px">&nbsp;
                                            </td>

                                        </tr>
                            
                                
                                    </table>
                                </div>
                            </div>

                            <div id="Div2" runat="server" style="width: 100%;">
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
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonSave checkRequired" AccessKey="s" OnClientClick="if ( ! UserSaveConfirmation()) return false;" OnClick="btnSave_Click" />
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="buttonEdit" OnClick="btnEdit_Click" />
                    </td>

                    <td>
                        <asp:Button ID="btnAuthorize" runat="server" Text="Authorize" CssClass="buttoncommon" OnClientClick="if ( ! UserAuthorizeConfirmation()) return false;" OnClick="btnAuthorize_Click" Visible="False" />
                    </td>
                    <td>
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="buttonDelete" />
                    </td>
                    <td>
                        <asp:Button ID="btnRefresh" runat="server" Text="Refresh" CssClass="buttonRefresh checkIsDirty" OnClick="btnRefresh_Click" />
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
