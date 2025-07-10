<%@ Page Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="QCPass_List.aspx.cs" Inherits="PG.Web.Inventory.QCPass_List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="../javascript/jquery.ui.combogrid.js" type="text/javascript"></script>
    <script src="../javascript/jquery.attributeobserver.js" type="text/javascript"></script>
    <link href="../css/jquery.ui.combogrid.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">
        // <!CDATA[

        ContentForm.CalendarImageURL = "../image/calendar.png";

        var btnGridPageGoTo = '<%=btnGridPageGoTo.ClientID %>';
        var txtGridPageNo = '<%=txtGridPageNo.ClientID %>';

        var SupplierListServiceLink = '<%=this.SupplierListServiceLink%>';
        var txtSupplierName = '<%=txtSupplierName.ClientID%>';
        var btnSupplierID = '<%=btnSupplierID.ClientID%>';
        var hdnSupplierID = '<%=hdnSupplierID.ClientID%>';


        var ItemListServiceLink = '<%=this.ItemListServiceLink%>';
        var btnItemLoad = '<%= btnItemLoad.ClientID%>';
        var hdnItemIdForFilter = '<%= hdnItemIdForFilter.ClientID%>';
        var txtItemName = '<%= txtItemName.ClientID%>';

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
            $('#' + txtGridPageNo).keydown(function (e) {
                if (e.keyCode == 13) {
                    e.preventDefault();
                    $('#' + btnGridPageGoTo).click();
                }
            });
        });



        function tbopen(key, userid) {
            key = key || '';

            var url = IForm.RootPath + "Inventory/QC_Pass_List_view.aspx?QCPassMasterId=" + key;
            //var url = IForm.RootPath + "Accounting/GeneralLedger/JournalFull.aspx?id=" + key;


            if (IForm.PageMode == Enums.PageMode.InTab) {

                var tdata = new xtabdata();
                tdata.linktype = Enums.LinkType.Direct;
                tdata.id = 0;
                tdata.name = "QC Pass Entry";
                //tdata.label = "User: " + userid;
                tdata.label = "QC Pass Entry";
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

        $(document).ready(function () {

            if ($('#' + txtSupplierName).is(':visible')) {

                bindSupplierList();

            }


            if ($('#' + txtItemName).is(':visible')) {
                bindItemList();
            }

        });

        function bindSupplierList() {
            var cgColumns = [{ 'columnName': 'supcode', 'width': '80', 'align': 'left', 'highlight': 2, 'label': 'Code' }
                             , { 'columnName': 'supname', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                             , { 'columnName': 'supaddress', 'width': '200', 'align': 'left', 'highlight': 4, 'label': 'Address' }
                             , { 'columnName': 'supphone', 'width': '80', 'align': 'left', 'highlight': 0, 'label': 'phone' }



            ];


            //var companyid = $('#' + hdnCompanyID).val();
            //var depthead = $('#' + hdnEmpCode).val();
            //var locationid = $('#' + ddlLocation).val();
            // var seid = $('#' + txtExecutiveID).val();

            //var serviceURL = GLAccountServiceLink + "?isterm=1&includeempty=1&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            var serviceURL = SupplierListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            //serviceURL += "&companyid=" + companyid;
            serviceURL += "&ispaging=1&suptype=L";
            // serviceURL += "&locationid=" + locationid;
            //serviceURL += "&seid=" + seid;
            // serviceURL += "&empstatus=" + "A";
            //serviceURL += "&acctypefilter=" + Enums.GLAccountTypeFilter.AllAccount;
            // serviceURL += "&namecomptype=" + Enums.DataCompareType.Contains;



            var supplierIDElem = $('#' + txtSupplierName);

            $('#' + btnSupplierID).click(function (e) {

                //elmID = $(elem).attr('id');
                //$(elem).combogrid("show");
                $(supplierIDElem).combogrid("dropdownClick");
            });


            $(supplierIDElem).combogrid({
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
                        $('#' + txtSupplierName).val('');
                        return false;
                        //ClearGLAccountData(elemID);
                    }


                    if (ui.item.supid == '') {
                        event.preventDefault();
                        return false;
                        //ClearGLAccountData(elemID);
                    }
                    else {
                        // $('#' + hdnDealerID).val(ui.item.dealerid);
                        $('#' + hdnSupplierID).val(ui.item.supid);
                        $('#' + txtSupplierName).val(ui.item.supname);


                    }
                    return false;
                },

                lc: ''
            });


            $(supplierIDElem).blur(function () {
                var self = this;

                var customerID = $(supplierIDElem).val();
                if (customerID == '') {
                    $('#' + hdnSupplierID).val('0');
                    $('#' + txtSupplierName).val('');

                }
            });
        }

        function bindItemList() {

            var cgColumns = [{ 'columnName': 'itemname', 'width': '200', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                             , { 'columnName': 'uomname', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Uom' }
                             , { 'columnName': 'itemcode', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Code' }
                             , { 'columnName': 'itemgroupdesc', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Group Name' }
                             , { 'columnName': 'class_name', 'width': '180', 'align': 'left', 'highlight': 4, 'label': 'Class Name' }
                             //, { 'columnName': 'item_type', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Item Type' }



            ];
            var serviceURL = ItemListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;


            serviceURL += "&ispaging=1&isigr=1";
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
                width: 750,
                url: serviceURL,
                search: function (event, ui) {
                    var vgroupid = 0;
                    //var groupName = $('#' + txtGroupName).val();
                    //if (groupName != "") {
                    //    vgroupid = $('#' + hdnGroupID).val();
                    //    if (vgroupid == "0" || vgroupid == "") {
                    //        vgroupid = 0;
                    //    }
                    //} else {
                    //    $('#' + hdnGroupID).val('0');

                    //}
                    var newServiceURL = serviceURL;  // + "&groupid=" + vgroupid;

                    newServiceURL = JSUtility.AddTimeToQueryString(newServiceURL);
                    // var newServiceURL = serviceURL;
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
                    $('#' + hdnItemIdForFilter).val('0');
                    $('#' + txtItemName).val('');
                    //$('#' + txtGroupCode).val('');
                }
            });
        }


        // ]]>
    </script>
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
    <div id="dvPageContent" style="width: 100%; height: auto;">
        <div id="dvContentHeader" class="dvContentHeader">
            <div id="dvHeader" class="dvHeader">
                <asp:Label ID="lblHeader" CssClass="lblHeader" runat="server" Text="QC Pass List"></asp:Label>
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
                <table>
                    <tr>
                        <td></td>

                        <td align="right">
                            <asp:Label ID="Label5" runat="server" Text="QC Pass No:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtQCNo" runat="server" CssClass="textBox notEnterToTab" Width="150"></asp:TextBox>
                        </td>

                        <td align="right">
                            <asp:Label ID="Label4" runat="server" Text="MRR No:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtMRRNo" runat="server" CssClass="textBox notEnterToTab" Width="150"></asp:TextBox>
                        </td>
                        
                    </tr>
                      


                    <tr>
                        <td></td>

                        <td align="right">
                            <asp:Label ID="Label6" runat="server" Text="Date From"></asp:Label>
                        </td>
                        <td>
                                        <asp:TextBox ID="txtFromDate" runat="server"  Width="150" CssClass="textBox textDate dateParse"></asp:TextBox>
                        </td>
                        <td align="right">
                                        <asp:Label ID="lblToDate" runat="server" Text="Date To:"></asp:Label>
                                    </td>
                        <td>
                                        <asp:TextBox ID="txtToDate" runat="server"  Width="150" CssClass="textBox textDate dateParse"></asp:TextBox>
                        </td>
                        
                    </tr>

                          <tr>
                        <td></td>
                            <td align="right">
                            <asp:Label ID="Label7" runat="server" CssClass="" Text="Supplier :" ></asp:Label>

                            </td>
                       <td>
                            <table>
                                <tr>
                                    <td> <asp:TextBox ID="txtSupplierName" runat="server" CssClass="textBox" Width="200px"></asp:TextBox></td>
                                    <td> <input id="btnSupplierID" type="button" value="" runat="server" class="buttonDropdown" tabindex="-1" /></td>
                                </tr>
                            </table>
                           
                           
                        </td>
                       <td colspan="2"><asp:HiddenField ID="hdnSupplierID" runat="server" Value="0" /></td>


                    </tr>

                      <tr style="display:none;">
                           <td></td>
                        <td align="right">
                            <asp:Label ID="Label8" runat="server" Text="Item :"></asp:Label>

                        </td>
                        <td>
                            
                            <table>
                                <tr>
                                    <td> <asp:TextBox ID="txtItemName" Width="200px" runat="server" CssClass="textBox required" Enabled="true"></asp:TextBox></td>
                                    <td> <input id="btnItemLoad" type="button" value="" runat="server" class="buttonDropdown" tabindex="-1" /></td>
                                </tr>
                            </table>
                           
                           

                        </td>
                          <td colspan="2">
                               <asp:HiddenField ID="hdnItemIdForFilter" runat="server" />
                          </td>

                        <td style="text-align: right">&nbsp;</td>

                       
                       
                    </tr>
                     <tr>
                        <td></td>

                        <td align="right">
                            <asp:Label ID="Label3" runat="server" Text="QC Type:"></asp:Label>
                        </td>
                        <td>
                           <asp:DropDownList ID="ddlQCType" CssClass="dropDownList" runat="server"></asp:DropDownList>
                        </td>

                        <td>
                            &nbsp;</td>
                        <td colspan="5">
                            
                        </td>
                        
                    </tr>
                    <tr>
                        <td></td>

                        <td></td>
                        <td>
                            <asp:Button ID="btnRefresh" runat="server" CssClass="buttonRefresh" Style="padding-left: 22px;"
                                Text="Show Data" OnClick="btnRefresh_Click" />
                        </td>
                        <td>
                            <input id="btnAddNew" type="button" runat="server" value="New QC" class="buttonNew"
                                style="padding-left: 22px; width: 120px;" visible="false" />
                        </td>
                        <td>&nbsp;
                        </td>
                        <td>&nbsp;
                        </td>
                       
                    </tr>
                </table>
            </div>
            <div id="dvControls" style="width: 100%;">
                <div id="dvControlsInner" class="groupBoxContainer boxShadow" style="width: 950px">
                    <div id="dvGridContainer" style="width: 100%; height: auto; text-align: left;">
                        <div id="dvGridHeader" style="width: 100%; height: 25px; font-size: smaller;" class="subHeader">
                            <table style="height: 100%; color: #FFFFFF; font-weight: bold; text-align: center;"
                                class="defFont" cellspacing="1" cellpadding="1" rules="all">
                                <tr>
                                    <td width="52px" align="left"></td>
                                    <td width="152px" align="left">QC No
                                    </td>
                                    <td width="80px" align="left">QC Date
                                    </td>
                                     <td width="80px" align="left">QC Type
                                    </td>
                                      <td width="80px" align="left">Transfer?
                                    </td>
                                    <td width="150px" align="left">MRR No
                                    </td>
                                    <td width="100px" align="left">MRR Date
                                    </td>
                               <%--     <td width="200px" align="left">Item's
                                    </td>--%>
                                    <td width="200px" align="left">Supplier
                                    </td>
                                     <td width="200px" align="left">Create By
                                    </td>

                                    <%-- <td width="52px" align="center">
                                        Authorize?
                                    </td>
                                    --%>
                                </tr>
                            </table>
                        </div>
                        <div id="dvGrid" style="width:auto; height: 250px; overflow: auto;">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="1"
                                CellSpacing="1" ForeColor="#333333" GridLines="None" Font-Names="Arial" Font-Size="9pt" CssClass="grid"
                                DataKeyNames="QC_ID" OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="GridView1_RowDeleting"
                                EmptyDataText="There is no record" PageSize="15" OnPageIndexChanging="GridView1_PageIndexChanging"
                                OnSelectedIndexChanged="GridView1_SelectedIndexChanged" ShowHeader="False">
                                <PagerSettings Mode="NumericFirstLast" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <Columns>
                                    <asp:HyperLinkField HeaderText="" Text="">
                                        <ControlStyle CssClass="buttonViewGrid" Height="20px" Width="40px" />
                                        <ItemStyle Width="50px" />
                                    </asp:HyperLinkField>
                                    <asp:BoundField DataField="QC_NO" HeaderText="QC No" ItemStyle-Width="150px" />
                                    <asp:BoundField DataField="QC_DATE" HeaderText="Date" DataFormatString="{0:dd-MMM-yyyy}" ItemStyle-Width="100" />
                                    <asp:BoundField DataField="QC_TYPE_NAME" HeaderText="Qc Type" ItemStyle-Width="80px" />
                                     <asp:BoundField DataField="QC_PASS_STATUS" HeaderText="Is Transfer" ItemStyle-Width="80px" />
                                    <asp:BoundField DataField="MRR_NO" HeaderText="MRR No" ItemStyle-Width="150px" />
                                    <asp:BoundField DataField="MRR_DATE" HeaderText="MRR Date" DataFormatString="{0:dd-MMM-yyyy}" ItemStyle-Width="100px" />
                                    <%--<asp:BoundField DataField="ITEM_NAME" HeaderText="ItemName" ItemStyle-Width="200px" />--%>
                                    <asp:BoundField DataField="SUP_NAME" HeaderText="Supplier" ItemStyle-Width="200px" />
                                    <asp:BoundField DataField="FULLNAME" HeaderText="Create By" ItemStyle-Width="200px" />



                                    <%--  <asp:TemplateField HeaderText="Authorize?">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIsPosted" Text='<%# Eval("AUTH_STATUS").ToString() == "N" ? "No": "Yes" %>'
                                                runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Wrap="false" Width="52px" />
                                    </asp:TemplateField>--%>
                                </Columns>
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#999999" />
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            </asp:GridView>
                        </div>
                        <div id="dvGridFooter" style="width: 100%; height: 25px; font-size: smaller;" class="subFooter">
                            <table style="height: 100%; width: 100%; font-weight: bold;" cellspacing="2" cellpadding="1"
                                rules="all">
                                <tr>
                                    <td align="left" style="width: 40%">
                                        <table>
                                            <tr>
                                                <td style="width: 2px;"></td>
                                                <td>
                                                    <asp:Label ID="lblTotal" runat="server" Text="Rows: 0 of 0"></asp:Label>
                                                    <asp:HiddenField ID="hdnRowCount" runat="server" Value="0" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td align="right" style="width: 60%">
                                        <div id="dvGridPager" class="dvGridPager">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="btnGridPageGoTo" runat="server" Text="Go" OnClick="btnGridPageGoTo_Click" />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label2" runat="server" Text="Page Size:"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlGridPageSize" runat="server" CssClass="dropDownList" Width="50"
                                                            Height="18" AutoPostBack="True" OnSelectedIndexChanged="ddlGridPageSize_SelectedIndexChanged">
                                                            <asp:ListItem Value="10">10</asp:ListItem>
                                                            <asp:ListItem Value="20">20</asp:ListItem>
                                                            <asp:ListItem Value="30">30</asp:ListItem>
                                                            <asp:ListItem Value="50" Selected="True">50</asp:ListItem>
                                                            <asp:ListItem Value="100">100</asp:ListItem>
                                                            <asp:ListItem Value="200">200</asp:ListItem>
                                                            <asp:ListItem Value="0">all</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label1" runat="server" Text="Page:"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtGridPageNo" runat="server" CssClass="textBox" Width="30" Height="14"
                                                            Style="text-align: center;">0</asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblGridPageInfo" runat="server" Text=" of 0"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnGridPageFirst" runat="server" Text="" CssClass="btnGridPageFirst"
                                                            OnClick="btnGridPageFirst_Click" ToolTip="First" />
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnGridPagePrev" runat="server" Text="" CssClass="btnGridPagePrev"
                                                            OnClick="btnGridPagePrev_Click" ToolTip="Previous" />
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnGridPageNext" runat="server" Text="" CssClass="btnGridPageNext"
                                                            OnClick="btnGridPageNext_Click" ToolTip="Next" />
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnGridPageLast" runat="server" Text="" CssClass="btnGridPageLast"
                                                            OnClick="btnGridPageLast_Click" ToolTip="Last" />
                                                    </td>
                                                    <td style="width: 2px;"></td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="dvContentFooter" class="dvContentFooter">
        </div>
    </div>
</asp:Content>
