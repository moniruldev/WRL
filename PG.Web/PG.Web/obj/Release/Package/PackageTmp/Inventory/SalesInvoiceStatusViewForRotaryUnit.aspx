<%@ Page Language="C#"  MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="SalesInvoiceStatusViewForRotaryUnit.aspx.cs" Inherits="PG.Web.Inventory.SalesInvoiceStatusViewForRotaryUnit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register assembly="DropDownCheckBoxes" namespace="Saplin.Controls" tagprefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script src="../javascript/jquery.ui.combogrid.js" type="text/javascript"></script>
    <link href="../css/jquery.ui.combogrid.css" rel="stylesheet" type="text/css" />
     <script language="javascript" type="text/javascript">
         // <!CDATA[

         var isPageResize = true;
         ContentForm.CalendarImageURL = "../image/calendar.png";

         var ReportViewPageLink = '<%=this.ReportViewPageLink%>';
         var ReportViewPDFPageLink = '<%=this.ReportViewPDFPageLink%>';
        <%-- var ReportPrintPageLink = '<%=this.ReportPrintPageLink%>';
         var ReportPDFPageLink = '<%=this.ReportPDFPageLink%>';

         var GLAccountServiceLink = '<%=this.GLAccountServiceLink%>';
         var GLGroupServiceLink = '<%=this.GLGroupServiceLink%>';

         var GetJournalListServiceLink = '<%=this.GetJournalListServiceLink%>';

         var AccRefServiceLink = '<%=this.AccRefServiceLink%>';--%>

         var ifPrintButton = '<%=ifPrintButton.ClientID%>';


         var txtFromDateID = '<%=txtFromDate.ClientID%>';
         var txtToDateID = '<%=txtToDate.ClientID%>';



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




         // ]]>
    </script>
   <style type="text/css">
    
  /*.FixedHeader { POSITION: relative; TOP: expression(this.offsetParent.scrollTop); BACKGROUND-COLOR: white } */
    
    .FixedHeader { POSITION: relative; BACKGROUND-COLOR: white }
    
    #dvMessage
    {
        height: 20px;
    }
    
    .style1
    {
        width: 113px;
    }
    

    
       .auto-style34 {
           width: 43px;
       }
       .auto-style38 {
           width: 87px;
       }
       .auto-style39 {
           width: 101px;
       }
       .auto-style40 {
           width: 88px;
       }
       .auto-style41 {
           width: 99px;
       }
        
      
   </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="dvPageContent" style="width:100%; height:auto;">
    <div id="dvContentHeader" class="dvContentHeader">
    <div id="dvHeader" class="dvHeader" >
        <asp:Label ID="lblHeader" CssClass="lblHeader" runat="server" Text="Transfer to Rotary Status View"></asp:Label>
    </div>
    <!--Message Div -->
    <div id="dvMsg" runat="server" class="dvMessage">
        <asp:Label ID="lblMessage" runat="server" Font-Size="Small" Width="100%"> </asp:Label>
    </div>
     <div id="dvHeaderControl" class="dvHeaderControl">
     </div>
    </div>

    <div id="dvContentMain" class="dvContentMain">
    <div id = "dvControls" style="height:auto; width:100%">
        <div id="dvControlsInner" class="groupBoxContainer boxShadow">  
             <div id="groupBox">
                  <div id="groupHeader" class="groupHeader">
                      <div style="width:100%;height:20px;">
                         <table>
                            <tr>
                             <td>
                                <div id="dvIconEditMode" class="iconView" runat="server" ></div>
                             </td>
                             <td>
                                <span>Transfer to Rotary Status View</span> 
                             </td>
                            </tr>
                         </table>
                         
                      </div>
                      
                  </div>
                  <div id="groupContent" class="groupContent" style="width:70%; height:auto; overflow:auto;">
                  <div id="groupContenInner" style="width:100%;height:auto; text-align:left;">
              <table style="text-align:left;" border="0" cellspacing="4" cellpadding="2">
                
             
                 <tr>
                 <td>
                   </td>
                 <td style="" align="left">
                   <asp:Label id="lblInvoiceNo" runat="server" Text="Trans No." ></asp:Label>
                 </td>
                 <td style="" align="left">
                      
                    <asp:TextBox id="txtInvoiceNo" runat="server" CssClass="textBox  enableIsDirty" 
                        width="235px"></asp:TextBox>
                     
                      </td>
                  <td style="" align="left">
                      </td>
                 <td style="" align="left">
                   </td>
                     
                     
                 </tr>
                   <tr>
                 <td>
                   </td>
                 <td style="" align="left">
                   <asp:Label id="lblDeptName" runat="server" Text="Customer" ></asp:Label>
                 </td>
                 <td style="" align="left">
                   <asp:DropDownList ID="ddlCustomer" runat="server" Width="240"  CssClass="dropDownList enableIsDirty"> 
                       
                   </asp:DropDownList>
                 </td>
                  <td style="" align="left">
                      </td>
                 <td style="" align="left">
                     </td>
                     
                     
                 </tr>
                   
                   <tr>
                 <td>
                   </td>
                 <%--<td style="" align="left">
                   <asp:Label id="lblApproveType" runat="server" Text="Type" ></asp:Label>
                 </td>
                 <td style="" align="left">
                     <asp:DropDownList ID="ddlIRRCondition" runat="server" Width="240"  CssClass="dropDownList enableIsDirty">
                        
                         
                         <asp:ListItem Text="Received(IRR)" Value="1"></asp:ListItem>
                    </asp:DropDownList>
                 </td>--%>
                  <td style="" align="left">
                      </td>
                 <td style="" align="left">
                     </td>
                     
                     <td>
                     </td>
                        <td>
                     </td>
                 </tr>
                   <tr class="rowParam">
                  <td>
                  </td>
                  <td align="left">
                  <asp:Label ID="lblFromDate" runat="server" Text="Date From:"></asp:Label>
                   </td>
                  <td>
                     <table cellpadding="0" cellspacing="0">
                     <tr>
                     <td>
                      <asp:TextBox ID="txtFromDate" runat="server" Width="70px" CssClass="textBox textDate dateParse"></asp:TextBox>
                      </td>
                      <td >
                      </td>
                      <td>
                      <asp:Label ID="lblToDate" runat="server" Text="Date To:"></asp:Label>
                      </td>
                        <td >
                         </td>                               
                          <td>                                                              
                          <asp:TextBox ID="txtToDate" runat="server" Width="70px" CssClass="textBox textDate dateParse"></asp:TextBox>
                         </td>
                         </tr>
                         </table>
                        </td>
                        <td >
                          &nbsp;
                        </td>
                        <td>
                        &nbsp;
                        </td>
                   </tr> 
                 
                    <tr>
                 <td>
                   </td>
                 <td style="" align="left">
                   
                 </td>
                 <td style="" align="left">
                      
                    <asp:Button ID="btnSearch" runat="server"  CssClass="buttonRefresh" style="padding-left:22px;"
                Text="Show Data" onclick="btnSearch_Click" /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <%-- <asp:Button ID="buttonIRRPreview" runat="server"  CssClass="buttoncommon buttonPrint" style="padding-left:22px;" Width="100px"
                Text="IRR Preview" onclick="btnIRRPreview_Click" />--%>
                      </td>
                        
                  <td style="" align="right">
                      </td>
                 <td style="" align="left">
                   </td>
                     
                    
                 </tr>
              </table>
              </div>

              </div>
                  
            </div>
          </div>
        </div>
         <br />
        <br />

   <%--<div id="dvControls" style="width: 100%;">
                <div id="dvControlsInner" class="groupBoxContainer boxShadow" style="width: 1124px">
                    <div id="dvGridContainer" style="width: 100%; height: auto; text-align: left;">--%>
                        <div id="dvGridHeader" style="width: 100%; height: 25px; font-size: smaller;" class="subHeader">
                            <table style="height: 100%; color: #FFFFFF; font-weight: bold; text-align: left;"
                                class="defFont" cellspacing="1" cellpadding="1" rules="all">
                                <tr>
                                    <%--<td width="52px" align="left">
                                    </td>--%>
                                    <td width="150px" align="left">
                                       Trans No
                                    </td>
                                    <td width="120px" align="left">
                                       Trans Date
                                    </td>
                                     <td width="160px" align="left">
                                       Trans Time
                                    </td>
                                    
                                    <td width="150px" align="left">
                                       Customer
                                    </td>
                                    <td width="150px" align="left">
                                       DC No
                                    </td>
                                    <td width="120px" align="left">
                                       DC Date
                                    </td>
                                    <td width="150px" align="left">
                                       GP No
                                    </td>
                                    <td width="120px" align="left">
                                       GP Date
                                    </td>
                                    <%--<td width="120px" align="left">
                                       Invoice Status
                                    </td>--%>
                                     <td width="150px" align="left">
                                       Type Qty
                                    </td>
                                   

                                </tr>
                            </table>
                        </div>
                        <div id="dvGrid" style="width: auto; height: 350px; overflow: auto;">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="1"
                                CellSpacing="1" ForeColor="#333333" GridLines="None" Font-Names="Arial" Font-Size="9pt"
                                OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="GridView1_RowDeleting"
                                EmptyDataText="There is no record" PageSize="15" OnPageIndexChanging="GridView1_PageIndexChanging"
                                OnSelectedIndexChanged="GridView1_SelectedIndexChanged" ShowHeader="False" OnRowCommand="GridView1_RowCommand">
                                <PagerSettings Mode="NumericFirstLast" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <Columns>
                                   <%-- <asp:HyperLinkField HeaderText="" Text="">
                                        <ControlStyle CssClass="buttonViewGrid" Height="20px" Width="40px" />
                                        <ItemStyle Width="50px" />
                                    </asp:HyperLinkField>--%>
                                     <asp:TemplateField HeaderText="Invoice ID" Visible="false">
                                      <ItemTemplate>
                                     <asp:Label ID="lblINVOICEID" runat="server" Text='<%# Bind("INVOICE_ID") %>' Style="text-align: center;" Width="150px"></asp:Label>
                                                                   
                                      </ItemTemplate>
                                       <ItemStyle VerticalAlign="Top" />
                                     </asp:TemplateField>
                                    <asp:BoundField DataField="INVOICE_NO" HeaderText="Purchase No" ItemStyle-Width="150px" />
                                    <asp:BoundField DataField="INVOICE_DATE" HeaderText="Date" DataFormatString="{0:dd-MMM-yyyy}" ItemStyle-Width="120" />
                                    <asp:BoundField DataField="INVOICE_TIME" HeaderText="INVOICE_TIME" ItemStyle-Width="160" />
                                    
                                    <asp:BoundField DataField="CUST_NAME" HeaderText="Customer" ItemStyle-Width="150px" />
                                     <%--<asp:TemplateField HeaderText="DC ID" Visible="false">
                                      <ItemTemplate>
                                     <asp:Label ID="lblDCID" runat="server" Text='<%# Bind("DC_ID") %>' Style="text-align: center;" Width="150px"></asp:Label>
                                                                   
                                      </ItemTemplate>
                                       <ItemStyle VerticalAlign="Top" />
                                     </asp:TemplateField>--%>
                                     <asp:TemplateField HeaderText="DC NO" Visible="true">
                                      <ItemTemplate>
                                     <asp:Label ID="lblDCNO" runat="server" Text='<%# Bind("DC_NO") %>' Style="text-align: left;" Width="150px"></asp:Label>
                                      </ItemTemplate>
                                      <ItemStyle VerticalAlign="Top" />
                                     </asp:TemplateField>
                                  
                                    <asp:BoundField DataField="DC_DATE" HeaderText="Date" DataFormatString="{0:dd-MMM-yyyy}" ItemStyle-Width="120" />
                                          <asp:TemplateField HeaderText="GP NO" Visible="true">
                                      <ItemTemplate>
                                     <asp:Label ID="lblGPNO" runat="server" Text='<%# Bind("GP_NO") %>' Style="text-align: left;" Width="150px"></asp:Label>
                                      </ItemTemplate>
                                     <ItemStyle VerticalAlign="Top" />
                                     </asp:TemplateField>
                                    
                                    <asp:BoundField DataField="GP_DATE" HeaderText="Date" DataFormatString="{0:dd-MMM-yyyy}" ItemStyle-Width="120" />
                                    <%-- <asp:BoundField DataField="INVOICE_STATUS" HeaderText="INVOICE_STATUS" ItemStyle-Width="120" />--%>
                                   <asp:BoundField DataField="ITEMTYPEQTY" HeaderText="ITEMTYPEQTY" ItemStyle-Width="150" />
                                   
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
                                                        <asp:Label ID="Label1" runat="server" Text="Page Size:"></asp:Label>
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
                                                        <asp:Label ID="Label3" runat="server" Text="Page:"></asp:Label>
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
                <%--  </div>
                  </div>
            </div>--%>
   

        


         </div>
         <div id="dvContentFooter" class="dvContentFooter">
            <div id="dvContentFooterInner" class="dvContentFooterInner">
                <div style="width: 100%; height: 100%; margin-bottom: 0px;">
                    <div style="width: auto; min-width: 300px; height: auto; text-align: left;">
                        <table border="0">
                            <tr>
                                <td style="width: 100px;">
                                
                                </td>
                                <td>
                                    <asp:Label ID="Label2" runat="server" Text="Report View"></asp:Label> 
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlReportViewMode" runat="server" CssClass="dropDownList">
                                        <asp:ListItem Value="0">In This Tab</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="1">In New Tab</asp:ListItem>
                                        <asp:ListItem Value="2">In New Window</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                <asp:DropDownList ID="ddlReportViewType" runat="server" CssClass="dropDownList">
                                        <asp:ListItem Value="0">Screen</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="1">PDF</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                </td>
                               
                                <td style="width: 20px;">
                                </td>
                                <td style="width: 10px;">
                                </td>
                                <td>
                                    <div id="dvPrintIFrame" class="dvPrintIFrame">
                                        <iframe id="ifPrintButton" runat="server" width="0" height="0"></iframe>
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