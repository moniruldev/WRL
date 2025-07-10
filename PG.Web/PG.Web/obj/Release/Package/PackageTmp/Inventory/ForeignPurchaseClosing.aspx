<%@ Page Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="ForeignPurchaseClosing.aspx.cs" Inherits="PG.Web.Inventory.ForeignPurchaseClosing" %>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">
        // <!CDATA[


        ContentForm.CalendarImageURL = "../image/calendar.png";

        var btnGridPageGoTo = '<%=btnGridPageGoTo.ClientID %>';
        var txtGridPageNo = '<%=txtGridPageNo.ClientID %>';


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

            var url = IForm.RootPath + "Inventory/INVNewLCPurchaseClosing.aspx?purchaseMasterId=" + key;
          


            if (IForm.PageMode == Enums.PageMode.InTab) {

                var tdata = new xtabdata();
                tdata.linktype = Enums.LinkType.Direct;
                tdata.id = 0;
                tdata.name = "IMP Invoice";
                //tdata.label = "User: " + userid;
                tdata.label = "IMP Invoice";
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
                <asp:Label ID="lblHeader" CssClass="lblHeader" runat="server" Text="Foreign Purchase List"></asp:Label>
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

                        </td>
                        
                        <td>
                            <asp:Label ID="Label5" runat="server" Text="Purchase No:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtReqNo" runat="server" CssClass="textBox notEnterToTab" Width="200"></asp:TextBox>
                        </td>

                        <td>
                            <asp:Label ID="Label6" runat="server" Text="Date From"></asp:Label>
                        </td>
                        <td colspan="5">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtFromDate" runat="server" Width="80px" CssClass="textBox textDate dateParse"></asp:TextBox>
                                    </td>
                                    <td style="width: 4px;">
                                    </td>
                                    <td>
                                        <asp:Label ID="lblToDate" runat="server" Text="Date To:"></asp:Label>
                                    </td>
                                    <td style="width: 2px;">
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtToDate" runat="server" Width="80px" CssClass="textBox textDate dateParse"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        
                        <td>
                            <asp:Label ID="Label3" runat="server" Text="Invoice No:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtInvoiceNo" runat="server" CssClass="textBox notEnterToTab" Width="200"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:Button ID="btnRefresh" runat="server" CssClass="buttonRefresh" Style="padding-left: 22px;"
                                Text="Load" OnClick="btnRefresh_Click" />
                        </td>
                        <td>
                            <input id="btnAddNew" type="button" runat="server" value="New Purchase" class="buttonNew"
                                style="padding-left: 22px; width: 120px;" visible="false" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="dvControls" style="width: 100%;">
                <div id="dvControlsInner" class="groupBoxContainer boxShadow" style="width: 820px">
                    <div id="dvGridContainer" style="width: 100%; height: auto; text-align: left;">
                        <div id="dvGridHeader" style="width: 100%; height: 25px; font-size: smaller;" class="subHeader">
                            <table style="height: 100%; color: #FFFFFF; font-weight: bold; text-align: center;"
                                class="defFont" cellspacing="1" cellpadding="1" rules="all">
                                <tr>
                                     <td width="52px" align="left"></td>
                                    <td width="152px" align="left">
                                       Purchase No
                                    </td>
                                    <td width="82px" align="left">
                                       Purchase Date
                                    </td>
                                    
                                    <td width="152px" align="left">
                                       Invoice No
                                    </td>
                                    <td width="82px" align="left">
                                     Invoice Date
                                    </td>
                                   
                                     <td width="152px" align="left">
                                       LC No
                                    </td>
                                    <td width="82px" align="left">
                                     LC Date
                                    </td>
                                     <td width="82px" align="left">
                                     Is Closed
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="dvGrid" style="width: auto; height: 250px; overflow: auto;">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="1"
                                CellSpacing="1" ForeColor="#333333" GridLines="None" Font-Names="Arial" Font-Size="9pt"
                                DataKeyNames="IMP_PURCHASE_ID" OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="GridView1_RowDeleting"
                                EmptyDataText="There is no record" PageSize="15" OnPageIndexChanging="GridView1_PageIndexChanging"
                                OnSelectedIndexChanged="GridView1_SelectedIndexChanged" ShowHeader="False">
                                <PagerSettings Mode="NumericFirstLast" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <Columns>
                                   <asp:HyperLinkField HeaderText="" Text="">
                                        <ControlStyle CssClass="buttonViewGrid" Height="20px" Width="40px" />
                                        <ItemStyle Width="50px" />                                       
                                    </asp:HyperLinkField>
                                    <asp:BoundField DataField="IMP_PURCHASE_NO" HeaderText="Purchase No" ItemStyle-Width="150px" />
                                    <asp:BoundField DataField="IMP_PURCHASE_DATE" HeaderText="Date" DataFormatString="{0:dd-MMM-yyyy}"
                                        ItemStyle-Width="80" />
                                    <asp:BoundField DataField="IMP_INVOICE_NO" HeaderText="Purchase No" ItemStyle-Width="150px" />
                                    <asp:BoundField DataField="IMP_INVOICE_DATE" HeaderText="Date" DataFormatString="{0:dd-MMM-yyyy}"
                                        ItemStyle-Width="80" />

                                      <asp:BoundField DataField="LC_NO" HeaderText="Purchase No" ItemStyle-Width="150px" />
                                    <asp:BoundField DataField="LC_DATE" HeaderText="Date" DataFormatString="{0:dd-MMM-yyyy}"
                                        ItemStyle-Width="80" />
                                    <asp:BoundField DataField="IS_CLOSED" HeaderText="Is Closed" ItemStyle-Width="80" />
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
                                                <td style="width: 2px;">
                                                </td>
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
                                                    <td style="width: 2px;">
                                                    </td>
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
