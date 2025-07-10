<%@ Page Title="" Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="ProductionForcastList.aspx.cs" Inherits="PG.Web.Production.ProductionForcastList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../javascript/jquery.ui.combogrid.js" type="text/javascript"></script>
    <script src="../javascript/jquery.attributeobserver.js" type="text/javascript"></script>
    <link href="../css/jquery.ui.combogrid.css" rel="stylesheet" type="text/css" />
  
    <script language="javascript" type="text/javascript">
        var isPageResize = true;
        ContentForm.CalendarImageURL = "../image/calendar.png";

        function ShowProgress() {
            $('#' + updateProgressID).show();
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

            var url = IForm.RootPath + "Production/ProductionForcastEntry.aspx?id=" + key;
            //var url = IForm.RootPath + "Accounting/GeneralLedger/JournalFull.aspx?id=" + key;


            if (IForm.PageMode == Enums.PageMode.InTab) {

                var tdata = new xtabdata();
                tdata.linktype = Enums.LinkType.Direct;
                tdata.id = 0;
                tdata.name = "Forecast Entry";
                //tdata.label = "User: " + userid;
                tdata.label = "Forecast Entry";
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
   </script>

        
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="dvPageContent" style="width: 100%; height: auto;">
          <div id="dvContentHeader" class="dvContentHeader">
                <div id="dvHeader" class="dvHeader_Prod">
                    <asp:Label ID="lblHeader" CssClass="lblHeader" runat="server" Text="Finished Material Forecast List"></asp:Label>
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
                     <table style="width : 250px">
                              <tr>

                        <td align="right" class="auto-style2">
                            <asp:Label ID="Label8" runat="server" Text="Month:" ></asp:Label>
                        </td>
                        <td class="auto-style2" colspan="2">
                            <asp:TextBox ID="txtForcastMonth" runat="server" CssClass="textBox textDate dateParse" Style="text-align: left;" Width="100px" autofocus></asp:TextBox>
                            <%--<asp:RegularExpressionValidator runat="server" ControlToValidate="txtTranDate" ValidationExpression="(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$" ErrorMessage="Invalid date format." ValidationGroup="Group1" ForeColor="Red" />--%>
                        </td>
                        <td></td>
                        <td></td>
                    </tr>

                    <tr>
                        <td></td>
                            <td>
                                <%--<asp:Button ID="btnUpload" CssClass="buttonSearch" runat="server" OnClick="btnUpload_Click" Text="Load Data" />--%>

                                 <asp:Button ID="Button1" runat="server" CssClass="buttonSearch" Style="padding-left: 22px;"
                                Text="Show Data" OnClick="btnUpload_Click"  /> 
                                  <asp:HiddenField ID="hdnCompanyID" runat="server" Value ="0" />
                            </td>
                        <td></td>

                        <td></td>
                    </tr>
                         </table>
                    </div>


             <div id="dvControls" style="width: 100%;">
                <div id="dvControlsInner" class="groupBoxContainer boxShadow" style="width: 70%">
                    <div id="dvGridHeader2" style="width: 100%; height: 25px; font-size: smaller;" class="subHeader_Prod">
                                            <table style="height: 95%;  color: #FFFFFF; font-weight: bold; text-align: center;"
                                                class="defFont" cellspacing="1" cellpadding="1">
                                                <tr class="headerRow_Prod">
                                                     <td width="45px" class="headerColCenter">Action
                                                    </td>
                                                    <td width="80px" class="headerColCenter">FC Type
                                                    </td>
                                                     <td width="180px" class="headerColCenter">FC No.
                                                    </td>
                                                    <td width="90px" class="headerColCenter">Month
                                                    </td>
                                                    
                                                    <td width="120px" class="headerColCenter">Year
                                                    </td>
                                                 <%--   <td width="10px" class="headerColLeft">
                                                    </td>
                                                   <td width="100px" class="headerColLeft"> Forecast ID
                                                    </td>
                                                       <td width="10px" class="headerColLeft">
                                                    </td>--%>
                                                    <td width="300px" class="headerColCenter"> Forecast Desc
                                                    </td>
                                                    <td width="150px" class="headerColCenter">Forecast By 
                                                    </td>
                                                    <td width="120px" class="headerColCenter">Forecast Date
                                                    </td>
                                                    

                                                </tr>
                                            </table>
                                        </div>
                    <div id="dvGrid" style="width: 100%; height: 300px; overflow: auto;">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            CellPadding="1" CellSpacing="1" ForeColor="#333333" GridLines="None" 
            Font-Names="Arial" Font-Size="9pt" PageSize="15" 
            EmptyDataText="There is no record" 
            ShowHeader="False" Width="100%" OnRowDataBound="GridView1_RowDataBound"  >
             <PagerSettings Mode="NumericFirstLast" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <Columns>
                <asp:HyperLinkField HeaderText="" Text="">
                                        <ControlStyle CssClass="buttonViewGrid" Height="20px" Width="43px" />
                                        <ItemStyle Width="60px" />
                                    </asp:HyperLinkField>
                 <asp:TemplateField HeaderText="Type" Visible="true">
                  <ItemTemplate>
                 <asp:Label ID="lblFGFC_TYPE" runat="server" Text='<%# Bind("FGFC_TYPENAME") %>' Style="text-align: left;" Width="62px"></asp:Label>
                             <asp:HiddenField ID="lblFC_TYPE"   runat="server" Value='<%# Bind("FGFC_TYPE") %>' />                                     
                  </ItemTemplate>
                   <ItemStyle VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="FC NO" Visible="true">
                  <ItemTemplate>
                 <asp:Label ID="lblFC_NO" runat="server" Text='<%# Bind("FC_NO") %>' Style="text-align: left;" Width="150px"></asp:Label>
                 <asp:Label ID="lblFC_ID" runat="server" Text='<%# Bind("FC_ID") %>' Style="display:none; text-align: left;" Width="100px"></asp:Label>                                  
                  </ItemTemplate>
                   <ItemStyle VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Month" Visible="true">
                  <ItemTemplate>
                 <asp:Label ID="lblFOR_MONTH" runat="server" Text='<%# Bind("FOR_MONTH") %>' Style="text-align: left;" Width="70px"></asp:Label>
                                                                   
                  </ItemTemplate>
                   <ItemStyle VerticalAlign="Top" />
                </asp:TemplateField>

              
                <asp:TemplateField HeaderText=" YEAR" Visible="true">
                  <ItemTemplate>
                 <asp:Label ID="lblFOR_YEAR" runat="server" Text='<%# Bind("FOR_YEAR") %>' Style="text-align: left;" Width="90px"></asp:Label>
                                                                   
                  </ItemTemplate>
                   <ItemStyle VerticalAlign="Top" />
                </asp:TemplateField>
                

                <asp:TemplateField HeaderText="Forecast Description" Visible="true">
                  <ItemTemplate>
                 <asp:Label ID="lblFC_DESC" runat="server" Text='<%# Bind("FC_DESC") %>' Style="text-align: left;" Width="200px"></asp:Label>
                                                                   
                  </ItemTemplate>
                   <ItemStyle VerticalAlign="Top" />
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="Forecast By" Visible="true">
                      <ItemTemplate>
                            <asp:Label ID="lblFORCASTE_BY" runat="server" Text='<%# Bind("FORCAST_BY_NAME") %>' Style="text-align: left;" Width="100px"></asp:Label>
                      </ItemTemplate>
                       <ItemStyle VerticalAlign="Top" />
                </asp:TemplateField>

 

                

                 <asp:TemplateField HeaderText="Entry Date" Visible="true">
                  <ItemTemplate>
                 <asp:Label ID="lblENTRY_DATE" runat="server" Text='<%# Eval("ENTRY_DATE", "{0:dd-MMM-yyyy}")%>'  Style="text-align: center;" Width="110px"></asp:Label>
                      
                  </ItemTemplate>
                   <ItemStyle VerticalAlign="Top" />
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
                                                        <asp:Button ID="btnGridPageGoTo" runat="server" Text="Go"  />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label2" runat="server" Text="Page Size:"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlGridPageSize" runat="server" CssClass="dropDownList" Width="50"
                                                            Height="18" AutoPostBack="True" >
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
                                                        <asp:Label ID="Label4" runat="server" Text="Page:"></asp:Label>
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
                                                             ToolTip="First" />
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnGridPagePrev" runat="server" Text="" CssClass="btnGridPagePrev"
                                                            ToolTip="Previous" />
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnGridPageNext" runat="server" Text="" CssClass="btnGridPageNext"
                                                            ToolTip="Next" />
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnGridPageLast" runat="server" Text="" CssClass="btnGridPageLast"
                                                            ToolTip="Last" />
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
</asp:Content>
