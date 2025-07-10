<%@ Page Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true"
    CodeBehind="RejectionReceiveByStoreList.aspx.cs" Inherits="PG.Web.Inventory.RejectionReceiveByStoreList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

     <script src="../javascript/jquery.ui.combogrid.js" type="text/javascript"></script>
    <script src="../javascript/jquery.attributeobserver.js" type="text/javascript"></script>
    <link href="../css/jquery.ui.combogrid.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">
        // <!CDATA[


        ContentForm.CalendarImageURL = "../image/calendar.png";

        var btnGridPageGoTo = '<%=btnGridPageGoTo.ClientID %>';
        var txtGridPageNo = '<%=txtGridPageNo.ClientID %>';

        var ItemListServiceLink = '<%=this.ItemListServiceLink%>';
        var btnItemLoad = '<%= btnItemLoad.ClientID%>';
        var hdnItemIdForFilter = '<%= hdnItemIdForFilter.ClientID%>';
        var txtItemName = '<%= txtItemName.ClientID%>';


        function PageResizeCompleted(pg, cntMain) {
            resizeContentInner(cntMain);

        }

        $(document).ready(function () {

            if ($('#' + txtItemName).is(':visible')) {
                bindItemList();
            }

        });

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

            var url = IForm.RootPath + "Inventory/RejectionReceiveByStoreEntry.aspx?receiveId=" + key ;
            //var url = IForm.RootPath + "Accounting/GeneralLedger/JournalFull.aspx?id=" + key;


            if (IForm.PageMode == Enums.PageMode.InTab) {

                var tdata = new xtabdata();
                tdata.linktype = Enums.LinkType.Direct;
                tdata.id = 0;
                tdata.name = "Rejection Receive";
                //tdata.label = "User: " + userid;
                tdata.label = "Rejection Receive";
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

    


        function bindItemList() {

            var cgColumns = [{ 'columnName': 'itemname', 'width': '300', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                             , { 'columnName': 'uomname', 'width': '50', 'align': 'left', 'highlight': 4, 'label': 'Uom' }
                             , { 'columnName': 'itemcode', 'width': '50', 'align': 'left', 'highlight': 4, 'label': 'Code' }
                             //, { 'columnName': 'itemgroupdesc', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Group Name' }
                             //, { 'columnName': 'class_name', 'width': '180', 'align': 'left', 'highlight': 4, 'label': 'Class Name' }
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
                width: 450,
                url: serviceURL,
                search: function (event, ui) {
                    var vgroupid = 0;

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
            <div id="dvHeader" class="dvHeader_Prod">
                <asp:Label ID="lblHeader" CssClass="lblHeader" runat="server" Text="Rejection Receive By Store List"></asp:Label>
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
                        <td>
                            <asp:Label ID="lblIssueDept" runat="server" Text=" Issue From Dept. :"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlIssueDepartment" runat="server" Width="220px" CssClass="dropDownList"></asp:DropDownList>
                        </td>

                        <td>
                            <asp:Label ID="lblIssueNo" runat="server" Text="Issue No:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtIssueNo" runat="server" CssClass="textBox notEnterToTab" Width="120px"></asp:TextBox>
                        </td>

                        <td>&nbsp;</td>
                        <td>
                            <asp:Label ID="lblReceiveDept" runat="server" Text=" Receive From Dept. :"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlReceiveDept" runat="server" Width="220px" CssClass="dropDownList"></asp:DropDownList>
                        </td>
                   
                       
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="lblFromDate" runat="server" Text="Date From :"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFromDate" runat="server" Width="80px" CssClass="textBox textDate dateParse"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="lblToDate" runat="server" Text="Date To:"></asp:Label>
                        </td>


                        <td>
                            <asp:TextBox ID="txtToDate" runat="server" Width="80px" CssClass="textBox textDate dateParse"></asp:TextBox>
                        </td>
                        <td></td>
                       <td align="right">
                            <asp:Label ID="Label7" runat="server" Text="Item :"></asp:Label>

                        </td>
                        <td>
                            
                            <table>
                                <tr>
                                    <td> <asp:TextBox ID="txtItemName" Width="250px" runat="server" CssClass="textBox required" Enabled="true"></asp:TextBox></td>
                                    <td> <input id="btnItemLoad" type="button" value="" runat="server" class="buttonDropdown" tabindex="-1" /></td>
                                </tr>
                            </table>
                           
                           

                        </td>
                          <td colspan="2">
                               <asp:HiddenField ID="hdnItemIdForFilter" runat="server" />
                          </td>
                        <td></td>

                    </tr>

                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>


                        <td>
                            <asp:Button ID="btnRefresh" runat="server" CssClass="buttonRefresh" Style="padding-left: 22px;"
                                Text="Show Data" OnClick="btnRefresh_Click" />
                        </td>
                        <td></td>
                        <td></td>
                        <td>&nbsp;
                        </td>
                        <td>&nbsp;
                        </td>
                        <td></td>

                    </tr>
                </table>
            </div>
            <div id="dvControls" style="width: 100%;">
                <div id="dvControlsInner" class="groupBoxContainer boxShadow" style="width: 650px">
                    <div id="dvGridContainer" style="width: 100%; height: auto; text-align: left;">
                        <div id="dvGridHeader" style="width: 100%; height: 25px; font-size: smaller;" class="subHeader">
                            <table style="height: 100%; color: #FFFFFF; font-weight: bold; text-align: center;"
                                class="defFont" cellspacing="1" cellpadding="1" rules="all">
                                <tr class="headerRow_Prod">
                                    <td width="50px" align="left"></td>
                                    <td width="150px" align="left">Receive No
                                    </td>
                                    <td width="100px" align="left">Receive Date
                                    </td>
                                  
                                    <td width="200px" align="left">Receive Dept</td>
                                      <td width="200px" align="left">Issue Dept</td>
                                  

                                </tr>
                            </table>
                        </div>
                        <div id="dvGrid" style="width: 650px; height: 250px; overflow: auto;">
                            <asp:GridView ID="GridView1" Width="100%" runat="server" AutoGenerateColumns="False" CellPadding="1"
                                CellSpacing="1" ForeColor="#333333" GridLines="None" Font-Names="Arial" Font-Size="9pt"
                                OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="GridView1_RowDeleting"
                                EmptyDataText="There is no record" PageSize="15" OnPageIndexChanging="GridView1_PageIndexChanging"
                                OnSelectedIndexChanged="GridView1_SelectedIndexChanged" ShowHeader="False">
                                <PagerSettings Mode="NumericFirstLast" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <Columns>
                                    <asp:HyperLinkField HeaderText="" Text="View">
                                        <ControlStyle Height="20px" Width="50px" />
                                        <ItemStyle Width="50px" />
                                    </asp:HyperLinkField>

                                    <asp:BoundField DataField="REJ_RECEIVE_NO" HeaderText=" ISSUE No" ItemStyle-Width="150px" />
                                    <asp:BoundField DataField="REJ_RECEIVE_DATE" HeaderText="Date" DataFormatString="{0:dd-MMM-yyyy}"
                                        ItemStyle-Width="100" />
                                    <asp:BoundField DataField="DEPARTMENT_NAME" HeaderText="Receive Dept" ItemStyle-Width="200px" />
                                      <asp:BoundField DataField="FROM_DEPARTMENT_NAME" HeaderText="Issue Dept" ItemStyle-Width="200px" />

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
