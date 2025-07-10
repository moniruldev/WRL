<%@ Page Title="" Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="IB_Entry.aspx.cs" Inherits="PG.Web.Production.IB_Entry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../javascript/jquery.ui.combogrid.js" type="text/javascript"></script>

    <script src="../javascript/jquery.attributeobserver.js" type="text/javascript"></script>
    <link href="../css/jquery.ui.combogrid.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        var isPageResize = true;
        ContentForm.CalendarImageURL = "../image/calendar.png";
        var hdnCompanyID = '<%=hdnCompanyID.ClientID%>';

        var ItemListServiceLink = '<%=this.ItemListServiceLink%>';
        var ItemGroupListServiceLink = '<%=this.ItemGroupListServiceLink%>';
        var BOMItemListServiceLink = '<%=this.BOMItemListServiceLink%>';
        var BOMNameListServiceLink = '<%=this.BOMItemNameListServiceLink%>';
        var BOMListServiceLink = '<%= this.BOMListServiceLink%>';
        var PanelUOMServiceLink = '<%= this.PanelUOMServiceLink %>';
        var MachineListServiceLink = '<%= this.MachineListServiceLink%>';
        var SupporvisorListServiceLink = '<%= this.SupporvisorListServiceLink%>';

        //Cutting Part
        var updatePanelCutting = '<%=updatePanelCutting.ClientID%>';
        var UpdateProgressCutting = '<%=UpdateProgressCutting.ClientID%>';
        var gvCuttingID = '<%=gvCuttingID.ClientID%>';

        //Filling Part
        var updatePanelFilling = '<%=updatePanelFilling.ClientID%>';
        var UpdateProgressFilling = '<%=UpdateProgressFilling.ClientID%>';
        var gvdFillingID = '<%=gvdFillingID.ClientID%>';

        //Sulfation Part
        var updatePanelSulfation = '<%=updatePanelSulfation.ClientID%>';
        var UpdateProgressSulfation = '<%=UpdateProgressSulfation.ClientID%>';
        var gvSulfationID = '<%=gvSulfationID.ClientID%>';

        //Closing Part
        var updatePanelClosing = '<%=updatePanelClosing.ClientID%>';
        var UpdateProgressClosing = '<%=UpdateProgressClosing.ClientID%>';
        var gvClosingID = '<%=gvClosingID.ClientID%>';



        //Supervisor Service

        var txtSUPERVISOR_NAME = '<%= txtSUPERVISOR_NAME.ClientID%>';
        var btnSUPERVISOR_ID = '<%=btnSUPERVISOR_ID.ClientID%>';
        var hdnSUPERVISOR_ID = '<%=hdnSUPERVISOR_ID.ClientID%>';


      

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

        function ShowCuttingProgress() {
            $('#' + UpdateProgressCutting).show();
        }

        function ShowFillingProgress() {
            $('#' + UpdateProgressFilling).show();
        }

        function ShowSulfationProgress() {
            $('#' + UpdateProgressSulfation).show();
        }

        function ShowClosingProgress() {
            $('#' + UpdateProgressClosing).show();
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
                    if (panels[i].id == updatePanelCutting) {
                        bindCuttingItemList(gvCuttingID);
                        bindUsedItemList(gvCuttingID);
                    }
                    if (panels[i].id ==updatePanelFilling) {                      
                        bindFillingItemList(gvdFillingID);                       
                    }

                    if (panels[i].id == updatePanelSulfation) {
                        //alert(panels[i].id);
                        bindSulfationItemList(gvSulfationID);
                    }

                    if (panels[i].id == updatePanelClosing) {                      
                        bindClosingItemList(gvClosingID);
                    }
                }
            });
          
            bindSupporvisorList();
            bindCuttingItemList(gvCuttingID);
            bindUsedItemList(gvCuttingID)
            bindFillingItemList(gvdFillingID);
            bindSulfationItemList(gvSulfationID);
            bindClosingItemList(gvClosingID);
        });

        //cutting item service

        function bindCuttingItemList(gvCuttingID) {
            var cgColumns = [{ 'columnName': 'itemname', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                             , { 'columnName': 'uomname', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Uom' }
                             //, { 'columnName': 'itemcode', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Code' }
                             //, { 'columnName': 'closing_qty', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Cls Qty' }
                             , { 'columnName': 'itemgroupdesc', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Group Name' }
                             //, { 'columnName': 'class_name', 'width': '120', 'align': 'left', 'highlight': 4, 'label': 'Class Name' }
                             //, { 'columnName': 'item_type', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Item Type' }
            ];

            //var serviceURL = ItemListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            var serviceURL = BOMNameListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            //serviceURL += "&companyid=" + companyid; &isFinished=N
            serviceURL += "&ispaging=1&deptid=18&isFinished=N&groupid=23";
            var gridSelector = "#" + gvCuttingID;
            $(gridSelector).find('input[id$="txtITEM_NAME"]').each(function (index, elem) {
                ///list click
                var elemRow = $(elem).closest('tr.gridRow');
                //var hdnItemIDElem = $(elemRow).find('input[id$="txtCLOSINGITEM_NAME"]');
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
                    width: 500,
                    url: serviceURL,
                    search: function (event, ui) {
                        var elemRowCur = $(elem).closest('tr.gridRow');                     
                        var newServiceURL = serviceURL;
                       
                        newServiceURL = JSUtility.AddTimeToQueryString(newServiceURL);
                        $(this).combogrid("option", "url", newServiceURL);
                    },

                    select: function (event, ui) {
                        elemID = $(elem).attr('id');
                        if (!ui.item) {
                            event.preventDefault();
                            ClearCuttingItemData(elemID);
                            return false;
                        }
                        if (ui.item.id == 0) {
                            event.preventDefault();
                            return false;
                        }
                        else {
                            SetCuttingItemData(elemID, ui.item);
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
                    }
                    else {

                        if (grp == null) {
                            ClearCuttingItemData(elemID);
                        }
                        else {
                            SetCuttingItemData(elemID, grp);
                        }
                    }
                });

            });

        }

        function ClearCuttingItemData(txtITEM_NAME) {
            var detRow = $('#' + txtITEM_NAME).closest('tr.gridRow');
            $(detRow).find('input[id$="hdnItemID"]').val('0');
            $(detRow).find('input[id$="txtITEM_NAME"]').val('');
            $(detRow).find('input[id$="hdnUomID"]').val('0');
            $(detRow).find('input[id$="txtUOM_NAME"]').val('');
            $(detRow).find('input[id$="txtITEM_STANDARD_WEIGHT_KG"]').val('');
            $(detRow).find('input[id$="hdnITEM_STANDARD_WEIGHT_KG"]').val('');
            $(detRow).find('input[id$="txtITEM_WEIGHT"]').val('');
        }
        function SetCuttingItemData(txtITEM_NAME, data) {
            var detRow = $('#' + txtITEM_NAME).closest('tr.gridRow');
            $(detRow).find('input[id$="hdnItemID"]').val(data.itemid);
            $(detRow).find('input[id$="txtITEM_NAME"]').val(data.itemname);
            $(detRow).find('input[id$="txtITEM_STANDARD_WEIGHT_KG"]').val(data.itemstandardweightkg);
            $(detRow).find('input[id$="hdnITEM_STANDARD_WEIGHT_KG"]').val(data.itemstandardweightkg);
            $(detRow).find('input[id$="hdnUomID"]').val(data.uomid);
            $(detRow).find('input[id$="txtUOM_NAME"]').val(data.uomname);
            if (data.itemid == '6754')
            {
                $(detRow).find('input[id$="txtUSED_ITEM_NAME"]').val('Antimony Lead-1.80%');
                $(detRow).find('input[id$="hdnUSED_ITEM_ID"]').val('6754');
            }
            else
            {
                $(detRow).find('input[id$="txtUSED_ITEM_NAME"]').val('Antimony Lead-2.50%');
                $(detRow).find('input[id$="hdnUSED_ITEM_ID"]').val('3');
            }
               
            var pcqty = $(detRow).find('input[id$="txtItem_qty"]').val();
            var standardweight = $(detRow).find('input[id$="hdnITEM_STANDARD_WEIGHT_KG"]').val();
            var tweight = pcqty * standardweight;
            $(detRow).find('input[id$="txtITEM_WEIGHT"]').val(tweight);
        }


        //Filling item service

        function bindFillingItemList(gvdFillingID) {
            var cgColumns = [{ 'columnName': 'itemname', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                             , { 'columnName': 'uomname', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Uom Name' }
                             , { 'columnName': 'itemcode', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Code' }
                             , { 'columnName': 'closing_qty', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Cls Qty' }
                             , { 'columnName': 'itemgroupdesc', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Group Name' }
                             , { 'columnName': 'class_name', 'width': '120', 'align': 'left', 'highlight': 4, 'label': 'Class Name' }
                             , { 'columnName': 'item_type', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Item Type' }
            ];

            //   var serviceURL = ItemListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            var serviceURL = BOMNameListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            //serviceURL += "&companyid=" + companyid; 
            serviceURL += "&ispaging=1&deptid=18&isFinished=Y";
            var gridSelector = "#" + gvdFillingID;
            $(gridSelector).find('input[id$="txtITEM_NAME"]').each(function (index, elem) {
                ///list click
                var elemRow = $(elem).closest('tr.gridRow');
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
                    width: 800,
                    url: serviceURL,
                    search: function (event, ui) {
                        var elemRowCur = $(elem).closest('tr.gridRow');
                        var newServiceURL = serviceURL;
                        newServiceURL = JSUtility.AddTimeToQueryString(newServiceURL);
                        $(this).combogrid("option", "url", newServiceURL);
                    },

                    select: function (event, ui) {
                        elemID = $(elem).attr('id');
                        if (!ui.item) {
                            event.preventDefault();
                            ClearFillingItemData(elemID);
                            return false;
                        }
                        if (ui.item.id == 0) {
                            event.preventDefault();
                            return false;
                        }
                        else {
                            SetFillingItemData(elemID, ui.item);
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
                    }
                    else {

                        if (grp == null) {
                            ClearFillingItemData(elemID);
                        }
                        else {
                            SetFillingItemData(elemID, grp);
                        }
                    }
                });

            });

        }

        function ClearFillingItemData(txtITEM_NAME) {
            var detRow = $('#' + txtITEM_NAME).closest('tr.gridRow');
            $(detRow).find('input[id$="hdnItemID"]').val('0');
            $(detRow).find('input[id$="txtITEM_NAME"]').val('');
            $(detRow).find('input[id$="txtITEM_STD_PASTE_KG"]').val('0');
            $(detRow).find('input[id$="hdnUomID"]').val('0');
            $(detRow).find('input[id$="txtUOM_NAME"]').val('');
        }
        function SetFillingItemData(txtITEM_NAME, data) {
            var detRow = $('#' + txtITEM_NAME).closest('tr.gridRow');
            $(detRow).find('input[id$="hdnItemID"]').val(data.itemid);
            $(detRow).find('input[id$="txtITEM_NAME"]').val(data.itemname);
            $(detRow).find('input[id$="txtITEM_STD_PASTE_KG"]').val(data.past_pc_kg);
            $(detRow).find('input[id$="hdnUomID"]').val(data.uomid);
            $(detRow).find('input[id$="txtUOM_NAME"]').val(data.uomname);
        }



        //Sulfation item service

        function bindSulfationItemList(gvSulfationID) {
            var cgColumns = [{ 'columnName': 'itemname', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                             , { 'columnName': 'uomname', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Uom Name' }
                             , { 'columnName': 'itemcode', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Code' }
                             , { 'columnName': 'closing_qty', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Cls Qty' }
                             , { 'columnName': 'itemgroupdesc', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Group Name' }
                             , { 'columnName': 'class_name', 'width': '120', 'align': 'left', 'highlight': 4, 'label': 'Class Name' }
                             , { 'columnName': 'item_type', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Item Type' }
            ];

            var serviceURL = ItemListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            //serviceURL += "&companyid=" + companyid; &isFinished=N
            serviceURL += "&ispaging=1&deptid=18&isFinished=Y";
            var gridSelector = "#" + gvSulfationID;
            $(gridSelector).find('input[id$="txtITEM_NAME"]').each(function (index, elem) {
                ///list click
                var elemRow = $(elem).closest('tr.gridRow');
                //var hdnItemIDElem = $(elemRow).find('input[id$="txtCLOSINGITEM_NAME"]');
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
                    width: 800,
                    url: serviceURL,
                    search: function (event, ui) {
                        var elemRowCur = $(elem).closest('tr.gridRow');
                        var newServiceURL = serviceURL;
                        newServiceURL = JSUtility.AddTimeToQueryString(newServiceURL);
                        $(this).combogrid("option", "url", newServiceURL);
                    },

                    select: function (event, ui) {
                        elemID = $(elem).attr('id');
                        if (!ui.item) {
                            event.preventDefault();
                            ClearSulfationItemData(elemID);
                            return false;
                        }
                        if (ui.item.id == 0) {
                            event.preventDefault();
                            return false;
                        }
                        else {
                            SetSulfationItemData(elemID, ui.item);
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
                    }
                    else {

                        if (grp == null) {
                            ClearSulfationItemData(elemID);
                        }
                        else {
                            SetSulfationItemData(elemID, grp);
                        }
                    }
                });

            });

        }

        function ClearSulfationItemData(txtITEM_NAME) {
            var detRow = $('#' + txtITEM_NAME).closest('tr.gridRow');
            $(detRow).find('input[id$="hdnItemID"]').val('0');
            $(detRow).find('input[id$="txtITEM_NAME"]').val('');

            $(detRow).find('input[id$="hdnUomID"]').val('0');
            $(detRow).find('input[id$="txtUOM_NAME"]').val('');
        }
        function SetSulfationItemData(txtITEM_NAME, data) {
            var detRow = $('#' + txtITEM_NAME).closest('tr.gridRow');
            $(detRow).find('input[id$="hdnItemID"]').val(data.itemid);
            $(detRow).find('input[id$="txtITEM_NAME"]').val(data.itemname);

            $(detRow).find('input[id$="hdnUomID"]').val(data.uomid);
            $(detRow).find('input[id$="txtUOM_NAME"]').val(data.uomname);
        }




        //Closing item service

        function bindClosingItemList(gvClosingID) {
            var cgColumns = [{ 'columnName': 'itemname', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                             , { 'columnName': 'uomname', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Uom Name' }
                             , { 'columnName': 'itemcode', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Code' }
                             , { 'columnName': 'closing_qty', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Cls Qty' }
                             , { 'columnName': 'itemgroupdesc', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Group Name' }
                             , { 'columnName': 'class_name', 'width': '120', 'align': 'left', 'highlight': 4, 'label': 'Class Name' }
                             , { 'columnName': 'item_type', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Item Type' }
            ];

            var serviceURL = ItemListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            //serviceURL += "&companyid=" + companyid; &isFinished=N
            serviceURL += "&ispaging=1&deptid=18";
            var gridSelector = "#" + gvClosingID;
            $(gridSelector).find('input[id$="txtCLOSINGITEM_NAME"]').each(function (index, elem) {
                ///list click
                var elemRow = $(elem).closest('tr.gridRow');
                //var hdnItemIDElem = $(elemRow).find('input[id$="txtCLOSINGITEM_NAME"]');
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
                        var newServiceURL = serviceURL;
                        newServiceURL = JSUtility.AddTimeToQueryString(newServiceURL);
                        $(this).combogrid("option", "url", newServiceURL);
                    },

                    select: function (event, ui) {
                        elemID = $(elem).attr('id');
                        if (!ui.item) {
                            event.preventDefault();
                            ClearClosingItemData(elemID);
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
                    }
                    else {

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

        function ClearClosingItemData(txtITEM_NAME) {
            var detRow = $('#' + txtITEM_NAME).closest('tr.gridRow');
            $(detRow).find('input[id$="hdnCLOSING_ITEM_ID"]').val('0');
            $(detRow).find('input[id$="txtCLOSINGITEM_NAME"]').val('');

            $(detRow).find('input[id$="hdnCLOSING_UOM_ID"]').val('0');
            $(detRow).find('input[id$="txtCLOSING_UOM_NAME"]').val('');
        }
        function SetClosingItemData(txtITEM_NAME, data) {
            var detRow = $('#' + txtITEM_NAME).closest('tr.gridRow');

            $(detRow).find('input[id$="hdnCLOSING_ITEM_ID"]').val(data.itemid);
            $(detRow).find('input[id$="txtCLOSINGITEM_NAME"]').val(data.itemname);

            $(detRow).find('input[id$="hdnCLOSING_UOM_ID"]').val(data.uomid);
            $(detRow).find('input[id$="txtCLOSING_UOM_NAME"]').val(data.uomname);

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

            var serviceURL = ItemListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            serviceURL += "&ispaging=1&deptid=135&isFinished=N&for_production=Y";
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
                        var newServiceURL = serviceURL;//+ "&groupid=" + vgroupid
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
                        serviceURL += "&ispaging=1&deptid=135&isFinished=N&for_production=Y";
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

        function TestOnTextChange(texbx) {
            var detRow = $(texbx).closest('tr.gridRow');
            var pcqty = $(detRow).find('input[id$="txtItem_qty"]').val();
            var standardweight = $(detRow).find('input[id$="hdnITEM_STANDARD_WEIGHT_KG"]').val();
          //  var stdPasteweight = $(detRow).find('input[id$="hdnITEM_STANDARD_WEIGHT_KG"]').val();
            var tweight = pcqty * standardweight;
            $(detRow).find('input[id$="txtITEM_WEIGHT"]').val(tweight);
        }
        

        function CalculationTotalPaste(texbx) {
            var detRow = $(texbx).closest('tr.gridRow');
            var pcqty = $(detRow).find('input[id$="txtItem_qty"]').val();
          //  var standardweight = $(detRow).find('input[id$="hdnITEM_STANDARD_WEIGHT_KG"]').val();
            var stdPasteweight = $(detRow).find('input[id$="txtITEM_STD_PASTE_KG"]').val();
            var tweight = pcqty * stdPasteweight;
            $(detRow).find('input[id$="txtITEM_WEIGHT_PASTE_KG"]').val(Number(tweight).toFixed(2));
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

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="dvPageContent" style="width: 100%; height: 100%;" onkeydown="if(event.keyCode==13){event.keyCode=9; return event.keyCode;  }">
        <asp:HiddenField ID="hdnLoggedInUser" runat="server" />
        <asp:HiddenField ID="hdnPROD_ID" runat="server" />
        <asp:HiddenField ID="hndFORECUSTID" runat="server" />
        <asp:HiddenField ID="hdnSUPERVISOR_ID" runat="server" />
        <asp:HiddenField ID="hdnCompanyID" runat="server" Value="0" />
        <div id="dvContentHeader" class="dvContentHeader">
            <div id="dvHeader" class="dvHeader_Prod" align="left">
                <asp:Label ID="lblHeader" runat="server" Text="IB Entry" CssClass="lblHeader" Font-Bold="true" Font-Size="15px"></asp:Label>
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
                            <asp:TextBox ID="txtSUPERVISOR_NAME" runat="server" CssClass="textBox textAutoSelect " Width="150px" autofocus></asp:TextBox>
                            <input id="btnSUPERVISOR_ID" type="button" value="" runat="server" class="buttonDropdown" style="width: 15px" tabindex="-1" />
                        </td>
                        <td>&nbsp;</td>
                        <td style="text-align: left">&nbsp;</td>


                    </tr>
                    <tr>
                        <td style="text-align: right">

                            <%--<asp:Label ID="lblREFNOMANUAL" runat="server" Text="Ref. No : "></asp:Label>--%>
                            <asp:Label ID="lblProcessCode" runat="server" Text="Batch NO :"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <%--<asp:TextBox ID="txtREF_NO_MANUAL" runat="server" CssClass="textBox" Width="163px" TabIndex="1"></asp:TextBox>--%>
                            <asp:TextBox ID="txtPROD_BATCH_NO" runat="server" CssClass="textBox textNumberOnly " Width="163px" TabIndex="1"></asp:TextBox>
                            <asp:TextBox ID="txtBATCH_ID" runat="server" CssClass="textBox" Style="text-align: left; display: none" Width="150px"></asp:TextBox>
                        </td>
                        <td style="text-align: right">
                            <asp:Label ID="lblFORECUSTID" runat="server" Text="Forecust Month : "></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFORECUSTMONTH" runat="server" Style="display: none" CssClass="colourdisabletextBox" Enabled="false" TabIndex="5" Width="163px"></asp:TextBox>
                            <asp:DropDownList ID="ddlFORECUSTMONTH" CssClass="dropDownList required" runat="server" Width="190px" TabIndex="2"></asp:DropDownList>
                        </td>
                        <td style="text-align: right">
                            <asp:Label ID="lblSHIFT_ID" runat="server" Text="Shift : "></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlSHIFT_ID" runat="server" CssClass="dropDownList" Width="155px" TabIndex="3" OnSelectedIndexChanged="ddlSHIFT_ID_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>

                        </td>
                        <td></td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: right">Batch Start Time : </td>
                        <td>
                            <asp:TextBox ID="txtBATCH_STARTTIME" runat="server" CssClass="textBox textDate   dateParse" Style="text-align: left;" Width="75px" TabIndex="4" MaxLength="11"></asp:TextBox>
                            <asp:TextBox ID="txtBATCH_STARTTIMEs" runat="server" CssClass="textBox" Width="60px" TabIndex="5"></asp:TextBox>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" AcceptAMPM="true" Mask="99:99" MaskType="Time" MessageValidatorTip="true" TargetControlID="txtBATCH_STARTTIMEs" AutoCompleteValue="00:00 AM"></cc1:MaskedEditExtender>
                        </td>
                        <td style="text-align: right">Batch End Time : </td>
                        <td>
                            <asp:TextBox ID="txtBATCH_ENDTIME" runat="server" CssClass="textBox textDate dateParse" Style="text-align: left;" Width="100px" TabIndex="6" MaxLength="11"></asp:TextBox>
                            <asp:TextBox ID="txtBATCH_ENDTIMEs" runat="server" CssClass="textBox" Width="60px" TabIndex="7"></asp:TextBox>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AcceptAMPM="true" Mask="99:99" MaskType="Time" MessageValidatorTip="true" TargetControlID="txtBATCH_ENDTIMEs" AutoCompleteValue="00:00 AM"></cc1:MaskedEditExtender>

                        </td>
                        <td style="text-align: right">
                            <%--<asp:Label ID="lblREJECTED_QTY" runat="server"   Text="Rejected Qty :"></asp:Label>--%>
                            <asp:Label ID="lblPRODUCTION_DATE" runat="server" Text="Production Date :"></asp:Label>
                        </td>
                        <td>
                            <%--<asp:TextBox ID="txtREJECTED_QTY" runat="server" CssClass="textBox textNumberOnly" Width="150px" onkeypress=" return isNumberKey(event,this);" TabIndex="7"></asp:TextBox>--%>
                            <asp:TextBox ID="txtPRODUCTION_DATE" runat="server" CssClass="textBox textDate dateParse" Style="text-align: left;" Width="100px"></asp:TextBox>
                        </td>
                        <td>
                            <%--<asp:CheckBox ID="chkISSULPHATION" runat="server" Checked="true" ForeColor="White" Enabled="false" Text="Sulphation" />--%>
                        </td>
                        <td>&nbsp;</td>

                    </tr>
                </table>
            </div>
            <div id="Div9" runat="server" style="height: 10px; float: left; width: 100%;">
            </div>
            <div id="Div10" runat="server" class="" style="float: right; width: 100%; text-align: left; border-top: solid 1px #0b07f5;">
                <span style="font-weight: bold; font-size: 15px; color: #ff3b00;">Cutting Details: </span>
            </div>
            <div id="dvGridHeader5" style="float: left; width: 1000px; height: 25px; font-size: smaller;" class="subHeader_Prod">
                <table style="height: 80%; color: #FFFFFF; font-weight: bold; text-align: left;"
                    class="defFont" cellspacing="1" cellpadding="1">
                    <tr class="headerRow_Prod">
                        <td width="30px" class="headerColCenter_prod">SL#
                        </td>

                        <td width="200px" class="headerColCenter_prod">Item
                        </td>
                        <td width="15px" class="headerColCenter_prod"></td>
                        <td width="102px" class="headerColCenter_prod">Qty
                        </td>
                        <td width="105px" class="headerColCenter_prod">UOM
                        </td>
                        <td width="30px" class="headerColCenter_prod">STD Weight
                                            </td>
                                            <td width="49px" class="headerColCenter_prod">Item Weight
                                            </td>
                                            <td width="52px" class="headerColCenter_prod">  Weight UOM
                                            </td>
                        <td width="150px" class="headerColCenter_prod">Used Item
                                            </td>
                        <td width="200px" class="headerColCenter_prod">Remarks
                        </td>
                        <td width="16px" class="headerColCenter_prod">Delete
                        </td>

                    </tr>
                </table>
            </div>
            <div id="groupBoxContainer boxShadow2" style="height: auto; width: 100%;">
                <div id="groupDataDetails2" style="width: 100%; height: auto;">

                    <div id="Div11" class=" " runat="server" style="height: auto; text-align: left; width: 100%">

                        <div id="dvGrid2" style="width: 1100px; height: 90px; overflow: auto;" class="dvGrid">
                            <asp:UpdatePanel ID="updatePanelCutting" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:GridView ID="gvCuttingID" runat="server" AutoGenerateColumns="False" ShowHeader="False"
                                        CellPadding="1" CellSpacing="1" ForeColor="#333333" GridLines="Both" DataKeyNames="ITEM_ID"
                                        EnableModelValidation="True" ClientIDMode="AutoID" OnRowCommand="gvCuttingID_RowCommand" OnRowCreated="gvCuttingID_RowCreated" OnRowDataBound="gvCuttingID_RowDataBound" OnRowDeleting="gvCuttingID_RowDeleting">
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
                                                                        <asp:HiddenField ID="hdnPROD_DTL_ID" runat="server" Value='<%# Bind("PROD_DTL_ID") %>' />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtITEM_NAME" runat="server" CssClass="textBox textAutoSelect" Width="195px" Text='<%# Bind("ITEM_NAME") %>'></asp:TextBox>
                                                                        <asp:HiddenField ID="hdnItemID" runat="server" Value='<%# Bind("ITEM_ID") %>' />
                                                                    </td>
                                                                    <td>
                                                                        <input id="btnITEM_NAME" type="button" value="" runat="server" class="buttonDropdown"
                                                                            tabindex="-1" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtItem_qty" runat="server" CssClass="textBox textAutoSelect" Width="100px" BackColor="Khaki" align="right" Text='<%# Bind("ITEM_QTY") %>' onchange="TestOnTextChange(this)" onkeypress="return isNumberKey(event,this);"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtUOM_NAME" runat="server" CssClass="textBox textAutoSelect" Width="100px" Text='<%# Bind("UOM_NAME") %>'></asp:TextBox>
                                                                        <asp:HiddenField ID="hdnUomID" runat="server" Value='<%# Bind("UOM_ID") %>' />
                                                                    </td>

                                                                     <td>
                                                                                 <asp:HiddenField ID="hdnITEM_STANDARD_WEIGHT_KG" runat="server"  value='<%# Bind("ITEM_STANDARD_WEIGHT_KG") %>'></asp:HiddenField>
                                                                                <asp:TextBox ID="txtITEM_STANDARD_WEIGHT_KG" runat="server" CssClass="txtstdweightQty textBox textAutoSelect" Width="40px" align="left" Text='<%# Bind("ITEM_STANDARD_WEIGHT_KG") %>'  onkeypress="return isNumberKey(event,this);"></asp:TextBox>
                                                                            </td>

                                                                             <td>
                                                                                <asp:TextBox ID="txtITEM_WEIGHT" runat="server" CssClass="txtWeightQty textBox textAutoSelect" Width="45px" align="left" Text='<%# Bind("ITEM_WEIGHT") %>'  onkeypress="return isNumberKey(event,this);" BackColor="WhiteSmoke"></asp:TextBox>
                                                                            </td>
                                                                             <td>
                                                                                <asp:TextBox ID="txtWEIGHT_UOM_NAME" runat="server" CssClass="textBox textAutoSelect" Width="48px" Text="Kg" Style=""   ></asp:TextBox>
                                                                                <asp:HiddenField ID="hndWEIGHT_UOM_ID" runat="server" Value="2" />
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtUSED_ITEM_NAME" runat="server" CssClass="textBox textAutoSelect" Width="130px" Text='<%# Bind("USED_ITEM_NAME") %>'></asp:TextBox>
                                                                                <asp:HiddenField ID="hdnUSED_ITEM_ID" runat="server" Value='<%# Bind("USED_ITEM_ID") %>' />
                                                                            </td>
                                                                            <td>
                                                                                <input id="btnUSED_ITEM" type="button" value="" runat="server" class="buttonDropdown" tabindex="-1" />
                                                                            </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtRemarks" runat="server" CssClass="textBox textAutoSelect" Width="200px" Text='<%# Bind("REMARKS") %>' Style=""></asp:TextBox>
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
                                    <input id="Hidden5" type="hidden" runat="server" value="[]" />
                                    <input id="Hidden6" type="hidden" runat="server" value="[]" />
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnCuttingNewRow" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>


                        <div id="divGridControls1" style="width: 100%; height: 30px; border-top: solid 1px #C0C0C0; border-bottom: solid 1px #0b07f5;">
                            <table style="width: 70%; height: 100%; text-align: center;" cellspacing="1" cellpadding="1"
                                border="0">
                                <tr>
                                    <td style="width: 2px"></td>

                                    <td style="width: 160px; text-align: right;"></td>
                                    <td align="right" style="text-align: left">&nbsp;
                                    </td>
                                    <td align="right">&nbsp;</td>
                                    <td align="right">&nbsp;</td>
                                    <td align="right" style="width: 90px">&nbsp;
                                    </td>
                                    <td style="width: 90px" align="left">
                                        <asp:Button ID="btnCuttingNewRow" runat="server" CssClass="buttonNewRow" Text="" OnClientClick="ShowCuttingProgress()" OnClick="btnCuttingNewRow_Click" />
                                    </td>
                                    <td style="width: 20px;">
                                        <asp:UpdateProgress ID="UpdateProgressCutting" runat="server" AssociatedUpdatePanelID="updatePanelCutting"
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

                    <div id="Div12" runat="server" style="background-color: red; width: 100%;">
                    </div>



                </div>
            </div>

            <div id="Div6" runat="server" class="" style="width: 100%; text-align: left; border-top: solid 1px #0b07f5;">
                <span style="font-weight: bold; font-size: 15px; color: #ff3b00;">Filling Details: </span>
            </div>
            <div id="dvGridHeader3" style="width: 900px; height: 25px; font-size: smaller;" class="subHeader_Prod">
                <table style="height: 80%; color: #FFFFFF; font-weight: bold; text-align: left;"
                    class="defFont" cellspacing="1" cellpadding="1">
                    <tr class="headerRow_Prod">
                        <td width="30px" class="headerColCenter_prod">SL#
                        </td>
                        <%--  <td width="140px" class="headerColCenter">Item Group
                                            </td>
                                        <td width="80px" class="headerColCenter_prod"> Machine Name
                                            </td>--%>
                        <td width="200px" class="headerColCenter_prod">Item
                        </td>
                        <td width="15px" class="headerColCenter_prod"></td>
                        <%-- <td width="52px" class="headerColCenter_prod">Panel Qty
                                            </td>
                                             <td width="62px" class="headerColCenter_prod">Panel UOM
                                            </td>
                                           <td width="15px" class="headerColLeft"></td>--%>
                        <td width="100px" class="headerColCenter_prod">Paste Wt/Pc (kg)
                        </td>
                        <td width="102px" class="headerColCenter_prod">Qty
                        </td>
                        <td width="200px" class="headerColCenter_prod">Total Paste Wt(kg) </td>
                        <td width="105px" class="headerColCenter_prod">UOM
                        </td>

                        <td width="200px" class="headerColCenter_prod">Remarks
                        </td>
                        <td width="16px" class="headerColCenter_prod">Delete
                        </td>

                    </tr>
                </table>
            </div>
            <div id="groupBoxContainer boxShadow1" style="height: auto; width: 100%;">
                <div id="groupDataDetails1" style="width: 90%; height: auto;">

                    <div id="Div7" class=" " runat="server" style="height: auto; text-align: left; width: 100%">

                        <div id="dvGrid1" style="width: 1100px; height: 100px; overflow: auto;" class="dvGrid">
                            <asp:UpdatePanel ID="updatePanelFilling" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:GridView ID="gvdFillingID" runat="server" AutoGenerateColumns="False" ShowHeader="False"
                                        CellPadding="1" CellSpacing="1" ForeColor="#333333" GridLines="Both" DataKeyNames="ITEM_ID"
                                        EnableModelValidation="True" ClientIDMode="AutoID" OnRowCommand="gvdFillingID_RowCommand" OnRowCreated="gvdFillingID_RowCreated" OnRowDataBound="gvdFillingID_RowDataBound" OnRowDeleting="gvdFillingID_RowDeleting">
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
                                                                    <%--<td>
                                                                                <asp:TextBox ID="txtGroupName" runat="server" CssClass="textBox textAutoSelect" Width="123" Text='<%# Bind("ITEM_GROUP_DESC") %>'></asp:TextBox>
                                                                                <asp:HiddenField ID="hdngroupId" runat="server" Value='<%# Bind("ITEM_GROUP_ID") %>' />
                                                                            </td>
                                                                            <td>
                                                                                <input id="btnItemGroup" type="button" value="" runat="server" class="buttonDropdown" tabindex="-1" />
                                                                            </td>

                                                                              <td>
                                                                                 <asp:TextBox ID="txtMACHINE_NAME" runat="server" CssClass="textBox textAutoSelect" Width="60px" Text='<%#Bind("MACHINE_NAME") %>'></asp:TextBox>
                                                                                 <asp:HiddenField ID="hndMACHINE_ID" runat="server" Value='<%# Bind("MACHINE_ID") %>' />
                                                                            </td>
                                                                            <td>
                                                                                <input id="btnMACHINE_NAME" type="button" value="" runat="server"  class="buttonDropdown" tabindex="-1" />
                                                                            </td>--%>
                                                                    <td>
                                                                        <asp:HiddenField ID="hdnPROD_DTL_ID" runat="server" Value='<%# Bind("PROD_DTL_ID") %>' />
                                                                    </td>

                                                                    <td>
                                                                        <asp:TextBox ID="txtITEM_NAME" runat="server" CssClass="textBox textAutoSelect" Width="168px" Text='<%# Bind("ITEM_NAME") %>'></asp:TextBox>
                                                                        <asp:HiddenField ID="hdnItemID" runat="server" Value='<%# Bind("ITEM_ID") %>' />
                                                                    </td>
                                                                    <td>
                                                                        <input id="btnITEM_NAME" type="button" value="" runat="server" class="buttonDropdown"
                                                                            tabindex="-1" />
                                                                    </td>
                                                                    <%--<td>
                                                                                <asp:TextBox ID="txtITEM_PANEL_QTY" runat="server" CssClass="textBox textAutoSelect" Width="50px"   align="left" Text='<%# Bind("ITEM_PANEL_QTY") %>' onchange="TestOnTextChange(this)" onkeypress=" return isNumberKey(event,this);"></asp:TextBox>
                                                                            </td>

                                                                             <td>
                                                                                <asp:TextBox ID="txtPANEL_UOM_ID" runat="server" CssClass="textBox textAutoSelect" Width="40px" Text='<%#Bind("PANEL_UOM_NAME") %>'></asp:TextBox>
                                                                                 <asp:HiddenField ID="hdnPANEL_UOM_ID" runat="server" Value='<%# Bind("PANEL_UOM_ID") %>' />
                                                                                  <asp:HiddenField ID="hdnPANEL_PC" runat="server" Value='<%# Bind("PANEL_PC") %>' />
                                                                            </td>
                                                                            <td>
                                                                                <input id="btnPANEL_UOM_ID" type="button" value="" runat="server" class="buttonDropdown" tabindex="-1" />
                                                                            </td>--%>
                                                                    <td>
                                                                       <asp:TextBox ID="txtITEM_STD_PASTE_KG" runat="server" CssClass="textBox textAutoSelect" Width="88px"   align="right" style="text-align : right;" Text='<%# Bind("ITEM_STD_PASTE_KG") %>'  onkeypress=" return isNumberKey(event,this);"></asp:TextBox>
                                                                   </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtItem_qty" runat="server" CssClass="textBox textAutoSelect" Width="88px" BackColor="Khaki" align="right" style="text-align : right;"  Text='<%# Bind("ITEM_QTY") %>' onchange="CalculationTotalPaste(this)" onkeypress="return isNumberKey(event,this);"></asp:TextBox>
                                                                    </td>
                                                                     <td>
                                                                       <asp:TextBox ID="txtITEM_WEIGHT_PASTE_KG" runat="server" CssClass="textBox textAutoSelect" Width="176px"   style="text-align : right;"  align="right" Text='<%# Eval("ITEM_WEIGHT_PASTE_KG", "{0:#,##0.00}") %>'></asp:TextBox>
                                                                   </td>
                                                                    <td>

                                                                        <asp:TextBox ID="txtUOM_NAME" runat="server" CssClass="textBox textAutoSelect" Width="88px" Text='<%# Bind("UOM_NAME") %>'></asp:TextBox>
                                                                        <asp:HiddenField ID="hdnUomID" runat="server" Value='<%# Bind("UOM_ID") %>' />
                                                                    </td>

                                                                    <%--<td>
                                                                                <asp:TextBox ID="txtITEM_WEIGHT" runat="server" CssClass="textBox textAutoSelect" Width="45px" align="left" Text='<%# Bind("ITEM_WEIGHT") %>'  onkeypress="return isNumberKey(event,this);"></asp:TextBox>
                                                                            </td>
                                                                             <td>
                                                                                <asp:TextBox ID="txtWEIGHT_UOM_NAME" runat="server" CssClass="textBox textAutoSelect" Width="42px" Text='<%# Bind("WEIGHT_UOM_NAME") %>' Style=""   ></asp:TextBox>
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

                                                                            <td>
                                                                                <asp:TextBox ID="txtUSED_BAR_PC" runat="server" CssClass="textBox textAutoSelect" Width="48px" Text='<%# Bind("USED_BAR_PC") %>' onchange="CalcBarWeight(this)"  onkeypress="return isNumberKey(event,this);" ></asp:TextBox>
                                                                            </td>--%>

                                                                    <%-- <td>
                                                                                <asp:TextBox ID="txtBAR_TYPE" runat="server" CssClass="textBox textAutoSelect" Width="50px" Text='<%# Bind("BAR_TYPE_NAME") %>'></asp:TextBox>
                                                                                <asp:HiddenField ID="hdnBAR_TYPE" runat="server" Value='<%# Bind("BAR_TYPE") %>' />
                                                                                <asp:HiddenField ID="hdnBAR_WEIGHT" runat ="server" Value='<%# Bind("BAR_WEIGHT") %>' />

                                                                            </td>
                                                                             <td>
                                                                                <input id="btnBAR_TYPE" type="button" value="" runat="server"  class="buttonDropdown" tabindex="-1" />
                                                                            </td>

                                                                            <td>
                                                                                <asp:TextBox ID="txtUSED_QTY_KG"  CssClass="textBox" runat="server"  onkeypress="return isNumberKey(event,this);" Text='<%# Bind("USED_QTY_KG") %>'  Width="45px"></asp:TextBox>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtBOM_Name" runat="server" CssClass="textBox textAutoSelect" Width="129px" Text='<%#Bind("BOM_NAME") %>'></asp:TextBox>
                                                                                 <asp:HiddenField ID="hdnITEM_BOM_ID" runat="server" Value='<%# Bind("BOM_ID") %>' />
                                                                            </td>
                                                                            <td>
                                                                                <input id="btnITEM_BOM_ID" type="button" value="" runat="server" class="buttonDropdown" tabindex="-1"/>
                                                                            </td>--%>
                                                                    <td>
                                                                        <asp:TextBox ID="txtRemarks" runat="server" CssClass="textBox textAutoSelect" Width="175px" Text='<%# Bind("REMARKS") %>' Style=""></asp:TextBox>
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
                                    <input id="Hidden3" type="hidden" runat="server" value="[]" />
                                    <input id="Hidden4" type="hidden" runat="server" value="[]" />
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnFillingNewRow" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>


                        <div id="divGridControls2" style="width: 100%; height: 30px; border-top: solid 1px #C0C0C0; border-bottom: solid 1px #0b07f5;">
                            <table style="width: 70%; height: 100%; text-align: center;" cellspacing="1" cellpadding="1"
                                border="0">
                                <tr>
                                    <td style="width: 2px"></td>

                                    <td style="width: 160px; text-align: right;"></td>
                                    <td align="right" style="text-align: left">&nbsp;
                                    </td>
                                    <td align="right">&nbsp;</td>
                                    <td align="right">&nbsp;</td>
                                    <td align="right" style="width: 90px">&nbsp;
                                    </td>
                                    <td style="width: 90px" align="left">
                                        <asp:Button ID="btnFillingNewRow" runat="server"   CssClass="buttonNewRow" Text="" OnClientClick="ShowFillingProgress()" OnClick="btnFillingNewRow_Click" />
                                    </td>
                                    <td style="width: 20px;">
                                        <asp:UpdateProgress ID="UpdateProgressFilling" runat="server" AssociatedUpdatePanelID="updatePanelFilling"
                                            DisplayAfter="300">
                                            <ProgressTemplate>
                                                <asp:Image ID="imgProgress3" runat="server" ImageUrl="~/image/loader.gif" />
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </td>



                                </tr>


                            </table>
                        </div>
                    </div>

                    <div id="Div8" runat="server" style="background-color: red; width: 100%;">
                    </div>



                </div>
            </div>


            <div id="dvGridSeparator" runat="server" style="height: 10px; float: left; width: 100%;">
            </div>
            <div id="Div5" runat="server" class="" style="float: right; width: 100%; text-align: left; border-top: solid 1px #0b07f5;">
                <span style="font-weight: bold; font-size: 15px; color: #ff3b00;">Sulphation Details: </span>
            </div>
            <div id="dvGridHeader2" style="float: left; width: 950px; height: 25px; font-size: smaller;" class="subHeader_Prod">
                <table style="height: 100%; color: #FFFFFF; font-weight: bold; text-align: left;"
                    class="defFont" cellspacing="1" cellpadding="1">
                    <tr class="headerRow_Prod">
                        <td width="30px" class="headerColCenter_prod">SL#
                        </td>
                        <td width="70px" class="headerColCenter_prod">Filling Batch</td>
                        <td width="215px" class="headerColCenter_prod">Item
                        </td>
                        <td width="20px" class="headerColCenter_prod"></td>
                        <td width="85px" class="headerColCenter_prod">Qty
                        </td>
                        <td width="100px" class="headerColCenter_prod">UOM
                        </td>

                        <td width="90px" class="headerColCenter_prod">Temp.
                        </td>
                        <td width="95px" class="headerColCenter_prod">Sp. Gra.
                        </td>
                        <td width="95px" class="headerColCenter_prod">Start Time
                        </td>
                        <td width="135px" class="headerColCenter_prod">End Date  
                        </td>
                        <td width="105px" class="headerColCenter_prod">End Time
                        </td>
                        <td width="200px" class="headerColCenter_prod">Remarks
                        </td>
                        <td width="16px" class="headerColCenter_prod">Delete
                        </td>

                    </tr>
                </table>
            </div>
            <div id="groupBoxContainer boxShadow" style="height: auto; width: 100%;">
                <div id="groupDataDetails" style="width: 90%; height: auto;">

                    <div id="dvGridContainer2" class=" " runat="server" style="height: auto; text-align: left; width: 100%">

                        <div id="dvGrid" style="width: 950px; height: 100px; overflow: auto;" class="dvGrid">
                            <asp:UpdatePanel ID="updatePanelSulfation" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:GridView ID="gvSulfationID" runat="server" AutoGenerateColumns="False" ShowHeader="False"
                                        CellPadding="1" CellSpacing="1" ForeColor="#333333" GridLines="Both" DataKeyNames="ITEM_ID"
                                        EnableModelValidation="True" ClientIDMode="AutoID" OnRowCommand="gvSulfationID_RowCommand" OnRowCreated="gvSulfationID_RowCreated" OnRowDataBound="gvSulfationID_RowDataBound" OnRowDeleting="gvSulfationID_RowDeleting">
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
                                                                        <asp:TextBox ID="txtFILLING_BATCH" runat="server" style="text-transform:uppercase;" CssClass="textBox textAutoSelect"  Width="48px" Text='<%#Bind("FILLING_BATCH") %>'></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:HiddenField ID="hdnPROD_DTL_ID" runat="server" Value='<%# Bind("PROD_DTL_ID") %>' />
                                                                    </td>

                                                                    <td>
                                                                        <asp:TextBox ID="txtITEM_NAME" runat="server" CssClass="textBox textAutoSelect" Width="133px" Text='<%# Bind("ITEM_NAME") %>'></asp:TextBox>
                                                                        <asp:HiddenField ID="hdnItemID" runat="server" Value='<%# Bind("ITEM_ID") %>' />
                                                                    </td>
                                                                    <td>
                                                                        <input id="btnITEM_NAME" type="button" value="" runat="server" class="buttonDropdown"
                                                                            tabindex="-1" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtItem_qty" runat="server" CssClass="textBox textAutoSelect" BackColor="Khaki" Width="60px" align="right" Text='<%# Bind("ITEM_QTY") %>' onkeypress="return isNumberKey(event,this);"></asp:TextBox>
                                                                    </td>

                                                                    <td>

                                                                        <asp:TextBox ID="txtUOM_NAME" runat="server" CssClass="textBox textAutoSelect" Width="65px" Text='<%# Bind("UOM_NAME") %>'></asp:TextBox>
                                                                        <asp:HiddenField ID="hdnUomID" runat="server" Value='<%# Bind("UOM_ID") %>' />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtTEMPARATURE" runat="server" CssClass="textBox textAutoSelect" Width="72px" align="right" Text='<%# Bind("TEMPARATURE") %>' onkeypress="return isNumberKey(event,this);"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtSULFURIC_GRAVITY" runat="server" CssClass="textBox textAutoSelect" Width="68px" align="right" Text='<%# Bind("SULFURIC_GRAVITY") %>' onkeypress="return isNumberKey(event,this);"></asp:TextBox>
                                                                    </td>


                                                                    <td>
                                                                        <cc1:MaskedEditExtender ID="MaskedEditExtender3" runat="server" AcceptAMPM="true" Mask="99:99" MaskType="Time" MessageValidatorTip="true" TargetControlID="txtSULPHATION_STARTTIME" AutoCompleteValue="00:00AM"></cc1:MaskedEditExtender>
                                                                        <asp:TextBox ID="txtSULPHATION_STARTTIME" runat="server" CssClass="textBox textAutoSelect" Width="68px" align="right" Text='<%# Bind("SULPHATION_STARTTIME") %>'></asp:TextBox>
                                                                    </td>

                                                                    <td style="white-space: nowrap">
                                                                        <asp:TextBox ID="txtSULPHATION_OFFDATE" runat="server" CssClass="textBox textDate dateParse" Style="text-align: left;" Width="70px" Text='<%# Bind("SULPHATION_OFFDATE" ,"{0:dd-MMM-yyyy}") %>'></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <cc1:MaskedEditExtender ID="MaskedEditExtender4" runat="server" AcceptAMPM="true" Mask="99:99" MaskType="Time" MessageValidatorTip="true" TargetControlID="txtSULPHATION_OFFTIME" AutoCompleteValue="00:00AM"></cc1:MaskedEditExtender>
                                                                        <asp:TextBox ID="txtSULPHATION_OFFTIME" runat="server" CssClass="textBox textAutoSelect" Width="72px" align="right" Text='<%# Bind("SULPHATION_OFFTIME") %>'></asp:TextBox>
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
                                    <asp:AsyncPostBackTrigger ControlID="btnSulphationNewRow" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>


                        <div id="divGridControls3" style="width: 100%; height: 30px; border-top: solid 1px #C0C0C0; border-bottom: solid 1px #0b07f5;">
                            <table style="width: 70%; height: 100%; text-align: center;" cellspacing="1" cellpadding="1"
                                border="0">
                                <tr>
                                    <td style="width: 2px"></td>

                                    <td style="width: 160px; text-align: right;"></td>
                                    <td align="right" style="text-align: left">&nbsp;
                                    </td>
                                    <td align="right">&nbsp;</td>
                                    <td align="right">&nbsp;</td>
                                    <td align="right" style="width: 90px">&nbsp;
                                    </td>
                                    <td style="width: 90px" align="left">
                                        <asp:Button ID="btnSulphationNewRow" runat="server" CssClass="buttonNewRow" Text="" OnClientClick="ShowSulfationProgress()" OnClick="btnSulphationNewRow_Click" />
                                    </td>
                                    <td style="width: 20px;">
                                        <asp:UpdateProgress ID="UpdateProgressSulfation" runat="server" AssociatedUpdatePanelID="updatePanelSulfation"
                                            DisplayAfter="300">
                                            <ProgressTemplate>
                                                <asp:Image ID="imgProgress4" runat="server" ImageUrl="~/image/loader.gif" />
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>

                    <div id="Div2" runat="server" style="background-color: red; width: 100%;">
                    </div>
                    <div id="Div3" runat="server" class="" style="width: 100%; text-align: left;">
                        <span style="font-weight: bold; font-size: 15; color: #ff3b00;">Closing Raw Materials Details: </span>
                    </div>

                    <div id="Div1" runat="server" class="" style="width: auto; height: auto; text-align: left; width: 100%">
                        <div id="dvGridHeaderClosing" style="width: 730px; height: 25px; font-size: smaller;" class="subHeader_Prod">
                            <table style="height: 80%; color: #FFFFFF; font-weight: bold; text-align: left;"
                                class="defFont" cellspacing="1" cellpadding="1">
                                <tr class="headerRow_Prod">
                                    <td width="50px" class="headerColCenter_prod">SL#
                                    </td>
                                    <td width="143px" class="headerColCenter_prod">Item
                                    </td>
                                    <td width="15px" class="headerColCenter_prod"></td>
                                    <td width="50px" class="headerColCenter_prod">UOM
                                    </td>
                                    <td width="125px" class="headerColCenter_prod">Op Stock
                                    </td>
                                    <%-- <td width="145px" class="headerColCenter" style="display:none;">Manual Opening Stock
                                                    </td>--%>
                                    <td width="70px" class="headerColCenter_prod">Closing Qty
                                    </td>
                                    <%--
                                    <td width="40px" class="headerColCenter_prod">Wastage Qty
                                    </td>
                                    <td width="40px" class="headerColCenter_prod">Rejected Qty
                                    </td>
                                   <td width="40px" class="headerColCenter_prod"> Pos Dev
                                                    </td>
                                                    <td width="40px" class="headerColCenter_prod"> Neg Dev
                                                    </td>--%>
                                    <td width="50px" class="headerColCenter_prod">Used Qty
                                    </td>
                                    <td width="220px" class="headerColCenter_prod">Remarks
                                    </td>
                                    <td width="16px" class="headerColCenter_prod">Delete
                                    </td>

                                </tr>
                            </table>
                        </div>
                        <div id="dvGridClosing" style="width: auto; height: 100px; overflow: auto;" class="dvGrid">
                            <asp:UpdatePanel ID="updatePanelClosing" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:GridView ID="gvClosingID" runat="server" AutoGenerateColumns="False" ShowHeader="False"
                                        CellPadding="1" CellSpacing="1" ForeColor="#333333" GridLines="Both" DataKeyNames="CLOSING_ITEM_ID"
                                        EnableModelValidation="True" ClientIDMode="AutoID" OnRowCommand="gvClosingID_RowCommand" OnRowCreated="gvClosingID_RowCreated" OnRowDataBound="gvClosingID_RowDataBound" OnRowDeleting="gvClosingID_RowDeleting">
                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                        <Columns>


                                            <asp:TemplateField HeaderText="SL#">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCLOSING_SI" runat="server" Text='<%# Bind("CLOSING_SI") %>' Style="text-align: center;" Width="40px">
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
                                                                        <asp:TextBox ID="txtCLOSINGITEM_NAME" runat="server" CssClass="textBox textAutoSelect" Width="135px" Text='<%# Bind("CLOSINGITEM_NAME") %>'></asp:TextBox>
                                                                        <asp:HiddenField ID="hdnCLOSING_ITEM_ID" runat="server" Value='<%# Bind("CLOSING_ITEM_ID") %>' />
                                                                    </td>
                                                                    <td>
                                                                        <input id="btnCLOSINGITEM_NAME" type="button" value="" runat="server" class="buttonDropdown" tabindex="-1" />
                                                                    </td>
                                                                    <td>

                                                                        <asp:TextBox ID="txtCLOSING_UOM_NAME" runat="server" CssClass="textBox textAutoSelect" Width="44px" Text='<%# Bind("CLOSING_UOM_NAME") %>'></asp:TextBox>
                                                                        <asp:HiddenField ID="hdnCLOSING_UOM_ID" runat="server" Value='<%# Bind("CLOSING_UOM_ID") %>' />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtSYSTEM_OPENING_STOCK" runat="server" CssClass="textBox textAutoSelect" Width="110px" align="right" Text='<%# Bind("SYSTEM_OPENING_STOCK") %>' onkeypress="return isNumberKey(event,this);"></asp:TextBox>
                                                                    </td>

                                                                    <%-- <td >
                                                                                        <asp:TextBox ID="txtMANUAL_OPENING_STOCK" runat="server" CssClass="textBox textAutoSelect" Width="127" align="right" style="display:none;" onchange="calcIssueStock(this)" Text='<%# Bind("MANUAL_OPENING_STOCK") %>'  onkeypress="return isNumberKey(event,this);"></asp:TextBox>
                                                                                    </td> onkeypress="return isNumberKey(event,this);" --%>



                                                                    <td>
                                                                        <asp:TextBox ID="txtCLOSING_QTY" runat="server" CssClass="textBox textAutoSelect" Width="65px" align="right" Text='<%# Bind("CLOSING_QTY") %>' onchange="calcIssueStock(this)" onkeypress="return isNumberKey(event,this);"></asp:TextBox>
                                                                    </td>

                                                                      <%-- <td>
                                                                        <asp:TextBox ID="txtWASTAGE_QTY" runat="server" CssClass="textBox textAutoSelect" Width="45px" align="right" Text='<%# Bind("WASTAGE_QTY") %>' onkeypress="return isNumberKey(event,this);"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtREJECTED_QTY" runat="server" CssClass="textBox textAutoSelect" Width="45px" align="right" Text='<%# Bind("REJECTED_QTY") %>' onkeypress="return isNumberKey(event,this);"></asp:TextBox>
                                                                    </td>
                                                                  <td>
                                                                                        <asp:TextBox ID="txtPOSITIVE_DEV" runat="server" CssClass="textBox textAutoSelect"   Width="70" align="right" Text='<%# Bind("POSITIVE_DEV") %>' onkeypress="return isNumberKey(event,this);" ></asp:TextBox>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtNEGATIVE_DEV" runat="server" CssClass="textBox textAutoSelect"   Width="70" align="right" Text='<%# Bind("NEGATIVE_DEV") %>' onkeypress="return isNumberKey(event,this);" ></asp:TextBox>
                                                                                    </td>--%>
                                                                    <td>
                                                                        <asp:TextBox ID="txtISSUE_STOCK" runat="server" CssClass="textBox textAutoSelect" BackColor="Khaki" Width="45px" align="right" Text='<%# Bind("ISSUE_STOCK") %>' onchange="calcCloseingStock(this)" onkeypress="return isNumberKey(event,this);"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtCLOSING_REMARKS" runat="server" CssClass="textBox textAutoSelect" Width="200px" Text='<%# Bind("CLOSING_REMARKS") %>' Style=""></asp:TextBox>
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
                        <div id="divGridControlsClosing" style="width: 100%; height: 30px; border-top: solid 1px #C0C0C0;">
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
                                        <asp:Button ID="btnNewRowClosing" runat="server" CssClass="buttonNewRow" Text="" OnClientClick="ShowClosingProgress()" OnClick="btnNewRowClosing_Click" />
                                    </td>
                                    <td style="width: 20px;">
                                        <asp:UpdateProgress ID="UpdateProgressClosing" runat="server" AssociatedUpdatePanelID="updatePanelClosing"
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
                        <asp:Button ID="btnAddNew" runat="server" Text="New" CssClass="buttonNew" OnClick="btnAddNew_Click" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonCancel checkIsDirty" OnClick="btnAddNew_Click" />
                    </td>
                    <td>
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonSave checkRequired" AccessKey="s" OnClientClick="if ( ! UserSaveConfirmation()) return false;" OnClick="btnSave_Click" />
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="buttonEdit" OnClick="btnEdit_Click" />
                    </td>

                    <td>
                        <asp:Button ID="btnAuthorize" runat="server" Text="Authorize" CssClass="buttoncommon" OnClientClick="if ( ! UserAuthorizeConfirmation()) return false;" OnClick="btnAuthorize_Click" />
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
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
            </table>
        </div>










    </div>
</asp:Content>
