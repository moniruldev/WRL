<%@ Page Title="" Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="Plate_Weight_Entry.aspx.cs" Inherits="PG.Web.Production.Plate_Weight_Entry" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="../javascript/jquery.ui.combogrid.js" type="text/javascript"></script>
    <script src="../javascript/jquery.attributeobserver.js" type="text/javascript"></script>
    <link href="../css/jquery.ui.combogrid.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">

        var ItemListServiceLink = '<%=this.ItemListServiceLink%>';
        var PanelUOMServiceLink = '<%= this.PanelUOMServiceLink %>';
        var ddlDeptID = '<%=ddlDeptID.ClientID%>';
        var txtItemName = '<%=txtItemName.ClientID%>';
        var btnItemID = '<%= btnItemID.ClientID%>';
        var hdnItemID = '<%=hdnItemID.ClientID %>';
        var hdnDeptID = '<%=hdnDeptID.ClientID %>';
        var txtUOMName = '<%=txtUOMName.ClientID%>';
        var btnUOMName = '<%=btnUOMName.ClientID%>';
        var hdnUOMID = '<%=hdnUOMID.ClientID%>';
        var btnSave = '<%=btnSave.ClientID%>';
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


            if ($('#' + txtItemName).is(':visible')) {

                bindItemList();

            }

            if ($('#' + txtUOMName).is(':visible')) {

                bindUomList();

            }


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


        function bindItemList() {

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


                        $('#' + hdnItemID).val('0');
                        return false;
                        //ClearGLAccountData(elemID);
                    }


                    if (ui.item.itemid == '') {
                        event.preventDefault();
                        return false;
                        //ClearGLAccountData(elemID);
                    }
                    else {

                        $('#' + hdnItemID).val(ui.item.itemid);
                        $('#' + txtItemName).val(ui.item.itemname);



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
                    $('#' + hdnItemID).val('0');

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

        function calculatePanel()
        {
            var txtPanelQty = document.getElementById("<%=txtPanelQty.ClientID%>").value;
            if (txtPanelQty == "") {
                alert("Please Enter Panel Quantity!!");
                document.getElementById("<%=txtPanelQty.ClientID%>").focus();
                return false;
            }
            
            var txtGridWeightPanel =parseFloat( document.getElementById("<%=txtGridWeightPanel.ClientID%>").value);
            var txtPasteWeightPanel = parseFloat(document.getElementById("<%=txtPasteWeightPanel.ClientID%>").value);
            var totalPanel = txtGridWeightPanel + txtPasteWeightPanel;
            if (!isNaN(totalPanel)){
                document.getElementById("<%=txtTotalPanel.ClientID%>").value = totalPanel;
            }
            var txtGridWeightPanelKg = (txtGridWeightPanel / 1000);
            var txtPasteWeightPanelKg = (txtPasteWeightPanel / 1000);
            var totalPanelKg = parseFloat(txtGridWeightPanelKg + txtPasteWeightPanelKg);
            if (!isNaN(txtGridWeightPanelKg)) {
                document.getElementById("<%=txtGridWeightPanelKg.ClientID%>").value = txtGridWeightPanelKg;
            }
            if (!isNaN(txtPasteWeightPanelKg)) {
                document.getElementById("<%=txtPasteWeightPanelKg.ClientID%>").value = txtPasteWeightPanelKg;
            }
            if (!isNaN(totalPanelKg)) {
                document.getElementById("<%=txtTotalPanelKg.ClientID%>").value = totalPanelKg;
            }
            var txtGridWeightPcs = (txtGridWeightPanel / txtPanelQty);
            var txtPasteWeightPcs = (txtPasteWeightPanel / txtPanelQty);
            var txtTotalPcs = parseFloat(txtGridWeightPcs + txtPasteWeightPcs);

            if (!isNaN(txtGridWeightPcs)) {
                document.getElementById("<%=txtGridWeightPcs.ClientID%>").value = txtGridWeightPcs;
            }
            if (!isNaN(txtPasteWeightPcs)) {
                document.getElementById("<%=txtPasteWeightPcs.ClientID%>").value = txtPasteWeightPcs;
            }
            if (!isNaN(txtTotalPcs)) {
                document.getElementById("<%=txtTotalPcs.ClientID%>").value = txtTotalPcs;
            }
            var txtGridWeightPcsKg = (txtGridWeightPcs / 1000);
            var txtPasteWeightPcsKg = (txtPasteWeightPcs / 1000);
            var txtTotalPcsKg = parseFloat(txtGridWeightPcsKg + txtPasteWeightPcsKg);

            if (!isNaN(txtGridWeightPcsKg)) {
                document.getElementById("<%=txtGridWeightPcsKg.ClientID%>").value = txtGridWeightPcsKg;
            }
            if (!isNaN(txtPasteWeightPcsKg)) {
                document.getElementById("<%=txtPasteWeightPcsKg.ClientID%>").value = txtPasteWeightPcsKg;
            }
            if (!isNaN(txtTotalPcsKg)) {
                document.getElementById("<%=txtTotalPcsKg.ClientID%>").value = txtTotalPcsKg;

            }
          
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="dvPageContent" style="width:100%;height:100%">
        <div id="dvContentHeader" class="dvContentHeader">
            <div id="dvHeader" class="dvHeader_Prod" align="left">
                <asp:Label ID="lblHeader" runat="server" Text="Plate Weight Entry " CssClass="lblHeader" Font-Bold="true" Font-Size="15px"></asp:Label>

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
                            <asp:Label ID="lblDeptID" runat="server" Text="Department : "></asp:Label>
                        </td>
                        <td>
                        <asp:DropDownList ID="ddlDeptID" runat="server" CssClass="dropDownList required" Width="205px"></asp:DropDownList>
                        </td>
                        <td><asp:HiddenField ID="hdnDeptID" runat="server" Value="" /></td>
                        <td>

                        </td>
                        <td style="text-align:right">
                            <asp:Label ID="lblUOM" runat="server" Text="UOM:"></asp:Label>
                        </td>
                         <td>
                            <asp:TextBox ID="txtUOMName" runat="server" CssClass="textBox textAutoSelect" Width="160px" Height="20px" ></asp:TextBox>
                            <input id="btnUOMName" type="button" value="" runat="server" class="buttonDropdown" style="width: 15px " tabindex="-1" />
                        </td>
                        <td>
                            <asp:HiddenField ID="hdnUOMID" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        
                        <td  style="text-align:right;">
                            <asp:Label ID="lblItemID" runat="server" Text="Item: "></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtItemName" runat="server" CssClass="textBox textAutoSelect" Width="202px" Height="20px"></asp:TextBox>
                            <input id="btnItemID" type="button" value="" runat="server" class="buttonDropdown" style="width: 15px " tabindex="-1" />
                        </td>
                        <td>
                            <asp:HiddenField ID="hdnItemID" runat="server" />
                            
                        </td>
                        <td></td>
                        <td>
                            <asp:Label ID="lblPanelQty" runat="server" Text="Panel Quantity:"></asp:Label>
                        </td>
                        <td>
                           <asp:TextBox ID="txtPanelQty" CssClass="textBox textAutoSelect" placeHolder="0" runat="server" onChange="calculatePanel()" onkeypress=" return isNumberKey(event,this);" ></asp:TextBox>
                        </td>
                        <td>
                            <asp:HiddenField ID="hdnID" runat="server" Value="" />
                        </td>
                    </tr>
                </table>

            </div>
            <div id="dvControls" style="width:850px;height:300px;border:1px solid blue" >
                <div id="dvPanel" style="width:400px; float:left; font-weight:bold; padding-left:20px;">
                    <fieldset style=" height:250px; border:1px solid">
                        <legend style=" font-size:18px;color:red;">Panel</legend>
                        <table>
                            <tr>
                                <td style="text-align:right;">
                                    <asp:Label ID="lblGridWeightPanel" runat="server" Text="Grid Weight:"></asp:Label>
                                </td>
                                 <td>
                                     <asp:TextBox ID="txtGridWeightPanel" CssClass="textBox textAutoSelect"  placeHolder="0" runat="server" onChange="calculatePanel()" onkeypress=" return isNumberKey(event,this);" OnTextChanged="txtGridWeightPanel_TextChanged"></asp:TextBox>
                                 </td>
                                <td style="text-align:left">
                                    <asp:Label ID="lblUnitGridPanel" runat="server" Text="gm"></asp:Label>
                                </td>

                            </tr>
                            <tr>
                                <td style="text-align:right;">
                                    <asp:Label ID="lblPasteWeightPanel" runat="server" Text="Paste Weight:"></asp:Label>
                                </td>
                                 <td>
                                     <asp:TextBox ID="txtPasteWeightPanel" runat="server"   placeHolder="0" CssClass="textBox textAutoSelect" onChange="calculatePanel()" onkeypress=" return isNumberKey(event,this);"></asp:TextBox>
                                 </td>
                                <td style="text-align:left">
                                    <asp:Label ID="lblUnitPastePanel" runat="server" Text="gm"></asp:Label>
                                </td>

                            </tr>
                            <tr>
                                <td style="text-align:right;">
                                    <asp:Label ID="lblTotalPanel" runat="server" Text="Total:"></asp:Label>
                                </td>
                                 <td>
                                     <asp:TextBox ID="txtTotalPanel" runat="server" CssClass="textBox textAutoSelect" ></asp:TextBox>
                                 </td>
                                <td style="text-align:left">
                                    <asp:Label ID="lblUnitTotalPanel" runat="server" Text="gm"></asp:Label>
                                </td>

                            </tr>
                            <tr style="height:30px">
                                 <td></td>
                             </tr>
                             

                             <tr>
                                <td style="text-align:right;">
                                    <asp:Label ID="lblGridWeightPanelKg" runat="server" Text="Grid Weight:"></asp:Label>
                                </td>
                                 <td>
                                     <asp:TextBox ID="txtGridWeightPanelKg" CssClass="textBox textAutoSelect" runat="server" ></asp:TextBox>
                                 </td>
                                <td style="text-align:left">
                                    <asp:Label ID="lblUnitGridKgPanel" runat="server" Text="Kg"></asp:Label>
                                </td>

                            </tr>
                            <tr>
                                <td style="text-align:right;">
                                    <asp:Label ID="lblPasteWeightPanelKg" runat="server" Text="Paste Weight:"></asp:Label>
                                </td>
                                 <td>
                                     <asp:TextBox ID="txtPasteWeightPanelKg" runat="server" CssClass="textBox textAutoSelect" ></asp:TextBox>
                                 </td>
                                <td style="text-align:left">
                                    <asp:Label ID="lblUnitPasteKgPanel" runat="server" Text="Kg"></asp:Label>
                                </td>

                            </tr>
                            <tr>
                                <td style="text-align:right;">
                                    <asp:Label ID="lblTotalPanelKg" runat="server" Text="Total:"></asp:Label>
                                </td>
                                 <td>
                                     <asp:TextBox ID="txtTotalPanelKg" runat="server" CssClass="textBox textAutoSelect" ></asp:TextBox>
                                 </td>
                                <td style="text-align:left">
                                    <asp:Label ID="lblUnitTotalPanelKg" runat="server" Text="Kg"></asp:Label>
                                </td>

                            </tr>
                        </table>

                    </fieldset>

                </div>
                <div id="dvPcs" style="width:400px; float:left; padding-left:10px; font-weight:bold;">
                    <fieldset style=" height:250px; border:1px solid">
                        <legend style=" font-size:18px;color:red;">Pcs</legend>
                         <table>
                            <tr>
                                <td style="text-align:right;">
                                    <asp:Label ID="lblGridWeightPcs" runat="server" Text="Grid Weight:"></asp:Label>
                                </td>
                                 <td>
                                     <asp:TextBox ID="txtGridWeightPcs" runat="server" CssClass="textBox textAutoSelect" ></asp:TextBox>
                                 </td>
                                <td style="text-align:left">
                                    <asp:Label ID="lblUnitGridPcs" runat="server" Text="gm"></asp:Label>
                                </td>

                            </tr>
                            <tr>
                                <td style="text-align:right;">
                                    <asp:Label ID="lblPasteWeightPcs" runat="server" Text="Paste Weight:"></asp:Label>
                                </td>
                                 <td>
                                     <asp:TextBox ID="txtPasteWeightPcs" runat="server" CssClass="textBox textAutoSelect"  ></asp:TextBox>
                                 </td>
                                <td style="text-align:left">
                                    <asp:Label ID="lblUnitPastePcs" runat="server" Text="gm"></asp:Label>
                                </td>

                            </tr>
                            <tr>
                                <td style="text-align:right;">
                                    <asp:Label ID="lblTotalPcs" runat="server" Text="Total:"></asp:Label>
                                </td>
                                 <td>
                                     <asp:TextBox ID="txtTotalPcs" runat="server" CssClass="textBox textAutoSelect" ></asp:TextBox>
                                 </td>
                                <td style="text-align:left">
                                    <asp:Label ID="lblUnitTotalPcs" runat="server" Text="gm"></asp:Label>
                                </td>

                            </tr>
                             <tr style="height:30px">
                                 <td></td>
                             </tr>
                             

                             <tr>
                                <td style="text-align:right;">
                                    <asp:Label ID="lblGridWeightPcsKg" runat="server" Text="Grid Weight:"></asp:Label>
                                </td>
                                 <td>
                                     <asp:TextBox ID="txtGridWeightPcsKg" runat="server" CssClass="textBox textAutoSelect"  ></asp:TextBox>
                                 </td>
                                <td style="text-align:left">
                                    <asp:Label ID="lblUnitGridKgPcs" runat="server" Text="Kg"></asp:Label>
                                </td>

                            </tr>
                            <tr>
                                <td style="text-align:right;">
                                    <asp:Label ID="lblPasteWeightPcsKg" runat="server" Text="Paste Weight:"></asp:Label>
                                </td>
                                 <td>
                                     <asp:TextBox ID="txtPasteWeightPcsKg" runat="server" CssClass="textBox textAutoSelect" ></asp:TextBox>
                                 </td>
                                <td style="text-align:left">
                                    <asp:Label ID="lblUnitTotalKgPcs" runat="server" Text="Kg"></asp:Label>
                                </td>

                            </tr>
                            <tr>
                                <td style="text-align:right;">
                                    <asp:Label ID="lblTotalPcsKg" runat="server" Text="Total:"></asp:Label>
                                </td>
                                 <td>
                                     <asp:TextBox ID="txtTotalPcsKg" runat="server" CssClass="textBox textAutoSelect"  ></asp:TextBox>
                                 </td>
                                <td style="text-align:left">
                                    <asp:Label ID="lblUnitTotalPcsKg" runat="server" Text="Kg"></asp:Label>
                                </td>

                            </tr>
                        </table>

                    </fieldset>

                </div>

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
