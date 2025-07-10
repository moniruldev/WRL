<%@ Page Language="C#"  MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="ReOrderToPurchase.aspx.cs" Inherits="PG.Web.Report.INV.ReOrderToPurchase" %>
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




         function ReportPrint(key, isPDFAutoPrint) {
             var rptPageLink = ReportViewPageLink;
             if (isPDFAutoPrint) {
                 //rptPageLink = ReportPDFPageLink;
                 rptPageLink = ReportViewPDFPageLink;
             }

             //var url = "./Report/ReportView.aspx?rk=" + key
             var now = new Date();
             var strTime = now.getTime().toString();
             var url = ReportViewPageLink + "?rk=" + key + "&_tt=" + strTime;

             //var url = rptPageLink + "?rk=" + key;

             iframe = document.getElementById(ifPrintButton);
             if (iframe === null) {
                 iframe = document.createElement('iframe');
                 iframe.id = hiddenIFrameID;
                 //        iframe.style.display = 'none';
                 //        iframe.style = 'none';
                 document.body.appendChild(iframe);
             }
             iframe.src = url;
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
        <asp:Label ID="lblHeader" CssClass="lblHeader" runat="server" Text="Re Order To Purchase Info"></asp:Label>
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
                                <span>Re Order To Purchase</span> 
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
                   <asp:Label id="lblCategory" runat="server" Text="Group" ></asp:Label>
                 </td>
                 <td style="" align="left">
                      
                     <asp:DropDownList ID="ddlGroup" runat="server" Width="130"  CssClass="dropDownList enableIsDirty" AutoPostBack="True" OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged"  >

                     </asp:DropDownList>
                     
                 </td>
                  <td style="" align="right">
                      </td>
                 <td style="" align="left">
                     </td>
                     
                     <td>
                     </td>
                 </tr>
                 
                   <tr>
                 <td>
                   </td>
                 <td style="" align="right">
                   <asp:Label id="lblItemCode" runat="server" Text="Item" ></asp:Label>
                 </td>
                 <td style="" align="left">
                      
                     <asp:DropDownList ID="ddlItem" runat="server" Width="130"  CssClass="dropDownList enableIsDirty">

                     </asp:DropDownList>
                     
                 </td>
                  <td style="" align="right">
                      </td>
                 <td style="" align="left">
                     </td>
                     
                     <td>
                     </td>
                 </tr>
                  <tr>
                 <td>
                   </td>
                 <td style="" align="right">
                   <asp:Label id="lblLevel" runat="server" Text="Level" ></asp:Label>
                 </td>
                 <td style="" align="left">
                      
                     <asp:DropDownList ID="ddlLevel" runat="server" Width="130"  CssClass="dropDownList enableIsDirty">
                        <asp:ListItem Text="Reorder Level" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Safety Level" Value="2"></asp:ListItem>
                        <asp:ListItem Text="Lead Time" Value="3"></asp:ListItem>
                    </asp:DropDownList>
                     
                 </td>
                  <td style="" align="right">
                      </td>
                 <td style="" align="left">
                     </td>
                     
                     <td>
                     </td>
                 </tr>

                    <tr>
                 <td>
                   </td>
                 <td style="" align="right">
                   
                 </td>
                 <td style="" align="left">
                      
                    <asp:Button ID="btnSearch" runat="server"  CssClass="buttonRefresh" style="padding-left:22px;" Text="Load" onclick="btnSearch_Click" /> &nbsp;&nbsp;&nbsp;&nbsp;
                     <asp:Button ID="btnReOrderPreview" runat="server"  CssClass="buttoncommon buttonPrint" style="padding-left:22px;" Height="20px" Width="100px" Text="Preview" onclick="btnReOrderPreview_Click" />
                   
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
        <div id="dvGridHeader" style="height:25px;overflow:auto; font-size: smaller;" class="subHeader">
            <table style="height: 100%; color: #FFFFFF; font-weight: bold; text-align: center; width: 743px;" class="defFont" 
                cellspacing="1" cellpadding="1" rules="all" >
            <tr>
                 <td align="left" Width="40px">SL</td>
                <td align="left" Width="170px">Item</td>
                <td align="left" Width="90px">Group</td>
                <td align="left"  Width="90px">Class</td>
                <td align="left"  Width="70px">Re Order Lebel</td>
                <td align="left"  Width="70px">Safety Lebel</td>
                <td align="left"  Width="70px">Lead Time(days)</td>
                <td align="left"  Width="70px">Closing Quantity</td>
            </tr>
            </table>
        </div> 
        <div id="dvGrid" style="height: 280px; overflow:auto;">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            CellPadding="1" CellSpacing="1" ForeColor="#333333" GridLines="None" 
            Font-Names="Arial" Font-Size="9pt" 
            ShowHeader="False" Width="743px"  >
            <PagerSettings Mode="NumericFirstLast" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <Columns>

            <asp:TemplateField HeaderText="SL" HeaderStyle-HorizontalAlign="Left">
            <ItemTemplate>
                <%# Container.DataItemIndex + 1 %>
            </ItemTemplate>
            <HeaderStyle HorizontalAlign="Left" />
            <ItemStyle Width="40px" HorizontalAlign="Left"></ItemStyle>
            </asp:TemplateField>
                <asp:BoundField DataField="ITEM_NAME" HeaderText="Item" >
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle Width="170px" HorizontalAlign="Left" />
                </asp:BoundField>  
                 <asp:BoundField DataField="ITEM_GROUP_NAME" HeaderText="Group" >
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle Width="90px" HorizontalAlign="Left" />
                </asp:BoundField>  
                <asp:BoundField DataField="ITEM_CLASS_NAME" HeaderText="Class" >
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle Width="90px" HorizontalAlign="Left" />
                </asp:BoundField> 
                <asp:BoundField DataField="RE_ORDER_LEBEL" HeaderText="Re Order Lebel " >
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle Width="70px" HorizontalAlign="Left" />
                </asp:BoundField> 
                 <asp:BoundField DataField="SAFETY_STOCK" HeaderText="Safety Lebel" >
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle Width="70px" HorizontalAlign="Left" />
                </asp:BoundField> 
                 <asp:BoundField DataField="DELIVERY_LEAD_TIME" HeaderText="Lead Time(days)" >
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle Width="70px" HorizontalAlign="Left" />
                </asp:BoundField> 
                 <asp:BoundField DataField="CLS_QNTY" HeaderText="Closing Quantity " >
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle Width="70px" HorizontalAlign="Left" />
                </asp:BoundField> 
               
            </Columns>
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#999999" />
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
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

