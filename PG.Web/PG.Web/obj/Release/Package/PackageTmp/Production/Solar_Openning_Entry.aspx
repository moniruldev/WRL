<%@ Page Title="" Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="Solar_Openning_Entry.aspx.cs" Inherits="PG.Web.Production.Solar_Openning_Entry" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../javascript/jquery.ui.combogrid.js" type="text/javascript"></script>
    <script src="../javascript/jquery.attributeobserver.js" type="text/javascript"></script>
    <link href="../css/jquery.ui.combogrid.css" rel="stylesheet" type="text/css" />
     <script language="javascript" type="text/javascript">
         var isPageResize = true;
         ContentForm.CalendarImageURL = "../image/calendar.png";

         var hdnCompanyID = '<%=hdnCompanyID.ClientID%>';
         var ddlDEPT_ID = '<%=ddlDEPT_ID.ClientID%>';
       <%--  var ItemGroupListServiceLink = '<%=this.ItemGroupListServiceLink%>';
         var ItemListServiceLink = '<%=this.ItemListServiceLink%>';
         var BOMItemListServiceLink = '<%=this.BOMItemListServiceLink%>';--%>
         var BOMNameListServiceLink = '<%=this.BOMItemNameListServiceLink%>';
       <%--  var ItemListServiceLinkd = '<%=this.Get_LinkBatch_WiseItem_List%>';

         var BOMListServiceLink = '<%= this.BOMListServiceLink%>';--%>
         var gridUpdatePanelIDDet = '<%=UpdatePanel1.ClientID%>';
         var gridViewIDDet = '<%=GRDDTLITEMLIST.ClientID%>';
         var updateProgressID = '<%=UpdateProgress2.ClientID%>';

         var txtOPENNING_DATE =  '<%=txtOPENNING_DATE.ClientID%>';
       <%--  var txtTotalItem_qty = '<%=txtTotalItem_qty.ClientID%>';
         var txtTotalITEM_WEIGHT = '<%=txtTotalITEM_WEIGHT.ClientID%>';
         var txtTotalMRB_PLATE_QTY = '<%=txtTotalMRB_PLATE_QTY.ClientID%>';
         var txtTotalMRB_PLATE_WEIGHT = '<%=txtTotalMRB_PLATE_WEIGHT.ClientID%>';
         var txtTotalSCRAP_BATTERY_WEIGHT = '<%=txtTotalSCRAP_BATTERY_WEIGHT.ClientID%>';--%>

         function Calc(texbx) {

             var SCRAP_BATTERY_WEIGHT = 0;
             var detRow = $(texbx).closest('tr.gridRow');
             var txtItem_qty = $(detRow).find('input[id$="txtItem_qty"]').val();
             var txtITEM_WEIGHT = $(detRow).find('input[id$="txtITEM_WEIGHT"]').val();
             var txtMRB_PLATE_QTY = $(detRow).find('input[id$="txtMRB_PLATE_QTY"]').val();
             var txtMRB_PLATE_WEIGHT = $(detRow).find('input[id$="txtMRB_PLATE_WEIGHT"]').val();
             SCRAP_BATTERY_WEIGHT = Number(txtITEM_WEIGHT) - Number(txtMRB_PLATE_WEIGHT);
             //alert(totalusedqty);
             $(detRow).find('input[id$="txtSCRAP_BATTERY_WEIGHT"]').val(SCRAP_BATTERY_WEIGHT);
         }

         function calcTotalCharged(texbx)
         {
             var detRow = $(texbx).closest('tr.gridRow');
             var txtC_PACKED = $(detRow).find('input[id$="txtC_PACKED"]').val();
             var txtC_UNPACKED = $(detRow).find('input[id$="txtC_UNPACKED"]').val();
             var txtTOTAL_CHARGE_STOCK = Number(txtC_PACKED) + Number(txtC_UNPACKED);
             $(detRow).find('input[id$="txtTOTAL_CHARGE_STOCK"]').val(txtTOTAL_CHARGE_STOCK);
             var txtOP_ON_CHARGED = $(detRow).find('input[id$="txtOP_ON_CHARGED"]').val();
             var txtTOTAL_UN_CHARGE_STOCK = $(detRow).find('input[id$="txtTOTAL_UN_CHARGE_STOCK"]').val();
             var txtTOTAL_OPENNING = Number(txtTOTAL_CHARGE_STOCK) + Number(txtTOTAL_UN_CHARGE_STOCK) + Number(txtOP_ON_CHARGED);
             $(detRow).find('input[id$="txtTOTAL_OPENNING"]').val(txtTOTAL_OPENNING);
         }

         function calcTotalUnCharged(texbx) {
             var detRow = $(texbx).closest('tr.gridRow');
             var txtUN_GREEN = $(detRow).find('input[id$="txtUN_GREEN"]').val();
             var txtUN_FORMED = $(detRow).find('input[id$="txtUN_FORMED"]').val();
             var txtTOTAL_UN_CHARGE_STOCK = Number(txtUN_GREEN) + Number(txtUN_FORMED);
              
             $(detRow).find('input[id$="txtTOTAL_UN_CHARGE_STOCK"]').val(txtTOTAL_UN_CHARGE_STOCK);
             var txtOP_ON_CHARGED = $(detRow).find('input[id$="txtOP_ON_CHARGED"]').val();
             var txtTOTAL_CHARGE_STOCK = $(detRow).find('input[id$="txtTOTAL_CHARGE_STOCK"]').val();
             var txtTOTAL_OPENNING = Number(txtTOTAL_CHARGE_STOCK) + Number(txtTOTAL_UN_CHARGE_STOCK) + Number(txtOP_ON_CHARGED);
             $(detRow).find('input[id$="txtTOTAL_OPENNING"]').val(txtTOTAL_OPENNING);
         }


         $(document).on('keyup', '.txtQty', function () {
             sumGrandQty();
         });

         function sumGrandQty() {

             var totAdded1 = GetTotalSumbqQty();
             $("#" + txtTotalItem_qty).val(JSUtility.FormatCurrency(totAdded1));

             var totAdded2 = GetTotalSumbwQty();
             $("#" + txtTotalITEM_WEIGHT).val(JSUtility.FormatCurrency(totAdded2));

             var totAdded3 = GetTotalSumpqQty();
             $("#" + txtTotalMRB_PLATE_QTY).val(JSUtility.FormatCurrency(totAdded3));

             var totAdded4 = GetTotalSumpwQty();
             $("#" + txtTotalMRB_PLATE_WEIGHT).val(JSUtility.FormatCurrency(totAdded4));

             var totAdded5 = GetTotalSumswQty();
             $("#" + txtTotalSCRAP_BATTERY_WEIGHT).val(JSUtility.FormatCurrency(totAdded5));

         }

         function GetTotalSumbqQty() {
             debugger;
             var totAdd = 0;
             $(document).find('.txtQtybq').each(function (index, elem) {
                       var addQty = parseFloat(JSUtility.GetNumber($(elem).val()));
                 if (!isNaN(addQty)) {
                     totAdd += addQty;
                 }
             });
             return totAdd;
         }

         function GetTotalSumbwQty() {
             debugger;
             var totAdd = 0;
             $(document).find('.txtQtybw').each(function (index, elem) {
                 var addQty = parseFloat(JSUtility.GetNumber($(elem).val()));
                 if (!isNaN(addQty)) {
                     totAdd += addQty;
                 }
             });
             return totAdd;
         }

         function GetTotalSumpqQty() {
             debugger;
             var totAdd = 0;
             $(document).find('.txtQtypq').each(function (index, elem) {
                 var addQty = parseFloat(JSUtility.GetNumber($(elem).val()));
                 if (!isNaN(addQty)) {
                     totAdd += addQty;
                 }
             });
             return totAdd;
         }

         function GetTotalSumpwQty() {
             debugger;
             var totAdd = 0;
             $(document).find('.txtQtypw').each(function (index, elem) {
                 var addQty = parseFloat(JSUtility.GetNumber($(elem).val()));
                 if (!isNaN(addQty)) {
                     totAdd += addQty;
                 }
             });
             return totAdd;
         }
         function GetTotalSumswQty() {
             debugger;
             var totAdd1 = 0;
             var totAdd2 = 0;
             $(document).find('.txtQtypw').each(function (index, elem) {
                 var addQty1 = parseFloat(JSUtility.GetNumber($(elem).val()));
                 if (!isNaN(addQty1)) {
                     totAdd1 += Number(addQty1);
                 }
             });

             $(document).find('.txtQtybw').each(function (index, elem) {
                 var addQty2 = parseFloat(JSUtility.GetNumber($(elem).val()));
                 if (!isNaN(addQty2)) {
                     totAdd2 += Number(addQty2);
                 }
             });

             return Number(totAdd2) - Number(totAdd1);
         }

         

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

         function validationPackingQty(txtbox) {

             var detRow = $(txtbox).closest('tr.gridRow');
             var batchno = $('#' + txtPROD_BATCH_NO).val();
             var inputitemqty = $(detRow).find('input[id$="txtItem_qty"]').val();
             var packing_batchNo = $(detRow).find('input[id$="txtPACK_FINISHED_BATCH"]').val();
             var balAssembleQty = $(detRow).find('input[id$="hdnBALPACKINGQTY"]').val();
             if (batchno != packing_batchNo) {
                 if (inputitemqty > balAssembleQty) {
                     alert("Please enter valid packing quantity!! ")
                     $(detRow).find('input[id$="txtItem_qty"]').val('0');
                 }

             }

         }

         function ShowProgress() {
             $('#' + updateProgressID).show();
         }

         function ShowPackProgress() {
             $('#' + updatePackProgressID).show();
         }
         function ShowClosingProgress() {
             $('#' + updateClosingProgressID).show();
         }

         function UserDeleteConfirmation() {
             return confirm("Are you sure you want to Save ?");
         }
         function UserSaveConfirmation() {
             return confirm("Are you sure you want to Save?");
         }

         function UserAuthorizeConfirmation() {
             return confirm("Are you sure you want to Authorized?");
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

         function GetTotalSumAddedQty() {
             debugger;
             var totAdd = 0;
             var totalAmount = 0;
             var itemTotalWt = 0;


             $(document).find('.txtQty').each(function (index, elem) {
                 // var weight = $(elem).closest('tr').find('.txtPcQty').val();             

                 var addQty = parseFloat(JSUtility.GetNumber($(elem).val()));
                 if (!isNaN(addQty)) {
                     totAdd += addQty;

                     //   itemTotalWt = itemTotalWt + (weight * addQty);
                 }
             });
             //$("#" + txtPanelWeight).val(JSUtility.FormatCurrency(itemTotalWt));
             return totAdd;
         }


         function tbopen(key, isPrint, isPDFAutoPrint, showWait) {
             key = key || '';
             isPrint = isPrint || false;
             showWait = showWait || true;

             if (isPrint) {
                 if (key != '') {
                     ReportPrint(key, isPDFAutoPrint);
                     return;
                 }
             }

             //var url = "/Report/ReportView.aspx?rk=" + key

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



         $(document).ready(function () {
             var pageInstance = Sys.WebForms.PageRequestManager.getInstance();
             pageInstance.add_pageLoaded(function (sender, args) {
                 var panels = args.get_panelsUpdated();
                 for (i = 0; i < panels.length; i++) {
                     if (panels[i].id == gridUpdatePanelIDDet) {
                         bindItemList(gridViewIDDet);
                     }
                 }
             });
             bindItemList(gridViewIDDet);
         });


         
        

         function bindItemList(gridViewID) {
             var cgColumns = [
                                { 'columnName': 'itemname', 'width': '160', 'align': 'left', 'highlight': 4, 'label': 'Battery Name' }
                              , { 'columnName': 'uomname', 'width': '70', 'align': 'left', 'highlight': 4, 'label': 'Uom' }
             ];
             var serviceURL = BOMNameListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
             serviceURL += "&ispaging=1&deptid=54&isFinished=Y&for_production=Y";
             var gridSelector = "#" + gridViewID;
             $(gridSelector).find('input[id$="txtITEM_NAME"]').each(function (index, elem) {
                 ///list click
                 var elemRow = $(elem).closest('tr.gridRow');

                 var hdnItemIDElem = $(elemRow).find('input[id$="txtITEM_NAME"]');

                 $(elem).closest('tr').find('input[id$="btnITEM_NAME"]').click(function (e) {
                     elmID = $(elem).attr('id');
                     $(elem).combogrid("dropdownClick");
                 });

                 $(elem).combogrid({
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
                     width: 350,
                     url: serviceURL,
                     search: function (event, ui) {
                         var elemRowCur = $(elem).closest('tr.gridRow');
                         var vgroupid = $(elemRowCur).find('input[id$="hdngroupId"]').val();
                         var vfdate = $('#' + txtOPENNING_DATE).val();
                         var newServiceURL = serviceURL + "&fdate=" + vfdate;//+ "&snd_transfer=Y&groupid=" + vgroupid + "&deptid=140"
                         newServiceURL = JSUtility.AddTimeToQueryString(newServiceURL);
                         $(this).combogrid("option", "url", newServiceURL);
                     },

                     select: function (event, ui) {
                         elemID = $(elem).attr('id');
                         if (!ui.item) {
                             event.preventDefault();
                             ClearItemData(elemID);
                             return false;
                         }
                         if (ui.item.id == 0) {
                             event.preventDefault();
                             return false;
                         }
                         else {
                             SetItemData(elemID, ui.item);
                         }
                         return false;
                     }
                     // lc: ''
                 });

                 $(elem).blur(function () {
                     var self = this;
                     elemID = $(elem).attr('id');
                     eCode = $(elem).val();
                     isComboGridOpen = $(self).combogrid('isOpened');
                     if (eCode == '') {
                         ClearItemData(elemID);
                     }
                     else {
                         var vfdate = $('#' + txtOPENNING_DATE).val();
                         var serviceURL = BOMNameListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
                         serviceURL += "&ispaging=1&fdate=" + vfdate;
                         var prcNo = GetItemNo(eCode, serviceURL);
                         if (prcNo == null) {
                             ClearItemData(elemID);
                         }
                         else {
                             SetItemData(elemID, grp);
                         }

                     }
                 });

             });

         }

         function GetItemNo(eCode, serviceURL) {
             var prcNo = null;
             var isError = false;
             var isComplete = false;
             //ajax call

             var newServiceURL = serviceURL + " &selectedId=" + eCode;
             var dummyVar = $.ajax({
                 type: "GET",
                 cache: false,
                 async: false,
                 dataType: "json",
                 url: newServiceURL,

                 success: function (prddata) {
                 
                     if (prddata.rows.length > 0) {
                         prcNo = prddata.rows[0];
                     }
                 },
                 complete: function () {
                     if (!isError) {
                        
                     }
                     isComplete = true;
                 },
                 error: function (XMLHttpRequest, textStatus, errorThrown) {
                     isError = true;
                     alert(textStatus);
                 }
             });
             return prcNo;
         }


         function ClearItemData(txtItemID) {
             //$('#' + txtGLAccCodeID).val('');
             var detRow = $('#' + txtItemID).closest('tr.gridRow');
             $(detRow).find('input[id$="hdnItemId"]').val('0');
             $(detRow).find('input[id$="txtITEM_NAME"]').val('');
             $(detRow).find('input[id$="hdnUomID"]').val('0');
             $(detRow).find('input[id$="txtUOM_NAME"]').val('');

         }
         function SetItemData(txtItemCodeID, data) {
             $('#' + txtItemCodeID).val(data.itemid);

             var detRow = $('#' + txtItemCodeID).closest('tr.gridRow');
             $(detRow).find('input[id$="hdnItemID"]').val(data.itemid);
             $(detRow).find('input[id$="txtITEM_NAME"]').val(data.itemname);
            
             $(detRow).find('input[id$="txtOPENNING_QTY"]').val(data.closing_qty);

             //$(detRow).find('input[id$="hdnUomID"]').val(data.uomid);
             //$(detRow).find('input[id$="txtUOM_NAME"]').val(data.uomname);

         }
           </script>


    <style type="text/css">
        .auto-style1 {
            width: 171px;
        }
        .auto-style2 {
            width: 209px;
        }
        .auto-style3 {
            width: 85px;
        }
        .auto-style4 {
            width: 122px;
        }
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div id="dvPageContent" style="width: 100%; height: 100%;">
         <asp:HiddenField ID="hdnLoggedInUser" runat="server" />
         <asp:HiddenField ID="hdnCompanyID" runat="server" Value="0" />
          <div id="dvContentHeader" class="dvContentHeader">
            <div id="dvHeader" class="dvHeader_Prod" align="left">
                <asp:Label ID="lblHeader" runat="server" Text="Solar Openning Entry" CssClass="lblHeader" Font-Bold="true" Font-Size="15px"></asp:Label>
            </div>
            <div id="dvMessage" class="dvMessage">
                <asp:Label ID="lblMessage" runat="server" CssClass="lblMessage" Text="" Width="100%"></asp:Label>
            </div>

            <div id="dvHeaderControl" class="dvHeaderControl">
            </div>

        </div>

           <div id="dvContentMain" class="dvContentMain" align="left">

                 <div id="dvControlsHead" style="height: auto; width: 100%;" align="left">
                      <table border="0" cellspacing="4" cellpadding="2" align="left" style="width: 90%" id="tblProductionMstEntry">
                          
                          
                          <tr>
                           
                              <td style="text-align:right;" class="auto-style4"> <asp:Label ID="lblPRODUCTION_DATE" runat="server"  Text="Openning Date :"></asp:Label> </td>
                               <td class="auto-style1" >
                                   <asp:TextBox ID="txtOPENNING_DATE" runat="server" CssClass="textBox textDate dateParse" Style="text-align: left;" Width="100px"></asp:TextBox>
                               </td>
                              <td style="text-align:right;" class="auto-style3">
                                  <asp:Label ID="lblDEPT_ID" runat="server" Text="Department : "></asp:Label>
                              </td>
                              <td class="auto-style2">
                              <asp:DropDownList ID="ddlDEPT_ID" runat="server" CssClass="dropDownList required" Width="220px" ></asp:DropDownList> 
                              </td>
                              <td style="text-align:right;">
                                 
                              </td>
                              <td></td>
                              <td></td>
                              <td><asp:HiddenField ID="hdnOPEN_MST_ID" runat="server" /></td>
                          </tr>
                          
                          </table>
                     </div>

               <div id="dvControls" style="height: auto; width: 100%; ">
 
            <div id="groupDataDetails" style="width: 100%; height: auto;">
                <table style="width : 1200px;">
                    <tr >
                        
                        <td style="border-right: solid 2px #C0C0C0;">
                                <div id="dvGridContainer2" runat="server" class="" style="width: auto; height: auto; text-align: left; float : left; padding-right : 5px; border: solid 1px #2e4be5;">
                                                    <div id="groupDataHeaderCredit" runat="server" class="" style="width: 100%; text-align: left;">
                                                    <span style="font-weight: bold;font-size : 15px; color :#ff3b00;">Items Details: </span>
                                                </div>
                                                    <div id="dvGridHeader2" style=" width:1200px; height: 35px; font-size: smaller;" class="subHeader_Prod">
                                                        <table style="height: 80%; color: #FFFFFF; font-weight: bold; text-align: left;"
                                                            class="defFont" cellspacing="1" cellpadding="1">

                                                            <tr class="headerRow_Prod">
                                                                <td width="30px" class="headerColCenter_prod">SL#
                                                                </td>
                                                                <td width="155px" class="headerColCenter_prod">Battery Type
                                                                </td>
                                                                 <td width="15px" class="headerColCenter_prod"></td>
                                                                  <td width="90px" class="headerColCenter_prod">Total Openning Qty
                                                                </td>
                                                                 <td width="90px" class="headerColCenter_prod">Charged Packed
                                                                </td>
                                                                <td width="90px" class="headerColCenter_prod">Charged <br /> Unpacked
                                                                </td>
                                                                <%-- <td width="15px" class="headerColCenter_prod"></td>--%>
                                                                  <td width="90px" class="headerColCenter_prod"> Total Charged Stock
                                                                </td>
                                                                 <td width="90px" class="headerColCenter_prod">Op. On Charging
                                                                </td>
                                                                <td width="90px" class="headerColCenter_prod">Uncharged <br />Green Plate
                                                                </td>
                                                                <td width="90px" class="headerColCenter_prod">Uncharged <br /> Formed Plate
                                                                </td>
                                                                <td width="100" class="headerColCenter_prod"> Total <br />Uncharged Stock
                                                                </td>
                                                                <td width="100px" class="headerColCenter_prod"> Total </td>
                                                                <td width="16px" class="headerColCenter_prod">Delete
                                                                </td>
                                
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <div id="dvGrid" style="width: 1200px; height: 300px; overflow: auto;" class="dvGrid">
                                                      <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                                <asp:GridView ID="GRDDTLITEMLIST" runat="server" AutoGenerateColumns="False" ShowHeader="False" 
                                                                    CellPadding="1" CellSpacing="1" ForeColor="#333333" GridLines="Both" DataKeyNames="ITEM_ID" 
                                                                    EnableModelValidation="True" ClientIDMode="AutoID" OnRowCommand="GRDDTLITEMLIST_RowCommand" OnRowCreated="GRDDTLITEMLIST_RowCreated" OnRowDataBound="GRDDTLITEMLIST_RowDataBound" OnRowDeleting="GRDDTLITEMLIST_RowDeleting"  >
                                                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                                    <Columns>
                                                                        <asp:TemplateField  HeaderText="SL#">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblSLNo" runat="server" Text=' <%#Container.DataItemIndex+1 %>' Style="text-align: center;"
                                                                                    Width="30px">
                                                                                </asp:Label>

                                                                            </ItemTemplate>
                                                                            <ItemStyle VerticalAlign="Top" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText=" Item Type" HeaderStyle-HorizontalAlign="Left">
                                                                            <ItemTemplate>
                                                                                <div>
                                                                                    <table border="0" cellpadding="1" cellspacing="1">
                                                                                        <tbody>
                                                                                            <tr>
                                                                                                 
                                                                                        <td>
                                                                                            <asp:HiddenField ID="hdnOPENING_ID" runat="server" Value='<%# Bind("OPENING_ID") %>' />
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="txtITEM_NAME" runat="server" CssClass="textBox textAutoSelect" Width="150px" Text='<%# Bind("ITEM_NAME") %>'></asp:TextBox>
                                                                                            <asp:HiddenField ID="hdnItemID" runat="server" Value='<%# Bind("ITEM_ID") %>' />
                                                                                        </td>
                                                                                        <td>
                                                                                            <input id="btnITEM_NAME" type="button" value="" runat="server" class="buttonDropdown" tabindex="-1" />
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="txtOPENNING_QTY" runat="server" CssClass="textBox textAutoSelect txtQty txtQtybq" style="text-align : right;" Width="88px"  align="right" Text='<%# Bind("OPENNING_QTY") %>'  onkeypress="return isNumberKey(event,this);"></asp:TextBox>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="txtC_PACKED" runat="server" CssClass="textBox textAutoSelect txtQty txtQtybw"  onchange="calcTotalCharged(this)" style="text-align : right; background-color : yellow;" Width="88px" align="right" Text='<%# Bind("C_PACKED") %>'    onkeypress="return isNumberKey(event,this);"></asp:TextBox>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="txtC_UNPACKED" runat="server" CssClass="textBox textAutoSelect txtQty txtQtypq"  onchange="calcTotalCharged(this)" Width="88px" style="text-align : right;  background-color : yellow;" align="right" Text='<%# Bind("C_UNPACKED") %>'  onkeypress="return isNumberKey(event,this);"></asp:TextBox>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="txtTOTAL_CHARGE_STOCK" runat="server" CssClass="textBox textAutoSelect txtQty txtQtypw" Width="88px" style="text-align : right;" align="right" Text='<%# Bind("TOTAL_CHARGE_STOCK") %>'  onkeypress="return isNumberKey(event,this);"></asp:TextBox>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="txtOP_ON_CHARGED" runat="server" CssClass="textBox textAutoSelect txtQtysw" onchange="calcTotalUnCharged(this)" Width="86px" style="text-align : right;  background-color : aqua;" align="right" Text='<%# Bind("OP_ON_CHARGED") %>'  onkeypress="return isNumberKey(event,this);"></asp:TextBox>
                                                                                        </td>
                                                                                         <td>
                                                                                            <asp:TextBox ID="txtUN_GREEN" runat="server" CssClass="textBox textAutoSelect txtQtysw" onchange="calcTotalUnCharged(this)"  Width="88px" style="text-align : right;  background-color : yellow;" align="right" Text='<%# Bind("UN_GREEN") %>'  onkeypress="return isNumberKey(event,this);"></asp:TextBox>
                                                                                        </td>
                                                                                         <td>
                                                                                            <asp:TextBox ID="txtUN_FORMED" runat="server" CssClass="textBox textAutoSelect txtQtysw" onchange="calcTotalUnCharged(this)" Width="87px" style="text-align : right;  background-color : yellow;" align="right" Text='<%# Bind("UN_FORMED") %>'  onkeypress="return isNumberKey(event,this);"></asp:TextBox>
                                                                                        </td>
                                                                                                
                                                                                         <td>
                                                                                            <asp:TextBox ID="txtTOTAL_UN_CHARGE_STOCK" runat="server" CssClass="textBox textAutoSelect txtQtysw" Width="98px" style="text-align : right;" align="right" Text='<%# Bind("TOTAL_UN_CHARGE_STOCK") %>'  onkeypress="return isNumberKey(event,this);"></asp:TextBox>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="txtTOTAL_OPENNING" runat="server" CssClass="textBox textAutoSelect" Width="100px" Text='<%# Bind("TOTAL_OPENNING") %>' Style="" onkeypress="return isNumberKey(event,this);"></asp:TextBox>
                                                                                        </td>

                                                                                            </tr>
                                                                                        </tbody>
                                                                                    </table>
                                                                                </div>
                                                                                <div style="overflow: visible;">
                                                                                </div>
                                                                            </ItemTemplate>
                                                                            <ItemStyle Width="10" />
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Delete" ShowHeader="false">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="btnDeleteRow" CssClass="buttonDeleteIcon" Height="16px" Width="18px"
                                                                                    CommandName="delete" runat="server">
                                                                                </asp:LinkButton>
                                                                            </ItemTemplate>
                                                                            <ItemStyle VerticalAlign="Top" />
                                                                        </asp:TemplateField>

                                        

                                                                        <asp:TemplateField Visible="false">
                                                                            <ItemTemplate>
                                                                                <div style="width: 10px;">
                                                                                    <div>
                                                                                        <div style="background-position: right center; height: 25px; cursor: pointer; background-image: url('../image/more.png'); background-repeat: no-repeat; text-align: left; vertical-align: middle;"
                                                                                            onclick="togglePannelStatus(this)"
                                                                                            title="More..">
                                                                                            ...
                                                                                        </div>
                                                                                        <div style="display: none;">
                                                                                            <div class="gridPanel" style="float: right; width: 0px; height: 0px;">
                                                                                                <div style="position: relative; height: 100%; width: 100%;">
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Font-Size="Smaller" />
                                                                    <EditRowStyle BackColor="#999999" />
                                                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                                </asp:GridView>
                                                                <input id="hdnJournalDetRefJson2" type="hidden" runat="server" value="[]" />
                                                                <input id="hdnJournalDetInsJson2" type="hidden" runat="server" value="[]" />
                                                       </ContentTemplate>
                                                         <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="btnNewRow" EventName="Click" />
                                                            </Triggers>
                                                    </asp:UpdatePanel>


                                                    </div>
                                                    <div id="divGridControls2" style="width: auto; height: 30px; border-top: solid 1px #C0C0C0;">
                                                        <table style="width: 100%; height: 30px; text-align: center;" cellspacing="1" cellpadding="1"
                                                            border="0">
                                                            <tr>
                                            <td style="width: 2px"></td>
                              
                                            <td style="width: 160px; text-align: right;">
                                               <%-- <asp:Label ID="lbl" runat="server" Text="Total :"></asp:Label>--%>
                                                                             </td>
                                            <td align="right"style=" width: 100px; text-align: right;">
                                     <%--         <asp:TextBox ID="txtTotalItem_qty" runat="server" Width="80px" Text="0" CssClass="textBox textBoxReadOnly"></asp:TextBox>--%>
                                            </td>
                                            <td style="width: 80px; text-align: right;">
                                                <%-- <asp:TextBox ID="txtTotalITEM_WEIGHT" runat="server" Width="80px" Text="0" CssClass="textBox textBoxReadOnly"></asp:TextBox>--%>
                                                                             </td>
                                             <td  style="width:250px; text-align: right;" align="right">
                                               <%--  <asp:TextBox ID="txtTotalMRB_PLATE_QTY" runat="server" Width="90px" Text="0" CssClass="textBox textBoxReadOnly"></asp:TextBox>--%>
                                             </td>
                                            <td align="right" style="width: 100px">
                                                <%-- <asp:TextBox ID="txtTotalMRB_PLATE_WEIGHT" runat="server" Width="80px" Text="0" CssClass="textBox textBoxReadOnly"></asp:TextBox>--%>
                                                &nbsp;</td>
                                            <td align="right" style="width: 90px">
                                                 <%--<asp:TextBox ID="txtTotalSCRAP_BATTERY_WEIGHT" runat="server" Width="80px" Text="0" CssClass="textBox textBoxReadOnly"></asp:TextBox>--%>
                                            </td>
                                                                
                                                            <%--    </tr>
                                                            <tr>--%>
                                                                <td style="width: 200px"></td>
                                                                  <td style="width: 90px" align="left">
                                                                    <asp:Button ID="btnNewRow" runat="server" CssClass="buttonNewRow" Text="" OnClientClick="ShowProgress()" OnClick="btnNewRow_Click" />
                                                                </td>
                                                                <td style="width: 20px;">
                                                                    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                                                                        DisplayAfter="300">
                                                                        <ProgressTemplate>
                                                                            <asp:Image ID="imgProgress2" runat="server" ImageUrl="~/image/loader.gif" />
                                                                        </ProgressTemplate>
                                                                    </asp:UpdateProgress>
                                                                </td>
                                                               
                                                                <td style="width: 160px; text-align: right;">
                                                                </td>
                                                                <%--<td style="width: 160px; text-align: right;">
                                                                <asp:Label ID="lbl" runat="server" Text="Total :"></asp:Label>
                                                                  </td>
                                                                 <td align="right" style="width: 80px; text-align: right;">
                                                                  <asp:TextBox ID="txtTotalPanelQty" runat="server" Width="80px" Text="0" CssClass="textBoxReadOnly"></asp:TextBox>
                                                                </td>--%>
                                                                <td align="right">
                                                                    &nbsp;</td>
                                                                <td align="right">
                                                                    &nbsp;</td>
                                                                <td align="right">&nbsp;
                                                                </td>
                                                               
                                                            </tr>
                            
                                
                                                        </table>
                                                    </div>
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
                            <asp:Button ID="btnAddNew" runat="server" Text="New" CssClass="buttonNew" OnClick="btnAddNew_Click"  />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonCancel checkIsDirty" />
                        </td>
                        <td>
                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonSave checkRequired" AccessKey="s" OnClientClick="if ( ! UserDeleteConfirmation())  return false;" OnClick="btnSave_Click" />
                            <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="buttonEdit" OnClick="btnEdit_Click" />
                        </td>
                        <td>
                            <asp:Button ID="btnAuthorize" runat="server" Text="Authorize" CssClass="buttonAthorize" OnClientClick="if ( ! UserAuthorizeConfirmation()) return false;" OnClick="btnAuthorize_Click" />
                        </td>
                        <td>
                            <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="buttonDelete" />
                        </td>
                        <td>
                            <asp:Button ID="btnRefresh" runat="server" Text="Refresh" CssClass="buttonRefresh checkIsDirty" />
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
                        </td>
                    </tr>
                </table>
            </div>

            </div>

        
</asp:Content>