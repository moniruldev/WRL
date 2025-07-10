<%@ Page Title="" Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="INVWorkingMonthEntry.aspx.cs" Inherits="PG.Web.Inventory.INVWorkingMonthEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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

            var url = IForm.RootPath + "Inventory/INVDirectPurchaseNew.aspx?id=" + key;
            //var url = IForm.RootPath + "Accounting/GeneralLedger/JournalFull.aspx?id=" + key;


            if (IForm.PageMode == Enums.PageMode.InTab) {

                var tdata = new xtabdata();
                tdata.linktype = Enums.LinkType.Direct;
                tdata.id = 0;
                tdata.name = "Direct Purchase(Without Indent)";
                //tdata.label = "User: " + userid;
                tdata.label = "Direct Purchase(Without Indent)";
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
                <asp:Label ID="lblHeader" CssClass="lblHeader" runat="server" Text="Working Month Entry"></asp:Label>
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
                <asp:HiddenField ID="hdnYearId" runat="server" />
                <table style="text-align: left;" border="0" cellspacing="4" cellpadding="2">



                    <tr>
                        <td></td>
                        <td style="" align="right">
                            <asp:Label ID="Label4" runat="server" Text="Year:"></asp:Label>
                        </td>
                        <td style="" align="left">
                            <asp:TextBox ID="txtYear" runat="server" CssClass="textBox" ReadOnly="true" Enabled="false"></asp:TextBox>
                            <%--  <asp:DropDownList ID="ddlYear" runat="server" CssClass="dropDownList" Width="235px">
                            </asp:DropDownList>--%>

                        </td>
                        <td></td>
                        <td></td>

                    </tr>

                    <tr>
                        <td></td>
                        <td style="" align="right">
                            <asp:Label ID="Label3" runat="server" Text="Month:"></asp:Label>
                        </td>
                        <td style="" align="left">
                            <asp:DropDownList ID="ddlMonth" runat="server" CssClass="dropDownList" Width="165px">
                                <asp:ListItem Value="1">January</asp:ListItem>
                                <asp:ListItem Value="2">February</asp:ListItem>
                                <asp:ListItem Value="3">March</asp:ListItem>
                                <asp:ListItem Value="4">April</asp:ListItem>
                                <asp:ListItem Value="5">May</asp:ListItem>
                                <asp:ListItem Value="6">June</asp:ListItem>
                                <asp:ListItem Value="7">July</asp:ListItem>
                                <asp:ListItem Value="8">August</asp:ListItem>
                                <asp:ListItem Value="9">September</asp:ListItem>
                                <asp:ListItem Value="10">October</asp:ListItem>
                                <asp:ListItem Value="11">November</asp:ListItem>
                                <asp:ListItem Value="12">December</asp:ListItem>
                            </asp:DropDownList>

                        </td>
                        <td></td>
                        <td></td>

                    </tr>
                    <tr>
                        <td></td>
                        <td style="" align="right"></td>
                        <td style="" align="left">
                            <asp:CheckBox ID="cbIsOpen" runat="server" Text="Is Open" Checked="true" />
                        </td>
                        <td></td>
                        <td></td>

                    </tr>


                    <tr>
                        <td></td>
                        <td style="" align="left"></td>
                        <td style="" align="left">
                            <asp:Button ID="btnSave" runat="server" CssClass="buttoncommon buttonSave" Style="padding-left: 22px;" Width="100px" Text="Save" OnClick="btnSave_Click" />
                            <asp:Button ID="btnLoad" runat="server" CssClass="buttoncommon buttonRefresh" Style="padding-left: 22px;" Width="100px" Text="Load" OnClick="btnLoad_Click" />
                            <asp:Button ID="btnRefresh" runat="server" CssClass="buttoncommon buttonRefresh" Style="padding-left: 22px;" Width="100px" Text="Refresh" OnClick="btnRefresh_Click" />
                        </td>



                    </tr>
                </table>



            </div>
            <div id="dvControls" style="width: 100%;">
                <div id="dvControlsInner" class="groupBoxContainer boxShadow" style="width: 750px">
                    <div id="dvGridContainer" style="width: 100%; height: auto; text-align: left;">
                        <div id="dvGridHeader" style="width: 100%; height: 25px; font-size: smaller;" class="subHeader">
                            <table style="height: 100%; color: #FFFFFF; font-weight: bold; text-align: center;"
                                class="defFont" cellspacing="1" cellpadding="1" rules="all">
                                <tr>
                                    <td width="80px" align="left">Year
                                    </td>
                                    <td width="100px" align="left">Month
                                    </td>
                                    <td width="170px" align="left">Start Date
                                    </td>
                                    <td width="150px" align="left">End Date
                                    </td>
                                    <td width="100px" align="left">IS OPEN
                                    </td>

                                    <td width="100px" align="left">Entry By
                                    </td>

                                </tr>
                            </table>
                        </div>
                        <div id="dvGrid" style="width: auto; height: 250px; overflow: auto;">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="1"
                                CellSpacing="1" ForeColor="#333333" GridLines="None" Font-Names="Arial" Font-Size="9pt"
                                EmptyDataText="There is no record" PageSize="15" OnPageIndexChanging="GridView1_PageIndexChanging"
                                OnSelectedIndexChanged="GridView1_SelectedIndexChanged" ShowHeader="False" OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound">
                                <PagerSettings Mode="NumericFirstLast" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <Columns>

                                    <asp:TemplateField HeaderText="Year">

                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdnWorkingMonthId" runat="server" Value='<%# Bind("WORKING_MONTH_ID") %>' />
                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("YEAR") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="80px" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="MONTH_NAME" HeaderText="Date" ItemStyle-Width="100">
                                        <ItemStyle Width="100px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="START_DATE" HeaderText="Start Date" DataFormatString="{0:dd-MMM-yyyy}" ItemStyle-Width="170">
                                        <ItemStyle Width="170px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="END_DATE" HeaderText="End Date" DataFormatString="{0:dd-MMM-yyyy}" ItemStyle-Width="150px">
                                        <ItemStyle Width="150px"></ItemStyle>
                                    </asp:BoundField>

                                    <asp:TemplateField HeaderText="Is Open">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtIsOpen" runat="server" Text='<%# Bind("IS_OPEN") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblIsOpen" runat="server" Text='<%# Bind("IS_OPEN") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="100px" />
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="FULLNAME" HeaderText="Date" ItemStyle-Width="120">
                                        <ItemStyle Width="120px"></ItemStyle>
                                    </asp:BoundField>
                                  <%--  <asp:TemplateField HeaderText="Delete" ShowHeader="false">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnDeleteRow" CssClass="buttonEditGrid" Height="16px" Width="16px"
                                                CommandName="edit" runat="server">
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>--%>


                                     <asp:TemplateField HeaderText="Open" ShowHeader="False" Visible="false">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnOpen" Height="20px" Width="50px" ToolTip="Yes" Text="Open"
                                                CommandName="Approve" runat="server">
                                            </asp:LinkButton>

                                        </ItemTemplate>

                                        <ItemStyle VerticalAlign="Top"></ItemStyle>
                                    </asp:TemplateField>


                                     <asp:TemplateField HeaderText="Close" ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnClose" Height="20px" Width="50px" ToolTip="Yes" Text="Close"
                                                CommandName="Close" runat="server">
                                            </asp:LinkButton>

                                        </ItemTemplate>

                                        <ItemStyle VerticalAlign="Top"></ItemStyle>
                                    </asp:TemplateField>


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

