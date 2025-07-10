<%@ Page Title="" Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="RM_ForcastEntry_FM_Wise.aspx.cs"  Inherits="PG.Web.Production.RM_ForcastEntry_FM_Wise" %>

 
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script src="../javascript/jquery.ui.combogrid.js" type="text/javascript"></script>
    <script src="../javascript/jquery.attributeobserver.js" type="text/javascript"></script>
    <link href="../css/jquery.ui.combogrid.css" rel="stylesheet" type="text/css" />

     <script language="javascript" type="text/javascript">

         var ItemBomWiseListServiceLink = '<%=this.ItemBomWiseListServiceLink%>';
         var BOMListServiceLink = '<%= this.BOMListServiceLink%>';
         var hdnFFC_ID = '<%=this.hdnFFC_ID.ClientID%>';
         var txtFN_ITEM_NAME ='<%=txtFN_ITEM_NAME.ClientID%>';
         var btnFN_ITEM_NAME = '<%=btnFN_ITEM_NAME.ClientID%>';
         var txtForcastMonth = '<%=txtForcastMonth.ClientID%>';
         var hdnFN_ITEM_ID = '<%=hdnFN_ITEM_ID.ClientID%>';
         var hdnFN_UOM_ID = '<%=hdnFN_UOM_ID.ClientID%>';

         var txtBOM_NAME = '<%=txtBOM_NAME.ClientID%>';
         var hdnFN_BOM_ID = '<%=hdnFN_BOM_ID.ClientID%>';
         var btnBOM_NAME = '<%=btnBOM_NAME.ClientID%>';
         var txtFN_FC_QTY = '<%=txtFN_FC_QTY.ClientID%>';

         var isPageResize = true;
         ContentForm.CalendarImageURL = "../image/calendar.png";
         function isNumberKey(evt, obj) {

             var charCode = (evt.which) ? evt.which : event.keyCode
             var value = obj.value;
             var dotcontains = value.indexOf(".") != -1;
             if (dotcontains)
                 if (charCode == 46) return false;
             if (charCode == 46) return true;
             if (charCode > 31 && (charCode < 48 || charCode > 57))
                 return false;
             return true;
         }




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
             if ($('#' + txtFN_ITEM_NAME).is(':visible')) {
                 bindBOMItemList();
                 
             }

           
             $(".datepicker").datepicker({
                 dateFormat: 'dd-M-yy',
                 showOn: "button",
                 buttonImage: "../image/calendar.png",
                 buttonImageOnly: true,
                 showButtonPanel: true,
                 onSelect: function (input) {
                     setTimeout(function () {
                        // $("[id*=Button1]").click();
                     }
                   
                     , 1);
                 }
             });

             if ($('#' + txtBOM_NAME).is(':visible')) {
                 
                 bindBOMList();
             }
         });
         //this is for group dropdown
         function bindBOMItemList() {

             var cgColumns = [
                                //{ 'columnName': 'ffcNo', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'FG Forecast' } ,
                                { 'columnName': 'itemname', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Item Name' }
                                //, { 'columnName': 'itemid', 'width': '70', 'align': 'left', 'highlight': 4, 'label': 'Item ID' }
                               // , { 'columnName': 'uomname', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'UOM Name' }
                                //, { 'columnName': 'uomid', 'width': '50', 'align': 'left', 'highlight': 4,  'label': 'UOM ID' }
                                //, { 'columnName': 'fcid', 'width': '50', 'align': 'left', 'highlight': 4, 'label': 'FG FC ID' }
                                , { 'columnName': 'bomno', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'BOM_NO' }
                                //, { 'columnName': 'bomid', 'width': '60', 'align': 'left', 'highlight': 4, 'label': 'BOM ID' }
                               // , { 'columnName': 'bomitemid', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'BOM_ITEM_ID' }
                                 , { 'columnName': 'itemfcqty', 'width': '50', 'align': 'left', 'highlight': 4, 'label': 'QTY' }
             ];
             var serviceURL = ItemBomWiseListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
             serviceURL += "&ispaging=1";
             var fnItemElem = $('#' + txtFN_ITEM_NAME);
             
             $('#' + btnFN_ITEM_NAME).click(function (e) {

                 $(fnItemElem).combogrid("dropdownClick");
                


             });

             $(fnItemElem).combogrid({
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
                 url: serviceURL,
                 search: function (event, ui) {
                     var ffc_id = $('#' + hdnFFC_ID).val();
                     var ffc_month = $('#' + txtForcastMonth).val();
                     if (ffc_month == '' && ffc_id=='')
                     {
                         alert(" Please Select Month-Year !!! ");
                         return false;
                     }
                     else
                     {
                         var newServiceURL = serviceURL + "&ffc_id=" + ffc_id + "&ffc_month=" + ffc_month;
                         $(this).combogrid("option", "url", newServiceURL);
                     }
                    
                 },
                 select: function (event, ui) {
                     if (!ui.item) {
                         event.preventDefault();

                         // $('#' + hdnDealerID).val('0');
                         //$('#' + txtDealerID).val('');
                         return false;
                         //ClearGLAccountData(elemID);
                     }

                     if (ui.item.dealerid == '') {
                         event.preventDefault();
                         return false;
                         //ClearGLAccountData(elemID);
                     }
                     else {
                         $('#' + txtFN_ITEM_NAME).val(ui.item.itemname);
                         $('#' + hdnFN_ITEM_ID).val(ui.item.itemid);
                         $('#' + txtBOM_NAME).val(ui.item.bomno);
                         $('#' + hdnFN_BOM_ID).val(ui.item.bomid);
                         $('#' + txtFN_FC_QTY).val(ui.item.itemfcqty);
                         $("[id*=btnRMFC]").click();
                     }
                     return false;
                 },

                 lc: ''
             });


             $(fnItemElem).blur(function () {
                 var self = this;

                 var groupID = $(fnItemElem).val();
                 if (groupID == '') {

                     $('#' + txtFN_ITEM_NAME).val('');
                     $('#' + hdnFN_ITEM_ID).val(0);
                     $('#' + txtBOM_NAME).val('');
                     $('#' + hdnFN_BOM_ID).val(0);
                     $('#' + txtFN_FC_QTY).val('0');
                 }
             });
         }
         function bindBOMList() {
             var cgColumns = [{ 'columnName': 'bomid', 'width': '50', 'align': 'left', 'highlight': 4, 'label': 'BOM ID' }
                              , { 'columnName': 'bomitemdesc', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'BOM Name' }
                              , { 'columnName': 'bomitemid', 'width': '50', 'align': 'left', 'highlight': 4, 'label': 'Item ID' }
                              , { 'columnName': 'itemname', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Item Name' }
                              , { 'columnName': 'bomno', 'width': '200', 'align': 'left', 'highlight': 4, 'label': 'BOM NO' }
                              , { 'columnName': 'bomver', 'width': '50', 'align': 'left', 'highlight': 4, 'label': 'BOM Ver' }


             ];
             
             var serviceURL = BOMListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
             serviceURL += "&ispaging=1";
             
             var fnBomElem = $('#' + txtBOM_NAME);
             
             $('#' + btnBOM_NAME).click(function (e) {
                 $(fnBomElem).combogrid("dropdownClick");
             });
             $(fnBomElem).combogrid({
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
                     width: 600,
                     url: serviceURL,
                     search: function (event, ui) {
                         //var elemRowCur = $(elem).closest('tr.gridRow');
                         //var ItemID = $(elemRowCur).find('input[id$="hdnItemID"]').val();
                         //var vgroupid = $('#' + hdngroupId).val();+ "&groupid=" + vgroupid

                         var ItemID = $('#' + hdnFN_ITEM_ID).val();
                         var newServiceURL = serviceURL + "&itemid=" + ItemID;
                         //var newServiceURL = serviceURL + "&companycode=" + companyCode + "&branchcode=" + branchCode + "&deptcode=" + deptCode
                         newServiceURL = JSUtility.AddTimeToQueryString(newServiceURL);
                         $(this).combogrid("option", "url", newServiceURL);


                     },

                     select: function (event, ui) {
                         //alert(ui.item.typename);
                         //$(".txtComboGrid").val(ui.item.code);
                         //elemID = $(elem).attr('id');
                         //                    if (!validateGLAccount(elemID, ui.item)) {
                         //                        $(elem).val(prevGLCode);
                         //                        return false;
                         //                    }
                         if (!ui.item) {
                             event.preventDefault();
                              
                             return false;
                             //ClearGLAccountData(elemID);
                         }



                         if (ui.item.id == 0) {
                             event.preventDefault();
                             return false;
                             //ClearGLAccountData(elemID);
                         }
                         else {
                             $('#' + txtBOM_NAME).val(ui.item.bomno);
                             $('#' + hdnFN_BOM_ID).val(ui.item.bomid);
                         }
                         // setDetInstrument(hdnIsInstrumentElem, txtInstrumentElem, btnInstrumentElem);
                         return false;
                     },


                      lc: ''
                 });

             $(fnBomElem).blur(function () {
                     var self = this;
                     var bomname = $(fnBomElem).val();
                     if (bomname == '') {
                         $('#' + txtBOM_NAME).val('');
                         $('#' + hdnFN_BOM_ID).val('0');
                     }
             });

         }
         </script>
     <style type="text/css">
         .auto-style1 {
             width: 100%;
             height: 101px;
         }
     </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div id="dvPageContent" style="width: 100%; height: 100%;">
         <asp:HiddenField ID="hdnLoggedInUser" runat="server" />
         <asp:HiddenField ID="hdnCompanyID" runat="server" Value="0" />

             <div id="dvContentHeader" class="dvContentHeader">
                <div id="dvHeader" class="dvHeader_Prod" align="left">
                    <asp:Label ID="lblHeader" runat="server" Text="Raw Material forecast Entry" CssClass="lblHeader" Font-Bold="true" Font-Size="15px"></asp:Label>
                </div>
                <div id="dvMessage" class="dvMessage">
                    <asp:Label ID="lblMessage" runat="server" CssClass="lblMessage" Text="" Width="100%"></asp:Label>
                </div>
                <div id="dvHeaderControl" class="dvHeaderControl">
                </div>

            </div>

          <div id="dvContentMain" class="dvContentMain">
                <div id="dvControlsHead" style="height: auto; width: 100%;">
                    <table>
                       <tr>
                           <td class="auto-style1">
                                 <table style="" border="0" cellspacing="2" cellpadding="1">
                                    <tr>

                                        <td align="right" class="auto-style2">
                                            <asp:Label ID="lbl" runat="server" Text="Forecast NO :" ></asp:Label>
                                        </td>
                                        <td class="auto-style2">
                                            <asp:TextBox ID="txtRFC_NO" runat="server"   Style="text-align: left;"  CssClass="colourdisabletextBox" Width="200px" Enabled="False" ></asp:TextBox>
                                        </td>
                                        <td align="right" class="auto-style2">
                                            <asp:Label ID="Label6" runat="server" Text="Forecast Desc :" ></asp:Label>
                                        </td>
                                        <td class="auto-style2" colspan="3">
                                            <asp:TextBox ID="txtFC_DESC" runat="server" CssClass="textBox" Style="text-align: left;" Width="355px" autofocus></asp:TextBox>
                                        </td>
                                         
                                    </tr>           
                                    <tr>
                                        <td align="right" class="auto-style2">
                                            <asp:Label ID="Label8" runat="server" Text="Month :" ></asp:Label>
                                        </td>
                                        <td class="auto-style2">
                                            <asp:TextBox ID="txtForcastMonth" runat="server" CssClass="textBox datepicker dateParse" Style="text-align: left;" Width="73px" ></asp:TextBox>
                                        </td>
                                        <td style="text-align: right">
                                            <%--<asp:Label ID="lblFinishedFC_NO" runat="server" Text="Finished Forecust NO :"></asp:Label>--%>
                                             <asp:Label ID="lblFFCQty" runat="server" Text="Forecast Qty : "></asp:Label>
                                        </td>
                                        <td style="text-align: left">
                                            <%--  <asp:TextBox ID="txtFFC_NO" runat="server" CssClass="colourdisabletextBox" ReadOnly="true" Enable="false"></asp:TextBox>
                                             <asp:DropDownList ID="ddlFN_FC_NO" runat="server" CssClass="dropDownList" Width="180px">
                        </asp:DropDownList>--%>
                            

                                             <asp:TextBox ID="txtFN_FC_QTY" runat ="server" CssClass="colourdisabletextBox"></asp:TextBox>
                            

                                        </td>
                                        <td style="text-align: right">
                                            <asp:HiddenField ID ="hdnAUTH_STATUS" runat="server" />
                                            <asp:CheckBox ID="chkAUTH_STATUS" runat="server" Text="  Authorize  " Checked="True" TextAlign="right" > </asp:CheckBox>
                                           </td>
                                        <td>
                                            &nbsp;</td>
                                       
                                    </tr>
                                     <tr>
                                         <td style="text-align: right">
                                           <asp:Label ID="lblFN_ITEM_NAME" runat="server" Text="Finished Iitem :"></asp:Label>

                            

                                         </td>
                                         <td>
                                            <asp:TextBox ID="txtFN_ITEM_NAME" runat="server" CssClass="textBox textAutoSelect" Width="200px"></asp:TextBox>
                                            <input id="btnFN_ITEM_NAME" type="button" value="" runat="server" class="buttonDropdown" style="width: 15px" tabindex="-1" />
                                             <asp:Button ID="btnRMFC" runat="server" Text="Button" style = "display:none" OnClick = "btnRMFC_Click" />
                                         </td>
                                         <td style="text-align: right">
                                             <asp:Label ID="lblFFC_BOMName" runat ="server" Text="BOM Name :"></asp:Label>
                                         </td>
                                         <td>
                                              <asp:TextBox ID="txtBOM_NAME" runat="server" CssClass="textBox textAutoSelect" Width="150px"></asp:TextBox>
                                              <input id="btnBOM_NAME" type="button" value="" runat="server" class="buttonDropdown" style="width: 15px" tabindex="-1" /></td>
                                         <td style="text-align: right">
                                                <asp:Button ID="btnUpload" CssClass="buttonSearch"  Style="padding-left: 22px;"  Text="Show Data" runat="server" OnClick="btnUpload_Click"   />
                                         </td>
                                         <td> <asp:Button ID="Button1" runat="server" Text="Button" style = "display:none" OnClick = "Button1_Click" />
                                             &nbsp;</td>
                                     </tr>
                                    
                                     <tr>
                                         <td> <asp:HiddenField ID="hdnFN_ITEM_ID" runat="server"   />   </td>
                                         <td> <asp:HiddenField ID="hdnFN_UOM_ID" runat="server"   />  </td>
                                         <td><asp:HiddenField ID="hdnFFC_ID" runat="server" /></td>
                                         <td><asp:HiddenField ID="hdnRFC_ID"  runat="server" /></td>
                                         <td> <asp:HiddenField ID="hdnFN_BOM_ID"  runat="server" /></td>
                                         <td></td>
                                     </tr>
                                 </table>
                            </td>
                           </tr>
                    </table>
                    </div>



                          
          <div id="dvContentMain1" class="dvContentMain">
               <div id="Div1" runat="server" style="width: 100%;">
                   <span style="font-weight: bold;font-size : 15px; color :#ff3b00;">Required Plate Details: </span>
                </div>
              <div class="groupBoxContainer boxShadow" style="width:40%; height : 120px;">
                    <asp:GridView ID="grdRMDTL" runat="server" AutoGenerateColumns="False" 
            CellPadding="1" CellSpacing="1" ForeColor="#333333" GridLines="None" 
            Font-Names="Arial" Font-Size="9pt" PageSize="15" Width="100px"
            EmptyDataText="There is no record" 
            ShowHeader="True"   >
             <PagerSettings Mode="NumericFirstLast" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <Columns>
                 <asp:TemplateField HeaderText="Item Name" Visible="true">
                  <ItemTemplate>
                 <asp:Label ID="lblRM_ITEM_NAME" runat="server" Text='<%# Bind("RM_ITEM_NAME") %>' Style="text-align: left;" Width="250px"></asp:Label>
                                                                
                  </ItemTemplate>
                   <ItemStyle VerticalAlign="Top" />
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="UOM" Visible="true">
                  <ItemTemplate>
                 <asp:Label ID="lblRM_ITEM_NAME" runat="server" Text='<%# Bind("RM_UOM_NAME") %>' Style="text-align: left;" Width="150px"></asp:Label>
                  </ItemTemplate>
                   <ItemStyle VerticalAlign="Top" />
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Req Qty" Visible="true">
                  <ItemTemplate>
                 <asp:Label ID="lblRM_ITEM_NAME" runat="server" Text='<%# Bind("RM_FC_QTY") %>' Style="text-align: left;" Width="150px"></asp:Label>
                                                             
                  </ItemTemplate>
                   <ItemStyle VerticalAlign="Top" />
                </asp:TemplateField>
                </Columns>
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle  BackColor="#ED63B9"  Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#999999" />
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        </asp:GridView>
                    
              </div>
              <div id="Div2" runat="server" style="width: 100%;">
                </div>

              <div id="Div3" runat="server" style="width: 100%;">
                   <span style="font-weight: bold;font-size : 15px; color :#ff3b00;">Required RM Details: </span>
                </div>
             <div id="dvControls" style="width: 1000px;">
                <div id="dvControlsInner" class="groupBoxContainer boxShadow" style="width: 900px">
                    <div id="dvGridHeader2" style="width: 850px; height: 25px; font-size: smaller;" class="subHeader_Prod">
                                            <table style="height: 100%; color: #FFFFFF; font-weight: bold; text-align: center;"
                                                class="defFont" cellspacing="1" cellpadding="1">
                                                <tr class="headerRow_Prod">
                                                    <%-- <td width="220px" class="headerColCenter"> RM BOM Name
                                                    </td>--%>
                                                    <td width="225px" class="headerColCenter_prod">Item Name
                                                    </td>
                                                    <td width="60px" class="headerColCenter_prod">UOM 
                                                    </td>
                                                     <td width="100px" class="headerColCenter_prod">RM Qty
                                                    </td>
                                                    <td width="53px" class="headerColCenter_prod">Wastage (%)
                                                    </td>
                                                    <td width="90px" class="headerColCenter_prod">Req. Qty
                                                    </td>
                                                    <td width="120px" class="headerColCenter_prod"> Qty (Wastage)
                                                    </td>
                                                     <td width="200px" class="headerColCenter_prod">Remarks
                                                    </td>

                                                </tr>
                                            </table>
                                        </div>
                    <div id="dvGrid" style="width: 950px; height: 350px; overflow: auto;">
        <asp:GridView ID="grdRMForcast" runat="server" AutoGenerateColumns="False" 
            CellPadding="1" CellSpacing="1" ForeColor="#333333" GridLines="None" 
            Font-Names="Arial" Font-Size="9pt" PageSize="15" 
            EmptyDataText="There is no record" 
            ShowHeader="False" Width="800px"  >
             <PagerSettings Mode="NumericFirstLast" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <Columns>
                <asp:TemplateField HeaderText="Finished Item Name" Visible="true">
                  <ItemTemplate>
                                        
                  </ItemTemplate>
                   <ItemStyle VerticalAlign="Top" />
                </asp:TemplateField>
             <%--    <asp:TemplateField HeaderText="Finishing Forecast Qty" Visible="true">
                  <ItemTemplate>
                        <asp:Label ID="lblFN_FC_QTY" runat="server" Text='<%# Bind("FN_FC_QTY") %>' Style="text-align: left;" Width="45px"></asp:Label>
                  </ItemTemplate>
                   <ItemStyle VerticalAlign="Top" />
                </asp:TemplateField>

               

                <asp:TemplateField HeaderText="RM BOM Name" Visible="true">
                  <ItemTemplate>
                 <asp:Label ID="lblRMBOM_Name" runat="server" Text='<%# Bind("RM_BOM_Name") %>' Style="text-align: left;" Width="220px"></asp:Label>
                         <asp:HiddenField ID="hdnRMBOM_ID" runat="server" Value='<%# Bind("RM_BOM_ID") %>' />       
                       <asp:HiddenField ID="hdnRMBOM_NO" runat="server" Value='<%# Bind("RM_BOM_NO") %>' />                                         
                  </ItemTemplate>
                   <ItemStyle VerticalAlign="Top" />
                </asp:TemplateField>--%>


                 <asp:TemplateField HeaderText="RM Item Name" Visible="true">
                  <ItemTemplate>
                 <asp:Label ID="lblRM_ITEM_NAME" runat="server" Text='<%# Bind("RM_ITEM_NAME") %>' Style="text-align: left;" Width="220px"></asp:Label>
                           <asp:HiddenField ID="hdnRM_ITEM_ID" runat="server" Value ='<%# Bind("RM_ITEM_ID") %>' />                                       
                  </ItemTemplate>
                   <ItemStyle VerticalAlign="Top" />
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="UOM" Visible="true">
                      <ItemTemplate>
                            <asp:Label ID="lblRM_UOM_NAME" runat="server" Text='<%# Bind("RM_UOM_NAME") %>' Style="text-align: left;" Width="60px"></asp:Label>
                          <asp:HiddenField ID="hdnRM_UOM_ID" runat="server" Value ='<%# Bind("RM_UOM_ID") %>' />
                      </ItemTemplate>
                       <ItemStyle VerticalAlign="Top" />
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="BOM Qty" Visible="true">
                  <ItemTemplate>
                        <asp:Label ID="lblRMBOM_QTY" runat="server" Text='<%# Bind("RM_BOM_QTY") %>' Style="text-align: right;"   Width="100px"></asp:Label>
                  </ItemTemplate>
                   <ItemStyle VerticalAlign="Top" />
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="Wastage (%)" Visible="true">
                  <ItemTemplate>
                        <asp:Label ID="lblWASTAGE_PERCENT" runat="server" Text='<%# Bind("BOM_WASTAGE_PERCENT") %>' Style="text-align: right;"   Width="53px"></asp:Label>
                  </ItemTemplate>
                   <ItemStyle VerticalAlign="Top" />
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="Forecast Qty" Visible="true">
                  <ItemTemplate>
                      <asp:Label ID="lblRM_FC_QTY" runat="server" Text='<%# Bind("RM_FC_QTY") %>' CssClass=" textBox textNumberOnly"  onkeypress="return isNumberKey(event,this);" Width="85px"></asp:Label>                                                 
                  </ItemTemplate>
                   <ItemStyle VerticalAlign="Top" />
                </asp:TemplateField>

                  <asp:TemplateField HeaderText="Forecast Qty (With Wastage)" Visible="true" >
                  <ItemTemplate>
                      <asp:Label ID="lblRM_FC_QTY_WASTAGE" runat="server" Text='<%# Bind("RM_FC_QTY_WASTAGE") %>' CssClass=" textBox textNumberOnly"  onkeypress="return isNumberKey(event,this);" Width="115px"></asp:Label>                                                 
                  </ItemTemplate>
                   <ItemStyle VerticalAlign="Top" />
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="Remarks" Visible="true">
                  <ItemTemplate>
                      <asp:TextBox ID="txtREMARKS" runat="server" Text='<%# Bind("RM_REMARKS") %>' CssClass=" textBox"    Width="195px"></asp:TextBox>                                                 
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
                </div>
            </div>

             <div id="dvGridFooter" style="width: 100%; height: 25px; font-size: smaller;" class="subFooter">
                            <table style="height: 100%; width: 100%; font-weight: bold;" cellspacing="2" cellpadding="1"
                                rules="all">
                                <tr>
                                    <td align="left" style="width: 150px">
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
                                    <td align="left" style="width: 400px">
                                        <div id="dvGridPager" class="dvGridPager">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="btnGridPageGoTo" runat="server" Text="Go"  />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label4" runat="server" Text="Page Size:"></asp:Label>
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
                                                        <asp:Label ID="Label5" runat="server" Text="Page:"></asp:Label>
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
         <div id="dvContentFooter" class="dvContentFooter">
            <table>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="btnAddNew" runat="server" Text="New" CssClass="buttonNew" OnClick="btnAddNew_Click" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonCancel checkIsDirty" OnClick="btnCancel_Click" />
                            </td>
                            <td>
                                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonSave checkRequired" AccessKey="s" OnClick="btnSave_Click" OnClientClick="if ( ! UserDeleteConfirmation()) return false;"  />
                                <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="buttonEdit" OnClick="btnEdit_Click" />
                            </td>
                    
                  
                            <td>
                                <asp:Button ID="btnRefresh" runat="server" Text="Refresh" CssClass="buttonRefresh checkIsDirty" OnClick="btnRefresh_Click" />
                            </td>
                            <td>
                     
                            </td>
                            <td>
                                <input id="btnClose" type="button" runat="server" class="buttonClose" value="Close" onclick="if (ContentForm) { ContentForm.CloseForm(); }" />
                            </td>
                            <td></td>
                            <td>
                                
                            </td>
                            <td>
                                
                            </td>
                            <td>
                                <asp:Button ID="btnPopupTrigger" runat="server" Text="Button" CssClass="buttonHidden" />
                                <asp:HiddenField ID="hdnPopupTriggerID" runat="server" Value="" />
                                <asp:HiddenField ID="hdnPopupCommand" runat="server" Value="" />
                            </td>
                        </tr>
                    </table>
    </div> 
             
        </div>
</asp:Content>
