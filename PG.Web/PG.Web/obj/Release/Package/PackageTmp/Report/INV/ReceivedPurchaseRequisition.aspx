<%@ Page Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="ReceivedPurchaseRequisition.aspx.cs" Inherits="PG.Web.Report.INV.ReceivedPurchaseRequisition" %>

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
    
         .FixedHeader {
            position: absolute;
            font-weight: bold;
        }     
    
   </style>

    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="dvPageContent" style="width:100%; height:auto;">
    <div id="dvContentHeader" class="dvContentHeader">
    <div id="dvHeader" class="dvHeader" >
        <asp:Label ID="lblHeader" CssClass="lblHeader" runat="server" Text="Received Purchase Requisition Report"></asp:Label>
    </div>
    <!--Message Div -->
    <div id="dvMsg" runat="server" class="dvMessage">
        <asp:Label ID="lblMessage" runat="server" Font-Size="Small" Width="100%"> </asp:Label>
    </div>
     <div id="dvHeaderControl" class="dvHeaderControl">
     </div>
    </div>

    <div id="dvContentMain" class="dvContentMain">
     <div id="dvControlsInner1" class="boxShadow">
                  <div id="groupHeader" class="groupHeader">
                      <div style="width:100%;height:20px;">
                         <table>
                            <tr>
                             <td>
                                <div id="dvIconEditMode" class="iconView" runat="server" ></div>
                             </td>
                             <td>
                                <span>Receive Purchase Requisition View</span> 
                             </td>
                            </tr>
                         </table>
                      </div>
                  </div>
              <table style="text-align:left;" border="0" cellspacing="4" cellpadding="2">
              
                  <tr>
                 <td>
                   </td>
                 <td style="" align="right">
                   <asp:Label id="lblSectionName" runat="server" Text="Section/Dept. Name" ></asp:Label>
                 </td>
                 <td style="" align="left">
                   <asp:DropDownList ID="ddlSectionName" runat="server" Width="130"  CssClass="dropDownList enableIsDirty"> </asp:DropDownList>
                 </td>
                  <td style="" align="right">
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
                 <td style="" align="right">
                   
                 </td>
                 <td style="" align="left">
                      
                    <asp:Button ID="btnSearch" runat="server"  CssClass="buttonRefresh" style="padding-left:22px;" Text="Load" onclick="btnSearch_Click" /> &nbsp;&nbsp;&nbsp;&nbsp;
              
                   
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
        <br />
        <br />

  <div id="dvGrid" style="height: 340px; overflow:auto;">
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
            <asp:TemplateField HeaderText="Branch" HeaderStyle-Width="60px" ItemStyle-Width="60px">
                <ItemTemplate>
                    <asp:Label ID="lblDistI" runat="server" Text='<%#Eval("BRANCH_NAME")%>'></asp:Label>
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
                    <asp:Label ID="lblDistID" runat="server" Text='<%#Eval("ITEM_DESC")%>'></asp:Label>
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
             <asp:BoundField DataField="APRV_DATE" HeaderText="Aprv Date " >
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle Width="120px" HorizontalAlign="Left" />
                </asp:BoundField>
        </Columns>
        <Columns>
            <asp:TemplateField HeaderText="Aprv Qty" HeaderStyle-Width="80px" ItemStyle-Width="80px">
                <ItemTemplate>
                    <asp:Label ID="lblDistID" runat="server" Text='<%#Eval("REQ_APRV_QNTY")%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <Columns>
             <asp:BoundField DataField="CNCL_DATE" HeaderText="Cancel Date " >
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle Width="120px" HorizontalAlign="Left" />
                </asp:BoundField>
        </Columns>
         <Columns>
            <asp:TemplateField HeaderText="Iss No" HeaderStyle-Width="120px" ItemStyle-Width="80px">
                <ItemTemplate>
                    <asp:Label ID="lblDistID" runat="server" Text='<%#Eval("ISSUE_NO")%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
         <Columns>
             <asp:BoundField DataField="ISSUE_DATE" HeaderText="Iss Date" >
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
                    <asp:Label ID="lblDistID" runat="server" Text='<%#Eval("RCV_NO")%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
         <Columns>
             <asp:BoundField DataField="RCV_DATE" HeaderText="Recv Date" >
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
         <%--<Columns>
            <asp:TemplateField HeaderText="" HeaderStyle-Width="80px" ItemStyle-Width="80px">
               <ItemTemplate>
                   <asp:Button ID="btnprint" runat="server" Text="Print" CssClass="buttoncommon buttonPrint" Height="20px" Width="60px" CommandName="btnReport" />
                </ItemTemplate>  
            </asp:TemplateField>
        </Columns>--%>
    </asp:GridView>  
</div>

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

