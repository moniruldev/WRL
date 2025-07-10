<%@ Page Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="Requisition.aspx.cs" Inherits="PG.Web.Report.INV.Requisition" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/form.css" rel="stylesheet" type="text/css" />

    <script src="../javascript/jquery.ui.combogrid.js" type="text/javascript"></script>
    <script src="../javascript/jquery.attributeobserver.js" type="text/javascript"></script>
    <link href="../css/jquery.ui.combogrid.css" rel="stylesheet" type="text/css" />

    <script src="../javascript/CommonJSForSetReset.js" type="text/javascript"></script>
    <script src="javascript/jquery-latest.min.js" type="text/javascript"></script>
    <style type="text/css">
        .cmnTable {
            border-collapse: collapse;
            width: 101%;
        }

            .cmnTable th {
                text-align: left;
                padding: 6px;
            }

            .cmnTable tr:nth-child(even) {
                background-color: #f2f2f2;
            }

            .cmnTable th {
                background-color: #C3D8F2;
            }

        .has-warning {
            border: 1px solid red;
        }

        .auto-style1 {
            width: 173px;
        }
    </style>

    <style type="text/css">
        #dvControlsTab {
            padding: 0px;
            background: none;
            border-width: 0px;
        }

            #dvControlsTab .ui-tabs-nav {
                padding-left: 0px;
                background: transparent;
                border-width: 0px 0px 1px 0px;
                border-radius: 0px;
                -moz-border-radius: 0px;
                -webkit-border-radius: 0px;
            }

            #dvControlsTab .ui-tabs-selected a {
                color: #000;
                font-weight: bold; /*
           border-top: 3px solid #fabd23; 
           border-left: 1px solid #fabd23; 
           border-right: 1px solid #fabd23;
            */
                border-top: 3px solid blue;
                margin-bottom: -1px;
                overflow: visible;
            }

            #dvControlsTab .ui-state-default {
                /*background: transparent;*/ /* border: none; */
            }

                #dvControlsTab .ui-state-default a {
                    /*color: #c0c0c0;*/
                }

            #dvControlsTab .ui-state-active a {
                /* color: #459E00; */
                color: blue;
            }


        .groupBoxContainer {
            height: 100%;
            width: 1024px;
            overflow: auto;
            margin-left: 5px;
            margin-top: 5px;
        }

        .groupHeader {
            height: 20px;
            background-image: url('../../image/header13.png');
            background-repeat: repeat-x;
            color: black;
            font-weight: bold;
        }

        .groupBox {
            background-image: url('../../image/bg_greendot.gif');
            height: 100%;
            width: 100%;
            min-width: 500px;
            display: inline-block;
            text-align: center;
            vertical-align: middle;
        }

        .groupContent {
            width: 100%;
            height: 100%;
        }

        .groupContenInner {
            width: 100%;
            height: auto;
            overflow: auto;
        }


        .subHeader {
            height: 20px;
            width: 100%;
            background-image: url('../../image/header13.png');
            background-repeat: repeat-x;
            color: White;
            vertical-align: middle;
            font-weight: bold;
        }

            .subHeader span {
                margin-left: 2px;
            }


        .groupHeader span {
            margin-left: 2px;
            margin-top: 4px;
        }

        .dvGridDetailsPopup {
            display: none;
            border: 0px solid black;
            height: 0px;
            width: 0px;
        }

        .ui-widget input {
            font-size: 11px;
        }

        .ui-widget select {
            font-size: 11px;
        }


        .dvPopupProject {
            display: none;
            border: 0px solid black;
            height: 0px;
            width: 0px;
        }


        .btnSearch {
            height: 19px;
            width: 16px;
            background-image: url('../../image/search.png');
            background-repeat: no-repeat;
            background-position: center bottom;
            cursor: pointer;
        }

        .dvPopupGLAccount {
            display: none;
            border: 0px solid black;
            width: 0px;
            height: 0px;
        }


        .dvPopupTranType {
            display: none;
            border: 0px solid black;
            width: 0px;
            height: 0px;
        }

        .dvPopupCashTranInfo {
            display: none;
            border: 0px solid black;
            width: 0px;
            height: 0px;
        }






        .dvPopupIns {
            display: none;
            border: 0px solid black;
            width: 0px;
            height: 0px;
        }



        .ui-dialog .ui-dialog-content {
            padding: 2px 0px 0px 0px;
        }

        .ui-dialog .ui-dialog-titlebar {
            padding: 4px 2px 0px 2px;
        }

        .tableRowOdd {
            background-color: #F7F6F3;
        }

        .tableRowEven {
            background-color: White;
        }







        .hidden {
            /*visibility:hidden;*/
            display: none;
        }

        #Text1 {
            width: 538px;
        }

        .auto-style2 {
            height: 24px;
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


    <script type="text/javascript" language="javascript">

        //Report Part     
        var GetJSonDataServiceLink = '<%=this.GetJSonDataServiceLink%>';
        var ItemGroupListServiceLink = '<%=this.ItemGroupListServiceLink%>';
        var ItemListServiceLink = '<%=this.ItemListServiceLink%>';

        var lblMessage = '<%=lblMessage.ClientID%>';


        var txtReqQnty = '<%=txtReqQnty.ClientID%>';
        var taItemNote = '<%=taItemNote.ClientID%>';


       <%-- var ddlCompany = '<%= ddlCompany.ClientID%>';--%>
        var ddlFromDepartment = '<%=ddlFromDepartment.ClientID%>';
       <%-- var ddlToDepartment = '<%= ddlToDepartment.ClientID%>';--%>
       <%-- var ddlStore = '<%=ddlStore.ClientID%>';--%>
     <%--   var ddlBranchId = '<%= ddlBranchId.ClientID %>';--%>
        var txtReqDate = '<%= txtReqDate.ClientID %>';
        var taRemarks = '<%= taRemarks.ClientID %>';

        var btnSave = '<%=btnSave.ClientID%>';
        var btnReset = '<%= btnReset.ClientID%>';

        var hdnGroupID = '<%= hdnGroupID.ClientID%>';
        var txtGroupName = '<%= txtGroupName.ClientID%>';
        var btnGroupID = '<%= btnGroupID.ClientID%>';

        var hdnItemID = '<%=hdnItemID.ClientID %>';
        var txtItemName = '<%=txtItemName.ClientID %>';
        var btnItemID = '<%= btnItemID.ClientID%>';
        var hdnUomID = '<%=hdnUomID.ClientID %>';
        var hdnUomName = '<%= hdnUomName.ClientID%>';
        var cbIsAuthorized = '<%=cbIsAuthorized.ClientID%>';


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
            var now = new Date();
            var strTime = now.getTime().toString();
            var url = ReportViewPageLink + "?rk=" + key + "&_tt=" + strTime;

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


        //Report End




        $(document).ready(function () {

            $('#' + btnSave).on('click', function () {
                Save();
            });

            $('#' + btnReset).on('click', function () {
                ResetAfterSave();
            });

            $('#' + txtReqDate).val(GetCurrentDate());
            if ($('#' + txtGroupName).is(':visible')) {
                bindGroupList();
            }

            bindItemList();

        });





        $(document).on('click', '#btnAddNewItem', function () {
            $('.has-warning').removeClass('has-warning');
            var isValid = true;
            ResetMessage();
            isValid = ValidateInput("#tblAddNewItem");
            if (isValid) {

                var itemId = $('#' + hdnItemID).val();
                var groupId = $('#' + hdnGroupID).val();
                var uomId = $('#' + hdnUomID).val();
                var itemName = $('#' + txtItemName).val();

                var groupName = $('#' + txtGroupName).val();
                //var description = $('#' + txtItemDescription).val();
                var quantity = $('#' + txtReqQnty).val();
                var uomName = $('#' + hdnUomName).val();

                //var uomName = $('#' + txtUom).val();
                var note = $('#' + taItemNote).val();

                if ($(this).val() == 'Add') {
                    var itemRow = $('#tblItemList #row' + itemId);
                    if (!(itemRow.length > 0)) {
                        var newtr = newtr + '<tr id="row' + itemId + '">' +
                                     '<td><span class="slno" ></span></td>' +
                                         '<td><input type="hidden" id="hdnGroupId" value=' + groupId + '><input type="hidden" id="hdnItemId" value=' + itemId + '><span id="lblGroupName">' + groupName + '</span></td>' +
                                            '<td><span id="lblItemName">' + itemName + '</span></td>' +
                                          '<td><span id="lblQnty">' + quantity + '</span></td>' +
                                          '<td><input type="hidden" id="hdnUomId" value=' + uomId + '><span id="lblUom">' + uomName + '</span></td>' +
                                          '<td><span id="lblNote">' + note + '</span></td>' +
                                        '<td><input type="button" class="editRowWiseItem buttonEditGrid"  ><button class="deleteTempRow"><img src="../image/btnDeleteIconHover.png" alt="Delete" /></button></td>' +
                                   '<td></td>' +
                                 '</tr>';

                        $('#tblItemList').find('tbody').append(newtr);

                        ResetInput('tblAddNewItem');
                        Resetddl('tblAddNewItem');
                        $('#' + taItemNote).val('');
                        GenerateNewSlNo('tblItemList');
                    } else {
                        SetMessage('Already Added this item', 'red');
                    }

                } else {
                    var parentTr = $('#tblItemList #row' + itemId);
                    if (parentTr.length > 0) {
                        $(parentTr).find('#lblGroupName').text(groupName);
                        $(parentTr).find('#hdnGroupId').val(groupId);
                        $(parentTr).find('#hdnItemId').val(itemId);
                        $(parentTr).find('#lblItemName').text(itemName);
                        $(parentTr).find('#lblQnty').text(quantity);
                        $(parentTr).find('#lblUom').text(uomName);
                        $(parentTr).find('#hdnUomId').val(uomId);
                        $(parentTr).find('#lblNote').text(note);
                        ResetInput('tblAddNewItem');
                        Resetddl('tblAddNewItem');
                        $("#btnAddNewItem").val('Add');
                        ResetInput('tblAddNewItem');
                        Resetddl('tblAddNewItem');
                        $('#' + taItemNote).val('');
                    } else {
                        SetMessage('Item Not Found.', 'red');
                    }

                }
                $('#' + hdnItemID).val('');
                $('#' + hdnGroupID).val('');
                $('#' + hdnUomID).val('');
            }
        });

        $(document).on('click', '.editRowWiseItem', function () {
            debugger;
            var parentTr = $(this).closest('tr');
            var itemId = $(parentTr).find('#hdnItemId').val();
            var itemName = $(parentTr).find('#lblItemName').text();
            var groupId = $(parentTr).find('#hdnGroupId').val();
            var groupName = $(parentTr).find('#lblGroupName').text();
            var quantity = $(parentTr).find('#lblQnty').text();
            var note = $(parentTr).find('#lblNote').text();
            var unitName = $(parentTr).find('#lblUom').text();
            var unitId = $(parentTr).find('#hdnUomId').val();

            $('#' + txtGroupName).val(groupName);
            $('#' + hdnGroupID).val(groupId);
            $('#' + txtItemName).val(itemName);
            $('#' + hdnItemID).val(itemId);
            $('#' + hdnUomName).val(unitName);
            $('#' + hdnUomID).val(unitId);
            $('#' + txtReqQnty).val(quantity);
            $('#' + taItemNote).val(note);
            $("#btnAddNewItem").val('Update');
        });


        function ResetAfterSave() {
            ResetInput("tblHeader");
            ResetTable("tblItemList", true);
            ResetInput('tblAddNewItem');
            Resetddl('tblAddNewItem');
            Resetddl('tblHeader');
            ResetMessage();
            $('#' + txtReqDate).val(GetCurrentDate());
            $('#' + taRemarks).val("");
            $('#' + taItemNote).val("");
            $("#btnAddNewItem").val('Add');
            $('.has-warning').removeClass('has-warning');
            ResetCheckBoxAndRadioById("cbIsAuthorized");
        }




        function Save() {

            debugger;
            $(".has-warning").removeClass("has-warning");
            var isValid = true;

            isValid = ValidateInput("#tblHeader");
            if (!isValid) {
                return false;
            }
            isValid = ValidateInput("#tblItemList");

            if (!isValid) {
                return false;
            }
            if (isValid) {
                debugger;
                var tblItemList = $('#tblItemList').find('tbody').find('tr');
                var objList = new Array();
                var objMaster = new Object();

                objMaster.STORE_ID = 1;
                //objMaster.STORE_ID = $('#' + ddlStore).val();
                objMaster.REQ_COMPANY_ID = 2;
                //objMaster.REQ_COMPANY_ID = $('#' + ddlCompany).val();
                objMaster.REQ_BRANCH_ID = 4;
                //objMaster.REQ_BRANCH_ID = $('#' + ddlBranchId).val();
                objMaster.FROM_DEPARTMENT_ID = $('#' + ddlFromDepartment).val();
                objMaster.TO_DEPERATMENT_ID = 26;
                //objMaster.TO_DEPERATMENT_ID = $('#' + ddlToDepartment).val();
                objMaster.REQ_REMARKS = $('#' + taRemarks).val();
                objMaster.REQ_DATE = $('#' + txtReqDate).val();
                objMaster.REQUIRED_DATE = $('#' + txtReqDate).val();
                objMaster.CREATE_BY = 0;
                objMaster.REQ_ID = 0;
                objMaster.AUTH_STATUS = "Y";
                //objMaster.AUTH_STATUS = $('#' + cbIsAuthorized).is(':checked') ? "Y" : "N";

                for (var k = 0; k < tblItemList.length; k++) {
                    debugger;
                    var obj = new Object();
                    obj.REQ_QNTY = $(tblItemList[k]).find('#lblQnty').text();
                    if (obj.REQ_QNTY != "" && obj.REQ_QNTY != "0") {

                        obj.REQ_APRV_QNTY = obj.REQ_QNTY;
                        obj.ITEM_ID = $(tblItemList[k]).find('#hdnItemId').val();
                        obj.UOM_ID = $(tblItemList[k]).find('#hdnUomId').val();
                        obj.UOM_ID = 1;
                        obj.REQ_DET_SLNO = $(tblItemList[k]).find('.slno').text();
                        obj.REQ_NOTE = $(tblItemList[k]).find('#lblNote').text();
                        //  obj.APRV_NOTE = $(tblItemList[k]).find('#lblNote').text();
                        obj.REQ_DET_ID = 0;
                        obj.CREATE_BY = 0;
                        objList.push(obj);
                    }

                }

                if (objList.length > 0) {
                    var y = confirm('Are you sure to Save?');
                    if (y) {
                        $.ajax({
                            type: "POST",
                            url: "../Inventory/Requisition.aspx/Save",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            async: true,
                            data: JSON.stringify({ Obj: objMaster, objList: objList }),
                            success: function (result) {
                                var data = JSON.parse(result.d);
                                if (data.Status == "Success") {
                                    ResetAfterSave();
                                    SetMessage("Requisition Saved Successfully.", "green");
                                } else {
                                    ResetAndSetMessage("S", data.Message, "red");
                                }

                            },
                            error: function (result) {
                                alert(result.responseText);
                            }

                        });
                    }
                } else {
                    SetMessage("Please add item first.", "red");

                }

            }

        }


        //this is for group dropdown
        function bindGroupList() {
            var cgColumns = [
                             //{ 'columnName': 'itemgroupid', 'width': '80', 'align': 'left', 'highlight': 2, 'label': 'Group ID' }
                             { 'columnName': 'itemgroupcode', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Code' }
                            , { 'columnName': 'itemgroupdesc', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                            , { 'columnName': 'itemgroupnameparent', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Group Parent' }


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
                width: 600,
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
                        //$('#' + txtDealerID).val('');
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
                             , { 'columnName': 'itemgroupdesc', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Group Name' }
                             , { 'columnName': 'uomname', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Uom Name' }
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
                width: 600,
                url: itemServiceURL,
                search: function (event, ui) {
                    //var companyCode = $('#' + ddlCompany).val();
                    //var branchCode = $('#' + hdnBranch).val();
                    //var deptCode = $('#' + hdnDepartment).val();
                    //var locationid = $('#' + lblLocationID).val();
                    // var seid = $('#' + txtExecutiveID).val();

                    var vgroupid = $('#' + hdnGroupID).val();
                    var newServiceURL = itemServiceURL + "&groupid=" + vgroupid;
                    // var newServiceURL = itemServiceURL;
                    $(this).combogrid("option", "url", newServiceURL);


                },
                select: function (event, ui) {
                    if (!ui.item) {
                        event.preventDefault();

                        // $('#' + hdnDealerID).val('0');
                        //$('#' + txtDealerID).val('');
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
                        $('#' + hdnItemID).val(ui.item.itemid);
                        $('#' + txtItemName).val(ui.item.itemname);

                        $('#' + hdnUomID).val(ui.item.itemid);
                        $('#' + hdnUomName).val(ui.item.uomname);

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
                    //$('#' + txtGroupCode).val('');
                }
            });
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hdnGroupID" runat="server" />
    <asp:HiddenField ID="hdnItemID" runat="server" />
    <asp:HiddenField ID="hdnUomID" runat="server" />
    <asp:HiddenField ID="hdnUomName" runat="server" />

    <div style="min-height: 30px; width: 100%; text-align: center !important;" id="dvMessage">
        <span runat="server" id="lblMessage" style="font-weight: bold; text-align: center; font-size: 20px;"></span>
    </div>
    <div id="dvHeader" class="dvHeader" align="center" style="width: 100%; height: auto;">
        <asp:Label ID="lblHeader" runat="server" Font-Bold="True" Font-Size="Large">INTERNAL REQUISITION OF GOODS(IRG)</asp:Label>
    </div>
    <div id="dvContentMain" class="dvContentMain" style="height: 400px;">

        <div id="pLoading" style="display: none; margin: 0px; padding: 0px; position: fixed; right: 0px; top: 0px; width: 100%; height: 100%; z-index: 30001; opacity: 0.8;">
            <p style="position: absolute; color: White; top: 20%; left: 20%;">
                <img src="../image/loading.gif" alt="loading...">
            </p>
        </div>


        <table style="text-align: left; width: 100%" border="0" cellspacing="4" cellpadding="2" id="tblHeader">
            <thead>
                <tr>

                    <th>
                        <asp:Label ID="lblReqDept" runat="server" Text="Req Date."></asp:Label>
                    </th>
                    <th>
                        <asp:TextBox ID="txtReqDate" runat="server" CssClass="textBox textDate  enableIsDirty" Width="130px"></asp:TextBox>
                    </th>

                  

                    <th>
                        <asp:Label ID="lblReqLoc" runat="server" Text="Req Department"></asp:Label>
                    </th>
                    <th>
                        <asp:DropDownList ID="ddlFromDepartment" runat="server" CssClass="dropDownList required">
                        </asp:DropDownList>
                    </th>

                </tr>
                <%--<tr>

                      <th>
                        <asp:Label ID="Label3" runat="server" Text="Company"></asp:Label>
                    </th>
                    <th>
                        <asp:DropDownList ID="ddlCompany" runat="server" CssClass="dropDownList required">
                        </asp:DropDownList>
                    </th>
                    <th>
                        <asp:Label ID="Label1" runat="server" Text="To Department"></asp:Label>
                    </th>
                    <th>
                        <asp:DropDownList ID="ddlToDepartment" runat="server" CssClass="dropDownList required">
                        </asp:DropDownList>
                    </th>
                       <th>
                        <asp:Label ID="Label4" runat="server" Text="Branch"></asp:Label>
                    </th>
                    <th>
                        <asp:DropDownList ID="ddlBranchId" runat="server" CssClass="dropDownList required">
                        </asp:DropDownList>
                    </th>
                     <th>
                        <asp:Label ID="Label2" runat="server" Text="Store Name"></asp:Label>
                    </th>
                    <th>

                        <asp:DropDownList ID="ddlStore" runat="server" CssClass="dropDownList required">
                        </asp:DropDownList>
                    </th>
                </tr>--%>

                <tr>

                 

                   

                    <th>
                        <asp:Label ID="lblReqDate" runat="server" Text="Remarks"></asp:Label>
                    </th>
                    <th>
                        <textarea id="taRemarks" class="textAreaAutoSize" runat="server"></textarea>
                    </th>
                    <th>
                        <input type="checkbox" id="cbIsAuthorized" runat="server" />Authorized</th>
                </tr>
            </thead>


        </table>

        <br />
        <br />
        <%--   <fieldset style="border: 1px solid black">
            <legend>Add New Item</legend>
            
        </fieldset>--%>

        <table style="text-align: left; width: 100%" border="0" cellspacing="4" cellpadding="2" id="tblAddNewItem">
            <tr>
                <th>
                    <asp:Label ID="lblCode" runat="server" Text="Item Group"></asp:Label>
                </th>

                <th>
                    <asp:Label ID="lblItemDescription" runat="server" Text="Item"></asp:Label>
                </th>

                <th>
                    <asp:Label ID="lblReqQnty" runat="server" Text="Qnty."></asp:Label>
                </th>
                <th>
                    <asp:Label ID="lblNote" runat="server" Text="Note"></asp:Label>
                </th>

                <th></th>

            </tr>
            <tr>

                <td class="auto-style2">

                    <asp:TextBox ID="txtGroupName" runat="server" CssClass="textBox required" Enabled="true"></asp:TextBox><input id="btnGroupID" type="button" value="" runat="server" class="buttonDropdown" tabindex="-1" />

                </td>

                <td>
                    <asp:TextBox ID="txtItemName" runat="server" CssClass="textBox required" Enabled="true"></asp:TextBox>
                    <input id="btnItemID" type="button" value="" runat="server" class="buttonDropdown" tabindex="-1" />

                </td>

                <%-- <td>
                    <asp:DropDownList ID="ddlItemGroup" runat="server" CssClass="dropDownList required" Width="130px">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="ddlItem" runat="server" CssClass="dropDownList required" Width="150px">
                    </asp:DropDownList>
                </td>--%>


                <%-- <td>
                    <asp:TextBox ID="txtCode" runat="server" CssClass="textBox required" Width="130px"></asp:TextBox>
                </td>

                <td>
                    <asp:TextBox ID="txtItemDescription" runat="server" CssClass="textBox required" Width="130px"></asp:TextBox>
                </td>--%>
                <td>
                    <asp:TextBox ID="txtReqQnty" runat="server" CssClass="textBox  numberOnly required" Width="80px"></asp:TextBox>
                </td>
                <td>
                    <textarea id="taItemNote" cols="30" runat="server" rows="2"></textarea>
                    <%--  <asp:TextBox id="txtNote" runat="server" CssClass="textBox  enableIsDirty" width="130px"></asp:TextBox>--%>
                </td>

                <td>
                    <input type="button" class="buttoncommon" value="Add" id="btnAddNewItem" /></td>

            </tr>


        </table>

        <table border="0" class="cmnTable" id="tblItemList" align="center" style="width: 100%">
            <thead>
                <tr>
                    <th>SL.</th>
                    <th>Item Group</th>
                    <th>Item</th>
                    <th>Qnty</th>
                    <th>Uom</th>
                    <th>Note</th>
                    <th>Action</th>
                </tr>
            </thead>
            <tbody>
            </tbody>
        </table>
    </div>
    <div id="dvContentFooter" class="dvContentFooter" align="center">
        <table>
            <tr>

                <td>
                    <input id="btnSave" type="button" runat="server" class="buttonSave" value="Save" />

                    <input id="btnEdit" type="button" runat="server" class="buttonEdit" value="Edit" style="display: none" />

                </td>

                <td>

                    <input type="button" runat="server" id="btnReset" class="buttonRefresh" value="Reset" />

                </td>
                <td>
                    <input id="btnPrintBSR" type="button" class="buttonPrint" style="text-align: right; width: 100px !important; display: none" value="Print" onclick="CheckBsrNumber()" />

                </td>

                <td>
                    <input type="button" id="btnClose" class="buttonClose" value="Close" onclick="if (ContentForm) { ContentForm.CloseForm(); }" />


                </td>
            </tr>
        </table>
    </div>
</asp:Content>



