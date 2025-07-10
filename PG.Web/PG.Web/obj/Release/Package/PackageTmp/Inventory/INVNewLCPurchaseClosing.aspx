<%@ Page Title="" Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="INVNewLCPurchaseClosing.aspx.cs" Inherits="PG.Web.Inventory.INVNewLCPurchaseClosing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="../javascript/jquery.ui.combogrid.js" type="text/javascript"></script>
    <link href="../css/jquery.ui.combogrid.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .taBorder{
            border:1px solid gray;
        }

    </style>

    <script language="javascript" type="text/javascript">
        // <!CDATA[

        var isPageResize = true;
        ContentForm.CalendarImageURL = "../image/calendar.png";

       <%-- var hdnCompanyID = '<%=hdnCompanyID.ClientID%>';--%>

        var ReportViewPageLink = '<%=this.ReportViewPageLink%>';
        var ReportViewPDFPageLink = '<%=this.ReportViewPDFPageLink%>';
        var ReportPrintPageLink = '<%=this.ReportPrintPageLink%>';
        var ReportPDFPageLink = '<%=this.ReportPDFPageLink%>';
        var LCListServiceLink = '<%=this.LCListServiceLink%>';

        var txtInvoiceNo = '<%= txtInvoiceNo.ClientID%>';
        var txtInvoiceDate = '<%= txtInvoiceDate.ClientID%>';
        var txtPurchaseDate = '<%= txtPurchaseDate.ClientID%>';


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

        function UserDeleteConfirmation() {
            return confirm("Are you sure to Save and Authorized?");
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

        function fromParent(val1) {
            alert('this is called from parent: ' + val1);
        }

        $(document).ready(function () {
            if ($('#' + txtLCNo).is(':visible')) {
                bindLCList();
            }
        });

        function bindLCList() {

            var cgColumns = [
                               { 'columnName': 'lcno', 'width': '120', 'align': 'left', 'highlight': 4, 'label': 'Lc No' }
                             , { 'columnName': 'lcdate', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'LC Date' }
                             , { 'columnName': 'bank_name', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Bank Name' }
                             , { 'columnName': 'lc_type_desc', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Lc Type' }
                             , { 'columnName': 'sup_name', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Supplier Name' }
            ];

            var serviceURL = LCListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;

            serviceURL += "&ispaging=1";

            var lcnoNoElem = $('#' + txtLCNo);
            $('#' + btnLCID).click(function (e) {
                $(lcnoNoElem).combogrid("dropdownClick");
            });

            $(lcnoNoElem).combogrid({
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

                    var newServiceURL = serviceURL;
                    $(this).combogrid("option", "url", newServiceURL);
                },
                select: function (event, ui) {
                    if (!ui.item) {
                        event.preventDefault();
                        //$('#' + txtDealerID).val('');
                        return false;
                    }

                    if (ui.item.lcid == '') {
                        event.preventDefault();
                        return false;
                    }
                    else {
                        // $('#' + hdnDealerID).val(ui.item.dealerid);
                        $('#' + txtLCNo).val(ui.item.lcno);
                        $('#' + hdnLCID).val(ui.item.lcid);
                    }

                    return false;
                },

                lc: ''
            });


            $(lcnoNoElem).blur(function () {
                var self = this;

                var invoiceID = $(lcnoNoElem).val();
                if (invoiceID == '') {
                    // $('#' + hdnDealerID).val('0');
                    $('#' + txtLCNo).val('');
                    $('#' + hdnLCID).val('');
                }
            });
        }


    </script>

    <style type="text/css">
        #dvControlsTab {
            padding: 0px;
            background: none;
            border-width: 0px;
        }

            #dvControlsTab .ui-tabs-nav {
                padding-left: 0px;
                background: transparent;
                border-width: 0px 0px 1px 0px;
                border-radius: 0px;
                -moz-border-radius: 0px;
                -webkit-border-radius: 0px;
            }

            #dvControlsTab .ui-tabs-selected a {
                color: #000;
                font-weight: bold; /*
           border-top: 3px solid #fabd23; 
           border-left: 1px solid #fabd23; 
           border-right: 1px solid #fabd23;
            */
                border-top: 3px solid blue;
                margin-bottom: -1px;
                overflow: visible;
            }

            #dvControlsTab .ui-state-default {
                /*background: transparent;*/ /* border: none; */
            }

                #dvControlsTab .ui-state-default a {
                    /*color: #c0c0c0;*/
                }

            #dvControlsTab .ui-state-active a {
                /* color: #459E00; */
                color: blue;
            }


        .groupBoxContainer {
            height: 100%;
            width: 1024px;
            overflow: auto;
            margin-left: 5px;
            margin-top: 5px;
        }

        .groupHeader {
            height: 20px;
            background-image: url('../../image/header13.png');
            background-repeat: repeat-x;
            color: black;
            font-weight: bold;
        }

        .groupBox {
            background-image: url('../../image/bg_greendot.gif');
            height: 100%;
            width: 100%;
            min-width: 500px;
            display: inline-block;
            text-align: center;
            vertical-align: middle;
        }

        .groupContent {
            width: 100%;
            height: 100%;
        }

        .groupContenInner {
            width: 100%;
            height: auto;
            overflow: auto;
        }


        .subHeader {
            height: 20px;
            width: 100%;
            background-image: url('../../image/header13.png');
            background-repeat: repeat-x;
            color: White;
            vertical-align: middle;
            font-weight: bold;
        }

            .subHeader span {
                margin-left: 2px;
            }


        .groupHeader span {
            margin-left: 2px;
            margin-top: 4px;
        }

        .dvGridDetailsPopup {
            display: none;
            border: 0px solid black;
            height: 0px;
            width: 0px;
        }

        .ui-widget input {
            font-size: 11px;
        }

        .ui-widget select {
            font-size: 11px;
        }


        .dvPopupProject {
            display: none;
            border: 0px solid black;
            height: 0px;
            width: 0px;
        }


        .btnSearch {
            height: 19px;
            width: 16px;
            background-image: url('../../image/search.png');
            background-repeat: no-repeat;
            background-position: center bottom;
            cursor: pointer;
        }

        .dvPopupGLAccount {
            display: none;
            border: 0px solid black;
            width: 0px;
            height: 0px;
        }


        .dvPopupTranType {
            display: none;
            border: 0px solid black;
            width: 0px;
            height: 0px;
        }

        .dvPopupCashTranInfo {
            display: none;
            border: 0px solid black;
            width: 0px;
            height: 0px;
        }






        .dvPopupIns {
            display: none;
            border: 0px solid black;
            width: 0px;
            height: 0px;
        }



        .ui-dialog .ui-dialog-content {
            padding: 2px 0px 0px 0px;
        }

        .ui-dialog .ui-dialog-titlebar {
            padding: 4px 2px 0px 2px;
        }

        .tableRowOdd {
            background-color: #F7F6F3;
        }

        .tableRowEven {
            background-color: White;
        }







        .hidden {
            /*visibility:hidden;*/
            display: none;
        }

        #Text1 {
            width: 538px;
        }

        </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="dvPageContent" style="width: 100%; height: 100%;">

        <div id="dvContentHeader" class="dvContentHeader">
            <div id="dvHeader" class="dvHeader" align="left">
                <asp:Label ID="lblHeader" runat="server" Text="Invoice Close" CssClass="lblHeader" Font-Bold="true" Font-Size="15px"></asp:Label>
            </div>
            <div id="dvMessage" class="dvMessage">
                <asp:Label ID="lblMessage" runat="server" CssClass="lblMessage" Text="" Width="100%"></asp:Label>
            </div>

            <div id="dvHeaderControl" class="dvHeaderControl">
            </div>

        </div>

        <div id="dvContentMain" class="dvContentMain" align="center">
            <div id="dvControlsHead" style="height: auto; width: 100%;" align="center">
                <table cellspacing="2" border="0" align="center" style="width: 100%">
                    
                    <tr>
                       
                         <td align="right">
                            <asp:Label ID="Label6" runat="server" Text="Purchase No:" Style="height: 19px; width: 43px;" Font-Bold="True" Visible="true"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPurchaseNO" runat="server" CssClass="textBox" ReadOnly="true" BackColor="#FFFFCC"></asp:TextBox>
                        </td>
                        <td align="right">
                            <asp:Label ID="Label7" runat="server" Text="Purchase Date:" Style="height: 19px; width: 43px;" Font-Bold="True" Visible="true"></asp:Label>

                        </td>
                        <td>
                            <asp:TextBox ID="txtPurchaseDate" runat="server" CssClass="textBox textDate" Enabled="false"></asp:TextBox>
                        </td>
                        <td align="right">
                            <asp:Label ID="Label1" runat="server" Text="Invoice No:" Style="height: 19px; width: 43px;" Font-Bold="True" Visible="true"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtInvoiceNo" runat="server" CssClass="textBox" Enabled="false" ></asp:TextBox>
                        </td>
                        <td align="right" >
                            <asp:Label ID="Label5" runat="server" Text="Invoice Date:" Style="height: 19px; width: 43px;" Font-Bold="True" Visible="true"></asp:Label>

                        </td>
                        <td>
                            <asp:TextBox ID="txtInvoiceDate" runat="server" CssClass="textBox textDate" Enabled="false" ></asp:TextBox>
                        </td>
                        

                    </tr>

                     <tr>
                      
                        <td align="right">
                            <asp:Label ID="Label9" runat="server" Text="Pur Remarks" Style="height: 19px; width: 43px;" Font-Bold="True" Visible="true"></asp:Label>

                        </td>
                        <td colspan="5" align="left">
                            <asp:TextBox ID="txtPurRemarks" runat="server" CssClass="textBox" TextMode="MultiLine" Height="30px" Width="60%" Enabled="false"></asp:TextBox>
                        </td>
                      <td>
                          <asp:CheckBox  ID="cbIsClosed" runat="server" Text="Is Invoice Closed" Font-Bold="true" />
                      </td>
                         <td></td>
                    </tr>
                    <tr>
                       
                        <td align="right">
                            <asp:Label ID="Label3" runat="server" Text="LC NO" Style="height: 19px; width: 43px;" Font-Bold="True" Visible="true"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblLCNO" runat="server"></asp:Label>
                        </td>
                        <td colspan="2" align="right">
                            <asp:Label ID="lblCustomerName" runat="server" Text="LC Type" Style="height: 19px; width: 43px;" Font-Bold="True" Visible="true"></asp:Label>

                        </td>
                        <td>
                            <asp:Label ID="lblLCType" runat="server"></asp:Label>
                        </td>
                        <td align="right">
                            <asp:Label ID="lblCustomerAddress" runat="server" Text="Bank" Style="height: 19px; width: 43px;" Font-Bold="True" Visible="true"></asp:Label>

                        </td>
                        <td>
                            <asp:Label ID="lblBankName" runat="server"></asp:Label>
                        </td>
                        <td></td>

                    </tr>

                     <tr>
                      
                        <td align="right">
                            <asp:Label ID="Label2" runat="server" Text="LC Date" Style="height: 19px; width: 43px;" Font-Bold="True" Visible="true"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblLCDate" runat="server"></asp:Label>
                        </td>
                        <td align="right">
                            <asp:Label ID="Label4" runat="server" Text="LC Remarks" Style="height: 19px; width: 43px;" Font-Bold="True" Visible="true"></asp:Label>

                        </td>
                            <td align="right" class="auto-style2">
                            <asp:Label ID="Label8" runat="server" Text="Supplier :" Font-Bold="true"></asp:Label>
                        </td>
                        <td align="left" class="auto-style2">
                            <asp:TextBox ID="txtSupplier" runat="server" CssClass="textBox"  Width="150px" Enabled="false"></asp:TextBox>
                            <asp:HiddenField ID="hdnSupplierId" runat="server" /><asp:HiddenField ID="hdnPurMstId" runat="server" />
                        </td>
                        <td colspan="2" align="left">
                            <asp:TextBox ID="txtLCREmarks" runat="server" CssClass="textBox" TextMode="MultiLine" Height="30px" Width="300px" Enabled="false"></asp:TextBox>
                        </td>
                     <td></td>

                    </tr>


                </table>

            </div>
            <br />

            <div id="dvControls" style="height: auto; width: 100%;">
                <div id="dvControlsInner" class="groupBoxContainer boxShadow" style="height: auto; width: 100%;">


                    <div id="dvGridContainer" style="width: 100%; height: 100%;">


                        <div id="dvGrid" style="width: 100%; height: 300px; overflow: auto;">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                                CellPadding="1" CellSpacing="1" ForeColor="#333333" GridLines="None"
                                Font-Names="Arial" Font-Size="9pt" PageSize="15"
                                EmptyDataText="There is no record" Width="100%" Style="margin-bottom: 0px">
                                <PagerSettings Mode="NumericFirstLast" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <Columns>
                                    <asp:TemplateField HeaderText="SL#">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdnPurDtlId" runat="server" Value='<%# Bind("IMP_PUR_DET_ID") %>'  />
                                            <asp:Label ID="lblSLNo" runat="server" Text='<%# Bind("IMP_PUR_DET_SLNO") %>' Style="text-align: center;" Width="80px"> </asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="ITEM_ID" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblItemId" runat="server" Text='<%# Bind("ITEM_ID") %>' Style="text-align: center;" Width="100px"></asp:Label>

                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="UOM_ID" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUomId" runat="server" Text='<%# Bind("UOM_ID") %>' Style="text-align: center;" Width="100px"></asp:Label>

                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Item Name" Visible="true">
                                        <ItemTemplate>
                                            <asp:Label ID="lblItemName" runat="server" Text='<%# Bind("ITEM_NAME") %>' Style="text-align: center;" Width="150px"></asp:Label>

                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="LC Qty" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="txtLCQuantity" CssClass="textNumberOnly" runat="server" Text='<%# Bind("LC_QTY") %>' Style="text-align: center;" Width="100px"></asp:Label>

                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Already MrrQty" Visible="true">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAlreadyMrrQty" runat="server" Text='<%# Bind("ALREADY_MRR_QTY") %>' Style="text-align: center;" Width="100px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Pur Qty" Visible="true">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtPurQuantity" Enabled="false" CssClass="textBox textNumberOnly" Text='<%# Bind("PUR_QTY") %>' runat="server" Style="text-align: center;" Width="100px"></asp:TextBox>                                          
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Less Qty" Visible="true">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtDIF_QTY" Text='<%# Bind("LESS_QTY") %>' CssClass="textBox textNumberOnly" runat="server" Style="text-align: center;" Width="100px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Extra Qty" Visible="true">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtEXTRA_QTY" Text='<%# Bind("EXTRA_QTY") %>' CssClass="textBox textNumberOnly" runat="server" Style="text-align: center;" Width="100px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    
                                     <asp:TemplateField HeaderText="Uom" Visible="true">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUom_Name" runat="server" Text='<%# Bind("UOM_NAME") %>' Style="text-align: center;" Width="50px"></asp:Label>  
                                            
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                      
                                    <asp:TemplateField HeaderText="Unit Price" Visible="false">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtUnitPrice" Text='<%# Bind("PUR_UNIT_RATE") %>' CssClass="textBox textNumberOnly" runat="server" Style="text-align: center;" Width="100px"></asp:TextBox>

                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                  
                                 
                                    <asp:TemplateField HeaderText="Pur Remarks" Visible="true">
                                        <ItemTemplate>

                                            <asp:Label ID="txtPurNote" runat="server" Text='<%# Bind("REMARKS") %>' Style="text-align: center;" Width="100px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" Wrap="true" />
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Adjust Remarks" Visible="true">
                                        <ItemTemplate>
                                             <asp:TextBox ID="txtAdjustNote" TextMode="MultiLine" Text='<%# Bind("DIFF_REMARKS") %>' CssClass="textAreaAutoSize textNumberOnly taBorder" runat="server" Style="text-align: center;"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle   VerticalAlign="Middle" />
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
            </div>

        </div>

        <div id="dvContentFooter" class="dvContentFooter">
            <table align="center">
                <tr>
                    <td>  <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="buttonEdit" OnClick="btnEdit_Click" Visible="false" /></td>
                    <td>
                        <asp:Button ID="btnAddNew" runat="server" Text="New" CssClass="buttonNew" OnClick="btnAddNew_Click" Visible="false" />
                     
                    </td>
                    <td>
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonSave" OnClick="btnSave_Click" OnClientClick="if ( ! UserDeleteConfirmation()) return false;" />
                           <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonCancel checkIsDirty" OnClick="btnCancel_Click" />
                    </td>

                    <td>
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="buttonDelete" Visible="false" />
                    </td>

                    <td>
                        <asp:Button ID="btnRefresh" runat="server" Text="Refresh" CssClass="buttonRefresh checkIsDirty" OnClick="btnRefresh_Click" Visible="false" />
                    </td>


                    <td></td>


                    <td>
                        <input id="btnClose" type="button" runat="server" class="buttonClose" value="Close" onclick="if (ContentForm) { ContentForm.CloseForm(); }" />
                    </td>


                </tr>
            </table>
        </div>


    </div>
</asp:Content>
