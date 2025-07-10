<%@ Page Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="IRRByDepartmentgridview.aspx.cs" Inherits="PG.Web.Report.INV.IRRByDepartmentgridview" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register assembly="DropDownCheckBoxes" namespace="Saplin.Controls" tagprefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script src="../../javascript/jquery.ui.combogrid.js" type="text/javascript"></script>
    <link href="../../css/jquery.ui.combogrid.css" rel="stylesheet" type="text/css" />
     <script language="javascript" type="text/javascript">
         // <!CDATA[

         var isPageResize = true;
         ContentForm.CalendarImageURL = "../../image/calendar.png";

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
        <asp:Label ID="lblHeader" CssClass="lblHeader" runat="server" Text="Requisition to Store by Department"></asp:Label>
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
                                <span>Requisition to Store by Department</span> 
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
                   <asp:Label id="lblInvoiceNo" runat="server" Text="Requisition No." ></asp:Label>
                 </td>
                 <td style="" align="left">
                      
                    <asp:TextBox id="txtInternalReceiveNo" runat="server" CssClass="textBox  enableIsDirty" 
                        width="235px"></asp:TextBox>
                     
                      </td>
                  <td style="" align="left">
                      </td>
                 <td style="" align="left">
                   </td>
                     
                      <td>
                     </td>
                 </tr>
                   <tr>
                 <td>
                   </td>
                 <td style="" align="left">
                   <asp:Label id="lblDeptName" runat="server" Text="Department Name" ></asp:Label>
                 </td>
                 <td style="" align="left">
                   <asp:DropDownList ID="ddlDeptName" runat="server" Width="240"  CssClass="dropDownList enableIsDirty"> </asp:DropDownList>
                 </td>
                  <td style="" align="left">
                      </td>
                 <td style="" align="left">
                     </td>
                     
                     <td>
                     </td>
                 </tr>
                   
                   <tr>
                 <td>
                   </td>
                 <td style="" align="left">
                   <asp:Label id="lblApproveType" runat="server" Text="Type" ></asp:Label>
                 </td>
                 <td style="" align="left">
                     <asp:DropDownList ID="ddlIRRCondition" runat="server" Width="240"  CssClass="dropDownList enableIsDirty">
                         <asp:ListItem Selected="True" Text="--All--" Value="0"></asp:ListItem>
                         <asp:ListItem Text="Waiting For ITC" Value="1"></asp:ListItem>
                        <%-- <asp:ListItem Text="Rejected" Value="2"></asp:ListItem>--%>
                         <asp:ListItem Text="Waiting For IRR" Value="2"></asp:ListItem>
                         <asp:ListItem Text="Received(IRR)" Value="3"></asp:ListItem>
                    </asp:DropDownList>
                 </td>
                  <td style="" align="left">
                      </td>
                 <td style="" align="left">
                     </td>
                     
                     <td>
                     </td>
                 </tr>
                   <tr class="rowParam">
                  <td>
                  </td>
                  <td align="right">
                  <asp:Label ID="lblFromDate" runat="server" Text="Date From:"></asp:Label>
                   </td>
                  <td>
                     <table cellpadding="0" cellspacing="0">
                     <tr>
                     <td>
                      <asp:TextBox ID="txtFromDate" runat="server" Width="70px" CssClass="textBox textDate dateParse"></asp:TextBox>
                      </td>
                      <td style="width: 4px;">
                      </td>
                      <td>
                      <asp:Label ID="lblToDate" runat="server" Text="Date To:"></asp:Label>
                      </td>
                        <td style="width: 2px;">
                         </td>                               
                          <td>                                                              
                          <asp:TextBox ID="txtToDate" runat="server" Width="70px" CssClass="textBox textDate dateParse"></asp:TextBox>
                         </td>
                         </tr>
                         </table>
                        </td>
                        <td align="right" class="">
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
                    <asp:Button ID="buttonIRRPreview" runat="server"  CssClass="buttoncommon buttonPrint" style="padding-left:22px;" Width="100px"
                Text="Preview" onclick="btnPreview_Click" />
                      </td>
                        
                  <td style="" align="right">
                      </td>
                 <td style="" align="left">
                   </td>
                     
                      <td>
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

        <div id="dvGrid" style="width:98%; height: 300px; overflow: auto;">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            CellPadding="1" CellSpacing="1" ForeColor="#333333" GridLines="None" 
            Font-Names="Arial" Font-Size="9pt" PageSize="15" 
            EmptyDataText="There is no record" 
            ShowHeader="true" Width="98%"  style="margin-bottom: 0px" CaptionAlign="Left">
             <PagerSettings Mode="NumericFirstLast" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <Columns>
                 <asp:TemplateField HeaderText="SL" HeaderStyle-Width="30px" ItemStyle-Width="30px">
                <ItemTemplate>
                    <%# Container.DataItemIndex + 1 %>
                     <ItemStyle VerticalAlign="Top" />
                </ItemTemplate>
            </asp:TemplateField>
           <%-- <asp:TemplateField HeaderText="SL#">
              <ItemTemplate>
                   <asp:Label ID="lblSLNo" runat="server" Text='<%# Bind("INV_DET_SLNO") %>' Style="text-align: center;" Width="80px"> </asp:Label></ItemTemplate>
                   <ItemStyle VerticalAlign="Top" />
                   </asp:TemplateField>--%>

                <asp:TemplateField HeaderText="Req. No" Visible="true" >
                  <ItemTemplate>
                 <asp:Label ID="lblREQNO" runat="server" Text='<%# Bind("REQ_NO") %>' Style="text-align: left;" Width="150px"></asp:Label>
                                                                   
                  </ItemTemplate>
                   <ItemStyle VerticalAlign="Top" />
                </asp:TemplateField>

                <asp:boundfield datafield="REQ_DATE" DataFormatString="{0:dd/MM/yyyy}" htmlencode="false" HeaderText="Req. Date" Visible="false" />
                 <%--<asp:TemplateField HeaderText="Req. Date" Visible="true">
                  <ItemTemplate>
                 <asp:Label ID="lblReqDate" runat="server" Text='<%# Bind("REQ_DATE") %>'   Style="text-align: center;" Width="100px"></asp:Label>
                                                                   
                  </ItemTemplate>
                   <ItemStyle VerticalAlign="Top" />
                </asp:TemplateField>--%>

                 <asp:TemplateField HeaderText="Req. Date Time" Visible="true" >
                  <ItemTemplate>
                 <asp:Label ID="lblReqtime" runat="server" Text='<%# Bind("REQ_TIME") %>' Style="text-align: left;" Width="140px" ></asp:Label>
                                                                   
                  </ItemTemplate>
                   <ItemStyle VerticalAlign="Top" />
                </asp:TemplateField>
             
                <asp:TemplateField HeaderText="From Dept." Visible="true">
                  <ItemTemplate>
                 <asp:Label ID="lblFromDept" runat="server" Text='<%# Bind("FROM_DEPT") %>' Style="text-align: left;" Width="100px" onkeypress="return onlyNos(event,this);"></asp:Label>
                                                                   
                  </ItemTemplate>
                   <ItemStyle VerticalAlign="Top" />
                </asp:TemplateField>

               <%-- <asp:TemplateField HeaderText="To Dept." Visible="true">
                  <ItemTemplate>
                 <asp:Label ID="lblToDept" runat="server" Text='<%# Bind("TO_DEPT") %>' Style="text-align: center;" Width="180px"></asp:Label>
                                                                   
                  </ItemTemplate>
                   <ItemStyle VerticalAlign="Top" />
                </asp:TemplateField>--%>

                 <asp:TemplateField HeaderText="IGR Status" Visible="true">
                  <ItemTemplate>
                 <asp:Label ID="lblIGRStatus" runat="server" Text='<%# Bind("IGR_STATUS") %>' Style="text-align: left;" Width="100px"></asp:Label>
                                                                   
                  </ItemTemplate>
                   <ItemStyle VerticalAlign="Top" />
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="ITC No" Visible="true">
                  <ItemTemplate>
                 <asp:Label ID="lblITCNO" runat="server" Text='<%# Bind("ITC_NO") %>' Style="text-align: left;" Width="120px"></asp:Label>
                                                                   
                  </ItemTemplate>
                   <ItemStyle VerticalAlign="Top" />
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="ITC Date Time" Visible="true" >
                  <ItemTemplate>
                 <asp:Label ID="lblITCTime" runat="server" Text='<%# Bind("REQ_ISSUE_TIME") %>' Style="text-align: left;" Width="140px" ></asp:Label>
                                                                   
                  </ItemTemplate>
                   <ItemStyle VerticalAlign="Top" />
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="IRR No" Visible="true">
                  <ItemTemplate>
                 <asp:Label ID="lblIRRNo" runat="server" Text='<%# Bind("IRR_NO") %>' Style="text-align: left;" Width="120px"></asp:Label>
                                                                   
                  </ItemTemplate>
                   <ItemStyle VerticalAlign="Top" />
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="IRR Date Time" Visible="true" >
                  <ItemTemplate>
                 <asp:Label ID="lblIRRTime" runat="server" Text='<%# Bind("ISSUE_RECEIVE_TIME") %>' Style="text-align: left;" Width="140px" ></asp:Label>
                                                                   
                  </ItemTemplate>
                   <ItemStyle VerticalAlign="Top" />
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="TYPE_QTY" Visible="true">
                  <ItemTemplate>
                 <asp:Label ID="lblTypeQty" runat="server" Text='<%# Bind("TYPE_QTY") %>' Style="text-align: left;" Width="350px"></asp:Label>
                                                                   
                  </ItemTemplate>
                   <ItemStyle VerticalAlign="Top" />
                </asp:TemplateField>
                

               <%--  <asp:TemplateField HeaderText="IsVisible">
                <ItemTemplate >
                    <asp:CheckBox ID="chk" runat="server"   />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center"  />
                <ItemStyle BorderColor="Gray" HorizontalAlign="Center" Width="100px"  />
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
   
   <%-- <div id="dvGrid" style="height: 320px; overflow:auto;">
    <asp:GridView ID="GridView1" runat="server" HeaderStyle-BackColor="YellowGreen"
     AutoGenerateColumns="false" AlternatingRowStyle-BackColor="WhiteSmoke" OnRowDataBound="GridView1_RowDataBound" >
        <Columns>
            <asp:TemplateField HeaderText="SL" HeaderStyle-Width="30px" ItemStyle-Width="30px">
                <ItemTemplate>
                    <%# Container.DataItemIndex + 1 %>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        
        <Columns>
            <asp:TemplateField HeaderText="Dept" HeaderStyle-Width="100px" ItemStyle-Width="60px">
                <ItemTemplate>
                    <asp:Label ID="lblDist" runat="server" Text='<%#Eval("DEPARTMENT_NAME")%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
         <Columns>
            <asp:TemplateField HeaderText="Item" HeaderStyle-Width="160px" ItemStyle-Width="80px">
                <ItemTemplate>
                    <asp:Label ID="lblDistID" runat="server" Text='<%#Eval("ITEM_NAME")%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <Columns>
            <asp:TemplateField HeaderText="Unit" HeaderStyle-Width="60px" ItemStyle-Width="80px">
                <ItemTemplate>
                    <asp:Label ID="lblDistID" runat="server" Text='<%#Eval("UOM_NAME")%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <Columns>
            <asp:TemplateField HeaderText="Type" HeaderStyle-Width="60px" ItemStyle-Width="80px">
                <ItemTemplate>
                    <asp:Label ID="lblDistID" runat="server" Text='<%#Eval("ITEM_TYPE_NAME")%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <Columns>
            <asp:TemplateField HeaderText="Group" HeaderStyle-Width="160px" ItemStyle-Width="80px">
                <ItemTemplate>
                    <asp:Label ID="lblDistID" runat="server" Text='<%#Eval("ITEM_GROUP_NAME")%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <Columns>
            <asp:TemplateField HeaderText="Class" HeaderStyle-Width="160px" ItemStyle-Width="80px">
                <ItemTemplate>
                    <asp:Label ID="lblDistID" runat="server" Text='<%#Eval("ITEM_CLASS_NAME")%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <Columns>
            <asp:TemplateField HeaderText="Req No" HeaderStyle-Width="120px" ItemStyle-Width="80px">
                <ItemTemplate>
                    <asp:Label ID="lblDis" runat="server" Text='<%#Eval("REQ_NO")%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
         <Columns>
             <asp:BoundField DataField="REQ_DATE" HeaderText="Req Date " >
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle Width="120px" HorizontalAlign="Left" />
                </asp:BoundField>
        </Columns>
         <Columns>
            <asp:TemplateField HeaderText="Req Qty" HeaderStyle-Width="60px" ItemStyle-Width="80px">
                <ItemTemplate>
                    <asp:Label ID="lblDistID" runat="server" Text='<%#Eval("REQ_QNTY")%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        
        <Columns>
             <asp:BoundField DataField="CANCEL_DATE" HeaderText="Cancel Date " >
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle Width="120px" HorizontalAlign="Left" />
                </asp:BoundField>
        </Columns>
         <Columns>
            <asp:TemplateField HeaderText="Iss No" HeaderStyle-Width="120px" ItemStyle-Width="80px">
                <ItemTemplate>
                    <asp:Label ID="lblDistID" runat="server" Text='<%#Eval("REQ_ISSUE_NO")%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
         <Columns>
             <asp:BoundField DataField="REQ_ISSUE_DATE" HeaderText="Iss Date" >
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle Width="120px" HorizontalAlign="Left" />
                </asp:BoundField> 
        </Columns>
         <Columns>
            <asp:TemplateField HeaderText="Iss Qty" HeaderStyle-Width="60px" ItemStyle-Width="80px">
                <ItemTemplate>
                    <asp:Label ID="lblDistID" runat="server" Text='<%#Eval("ISSUE_QNTY")%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
         <Columns>
            <asp:TemplateField HeaderText="Rcv No" HeaderStyle-Width="120px" ItemStyle-Width="80px">
                <ItemTemplate>
                    <asp:Label ID="lblDistID" runat="server" Text='<%#Eval("ISSUE_RECEIVE_NO")%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
         <Columns>
             <asp:BoundField DataField="ISSUE_RECEIVE_DATE" HeaderText="Recv Date" >
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle Width="120px" HorizontalAlign="Left" />
                </asp:BoundField>
        </Columns>
         <Columns>
            <asp:TemplateField HeaderText="Rcv Qty" HeaderStyle-Width="60px" ItemStyle-Width="80px">
                <ItemTemplate>
                    <asp:Label ID="lblDistID" runat="server" Text='<%#Eval("RCV_QNTY")%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        
    </asp:GridView>  
</div>--%>

        <div id="dvGridFooter" style="width:100%;height:25px; font-size: smaller;" class="subFooter">
            <table style="height: 100%; font-weight: bold;"
                cellspacing="1" cellpadding="1" rules="all" >
            <tr>
                <td width="5px" align="left"></td>
                <td align="left">
                  <asp:Label ID="lblTotal" runat="server" Text="Total: 0" 
                     style="width: 96px;"></asp:Label>
                <asp:HiddenField ID="hdnRowCount" runat="server" Value="0" />
                </td>
                <td width="50px"></td>
                
            </tr>
            </table>
        </div> 
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

