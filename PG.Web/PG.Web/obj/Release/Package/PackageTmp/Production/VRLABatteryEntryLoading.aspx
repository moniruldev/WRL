<%@ Page Title="" Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="VRLABatteryEntryLoading.aspx.cs" Inherits="PG.Web.Production.VRLABatteryEntryLoading" %>

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


        <%--  var gridClosingUpdatePanelIDDet = '<%=UpdatePanel2.ClientID%>';--%>
        <%-- var gridViewClosingIDDet = '<%=grdClosingRowMaterial.ClientID%>';--%>
     <%--   var updateClosingProgressID = '<%=UpdateProgress3.ClientID%>';--%>


        var txtSUPERVISOR_NAME = '<%= txtSUPERVISOR_NAME.ClientID%>';
        var btnSUPERVISOR_ID = '<%=btnSUPERVISOR_ID.ClientID%>';
        var hdnSUPERVISOR_ID = '<%=hdnSUPERVISOR_ID.ClientID%>';

       <%-- var txtMACHINE_NAME = '<%= txtMACHINE_NAME.ClientID%>';
        var btnMACHINE_NAME = '<%=btnMACHINE_NAME.ClientID%>';
        var hndMACHINE_ID = '<%=hndMACHINE_ID.ClientID%>';--%>


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
                        //bindBOMItemList(gridViewIDDet);
                        //bindPanelUOMList(gridViewIDDet);
                        //bindWeightUOMList(gridViewIDDet);
                        //bindMachineList(gridViewIDDet);
                        //// bindOperatorList(gridViewIDDet);
                        //  bindBARTYPEList(gridViewIDDet);

                    }


                    //if (panels[i].id == gridClosingUpdatePanelIDDet) {
                    //    //bindClosingItemList(gridViewClosingIDDet);
                    //}
                }
            });


            //  bindItemGroupList(gridViewIDDet);
            bindItemList(gridViewIDDet);
            //bindBOMItemList(gridViewIDDet);
            //bindPanelUOMList(gridViewIDDet);
            // bindWeightUOMList(gridViewIDDet);
            //bindMachineList(gridViewIDDet);
            // bindOperatorList(gridViewIDDet);
            // bindBARTYPEList(gridViewIDDet);
            bindSupporvisorList();


            //bindClosingItemList(gridViewClosingIDDet);

        });

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
                             , { 'columnName': 'uomname', 'width': '70', 'align': 'left', 'highlight': 4, 'label': 'UOM' }
                           //  , { 'columnName': 'itemcode', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Code' }
                            // , { 'columnName': 'itemgroupdesc', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Group Name' }
                            //  , { 'columnName': 'item_type', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Item Type' }
                             //, { 'columnName': 'closing_qty', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Cls Qty' }
                             //, { 'columnName': 'class_name', 'width': '120', 'align': 'left', 'highlight': 4, 'label': 'Class Name' }
                             , { 'columnName': 'bomname', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'BOM' }
                             , { 'columnName': 'bomno', 'width': '160', 'align': 'left', 'highlight': 4, 'label': 'BOM NO' }
                             //, { 'columnName': 'uomid', 'width': '50', 'align': 'left', 'highlight': 4, 'label': 'Uom ID' }
                             // , { 'columnName': 'bomid', 'width': '50', 'align': 'left', 'highlight': 4, 'label': 'BOM ID' }
            ];

            //var companyid = $('#' + hdnCompanyID).val();
            //var depthead = $('#' + hdnEmpCode).val();
            //var locationid = $('#' + ddlLocation).val();
            //var serviceURL = GLAccountServiceLink + "?isterm=1&includeempty=1&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            var serviceURL = BOMNameListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            serviceURL += "&ispaging=1&isFinished=Y&for_production=Y";
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

                        var specId = $(elemRowCur).find('select[id$="ddlSpecificationType"]').val();
                       <%--  var e = document.getElementById("<%=ddlDEPT_ID.ClientID%>");
                         var deptid = e.options[e.selectedIndex].value; "&groupid=" + vgroupid + --%>
                        var newServiceURL = serviceURL + "&deptid=142" + "&is_solar_Loading=Y" + "&specId=" + specId;
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
                        serviceURL += "&ispaging=1&isFinished=Y&deptid=142&for_production=Y";

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



        function bindClosingItemList(gridViewIDD) {
            var cgColumns = [{ 'columnName': 'itemname', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                             , { 'columnName': 'uomname', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Uom Name' }
                             , { 'columnName': 'itemcode', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Code' }
                             , { 'columnName': 'closing_qty', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Cls Qty' }
                             , { 'columnName': 'itemgroupdesc', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Group Name' }
                             , { 'columnName': 'class_name', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Class Name' }
                             //, { 'columnName': 'item_type', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Item Type' }
            ];


            //var companyid = $('#' + hdnCompanyID).val();
            //var depthead = $('#' + hdnEmpCode).val();
            //var locationid = $('#' + ddlLocation).val();

            //var serviceURL = GLAccountServiceLink + "?isterm=1&includeempty=1&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            var serviceURL = ItemListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            //serviceURL += "&companyid=" + companyid;&deptid=135
            serviceURL += "&ispaging=1&forProduction=Y&deptid=11&isFinished=N";
            //serviceURL += "&locationid=" + locationid;
            // serviceURL += "&empstatus=" + "A";
            //serviceURL += "&acctypefilter=" + Enums.GLAccountTypeFilter.AllAccount;
            // serviceURL += "&namecomptype=" + Enums.DataCompareType.Contains;


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

                        var deptid = $('#' + ddlDEPT_ID).val();
                        var newServiceURL = serviceURL;

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
                            SetItemData(elemID, ui.item);
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
            $(detRow).find('input[id$="txtPANEL_UOM_ID"]').val(data.paneluomname);
            $(detRow).find('input[id$="hdnPANEL_UOM_ID"]').val(data.paneluomid);
            $(detRow).find('input[id$="hdnPANEL_PC"]').val(data.panelpc);
            $(detRow).find('input[id$="txtBOM_Name"]').val(data.bomname);
            $(detRow).find('input[id$="hdnITEM_BOM_ID"]').val(data.bomid);
            $(detRow).find('input[id$="txtClosingQty"]').val(data.closing_qty);

            //   $(detRow).find('input[id$="txtWEIGHT_UOM_NAME"]').val('kg');
            //  $(detRow).find('input[id$="hndWEIGHT_UOM_ID"]').val('2');


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

            var panelqty = $(detRow).find('input[id$="txtITEM_PANEL_QTY"]').val();
            var tpcqty = pcqty * panelqty;
            $(detRow).find('input[id$="txtItem_qty"]').val(tpcqty);
        }


        function calcIssueStock(txtbox) {

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
            var weight = $(detRow).find('input[id$="hdnBAR_WEIGHT"]').val();

            var pcqty = $(detRow).find('input[id$="txtUSED_BAR_PC"]').val();
            var tpcqty = pcqty * weight;
            $(detRow).find('input[id$="txtUSED_QTY_KG"]').val(tpcqty);
        }

        function bindMachineList() {
            var cgColumns = [
                                { 'columnName': 'machinename', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Machine Name' }
                              , { 'columnName': 'machinedescription', 'width': '200', 'align': 'left', 'highlight': 4, 'label': '  Description' }

            ];
            var e = document.getElementById("<%=ddlDEPT_ID.ClientID%>");
            var dept_id = e.options[e.selectedIndex].value;
            var serviceURL = MachineListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            serviceURL += "&ispaging=0&deptid=" + dept_id;
            var groupIDElem = $('#' + txtMACHINE_NAME);

            $('#' + btnMACHINE_NAME).click(function (e) {
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
                    var dept_id = $('#' + ddlDEPT_ID).val();
                    var newServiceURL = serviceURL + "&deptid=" + dept_id;
                    newServiceURL = JSUtility.AddTimeToQueryString(newServiceURL);
                },
                select: function (event, ui) {
                    if (!ui.item) {
                        event.preventDefault();
                        return false;
                    }
                    if (!ui.item) {
                        event.preventDefault();
                        return false;
                    }
                    else {
                        $('#' + hndMACHINE_ID).val(ui.item.machineid);
                        $('#' + txtMACHINE_NAME).val(ui.item.machinename);
                    }
                    return false;
                },

                lc: ''
            });


            $(groupIDElem).blur(function () {

                <%-- var self = this;
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
                     $('#' + hdnSUPERVISOR_ID).val('0');
                     $('#' + txtSUPERVISOR_NAME).val('');
                 }--%>

            });
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
    </script>


    <style type="text/css">
        .auto-style1 {
            height: 30px;
        }
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="dvPageContent" style="width: 100%; height: 100%;">
        <asp:HiddenField ID="hdnLoggedInUser" runat="server" />
        <asp:HiddenField ID="hdnCompanyID" runat="server" Value="0" />
        <asp:HiddenField ID="hdnPROD_ID" runat="server" />
        <asp:HiddenField ID="hdnSUPERVISOR_ID" runat="server" />
        <asp:HiddenField ID="hndFORECUSTID" runat="server" />
        <div id="dvContentHeader" class="dvContentHeader">
            <div id="dvHeader" class="dvHeader_Prod" align="left">
                <asp:Label ID="lblHeader" runat="server" Text="VRLA Charging Loading Entry" CssClass="lblHeader" Font-Bold="true" Font-Size="15px"></asp:Label>
            </div>
            <div id="dvMessage" class="dvMessage">
                <asp:Label ID="lblMessage" runat="server" CssClass="lblMessage" Text="" Width="100%"></asp:Label>
            </div>

            <div id="dvHeaderControl" class="dvHeaderControl">
            </div>

        </div>

        <div id="dvContentMain" class="dvContentMain" align="left">
            <div id="dvControlsHead" style="height: auto; width: 100%;" align="left">

                <fieldset style="border: 1px solid">
                    <legend>Production Information</legend>
                    <div style="float: left">
                        <table border="0" cellspacing="4" cellpadding="2" align="left" style="width: 100%;" id="tblProductionMstEntry">
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

                                    <asp:DropDownList ID="ddlDEPT_ID" runat="server" CssClass="dropDownList required" Width="190px"></asp:DropDownList>
                                </td>
                                <td style="text-align: right">
                                    <asp:Label ID="lblSUPERVISOR_ID" runat="server" Text="Supervisor : "></asp:Label>
                                </td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtSUPERVISOR_NAME" runat="server" CssClass="textBox textAutoSelect" Width="160px" autofocus></asp:TextBox>
                                    <input id="btnSUPERVISOR_ID" type="button" value="" runat="server" class="buttonDropdown" style="width: 15px" tabindex="-1" />
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
                                    <asp:TextBox ID="txtFORECUSTMONTH" runat="server" Style="display: none" CssClass="colourdisabletextBox" Enabled="false" Width="163px"></asp:TextBox>
                                    <asp:DropDownList ID="ddlFORECUSTMONTH" CssClass="dropDownList required" runat="server" Width="190px"></asp:DropDownList>
                                </td>
                                <td style="text-align: right">
                                    <asp:Label ID="lblSHIFT_ID" runat="server" Text="Shift : "></asp:Label>
                                </td>
                                <td style="text-align: left">
                                    <asp:DropDownList ID="ddlSHIFT_ID" runat="server" CssClass="dropDownList" Width="190px" AutoPostBack="True" OnSelectedIndexChanged="ddlSHIFT_ID_SelectedIndexChanged"></asp:DropDownList>

                                </td>

                            </tr>
                            <tr>

                                <td style="text-align: right">
                                    <asp:Label ID="lblProcessCode" runat="server" Text="Batch NO :"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPROD_BATCH_NO" runat="server" CssClass="textBox textNumberOnly" Width="163px"></asp:TextBox>

                                </td>
                                <td style="text-align: right">Batch Start Time : </td>
                                <td>
                                    <asp:TextBox ID="txtBATCH_STARTTIME" runat="server" CssClass="textBox textDate dateParse" Style="text-align: left;" Width="75px"></asp:TextBox>
                                    <asp:TextBox ID="txtBATCH_STARTTIMEs" runat="server" CssClass="textBox" Width="60px">  </asp:TextBox>
                                    <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" AcceptAMPM="true" Mask="99:99" MaskType="Time" MessageValidatorTip="true" TargetControlID="txtBATCH_STARTTIMEs" AutoCompleteValue="00:00 AM"></cc1:MaskedEditExtender>
                                </td>
                                <td style="text-align: right">
                                    <asp:Label ID="lblPRODUCTION_DATE" runat="server" Text="Production Date :"></asp:Label>

                                </td>
                                <td>
                                    <asp:TextBox ID="txtPRODUCTION_DATE" runat="server" CssClass="textBox textDate dateParse" Style="text-align: left;" Width="100px"></asp:TextBox>
                                </td>



                            </tr>
                            <tr style="display: none">

                                <td style="text-align: right">Batch End Time :
                                </td>
                                <td colspan="2">

                                    <asp:TextBox ID="txtBATCH_ENDTIME" runat="server" CssClass="textBox textDate dateParse" Style="text-align: left;" Width="100px"></asp:TextBox>
                                    <asp:TextBox ID="txtBATCH_ENDTIMEs" runat="server" CssClass="textBox" Width="60px">  </asp:TextBox>
                                    <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AcceptAMPM="true" Mask="99:99" MaskType="Time" MessageValidatorTip="true" TargetControlID="txtBATCH_ENDTIMEs" AutoCompleteValue="00:00 AM"></cc1:MaskedEditExtender>

                                </td>

                                <td>
                                    <asp:TextBox ID="txtBATCH_ID" runat="server" CssClass="textBox" Style="text-align: left; display: none" Width="150px"></asp:TextBox>
                                </td>
                                <td></td>
                                <td style="text-align: right"></td>
                                <td>&nbsp;</td>

                            </tr>
                        </table>
                    </div>
                </fieldset>



                <%-- <fieldset style="border: 1px solid;">
                    <legend>Circuit Information</legend>
                    <div style="float: left;">
                        <table border="0" cellspacing="4" cellpadding="2" align="left" style="width: 100%" id="tblCircuit">
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="Label1" runat="server" Text="Circuit : "></asp:Label>
                                </td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtMACHINE_NAME" runat="server" CssClass="textBox textAutoSelect"></asp:TextBox>
                                    <asp:HiddenField ID="hndMACHINE_ID" runat="server" />
                                    <input id="btnMACHINE_NAME" type="button" value="" runat="server" class="buttonDropdown" tabindex="-1" />
                                </td>
                                <td style="text-align: right">
                                    <asp:Label ID="Label2" runat="server" Text="Batch Start Time : "></asp:Label>
                                </td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txt_FORMATION_STARTTIME" Width="60px" runat="server" CssClass="textBox">  </asp:TextBox>
                                    <cc1:MaskedEditExtender ID="MaskedEditExtender3" runat="server" AcceptAMPM="true" Mask="99:99" MaskType="Time" MessageValidatorTip="true" TargetControlID="txt_FORMATION_STARTTIME" AutoCompleteValue="00:00 AM"></cc1:MaskedEditExtender>

                                </td>
                                <td style="text-align: right">
                                    <asp:Label ID="Label3" runat="server" Text="Cycle Time : "></asp:Label>
                                </td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtCYCLETIME" runat="server" CssClass="textBox textAutoSelect" align="right"></asp:TextBox>

                                </td>

                                <td style="text-align: right">
                                    <asp:Label ID="Label7" runat="server" Text="Temp : "></asp:Label>
                                </td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtTEMPARATURE" runat="server" CssClass="textBox textAutoSelect" align="right"></asp:TextBox>

                                </td>


                            </tr>
                            <tr>
                                <td style="text-align: right" class="auto-style1">
                                    <asp:Label ID="Label4" runat="server" Text="Batch Sl No : "></asp:Label>
                                </td>
                                <td style="text-align: left" class="auto-style1">
                                    <asp:TextBox ID="txtFormationBatchSlNo" runat="server" CssClass="textBox textAutoSelect" align="right" onkeypress="return isNumberKey(event,this);"></asp:TextBox>

                                </td>
                                <td style="text-align: right" class="auto-style1">
                                    <asp:Label ID="Label6" runat="server" Text="Amp : "></asp:Label>
                                </td>
                                <td style="text-align: left" class="auto-style1">
                                    <asp:TextBox ID="txtAMPERE" runat="server" CssClass="textBox textAutoSelect" align="right"></asp:TextBox>

                                </td>
                                <td style="text-align: right" class="auto-style1">
                                    <asp:Label ID="Label8" runat="server" Text="SP.GR : "></asp:Label>
                                </td>
                                <td style="text-align: left" class="auto-style1">
                                    <asp:TextBox ID="txtSpGr" runat="server" CssClass="textBox textAutoSelect" align="right"></asp:TextBox>

                                </td>
                                <td style="text-align: right" class="auto-style1"></td>
                                <td style="text-align: left" class="auto-style1"></td>
                            </tr>

                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="Label9" runat="server" Text="Remarks : "></asp:Label>
                                </td>
                                <td style="text-align: left" colspan="5">
                                    <asp:TextBox ID="txtRemarks" TextMode="MultiLine" runat="server" Width="400px" CssClass="textAreaAutoSize textAutoSelect"></asp:TextBox>
                                </td>
                            </tr>


                        </table>
                    </div>
                </fieldset>--%>
            </div>

            <div id="dvControls" style="height: auto; width: 100%;">

                <div id="groupDataDetails" style="width: 60%; height: auto;">
                    <div id="dvGridSeparator" runat="server" style="width: 100%;">
                    </div>
                    <div id="groupDataHeaderCredit" runat="server" class="" style="float: right; width: 100%; text-align: left; border-top: solid 1px #0b07f5;">
                        <span style="font-weight: bold; font-size: 15px; color: #ff3b00;">Production Items Details: </span>
                    </div>
                    <div id="dvGridContainer2" runat="server" class="" style="width: auto; height: auto; text-align: left">
                        <div id="dvGridHeader2" style="float: left; width: 800px; height: 25px; font-size: smaller;" class="subHeader_Prod">
                            <table style="height: 80%; color: #FFFFFF; font-weight: bold; text-align: left;"
                                class="defFont" cellspacing="1" cellpadding="1">
                                <tr class="headerRow_Prod">
                                    <td width="30px" class="headerColCenter_prod">SL#
                                    </td>
                                    <td width="134px" class="headerColCenter_prod">Item
                                    </td>
                                    <td width="15px" class="headerColLeft"></td>
                                    <td width="100px" class="headerColCenter_prod">Closing Qty
                                    </td>
                                    <%--<td width="52px" class="headerColCenter_prod">Panel Qty
                                    </td>
                                     <td width="80px" class="headerColCenter_prod">Unformed
                                    </td>--%>
                                    <%-- <td width="100px" class="headerColCenter_prod">Batch No
                                    </td>--%>
                                    <%-- 
                                    <td width="62px" class="headerColCenter_prod">Panel UOM
                                    </td>--%>

                                    <td width="55px" class="headerColCenter_prod">Item Qty
                                    </td>
                                    <td width="130px" class="headerColCenter_prod">Item Type
                                    </td>
                                    <td width="50px" class="headerColCenter_prod">UOM
                                    </td>
                                    <td width="140px" class="headerColCenter_prod">Remarks
                                    </td>
                                    <td width="16px" class="headerColCenter_prod">Delete
                                    </td>

                                </tr>
                            </table>
                        </div>
                        <div id="dvGrid" style="width: 800px; height: 300px; overflow: auto;" class="dvGrid">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:GridView ID="GRDDTLITEMLIST" runat="server" AutoGenerateColumns="False" ShowHeader="False"
                                        CellPadding="1" CellSpacing="1" ForeColor="#333333" GridLines="Both" DataKeyNames="ITEM_ID"
                                        EnableModelValidation="True" ClientIDMode="AutoID" OnRowCommand="GRDDTLITEMLIST_RowCommand" OnRowCreated="GRDDTLITEMLIST_RowCreated" OnRowDataBound="GRDDTLITEMLIST_RowDataBound" OnRowDeleting="GRDDTLITEMLIST_RowDeleting">
                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                        <Columns>


                                            <asp:TemplateField HeaderText="SL#">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSLNo" runat="server" Text='<%# Bind("SLNO") %>' Style="text-align: center;"
                                                        Width="30px"> </asp:Label>

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
                                                                        <asp:HiddenField ID="hdnPROD_DTL_ID" runat="server" Value='<%# Bind("PROD_DTL_ID") %>' />
                                                                    </td>



                                                                    <td>
                                                                        <asp:TextBox ID="txtITEM_NAME" runat="server" CssClass="textBox textAutoSelect" Width="130px" Text='<%# Bind("ITEM_NAME") %>'></asp:TextBox>
                                                                        <asp:HiddenField ID="hdnItemID" runat="server" Value='<%# Bind("ITEM_ID") %>' />
                                                                    </td>
                                                                    <td>
                                                                        <input id="btnITEM_NAME" type="button" value="" runat="server" class="buttonDropdown"
                                                                            tabindex="-1" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtClosingQty" runat="server" CssClass="textBox textAutoSelect" Width="100px" align="right" Text='<%# Bind("CLOSING_QTY") %>' onkeypress="return isNumberKey(event,this);"></asp:TextBox>
                                                                    </td>

                                                                    <%-- <td>
                                                                        <asp:TextBox ID="txtITEM_PANEL_QTY" runat="server" CssClass="textBox textAutoSelect" Width="50px" align="left" Text='<%# Bind("ITEM_PANEL_QTY") %>' onchange="TestOnTextChange(this)" onkeypress=" return isNumberKey(event,this);"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:HiddenField ID="hdnIsUnformed" runat="server" Value='<%# Bind("IS_UNFORMED") %>'/>
                                                                        <asp:CheckBox ID="cbIsUnformed" Width="80px"  runat="server" />
                                                                    </td>--%>
                                                                    <%--   <td>
                                                                        <asp:TextBox ID="txt_BATCH_NO" runat="server" CssClass="textBox textAutoSelect" Width="100px" align="right" Text='<%# Bind("BATCH_NO") %>'></asp:TextBox>
                                                                    </td>--%>


                                                                    <%--<td>
                                                                        <asp:TextBox ID="txtPANEL_UOM_ID" runat="server" CssClass="textBox textAutoSelect" Width="40px" Text='<%#Bind("PANEL_UOM_NAME") %>'></asp:TextBox>
                                                                        <asp:HiddenField ID="hdnPANEL_UOM_ID" runat="server" Value='<%# Bind("PANEL_UOM_ID") %>' />
                                                                        <asp:HiddenField ID="hdnPANEL_PC" runat="server" Value='<%# Bind("PANEL_PC") %>' />
                                                                    </td>
                                                                    <td>
                                                                        <input id="btnPANEL_UOM_ID" type="button" value="" runat="server" class="buttonDropdown" tabindex="-1" />
                                                                    </td>--%>

                                                                    <td>
                                                                        <asp:TextBox ID="txtItem_qty" runat="server" CssClass="textBox textAutoSelect" Width="55px" align="right" Text='<%# Bind("ITEM_QTY") %>' onkeypress="return isNumberKey(event,this);"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:HiddenField ID="hdnSpecificationType" runat="server" Value='<%# Bind("ITEM_SPECIFICATION_ID") %>' />
                                                                        <asp:DropDownList ID="ddlSpecificationType" runat="server" CssClass="dropDownList" Width="135px" AutoPostBack="true" OnSelectedIndexChanged="ddlSpecificationType_SelectedIndexChanged" >
                                                                        </asp:DropDownList>
                                                                    </td>

                                                                    <td>
                                                                        <asp:TextBox ID="txtUOM_NAME" runat="server" CssClass="textBox textAutoSelect" Width="45px" Text='<%# Bind("UOM_NAME") %>'></asp:TextBox>
                                                                        <asp:HiddenField ID="hdnUomID" runat="server" Value='<%# Bind("UOM_ID") %>' />
                                                                    </td>

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
                                                        CommandName="delete" runat="server"> </asp:LinkButton>
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
                        <div id="divGridControls2" style="width: 95%; height: 30px; border-top: solid 1px #C0C0C0; border-bottom: solid 1px #0b07f5;">
                            <table style="width: 89%; height: 70%; text-align: center;" cellspacing="1" cellpadding="1"
                                border="0">
                                <tr>
                                    <td style="width: 20px"></td>
                                    <td style="width: 120px; text-align: right;"></td>
                                    <td align="right" style="text-align: left">&nbsp;
                                    </td>
                                    <td align="right">&nbsp;</td>

                                    <td align="right">&nbsp;</td>
                                    <td align="right">&nbsp;</td>
                                    <td style="width: 80px" align="left">
                                        <asp:Button ID="btnNewRow" runat="server" CssClass="buttonNewRow" Text="" OnClientClick="ShowProgress()" OnClick="btnNewRow_Click" />
                                    </td>
                                    <td style="width: 20px;">
                                        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                                            DisplayAfter="300">
                                            <ProgressTemplate>
                                                <asp:Image ID="imgProgress2" runat="server" ImageUrl="~/image/loader.gif" Height="16px" />
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </td>
                                </tr>


                            </table>
                        </div>
                    </div>


                    <%-- <div id="Div3" runat="server" class="" style="width: 100%; text-align: left;">
                        <span style="font-weight: bold; font-size: 15px; color: #ff3b00;">Used Raw Materials Details: </span>
                    </div>--%>

                    <%--                    <div id="Div1" runat="server" class="" style="width: auto; height: auto; text-align: left">
                        <div id="dvGridHeaderClosing" style="width: 1060px; height: 25px; font-size: smaller;" class="subHeader_Prod">
                            <table style="height: 80%; color: #FFFFFF; font-weight: bold; text-align: left;"
                                class="defFont" cellspacing="1" cellpadding="1">
                                <tr class="headerRow_Prod">
                                    <td width="65px" class="headerColCenter_prod">SL#
                                    </td>
                                    <td width="135px" class="headerColCenter_prod">Item
                                    </td>
                                    <td width="15px" class="headerColLeft"></td>
                                    <td width="70px" class="headerColCenter_prod">UOM
                                    </td>
                                    <td width="100px" class="headerColCenter_prod">OP Stock
                                    </td>
                                    <td width="100px" class="headerColCenter_prod">Closing Qty
                                    </td>
                                   

                                    <td width="100px" class="headerColCenter_prod">Used Qty
                                    </td>

                                    <td width="150px" class="headerColCenter_prod">Remarks
                                    </td>
                                    <td width="16px" class="headerColCenter">Delete
                                    </td>

                                </tr>
                            </table>
                        </div>
                        <div id="dvGridClosing" style="width: 1040px; height: 100px; overflow: auto;" class="dvGrid">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:GridView ID="grdClosingRowMaterial" runat="server" AutoGenerateColumns="False" ShowHeader="False"
                                        CellPadding="1" CellSpacing="1" ForeColor="#333333" GridLines="Both" DataKeyNames="CLOSING_ITEM_ID"
                                        EnableModelValidation="True" ClientIDMode="AutoID" OnRowCommand="grdClosingRowMaterial_RowCommand" OnRowCreated="grdClosingRowMaterial_RowCreated" OnRowDataBound="grdClosingRowMaterial_RowDataBound" OnRowDeleting="grdClosingRowMaterial_RowDeleting">
                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                        <Columns>


                                            <asp:TemplateField HeaderText="SL#">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCLOSING_SI" runat="server" Text='<%# Bind("CLOSING_SI") %>' Style="text-align: center;"
                                                        Width="60px"> </asp:Label>

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
                                                                        <asp:TextBox ID="txtCLOSINGITEM_NAME" runat="server" CssClass="textBox textAutoSelect" Width="135px" Text='<%# Bind("CLOSINGITEM_NAME") %>'></asp:TextBox>
                                                                        <asp:HiddenField ID="hdnCLOSING_ITEM_ID" runat="server" Value='<%# Bind("CLOSING_ITEM_ID") %>' />
                                                                    </td>
                                                                    <td>
                                                                        <input id="btnCLOSINGITEM_NAME" type="button" value="" runat="server" class="buttonDropdown" tabindex="-1" />
                                                                    </td>
                                                                    <td>

                                                                        <asp:TextBox ID="txtCLOSING_UOM_NAME" runat="server" CssClass="textBox textAutoSelect" Width="65px" Text='<%# Bind("CLOSING_UOM_NAME") %>'></asp:TextBox>
                                                                        <asp:HiddenField ID="hdnCLOSING_UOM_ID" runat="server" Value='<%# Bind("CLOSING_UOM_ID") %>' />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtSYSTEM_OPENING_STOCK" runat="server" CssClass="textBox textAutoSelect" Width="100px" align="right" Text='<%# Bind("SYSTEM_OPENING_STOCK") %>' onkeypress="return isNumberKey(event,this);"></asp:TextBox>
                                                                    </td>

                                                                    <td>
                                                                        <asp:TextBox ID="txtCLOSING_QTY" runat="server" CssClass="textBox textAutoSelect" Width="100px" align="right" Text='<%# Bind("CLOSING_QTY") %>' onchange="calcIssueStock(this)"></asp:TextBox>
                                                                    </td>

                                                                    <td>
                                                                        <asp:TextBox ID="txtISSUE_STOCK" runat="server" CssClass="textBox textAutoSelect" Width="100" align="right" Text='<%# Bind("ISSUE_STOCK") %>' onchange="calcCloseingStock(this)" onkeypress="return isNumberKey(event,this);"></asp:TextBox>
                                                                    </td>




                                                                    <td>
                                                                        <asp:TextBox ID="txtCLOSING_REMARKS" runat="server" CssClass="textBox textAutoSelect" Width="140px" Text='<%# Bind("CLOSING_REMARKS") %>' Style=""></asp:TextBox>
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
                                                        CommandName="delete" runat="server"> </asp:LinkButton>
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
                        <div id="divGridControlsClosing" style="width: 90%; height: 30px; border-top: solid 1px #C0C0C0;">
                            <table style="width: 90%; height: 100%; text-align: center;" cellspacing="1" cellpadding="1"
                                border="0">
                                <tr>
                                    <td style="width: 2px"></td>

                                    <td style="width: 160px; text-align: right;">
                                        <%-- <asp:TextBox ID="txtTotQty" runat="server" CssClass="textBox" Style="text-align: right;"
                                        Width="100" TabIndex="-1" Font-Bold="True"></asp:TextBox>--%>
                    <%-- </td>
                                    <td align="right" style="text-align: left">
                                      
                                    </td>
                                    <td align="right">&nbsp;</td>
                                    <td align="right">&nbsp;</td>
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
                        </div>--%>
                    <%-- </div>--%>
                </div>


            </div>
        </div>
        <div id="dvContentFooter" class="dvContentFooter">
            <table>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="btnAddNew" runat="server" Text="New" CssClass="buttonNew" OnClick="btnAddNew_Click" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonCancel checkIsDirty" OnClick="btnCancel_Click" />
                    </td>
                    <td>
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonSave checkRequired" AccessKey="s" OnClientClick="if ( ! UserDeleteConfirmation()) return false;" OnClick="btnSave_Click" />
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="buttonEdit" OnClick="btnEdit_Click" />
                    </td>
                    <td>
                        <asp:Button ID="btnAuthorize" runat="server" Text="Authorize" CssClass="buttoncommon" OnClientClick="if ( ! UserAuthorizeConfirmation()) return false;" OnClick="btnAuthorize_Click" />
                    </td>
                    <td>
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="buttonDelete" Visible="false" />
                    </td>
                    <td>
                        <asp:Button ID="btnRefresh" runat="server" Text="Refresh" CssClass="buttonRefresh checkIsDirty" Visible="false" />
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
                        <%--<asp:Label ID="Label2" runat="server" Text="Print Format:"></asp:Label>--%>
                    </td>
                    <td>
                        <%--<asp:DropDownList ID="ddlReportFormat" runat="server" CssClass="dropDownList" Width="100px">
                        </asp:DropDownList>--%>
                    </td>
                    <td></td>
                </tr>
            </table>
        </div>






    </div>
</asp:Content>
