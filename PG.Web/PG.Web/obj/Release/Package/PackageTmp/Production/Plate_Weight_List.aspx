<%@ Page Title="" Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" ViewStateMode="Disabled"  CodeBehind="Plate_Weight_List.aspx.cs" Inherits="PG.Web.Production.Plate_Weight_List" %>
<%@ Register assembly="DevExpress.Web.v14.2, Version=14.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <script src="../javascript/jquery.ui.combogrid.js" type="text/javascript"></script>
 <script src="../javascript/jquery.attributeobserver.js" type="text/javascript"></script>
 <link href="../css/jquery.ui.combogrid.css" rel="stylesheet" type="text/css" />

 <script language="javascript" type="text/javascript">

     var ItemListServiceLink = '<%=this.ItemListServiceLink%>';
     var ddlDeptID = '<%=ddlDeptID.ClientID%>';
     var txtItemName = '<%=txtItemName.ClientID%>';
     var btnItemID = '<%= btnItemID.ClientID%>';
     var hdnItemID = '<%=hdnItemID.ClientID %>';
     var hdnDeptID = '<%=hdnDeptID.ClientID %>';
   

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

         if ($('#' + txtItemName).is(':visible')) {

             bindItemList();
         }
         $('#' + txtGridPageNo).keydown(function (e) {
             if (e.keyCode == 13) {
                 e.preventDefault();
                 $('#' + btnGridPageGoTo).click();
             }
         });
     });

     function bindItemList() {

         var cgColumns = [
                           { 'columnName': 'itemname', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                          , { 'columnName': 'itemcode', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Code' }
                          , { 'columnName': 'itemgroupdesc', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Group Name' }
                          , { 'columnName': 'uomname', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Uom Name' }
                           , { 'columnName': 'class_name', 'width': '120', 'align': 'left', 'highlight': 4, 'label': 'Class Name' }
                          , { 'columnName': 'item_type', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Item Type' }
         ];

         var itemServiceURL = ItemListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;

         itemServiceURL += "&ispaging=1";
         var itemIDElem = $('#' + txtItemName);

         $('#' + btnItemID).click(function (e) {
             $(itemIDElem).combogrid("dropdownClick");
         });

         $(itemIDElem).combogrid({
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
             width: 750,
             url: itemServiceURL,
             search: function (event, ui) {

                 var vdeptid = $('#' + ddlDeptID).val();
                 var newServiceURL = itemServiceURL + "&deptid=" + vdeptid;
                 $(this).combogrid("option", "url", newServiceURL);


             },
             select: function (event, ui) {
                 if (!ui.item) {
                     event.preventDefault();


                     $('#' + hdnItemID).val('0');
                     return false;
                     //ClearGLAccountData(elemID);
                 }


                 if (ui.item.itemid == '') {
                     event.preventDefault();
                     return false;
                     //ClearGLAccountData(elemID);
                 }
                 else {

                     $('#' + hdnItemID).val(ui.item.itemid);
                     $('#' + txtItemName).val(ui.item.itemname);



                 }
                 return false;
             },

             lc: ''
         });


         $(itemIDElem).blur(function () {
             var self = this;
             var groupID = $(itemIDElem).val();
             if (groupID == '') {
                 // $('#' + hdnDealerID).val('0');
                 $('#' + txtItemName).val('');
                 //$('#' + txtGroupCode).val('');
                 $('#' + hdnItemID).val('0');

             }
         });
     }

     function tbopen(key, userid) {
         key = key || '';

         var url = IForm.RootPath + "Production/Plate_Weight_Entry.aspx?id=" + key;
         //var url = IForm.RootPath + "Accounting/GeneralLedger/JournalFull.aspx?id=" + key;


         if (IForm.PageMode == Enums.PageMode.InTab) {

             var tdata = new xtabdata();
             tdata.linktype = Enums.LinkType.Direct;
             tdata.id = 0;
             tdata.name = "Plate Weight Entry";
             //tdata.label = "User: " + userid;
             tdata.label = "Plate Weight Entry";
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

     <style type="text/css">
         .auto-style1 {
             height: 20px;
         }
     </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="dvPageContent" style="width: 100%; height: auto;">
          <div id="dvContentHeader" class="dvContentHeader">
                <div id="dvHeader" class="dvHeader_Prod">
                    <asp:Label ID="lblHeader" CssClass="lblHeader" runat="server" Text="Plate Weight List"></asp:Label>
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
                
                 <table style="width : 650px">
                     <tr>

                         <td style="text-align:right;">
                            <asp:Label ID="lblDeptID" runat="server" Text="Department : "></asp:Label>
                        </td>
                        <td>
                        <asp:DropDownList ID="ddlDeptID" runat="server" CssClass="dropDownList required" Width="205px" ViewStateMode="Enabled"></asp:DropDownList>
                        </td>
                        <td><asp:HiddenField ID="hdnDeptID" runat="server" Value ="0" /></td>
                      
                    </tr>

                      <tr>
                        
                           <td  style="text-align:right;">
                            <asp:Label ID="lblItemID" runat="server" Text="Item: "></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtItemName" runat="server" CssClass="textBox textAutoSelect" Width="202px" Height="20px"></asp:TextBox>
                            <input id="btnItemID" type="button" value="" runat="server" class="buttonDropdown" style="width: 15px " tabindex="-1" />
                            
                        </td>
                        <td>
                            <asp:HiddenField ID="hdnItemID" runat="server" />
                            
                        </td>
                         <td></td>
                         </tr>
                          <tr>
                             <td></td>
                              <td></td>
                             <td><asp:HiddenField ID="hdnLoggedInUser" runat="server" Value ="0" /></td>
                         </tr>

                    <tr>
                        <td></td>
                            <td>
                                
                                 <asp:Button ID="btnShow" runat="server" CssClass="buttonSearch" Style="padding-left: 22px;"
                                Text="Show Data" OnClick="btnShow_Click"  /> 
                                <asp:Button ID="btnAddNew" runat="server" CssClass=" buttonNew" Style="padding-left: 22px;"
                                Text="New Entry" OnClick="btnAddNew_Click"  />
                                  
                            </td>
                       
                    </tr>
                         </table>
                      
                    </div>


             <div id="dvControls" style="width: 100%; height : 600px;">
                <div id="dvControlsInner" class="groupBoxContainer boxShadow" style="width: 1000px; height : auto;">
                 
                        <div id="dvGrid" style="width: 100%; height: auto; overflow: auto;">
                            <dx:ASPxGridView ID="grdPlateWeightList" runat="server" AutoGenerateColumns="False" Width="98%"  ClientInstanceName="grdPlateWeightList" >
                                <Columns>
                                      <dx:GridViewDataTextColumn Caption="Action" UnboundType="String"  Width="50px" VisibleIndex="0">
                                        <DataItemTemplate>
                                            <dx:ASPxHyperLink ID="hyperLink"  runat="server" OnInit="hyperLink_Init">
                                            </dx:ASPxHyperLink>
                                        </DataItemTemplate>
                                    </dx:GridViewDataTextColumn>
                                   
                                    <dx:GridViewDataTextColumn Caption="Panel Grid Weight" Name="lblGridWeightPanel" Width="80px" VisibleIndex="3" FieldName="GRID_PANEL_GM">
                                    </dx:GridViewDataTextColumn>

                                     <dx:GridViewDataTextColumn Caption="Panel Paste Weight" Name="lblPasteWeightPanel" Width="80px" VisibleIndex="4" FieldName="PASTE_PANEL_GM">
                                    </dx:GridViewDataTextColumn>

                                    <dx:GridViewDataTextColumn Caption="Panel Quantity" Name="lblPanelQuantity" VisibleIndex="5" Width="100px" FieldName="PANEL_PC">
                                    </dx:GridViewDataTextColumn>

                                    <dx:GridViewDataTextColumn Caption="Total Panel (gm)" Name="lblTotalPanelWeight" VisibleIndex="6" Width="100px" FieldName="GRID_PASTE_PANEL_GM">
                                    </dx:GridViewDataTextColumn>

                                    <dx:GridViewDataTextColumn Caption="Total Pcs (gm)" Name="lblTotalPcsWeight" VisibleIndex="7" Width="100px" FieldName="GRID_PASTE_PC">
                                    </dx:GridViewDataTextColumn>
                                    
                                    <dx:GridViewDataTextColumn Caption="Item Name" Name="lblItemName" VisibleIndex="2" Width="100px" FieldName="ITEM_NAME">
                                    </dx:GridViewDataTextColumn>

                                    <dx:GridViewDataTextColumn Caption="ITEM_ID" FieldName="ITEM_ID" Name="hdnItemID" Visible="false" VisibleIndex="8">
                                    </dx:GridViewDataTextColumn>
                                   
                                    <dx:GridViewDataTextColumn Caption="ID" FieldName="ID" Name="hdnID" Visible="false" VisibleIndex="1">
                                    </dx:GridViewDataTextColumn>
                                  
                                </Columns>
                                <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
                                <SettingsSearchPanel Visible="True"  />
                                <SettingsPager AlwaysShowPager="True" PageSize="20">
                                </SettingsPager>
                                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="400"    />
                                <Styles>
                                    <Header CssClass="headerRow_Prod" HorizontalAlign="Center" Font-Bold="True" ForeColor="White" Font-Size="9pt">
                                         
                                    </Header>
                                    <AlternatingRow BackColor="#FFFFCC">
                                    </AlternatingRow>
                                </Styles>
                            </dx:ASPxGridView>


                       
                        </div>
                 

                </div>
            </div>
             </div>
        </div>

</asp:Content>
