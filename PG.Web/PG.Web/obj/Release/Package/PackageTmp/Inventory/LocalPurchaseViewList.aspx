<%@ Page Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="LocalPurchaseViewList.aspx.cs" Inherits="PG.Web.Report.Inventory.LocalPurchaseViewList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="DropDownCheckBoxes" Namespace="Saplin.Controls" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../javascript/jquery.ui.combogrid.js" type="text/javascript"></script>
    <link href="../css/jquery.ui.combogrid.css" rel="stylesheet" type="text/css" />
    <script src="../javascript/jquery.attributeobserver.js" type="text/javascript"></script>

     <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css"/> 
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap-theme.min.css"/>  
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js" type="text/javascript"></script> 

    <script language="javascript" type="text/javascript">
        // <!CDATA[    

        var btnGridPageGoTo = '<%=btnGridPageGoTo.ClientID %>';
        var txtGridPageNo = '<%=txtGridPageNo.ClientID %>';


        var isPageResize = true;
        ContentForm.CalendarImageURL = "../image/calendar.png";

        var SupplierListServiceLink = '<%=this.SupplierListServiceLink%>';
        var ReportViewPageLink = '<%=this.ReportViewPageLink%>';
        var ReportViewPDFPageLink = '<%=this.ReportViewPDFPageLink%>';

        var txtSupplierName = '<%=txtSupplierName.ClientID%>';
        var btnSupplierID = '<%=btnSupplierID.ClientID%>';
        var hdnSupplierID = '<%=hdnSupplierID.ClientID%>';
        <%-- var ReportPrintPageLink = '<%=this.ReportPrintPageLink%>';
         var ReportPDFPageLink = '<%=this.ReportPDFPageLink%>';

         var GLAccountServiceLink = '<%=this.GLAccountServiceLink%>';
         var GLGroupServiceLink = '<%=this.GLGroupServiceLink%>';

         var GetJournalListServiceLink = '<%=this.GetJournalListServiceLink%>';

         var AccRefServiceLink = '<%=this.AccRefServiceLink%>';--%>

        var ifPrintButton = '<%=ifPrintButton.ClientID%>';


         <%--var txtFromDateID = '<%=txtFromDate.ClientID%>';
         var txtToDateID = '<%=txtToDate.ClientID%>';--%>



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

            $('#' + txtGridPageNo).keydown(function (e) {
                if (e.keyCode == 13) {
                    e.preventDefault();
                    $('#' + btnGridPageGoTo).click();
                }
            });


            if ($('#' + txtSupplierName).is(':visible')) {

                bindSupplierList();

            }





        });

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

        function bindSupplierList() {
            var cgColumns = [{ 'columnName': 'supcode', 'width': '80', 'align': 'left', 'highlight': 2, 'label': 'Code' }
                             , { 'columnName': 'supname', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                             , { 'columnName': 'supaddress', 'width': '200', 'align': 'left', 'highlight': 4, 'label': 'Address' }
                             , { 'columnName': 'supphone', 'width': '80', 'align': 'left', 'highlight': 0, 'label': 'phone' }



            ];


            //var companyid = $('#' + hdnCompanyID).val();
            //var depthead = $('#' + hdnEmpCode).val();
            //var locationid = $('#' + ddlLocation).val();
            // var seid = $('#' + txtExecutiveID).val();

            //var serviceURL = GLAccountServiceLink + "?isterm=1&includeempty=1&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            var serviceURL = SupplierListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            //serviceURL += "&companyid=" + companyid;
            serviceURL += "&ispaging=1";
            // serviceURL += "&locationid=" + locationid;
            //serviceURL += "&seid=" + seid;
            // serviceURL += "&empstatus=" + "A";
            //serviceURL += "&acctypefilter=" + Enums.GLAccountTypeFilter.AllAccount;
            // serviceURL += "&namecomptype=" + Enums.DataCompareType.Contains;



            var supplierIDElem = $('#' + txtSupplierName);

            $('#' + btnSupplierID).click(function (e) {
                //elmID = $(elem).attr('id');
                //$(elem).combogrid("show");
                $(supplierIDElem).combogrid("dropdownClick");
            });


            $(supplierIDElem).combogrid({
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
                    //var companyCode = $('#' + ddlCompany).val();
                    //var branchCode = $('#' + hdnBranch).val();
                    //var deptCode = $('#' + hdnDepartment).val();
                    //var locationid = $('#' + lblLocationID).val();
                    // var seid = $('#' + txtExecutiveID).val();

                    if ($('#' + txtSupplierName).val() == '') {
                        $('#' + hdnSupplierID).val('');
                    }

                    var newServiceURL = serviceURL;
                    $(this).combogrid("option", "url", newServiceURL);


                },
                select: function (event, ui) {
                    if (!ui.item) {
                        event.preventDefault();

                        // $('#' + hdnDealerID).val('0');
                        $('#' + txtDealerID).val('');
                        return false;
                        //ClearGLAccountData(elemID);
                    }


                    if (ui.item.dealerid == '') {
                        event.preventDefault();
                        return false;
                        //ClearGLAccountData(elemID);
                    }
                    else {
                        // $('#' + hdnDealerID).val(ui.item.dealerid);
                        $('#' + hdnSupplierID).val(ui.item.supid);
                        $('#' + txtSupplierName).val(ui.item.supname);


                    }
                    return false;
                },

                lc: ''
            });


            $(supplierIDElem).blur(function () {
                var self = this;

                var customerID = $(supplierIDElem).val();
                if (customerID == '') {
                    // $('#' + hdnDealerID).val('0');
                    $('#' + txtSupplierName).val('');

                }
            });
        }


        // ]]>
    </script>
    <style type="text/css">
        /*.FixedHeader { POSITION: relative; TOP: expression(this.offsetParent.scrollTop); BACKGROUND-COLOR: white } */

        .FixedHeader {
            POSITION: relative;
            BACKGROUND-COLOR: white;
        }

        #dvMessage {
            height: 20px;
        }

        .style1 {
            width: 113px;
        }



        .auto-style42 {
        width: 100px;
    }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hdnSupplierID" runat="server" Value="0" />



    <div id="dvPageContent" style="width: 100%; height: auto;">
        <div id="dvContentHeader" class="dvContentHeader">
            <div id="dvHeader" class="dvHeader">
                <asp:Label ID="lblHeader" CssClass="lblHeader" runat="server" Text="Local Purchase View"></asp:Label>
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
                <table >
                      <tr>

                       
                         <td align="right" >
                            <asp:Label ID="lblIndentDepartment" runat="server"  Text="Indent Department:"></asp:Label><span style="color: red">*</span>

                       </td>
                       <td align="left" colspan="4">
                       <asp:DropDownList ID="ddlFromDepartment" runat="server" CssClass="dropDownList required">
                       </asp:DropDownList>
                       </td>

                    </tr>
                    <tr>

                        <td align="right">
                            <asp:Label ID="LblLocation" runat="server" Text="Supplier:"></asp:Label><span style="color: red">*</span>
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td> <asp:TextBox ID="txtSupplierName" runat="server" CssClass="textBox" Width="235px"></asp:TextBox></td>
                                    <td>  <input id="btnSupplierID" type="button" value="" runat="server" class="buttonDropdown"
                                tabindex="-1" /></td>
                                </tr>
                            </table>
                           
                          
                        </td>
                        <td align="right" >
                            <asp:Label ID="lblPONo" runat="server" Text="PO No:"></asp:Label>

                        </td>
                        <td>
                            <asp:TextBox ID="txtPONo" runat="server" CssClass="textBox notEnterToTab"></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="lblDateFrom" runat="server" Text="Date From:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="textBox textDate dateParse"></asp:TextBox>
                        </td>

                        <td align="right" >
                            <asp:Label ID="lblToDate" runat="server" Text="Date To:"></asp:Label>
                        </td>

                        <td>
                            <asp:TextBox ID="txtToDate" runat="server" CssClass="textBox textDate dateParse"></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                        <td></td>
                        <td align="right">
                            <asp:Button ID="btnLoadGridData" runat="server" CssClass="buttonRefresh" Text="Show Data" OnClick="btnLoadGridData_Click" /></td>

                        <td class="auto-style42">
                            <%--<asp:Button ID="buttonIRRPreview" runat="server" CssClass="buttoncommon buttonPrint" Style="padding-left: 22px;" Width="100px" Text="Preview" OnClick="buttonIRRPreview_Click" />--%>

                        </td>
                        <td>

                            <%--<input id="btnAddNew" type="button" runat="server" value="New IGR (Requisition)" class="buttonNew" />--%>
                        </td>
                    </tr>

                </table>
            </div>
            <div id="dvControls" style="width: 100%;">
                <div id="dvControlsInner" class="groupBoxContainer boxShadow" style="width: 95%">
                    <div id="dvGridContainer" style="width: 100%; height: auto; text-align: left;">
                        <div id="dvGridHeader" style="width: 100%; height: 25px; font-size: smaller;" class="subHeader">
                            <table style="height: 100%; color: #FFFFFF; font-weight: bold; text-align: center;"
                                class="defFont" cellspacing="1" cellpadding="1" rules="all">
                                <tr>
                                    <td width="40px" align="left">SL#</td>
                                    <td width="150px" align="left">PO NO</td>
                                    <td width="80px" align="left">PO Date
                                    </td>
                                    <td width="100px" align="left">P. Apr.?</td>
                                    <td width="100px" align="left">F. Apr.?</td>
                                    <td width="100px" align="left">Dir. Apr.?</td>
                                     <td width="200px" align="left">Supplier
                                    </td>
                                    <td width="100px" align="left">Item Details
                                    </td>
                                     <td width="100px" align="left">PO By
                                    </td>
                                    <td width="100px" align="left">
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="dvGrid" style="width: auto; height: 360px; overflow: auto;">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="1"
                                CellSpacing="1" ForeColor="#333333" GridLines="None" Font-Names="Arial" Font-Size="9pt"
                                OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound"
                                EmptyDataText="There is no record" PageSize="15" OnPageIndexChanging="GridView1_PageIndexChanging"
                                OnSelectedIndexChanged="GridView1_SelectedIndexChanged" ShowHeader="False">
                                <PagerSettings Mode="NumericFirstLast" Visible="False" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <Columns>
                                     <asp:TemplateField HeaderText = "Row Number" ItemStyle-Width="40">
        <ItemTemplate>
            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
        </ItemTemplate>
    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="MRR_NO" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPOId" runat="server" Text='<%# Bind("PURCHASE_ID") %>' Style="text-align: center;" Width="150px"></asp:Label>

                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="PURCHASE_NO" HeaderText="Purchase No" ItemStyle-Width="150px">
                                        <ItemStyle Width="150px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PURCHASE_DATE" HeaderText="Date" DataFormatString="{0:dd-MMM-yyyy}"
                                        ItemStyle-Width="80">
                                        <ItemStyle Width="80px"></ItemStyle>
                                    </asp:BoundField>
                                    
                                  
                                    <asp:BoundField DataField="MRR_ITEM_DETAILS" HeaderText="ITEM DETAILS" ItemStyle-Width="400" Visible="false">

                                        <ItemStyle Width="400px"></ItemStyle>
                                    </asp:BoundField>

                                  

                                      <asp:TemplateField HeaderText="Purchase Head App.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPurchase_H_STATUS" runat="server" Text='<%# Bind("APPROVED_STATUS_PUR_H") %>' Style="text-align: left;" Width="100px"></asp:Label>

                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Finance Head App.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFinance_STATUS" runat="server" Text='<%# Bind("APPROVED_STATUS_FINANCE_H") %>' Style="text-align: left;" Width="100px"></asp:Label>

                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Directior App.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDirectorStatus" runat="server" Text='<%# Bind("APPROVED_STATUS_DIRECTOR") %>' Style="text-align: left;" Width="100px"></asp:Label>

                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="SUP_NAME" HeaderText="SUPPLIER NAME" ItemStyle-Width="200">
                                        <ItemStyle Width="200px"></ItemStyle>
                                    </asp:BoundField>
                                    
                                 <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkItemDtl"  CssClass="btn btn-default"  CommandName="itemdtl" CommandArgument='<%# Bind("PURCHASE_NO") %>'><i class="glyphicon glyphicon-info-sign" style="padding-right:4px; font-size:14px; color:#4695cf"></i>Items</asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle Width="100px" />
                                    </asp:TemplateField>
                                     <asp:BoundField DataField="FULLNAME" HeaderText="Entry By" ItemStyle-Width="150">
                                        <ItemStyle Width="150px"></ItemStyle>
                                    </asp:BoundField>

                                    <asp:TemplateField HeaderText="Print">
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkprint"  CssClass="btn btn-default"  CommandName="print" CommandArgument='<%# Bind("PURCHASE_NO") %>'><i class="glyphicon glyphicon-print" style="padding-right:4px; font-size:14px; color:#4695cf"></i>Print</asp:LinkButton>

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
                         <div id="myModalItemDetails" class="modal fade" >  
            <div class="modal-dialog" style="max-width:500px; align-content:space-around;"> <%-- --%>
                <div class="modal-content" style="max-width:500px;">  
                    <div class="modal-header" style="max-width:500px;">  
                        
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>  
                        <h4 class="modal-title">Item Details</h4>  
                    </div>  
                    <div class="modal-body" style="overflow-y:scroll; overflow-x:scroll; max-height: 95%; max-width:1080px; margin-top: 10px; margin-bottom: 10px;"> <%-- --%>
                        <asp:Label ID="Label8" runat="server" ClientIDMode="Static"></asp:Label>  
                         <asp:Label ID="lblMPONo" runat="server" Text="Purchase No : " ></asp:Label>
                        <asp:Label ID="lblPONoText" runat="server" Visible="true"  Font-Bold="true" ></asp:Label>
                        <asp:HiddenField ID="hdnStatusDetailsPOID" runat="server" />
                       <asp:GridView ID="grdItemDetails" runat="server" HeaderStyle-BackColor="#3AC0F2" HeaderStyle-ForeColor="Black"
            RowStyle-BackColor="White" AlternatingRowStyle-BackColor="#A1DCF2" AlternatingRowStyle-ForeColor="#000"
            BorderStyle="None" BorderWidth="5px" CellPadding="10"  GridLines="Vertical" CssClass="gridView"
            AutoGenerateColumns="false">
            <Columns>
                 <asp:TemplateField HeaderText="SL" ItemStyle-Width="30px">   
                     <ItemTemplate>
                             <%# Container.DataItemIndex + 1 %>   
                     </ItemTemplate>
                 </asp:TemplateField>
                <asp:BoundField DataField="ITEM_NAME" HeaderText="ITEM NAME" ItemStyle-Width="300px" />
                  <asp:BoundField DataField="PURCHASE_QTY" HeaderText="QTY" ItemStyle-Width="80px" />
                <asp:BoundField DataField="UOM_NAME" HeaderText="UOM" ItemStyle-Width="60px" />
          
            </Columns>
        </asp:GridView>
                      
                    </div>  
                
                    <div class="modal-footer"> 
                        <table>
                            <tr>
                        <td>
                       
                   
                        <button type="button"  Class="buttonClose"  data-dismiss="modal">Close</button>  
                             </td>
                                </tr>
                          </table> 
                    </div>  
                </div>  
            </div>  
        </div> 
                        <div id="dvGridFooter" style="width: 100%; height: 25px; font-size: smaller;" class="subFooter">
                            <table style="height: 100%; width: 100%; font-weight: bold;" cellspacing="2" cellpadding="1"
                                rules="all">
                                <tr>
                                    <td align="left" style="width: 40%">
                                        <table>
                                            <tr>
                                                <td style="width: 2px;"></td>
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
                                                        <asp:Label ID="Label2" runat="server" Text="Page Size:"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlGridPageSize" runat="server" CssClass="dropDownList" Width="50"
                                                            Height="18" AutoPostBack="True" OnSelectedIndexChanged="ddlGridPageSize_SelectedIndexChanged">
                                                            <asp:ListItem Value="10">10</asp:ListItem>
                                                            <asp:ListItem Value="20">20</asp:ListItem>
                                                            <asp:ListItem Value="30">30</asp:ListItem>
                                                            <asp:ListItem Value="50">50</asp:ListItem>
                                                            <asp:ListItem Value="100">100</asp:ListItem>
                                                            <asp:ListItem Value="200">200</asp:ListItem>
                                                            <asp:ListItem Value="0" Selected="True">all</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label1" runat="server" Text="Page:"></asp:Label>
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
                                                    <td style="width: 2px;"></td>
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
            <div id="dvContentFooterInner" class="dvContentFooterInner">
                <div style="width: 100%; height: 100%; margin-bottom: 0px;">
                    <div style="width: auto; min-width: 300px; height: auto; text-align: left;">
                        <table border="0">
                            <tr>
                                <td style="width: 100px;"></td>
                                <td>
                                    <asp:Label ID="Label3" runat="server" Text="Report View" Visible="false"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlReportViewMode" runat="server" CssClass="dropDownList" Visible="false">
                                        <asp:ListItem Value="0">In This Tab</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="1">In New Tab</asp:ListItem>
                                        <asp:ListItem Value="2">In New Window</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlReportViewType" runat="server" CssClass="dropDownList" Visible="false">
                                     <%--   <asp:ListItem Value="0">Screen</asp:ListItem>--%>
                                        <asp:ListItem Selected="True" Value="1">PDF</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td></td>

                                <td style="width: 20px;"></td>
                                <td style="width: 10px;"></td>
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




    <%--<div id="dvPageContent" style="width: 100%; height: auto;">
        <div id="dvContentHeader" class="dvContentHeader">
            <div id="dvHeader" class="dvHeader">
                <asp:Label ID="lblHeader" CssClass="lblHeader" runat="server" Text="Material Receive Report"></asp:Label>
            </div>
            <!--Message Div -->
            <div id="dvMsg" runat="server" class="dvMessage">
                <asp:Label ID="lblMessage" runat="server" Font-Size="Small" Width="100%"> </asp:Label>
            </div>
            <div id="dvHeaderControl" class="dvHeaderControl">
            </div>
        </div>

        <div id="dvContentMain" class="dvContentMain">
            <div id="dvControls" style="height: auto; width: 100%">
                <div id="dvControlsInner" class="groupBoxContainer boxShadow">
                    <div id="groupBox">
                        <div id="groupHeader" class="groupHeader">
                            <div style="width: 100%; height: 20px;">
                                <table>
                                    <tr>
                                        <td>
                                            <div id="dvIconEditMode" class="iconView" runat="server"></div>
                                        </td>
                                        <td>
                                            <span>Material Receive Report</span>
                                        </td>
                                    </tr>
                                </table>

                            </div>

                        </div>

                        <div id="dvControlsHead" style="height: auto; width: 100%;">
                            <table>
                                <tr>                                 
                                     <td>
                                        <asp:Label ID="Label1" runat="server" Text="MRR No:"></asp:Label>

                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMaterialReceiveNo" runat="server" CssClass="textBox notEnterToTab"></asp:TextBox>
                                    </td>
                                       <td>
                                        <asp:Label ID="LblLocation" runat="server" Text="Supplier"></asp:Label><span style="color: red">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSupplierName" runat="server" CssClass="textBox" Width="180px"></asp:TextBox>
                                        <input id="btnSupplierID" type="button" value="" runat="server" class="buttonDropdown"
                                            tabindex="-1" />
                                    </td>
                                   

                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label3" runat="server" Text="Date From"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFromDate" runat="server" CssClass="textBox textDate dateParse"></asp:TextBox>
                                    </td>

                                    <td>
                                        <asp:Label ID="Label4" runat="server" Text="Date To:"></asp:Label>
                                    </td>

                                    <td>
                                        <asp:TextBox ID="txtToDate" runat="server" CssClass="textBox textDate dateParse"></asp:TextBox>
                                    </td>

                                </tr>
                                <tr>
                                    <td></td>
                                    <td></td>

                                    <td></td>
                                    <td>                                        
                                        <asp:Button ID="btnLoadGridData" runat="server" CssClass="buttonRefresh" Text="Show Data" OnClick="btnLoadGridData_Click" />
                                    </td>
                                </tr>

                            </table>
                        </div>


                    </div>
                </div>


            </div>
            <div id="dvGrid" style="width: 90%; height: 400px; overflow: auto;">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="1"
                    CellSpacing="1" ForeColor="#333333" GridLines="None" Font-Names="Arial" Font-Size="9pt"
                    DataKeyNames="MRR_ID" OnRowDataBound="GridView1_RowDataBound"
                    EmptyDataText="There is no record" OnRowCommand="GridView1_RowCommand">
                    <PagerSettings Mode="NumericFirstLast" Visible="False" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <Columns>
                   


                        <asp:TemplateField HeaderText="MRR_NO" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblMRRId" runat="server" Text='<%# Bind("MRR_NO") %>' Style="text-align: center;" Width="150px"></asp:Label>

                            </ItemTemplate>
                            <ItemStyle VerticalAlign="Top" />
                        </asp:TemplateField>
                        
                        <asp:BoundField DataField="MRR_NO" HeaderText="MRR No" ItemStyle-Width="150px" >
