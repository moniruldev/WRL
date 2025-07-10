<%@ Page Title="" Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true"  CodeBehind="ProductionForcastEntry_V1.aspx.cs" Inherits="PG.Web.Production.ProductionForcastEntry_V1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
 <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="../javascript/jquery.ui.combogrid.js" type="text/javascript"></script>
    <script src="../javascript/jquery.attributeobserver.js" type="text/javascript"></script>
    <link href="../css/jquery.ui.combogrid.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        var isPageResize = true;
        ContentForm.CalendarImageURL = "../image/calendar.png";

        var ItemGroupListServiceLink = '<%=this.ItemGroupListServiceLink%>';
        var BOMNameListServiceLink = '<%=this.BOMItemNameListServiceLink%>';
        var gridUpdatePanelIDDet = '<%=UpdatePanel1.ClientID%>';
        var gridViewIDDet = '<%=GRDDTLITEMLIST.ClientID%>';
        var updateProgressID = '<%=UpdateProgress2.ClientID%>';
        var ItemListServiceLink = '<%=this.ItemListServiceLink%>';

        

        function onlyNos(e, t) {
            try {
                if (window.event) {
                    var charCode = window.event.keyCode;
                }
                else if (e) {
                    var charCode = e.which;
                }
                else { return true; }
                if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                    return false;
                }
                return true;
            }
            catch (err) {
                alert(err.Description);
            }
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
        //this is for group dropdown
        function bindItemList(gridViewID) {
            var cgColumns = [
                               { 'columnName': 'itemname', 'width': '200', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                             , { 'columnName': 'uomname', 'width': '70', 'align': 'left', 'highlight': 4, 'label': 'Uom Name' }
                           //  , { 'columnName': 'itemcode', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Code' }
                            // , { 'columnName': 'itemgroupdesc', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Group Name' }
                            //  , { 'columnName': 'item_type', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Item Type' }
                             //, { 'columnName': 'closing_qty', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Cls Qty' }
                             //, { 'columnName': 'class_name', 'width': '120', 'align': 'left', 'highlight': 4, 'label': 'Class Name' }
                             , { 'columnName': 'bomname', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'BOM Name' }
                             , { 'columnName': 'bomno', 'width': '180', 'align': 'left', 'highlight': 4, 'label': 'BOM NO' }
                             , { 'columnName': 'uomid', 'width': '50', 'align': 'left', 'highlight': 4, 'label': 'Uom ID' }
                              , { 'columnName': 'bomid', 'width': '50', 'align': 'left', 'highlight': 4, 'label': 'BOM ID' }


            ];


            //var companyid = $('#' + hdnCompanyID).val();
            //var depthead = $('#' + hdnEmpCode).val();
            //var ddlDEPT_ID = $('#' + hdnSECTION).val();

            //var serviceURL = GLAccountServiceLink + "?isterm=1&includeempty=1&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            var serviceURL = BOMNameListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            //serviceURL += "&companyid=" + companyid;
            serviceURL += "&ispaging=1&isFinished=Y&batterytypeid=1";
            //serviceURL += "&locationid=" + locationid;
            // serviceURL += "&empstatus=" + "A";
            //serviceURL += "&acctypefilter=" + Enums.GLAccountTypeFilter.AllAccount;
            // serviceURL += "&namecomptype=" + Enums.DataCompareType.Contains;

            var gridSelector = "#" + gridViewID;


            $(gridSelector).find('input[id$="txtITEM_NAME"]').each(function (index, elem) {
                ///list click
                var elemRow = $(elem).closest('tr.gridRow');

                var hdnItemIDElem = $(elemRow).find('input[id$="txtITEM_NAME"]');

                //var prevGLCode = '';

                $(elem).closest('tr').find('input[id$="btnITEM_NAME"]').click(function (e) {
                    elmID = $(elem).attr('id');
                    //$(elem).combogrid("show");
                    $(elem).combogrid("dropdownClick");
                });



                //var compNameElem = $('#' + txtCompanyName);

                //$('#' + btnCompanyID).click(function (e) {
                //    //elmID = $(elem).attr('id');
                //    //$(elem).combogrid("show");
                //    $(compNameElem).combogrid("dropdownClick");
                //});


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
                    width: 800,
                    url: serviceURL,
                    search: function (event, ui) {
                        var elemRowCur = $(elem).closest('tr.gridRow');
                        //var vgroupid = $(elemRowCur).find('input[id$="hdngroupId"]').val();
                        //var vgroupid = $('#' + hdngroupId).val();+ "&groupid=" + vgroupid
                        var newServiceURL = serviceURL 
                        //var newServiceURL = serviceURL + "&companycode=" + companyCode + "&branchcode=" + branchCode + "&deptcode=" + deptCode
                        newServiceURL = JSUtility.AddTimeToQueryString(newServiceURL);
                        $(this).combogrid("option", "url", newServiceURL);


                    },

                    select: function (event, ui) {
                        //alert(ui.item.typename);
                        //$(".txtComboGrid").val(ui.item.code);
                        elemID = $(elem).attr('id');
                        //                    if (!validateGLAccount(elemID, ui.item)) {
                        //                        $(elem).val(prevGLCode);
                        //                        return false;
                        //                    }
                        if (!ui.item) {
                            event.preventDefault();
                            ClearItemData(elemID);
                            return false;
                            //ClearGLAccountData(elemID);
                        }
                        if (ui.item.id == 0) {
                            event.preventDefault();
                            return false;
                            //ClearGLAccountData(elemID);
                        }
                        else {
                            SetItemData(elemID, ui.item);
                        }
                        // setDetInstrument(hdnIsInstrumentElem, txtInstrumentElem, btnInstrumentElem);
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
                        var serviceURL = BOMNameListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
                        serviceURL += "&ispaging=1&batterytypeid=1";

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
                    //            if (accdata.menu[0].count > 0) {
                    //                menu = menudata.menu[0];
                    //            }
                    if (prddata.rows.length > 0) {
                        prcNo = prddata.rows[0];
                    }
                },
                complete: function () {
                    if (!isError) {
                        //return;
                        //alert (menu.name);
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

        }
        function SetItemData(txtItemCodeID, data) {
            $('#' + txtItemCodeID).val(data.itemid);

            var detRow = $('#' + txtItemCodeID).closest('tr.gridRow');
            $(detRow).find('input[id$="hdnItemID"]').val(data.itemid);
            $(detRow).find('input[id$="txtITEM_NAME"]').val(data.itemname);
           
            $(detRow).find('input[id$="hdnUomID"]').val(data.uomid);
            $(detRow).find('input[id$="txtUOM_NAME"]').val(data.uomname);
        }

        

        //$(document).on('keyup', '.txtQty', function () {
          
        //    sumGrandQty();

        //});

        var addQty = 0;
      


        function GetTotalSumAddedQty() {
            debugger;
            var totAdd = 0;
            $(document).find('.dxeEditArea').each(function (index, elem) {
                addQty = parseFloat(JSUtility.GetNumber($(elem).val()));             
                if (!isNaN(addQty)) {
                    totAdd += addQty;                  
                }
            });

            return totAdd;
        }

        function sumGrandQty() {
            var totAdded = GetTotalSumAddedQty();          
            $("#" + txtTotalQty).val(JSUtility.FormatCurrency(totAdded));
        }

        function showOverlay() {
            document.getElementById("overlay").style.display = "block";
        }

        function hideOverlay() {
            document.getElementById("overlay").style.display = "none";
        }

        //function OnCalculateTotal(visibleIndex, keyValue) {
        //    var controlCollection = ASPxClientControl.GetControlCollection();
        //    var itemfcqty= controlCollection.GetByName("lblITEM_FC_QTY_" + visibleIndex).GetNumber();
        //    controlCollection.GetByName("tbClientTotal_" + visibleIndex).SetText(itemfcqty.toFixed(2));
        //    hfChanges.Set("Row_" + visibleIndex.toString(), keyValue + "|" + unitPrice + "|" + unitsInStock);
        //}

    </script>

     <style type="text/css">

     .overlay {
        background-color: #000;
        cursor: wait;
        display: none;
        height: 100%;
        left: 0;
        opacity: 0.4;
        position: fixed;
        top: 0;
        width: 100%;
        z-index: 9999998;
    }

        
      
      
        .hidden
        {
            /*visibility:hidden;*/
            display:none;
        }


            
 

/* Style the tab */
.tab {
    overflow: hidden;
    border: 1px solid #ccc;
    background-color: #f1f1f1;
}

/* Style the buttons inside the tab */
.tab button {
    background-color: inherit;
    float: left;
    border: none;
    outline: none;
    cursor: pointer;
    padding: 14px 16px;
    transition: 0.3s;
    font-size: 17px;
    
}

/* Change background color of buttons on hover */
.tab button:hover {
    background-color: #ddd;
}

/* Create an active/current tablink class */
.tab button.active {
    background-color: #ccc;
}

/* Style the tab content */
.tabcontent {
    display: none;
    padding: 6px 12px;
    border: 1px solid #ccc;
    border-top: none;
}
         
    </style>
   

        

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="dvPageContent" style="width: 100%; height: auto;">
        <div id="dvContentHeader" class="dvContentHeader">
            <div id="dvHeader" class="dvHeader_Prod">
                <asp:Label ID="lblHeader" CssClass="lblHeader" runat="server" Text="Finished Goods Forecast Entry"></asp:Label>
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
                        <td align="right" class="auto-style2">
                            <asp:Label ID="lbl" runat="server" Text="Forecast NO :"></asp:Label>
                        </td>
                        <td class="auto-style2" colspan="2">
                            <asp:TextBox ID="txtFC_NO" runat="server" Style="text-align: left;" CssClass="colourdisabletextBox" Width="250px" Enabled="False"></asp:TextBox>
                        </td>
                        <td align="right" class="auto-style2">
                            <asp:Label ID="Label6" runat="server" Text="Forecast Desc :"></asp:Label>
                        </td>
                        <td class="auto-style2" colspan="2">
                            <asp:TextBox ID="txtFC_DESC" runat="server" CssClass="textBox" Style="text-align: left;" Width="250px" autofocus></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                        <td align="right" class="auto-style2">
                            <asp:Label ID="Label8" runat="server" Text="Month:"></asp:Label>
                        </td>
                        <td class="auto-style2">
                            <asp:TextBox ID="txtForcastMonth" runat="server" CssClass="textBox textDate dateParse" Style="text-align: left;" Width="73px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:HiddenField ID="hdnFC_ID" runat="server" />
                        </td>
                        <td style="text-align: right">
                            <asp:Label ID="Label1" runat="server" Text="FG Type :"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlFGFC_TYPE" runat="server" CssClass="dropDownList required" Width="150px" ViewStateMode="Enabled">
                                <asp:ListItem Selected="True" Value="1">Primary</asp:ListItem>
                                <asp:ListItem Value="2">Tender</asp:ListItem>
                                <asp:ListItem Value="3">Others</asp:ListItem>
                            </asp:DropDownList>

                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>
                            <asp:CheckBox ID="chkAUTH_STATUS" runat="server" Text="  Authorize   "   Style="display:none;" TextAlign="right"></asp:CheckBox>
                        </td>
                        <td>
                            <asp:HiddenField ID="hdnCompanyID" runat="server" Value="0" />
                        </td>

                        <td>
                            <asp:HiddenField ID="hdnAUTH_STATUS" runat="server" />
                        </td>
                        <td>
                                <asp:Button ID="btnUpload" CssClass="buttonSearch" runat="server" OnClick="btnUpload_Click" OnClientClick="showOverlay();" Text="Show Data" Style="padding-left: 22px;" />
                        </td>
                    </tr>
                </table>
            </div>
               
             <div id="Div5" runat="server" class="" style="float: right;width: 100%; text-align: left; border-top : solid 1px #0b07f5;">
                    <span style="font-weight: bold;font-size : 15px;color :#ff3b00;"> Details: </span>
                </div>
                <div id="dvGridHeader2" style="float:left; width: 831px; height: 25px; font-size: smaller;" class="subHeader_Prod">
                                    <table style="height: 80%; color: #FFFFFF;  font-weight: bold; text-align: left;"
                                        class="defFont" cellspacing="1" cellpadding="1">
                                        <tr class="headerRow_Prod">
                                            <td width="30px" class="headerColCenter_prod">SL#
                                            </td>
                                            <td width="200px" class="headerColCenter_prod">Item
                                            </td>
                                             <td width="15px" class="headerColCenter_prod"></td>
                                             <td width="45px" class="headerColCenter_prod">UOM
                                            </td>
                                            <td width="61px" class="headerColCenter_prod">Quantity
                                            </td>
                                            <td width="240px" class="headerColCenter_prod">Remarks
                                            </td>
                                            <td width="16px" class="headerColCenter_prod">Delete
                                            </td>
                                
                                        </tr>
                                    </table>
                                </div>
               <div id="groupBoxContainer boxShadow" style="height: auto; width: 100%; ">
                        <div id="groupDataDetails" style=" width: 90%; height: auto;">
                  
                            <div id="dvGridContainer2" class=" " runat="server" style="height: auto; text-align: left; width : 100%">
                               
                                <div id="dvGrid" style="width: 1100px; height: 340px; overflow: auto;" class="dvGrid">
                                  <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                            <asp:GridView ID="GRDDTLITEMLIST" runat="server" AutoGenerateColumns="False" ShowHeader="False" 
                                                CellPadding="1" CellSpacing="1" ForeColor="#333333" GridLines="Both" DataKeyNames="ITEM_ID" 
                                                EnableModelValidation="True" ClientIDMode="AutoID" OnRowCommand="GRDDTLITEMLIST_RowCommand" OnRowCreated="GRDDTLITEMLIST_RowCreated" OnRowDataBound="GRDDTLITEMLIST_RowDataBound" OnRowDeleting="GRDDTLITEMLIST_RowDeleting"  >
                                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                <Columns>
                                       

                                                    <asp:TemplateField HeaderText="SL#">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSLNo" runat="server" Text=  <%#Container.DataItemIndex+1 %> Style="text-align: center;"
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
                                                                                <asp:HiddenField ID="hdnFC_DET_ID" runat="server" Value='<%# Bind("FC_DET_ID") %>' />
                                                                            </td>

                                                                            <td>
                                                                                <asp:TextBox ID="txtITEM_NAME" runat="server" CssClass="textBox textAutoSelect" Width="196px" Text='<%# Bind("ITEM_NAME") %>'></asp:TextBox>
                                                                                <asp:HiddenField ID="hdnItemID" runat="server" Value='<%# Bind("ITEM_ID") %>' />
                                                                            </td>
                                                                            <td>
                                                                                <input id="btnITEM_NAME" type="button" value="" runat="server" class="buttonDropdown"
                                                                                    tabindex="-1" />
                                                                            </td>
                                                                             <td>

                                                                                <asp:TextBox ID="txtUOM_NAME" runat="server" CssClass="textBox textAutoSelect"  Width="40px" Text='<%# Bind("UOM_NAME") %>'></asp:TextBox>
                                                                                <asp:HiddenField ID="hdnUomID" runat="server" Value='<%# Bind("UOM_ID") %>' />
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtITEM_FC_QTY" runat="server" style="text-align: right;" CssClass="textBox textAutoSelect" Width="60px" BackColor="Khaki" align="right" Text='<%# Bind("ITEM_FC_QTY") %>'  onkeypress="return isNumberKey(event,this);"></asp:TextBox>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtREMARKS" runat="server" CssClass="textBox textAutoSelect" Width="240px" Text='<%# Bind("REMARKS") %>' Style=""></asp:TextBox>
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
                                                                            <div style="position: relative; height: 80%; width: 100%;">
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


                                <div id="divGridControls2" style="width: 100%; height: 30px; border-top: solid 1px #C0C0C0; border-bottom : solid 1px #0b07f5; ">
                                    <table style="width: 70%; height: 100%; text-align: center;" cellspacing="1" cellpadding="1"
                                        border="0">
                                        <tr>
                                            <td style="width: 2px"></td>
                              
                                            <td style="width: 160px; text-align: right;">
                                                                             </td>
                                            <td align="right" style="text-align: left">
                                               &nbsp;
                                            </td>
                                             <td align="right">
                                                &nbsp;</td>
                                            <td align="right">
                                                &nbsp;</td>
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

                                            <td align="right">
                                                &nbsp;</td>
                                           
                                            <td align="right" style="width: 90px">&nbsp;
                                            </td>
                                             
                               
                                

                                        </tr>
                            
                                
                                    </table>
                                </div>
                            </div>

                          
                          
                             </div>
                   </div>

 
        

     </div>
        <div id="dvContentFooter" class="dvContentFooter">
            <table>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="btnAddNew" runat="server" Text="New" CssClass="buttonNew" OnClick="btnAddNew_Click" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonCancel checkIsDirty" />
                    </td>
                    <td>
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonSave checkRequired" AccessKey="s" OnClick="btnSave_Click" OnClientClick="if ( ! UserDeleteConfirmation()) return false;" />
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="buttonEdit" OnClick="btnEdit_Click" />
                    </td>

                     <td>
                            <asp:Button ID="btnAuthorize" runat="server" Text="Authorize" CssClass="buttoncommon" OnClientClick="if ( ! UserAuthorizeConfirmation()) return false;" OnClick="btnAuthorize_Click" />
                        </td>
                    <td>
                        <asp:Button ID="btnRefresh" runat="server" Text="Refresh" CssClass="buttonRefresh checkIsDirty" OnClick="btnRefresh_Click" />
                    </td>
                    <td>
                        <%--<uc:PrintButton runat="server" ID="ucPrintButton" DefaultPrintAction="Preview" AutoPrint="False" />--%>
                    </td>
                    <td>
                        <input id="btnClose" type="button" runat="server" class="buttonClose" value="Close" onclick="if (ContentForm) { ContentForm.CloseForm(); }" />
                    </td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td>
                        <asp:Button ID="btnPopupTrigger" runat="server" Text="Button" CssClass="buttonHidden" />
                        <asp:HiddenField ID="hdnPopupTriggerID" runat="server" Value="" />
                        <asp:HiddenField ID="hdnPopupCommand" runat="server" Value="" />
                    </td>
                </tr>
            </table>
        </div>

    <div id="overlay" class="overlay" >
         <div style="margin:auto;width:200px;height:400px;background-color:black;border:solid 1px black;
                  text-align:center; vertical-align:middle;"> 
           <span style="color:white; font-size:medium;" >Please Wait...</span>
             <br />
             <img alt="" src="../../image/progress.gif" />
         </div>
    </div>

    <div id="overlayReport" class="overlay" style="opacity: 0.8;">
         <div style="margin:auto;width:450px;height:80px; position: relative;background-color: blue;
                  text-align:center; vertical-align:middle; cursor:auto; z-index: 9999999;">
           <table width="100%">
           <tr>
               <td>
                   <span style="color:white; font-size:medium;" >Click Open Report to view Report.</span>
               </td>
           </tr>
           <tr>
             <td>         
                <input id="btnOpenReportWindow" type="button" value="Open Report" class="buttoncommon" />
                <input id="btnCacnelReportWindow" type="button" value="Cancel" class="buttoncommon"  />  
             </td>
           </tr>            
            </table>
         </div>
    </div>
        </div>
</asp:Content>
