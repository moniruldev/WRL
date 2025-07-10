<%@ Page Title="" Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="INVNewDeptItemBinding.aspx.cs" Inherits="PG.Web.Inventory.INVNewDeptItemBinding" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="../javascript/jquery.ui.combogrid.js" type="text/javascript"></script>
    <script src="../javascript/jquery.attributeobserver.js" type="text/javascript"></script>
    <link href="../css/jquery.ui.combogrid.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">
        // <!CDATA[

        var ItemGroupListServiceLink = '<%=this.ItemGroupListServiceLink%>';
        var ItemListServiceLink = '<%=this.ItemListServiceLink%>';


        var ddlClassId = '<%= ddlItemClass.ClientID%>';
        var ddlTypeId = '<%= ddlItemType.ClientID%>';


        var hdnGroupID = '<%= hdnGroupID.ClientID%>';
        var txtGroupName = '<%= txtGroupName.ClientID%>';
        var btnGroupID = '<%= btnGroupID.ClientID%>';

        var hdnItemID = '<%=hdnItemID.ClientID %>';
        var txtItemName = '<%=txtItemName.ClientID %>';
        var btnItemID = '<%= btnItemID.ClientID%>';



        var isPageResize = true;
        var gridViewID = '<%=GridView1.ClientID%>';
        var gridRowCountID = '<%= hdnRowCount.ClientID %>';

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


        $(document).ready(function () {

            if ($('#' + txtGroupName).is(':visible')) {
                bindGroupList();
            }
            bindItemList();
        })




        //function tbopen(key, userid) {
        //    if (!key) {
        //        key = '';
        //    }

        //    var url = "Admin/Role.aspx?id=" + key
        //    //if (pageInTab == 1)
        //    if (ZForm.PageMode == Enums.PageMode.InTab) {

        //        var tdata = new xtabdata();
        //        tdata.linktype = Enums.LinkType.Direct;
        //        tdata.id = 4110;
        //        tdata.name = "Role";
        //        //tdata.label = "User: " + userid;
        //        tdata.label = "Role";
        //        tdata.type = 0;
        //        tdata.url = url;
        //        tdata.tabaction = Enums.TabAction.InNewTab;
        //        tdata.selecttab = 1;
        //        tdata.reload = 0;
        //        tdata.param = "";


        //        try {
        //            window.parent.TabMenu.OpenMenuByData(tdata);
        //        }
        //        catch (err) {
        //            alert("error in page");
        //        }
        //    }
        //    else {
        //        //on new window/tab
        //        //window.open(url,'_blank');   

        //        window.location = url;
        //    }
        //}

        function fromParent(val1) {
            alert('this is called from parent: ' + val1);
        }

        function chkCopy_Click() {

          <%--  var chkCopyID = '<%=chkCopy.ClientID%>';
            var ddlCopyID = '<%=ddlRoleCopy.ClientID%>';--%>

            if ($('#' + chkCopyID).is(':checked')) {
                // alert('a');
                //$("#ddlRoleCopy").show();
                //$('#ddlRoleCopy').attr('visibility', '');
                $("#" + ddlCopyID).css("visibility", "visible");
            }
            else {
                //  alert('h');
                //$("#ddlRoleCopy").hide();
                //$('#ddlRoleCopy').attr('visibility', 'hidden');
                $("#" + ddlCopyID).css("visibility", "hidden");
            }






            return true;
        }
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
                width: 900,
                url: itemServiceURL,
                search: function (event, ui) {



                    var classId = $('#' + ddlClassId).val();
                    var typeId = $('#' + ddlTypeId).val();

                    var vgroupid = $('#' + hdnGroupID).val();
                    var newServiceURL = itemServiceURL + "&groupid=" + vgroupid + "&classid=" + classId + "&typeid=" + typeId;
                    newServiceURL = JSUtility.AddTimeToQueryString(newServiceURL);
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
        // ]]>
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:HiddenField ID="hdnGroupID" runat="server" />
    <asp:HiddenField ID="hdnItemID" runat="server" />

    <%--<div  id="dvPageContent" style="width:100%; height:100%;" >--%>
    <div id="dvContentHeader" class="dvContentHeader">
        <div id="dvHeader" class="dvHeader">
            <asp:Label ID="lblHeader" runat="server" Text="Department Wise Item Binding" CssClass="lblHeader"></asp:Label>
        </div>
        <div id="dvMsg" runat="server" class="dvMessage" style="">
            <asp:Label ID="lblMessage" runat="server" Font-Size="Small" Width="100%" Height="16px"></asp:Label>
        </div>
        <div id="dvHeaderControl" class="dvHeaderControl">
        </div>

    </div>

    <div id="dvContentMain" class="dvContentMain">

        <div id="dvControlsHead" style="height: auto; width: 100%;">
            <table>

                <tr>
                    <td></td>
                    <td>
                        <asp:Label ID="lblDepartment" runat="server" Text="Department:"></asp:Label><span style="color: red">*</span>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlDepartment" runat="server" Width="175" CssClass="dropDownList enableIsDirty"></asp:DropDownList>

                    </td>
                </tr>

                <tr>

                    <td></td>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text="Item Type"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlItemType" runat="server" Width="175" CssClass="dropDownList enableIsDirty">
                            <asp:ListItem Value="">(--all--)</asp:ListItem>
                            <asp:ListItem Value="1">Local</asp:ListItem>
                            <asp:ListItem Value="2">Import</asp:ListItem>
                        </asp:DropDownList>

                    </td>



                </tr>
                <tr>

                    <td></td>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="Item Class"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlItemClass" runat="server" Width="175" CssClass="dropDownList enableIsDirty"></asp:DropDownList>

                    </td>



                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Label ID="lblItemGroup" runat="server" Text="Item Group"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtGroupName" runat="server" CssClass="textBox required" Enabled="true"></asp:TextBox><input id="btnGroupID" type="button" value="" runat="server" class="buttonDropdown" tabindex="-1" />

                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="Item "></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtItemName" runat="server" CssClass="textBox required" Enabled="true"></asp:TextBox>
                        <input id="btnItemID" type="button" value="" runat="server" class="buttonDropdown" tabindex="-1" /></td>

                </tr>
                <tr>
                    <td></td>
                    <td></td>
                    <td>
                        <asp:Button ID="btnLoad" runat="server" CssClass="buttonRefresh" Style="padding-left: 22px;" Text="Load" OnClick="btnLoad_Click" />
                        <asp:Button ID="btnReset" runat="server" CssClass="buttonRefresh" Style="padding-left: 22px;" Text="Reset" OnClick="btnReset_Click" />

                    </td>

                </tr>
            </table>

        </div>

        <div id="dvControls" style="height: auto; width: 100%;">
            <div id="dvControlsInner" class="groupBoxContainer boxShadow" style="width: 1000px !important">
                <div id="dvGridContainer" class="gridContainer" style="width: 100%; height: auto;">
                    <div id="dvGridHeader" style="width: 100%; height: 25px; font-size: smaller;" class="subHeader">
                        <table style="height: 90%; color: #FFFFFF; font-weight: bold; text-align: center;" class="defFont" cellspacing="1" cellpadding="1" rules="all">
                            <tr>

                                <td align="left" width="37px">SL</td>
                                <td width="204px" align="left">Item Name</td>
                                <td width="62px" align="left">Code</td>
                                <td width="204px" align="left">Description</td>
                                <td width="64px" align="left">Unit</td>
                                <td width="100px" align="left">Group</td>
                                <td width="100px" align="left">Class</td>
                                <td width="100px" align="left">Type</td>
                                <td align="left" width="64px">
                                    <asp:CheckBox ID="cbCheckAll" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </div>


                    <div id="dvGrid" style="width: 95%; height: 285px; overflow: auto;">
                        <asp:GridView Style="z-index: 100;"
                            ID="GridView1" runat="server" DataKeyNames="ITEM_ID"
                            CellPadding="1" CellSpacing="1"
                            AutoGenerateColumns="False"
                            Font-Names="Arial" Font-Size="10pt" ForeColor="#333333" GridLines="None"
                            ShowHeader="False" EmptyDataText="No Data Found">
                            <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <Columns>
                                <asp:TemplateField HeaderText="SL" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Width="40px" HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                <asp:BoundField DataField="ITEM_NAME" HeaderText="Item Name">
                                    <ItemStyle BorderColor="Gray" Width="200px" />
                                </asp:BoundField>

                                <asp:BoundField DataField="ITEM_CODE" HeaderText="Code">
                                    <ItemStyle BorderColor="Gray" Width="62px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ITEM_DESC" HeaderText="Description">
                                    <ItemStyle BorderColor="Gray" Width="200px" />
                                </asp:BoundField>

                                <asp:BoundField DataField="UOM_CODE" HeaderText="Unit">
                                    <ItemStyle BorderColor="Gray" Width="64px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ITEM_GROUP_NAME" HeaderText="Group">
                                    <ItemStyle BorderColor="Gray" Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ITEM_CLASS_NAME" HeaderText="Class">
                                    <ItemStyle BorderColor="Gray" Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ITEM_TYPE_NAME" HeaderText="Type">
                                    <ItemStyle BorderColor="Gray" Width="100px" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Bind with Department">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkAllow" runat="server" Checked='<%#Eval("IsAssigned")%>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="left" />
                                    <ItemStyle BorderColor="Gray" HorizontalAlign="left" Width="60px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="" HeaderStyle-Width="60px" ItemStyle-Width="60px" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="hdnIsAssigned" runat="server" Text='<%#Eval("IsAssigned")%>'></asp:Label>

                                        <asp:Label ID="lblITEM_ID" runat="server" Text='<%#Eval("ITEM_ID")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#999999" />
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        </asp:GridView>
                    </div>
                </div>

                <%-- </div>--%>
            </div>

        </div>

        <div id="dvGridFooter" style="width: 100%; height: 25px; font-size: smaller;" class="subFooter">
            <table style="height: 100%; font-weight: bold;"
                cellspacing="1" cellpadding="1" rules="all">
                <tr>
                    <td width="5px" align="left"></td>
                    <td align="left">
                        <asp:Label ID="lblTotal" runat="server" Text="Total: 0"></asp:Label>
                        <asp:HiddenField ID="hdnRowCount" runat="server" Value="0" />

                    </td>
                    <td width="50px"></td>

                </tr>
            </table>
        </div>


        <div id="dvContentFooter" class="dvContentFooter" align="center">
            <table>
                <tr>
                    <td></td>
                    <td></td>
                    <td></td>
               
                   
                    <td>
                        <asp:Button ID="btnSave" runat="server" CssClass="buttonSave" Style="" Text="Save" OnClick="btnSave_Click" />

                    </td>

                    <td>
                        <input type="button" id="btnClose" class="buttonClose" value="Close" onclick="if (ContentForm) { ContentForm.CloseForm(); }" />

                    </td>
                     <td></td>
                    <td></td>
                         <td></td>
                </tr>
            </table>
        </div>




    </div>

</asp:Content>
