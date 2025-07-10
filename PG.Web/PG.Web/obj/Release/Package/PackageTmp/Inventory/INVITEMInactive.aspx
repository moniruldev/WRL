<%@ Page Title="" Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="INVITEMInactive.aspx.cs" Inherits="PG.Web.Inventory.INVITEMInactive" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

   
      <%--<script language="javascript" type="text/javascript">
          // <!CDATA[

          var isPageResize = true;
          var gridViewID = '<%=GridView1.ClientID%>';
          var gridRowCountID = '<%= hdnRowCount.ClientID %>';

          var isGridScroll = false;

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


          function tbopen(key, userid) {
              if (!key) {
                  key = '';
              }

              var url = "Organization/VehicleUser.aspx?id=" + key
              //if (pageInTab == 1)
              if (ZForm.PageMode == Enums.PageMode.InTab) {

                  var tdata = new xtabdata();
                  tdata.linktype = Enums.LinkType.Direct;
                  tdata.id = 6310;
                  tdata.name = "VehicleUser";
                  //tdata.label = "User: " + userid;
                  tdata.label = "VehicleUser";
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
</script>--%>


    <%-- <script language="javascript" type="text/javascript">
         // <!CDATA[


         ContentForm.CalendarImageURL = "../../image/calendar.png";

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



         //function tbopen(key, userid) {
         //    key = key || '';

         //    var url = IForm.RootPath + "Organization/VehicleUser.aspx?id=" + key;
         //    //var url = IForm.RootPath + "Accounting/GeneralLedger/JournalFull.aspx?id=" + key;


         //    if (IForm.PageMode == Enums.PageMode.InTab) {

         //        var tdata = new xtabdata();
         //        tdata.linktype = Enums.LinkType.Direct;
         //        tdata.id = 0;
         //        tdata.name = "VehicleUser";
         //        //tdata.label = "User: " + userid;
         //        tdata.label = "VehicleUser";
         //        tdata.type = 0;
         //        tdata.url = url;
         //        tdata.tabaction = Enums.TabAction.InNewTab;
         //        tdata.selecttab = 1;
         //        tdata.reload = 0;
         //        tdata.param = "";


         //        try {
         //            window.parent.TabMenu.OpenMenuByData(tdata);
         //        }
         //        catch (err) {
         //            alert("error in page");
         //        }
         //    }
         //    else {
         //        //on new window/tab
         //        //window.open(url,'_blank');   

         //        window.location = url;
         //    }
         //}

         function fromParent(val1) {
             alert('this is called from parent: ' + val1);
         }




         // ]]>
    </script>--%>

<%-- <style type="text/css">
        .FixedHeader {
            /*position: absolute;*/
            font-weight: bold;
        }     
    </style> --%>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <div  id="dvPageContent" style="width:100%; height:100%;" >
 
  <div id="dvContentHeader" class="dvContentHeader">  
    <div id="dvHeader" class="dvHeader" align="center">
        <asp:Label ID="lblHeader" runat="server" Text="Item Modification" CssClass="lblHeader" Font-Bold="true" Font-Size="15px"></asp:Label>
    </div>
    <div id="dvMessage" class="dvMessage" >
        <asp:Label ID="lblMessage" runat="server" CssClass="lblMessage" Text="" Width="100%"></asp:Label>
    </div>
  
     <div id="dvHeaderControl" class="dvHeaderControl">
          
     </div>
    
  </div>  
  
  <div id="dvContentMain" class="dvContentMain" align="center"> 
       <div id="dvControlsHead" style="height:auto;width:100%;" align="center">
      <table style="" cellspacing="2" border="0" align="center">
            <tr>
              <td>
              </td>
              <td>
               <asp:Label ID="lblitemgroup" runat="server" Text="Item Group" style="height: 19px; width: 43px; " 
                Font-Bold="True" Visible="true"></asp:Label>
              </td>
              <td>
                <asp:DropDownList ID="ddlItemGroup" runat="server" CssClass="dropDownList" style="width: 166px;"  Visible="true">
                    <asp:ListItem Value="0">(---all---)</asp:ListItem>
                </asp:DropDownList>
              </td>
              <td>
              </td>
              <td> 
                <%-- <asp:Button ID="btnRefresh" runat="server"  CssClass="buttoncommon"
                    style="" Text="Refresh" OnClick="btnRefresh_Click" />--%>
              </td>
              <td>
                 <%--<input id="btnAddNew" type="button" runat="server" value="New Vehicle User" class="buttoncommon" style="width:110px" />--%> 
                     
              </td>
              <td>
                
              </td>
            </tr>
           <tr>
              <td>
              </td>
              <td>
               <asp:Label ID="lblItemType" runat="server" Text="Item Type" style="height: 19px; width: 43px;" Font-Bold="True" Visible="true"></asp:Label>
              </td>
              <td>
                <asp:DropDownList ID="ddlItemType" runat="server" CssClass="dropDownList" style="width: 166px;"  Visible="true">
                    <asp:ListItem Selected="True" Text="Foregin" Value="F"></asp:ListItem>
                    <asp:ListItem Text="Local" Value="L"></asp:ListItem>
                </asp:DropDownList>
              </td>
              <td>
              </td>
              <td> 
                 <asp:Button ID="btnLoad" runat="server"  CssClass="buttoncommon"
                    style="" Text="Load" OnClick="btnLoad_Click"  />
              </td>
              <td>
                 <%--<input id="btnAddNew" type="button" runat="server" value="New Vehicle User" class="buttoncommon" style="width:110px" />--%> 
                     
              </td>
              <td>
                
              </td>
            </tr>
            
         </table>        
     
        </div> 
  
    <div id = "dvControls" style="height:auto;width:100%;"> 
   <div id="dvControlsInner" class="groupBoxContainer boxShadow" style="height:auto;width:100%;">
   
        
  <div id="dvGridContainer"   style="width:100%; height:  100%;">
             
<%--<div id="dvGridHeader" style="width:100%;height:25px; font-size:larger;" class="subHeader">
            <table style="height: 100%;width:100%; color: #FFFFFF; font-weight: bold; text-align: center;" class="defFont" 
                cellspacing="1" cellpadding="1" rules="all" >
            <tr>
               
                <td width="100px" align="left">Cat_ID</td>--%>
                <%-- <td width="100px" align="left"> Category </td>--%>
                <%--<td width="100px" align="left">Cat_Sub_ID</td>--%>
               <%-- <td width="150px" align="left">Sub Category</td>--%>
         <%--       <td width="150px" align="left">ITEM_CODE</td>
                <td width="300px" align="left">ITEM Name</td>
                <td width="100px" align="left">ITEM_ID</td>
                <td width="100px" align="left">Check</td>
            </tr>
            </table>--%>
     <%--   </div> --%>
   
    <div id="dvGrid" style="width: 100%; height: 300px; overflow: auto;">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            CellPadding="1" CellSpacing="1" ForeColor="#333333" GridLines="None" 
            Font-Names="Arial" Font-Size="9pt" PageSize="15" 
            EmptyDataText="There is no record" 
            ShowHeader="true" Width="100%"  OnRowDataBound="GridView1_RowDataBound">
             <PagerSettings Mode="NumericFirstLast" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <Columns>
            <%--     <asp:BoundField DataField="CAT_ID" HeaderText="CAT_ID" ItemStyle-Width="100px" >
<ItemStyle Width="100px"></ItemStyle>
                </asp:BoundField>--%>

                <asp:TemplateField HeaderText="CAT_ID" Visible="false">
                  <ItemTemplate>
                 <asp:Label ID="lblCAT_ID" runat="server" Text='<%# Bind("CAT_ID") %>' Style="text-align: center;" Width="100px"></asp:Label>
                                                                   
                  </ItemTemplate>
                   <ItemStyle VerticalAlign="Top" />
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="Category" Visible="true" >
                  <ItemTemplate>
                 <asp:Label ID="lblCAT_DESC" runat="server" Text='<%# Bind("CATEGORY_DESC") %>' Style="text-align: center;" Width="100px"></asp:Label>
                                                                   
                  </ItemTemplate>
                   <ItemStyle VerticalAlign="Top" />
                </asp:TemplateField>
               <%-- <asp:BoundField DataField="CATEGORY_DESC"  HeaderText=" CATEGORY " ItemStyle-Width="100px" >
<ItemStyle Width="100px"></ItemStyle>
                </asp:BoundField>--%>
            <%--    <asp:BoundField DataField="CAT_SUB_ID" HeaderText=" Cat_Sub_ID " ItemStyle-Width="100px" >
<ItemStyle Width="100px"></ItemStyle>
                </asp:BoundField>--%>
                <asp:TemplateField HeaderText="CAT_SUB_ID" Visible="false">
                  <ItemTemplate>
                 <asp:Label ID="lblCAT_SUB_ID" runat="server" Text='<%# Bind("CAT_SUB_ID") %>' Style="text-align: center;" Width="100px"></asp:Label>
                                                                   
                  </ItemTemplate>
                   <ItemStyle VerticalAlign="Top" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Sub Category" Visible="true">
                  <ItemTemplate>
                 <asp:Label ID="lblSubCAT_DESC" runat="server" Text='<%# Bind("CAT_SUB_DESC") %>' Style="text-align: center;" Width="100px"></asp:Label>
                                                                   
                  </ItemTemplate>
                   <ItemStyle VerticalAlign="Top" />
                </asp:TemplateField>
                 <%-- <asp:BoundField DataField="CAT_SUB_DESC" HeaderText=" Sub Category " ItemStyle-Width="150px" >
<ItemStyle Width="150px"></ItemStyle>
                </asp:BoundField>--%>
      <%--           <asp:BoundField DataField="ITEM_CODE" HeaderText=" ITEM_CODE " ItemStyle-Width="150px" >
<ItemStyle Width="150px"></ItemStyle>
                </asp:BoundField>--%>
                <asp:TemplateField HeaderText="ITEM_CODE" Visible="false">
                  <ItemTemplate>
                 <asp:Label ID="lblITEM_CODE" runat="server" Text='<%# Bind("ITEM_CODE") %>' Style="text-align: center;" Width="150px"></asp:Label>
                                                                   
                  </ItemTemplate>
                   <ItemStyle VerticalAlign="Top" />
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="ITEM GROUP" Visible="true">
                  <ItemTemplate>
                 <asp:Label ID="lblITGROUP" runat="server" Text='<%# Bind("ITEM_GROUP_DESC") %>' Style="text-align: center;" Width="150px"></asp:Label>
                                                                   
                  </ItemTemplate>
                   <ItemStyle VerticalAlign="Top" />
                </asp:TemplateField>
                 <%--           <asp:BoundField DataField="ITEM_DESC" HeaderText=" ITEM Name " ItemStyle-Width="300px" >
<ItemStyle Width="300px"></ItemStyle>
                </asp:BoundField>--%>

                <asp:TemplateField HeaderText="ITEM Name" Visible="true">
                  <ItemTemplate>
                 <asp:Label ID="lblITEM_DESC" runat="server" Text='<%# Bind("ITEM_DESC") %>' Style="text-align: center;" Width="300px"></asp:Label>
                                                                   
                  </ItemTemplate>
                   <ItemStyle VerticalAlign="Top" />
                </asp:TemplateField>
<%--                              <asp:BoundField DataField="ITEM_ID" HeaderText=" ITEM_ID " ItemStyle-Width="100px" >
<ItemStyle Width="100px"></ItemStyle>
                </asp:BoundField>--%>
                 <asp:TemplateField HeaderText="Closing Stock" Visible="true">
                  <ItemTemplate>
                 <asp:Label ID="lblT_STOCK" runat="server" Text='<%# Bind("T_STOCK") %>' Style="text-align: center;" Width="100px"></asp:Label>
                                                                   
                  </ItemTemplate>
                   <ItemStyle VerticalAlign="Top" />
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="MSR Unit" Visible="true">
                  <ItemTemplate>
                 <asp:Label ID="lblMSR_NAME" runat="server" Text='<%# Bind("MSR_NAME") %>' Style="text-align: center;" Width="100px"></asp:Label>
                                                                   
                  </ItemTemplate>
                   <ItemStyle VerticalAlign="Top" />
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="ITEM_ID" Visible="false">
                  <ItemTemplate>
                 <asp:Label ID="lblITEM_ID" runat="server" Text='<%# Bind("ITEM_ID") %>' Style="text-align: center;" Width="100px"></asp:Label>
                                                                   
                  </ItemTemplate>
                   <ItemStyle VerticalAlign="Top" />
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="IsVisible">
                <ItemTemplate >
                    <asp:CheckBox ID="chk" runat="server"   />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center"  />
                <ItemStyle BorderColor="Gray" HorizontalAlign="Center" Width="100px"  />
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
   
<%--     <div id="dvGridFooter" style="width:100%;height:25px; font-size: smaller;" class="subFooter">
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
        </div>--%> 
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
    
  <div id="dvContentFooter" class="dvContentFooter">
    <table align="center">
			  <tr>
				<td>
				</td>
				<td>
				   <asp:Button ID="btnAddNew" runat="server" Text="New" CssClass="buttonNew"  />
					<asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonCancel checkIsDirty"  />
				</td>
				<td>
				  <asp:Button ID="btnSave" runat="server" Text="Update" CssClass="buttonSave" OnClick="btnSave_Click"  />
					
				</td>
				<td>
				 <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="buttonDelete" />
				</td>
				
				<td>
				   <asp:Button ID="btnRefresh" runat="server" Text="Refresh" CssClass="buttonRefresh checkIsDirty"  />
				   </td>

			   
				<td>
				   
				 </td>
				

				 <td>
					<input id="btnClose" type="button" runat="server" class="buttonClose" value="Close" onclick="if (ContentForm) { ContentForm.CloseForm(); }" />
				</td>


			  </tr>
		   </table> 
    </div> 


 </div>   
    
</asp:Content>