<ItemStyle Width="150px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="MRR_DATE" HeaderText="Date" DataFormatString="{0:dd-MMM-yyyy}"
                            ItemStyle-Width="80" >
<ItemStyle Width="80px"></ItemStyle>
                        </asp:BoundField>
                           <asp:BoundField DataField="SUP_NAME" HeaderText="SUPPLIER NAME" ItemStyle-Width="200" >
<ItemStyle Width="200px"></ItemStyle>
                        </asp:BoundField>
                         <asp:BoundField DataField="MRR_ITEM_DETAILS" HeaderText="ITEM DETAILS" ItemStyle-Width="400" >
                        
<ItemStyle Width="400px"></ItemStyle>
                        </asp:BoundField>
                        
                         <asp:TemplateField HeaderText="Purchase Info">
                            <ItemTemplate>
                                <asp:Label ID="lblPurchaseDesc" runat="server" Text='<%# Bind("PURCHASE_DESCRIPTION") %>' Style="text-align: left;" Width="200px"></asp:Label>

                            </ItemTemplate>
                            <ItemStyle VerticalAlign="Top" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="MRR Reprint">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnLinkDc" runat="server" CommandName="MRR_NO" Width="100px" Font-Bold="true">MRR Reprint</asp:LinkButton>
                            </ItemTemplate>
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
            <div id="dvGridFooter" style="width: 90%; height: 25px; font-size: smaller;" class="subFooter">
                            <table style="height: 90%; width: 100%; font-weight: bold;" cellspacing="2" cellpadding="1"
                                rules="all">
                                <tr>
                                    <td align="left" style="width: 40%">
                                        <table>
                                            <tr>
                                                <td style="width: 2px;"></td>
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
                                                        <asp:Label ID="Label5" runat="server" Text="Page Size:"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlGridPageSize" runat="server" CssClass="dropDownList" Width="50"
                                                            Height="18" AutoPostBack="True" OnSelectedIndexChanged="ddlGridPageSize_SelectedIndexChanged">
                                                            <asp:ListItem Value="10" Selected="True">10</asp:ListItem>
                                                            <asp:ListItem Value="20">20</asp:ListItem>
                                                            <asp:ListItem Value="30">30</asp:ListItem>
                                                            <asp:ListItem Value="50" >50</asp:ListItem>
                                                            <asp:ListItem Value="100">100</asp:ListItem>
                                                            <asp:ListItem Value="200">200</asp:ListItem>
                                                            <asp:ListItem Value="0">all</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label6" runat="server" Text="Page:"></asp:Label>
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
                                                    <td style="width: 2px;"></td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
            <div id="dvControls1" style="width: 100%;">
            </div>
        </div>





























        <div id="dvContentFooter" class="dvContentFooter">
            <div id="dvContentFooterInner" class="dvContentFooterInner">
                <div style="width: 100%; height: 100%; margin-bottom: 0px;">
                    <div style="width: auto; min-width: 300px; height: auto; text-align: left;">
                        <table border="0">
                            <tr>
                                <td style="width: 100px;"></td>
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
                                <td></td>

                                <td style="width: 20px;"></td>
                                <td style="width: 10px;"></td>
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



    </div>--%>
</asp:Content>
