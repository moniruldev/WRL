<%@ Page Title="" Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="Solar_Battery_Conversion.aspx.cs" Inherits="PG.Web.Production.Solar_Battery_Conversion" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="../javascript/jquery.ui.combogrid.js" type="text/javascript"></script>
    <script src="../javascript/jquery.attributeobserver.js" type="text/javascript"></script>
    <link href="../css/jquery.ui.combogrid.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">

        var ItemListServiceLink = '<%=this.ItemListServiceLink%>';
        var PanelUOMServiceLink = '<%= this.PanelUOMServiceLink %>';
        var ddlDeptID = '<%=ddlDeptID.ClientID%>';
       
        var hdnDeptID = '<%=hdnDeptID.ClientID %>';
        var txtUOMName = '<%=txtUOMName.ClientID%>';
        var btnUOMName = '<%=btnUOMName.ClientID%>';
        var hdnUOMID = '<%=hdnUOMID.ClientID%>';
        var btnSave = '<%=btnSave.ClientID%>';

        var txtFromItemName = '<%=txtFromItemName.ClientID%>';
        var txtToItemName = '<%=txtToItemName.ClientID%>';
        var hdnFromItemID = '<%=hdnFromItemID.ClientID%>';
        var hdnToItemID = '<%=hdnToItemID.ClientID%>';
        var txtBOM_NO = '<%=txtBOM_NO.ClientID%>';
        var hdnBOM_ID = '<%=hdnBOM_ID.ClientID%>';

        var btnFromItemID = '<%=btnFromItemID.ClientID%>';
        var btnToItemID = '<%=btnToItemID.ClientID%>';


       <%-- var txtGridWeightPanel = '<%=txtGridWeightPanel.ClientID%>';--%>

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

        $(document).ready(function () {


            if ($('#' + txtFromItemName).is(':visible')) {

                bindFromItemList();

            }
            if ($('#' + txtToItemName).is(':visible')) {

                bindToItemList();

            }

            //if ($('#' + txtUOMName).is(':visible')) {
            //    bindUomList();
            //}


            $("#" + btnSave).click(function (e) {

                var rValue = confirm("Are you sure to save?");
                return rValue;
            });



        });

        function bindUomList() {

            var cgColumns = [
               //{ 'columnName': 'uomid', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'UOM ID' }
               //             ,
                            { 'columnName': 'uomcodeshort', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'UOM Short Name' }
                            , { 'columnName': 'uomname', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'UOM Name' }
                            , { 'columnName': 'pcqty', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Pc Qty' }

            ];

            var itemServiceURL = PanelUOMServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;

            itemServiceURL += "&ispaging=1";
            var itemIDElem = $('#' + txtUOMName);

            $('#' + btnUOMName).click(function (e) {
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
                width: 400,
                url: itemServiceURL,
                search: function (event, ui) {

                    var vdeptid = $('#' + hdnDeptID).val();
                    var newServiceURL = itemServiceURL + "&deptid=" + vdeptid;
                    $(this).combogrid("option", "url", newServiceURL);


                },
                select: function (event, ui) {
                    if (!ui.item) {
                        event.preventDefault();


                        $('#' + hdnUOMID).val('0');
                        return false;
                        //ClearGLAccountData(elemID);
                    }


                    if (ui.item.itemid == '') {
                        event.preventDefault();
                        return false;
                        //ClearGLAccountData(elemID);
                    }
                    else {

                        $('#' + hdnUOMID).val(ui.item.uomid);
                        $('#' + txtUOMName).val(ui.item.uomcodeshort);



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
                    $('#' + txtUOMName).val('');
                    //$('#' + txtGroupCode).val('');
                    $('#' + hdnUOMID).val('0');

                }
            });
        }


        function bindFromItemList() {

            var cgColumns = [
                              { 'columnName': 'itemname', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                             , { 'columnName': 'itemcode', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Code' }
                             , { 'columnName': 'itemgroupdesc', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Group Name' }
                             , { 'columnName': 'uomname', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Uom' }
                             // , { 'columnName': 'class_name', 'width': '120', 'align': 'left', 'highlight': 4, 'label': 'Class Name' }
                             //, { 'columnName': 'item_type', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Item Type' }
                             , { 'columnName': 'bomno', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'BOM' }
            ];

            var itemServiceURL = ItemListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;

            itemServiceURL += "&ispaging=1&isFinished=Y&for_production=Y";
            var itemIDElem = $('#' + txtFromItemName);

            $('#' + btnFromItemID).click(function (e) {
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
                width: 750,
                url: itemServiceURL,
                search: function (event, ui) {

                    var vdeptid = $('#' + ddlDeptID).val();
                    var newServiceURL = itemServiceURL + "&deptid=" + vdeptid;
                    $(this).combogrid("option", "url", newServiceURL);


                },
                select: function (event, ui) {
                    if (!ui.item) {
                        event.preventDefault();


                        $('#' + hdnFromItemID).val('0');
                        return false;
                        //ClearGLAccountData(elemID);
                    }


                    if (ui.item.itemid == '') {
                        event.preventDefault();
                        return false;
                        //ClearGLAccountData(elemID);
                    }
                    else {
                        
                        $('#' + hdnFromItemID).val(ui.item.itemid);
                        $('#' + txtFromItemName).val(ui.item.itemname);
                        $('#' + hdnBOM_ID).val(ui.item.bomid);
                        $('#' + txtBOM_NO).val(ui.item.bomno);
                        
                    }
                    return false;
                },

                lc: ''
            });

            $(itemIDElem).blur(function () {
                var self = this;
                var groupID = $(itemIDElem).val();
                if (groupID == '') {
                    $('#' + txtFromItemName).val('');
                    $('#' + hdnFromItemID).val('0');
                    $('#' + txtBOM_NO).val('');
                    $('#' + hdnBOM_ID).val('0');

                }
                else {
                    var serviceURL = ItemListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
                    serviceURL += "&ispaging=1&isFinished=Y&for_production=Y";
                    var eCode = $('#' + txtFromItemName).val();
                    var prcNo = GetItemNo(eCode, serviceURL);
                    if (prcNo == null) {
                        $('#' + txtFromItemName).val('');
                        $('#' + hdnFromItemID).val('0');
                        $('#' + txtBOM_NO).val('');
                        $('#' + hdnBOM_ID).val('0');
                    }
                }
            });
        }


        function bindToItemList() {

            var cgColumns = [
                              { 'columnName': 'itemname', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                             , { 'columnName': 'itemcode', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Code' }
                             , { 'columnName': 'itemgroupdesc', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Group Name' }
                             , { 'columnName': 'uomname', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Uom Name' }
                              , { 'columnName': 'class_name', 'width': '120', 'align': 'left', 'highlight': 4, 'label': 'Class Name' }
                             , { 'columnName': 'item_type', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Item Type' }
            ];

            var itemServiceURL = ItemListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;

            itemServiceURL += "&ispaging=1";
            var itemIDElem = $('#' + txtToItemName);

            $('#' + btnToItemID).click(function (e) {
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
                width: 750,
                url: itemServiceURL,
                search: function (event, ui) {

                    var vdeptid = $('#' + ddlDeptID).val();
                    var newServiceURL = itemServiceURL + "&deptid=" + vdeptid;
                    $(this).combogrid("option", "url", newServiceURL);


                },
                select: function (event, ui) {
                    if (!ui.item) {
                        event.preventDefault();


                        $('#' + hdnToItemID).val('0');
                        return false;
                        //ClearGLAccountData(elemID);
                    }


                    if (ui.item.itemid == '') {
                        event.preventDefault();
                        return false;
                        //ClearGLAccountData(elemID);
                    }
                    else {

                        $('#' + hdnToItemID).val(ui.item.itemid);
                        $('#' + txtToItemName).val(ui.item.itemname);



                    }
                    return false;
                },

                lc: ''
            });

            $(itemIDElem).blur(function () {
                var self = this;
                var groupID = $(itemIDElem).val();
                if (groupID == '') {
                    $('#' + txtToItemName).val('');
                    $('#' + hdnToItemID).val('0');

                }
                else {
                    var serviceURL = ItemListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
                    serviceURL += "&ispaging=1&isFinished=Y&for_production=Y";
                    var eCode = $('#' + txtToItemName).val();
                    var prcNo = GetItemNo(eCode, serviceURL);
                    if (prcNo == null) {
                        $('#' + txtToItemName).val('');
                        $('#' + hdnToItemID).val('0');
                    }
                }
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

       

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="dvPageContent" style="width:100%;height:100%">
        <div id="dvContentHeader" class="dvContentHeader">
            <div id="dvHeader" class="dvHeader_Prod" align="left">
                <asp:Label ID="lblHeader" runat="server" Text="Battery Conversion" CssClass="lblHeader" Font-Bold="true" Font-Size="15px"></asp:Label>

            </div>
            <div id="dvMessage" class="dvMessage">
                <asp:Label ID="lblMessage" runat="server" CssClass="" Text="" Width="100%"></asp:Label>
            </div>
            <div id="dvHeaderControl" class="dvHeaderControl">
            </div>
        </div>
        <div id="dvContentMain" align="left" class="dvContentMain" style="height:auto">
            <div id="dvControlsHead" style="height: auto; width: 100%;" align="left">
                <table>
                    <tr>
                        
                        <td style="text-align:right;">
                            <asp:Label ID="Label2" runat="server" Text="Conversion NO : "></asp:Label>
                        </td>
                        <td>
                             <asp:TextBox ID="txtCONVERTNO" runat="server" Style="text-align: left; font-weight: 700;" CssClass="textBox" ForeColor="Red"  Enabled="False"></asp:TextBox>
                        </td>
                        <td>
                            <asp:HiddenField ID="hdnID" runat="server" Value="" />
                        </td>
                        <td>

                        </td>
                        <td style="text-align:right">
                             <asp:Label ID="lblDeptID" runat="server" Text="Department : "></asp:Label>
                        </td>
                         <td >
                      <asp:DropDownList ID="ddlDeptID" runat="server" CssClass="dropDownList required" Width="205px"></asp:DropDownList>
                        </td>
                        <td>
                            
                            <asp:HiddenField ID="hdnUOMID" runat="server" Value="1" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                                <asp:Label ID="lblRejectionDate" runat="server" Text="Date :" style="font-weight: 700"></asp:Label>
                        </td>
                        <td>
                             <asp:TextBox ID="txtConvertDate" runat="server" CssClass="textBox textDate dateParse" Style="text-align: left;" Width="100px"></asp:TextBox>
                        </td>
                        <td></td>
                        <td>
                  
                        </td>
                        <td style="text-align:right;">
                              <asp:Label ID="lblUOM" runat="server" Text="UOM:"></asp:Label>
                        </td>
                        <td>
                             <asp:TextBox ID="txtUOMName" runat="server" CssClass="textBox textAutoSelect" Width="160px" Height="20px" Text="Pcs" Enabled="False"></asp:TextBox>
                            <input id="btnUOMName" type="button" value="" runat="server" class="buttonDropdown" style="width: 15px " tabindex="-1" />
                        </td>
                    </tr>
                    <tr>
                        
                        <td  style="text-align:right;">
                            <asp:Label ID="lblItemID" runat="server" Text="From Item: "></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFromItemName" runat="server" CssClass="textBox textAutoSelect" Width="202px" Height="20px"></asp:TextBox>
                            <input id="btnFromItemID" type="button" value="" runat="server" class="buttonDropdown" style="width: 15px " tabindex="-1" />
                        </td>
                        <td>
                            <asp:HiddenField ID="hdnFromItemID" runat="server" />
                            
                        </td>
                        <td></td>
                        <td style="text-align: right">
                              <asp:Label ID="Label3" runat="server" Text="BOM : "></asp:Label>
                           
                        </td>
                        <td>
                               <asp:TextBox ID="txtBOM_NO" runat="server" CssClass="textBox textAutoSelect" Width="202px" Height="20px"></asp:TextBox>  
                        </td>
                        <td>
                             <asp:HiddenField ID="hdnBOM_ID" runat="server" />
                            &nbsp;</td>
                    </tr>

                     <tr>
                        
                        <td  style="text-align:right;">
                            <asp:Label ID="Label1" runat="server" Text="To Item: "></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtToItemName" runat="server" CssClass="textBox textAutoSelect" Width="202px" Height="20px"></asp:TextBox>
                            <input id="btnToItemID" type="button" value="" runat="server" class="buttonDropdown" style="width: 15px " tabindex="-1" />
                        </td>
                        <td>
                            <asp:HiddenField ID="hdnToItemID" runat="server" />
                            
                        </td>
                        <td></td>
                        <td style="text-align: right">
                             <asp:Label ID="lblQty" runat="server" Text="Quantity:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtQty" CssClass="textBox textAutoSelect" placeHolder="0" runat="server"  onkeypress=" return isNumberKey(event,this);" Text="0" ></asp:TextBox>
                        </td>
                        <td>
                            <asp:HiddenField ID="hdnDeptID" runat="server" Value="" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">Remarks :</td>
                        <td colspan="4">
                              <asp:TextBox ID="txtRemarks" CssClass="textBox textAutoSelect" Width="450px"   runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkRETURNRM" runat="server" Text="   Return RM    " Checked="false"  > </asp:CheckBox>
                        </td>
                    </tr>
                </table>

            </div>
            
            <div id="dvContentFooter" class="" align="left">
            <table>
                <tr>
                    <td style=""></td>
                    <td>
                        <asp:Button ID="btnAddNew" runat="server" Text="New" CssClass="buttonNew" OnClick="btnAddNew_Click"  />
                        
                    </td>
                    <td>
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonSave checkRequired" AccessKey="s" OnClientClick="if ( ! UserSaveConfirmation()) return false;" OnClick="btnSave_Click" />
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="buttonEdit" OnClick="btnEdit_Click" />
                    </td>
                    <td>
                        <asp:Button ID="btnAuthorize" runat="server" Text="Authorize" CssClass="buttoncommon" OnClientClick="if ( ! UserAuthorizeConfirmation()) return false;" OnClick="btnAuthorize_Click" />
                    </td>
                   
                    <td>
                        <asp:Button ID="btnRefresh" runat="server" Text="Refresh" CssClass="buttonRefresh checkIsDirty" OnClick="btnRefresh_Click" />
                    </td>
                    
                    <td>
                        <input id="btnClose" type="button" runat="server" class="buttonClose" value="Close" onclick="if (ContentForm) { ContentForm.CloseForm(); }" />
                    </td>
                   
                </tr>
            </table>
        </div>

        </div>

        

    </div>
</asp:Content>
