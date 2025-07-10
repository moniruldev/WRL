<%@ Page Title="" Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="InventoryTypeClassGroupItemView.aspx.cs" Inherits="PG.Web.Report.INV.InventoryTypeClassGroupItemView" EnableViewState="false"%>
<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../css/jquery.ui.combogrid.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        /*.FixedHeader { POSITION: relative; TOP: expression(this.offsetParent.scrollTop); BACKGROUND-COLOR: white } */

        .FixedHeader {
            position: relative;
            background-color: white;
        }

        #dvMessage {
            height: 20px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hdnDeptId" runat="server" />
    <div id="dvPageContent" style="width: 100%; height: auto;">
        <div id="dvContentHeader" class="dvContentHeader">
            <div id="dvHeader" class="dvHeader">
                <asp:Label ID="lblHeader" CssClass="lblHeader" runat="server" Text="Type Class Group Item View"></asp:Label>
            </div>
            <!--Message Div -->
            <div id="dvMsg" runat="server" class="dvMessage" style="">
                <asp:Label ID="lblMessage" runat="server" Font-Size="Small" Width="100%"> </asp:Label>
            </div>
            <div id="dvHeaderControl" class="dvHeaderControl">
            </div>
        </div>
        <div id="dvContentMain" class="dvContentMain">
            <div id="dvControlsHead" style="height: auto; width: 100%;">
               <table style="padding-left:10px">
                   <tr>
                       <td>
                           <asp:RadioButton ID="rdoItemType" runat="server" GroupName="ItemType" Text="Type" Checked="true" />
                       </td>
                       <td>
                            <table border="0">
                               <tr>
                                   <td><asp:TextBox ID="txtItemType" runat="server" CssClass="textBox"></asp:TextBox></td>
                                     <td>
                                        <input id="btnItemType" type="button" value="" runat="server" class="buttonDropdown"
                                            tabindex="-1" />
                                        <asp:HiddenField ID="hdnItemType" runat="server" Value="0" />
                                    </td>
                               </tr>
                           </table>
                       </td>
                   </tr>
                   <tr>
                       <td> <asp:RadioButton ID="rdoItemClass" runat="server" Text="Class" GroupName="ItemType"  /></td>
                       <td>
                            <table border="0">
                               <tr>
                                   <td><asp:TextBox ID="txtItemClass" runat="server" CssClass="textBox"></asp:TextBox></td>
                                     <td>
                                        <input id="btnItemClass" type="button" value="" runat="server" class="buttonDropdown"
                                            tabindex="-1" />
                                        <asp:HiddenField ID="hdnItemClass" runat="server" Value="0" />
                                    </td>
                               </tr>
                           </table>
                       </td>
                   </tr>
                    <tr>
                       <td> <asp:RadioButton ID="rdoItemGroup" runat="server" Text="Group" GroupName="ItemType" /></td>
                       <td>
                            <table border="0">
                               <tr>
                                   <td><asp:TextBox ID="txtItemGroup" runat="server" CssClass="textBox"></asp:TextBox></td>
                                     <td>
                                        <input id="btnItemGroup" type="button" value="" runat="server" class="buttonDropdown"
                                            tabindex="-1" />
                                        <asp:HiddenField ID="hdnItemGroup" runat="server" Value="0" />
                                    </td>
                               </tr>
                           </table>
                       </td>
                   </tr>
                    <tr>
                       <td> <asp:RadioButton ID="rdoItemName" runat="server" Text="Item" GroupName="ItemType" /></td>
                     <td>
                            <table border="0">
                               <tr>
                                   <td><asp:TextBox ID="txtItemName" runat="server" CssClass="textBox"></asp:TextBox></td>
                                     <td>
                                        <input id="btnItemName" type="button" value="" runat="server" class="buttonDropdown"
                                            tabindex="-1" />
                                        <asp:HiddenField ID="hdnItemName" runat="server" Value="0" />
                                    </td>
                               </tr>
                           </table>
                       </td>
                   </tr>
                   <tr>
                       <td>&nbsp;</td>
                       <td>
                       <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="buttonSearch" OnClick="btnSearch_Click"></asp:Button></td>
                   </tr>
               </table>
            </div>

            <div id="dvControls" style="width: 100%;">
                <div id="dvControlsInner" class="groupBoxContainer boxShadow" style="width:90%">
                    <div id="dvGridContainer" style="width: 100%; height: auto; text-align: left;">

                        <div id="dvGrid" style="width:100%; height: 250px; overflow: auto;">
                            <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="ASPxGridView1" runat="server" Width="100%" AutoGenerateColumns="True" KeyFieldName="Code"  >                                
                                <Settings VerticalScrollableHeight="250" ShowFooter="True" ShowGroupFooter="VisibleAlways" />
                                <TotalSummary>
                                    <dx:ASPxSummaryItem FieldName="Size" SummaryType="Sum" />
                                    <%--<dx:ASPxSummaryItem FieldName="INDT_QTY" ShowInColumn="INDT_QTY" DisplayFormat="Total:{0}" ShowInGroupFooterColumn="PURCHASE_QTY" SummaryType="Sum" />--%>
                                </TotalSummary>
                                <GroupSummary>
                                    <dx:ASPxSummaryItem SummaryType="Count" />
                                    <%--<dx:ASPxSummaryItem FieldName="INDT_QTY" ShowInGroupFooterColumn="INDT_QTY" SummaryType="Sum" />--%>
                                </GroupSummary>
                                
                                <SettingsBehavior AllowFixedGroups="True" AutoExpandAllGroups="True" SortMode="Value" />
                                <SettingsPager NumericButtonCount="20">
                                    <PageSizeItemSettings Visible="true" Items="50,100" />
                                </SettingsPager>
                                <Settings ShowGroupPanel="true" ShowFilterBar="Visible"  ShowGroupedColumns="True" ShowHeaderFilterButton="True" ShowGroupButtons="True" />

                                <SettingsDataSecurity AllowInsert="false" AllowEdit="false" AllowDelete="false" />
                                <Styles>
                                    <Header BackColor="#0033CC" ForeColor="White">
                                    </Header>
                                    <AlternatingRow BackColor="#FFFFCC">
                                    </AlternatingRow>
                                    <GroupFooter BackColor="#CCCCFF">
                                    </GroupFooter>
                                    <GroupPanel BackColor="#9999FF">
                                    </GroupPanel>
                                    <Cell Wrap="False"></Cell>
                                </Styles>
                            </dx:ASPxGridView>

                            <dx:ASPxButton ID="btnXlsExport" runat="server" Text="Export to XLS" UseSubmitBehavior="False"
                                OnClick="btnXlsExport_Click" />

                            <dx:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="ASPxGridView1"></dx:ASPxGridViewExporter>
                        </div>

                    </div>
                </div>
            </div>            
        </div>
        <div id="dvContentFooter" class="dvContentFooter">
        </div>
    </div>

    <script language="javascript" type="text/javascript">
        // <!CDATA[


        ContentForm.CalendarImageURL = "../image/calendar.png";

        var ItemTypeListServiceLink = '<%=this.ItemTypeListServiceLink%>';
        var ItemClassListServiceLink = '<%=this.ItemClassListServiceLink%>';
        var ItemGroupListServiceLink = '<%=this.ItemGroupListServiceLink%>';
        var ItemListServiceLink = '<%=this.ItemListServiceLink%>';


        var txtType = '<%=txtItemType.ClientID%>';
        var btnType = '<%=btnItemType.ClientID%>';
        var hdnType = '<%=hdnItemType.ClientID%>';


        var txtClass = '<%=txtItemClass.ClientID%>';
        var btnClass = '<%=btnItemClass.ClientID%>';
        var hdnClass = '<%=hdnItemClass.ClientID%>';

        var txtGroup = '<%=txtItemGroup.ClientID%>';
        var btnGroup = '<%=btnItemGroup.ClientID%>';
        var hdnGroup = '<%=hdnItemGroup.ClientID%>';

        var txtItem = '<%=txtItemName.ClientID%>';
        var btnItem = '<%=btnItemName.ClientID%>';
        var hdnItem = '<%=hdnItemName.ClientID%>';




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


        $(document).ready(function () {

            if ($('#' + txtType).is(':visible')) {
                bindTypeList();
            }

            if ($('#' + txtClass).is(':visible')) {
                bindClassList();
            }

            if ($('#' + txtGroup).is(':visible')) {
                bindGroupList();
            }

            if ($('#' + txtItem).is(':visible')) {
                bindItemList();
            }


            $('input:radio[id="rdoItemType"]').change(
    function () {
        if (this.checked && this.value == 'Yes') {
            alert($(this).val());
        }
    });

        });


        function bindTypeList() {
            var cgColumns = [
                             //{ 'columnName': 'itemgroupid', 'width': '80', 'align': 'left', 'highlight': 2, 'label': 'Group ID' }
                             { 'columnName': 'itemtypecode', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Code' }
                            , { 'columnName': 'itemtypename', 'width': '160', 'align': 'left', 'highlight': 4, 'label': 'Name' }


            ];
            var serviceURL = ItemTypeListServiceLink + "?isterm=1&includeempty=0&hasitem=1&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;

            serviceURL += "&ispaging=1";
            var typeIDElem = $('#' + txtType);

            $('#' + btnType).click(function (e) {
                $(typeIDElem).combogrid("dropdownClick");
            });

            $(typeIDElem).combogrid({
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
                width: 390,
                url: serviceURL,
                search: function (event, ui) {
                    //var companyCode = $('#' + ddlCompany).val();
                    //var branchCode = $('#' + hdnBranch).val();
                    //var deptCode = $('#' + hdnDepartment).val();
                    //var locationid = $('#' + lblLocationID).val();
                    // var seid = $('#' + txtExecutiveID).val();
                    //var vgroupid = $('#' + hdnGroupID).val();
                    var newServiceURL = serviceURL;
                    $(this).combogrid("option", "url", newServiceURL);


                },
                select: function (event, ui) {
                    if (!ui.item) {
                        event.preventDefault();

                        //$('#' + hdnGroupID).val('0');
                        $('#' + hdnType).val('0');
                        return false;
                        //ClearGLAccountData(elemID);
                    }


                    if (ui.item.itemtypeid == '') {
                        event.preventDefault();
                        return false;
                        //ClearGLAccountData(elemID);
                    }
                    else {
                        $('#' + hdnType).val(ui.item.itemtypeid);
                        $('#' + txtType).val(ui.item.itemtypename);
                    }
                    return false;
                },

                lc: ''
            });


            $(typeIDElem).blur(function () {
                var self = this;
                var typeID = $(typeIDElem).val();
                if (typeID == '') {
                    // $('#' + hdnDealerID).val('0');
                    $('#' + txtType).val('');
                    $('#' + hdnType).val('0');
                    //$('#' + txtGroupCode).val('');
                }
            });
        }


        function bindClassList() {
            var cgColumns = [
                             //{ 'columnName': 'itemgroupid', 'width': '80', 'align': 'left', 'highlight': 2, 'label': 'Group ID' }
                             { 'columnName': 'itemclasscode', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Code' }
                            , { 'columnName': 'itemclassname', 'width': '160', 'align': 'left', 'highlight': 4, 'label': 'Name' }


            ];
            var serviceURL = ItemClassListServiceLink + "?isterm=1&includeempty=0&hasitem=1&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;

            serviceURL += "&ispaging=1";
            var classIDElem = $('#' + txtClass);

            $('#' + btnClass).click(function (e) {
                $(classIDElem).combogrid("dropdownClick");
            });

            $(classIDElem).combogrid({
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
                width: 310,
                url: serviceURL,
                search: function (event, ui) {
                    //var companyCode = $('#' + ddlCompany).val();
                    //var branchCode = $('#' + hdnBranch).val();
                    //var deptCode = $('#' + hdnDepartment).val();
                    //var locationid = $('#' + lblLocationID).val();
                    // var seid = $('#' + txtExecutiveID).val();
                    //var vgroupid = $('#' + hdnGroupID).val();
                    var newServiceURL = serviceURL;
                    $(this).combogrid("option", "url", newServiceURL);


                },
                select: function (event, ui) {
                    if (!ui.item) {
                        event.preventDefault();

                        //$('#' + hdnGroupID).val('0');
                        $('#' + hdnClass).val('0');
                        return false;
                        //ClearGLAccountData(elemID);
                    }


                    if (ui.item.itemclassid == '') {
                        event.preventDefault();
                        return false;
                        //ClearGLAccountData(elemID);
                    }
                    else {
                        $('#' + hdnClass).val(ui.item.itemclassid);
                        $('#' + txtClass).val(ui.item.itemclassname);
                    }
                    return false;
                },

                lc: ''
            });


            $(classIDElem).blur(function () {
                var self = this;
                var classID = $(classIDElem).val();
                if (classID == '') {
                    // $('#' + hdnDealerID).val('0');
                    $('#' + txtClass).val('');
                    $('#' + hdnClass).val('0');
                    //$('#' + txtGroupCode).val('');
                }
            });
        }

        function bindGroupList() {
            var cgColumns = [
                             //{ 'columnName': 'itemgroupid', 'width': '80', 'align': 'left', 'highlight': 2, 'label': 'Group ID' }
                             { 'columnName': 'itemgroupcode', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Code' }
                            , { 'columnName': 'itemgroupdesc', 'width': '160', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                            , { 'columnName': 'itemgroupnameparent', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Group Parent' }


            ];
            var serviceURL = ItemGroupListServiceLink + "?isterm=1&includeempty=0&hasitem=1&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;

            serviceURL += "&ispaging=1";
            var groupIDElem = $('#' + txtGroup);

            $('#' + btnGroup).click(function (e) {
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
                width: 390,
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

                        $('#' + hdnGroup).val('0');
                        //$('#' + txtDealerID).val('');
                        return false;
                        //ClearGLAccountData(elemID);
                    }


                    if (ui.item.itemgroupid == '') {
                        event.preventDefault();
                        return false;
                        //ClearGLAccountData(elemID);
                    }
                    else {
                        // $('#' + hdnDealerID).val(ui.item.dealerid);
                        $('#' + hdnGroup).val(ui.item.itemgroupid);
                        $('#' + txtGroup).val(ui.item.itemgroupdesc);
                        //$('#' + txtGroupCode).val(ui.item.itemgroupcode);

                    }
                    return false;
                },

                lc: ''
            });

            $(groupIDElem).blur(function () {
                var self = this;
                elemID = $(groupIDElem).attr('id');
                eCode = $(groupIDElem).val();
                isComboGridOpen = $(self).combogrid('isOpened');
                if (eCode == '') {
                    //                    if (!validateGLAccount(elemID, null)) {
                    //                        $(elem).val(prevGLCode);
                    //                        return false;
                    //                    }
                    $('#' + hdnGroup).val('0');
                }
                else {
                    //grp = GetGLGroup(eCode);
                    ////                    if (!validateGLAccount(elemID, grp)) {
                    ////                        $(elem).val(prevGLCode);
                    ////                        return false;
                    ////                    }

                    if (grp == null) {
                        $('#' + hdnGroup).val('0');
                    }
                    else {
                        //SetItemGroupData(elemID, grp);
                    }
                }
                //setDetInstrument(hdnIsInstrumentElem, txtInstrumentElem, btnInstrumentElem);
                grpID = $(self).closest('tr').find('input[id$="hdnGroupID"]').val();
                if (grpID == '0' | grpID == '') {
                    $(self).addClass('textError');
                }

            });


            $(groupIDElem).blur(function () {
                var self = this;

                var groupID = $(groupIDElem).val();
                if (groupID == '') {
                    // $('#' + hdnDealerID).val('0');
                    $('#' + txtGroup).val('');
                    //$('#' + txtGroupCode).val('');
                }
            });
        }

        function bindItemList() {

            var cgColumns = [
                { 'columnName': 'itemcode', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Code' }
                             , { 'columnName': 'itemname', 'width': '200', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                             , { 'columnName': 'itemgroupdesc', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Group' }
                             , { 'columnName': 'uomname', 'width': '40', 'align': 'left', 'highlight': 4, 'label': 'UOM' }
                              //, { 'columnName': 'class_name', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Class Name' }
                             , { 'columnName': 'item_type', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Item Type' }
            ];

            var itemServiceURL = ItemListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;

            itemServiceURL += "&ispaging=1";
            var itemIDElem = $('#' + txtItem);

            $('#' + btnItem).click(function (e) {
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
                width: 670,
                url: itemServiceURL,
                search: function (event, ui) {
                    //var companyCode = $('#' + ddlCompany).val();
                    //var branchCode = $('#' + hdnBranch).val();
                    //var deptCode = $('#' + hdnDepartment).val();
                    //var locationid = $('#' + lblLocationID).val();
                    // var seid = $('#' + txtExecutiveID).val();
                    //var vgroupid = $('#' + hdnGroupID).val();
                    //var newServiceURL = itemServiceURL + "&groupid=" + vgroupid;
                    //$(this).combogrid("option", "url", newServiceURL);


                },
                select: function (event, ui) {
                    if (!ui.item) {
                        event.preventDefault();

                        //$('#' + hdnGroupID).val('0');
                        $('#' + hdnItem).val('0');
                        return false;
                        //ClearGLAccountData(elemID);
                    }


                    if (ui.item.itemid == '') {
                        event.preventDefault();
                        return false;
                        //ClearGLAccountData(elemID);
                    }
                    else {
                        // $('#' + hdnDealerID).val(ui.item.dealerid);
                        $('#' + hdnItem).val(ui.item.itemid);
                        $('#' + txtItem).val(ui.item.itemname);

                        //$('#' + hdnUomID).val(ui.item.itemid);
                        //$('#' + hdnUomName).val(ui.item.uomname);

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
                    $('#' + txtItem).val('');
                    $('#' + hdnItem).val('0');
                    //$('#' + txtGroupCode).val('');
                }
            });
        }

        

        function fromParent(val1) {
            alert('this is called from parent: ' + val1);
        }




        // ]]>
    </script>
     <script src="../../javascript/jquery.ui.combogrid.js" type="text/javascript"></script>
    <script src="../../javascript/jquery.attributeobserver.js" type="text/javascript"></script>
</asp:Content>
