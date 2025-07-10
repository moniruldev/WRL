<%@ Page Title="" Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="INVLCList.aspx.cs" Inherits="PG.Web.Inventory.INVLCList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

     <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css"/> 
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap-theme.min.css"/>  
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js" type="text/javascript"></script> 

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

             var url = IForm.RootPath + "Inventory/LC_ENTRY.aspx?id=" + key;
             //var url = IForm.RootPath + "Accounting/GeneralLedger/JournalFull.aspx?id=" + key;
             if (IForm.PageMode == Enums.PageMode.InTab) {

                 var tdata = new xtabdata();
                 tdata.linktype = Enums.LinkType.Direct;
                 tdata.id = 0;
                 tdata.name = "New LC";
                 //tdata.label = "User: " + userid;
                 tdata.label = "New LC";
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
                <asp:Label ID="lblHeader" CssClass="lblHeader" runat="server" Text="MRR Creation From LC"></asp:Label>
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

                        <td>
                            <asp:Label ID="Label5" runat="server" Text="LC No:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtReqNo" runat="server" CssClass="textBox notEnterToTab" Width="150"></asp:TextBox>
                        </td>
                           <td>
                            <asp:Label ID="lblIMPPONo" runat="server" Text="PO No:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtIMPPONo" runat="server" CssClass="textBox notEnterToTab" Width="150"></asp:TextBox>
                        </td>
                    
                       
                    </tr>
                    <tr>
                         <td></td>

                            <td>
                            <asp:Label ID="Label6" runat="server" Text="LC Date From"></asp:Label>
                        </td>
                        <td colspan="5">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtFromDate" runat="server" Width="80px" CssClass="textBox textDate dateParse"></asp:TextBox>
                                    </td>
                                    <td style="width: 4px;"></td>
                                    <td>
                                        <asp:Label ID="lblToDate" runat="server" Text="Date To:"></asp:Label>
                                    </td>
                                  <%--  <td style="width: 2px;"></td>--%>
                                    <td>
                                        <asp:TextBox ID="txtToDate" runat="server" Width="80px" CssClass="textBox textDate dateParse"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td></td>

                     

                    </tr>
                    <tr>
                        <td></td>

                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td></td>
                        <td>
                            <asp:Button ID="btnRefresh" runat="server" CssClass="buttonRefresh" Style="padding-left: 22px;"
                                Text="Search" OnClick="btnRefresh_Click" />
                        </td>
                        <td> <asp:Button ID="btnAddNew" runat="server" CssClass="buttonNew"    Text="New LC" Width="90px" Height="26px" /></td>
                        <td>&nbsp;
                        </td>
                        <td>&nbsp;
                        </td>
                        <td></td>
                        <td></td>
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
                                    <td style="width:80px"></td>
                                     <td width="100px" align="left">Detail's
                                    </td>
                                    <td width="130px" align="center">LC No
                                    </td>
                                    <td width="95px" align="center">LC Date
                                    </td>
                                     <td width="180px" align="center">PO No
                                    </td>
                                    <%-- <td width="95px" align="center">PO Date
                                    </td>--%>
                                    <td width="200px" align="center">Bank Name</td>
                                     <td width="50px" align="center">LC/TT
                                    </td>

                                    <td width="100px" align="center">LC Type
                                    </td>
                                    <td width="250px" align="center">Supplier
                                    </td>

                                </tr>
                            </table>
                        </div>
                        <div id="dvGrid" style="width: auto; height: 250px; overflow: auto;">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="1"
                                CellSpacing="1" ForeColor="#333333" GridLines="None" Font-Names="Arial" Font-Size="9pt"
                                DataKeyNames="LC_ID" OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="GridView1_RowDeleting"
                                EmptyDataText="No Data Found" PageSize="15" OnPageIndexChanging="GridView1_PageIndexChanging"
                                OnSelectedIndexChanged="GridView1_SelectedIndexChanged" ShowHeader="False" OnRowCommand="GridView1_RowCommand">
                                <PagerSettings Mode="NumericFirstLast" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <Columns>
                                    <asp:HyperLinkField HeaderText="" Text="VIEW">
                                        <ControlStyle Height="20px" Width="60px" />
                                        <ItemStyle Width="60px" />
                                    </asp:HyperLinkField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkItemDtl"  CssClass="btn btn-default"  CommandName="dtl" CommandArgument='<%# Bind("LC_NO") %>'><i class="glyphicon glyphicon-info-sign" style="padding-right:4px; font-size:14px; color:#4695cf"></i>Details</asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle Width="100px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="hdnLcId" runat="server" Text='<%# Bind("LC_ID") %>' Style="text-align: center;" Width="80px"> </asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="LC_NO" HeaderText="LC No" ItemStyle-Width="98px">
                                        <ItemStyle Width="120px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="LC_DATE" HeaderText="Date" DataFormatString="{0:dd-MMM-yyyy}"
                                        ItemStyle-Width="95">
                                        <ItemStyle Width="95px"></ItemStyle>
                                    </asp:BoundField>
                                       <asp:BoundField DataField="IMP_PURCHASE_NO" HeaderText="PO No" ItemStyle-Width="98px">
                                        <ItemStyle Width="180px"></ItemStyle>
                                    </asp:BoundField>
                                     
                                    <asp:BoundField DataField="BANK_NAME" ItemStyle-Width="200">

                                        <ItemStyle Width="200px"></ItemStyle>
                                    </asp:BoundField>
                                        <asp:BoundField DataField="MOD_PAY_DESC" ItemStyle-Width="50">

                                        <ItemStyle Width="50px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="LC_TYPE_DESC" ItemStyle-Width="100">

                                        <ItemStyle Width="100px"></ItemStyle>
                                    </asp:BoundField>

                                    <asp:BoundField DataField="SUP_NAME" ItemStyle-Width="250">

                                        <ItemStyle Width="250px"></ItemStyle>
                                    </asp:BoundField>


                                    <%-- 
                                         <asp:BoundField DataField="IMP_PURCHASE_DATE" HeaderText="Date" DataFormatString="{0:dd-MMM-yyyy}"
                                        ItemStyle-Width="95">
                                        <ItemStyle Width="95px"></ItemStyle>
                                    </asp:BoundField>
                                        <asp:TemplateField HeaderText="Is Authorized">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIsPosted" Text='<%# Eval("AUTH_STATUS").ToString() == "N" ? "No": "Yes" %>'
                                                runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Wrap="false" Width="80px" />
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
                         <div id="myModalItemDetails" class="modal fade" >  
            <div class="modal-dialog" style="max-width:1000px; align-content:space-around;"> <%-- --%>
                <div class="modal-content" style="max-width:1000px;">  
                    <div class="modal-header" style="max-width:1000px;">  
                        
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>  
                        <h4 class="modal-title">Item Details</h4>  
                    </div>  
                    <div class="modal-body" style="overflow-y:scroll; overflow-x:scroll; max-height:300px; max-width:1080px; margin-top: 10px; margin-bottom: 10px;"> <%-- --%>
                        <asp:Label ID="Label8" runat="server" ClientIDMode="Static"></asp:Label>  
                         <asp:Label ID="lblIndtNo" runat="server" Text="LC No : " ></asp:Label>
                        <asp:Label ID="lblIndtNoText" runat="server" Visible="true"  Font-Bold="true" ></asp:Label>
                        <asp:HiddenField ID="hdnStatusDetailsMRRID" runat="server" />
                       <asp:GridView ID="grdItemDetails" runat="server" HeaderStyle-BackColor="#3AC0F2" HeaderStyle-ForeColor="Black"
            RowStyle-BackColor="White" AlternatingRowStyle-BackColor="#A1DCF2" AlternatingRowStyle-ForeColor="#000"
            BorderStyle="None" BorderWidth="5px" CellPadding="10"  GridLines="Vertical" CssClass="gridView"
            AutoGenerateColumns="false">
            <Columns>
                 <asp:TemplateField HeaderText="SL" ItemStyle-Width="30px">   
                     <ItemTemplate>
                             <%# Container.DataItemIndex + 1 %>   
                     </ItemTemplate>
                 </asp:TemplateField>
                <asp:BoundField DataField="ITEM_NAME" HeaderText="ITEM NAME" ItemStyle-Width="600px" />
                  <asp:BoundField DataField="LC_QTY" HeaderText="LC QTY" ItemStyle-Width="80px" />
                <asp:BoundField DataField="UOM_CODE" HeaderText="UOM" ItemStyle-Width="60px" />
                <asp:BoundField DataField="CONVERTED_ITEM_QTY" HeaderText="Convert QTY" ItemStyle-Width="80px" />
                <asp:BoundField DataField="MRR_QTY" HeaderText="MRR QTY" ItemStyle-Width="80px" />
                <asp:BoundField DataField="MRR_UOM" HeaderText="MRR UOM" ItemStyle-Width="60px" />
              <asp:BoundField DataField="PENDING_QNTY" HeaderText="Pending QTY" ItemStyle-Width="80px" />
                  
          
            </Columns>
        </asp:GridView>
                      
                    </div>  
                
                    <div class="modal-footer"> 
                        <table>
                            <tr>
                        <td>
                       
                   
                        <button type="button"  Class="buttonClose"  data-dismiss="modal">Close</button>  
                             </td>
                                </tr>
                          </table> 
                    </div>  
                </div>  
            </div>  
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
