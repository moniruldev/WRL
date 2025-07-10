<%@ Page Title="" Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="Department_Item_Mapping_V1.aspx.cs" Inherits="PG.Web.Production.Department_Item_Mapping_V1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script src="../javascript/jquery.ui.combogrid.js" type="text/javascript"></script>
    <script src="../javascript/jquery.attributeobserver.js" type="text/javascript"></script>
    <link href="../css/jquery.ui.combogrid.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" >

        var isPageResize = true;
        ContentForm.CalendarImageURL = "../image/calendar.png";
        var ItemListServiceLink = '<%=this.ItemListServiceLink%>';
        var ddlDeptID = '<%=ddlDeptID.ClientID%>';
        var gridViewIDDet = '<%=grdDeptItemMapping.ClientID%>';
        var gridUpdatePanelIDDet = '<%=UpdatePanel1.ClientID%>';

        var txthItemName = '<%=txthItemName.ClientID%>';
        var btnhItemID = '<%= btnhItemID.ClientID%>';
        var hdnhItemID = '<%=hdnhItemID.ClientID %>';

        var updateProgressID = '<%=UpdateProgress2.ClientID%>';
        var hdnDeptID = '<%=hdnDeptID.ClientID %>';
        var btnSave = '<%=btnSave.ClientID%>';

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
        function UserSaveConfirmation() {
            return confirm("Are you sure you want to Save?");
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

                    }

                }

                bindItemList(gridViewIDDet);
            });

            if ($('#' + txthItemName).is(':visible')) {

                bindHeaderItemList();

            }


        });

        function bindHeaderItemList() {

            var cgColumns = [
                              { 'columnName': 'itemname', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                             , { 'columnName': 'itemcode', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Code' }
                             , { 'columnName': 'itemgroupdesc', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Group Name' }
                             , { 'columnName': 'uomname', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Uom Name' }
                              , { 'columnName': 'class_name', 'width': '120', 'align': 'left', 'highlight': 4, 'label': 'Class Name' }
                             //, { 'columnName': 'item_type', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Item Type' }
            ];

            var itemServiceURL = ItemListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;

            itemServiceURL += "&ispaging=1&isigr=1";
            var itemIDElem = $('#' + txthItemName);

            $('#' + btnhItemID).click(function (e) {
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
                width: 650,
                url: itemServiceURL,
                search: function (event, ui) {

                    //var vdeptid = $('#' + ddlDeptID).val();
                    var newServiceURL = itemServiceURL; // + "&deptid=" + vdeptid
                    $(this).combogrid("option", "url", newServiceURL);


                },
                select: function (event, ui) {
                    if (!ui.item) {
                        event.preventDefault();


                        $('#' + hdnhItemID).val('0');
                        return false;
                        //ClearGLAccountData(elemID);
                    }


                    if (ui.item.itemid == '') {
                        event.preventDefault();
                        return false;
                        //ClearGLAccountData(elemID);
                    }
                    else {

                        $('#' + hdnhItemID).val(ui.item.itemid);
                        $('#' + txthItemName).val(ui.item.itemname);



                    }
                    return false;
                },

                lc: ''
            });

            $(itemIDElem).blur(function () {
                var self = this;
                var groupID = $(itemIDElem).val();
                if (groupID == '') {
                    $('#' + txthItemName).val('');
                    $('#' + hdnhItemID).val('0');

                }
            });
        }



        function bindItemList(gridViewID) {
            var cgColumns = [
                               { 'columnName': 'itemname', 'width': '200', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                             , { 'columnName': 'uomname', 'width': '70', 'align': 'left', 'highlight': 4, 'label': 'Uom Name' }
                           //  , { 'columnName': 'itemcode', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Code' }
                             , { 'columnName': 'itemgroupdesc', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Group Name' }
                              , { 'columnName': 'item_type', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Item Type' }
                             //, { 'columnName': 'closing_qty', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Cls Qty' }
                             //, { 'columnName': 'class_name', 'width': '120', 'align': 'left', 'highlight': 4, 'label': 'Class Name' }
                           //  , { 'columnName': 'bomname', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'BOM Name' }
                           //  , { 'columnName': 'bomno', 'width': '180', 'align': 'left', 'highlight': 4, 'label': 'BOM NO' }
                             , { 'columnName': 'uomid', 'width': '50', 'align': 'left', 'highlight': 4, 'label': 'Uom ID' }
                            //  , { 'columnName': 'bomid', 'width': '50', 'align': 'left', 'highlight': 4, 'label': 'BOM ID' }


            ];


            //var companyid = $('#' + hdnCompanyID).val();
            //var depthead = $('#' + hdnEmpCode).val();
            //var ddlDEPT_ID = $('#' + ddlDeptID).val();

            //var serviceURL = GLAccountServiceLink + "?isterm=1&includeempty=1&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            var serviceURL = ItemListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            //serviceURL += "&companyid=" + companyid;
            serviceURL += "&ispaging=1&isigr=1";
            //serviceURL += "&locationid=" + locationid;
            // serviceURL += "&empstatus=" + "A";
            //serviceURL += "&acctypefilter=" + Enums.GLAccountTypeFilter.AllAccount;
            // serviceURL += "&namecomptype=" + Enums.DataCompareType.Contains;
            var gridSelector = "#" + gridViewID;
            $(gridSelector).find('input[id$="txtItemName"]').each(function (index, elem) {
                ///list click
                var elemRow = $(elem).closest('tr.gridRow');
                var hdnItemIDElem = $(elemRow).find('input[id$="txtItemName"]');
                //var prevGLCode = '';
                $(elem).closest('tr').find('input[id$="btnItemName"]').click(function (e) {
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
                        //var vgroupid = $(elemRowCur).find('input[id$="hdngroupId"]').val();
                        //var vgroupid = $('#' + hdngroupId).val();
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
                        var serviceURL = ItemListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
                        serviceURL += "&ispaging=1&isigr=1";

                        var prcNo = GetItemNo(eCode, serviceURL);

                        if (prcNo == null) {
                            ClearItemData(elemID);
                        }
                        else {
                            SetItemData(elemID, grp);
                        }

                    }
                });

            });

        }


        function SetItemData(txtItemCodeID, data) {
            $('#' + txtItemCodeID).val(data.itemid);

            var detRow = $('#' + txtItemCodeID).closest('tr.gridRow');
            $(detRow).find('input[id$="hdnItemID"]').val(data.itemid);
            $(detRow).find('input[id$="txtItemName"]').val(data.itemname);
            //$(detRow).find('input[id$="hdnUomID"]').val(data.uomid);
            //$(detRow).find('input[id$="txtUOM_NAME"]').val(data.uomname);
        }

        function ClearItemData(txtItemID) {
            //$('#' + txtGLAccCodeID).val('');
            var detRow = $('#' + txtItemID).closest('tr.gridRow');
            $(detRow).find('input[id$="hdnItemId"]').val('0');
            $(detRow).find('input[id$="txtItemName"]').val('');
            //$(detRow).find('input[id$="hdnUomID"]').val('0');
            //$(detRow).find('input[id$="txtUOM_NAME"]').val('');

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
     <div id="dvPageContent" style="width:100%;height:100%;" onkeydown="if(event.keyCode==13){event.keyCode=9; return event.keyCode;}">
        <div id="dvContentHeader" class="dvContentHeader">
            <div id="dvHeader" class="dvHeader_Prod" align="left">
                <asp:Label ID="lblHeader" runat="server" Text="Production Department Item Mapping" CssClass="lblHeader" Font-Bold="true" Font-Size="15px"></asp:Label>

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
                        <td></td>
                        <td></td>
                        <td><asp:HiddenField ID="hdnDeptID" runat="server" Value="" /></td>
                        <td style="text-align:right;">
                            <asp:Label runat="server" ID="lblDept" Text="Department : "></asp:Label>
                        </td>
                        <td>
                           <asp:DropDownList ID="ddlDeptID" runat="server" CssClass="dropDownList required" Width="200px"></asp:DropDownList>
                        </td>
                        <td width="10px"></td>
                        <td>
                            <%--<asp:CheckBox ID="chkItem" Text="" runat="server" />--%>
                        </td>
                        <td width="5px"></td>
                        <td>
                            
                        </td>
                    </tr>
                    <tr>
                         <td></td>
                        <td></td>
                        <td><asp:HiddenField ID="hdnhItemID" runat="server" Value="" /></td>
                        <td style="text-align:right;">
                            <asp:Label runat="server" ID="lblhItemName" Text="Item Name : "></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txthItemName" runat="server" CssClass="textBox textAutoSelect" Width="198px"></asp:TextBox>
                            
                        </td>
                        <td><input id="btnhItemID" type="button" value="" runat="server" class="buttonDropdown" style="width: 15px " tabindex="-1" /></td>

                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td>
                         <asp:HiddenField ID="hdnFinished" runat="server" Value="" />
                          </td>
                        <td style="text-align:right;">
                            <asp:Label runat="server" ID="lblFinished" Text="Is Finished : "></asp:Label>
                        </td>
                        <td>
                          <asp:DropDownList ID="ddlFinished" runat="server" CssClass="dropDownList required" >
                              <asp:ListItem Text="(--All--)" Value=""></asp:ListItem>
                              <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                              <asp:ListItem Text="No" Value="N"></asp:ListItem>
                           </asp:DropDownList>
                         </td>

                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td>
                          <asp:Button ID="btnShow" runat="server" CssClass="buttonSearch" Text="Show" OnClick="btnShow_Click" />  

                        </td>
                    </tr>
                   
                    </table>

            </div>
            <div id="dvGridControl" style="height:auto">
            <div id="dvGrid"  style="border:1px solid blue; width:1000px" >

                <div id="dvGridHeader" style="width:1000px;">
                    <table width="900px">
                      <tr class="headerRow_Prod" >
                          <td style="width:80px " align="center">SL#</td>
                          <td style="width:300px " align="center">Item Name</td>
                          <td style="width:100px " align="center">Order No</td>
                          <td style="width:100px " align="center">Is Finished</td>
                          <td style="width:100px " align="center">Is Mixture</td>
                          <td style="width:100px " align="center">Is By Product</td>
                          <td style="width:100px " align="center">Delete</td>
                      </tr>
                  </table> 
                </div>
                <div id="dvGridMain" class="dvGrid"  style="width:900px; height:300px; overflow: auto;">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:GridView ID="grdDeptItemMapping" runat="server" AutoGenerateColumns="false" ShowHeader="false" CellPadding="1" 
                                 CellSpacing="1" GridLines="Both" DataKeyNames="ITEM_ID" ClientIDMode="AutoID" OnRowCommand="grdDeptItemMapping_RowCommand"
                                  OnRowCreated="grdDeptItemMapping_RowCreated" OnRowDataBound="grdDeptItemMapping_RowDataBound" OnRowDeleting="grdDeptItemMapping_RowDeleting">
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <Columns>
                                    <asp:TemplateField ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSlNo" runat="server" Text='<%# Bind("SLNO")%>' style="text-align:center; width:80px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <div>
                                                <table>
                                                    <tbody>
                                                        <tr>
                                                            <td style="width:200px">
                                                                <asp:TextBox ID="txtItemName" runat="server" CssClass="textBox textAutoSelect" Width="280px" Text='<%# Bind("ITEM_NAME") %>'></asp:TextBox>
                                                                <asp:HiddenField ID="hdnItemID" runat="server" Value='<%# Bind("ITEM_ID") %>' />
                                                                <asp:HiddenField ID="hdnDeptItemID" runat="server" Value='<%# Bind("DEPT_ITEM_ID") %>' />
                                                            </td>
                                                            <td>
                                                                <input id="btnItemName" type="button" value="" runat="server" class="buttonDropdown" tabindex="-1" />
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtOrderNo" runat="server" CssClass="textBox textAutoSelect"  Width="100px" Text='<%# Bind("ORDER_NO") %>'></asp:TextBox>
                                                            </td>
                                                            <td style="width:100px; text-align:center;">
                                                                <asp:HiddenField ID="hdnIsFinished" runat="server" Value='<%# Bind("IS_FINISHED") %>' />
                                                                <asp:DropDownList ID="ddlIsFinished" runat="server" >
                                                                    <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                                                    <asp:ListItem Text="No" Value="N"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                             <td style="width:100px; text-align:center;">
                                                                 <asp:HiddenField ID="hdnIsMixture" runat="server" Value='<%# Bind("IS_MIXTURE") %>' />
                                                                <asp:DropDownList ID="ddlIsMixture" runat="server" >
                                                                    <asp:ListItem Text="Yes" Value="Y" ></asp:ListItem>
                                                                    <asp:ListItem Text="No" Value="N" ></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                             <td style="width:100px; text-align:center;">
                                                                 <asp:HiddenField ID="hdnIsByProduct" runat="server" Value='<%# Bind("IS_BY_PRODUCT") %>' />
                                                                <asp:DropDownList ID="ddlIsByProduct" runat="server" >
                                                                    <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                                                    <asp:ListItem Text="No" Value="N"></asp:ListItem>
                                                                     <asp:ListItem Text="Dross" Value="D"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnDeleteRow" runat="server" CssClass="buttonDeleteIcon" Height="16px" Width="18px" CommandName="delete"></asp:LinkButton>
                                        </ItemTemplate>
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
            </div>

                <div id="divGridControls2" style="width: 100%; height: 30px; border-top: solid 1px #C0C0C0; border-bottom : solid 1px #0b07f5; ">
                     <table style="width: 70%; height: 100%; text-align: center;" cellspacing="1" cellpadding="1" border="0">
                            <tr>
                                            <%--<td style="width: 2px"></td>
                              
                                            <td style="width: 160px; text-align: right;">
                                                                             </td>
                                            <td align="right" style="text-align: left">
                                               &nbsp;
                                            </td>
                                             <td align="right">
                                                &nbsp;</td>--%>
                                            <td style="width:10px" align="left">
                                                </td>
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

                                            <td align="right">
                                                &nbsp;</td>
                                           
                                            <td align="right" style="width: 90px">&nbsp;
                                            </td>

                                        </tr>
                            
                                    </table>

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
