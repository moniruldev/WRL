<%@ Page Title="" Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="BOMEntryDeptWise.aspx.cs" Inherits="PG.Web.Production.BOMEntryDeptWise" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../javascript/jquery.ui.combogrid.js" type="text/javascript"></script>
    <script src="../javascript/jquery.attributeobserver.js" type="text/javascript"></script>
    <link href="../css/jquery.ui.combogrid.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        var isPageResize = true;
        ContentForm.CalendarImageURL = "../image/calendar.png";

        var hdnCompanyID = '<%=hdnCompanyID.ClientID%>';
          var ItemGroupListServiceLink = '<%=this.ItemGroupListServiceLink%>';


        var ItemListServiceLink = '<%=this.ItemListServiceLink%>';
        var BOMItemListServiceLink = '<%=this.BOMItemListServiceLink%>';
        var BOMListServiceLink = '<%= this.BOMListServiceLink%>';

        var gridUpdatePanelIDDet = '<%=UpdatePanel1.ClientID%>';
        var gridViewIDDet = '<%=GRDDTLITEMLIST.ClientID%>';
        var updateProgressID = '<%=UpdateProgress2.ClientID%>';
        var txtPACKAGE_NAME = '<%=txtPACKAGE_NAME.ClientID%>';
        var txtBOMITEMNAME = '<%=txtBOMITEMNAME.ClientID%>';

        var btnBOMITEMID = '<%=btnBOMITEMID.ClientID%>';
        var hdnBOMITEMID = '<%=hdnBOMITEMID.ClientID%>';
        var ddlFromDepartment ='<%= ddlFromDepartment.ClientID %>';
        var hdnFromDepartment = '<%= hdnFromDepartment.ClientID %>';
      

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

        function onlyNoDecimals(e, t) {
            try {
                if (window.event) {
                    var charCode = window.event.keyCode;
                }
                else if (e) {
                    var charCode = e.which;
                }
                else { return true; }
                if (charCode > 31 && (charCode < 48 || charCode > 57 || charCode != 110)) {
                    return false;
                }
                return true;
            }
            catch (err) {
                alert(err.Description);
            }
        }

        function ShowProgress() {
            $('#' + updateProgressID).show();
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
                        bindItemGroupList(gridViewIDDet);
                        bindBOMItemList(gridViewIDDet);
                    }
                }
            });

            if ($('#' + txtBOMITEMNAME).is(':visible')) {

                bindBatteryTypeList();
            }
            // alert('OK 1');
            bindItemGroupList(gridViewIDDet);
            bindItemList(gridViewIDDet);
            bindBOMItemList(gridViewIDDet);
       

        });


        //var BATTERY_TYPE_IDElem = $('#' + txtBATTERY_TYPE_NAME);
        //$('#' + btnBATTERY_TYPE_ID).click(function (e) {
        //    $(BATTERY_TYPE_IDElem).combogrid("dropdownClick");
        //});



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


        function bindBatteryTypeList() {
            var cgColumns = [{ 'columnName': 'itemname', 'width': '170', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                              , { 'columnName': 'uomname', 'width': '70', 'align': 'left', 'highlight': 4, 'label': 'Uom' }
                              , { 'columnName': 'itemgroupdesc', 'width': '120', 'align': 'left', 'highlight': 4, 'label': 'Group Name' }
                              , { 'columnName': 'class_name', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Class Name' }
            ];

            var serviceURL = BOMItemListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            serviceURL += "&ispaging=1&isCompositeItem=2&isFinished=Y&forProduction=Y&isbom=1";
            var BATTERY_TYPE_NAMENameElem = $('#' + txtBOMITEMNAME);
            
            $('#' + btnBOMITEMID).click(function (e) {
                $(BATTERY_TYPE_NAMENameElem).combogrid("dropdownClick");
            });

            $(BATTERY_TYPE_NAMENameElem).combogrid({
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
                    var e = document.getElementById("<%=ddlFromDepartment.ClientID%>");
                    var dept_id = e.options[e.selectedIndex].value;
                    
                    var newServiceURL = serviceURL + "&deptid=" + dept_id;
                    
                    //var newServiceURL = serviceURL;
                    $(this).combogrid("option", "url", newServiceURL);


                },
                select: function (event, ui) {
                    if (!ui.item) {
                        event.preventDefault();
                        $('#' + hdnBOMITEMID).val('');
                        return false;
                    }


                    if (ui.item.seid == '') {
                        event.preventDefault();
                        return false;
                    }
                    else {
                        
                        $('#' + hdnBOMITEMID).val(ui.item.itemid);
                        $('#' + txtBOMITEMNAME).val(ui.item.itemname);
                        //document.getElementById("<%=ddlFromDepartment.ClientID%>").disabled = true;
                    }
                    return false;
                },

                lc: ''
            });


            $(BATTERY_TYPE_NAMENameElem).blur(function () {
                var self = this;
                var e = document.getElementById("<%=ddlFromDepartment.ClientID%>");
                var dept_id = e.options[e.selectedIndex].value;

                var serviceURL = BOMItemListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
                serviceURL += "&ispaging=1&isCompositeItem=2&deptid=" + dept_id;
                var eCode = $('#' + txtBOMITEMNAME).val();
                var prcNo = GetItemNo(eCode, serviceURL);

                if (prcNo == null) {
                    $('#' + hdnBOMITEMID).val('0');
                    $('#' + txtBOMITEMNAME).val('');
                }
                else {
                    $('#' + hdnBOMITEMID).val(ui.item.itemid);
                    $('#' + txtBOMITEMNAME).val(ui.item.itemname);
                }






                //var groupID = $(groupIDElem).val();
                //var groupID = $(BATTERY_TYPE_NAMENameElem).val();
                //if (groupID == '') {
                //    // $('#' + hdnDealerID).val('0');

                //    $('#' + hdnSUPERVISOR_ID).val('0');
                //    $('#' + txtSUPERVISOR_NAME).val('');

                //}
            });



        }


        function bindPackageList(gridViewID) {
            var cgColumns = [{ 'columnName': 'packageid', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Package ID' }
                             , { 'columnName': 'packageName', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Package Name' }
                             , { 'columnName': 'packageVer', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Package Ver' }
                             , { 'columnName': 'itemID', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Item ID' }
                             , { 'columnName': 'itemName', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Item Name' }
            ];
            var serviceURL1 = PackageListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            serviceURL1 += "&ispaging=1";
            var gridSelector = "#" + gridViewID;

            //var hdnItemIDElem = $(elemRow).find('input[id$="hdnItemId"]');
            //alert(hdnItemIDElem);

                      $(gridSelector).find('input[id$="txtPACKAGE_NAME"]').each(function (index, elem) {
                ///list click
                var elemRow = $(elem).closest('tr.gridRow');

               
                //var prevGLCode = '';

                $(elem).closest('tr').find('input[id$="btnPACKAGE_NAME"]').click(function (e) {
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
                    url: serviceURL1,
                   
                    search: function (event, ui) {
                        var elemRowCur = $(elem).closest('tr.gridRow');
                        var vitemid = $(elemRowCur).find('input[id$="hdnItemID"]').val();
                        
                        var newServiceURL1 = serviceURL1 + "&itemid=" + vitemid
                       // var newServiceURL1 = serviceURL1;
                        newServiceURL1 = JSUtility.AddTimeToQueryString(newServiceURL1);
                        $(this).combogrid("option", "url", newServiceURL1);
                    },
                    
                    select: function (event, ui) {
                        elemID = $(elem).attr('id');
                        if (!ui.item) {
                            event.preventDefault();
                            ClearPackageData(elemID);
                            return false;
                        }
                        if (ui.item.id == 0) {
                            event.preventDefault();
                            return false;
                        }
                        else {
                            SetPakageData(elemID, ui.item);

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
                            ClearPackageData(elemID);
                        }
                        else {
                            alert(elemID);
                            SetPakageData(elemID, grp);
                        }
                    }
                    //setDetInstrument(hdnIsInstrumentElem, txtInstrumentElem, btnInstrumentElem);
                    grpID = $(self).closest('tr').find('input[id$="hdnPACKAGE_ID"]').val();
                    if (grpID == '0' | grpID == '') {
                        $(self).addClass('textError');
                    }

                });

            });

        }



        function bindItemList(gridViewID) {
            var cgColumns = [{ 'columnName': 'itemname', 'width': '170', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                             , { 'columnName': 'uomname', 'width': '70', 'align': 'left', 'highlight': 4, 'label': 'Uom Name' }
                             //, { 'columnName': 'itemcode', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Code' }
                             , { 'columnName': 'itemgroupdesc', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Group Name' }
                             , { 'columnName': 'closing_qty', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Cls Qty' }
                             , { 'columnName': 'class_name', 'width': '120', 'align': 'left', 'highlight': 4, 'label': 'Class Name' }
                             , { 'columnName': 'item_type', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Item Type' }
                              , { 'columnName': 'is_prime', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Is Prime' }
            ];


            
            //var depthead = $('#' + hdnEmpCode).val();
            //var locationid = $('#' + ddlLocation).val();

            //var serviceURL = GLAccountServiceLink + "?isterm=1&includeempty=1&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            var serviceURL = ItemListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            //serviceURL += "&companyid=" + companyid;
            serviceURL += "&ispaging=1&isigr=1";
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
                    width: 800,
                    url: serviceURL,
                    search: function (event, ui) {
                        var elemRowCur = $(elem).closest('tr.gridRow');
                        var vgroupid = $(elemRowCur).find('input[id$="hdngroupId"]').val();
                        //var vgroupid = $('#' + hdngroupId).val();
                        var deptid = $('#' + hdnFromDepartment).val();
                        var newServiceURL = serviceURL + "&groupid=" + vgroupid + "&deptid=" + deptid;
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

                       
                        //var dept_id = $('#' + hdnFromDepartment).val();

                        var serviceURL = ItemListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
                        serviceURL += "&ispaging=1&isigr=1";//&deptid=" + dept_id;

                        var prcNo = GetItemNo(eCode, serviceURL);

                        if (prcNo == null) {
                            ClearItemData(elemID);
                        }
                        else {
                            SetItemData(elemID, grp);
                        }


                        //if (grp == null) {
                        //    ClearItemData(elemID);
                        //}
                        //else {
                        //    SetItemData(elemID, grp);
                        //}
                    }
                    //setDetInstrument(hdnIsInstrumentElem, txtInstrumentElem, btnInstrumentElem);
                    grpID = $(self).closest('tr').find('input[id$="hdngroupId"]').val();
                    if (grpID == '0' | grpID == '') {
                        $(self).addClass('textError');
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

        function bindBOMItemList(gridViewID) {
            var cgColumns = [{ 'columnName': 'bomid', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'BOM ID' }
                             , { 'columnName': 'bomitemdesc', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'BOM Name'}
                             , { 'columnName': 'bomitemid', 'width': '80', 'align': 'left',  'highlight': 4, 'label': 'Item ID' }
                             , { 'columnName': 'itemname', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Item Name' }
                             , { 'columnName': 'bomver', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'BOM Ver' }
                              
                          //, href: '~/Production/BOMEntryDeptWise.aspx?id=' + bomid 
                             
            ];
            var serviceURL = BOMListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            serviceURL += "&ispaging=1&isbom=1";
            // serviceURL += "&namecomptype=" + Enums.DataCompareType.Contains;


            var gridSelector = "#" + gridViewID;


            $(gridSelector).find('input[id$="txtBOM_ITEM_DESC"]').each(function (index, elem) {
                ///list click
                var elemRow = $(elem).closest('tr.gridRow');

                var hdnItemIDElem = $(elemRow).find('input[id$="txtBOM_ITEM_DESC"]');

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
                    width: 600,
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
            $(detRow).find('input[id$="hdnIS_PRIME"]').val('0');

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
            $(detRow).find('input[id$="hdnIS_PRIME"]').val(data.is_prime);
            
            //if (data.is_prime == 1)
            //    $(detRow).find('input[id$="btnITEM_BOM_ID"]').disabled = true;
            //else
            //    $(detRow).find('input[id$="btnITEM_BOM_ID"]').disabled = false;



        }


        function SetBOMData(txtBOM_ITEM_DESC, data) {
            //a.href = "~/Production/BOMEntryDeptWise.aspx?id='" + data.bomid + "'";
            $('#' + txtBOM_ITEM_DESC).val(data.bomitemdesc);
           
            var detRow = $('#' + txtBOM_ITEM_DESC).closest('tr.gridRow');
           // document.getElementById("abc").href = "~/Production/BOMEntryDeptWise.aspx?id='" + data.bomid + "'";
            //document.getElementById("abc").href = "~/Production/BOMEntryDeptWise.aspx?id='" + data.bomid + "'";
            //$(detRow).find('button[id$="abc"]').href = "~/Production/BOMEntryDeptWise.aspx?id='" + data.bomid + "'";
            //alert($(detRow).find('button[id$="abc"]').href);
            $(detRow).find('input[id$="hdnITEM_BOM_ID"]').val(data.bomid);
        }
        function ClearBOMData(txtBOM_ITEM_DESC) {
            var detRow = $('#' + txtBOM_ITEM_DESC).closest('tr.gridRow');
            $(detRow).find('input[id$="hdnITEM_BOM_ID"]').val('0');
            $(detRow).find('input[id$="txtBOM_ITEM_DESC"]').val('');

        }



        function SetPakageData(txtPackageID, data) {
            $('#' + txtPackageID).val(data.packageName);

            var detRow = $('#' + txtPackageID).closest('tr.gridRow');
            $(detRow).find('input[id$="hdnPACKAGE_ID"]').val(data.packageid);
        }
        function ClearPackageData(txtPackageID) {
            var detRow = $('#' + txtPackageID).closest('tr.gridRow');
            $(detRow).find('input[id$="hdnPACKAGE_ID"]').val('0');
            $(detRow).find('input[id$="txtPACKAGE_NAME"]').val('');

        }

     
    </script>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="dvPageContent" style="width: 100%; height: 100%;">
        <asp:HiddenField ID="hdnLoggedInUser" runat="server" />
        <div id="dvContentHeader" class="dvContentHeader">
            <div id="dvHeader" class="dvHeader_Prod" align="left">
                <asp:Label ID="lblHeader" runat="server" Text="Standard Consumption Entry" CssClass="lblHeader" Font-Bold="true" Font-Size="15px"></asp:Label>
            </div>
            <div id="dvMessage" class="dvMessage">
                            
                            <asp:HiddenField ID="BOM_ITEM_ID" runat="server"></asp:HiddenField>

                <asp:Label ID="lblMessage" runat="server" CssClass="lblMessage" Text="" Width="100%"></asp:Label>
            </div>

            <div id="dvHeaderControl" class="dvHeaderControl">
            </div>

        </div>
        <div id="dvContentMain" class="dvContentMain"  style=""> 
            <div id="dvControlsHead" style="height: auto; width: 90%;" >
                <table border="0"   style="width: 95%" >
                    <tr>
                        <td align="right" >
                            <asp:Label ID="lblBOMCODE" runat="server" Text="BOM NO.: "></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtBOMNO" runat="server" CssClass="textBox" Enabled="false" Width="150px" ></asp:TextBox>
                            <%--<asp:TextBox ID="txtBOMID" runat="server" CssClass="textBox" Enabled="false" Width="150px" style=" display : none" Visible="false"></asp:TextBox>--%>
                            <asp:HiddenField ID="hdnBOMID" runat="server" />
                        </td>

                        <td align="right" >
                            <asp:Label ID="lblBOM_VER" Text="Version : " runat="server"></asp:Label>
                            </td>
                        <td align="left">
                            <asp:TextBox ID="txtBOM_VER" runat="server" CssClass="textBox" Text="0"  onkeypress="return isNumberKey(event,this);"></asp:TextBox>
                            </td>

                        <td align="right">
                            Department:<span style="color: red">*</span>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlFromDepartment" runat="server" CssClass="dropDownList" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="ddlFromDepartment_SelectedIndexChanged"> </asp:DropDownList>
                            
                            <asp:HiddenField ID="hdnFromDepartment" runat="server" />
                        </td>


                        <td style="text-align: right">  &nbsp;</td>
                        <td align="left"> 
                            
                             &nbsp;</td>
                    </tr>

                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblBOMITEM" runat="server" Text="Item Name :"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtBOMITEMNAME" runat="server" CssClass="textBox textAutoSelect" Enabled="true" Width="220px"  ></asp:TextBox>
                            <input id="btnBOMITEMID" type="button" value="" runat="server" class="buttonDropdown" style="width: 15px" tabindex="-1" />
                            
                             <asp:HiddenField ID="hdnBOMITEMID" runat="server"  />
                           
                        </td>
                        <td style="text-align: right">
                          <asp:Label ID="lblActiveFromDate" runat="server" Text="Active Date :"></asp:Label>
                        </td>
                        <td>
                            
                            <asp:TextBox ID="txtACTIVE_FROM_DATE" runat="server" CssClass="textBox textDate dateParse" Style="text-align: left;" Width="100px"></asp:TextBox></td>
                        <td>
                            <asp:CheckBox ID="chkISACTIVE" runat="server" Text="   Is Active    " Checked="true"  > </asp:CheckBox>
                        </td>
                        <td>
                              <asp:CheckBox ID="chkAUTH_STATUS" runat="server" Text="   Authorize    " Enabled="False" > </asp:CheckBox>
                            <asp:HiddenField ID="hdnAUTH_STATUS"  runat="server" />
                        </td>
                        <td></td>
                        <td></td>



                    </tr>

                    <tr>
                        <td style="text-align: right">  
                            <asp:Label ID="lblMainITEMDESC" runat="server" Text="BOM Name : "></asp:Label>
                        </td>
                        <td><asp:TextBox ID="txtItemDesc" CssClass="textBox"  runat="server" Width="220px"></asp:TextBox></td>
                        <td align="right">  <asp:Label ID="lblRemarks" runat="server" Text="Remarks:"></asp:Label></td>
                        <td ><asp:TextBox ID="txtRemarks" runat="server" CssClass="textBox" TextMode="MultiLine" Height="30px" Width="200px"></asp:TextBox></td>
                        <td style="text-align: right; display : none">  
                            
                            <asp:CheckBox ID="chkISPACKAGE" Text="Package Name : " Visible="false"   CssClass="checkbox" runat="server"  /></td>
                        <td>

                            <asp:TextBox ID="txtPACKAGE_NAME" runat="server" cssClass="textBox" Width="200px" Visible="false"></asp:TextBox>

                             <asp:HiddenField ID="hdnCompanyID" runat="server" Value="0" />
                            </td>
                        <td></td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblstlm" runat="server" Text="Storage Location :"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlStlmId" runat="server" CssClass="dropDownList" AutoPostBack="true"></asp:DropDownList>
                        </td>
                        <td>

                        </td>
                        <td></td>
                         <td>

                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                </table>

            </div>
      
        <div id="dvControls" style="height: auto; width: 100%; ">
             <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
             <ContentTemplate>--%>
            <div id="groupDataDetails" style="width: 100%; height: auto;">
                <div id="dvGridSeparator" runat="server" style="width: 100%;">
                </div>
                <div id="groupDataHeaderCredit" runat="server" class="" style="width: 100%; text-align: left; font-size: small;">
                    <span style="font-weight: bold; color: #FF0000;">Require Items Details: </span>
                </div>
                <div id="dvGridContainer2" runat="server" class="" style="width: auto; height: auto; text-align: left">
                    <div id="dvGridHeader2" style="width: 1224px; height: 25px; font-size: smaller;" class="subHeader_Prod">
                        <table style="height: 100%; color: #FFFFFF; font-weight: bold; "
                            class="defFont" cellspacing="1" cellpadding="1">
                            <tr class="headerRow_Prod">
                                <td width="70px" class="headerColCenter">SL#
                                </td>
                                <td width="155px" class="headerColCenter">Item Group
                                </td>
                                <td width="15px" class="headerColLeft"></td>
                                <td width="190px" class="headerColCenter">Item
                                </td>
                                <td width="15px" class="headerColLeft"></td>
                                <%--<td width="40px" class="headerColCenter">Wastage %
                                </td>--%>
                                <td width="80px" class="headerColCenter">Item Qty
                                </td>
                                <td width="70px" class="headerColCenter">UOM
                                </td>
                                 <%--<td width="80px" class="headerColCenter">Wt./Pc(gm)
                                </td>--%>
                                   <td width="190px" class="headerColCenter">ITEM BOM
                                </td>
                                <td width="15px" class="headerColLeft"></td>
                                <%--<td width="25px" class="headerColCenter">Link</td>--%>
                                <td width="260px" class="headerColCenter">Remarks
                                </td>

                                <td width="16px" class="headerColCenter">Delete
                                </td>
                                
                            </tr>
                        </table>
                    </div>
                    <div id="dvGrid" style="width: 1300px; height: 200px; overflow: auto;" class="dvGrid">
                      <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                                <asp:GridView ID="GRDDTLITEMLIST" runat="server" AutoGenerateColumns="False" ShowHeader="False" 
                                    CellPadding="1" CellSpacing="1" ForeColor="#333333" GridLines="Both" DataKeyNames="ITEM_ID" 
                                    EnableModelValidation="True" ClientIDMode="AutoID" OnRowCommand="GRDDTLITEMLIST_RowCommand" OnRowCreated="GRDDTLITEMLIST_RowCreated" OnRowDataBound="GRDDTLITEMLIST_RowDataBound" OnRowDeleting="GRDDTLITEMLIST_RowDeleting"  >
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <Columns>
                                       

                                        <asp:TemplateField HeaderText="SL#">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSLNo" runat="server" Text='<%#Container.DataItemIndex+1 %>' Style="text-align: center;"
                                                    Width="68px">
                                                </asp:Label>

                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Battery Type" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <div>
                                                    <table border="0" cellpadding="1" cellspacing="1">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="txtGroupName" runat="server" CssClass="textBox textAutoSelect" Width="155" Text='<%# Bind("ITEM_GROUP_DESC") %>'></asp:TextBox>
                                                                    <asp:HiddenField ID="hdngroupId" runat="server" Value='<%# Bind("ITEM_GROUP_ID") %>' />
                                                                     <asp:HiddenField ID="hndBOM_DTL_ID" runat="server" Value='<%# Bind("BOM_DTL_ID") %>' />
                                                                      
                                                                </td>
                                                                <td>
                                                                    <input id="btnItemGroup" type="button" value="" runat="server" class="buttonDropdown" tabindex="-1" />
                                                                </td>

                                                                <td>
                                                                    <asp:TextBox ID="txtITEM_NAME" runat="server" CssClass="textBox textAutoSelect" Width="190" Text='<%# Bind("ITEM_NAME") %>'></asp:TextBox>
                                                                    <asp:HiddenField ID="hdnItemID" runat="server" Value='<%# Bind("ITEM_ID") %>' />
                                                                    <asp:HiddenField ID="hdnIS_PRIME" runat="server" />
                                                                </td>
                                                                <td>
                                                                    <input id="btnITEM_NAME" type="button" value="" runat="server" class="buttonDropdown"
                                                                        tabindex="-1" />
                                                                </td>
                                                               <%-- <td>
                                                                    
                                                                </td>--%>
                                                                <td>
                                                                    <asp:TextBox ID="txtWASTAGE_PERCENT" runat="server" CssClass="textBox textAutoSelect" Width="43" align="left" Text='<%# Bind("WASTAGE_PERCENT") %>'  onkeypress="return isNumberKey(event,this);" Visible="false"></asp:TextBox>
                                                                    <asp:TextBox ID="txtItem_qty" runat="server" CssClass="textBox textAutoSelect" Width="78" align="left" Text='<%# Bind("ITEM_QTY") %>'  onkeypress="return isNumberKey(event,this);"></asp:TextBox>
                                                                </td>

                                                                <td>
                                                                    <asp:TextBox ID="txtUOM_NAME" runat="server" CssClass="textBox textAutoSelect" Width="68" Text='<%# Bind("UOM_NAME") %>' Style="" Enabled="false" ></asp:TextBox>
                                                                    <asp:HiddenField ID="hdnUomID" runat="server" Value='<%# Bind("ITEM_UNIT_ID") %>' />
                                                                    <asp:TextBox ID="txtITEM_WEIGHT" runat="server" CssClass="textBox textAutoSelect" Width="75" align="left" Text='<%# Bind("ITEM_WEIGHT") %>'  onkeypress="return isNumberKey(event,this);" Visible="false"></asp:TextBox>
                                                                    
                                                                </td>
                                                                 <td width="15px">
                                                                     <asp:TextBox ID="txtBOM_ITEM_DESC" runat="server" CssClass="textBox textAutoSelect" Width="185" Text='<%#Bind("BOM_ITEM_DESC") %>' Visible="true"></asp:TextBox>
                                                                     <asp:HiddenField ID="hdnITEM_BOM_ID" runat="server" Value='<%# Bind("ITEM_BOM_ID") %>' />
                                                                      
                                                                </td>
                                                               <td>
                                                                   <input id="btnITEM_BOM_ID" type="button" value="" runat="server" class="buttonDropdown" tabindex="-1" visible="true" />
                                                                </td>
                                                               <%--  <td>
                                                                   
                                                                  
                                                                </td>--%>

                                                                <%--<td>
                                                                    <a href="#" id="abc">Link</a>
                                                                </td>--%>
                                                           
                                                                <td>
                                                                    <asp:TextBox ID="txtRemarks" runat="server" CssClass="textBox textAutoSelect" Width="255" Text='<%# Bind("REMARKS") %>' Style=""></asp:TextBox>
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
                    <div id="divGridControls2" style="width: 100%; height: 100px; border-top: solid 1px #C0C0C0;">
                        <table style="width: 100%; height: 100%; text-align: center;" cellspacing="1" cellpadding="1"
                            border="0">
                            <tr>
                                <td style="width: 2px"></td>
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
                               <%-- <td style="width: 160px; text-align: right;">
                                    <asp:Label ID="lbltotalQnty" runat="server" Text="Total Qty :" Font-Bold="True"></asp:Label>
                                </td>
                                <td align="right" style="text-align: left">
                                    <asp:TextBox ID="txtTotQty" runat="server" CssClass="textBox" Style="text-align: right;"
                                        Width="100" TabIndex="-1" Font-Bold="True"></asp:TextBox>
                                    &nbsp;
                                </td>--%>
                                <td align="right">
                                    &nbsp;</td>
                                <td align="right">
                                    &nbsp;</td>
                                <td align="right">&nbsp;
                                </td>

                            </tr>
                           <%-- <tr>
                                <td></td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td style="text-align: right">
                                    <asp:Label ID="lblWastage" runat="server" Text="Wastage : "  Font-Bold="True"></asp:Label>
                                </td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtWASTAGE" runat="server" CssClass="textBox" Width="100px" onkeypress="return isNumberKey(event,this);"></asp:TextBox>
                                </td>
                                <td></td>
                                <td></td>
                                <td></td>

                            </tr>--%>
                                <tr>
                                <td></td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                     &nbsp;</td>
                                <td style="text-align: right">
                                    <%--<asp:Label ID="lblDROSS" runat="server" Text="Dross : "  Font-Bold="True"></asp:Label>--%>
                                    </td>
                                <td style="text-align: left">
                                     <%--<asp:TextBox ID="txtDROSS" runat="server" CssClass="textBox" Width="100px" onkeypress="return isNumberKey(event,this);"></asp:TextBox>--%>
                                    </td>
                                <td></td>
                                <td></td>
                                <td></td>

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
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="buttonDelete" />
                    </td>
                    <td>
                        <asp:Button ID="btnRefresh" runat="server" Text="Refresh" CssClass="buttonRefresh checkIsDirty" OnClick="btnRefresh_Click" />
                    </td>
                    <td>
                        <%--<uc:PrintButton runat="server" ID="ucPrintButton" DefaultPrintAction="Preview" AutoPrint="False" />--%>
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
                    <td>
                    </td>
                </tr>
            </table>
        </div>

    </div>  
    
</asp:Content>
