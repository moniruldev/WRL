<%@ Page Title="" Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" ViewStateMode="Disabled" CodeBehind="ProductionForcastEntry.aspx.cs" Inherits="PG.Web.Production.ProductionForcastEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="../javascript/jquery.ui.combogrid.js" type="text/javascript"></script>
    <script src="../javascript/jquery.attributeobserver.js" type="text/javascript"></script>
    <link href="../css/jquery.ui.combogrid.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        var isPageResize = true;
        ContentForm.CalendarImageURL = "../image/calendar.png";

        var ItemGroupListServiceLink = '<%=this.ItemGroupListServiceLink%>';
      

        var txtTotalQty = '<%=txtTotalQty.ClientID%>';

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

        //$(document).ready(function () {
        //    if ($('#' + txtGroupName).is(':visible')) {
        //        bindGroupList();
        //    }
        //});
        //this is for group dropdown
        //function bindGroupList() {

        //    var cgColumns = [
        //                     { 'columnName': 'itemgroupcode', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Code' }
        //                    , { 'columnName': 'itemgroupdesc', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Name' }
        //                    , { 'columnName': 'itemgroupnameparent', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Group Parent' }
        //    ];
        //    var serviceURL = ItemGroupListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
        //    serviceURL += "&ispaging=1";
        //    var groupIDElem = $('#' + txtGroupName);

        //    $('#' + btnGroupID).click(function (e) {
        //        $(groupIDElem).combogrid("dropdownClick");
        //    });

        //    $(groupIDElem).combogrid({
        //        debug: true,
        //        searchButton: false,
        //        resetButton: false,
        //        alternate: true,
        //        munit: 'px',
        //        scrollBar: true,
        //        showPager: true,
        //        colModel: cgColumns,
        //        autoFocus: true,
        //        showError: true,
        //        width: 600,
        //        url: serviceURL,
        //        search: function (event, ui) {
        //            //var companyCode = $('#' + ddlCompany).val();
        //            //var branchCode = $('#' + hdnBranch).val();
        //            //var deptCode = $('#' + hdnDepartment).val();
        //            //var locationid = $('#' + lblLocationID).val();
        //            // var seid = $('#' + txtExecutiveID).val();
        //            var newServiceURL = serviceURL;
        //            $(this).combogrid("option", "url", newServiceURL);
        //        },
        //        select: function (event, ui) {
        //            if (!ui.item) {
        //                event.preventDefault();

        //                // $('#' + hdnDealerID).val('0');
        //                //$('#' + txtDealerID).val('');
        //                return false;
        //                //ClearGLAccountData(elemID);
        //            }


        //            if (ui.item.dealerid == '') {
        //                event.preventDefault();
        //                return false;
        //                //ClearGLAccountData(elemID);
        //            }
        //            else {
        //                // $('#' + hdnDealerID).val(ui.item.dealerid);
        //                $('#' + txtGroupID).val(ui.item.itemgroupid);
        //                $('#' + txtGroupName).val(ui.item.itemgroupdesc);


        //            }
        //            return false;
        //        },

        //        lc: ''
        //    });


        //    $(groupIDElem).blur(function () {
        //        var self = this;

        //        var groupID = $(groupIDElem).val();
        //        if (groupID == '') {
        //            // $('#' + hdnDealerID).val('0');

        //            $('#' + txtGroupID).val('0');
        //            $('#' + txtGroupName).val('');

        //        }
        //    });
        //}



        $(document).on('keyup', '.txtQty', function () {
          
            sumGrandQty();

        });

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
                        <%-- <td style="width: 100%">
                                                 <table style="" border="0" cellspacing="2" cellpadding="1">
                                                    <tr>

                                                        <td align="right" class="auto-style2">
                                                       
                                                            <asp:Label ID="Label3" runat="server" Text="Item Type:"></asp:Label>
                                                        </td>

                                                        <td class="auto-style2">

                                                            <asp:DropDownList ID="ddlItemType" runat="server" CssClass="dropDownList" Width="160"> </asp:DropDownList>
                                                        </td>
                                                      
                                                    </tr>

                                                     <tr>

                                                        <td align="right" class="auto-style2">
                                                            <asp:Label ID="Label1" runat="server" Text="Item Class:"></asp:Label>
                                                        </td>

                                                        <td class="auto-style2">

                                                            <asp:DropDownList ID="ddlItemClass" runat="server" CssClass="dropDownList" Width="160">  </asp:DropDownList>
                                                        </td>
                                                      
                                                    </tr>


                                                    <tr>

                                                        <td align="right" class="auto-style2">
                                                            <asp:Label ID="lblCustomer" runat="server" Text="Item Group:"></asp:Label>
                                                        </td>

                                                        <td class="auto-style2">

                                                            <asp:TextBox ID="txtGroupName" runat="server" CssClass="textBox" Enabled="true"></asp:TextBox>
                                                        </td>
                                                        <td class="auto-style2">
                                                            <input id="btnGroupID" type="button" value="" runat="server" class="buttonDropdown"
                                                                tabindex="-1" />
                                                        </td>
                                                        <td colspan="8" class="auto-style2">

                                                           
                                                            <asp:HiddenField ID="txtGroupID" runat="server" />

                                                        </td>
                                                    </tr>


                    <tr>--%>

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
                            <asp:CheckBox ID="chkAUTH_STATUS" runat="server" Text="  Authorize   " Checked="True" TextAlign="right"></asp:CheckBox>
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
            <div id="dvControls" style="width: 100%; height: 550px;">
                <div id="dvControlsInner" class="groupBoxContainer boxShadow" style="width: 100%; height: 650px;">
                    <dx:ASPxGridView ID="grdFG_Forecast" runat="server" AutoGenerateColumns="False" EnablePagingGestures="False" Width="70%" KeyFieldName="ITEM_ID" ClientInstanceName="grdFG_Forecast" OnDataBinding="grdFG_Forecast_DataBinding">
                        <Columns>
                            <dx:GridViewDataTextColumn Caption="Item Name" VisibleIndex="1" Width="250px" FieldName="ITEM_NAME">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="UOM" VisibleIndex="2" Width="40px" FieldName="UOM_NAME">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Forecast Qty" FieldName="ITEM_FC_QTY" Width=" 170px" VisibleIndex="3">
                                <DataItemTemplate>
                                    <dx:ASPxSpinEdit ID="lblITEM_FC_QTY" ClientInstanceName="lblITEM_FC_QTY" NumberType="Integer" CssClass="txtQty" OnInit="lblITEM_FC_QTY_Init" MinValue="0" MaxValue="50000" runat="server" AutoPostBack="false"
                                        Style="border: 1px solid; width: 150px" Value='<%# Bind("ITEM_FC_QTY") %>'>
                                        <ClientSideEvents KeyDown="function(s, e) {
                                                          if(ASPxClientUtils.GetKeyCode(e.htmlEvent) ===  ASPxKey.Enter)
			                                                    return ASPxClientUtils.PreventEventAndBubble(e.htmlEvent);  
	                                                        }" />
                                    </dx:ASPxSpinEdit>
                                </DataItemTemplate>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Remarks" FieldName="REMARKS" Width="250px" VisibleIndex="4">
                                <DataItemTemplate>
                                    <dx:ASPxTextBox ID="lblRemarks" ClientInstanceName="lblRemarks" runat="server"
                                        Style="border: 1px solid; width: 250px" Theme="Office2003Blue" Value='<%# Bind("REMARKS") %>' OnInit="lblRemarks_Init">
                                    </dx:ASPxTextBox>
                                </DataItemTemplate>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn VisibleIndex="5" Width="200px" Visible="false" FieldName="ITEM_ID">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="UOM_ID" Visible="false" VisibleIndex="6">
                            </dx:GridViewDataTextColumn>
                        </Columns>
                        <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
                        <%--<SettingsSearchPanel Visible="True"  />--%>
                        <SettingsBehavior AllowSort="False" />
                        <SettingsPager PageSize="25" Mode="ShowAllRecords">
                        </SettingsPager>
                        <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="350" ShowGroupButtons="False" />
                        <Styles>
                            <Header CssClass="headerRow_Prod" HorizontalAlign="Center" Font-Bold="True" ForeColor="White" Font-Size="9pt">
                            </Header>
                            <AlternatingRow BackColor="#FFFFCC">
                            </AlternatingRow>
                            <HeaderPanel BackColor="#669999">
                            </HeaderPanel>
                        </Styles>
                    </dx:ASPxGridView>
                    <div id="divGridControls2" style="width: 100%; height: 25px; border-top: solid 1px #C0C0C0;">
                    <table style="width: auto; height: 100%; text-align: center;" cellspacing="1" cellpadding="1"
                        border="0">
                        <tr>
                            <td style="width: 2px"></td>

                            <td style="width: 20px;"></td>
                            <td style="width: 370px"></td>
                            <td align="right">&nbsp;
                            </td>
                            <td align="right">
                                <asp:Label ID="lbltotalSalesAmount" runat="server" Text="Total Qnty:" Font-Bold="True"></asp:Label>
                            </td>
                            <td align="right">
                                <asp:TextBox ID="txtTotalQty" runat="server" ReadOnly="true" CssClass="textBox" Style="text-align: right;" Width="100" TabIndex="-1" Font-Bold="True"></asp:TextBox>
                            </td>
                        
                        </tr>
                    </table>
                </div>
                </div>
                

            <%--<div id="dvGridFooter" style="width: 100%; height: 25px; font-size: smaller;" class="subFooter">
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
                                                        <asp:Label ID="Label4" runat="server" Text="Page:"></asp:Label>
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
                        </div>--%>
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
</asp:Content>
