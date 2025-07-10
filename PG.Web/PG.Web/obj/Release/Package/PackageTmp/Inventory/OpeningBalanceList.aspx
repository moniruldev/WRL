<%@ Page Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="OpeningBalanceList.aspx.cs" Inherits="PG.Web.Inventory.OpeningBalanceList" %>
<%--<%@ Register src="~/Controls/ItemGroupTree.ascx" tagname="ItemGroupTree" tagprefix="uc1" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="../javascript/jquery.ui.combogrid.js" type="text/javascript"></script>
    <script src="../javascript/jquery.attributeobserver.js" type="text/javascript"></script>
    <link href="../css/jquery.ui.combogrid.css" rel="stylesheet" type="text/css" />

    <script src="../javascript/CommonJSForSetReset.js" type="text/javascript"></script>
    <link href="css/form.css" rel="stylesheet" type="text/css" />
    <script src="javascript/jquery-latest.min.js" type="text/javascript"></script>







   <%-- <script src="../javascript/pg.ui.itemgrouptree.js" type="text/javascript"></script>

	<link href="../css/skin/ui.dynatree.css" rel="stylesheet" type="text/css" />
	<link href="../css/pg.ui.itemgrouptree.css" rel="stylesheet" type="text/css" />--%>


    <style type="text/css">
  

        .selectrow {
            background-color: #9eddff !important;
            font-weight: bold;
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
    <script language="javascript" type="text/javascript">







        var isPageResize = true;
        <%-- var gridViewID = '<%=GridView1.ClientID%>';
        var gridRowCountID = '<%= hdnRowCount.ClientID %>';--%>
        var ItemGroupListServiceLink = '<%=this.ItemGroupListServiceLink%>';
        var ItemListServiceLink = '<%=this.ItemListServiceLink%>';
        var uomIdhdn = '<%= hdnUomId.ClientID %>';
        var itemIdhdn = '<%= hdnItemId.ClientID %>';
      <%--  var hdnGroupID = '<%= hdnGroupID.ClientID%>';--%>
     
        var btnItemLoad = '<%= btnItemLoad.ClientID%>';
        var hdnItemIdForFilter = '<%= hdnItemIdForFilter.ClientID%>';
        var txtItemName = '<%= txtItemName.ClientID%>';
      
        var ddlItemType = '<%= ddlItemType.ClientID%>';


  var hdnItemGroupIDParent = '<%=hdnItemGroupIDParent.ClientID %>';

        //this is for item group tree

   <%--     var groupPopupID = '<%=dvPopupItemGroup.ClientID %>';
        var dvItemGroupID = '<%=dvItemGroup.ClientID %>';
        var txtItemGroupNameParent = '<%=txtItemGroupNameParent.ClientID %>';
      
        var hdnItemGroupParentKey = '<%=hdnItemGroupParentKey.ClientID %>';--%>


  
        var isGridScroll = false;

        function PageResizeCompleted(pg, cntMain) {
            resizeContentInner(cntMain);

        }

        function resizeContentInner(cntMain) {
            var contHeight = $("#dvContentMain").height();
            var contHead = $("#dvControlsHead").height();
            var contFooter = $("#dvControlsFooter").height();

            var contInnerHeight = contHeight - contHead - contFooter - 5;
            $("#dvControls").height(contInnerHeight);

            $("#dvControlsInner").height(contInnerHeight - 10);



            $("#dvGridContainer").height(contInnerHeight - 10);
            var gridHeight = $("#dvGridContainer").height();
            var gridHeaderHeight = $("#dvGridHeader").height();
            var gridFooterHeight = $("#dvGridFooter").height();
            $("#dvGrid").height(gridHeight - gridHeaderHeight - gridFooterHeight - 2);


        }


        function tbopen(key, slno) {


            if (!key) {
                key = '';
            }

            var url = "Inventory/OpeningBalance.aspx?slno=" + key
            //if (pageInTab == 1)
            if (ZForm.PageMode == Enums.PageMode.InTab) {

                var tdata = new xtabdata();
                tdata.linktype = Enums.LinkType.Direct;
                tdata.id = 6310;
                tdata.name = "OpeningBalance";
                //tdata.label = "OpeningBalance: " + slno;
                tdata.label = "OpeningBalance";
                tdata.type = 0;
                tdata.url = url;
                tdata.tabaction = Enums.TabAction.InNewTab;
                tdata.selecttab = 1;
                tdata.reload = 0;
                tdata.param = "";


                try {
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

        function fromParent(val1) {
            alert('this is called from parent: ' + val1);
        }

        var btnLoadItem = '<%=btnLoadItem.ClientID%>';    
        var lblSlNo = '<%=lblSlNo.ClientID%>';
        var lblItemName = '<%=lblItemName.ClientID%>';
        var lblUomName = '<%=lblUomName.ClientID%>';
        var txtOpeningQnty = '<%=txtOpeningQnty.ClientID%>';
        var txtRate = '<%=txtRate.ClientID%>';
        var ddlStore = '<%=ddlStore.ClientID%>';
        var txtAuditDate = '<%=txtAuditDate.ClientID%>';
        var btnUpdate = '<%= btnUpdate.ClientID%>';
        var hdnSlNo = '<%= hdnSlNo.ClientID%>';
        var cbEditAlowed = '<%= cbEditAlowed.ClientID%>';

        var txtItemGroupNameParent = '<%=txtItemGroupNameParent.ClientID %>';
    <%--    var hdnGroupID = '<%=hdnGroupID.ClientID %>';--%>
        var btnGroupID = '<%=btnGroupID.ClientID %>';

        $(document).ready(function () {

            var divSearchPreviousSR = $("#divUpdateOpening").dialog({
                autoOpen: false,
                resizable: false,
                modal: true,
                closeOnEscape: true,
                width: 1100,
                height: 550
            });

            divSearchPreviousSR.parent().appendTo(jQuery("form:first"));

            //$('#' + btnLoadItem).click(function () {
            //    $("#divUpdateOpening").dialog("open");
            //});

            $('#' + btnLoadItem).click(function () {
                var groupId = $('#' + hdnItemGroupIDParent).val();
                var itemId = $('#' + hdnItemIdForFilter).val();
                var itemName = $('#' + txtItemName).val();
                //if (itemName != "") {
                LoadOpeningBalanceListByGroupId(0, itemId, groupId);
                //} else {
                //    alert('Please select Item.');
                //}
               
            });

            $('#' + btnUpdate).click(function () {
                Update();
            });
            //if ($('#' + txtGroupName).is(':visible')) {
            //    bindGroupList();
            //}
            if ($('#' + txtItemName).is(':visible')) {
                bindItemList();
            }

            if ($('#' +txtItemGroupNameParent ).is(':visible')) {
                bindGroupList();
            }

            //this is for item group tree hierarchy

            //$groupPopup = $('#' + groupPopupID).ItemGroupTree({
            //    title: 'Select Item Group',
            //    autoLink: true,
            //    autoLinkUpdate: true,
            //    linkControlID: dvItemGroupID,
            //    highlightLink: true,
            //    keyboard: true,

            //    okclick: function (event, data) {
            //        //alert('ok');
            //        SetItemGroupData(data);
            //        //OnGLGroupChange(data.glclassid, data.glgroupid);
            //        //ContentForm.MakeControlIsDirty(txtGLGroupNameParent, true);
            //    },
            //    open: function (event, ui) {
            //        // $("#dvGLGroup").addClass("dvGLGroupSelected");
            //    },
            //    close: function (event, ui) {
            //        //            $("#dvGLGroup").removeClass("dvGLGroupSelected");
            //        //            $('#' + ctlGLGroupText).focus();
            //        //            $('#' + ctlGLGroupText).select();
            //    }
            //});


            //$("#" + dvItemGroupID).find('.btnPopup').click(function (e) {
            //    //alert('ok');
            //    OpenItemGroupTree();
            //    //$("#" + groupPopupID).GroupTree("show", '');
            //});

            //$("#" + txtItemGroupNameParent).keydown(function (e) {
            //    switch (e.keyCode) {
            //        case 46:  //delete
            //            ClearData();
            //            break;
            //        case 8:  //backspace
            //            ClearData();
            //            e.preventDefault();
            //            break;
            //        case 13:  //enter
            //            OpenItemGroupTree();
            //            e.preventDefault();
            //            break;

            //    }

            //    //delete 
            //    if (e.keyCode == 46) {
            //        //alert('delete');
            //        ClearData();
            //    }
            //    // backspace
            //    if (e.keyCode == 8) {
            //        //alert('delete');
            //        ClearData();
            //        e.preventDefault();
            //    }

            //});


            //$("#" + dvItemGroupID).find('.btnClear').click(function (e) {
            //    ClearData();
            //});











        });



        function bindGroupList() {
            var cgColumns = [
                             //{ 'columnName': 'itemgroupid', 'width': '80', 'align': 'left', 'highlight': 2, 'label': 'Group ID' }
                             { 'columnName': 'itemgroupcode', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Code' }
                            , { 'columnName': 'itemgroupdesc', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                            , { 'columnName': 'itemgroupnameparent', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Group Parent' }
                            , { 'columnName': 'itemcode', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Next Item Code' }

            ];
            var serviceURL = ItemGroupListServiceLink + "?isterm=1&includeempty=0&hasitem=1&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;

            serviceURL += "&ispaging=1";
            var groupIDElem = $('#' + txtItemGroupNameParent);

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
                        $('#' + hdnItemGroupIDParent).val(ui.item.itemgroupid);
                        $('#' + txtItemGroupNameParent).val(ui.item.itemgroupdesc);
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
                    $('#' + txtItemGroupNameParent).val('');
                    $('#' + hdnItemGroupIDParent).val('0');
                   
                }
            });
        }

        function ResetAfterSave() {
            ResetInput("divUpdateOpening");
            //ResetMessage();
            $('.has-warning').removeClass('has-warning');
        }
        function LoadOpeningBalanceListByGroupId(id, itemId,groupid) {

            //if (itemId != "" && itemId != "0") {
                var gcObj = null;
                $.ajax({
                    type: "POST",
                    url: "../Inventory/OpeningBalanceList.aspx/LoadOpeningBalanceListByGroupId",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                    data: JSON.stringify({ id: id, itemId: itemId, groupid: groupid }),
                    success: function (locdata) {
                        debugger;

                        if (locdata.d.length > 0) {
                            gcObj = locdata.d;
                            var stockCheck = false;
                            debugger;
                            var tr = "";
                            var serialNo = 1;
                            $.each(gcObj, function (key, val) {
                                debugger;
                                var openingQty = 0;
                                var opRate = 0;
                                if (val.OPENING_QTY>0)
                                {
                                    openingQty = val.OPENING_QTY;
                                }
                                if (val.ITEM_OP_RATE>0)
                                {
                                    //alert(val.ITEM_OP_RATE);
                                    opRate =parseFloat(val.ITEM_OP_RATE);
                                }

                                //alert(val.ITEM_NAME);
                                //if (openingQty > 0 && opRate > 0) {

                                    if (opRate>0) {
                                        tr = tr + '<tr>' +
                                 '<td><span class="slno" id="lblSlNo">' + serialNo.toString() + '</span></td>' +
                                     '<td><input type="hidden" id="hdnItemId" value=' + val.ITEM_ID + '><input type="hidden" id="hdnUomId" value=' + val.UOM_ID + '><input type="hidden" id="hdnslId" value=' + val.SLNO + '><span id="lblItemName">' + val.ITEM_NAME + '</span></td>' +
                                     
                                        '<td><span id="lblITEM_CODE">' + val.ITEM_CODE + '</span></td>' +
                                       '<td><span id="lblOpeningQty">' + openingQty + '</span></td>' +
                                        '<td><span id= "lblUomName" >' + val.UOM_NAME + '</span></td>' +
                                      '<td><span id="lblOpeningRate">' +  opRate + '</span></td>' +
                                       '<td><input type="hidden" id="hdnStore" value=' + val.STORE_ID + '><span id="lblStoreName">' + val.STORE_NAME + '</span></td>' +
                                       '<td><span id="lblAuditDate">' + GetJsonDate(val.BAL_AUDIT_DATE) + '</span></td>' +
                                        '<td><span id="lblEditAlowed">' + val.EDIT_ALLOWED + '</span></td>' +
                                         '<td><input type="button" class="editRowWiseItem buttonEditGrid" title="Edit"></td>' +
                                  '<td></td>' +
                               '<td></td>' +
                             '</tr>';
                                    } else {
                                tr = tr + '<tr>' +
                                  '<td><span class="slno"  id="lblSlNo">' + serialNo.toString() + '</span></td>' +
                                      '<td><input type="hidden" id="hdnItemId" value=' + val.ITEM_ID + '><input type="hidden" id="hdnUomId" value=' + val.UOM_ID + '><input type="hidden" id="hdnslId" value=' + val.SLNO + '><span id="lblItemName">' + val.ITEM_NAME + '</span></td>' +
                                       '<td><span id="lblITEM_CODE">' + val.ITEM_CODE + '</span></td>' +
                                         '<td><span id="lblOpeningQty">' + openingQty + '</span></td>' +
                                         '<td><span id= "lblUomName" >' + val.UOM_NAME + '</span></td>' +
                                       '<td><span id="lblOpeningRate">' + opRate + '</span></td>' +
                                        '<td><input type="hidden" id="hdnStore" value=' + val.STORE_ID + '><span id="lblStoreName">' + val.STORE_NAME + '</span></td>' +
                                        '<td><span id="lblAuditDate">' + GetJsonDate(val.BAL_AUDIT_DATE) + '</span></td>' +
                                         '<td><span id="lblEditAlowed">' + val.EDIT_ALLOWED + '</span></td>' +
                                   '<td><input type="button" class="editRowWiseItem buttonEditGrid" title="Edit"></td>' +
                                '<td></td>' +
                              '</tr>';
                                    }

                            });

                            //if (stockCheck) {
                            //    $('#' + ddlIndentTo).prop('disabled', false);
                            //} else {
                            //    $('#' + ddlIndentTo).prop('disabled', true);
                            //}

                            $('#tblItemList').find('tbody').html(tr);
                            //$('#tblItemList').show();
                            //GenerateNewSlNo("tblItemList");
                            serialNo = parseInt(serialNo) + 1;
                         $('#' + hdnItemIdForFilter).val('');
                         $('#' + txtItemName).val('');
                        } else {
                            $('#tblItemList').find('tbody').html('<tr><td colspan="8"><h1 style="color:red">No Item Found</h1></td></tr>');
                        }
                     

                        
                    },
                    error: function (result) {
                        alert(result.responseText);
                    }

                });
            //} else {
            //    alert('Please select item.');
            //}



        }
        var selectedrow = "";
        $(document).on('click', '.editRowWiseItem', function () {
            debugger;
            var parentTr = $(this).closest('tr');

            $('tr').removeClass('selectrow');
            $(parentTr).addClass('selectrow');
            selectedrow = parentTr;
            var itemName = $(parentTr).find('#lblItemName').text();
            var UnitName = $(parentTr).find('#lblUomName').text();
            var openingQty = $(parentTr).find('#lblOpeningQty').text();
            var openingRate = $(parentTr).find('#lblOpeningRate').text();
            var editAlowed = $(parentTr).find('#lblEditAlowed').text();
            var storeName = $(parentTr).find('#lblStoreName').text();
            var slNo = $(parentTr).find('#hdnslId').val();
            var slNo2 = $(parentTr).find('#lblSlNo').text();
            var storeId = $(parentTr).find('#hdnStore').val();
            var auditDate = $(parentTr).find('#lblAuditDate').text();


            //alert(slNo2);
            //if (storeId!="" && storeId!="0")
            //{
            //    $('#' + ddlStore).val(storeId);
            //}

            if (auditDate != "") {
                $('#' + txtAuditDate).val(auditDate);
            } else {
                $('#' + txtAuditDate).val(GetCurrentDate());
            }

            $('#' + lblSlNo).text(slNo2);
            $('#' + hdnSlNo).val(slNo);
            $('#' + lblItemName).text(itemName);
            $('#' + lblUomName).text(UnitName);
            $('#' + txtOpeningQnty).val(openingQty);

           
            //if (parseInt(openingQty) > 0)
            //{
                //$('#' + txtOpeningQnty).attr('readonly', true);
            //}
            //else
            //{
            //    $('#' + txtOpeningQnty).attr('readonly', false);
            //}

            $('#' + txtRate).val(openingRate);

            if (parseInt(openingRate) > 0) {
                $('#' + txtRate).attr('readonly', false);
            }
            else {
                $('#' + txtRate).attr('readonly', false);
            }

            //if (parseInt(openingQty) > 0 && parseInt(openingRate) > 0)
            if (parseInt(openingRate) > 0)
            {
                //$('#' + btnUpdate).prop("disabled", true);
                $('#' + btnUpdate).removeAttr('disabled');
            }
            else
            {
                $('#' + btnUpdate).removeAttr('disabled');
            }

            $('#' + uomIdhdn).val($(parentTr).find('#hdnUomId').val());
            $('#' + itemIdhdn).val($(parentTr).find('#hdnItemId').val());
          

            if (editAlowed == "Y") {
                $('#' + cbEditAlowed).prop('checked', true);
            }
            else {
                $('#' + cbEditAlowed).prop('checked', true);
            }


            $("#divUpdateOpening").dialog("open");
        });

        function Update() {
            $(".has-warning").removeClass("has-warning");
            var isValid = true;

            isValid = ValidateInput("#tblUpdate");
            if (!isValid) {
                return false;
            }

            //if ($('#' + txtOpeningQnty).val() != "0") {
            //    $('#' + txtOpeningQnty).removeClass('has-warning');
              
            //} else {
            //    $('#' + txtOpeningQnty).addClass('has-warning');
            //    return false;
            //}


            if ($('#' + txtRate).val() != "0") {
                $('#' + txtRate).removeClass('has-warning');

            } else {
                $('#' + txtRate).addClass('has-warning');
                return false;
            }

            if (isValid) {
                debugger;
                var cObj = new Object();
                cObj.SLNO = $('#' + lblSlNo).text();

                //alert(cObj.SLNO);
                
                if (cObj.SLNO != "" && cObj.SLNO != "0") {
                    cObj.SLNO = Number(cObj.SLNO);
                } else {
                    cObj.SLNO = Number(0);
                }


                //alert(cObj.SLNO);
                cObj.OPENING_QTY = $('#' + txtOpeningQnty).val();
                cObj.ITEM_OP_RATE = $('#' + txtRate).val();
                cObj.BAL_AUDIT_DATE = $('#' + txtAuditDate).val();
                cObj.STORE_ID = $('#' + ddlStore).val();
                cObj.EDIT_ALLOWED = $('#' + cbEditAlowed).is(':checked') ? "Y" : "N";
                cObj.ENTRY_BY = 0;
                cObj.EDIT_BY = 0;
                cObj.ITEM_ID = $('#' + itemIdhdn).val();
                cObj.UOM_ID = $('#' + uomIdhdn).val();
             
                var storeName = $('#' + ddlStore + " option:selected").text();

                //var editAlowed = $(parentTr).find('#lblEditAlowed').text();
               // var storeName = $(parentTr).find('#lblStoreName').text();

                var y = confirm('Are you sure to Save?');
                if (y) {


                    $.ajax({
                        type: "POST",
                        url: "../Inventory/OpeningBalanceList.aspx/Update",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        async: true,
                        data: JSON.stringify({ obj: cObj }),
                        success: function (result) {
                            var data = JSON.parse(result.d);
                            if (data.Status == "Success") {
                                ResetAfterSave();
                                alert(data.Message);
                                $(selectedrow).find('#lblOpeningQty').text(cObj.OPENING_QTY);
                                $(selectedrow).find('#lblOpeningRate').text(cObj.ITEM_OP_RATE);
                                $(selectedrow).find('#lblEditAlowed').text(cObj.EDIT_ALLOWED);
                                $(selectedrow).find('#lblStoreName').text(storeName);
                                $("#divUpdateOpening").dialog("close");

                            }else if (data.Status == "Exist") {
                                alert(data.Message);
                            }
                            else {
                                alert(data.Message);

                            }

                        },
                        error: function (result) {
                            alert(result.responseText);
                        }

                    });
                }


            }

        }



        //this is for group dropdown

        //function bindGroupList() {
        //    var cgColumns = [
        //                     //{ 'columnName': 'itemgroupid', 'width': '80', 'align': 'left', 'highlight': 2, 'label': 'Group ID' }
        //                     { 'columnName': 'itemgroupcode', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Code' }
        //                    , { 'columnName': 'itemgroupdesc', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Name' }
        //                    , { 'columnName': 'itemgroupnameparent', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Group Parent' }


        //    ];
        //    var serviceURL = ItemGroupListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;

        //    serviceURL += "&ispaging=1";
        //    var groupIDElem = $('#' + txtGroupName);

        //    $('#' + btnGroupID).click(function (e) {
        //        $(groupIDElem).combogrid("dropdownClick");
        //    });

        //    $(groupIDElem).combogrid({
        //        debug: true,
        //        searchButton: false,
        //        resetButton: false,
        //        alternate: true,
        //        munit: 'px',
        //        scrollBar: true,
        //        showPager: true,
        //        colModel: cgColumns,
        //        autoFocus: true,
        //        showError: true,
        //        width: 600,
        //        url: serviceURL,
        //        search: function (event, ui) {
        //            //var companyCode = $('#' + ddlCompany).val();
        //            //var branchCode = $('#' + hdnBranch).val();
        //            //var deptCode = $('#' + hdnDepartment).val();
        //            //var locationid = $('#' + lblLocationID).val();
        //            // var seid = $('#' + txtExecutiveID).val();

        //            var newServiceURL = serviceURL;
        //            $(this).combogrid("option", "url", newServiceURL);


        //        },
        //        select: function (event, ui) {
        //            if (!ui.item) {
        //                event.preventDefault();

        //                // $('#' + hdnDealerID).val('0');
        //                //$('#' + txtDealerID).val('');
        //                return false;
        //                //ClearGLAccountData(elemID);
        //            }


        //            if (ui.item.dealerid == '') {
        //                event.preventDefault();
        //                return false;
        //                //ClearGLAccountData(elemID);
        //            }
        //            else {
        //                $('#' + hdnGroupID).val('0');
        //                // $('#' + hdnDealerID).val(ui.item.dealerid);
        //                $('#' + hdnGroupID).val(ui.item.itemgroupid);
        //                $('#' + txtGroupName).val(ui.item.itemgroupdesc);
        //                //$('#' + txtGroupCode).val(ui.item.itemgroupcode);

        //            }
        //            return false;
        //        },

        //        lc: ''
        //    });


        //    $(groupIDElem).blur(function () {
        //        var self = this;

        //        var groupID = $(groupIDElem).val();
        //        if (groupID == '') {
        //            // $('#' + hdnDealerID).val('0');
        //            $('#' + txtGroupName).val('');
        //            //$('#' + txtGroupCode).val('');
        //        }
        //    });
        //}

        //this is for item group tree

        //function OpenItemGroupTree() {


        //    if ($("#" + txtItemGroupNameParent).is(":disabled")) {
        //        $("#" + groupPopupID).ItemGroupTree("option", "enableSelect", false);
        //    }
        //    else {
        //        $("#" + groupPopupID).ItemGroupTree("option", "enableSelect", true);
        //    }

        //    if ($("#" + txtItemGroupNameParent).is(":disabled") == false) {
        //        var itemGroupKey = $("#" + hdnItemGroupParentKey).val();

        //        $("#" + groupPopupID).ItemGroupTree("show", itemGroupKey);
        //    }
        //}


        //function SetItemGroupData(data) {

        //    $("#" + txtItemGroupNameParent).val(data.itemgroupnameshow);
        //    $("#" + hdnItemGroupIDParent).val(data.itemgroupid);
        //    $("#" + hdnItemGroupParentKey).val(data.itemgroupkey);
        //    $("#" + txtItemName).val('');
        //    $("#" + hdnItemIdForFilter).val('');


        //}



        //function ClearData() {
        //    if ($('#' + txtItemGroupNameParent).is(":disabled") == false) {
        //        $("#" + txtItemGroupNameParent).val('');
        //        $("#" + hdnItemGroupIDParent).val('0');
        //        $("#" + hdnItemGroupParentKey).val('');
        //        $("#" + txtItemName).val('');
        //        $("#" + hdnItemIdForFilter).val('');
        //        ContentForm.MakeControlIsDirty(txtItmGroupNameParent, true);
        //    }
        //}

        //function OnGLGroupChange(glClassID, glGroupIDParent) {
        //    glClassID = glClassID || 0;
        //    glGroupIDParent = glGroupIDParent || 0;
        //    if (glGroupIDParent == 0) {
        //        if (glClassID == AccUtility.GLClass.Income | glClassID == AccUtility.GLClass.Expense) {
        //            $("#" + lblIsGrossProfit).css("visibility", "visible");
        //            $("#" + ddlIsGrossProfit).css("visibility", "visible");
        //            $("#" + ddlIsGrossProfit).val('0');
        //        }
        //        else {
        //            $("#" + lblIsGrossProfit).css("visibility", "hidden");
        //            $("#" + ddlIsGrossProfit).css("visibility", "hidden");
        //            $("#" + ddlIsGrossProfit).val($("#" + hdnGLGroupParentIsGrossProfit).val());
        //        }
        //    }
        //    else {
        //        $("#" + lblIsGrossProfit).css("visibility", "hidden");
        //        $("#" + ddlIsGrossProfit).css("visibility", "hidden");
        //        $("#" + ddlIsGrossProfit).val($("#" + hdnGLGroupParentIsGrossProfit).val());
        //    }


        //    var balanceType = $("#" + hdnGLGroupParentBalanceType).val();
        //    $("#" + ddlBalanceType).val(balanceType);

        //}

        //this is ending of item group tree






















        function bindItemList() {
            
            var cgColumns = [{ 'columnName': 'itemname', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                             , { 'columnName': 'uomname', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Uom Name' }
                             , { 'columnName': 'itemcode', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Code' }
                             , { 'columnName': 'itemgroupdesc', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Group Name' }
                             , { 'columnName': 'class_name', 'width': '120', 'align': 'left', 'highlight': 4, 'label': 'Class Name' }
                             , { 'columnName': 'item_type', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Item Type' }



            ];
            var serviceURL = ItemListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            

            serviceURL += "&ispaging=1";
            var groupIDElem = $('#' + txtItemName);

            $('#' + btnItemLoad).click(function (e) {
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
                width: 850,
                url: serviceURL,
                search: function (event, ui) {
                    //var companyCode = $('#' + ddlCompany).val();
                    //var branchCode = $('#' + hdnBranch).val();
                    //var deptCode = $('#' + hdnDepartment).val();
                    //var locationid = $('#' + lblLocationID).val();
                    // var seid = $('#' + txtExecutiveID).val();
                    var vgroupid = 0;

                  //  var vgroupid = $(hdnItemGroupIDParent).val();
                    //var vgroupid = $('#' + hdngroupId).val();
                  //  newServiceURL = JSUtility.AddTimeToQueryString(newServiceURL);
                    var newServiceURL = serviceURL + "&groupid=" + vgroupid;

                    var ddlItemType = $('[id*=ddlItemType] option:selected').val();
                    newServiceURL = newServiceURL + "&itemtypeid=" + ddlItemType;

                    newServiceURL = JSUtility.AddTimeToQueryString(newServiceURL);
                   // var newServiceURL = serviceURL;
                    $(this).combogrid("option", "url", newServiceURL);


                },
                select: function (event, ui) {
                    if (!ui.item) {
                        event.preventDefault();
                        $('#' + hdnItemIdForFilter).val('0');
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
                        $('#' + hdnItemIdForFilter).val('0');
                        // $('#' + hdnDealerID).val(ui.item.dealerid);
                        $('#' + hdnItemIdForFilter).val(ui.item.itemid);
                        $('#' + txtItemName).val(ui.item.itemname);
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
                    $('#' + txtItemName).val('');
                    //$('#' + txtGroupCode).val('');
                }
            });
        }

        // ]]>
    </script>


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

        .addItemBackImage {
            background-color: #5CB85C;
            border-radius: 6px;
            color: white;
            height: 20px;
            width: 40px;
        }


        .selectrow {
            background-color: #9eddff !important;
            font-weight: bold;
        }

        .auto-style3 {
            height: 34px;
        }
    </style>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:HiddenField ID="hdnItemGroupIDParent" runat="server" />
    <div id="dvPageContent" style="width: 100%; min-height: 300px;">

        <div id="dvContentHeader" class="dvContentHeader">
            <div id="dvHeader" class="dvHeader">
                <asp:Label ID="lblHeader" runat="server" Text="Opening Balance" CssClass="lblHeader"></asp:Label>
            </div>
            <div id="dvMessage" class="dvMessage">
                <asp:Label ID="lblMessage" runat="server" CssClass="lblMessage" Text="" Width="100%"></asp:Label>
            </div>

        </div>

        <div id="dvContentMain" class="dvContentMain" style="min-height: 350px;">

            <div id="dvControlsHead" style="height: auto; width: 100%;">
                <table style="" cellspacing="2" border="0">

                    <%--<tr>
                        <td></td>
                        <td><asp:Label ID="Label3" runat="server" Text="Item Group"></asp:Label></td>
                        <td align="left" colspan="2">
                            <div id="dvItemGroup" class="group_linkControl dvGroup" runat="server">
					   <table cellpadding="0" cellspacing="0" border="0">
						   <tr>
							   <td style="">
								   <asp:TextBox ID="txtItemGroupNameParent" runat="server" 
									   CssClass="group_linkText textBoxReadOnlyEdit fldRequired enableIsDirty" Width="350px" 
										></asp:TextBox>
							   </td>
							   <td>
								   <div class="btnPopup">
								   </div>
							   </td>
							   <td>
								   <div class="btnClear">
								   </div>
							   </td>
						   </tr>
					   </table>
					   <input type="hidden" ID="hdnItemGroupIDParent" runat="server"  value="0" />
					   <input type="hidden" ID="hdnItemGroupParentKey" runat="server"  value="" />

					   <input type="hidden" ID="hdnItemGroupIDParentEdit" runat="server"  value="0" />

				   </div>
                        </td>
                    </tr>--%>
                    

                    <tr>
                        <td></td>
                        <td style="" align="right">
                            <asp:Label ID="Label10" runat="server" Text="Group "></asp:Label>
                        </td>
                        <td style="" align="left">
                            <table>
                                <tr>
                                    <td> <asp:TextBox ID="txtItemGroupNameParent" Width="200px" runat="server" CssClass="textBox required" Enabled="true"></asp:TextBox></td>
                                    <td><input id="btnGroupID" type="button" value="" runat="server" class="buttonDropdown" tabindex="-1"  /></td>
                                </tr>
                            </table>
                            
                            
                        </td>
                        <td></td>
                        <td style="" align="right">
                             
                        </td>
                        <td></td>
                       

                    </tr>
                     <tr>
                         <td></td>

                        <td align="right">Item Type
           
                        </td>

                        <td>
                         <asp:DropDownList ID="ddlItemType" runat="server" CssClass="dropDownList" Width="120" >
                                                                                           
                         </asp:DropDownList>


                        </td>
                    </tr>

                    <tr>
                        <td></td>
                        <td style="" align="right">
                            <asp:Label ID="Label2" runat="server" Text="Item "></asp:Label>
                        </td>
                        <td style="" align="left">
                            <table>
                                <tr>
                                    <td> <asp:TextBox ID="txtItemName" Width="200px" runat="server" CssClass="textBox required" Enabled="true"></asp:TextBox></td>
                                    <td><input id="btnItemLoad" type="button" value="" runat="server" class="buttonDropdown" tabindex="-1"  /></td>
                                </tr>
                            </table>
                            
                            
                        </td>
                        <td></td>
                        <td style="" align="right">
                            <asp:HiddenField ID="hdnItemIdForFilter" runat="server" />
                        </td>
                        <td></td>
                       

                    </tr>
                 
                    <tr>
                        <td></td>
                        <td style="" align="right">
                           
                        </td>
                        <td style="" align="left">
                            
                            <input id="btnLoadItem" runat="server" class="buttonSearch" type="button" value="Load" style="height:25px"/></td>
                        <td></td>
                       
                    </tr>
                   
                </table>

            </div>
            <div>
                <table class="cmnTable" style="text-align: left; width: 100%" border="0" cellspacing="4" cellpadding="2" id="tblItemList">
                    <thead>
                        <tr>
                            <th>SL.</th>
                            <th>Name</th>
                            <th>Item Code</th>
                            <th>Opening Qty</th>
                            <th>Uom</th>
                            <th>Opening Rate</th>
                            <th>Store</th>
                            <th>Audit Date</th>
                            <th>Editable</th>
                            <th>Action</th>
                        </tr>
                    </thead>
                    <tbody></tbody>
                </table>
            </div>


        </div>
        <div id="dvGridFooter" style="width: 100%; height: 25px; font-size: smaller;" class="subFooter">
            <table class="cmnTable" style="text-align: left; width: 100%" border="0" cellspacing="4" cellpadding="2" id="tblSRList">
                <tr>
                    <td width="5px" align="left" class="auto-style3"></td>
                    <td align="left" class="auto-style3">
                        <asp:Label ID="lblTotal" runat="server" Text="Total: 0"
                            Style="width: 96px;"></asp:Label>
                        <asp:HiddenField ID="hdnRowCount" runat="server" Value="0" />
                    </td>
                    <td width="50px" class="auto-style3"></td>

                </tr>
            </table>
        </div>

    </div>



    <div id="divUpdateOpening" style="background-color: #ddeeff; max-height: 280px;">
        <asp:HiddenField ID="hdnUomId" runat="server" />
        <asp:HiddenField ID="hdnItemId" runat="server" />
        <div style="min-height: 30px; width: 100%;">
            <asp:Label ID="lblReplaceMessage" runat="server" Text="" Font-Bold="true" Font-Size="20px"></asp:Label>
        </div>
        <div class="dvHeader" align="center" style="width: 100%; height: auto">
            <asp:Label ID="Label4" runat="server" Text="Update Opening Balance " Font-Bold="true" Font-Size="Large"></asp:Label>
        </div>
        <div class="dvContentMain">
            <input type="hidden" id="hdnSlNo" runat="server" />
            <table border="0" cellspacing="4" cellpadding="2" align="center" style="width: 100%" id="tblUpdate">
                  <tr>
                    <td style="" align="right">
                        <asp:Label ID="Label3" runat="server" Text="Sl No:" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="left" class="auto-style1">

                        <asp:Label ID="lblSlNo" runat="server" Text="" Font-Bold="true"></asp:Label>
                    </td>

                </tr>
                <tr>
                    <td style="" align="right">
                        <asp:Label ID="lblBSRNO" runat="server" Text="Item Name :" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="left" class="auto-style1">

                        <asp:Label ID="lblItemName" runat="server" Text="" Font-Bold="true"></asp:Label>
                    </td>

                </tr>
                <tr>
                    <td style="" align="right">
                        <asp:Label ID="Label5" runat="server" Text="Unit :" Font-Bold="True"></asp:Label>
                    </td>
                    <td align="left" class="auto-style1">
                        <asp:Label ID="lblUomName" runat="server" Text="" Font-Bold="True"></asp:Label>
                    </td>

                </tr>
                <tr>
                    <td style="" align="right">
                        <asp:Label ID="Label6" runat="server" Text="Opening Quantity:" Font-Bold="True"></asp:Label><span style="color: red">*</span>
                    </td>
                    <td align="left" class="auto-style1">
                        <asp:TextBox ID="txtOpeningQnty" runat="server" CssClass="textBox required textNumberOnly numberOnly"></asp:TextBox>

                    </td>

                </tr>
                <tr>
                    <td style="" align="right">
                        <asp:Label ID="Label7" runat="server" Text="Rate:" Font-Bold="True"></asp:Label><span style="color: red">*</span>
                    </td>
                    <td align="left" class="auto-style1">
                        <asp:TextBox ID="txtRate" runat="server" CssClass="textBox required textNumberOnly numberOnly"></asp:TextBox>

                    </td>

                </tr>
                <tr>
                    <td style="" align="right">
                        <asp:Label ID="Label8" runat="server" Text="Store" Font-Bold="True"></asp:Label><span style="color: red">*</span>
                    </td>
                    <td align="left" class="auto-style1">
                        <asp:DropDownList ID="ddlStore" runat="server" Width="165" CssClass="dropDownList required"></asp:DropDownList>

                    </td>

                </tr>

                <tr>
                    <td style="" align="right">
                        <asp:Label ID="Label9" runat="server" Text="Audit Date" Font-Bold="True"></asp:Label>
                    </td>
                    <td align="left" class="auto-style1">
                        <asp:TextBox ID="txtAuditDate" runat="server" Width="160px" CssClass="textBox textDate dateParse required"></asp:TextBox>

                    </td>

                </tr>
                <tr>
                    <td style="" align="right">
                        <asp:Label ID="Label1" runat="server" Text="Edit Alowed" Font-Bold="True"  ></asp:Label>
                    </td>
                    <td align="left" class="auto-style1">
                        <input type="checkbox" id="cbEditAlowed" runat="server"/>

                    </td>

                </tr>
                <tr>
                    <td style="" align="right"></td>
                    <td align="left" class="auto-style1">
                        <input id="btnUpdate" type="button" runat="server" class="buttonSave" value="Update" />
                    </td>

                </tr>
            </table>
        </div>
    </div>


  <%--  <div id="dvPopupItemGroup" class="dvPopupGroup" runat="server">
		<uc1:itemgrouptree ID="ItemGroupTree1" runat="server" />
	</div>--%>
</asp:Content>

