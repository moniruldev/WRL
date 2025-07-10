<%@ Page Language="C#"  MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="DC_GP_StatusViewAndReportForRotaryUnit.aspx.cs" Inherits="PG.Web.Inventory.DC_GP_StatusViewAndReportForRotaryUnit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

 <script src="../javascript/jquery.ui.combogrid.js" type="text/javascript"></script>
    <script src="../javascript/jquery.attributeobserver.js" type="text/javascript"></script>
    <link href="../css/jquery.ui.combogrid.css" rel="stylesheet" type="text/css" />


    <script language="javascript" type="text/javascript">
        // <!CDATA[

        var isPageResize = true;
        ContentForm.CalendarImageURL = "../image/calendar.png";

       <%-- var hdnCompanyID = '<%=hdnCompanyID.ClientID%>';--%>

        var ReportViewPageLink = '<%=this.ReportViewPageLink%>';
        var ReportViewPDFPageLink = '<%=this.ReportViewPDFPageLink%>';
        var ReportPrintPageLink = '<%=this.ReportPrintPageLink%>';
        var ReportPDFPageLink = '<%=this.ReportPDFPageLink%>';
        var CustomerListServiceLink = '<%=this.CustomerListServiceLink%>';
        <%--var InvoiceListServiceLink = '<%=this.InvoiceListServiceLink%>';--%>


      <%--  var txtCustomerName = '<%=txtCustomerName.ClientID%>';
        var btnCustomerID = '<%=btnCustomerID.ClientID%>';--%>

      <%--  var txtCustomerID = '<%=txtCustomerID.ClientID%>';--%>
        <%--var hdnCustomerID = '<%=hdnCustomerID.ClientID%>';
        var txtCustomerAddress = '<%=txtCustomerAddress.ClientID%>';--%>

        <%--var txtInvoiceNo = '<%=txtInvoiceNo.ClientID%>';
        var btnInvoiceID = '<%=btnInvoiceID.ClientID%>';

        var hdnInvoiceID = '<%=hdnInvoiceID.ClientID%>';--%>


        var btnGridPageGoTo = '<%=btnGridPageGoTo.ClientID %>';
        var txtGridPageNo = '<%=txtGridPageNo.ClientID %>';


        function ShowProgress() {
            $('#' + updateProgressID).show();
        }

        function UserConfirmation() {
            return confirm("Are you sure you want to Stock Issue and Print DC,GP ?");
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


        function tbopen(key, userid) {
            key = key || '';

            var url = IForm.RootPath + "Inventory/NewDCGPAgainstInvoice.aspx?id=" + key;
            //var url = IForm.RootPath + "Accounting/GeneralLedger/JournalFull.aspx?id=" + key;


            if (IForm.PageMode == Enums.PageMode.InTab) {

                var tdata = new xtabdata();
                tdata.linktype = Enums.LinkType.Direct;
                tdata.id = 0;
                tdata.name = "DC and GP Against Invoice";
                //tdata.label = "User: " + userid;
                tdata.label = "DC and GP Against Invoice";
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

        // alert('OK');
        $(document).ready(function () {

            var pageInstance = Sys.WebForms.PageRequestManager.getInstance();

            pageInstance.add_pageLoaded(function (sender, args) {
                var panels = args.get_panelsUpdated();
                for (i = 0; i < panels.length; i++) {
                    //alert(panels[i].id);
                    //ContentForm.InitDefualtFeatureInScope(panels[i].id);

                    if (panels[i].id == gridUpdatePanelIDDet) {
                        bindItemList(gridViewIDDet);
                        bindItemGroupList(gridViewIDDet);
                    }

                }
                //$('#' + dvGridDetailsPopup).parent().appendTo(jQuery("form:first"));
                //gridTaskAfter();

            });

            //alert('OK 1');
            if ($('#' + txtCustomerName).is(':visible')) {
                //alert('OK 2');
                bindCustomerList();
                //alert('OK 3');
            }
            //alert('OK 1');

            //if ($('#' + txtInvoiceNo).is(':visible')) {

            //    bindInvoiceList();
            //}

        });


        function bindCustomerList() {
            var cgColumns = [{ 'columnName': 'custname', 'width': '180', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                             , { 'columnName': 'custcode', 'width': '50', 'align': 'left', 'highlight': 4, 'label': 'Code' }
                             , { 'columnName': 'custaddress', 'width': '100', 'align': 'left', 'highlight': 0, 'label': 'address' }
                             , { 'columnName': 'custphone', 'width': '150', 'align': 'left', 'highlight': 0, 'label': 'Phone' }


            ];


            //var companyid = $('#' + hdnCompanyID).val();
            //var depthead = $('#' + hdnEmpCode).val();
            //var locationid = $('#' + ddlLocation).val();
            // var seid = $('#' + txtExecutiveID).val();

            //var serviceURL = GLAccountServiceLink + "?isterm=1&includeempty=1&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            var serviceURL = CustomerListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            //serviceURL += "&companyid=" + companyid;
            serviceURL += "&ispaging=1";
            // serviceURL += "&locationid=" + locationid;
            //serviceURL += "&seid=" + seid;
            // serviceURL += "&empstatus=" + "A";
            //serviceURL += "&acctypefilter=" + Enums.GLAccountTypeFilter.AllAccount;
            // serviceURL += "&namecomptype=" + Enums.DataCompareType.Contains;



            var customerIDElem = $('#' + txtCustomerName);

            $('#' + btnCustomerID).click(function (e) {
                //elmID = $(elem).attr('id');
                //$(elem).combogrid("show");
                $(customerIDElem).combogrid("dropdownClick");
            });


            $(customerIDElem).combogrid({
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
                width: 700,
                url: serviceURL,
                search: function (event, ui) {
                    //var companyCode = $('#' + ddlCompany).val();
                    //var branchCode = $('#' + hdnBranch).val();
                    //var deptCode = $('#' + hdnDepartment).val();
                    //var locationid = $('#' + lblLocationID).val();
                    // var seid = $('#' + txtExecutiveID).val();
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
                        $('#' + hdnCustomerID).val(ui.item.customerid);
                        $('#' + txtCustomerName).val(ui.item.custname);
                        $('#' + txtCustomerAddress).val(ui.item.custaddress);

                    }
                    return false;
                },

                lc: ''
            });


            $(customerIDElem).blur(function () {
                var self = this;

                var customerID = $(customerIDElem).val();
                if (customerID == '') {
                    // $('#' + hdnDealerID).val('0');
                    $('#' + txtCustomerName).val('');
                    $('#' + txtCustomerAddress).val('');
                }
            });
        }

      
    </script>

   <style type="text/css">
        /*.FixedHeader { POSITION: relative; TOP: expression(this.offsetParent.scrollTop); BACKGROUND-COLOR: white } */

        .FixedHeader {
            position: relative;
            background-color: white;
        }

        #dvMessage {
            height: 20px;
        }
    </style>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    

    <div id="dvPageContent" style="width: 100%; height: 100%;">

        <div id="dvContentHeader" class="dvContentHeader">
            <div id="dvHeader" class="dvHeader" align="left">
                <asp:Label ID="lblHeader" runat="server" Text="DC and GP Reprint" CssClass="lblHeader" Font-Bold="true" Font-Size="15px"></asp:Label>
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
                 <td>
                   </td>
                 <td style="" align="left">
                   <asp:Label id="Label6" runat="server" Text="Invoice No." ></asp:Label>
                 </td>
                 <td style="" align="left">
                      
                    <asp:TextBox id="txtInvoiceNo" runat="server" CssClass="textBox  enableIsDirty" 
                        width="235px"></asp:TextBox>
                     
                      </td>
                  <td style="" align="left">
                      </td>
                 <td style="" align="left">
                   </td>
                     
                      <td>
                     </td>
                 </tr>
                   <tr>
                 <td>
                   </td>
                 <td style="" align="left">
                   <asp:Label id="Label12" runat="server" Text="Customer" ></asp:Label>
                 </td>
                 <td style="" align="left">
                   <asp:DropDownList ID="ddlCustomer" runat="server" Width="240"  CssClass="dropDownList enableIsDirty"> </asp:DropDownList>
                 </td>
                  <td style="" align="left">
                      </td>
                 <td style="" align="left">
                     </td>
                     
                     <td>
                     </td>
                 </tr>
                   
                   <tr>
                 <td>
                   </td>
                 <%--<td style="" align="left">
                   <asp:Label id="lblApproveType" runat="server" Text="Type" ></asp:Label>
                 </td>
                 <td style="" align="left">
                     <asp:DropDownList ID="ddlIRRCondition" runat="server" Width="240"  CssClass="dropDownList enableIsDirty">
                        
                         
                         <asp:ListItem Text="Received(IRR)" Value="1"></asp:ListItem>
                    </asp:DropDownList>
                 </td>--%>
                  <td style="" align="left">
                      </td>
                 <td style="" align="left">
                     </td>
                     
                     <td>
                     </td>
                 </tr>
                   <tr class="rowParam">
                  <td>
                  </td>
                  <td align="left">
                  <asp:Label ID="Label13" runat="server" Text="Date From:"></asp:Label>
                   </td>
                  <td>
                     <table cellpadding="0" cellspacing="0">
                     <tr>
                     <td>
                      <asp:TextBox ID="txtFromDate" runat="server" Width="70px" CssClass="textBox textDate dateParse"></asp:TextBox>
                      </td>
                      <td style="width: 4px;">
                      </td>
                      <td>
                      <asp:Label ID="Label14" runat="server" Text="Date To:"></asp:Label>
                      </td>
                        <td style="width: 2px;">
                         </td>                               
                          <td>                                                              
                          <asp:TextBox ID="txtToDate" runat="server" Width="70px" CssClass="textBox textDate dateParse"></asp:TextBox>
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
                 <td style="" align="left">
                   
                 </td>
                 <td style="" align="left">
                      
                     <asp:Button ID="btnSearch" runat="server"  CssClass="buttonRefresh" style="padding-left:22px;"
                Text="Show Data" onclick="btnSearch_Click" />
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
            <div id="dvControls" style="width: 100%;">
                <div id="dvControlsInner" class="groupBoxContainer boxShadow" style="width: 100%">
                    <div id="dvGridContainer" style="width: 100%; height: auto; text-align: left;">
                        <div id="dvGridHeader" style="width: 100%; height: 25px; font-size: smaller;" class="subHeader">
                           <table style="height: 100%; color: #FFFFFF; font-weight: bold; text-align: center;" class="defFont"  cellspacing="1" cellpadding="1" rules="all">
                                 <tr>
                                    <%--<td width="52px" align="left">
                                    </td>--%>
                                    <td width="150px" align="left">
                                       Invoice No
                                    </td>
                                    <td width="120px" align="left">
                                       Invoice Date
                                    </td>
                                     <td width="160px" align="left">
                                       Invoice Time
                                    </td>
                                    
                                    <td width="150px" align="left">
                                       Customer
                                    </td>
                                    <td width="130px" align="left">
                                       DC No
                                    </td>
                                    <td width="130px" align="left">
                                      GP No
                                    </td>
                                    <td width="150px" align="left">
                                        DC Date
                                    </td>
                                    <td width="120px" align="left">
                                       GP Date
                                    </td>
                                    <%--<td width="120px" align="left">
                                       Invoice Status
                                    </td>--%>
                                     <td width="150px" align="left">
                                       Type Qty
                                    </td>
                                   <td width="100px" align="left">
                                       DC Reprint
                                   </td>
                                    <td width="100px" align="left">
                                        GP Reprint
                                    </td>

                                </tr>
                            </table>
                        </div>
                        <div id="dvGrid" style="width: auto; height: 350px; overflow: auto;">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="1"
                                CellSpacing="1" ForeColor="#333333" GridLines="None" Font-Names="Arial" Font-Size="9pt"
                                OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="GridView1_RowDeleting"
                                EmptyDataText="There is no record" PageSize="15" OnPageIndexChanging="GridView1_PageIndexChanging"
                                OnSelectedIndexChanged="GridView1_SelectedIndexChanged" ShowHeader="False" OnRowCommand="GridView1_RowCommand">
                                <PagerSettings Mode="NumericFirstLast" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                 <Columns>
                                   <%-- <asp:HyperLinkField HeaderText="" Text="">
                                        <ControlStyle CssClass="buttonViewGrid" Height="20px" Width="40px" />
                                        <ItemStyle Width="50px" />
                                    </asp:HyperLinkField>--%>
                                     
                                       <asp:TemplateField HeaderText="INV NO" Visible="false">
                                      <ItemTemplate>
                                     <asp:Label ID="lblInvid" runat="server" Text='<%# Bind("INVOICE_ID") %>' Style="text-align: center;" Width="150px"></asp:Label>
                                                                   
                                      </ItemTemplate>
                                       <ItemStyle VerticalAlign="Top" />
                                     </asp:TemplateField>
                                      <asp:TemplateField HeaderText="IsRotary" Visible="false">
                                      <ItemTemplate>
                                     <asp:Label ID="lblIsRotary" runat="server" Text='<%# Bind("IS_ROTARY") %>' Style="text-align: center;" Width="150px"></asp:Label>
                                                                   
                                      </ItemTemplate>
                                       <ItemStyle VerticalAlign="Top" />
                                     </asp:TemplateField>

                                      <asp:TemplateField HeaderText="DC NO" Visible="true">
                                      <ItemTemplate>
                                     <asp:Label ID="lblInvno" runat="server" Text='<%# Bind("INVOICE_NO") %>' Style="text-align: center;" Width="150px"></asp:Label>
                                                                   
                                      </ItemTemplate>
                                       <ItemStyle VerticalAlign="Top" />
                                     </asp:TemplateField>
                                  <%--  <asp:BoundField DataField="INVOICE_NO" HeaderText="Purchase No" ItemStyle-Width="150px" />--%>
                                    <asp:BoundField DataField="INVOICE_DATE" HeaderText="Date" DataFormatString="{0:dd-MMM-yyyy}" ItemStyle-Width="120px" />
                                    <asp:BoundField DataField="INVOICE_TIME" HeaderText="INVOICE_TIME" ItemStyle-Width="160px" />
                                    
                                    <asp:BoundField DataField="CUST_NAME" HeaderText="Customer" ItemStyle-Width="150px" />
                                    <asp:TemplateField HeaderText="DC NO" Visible="true">
                                      <ItemTemplate>
                                     <asp:Label ID="lblDCNO" runat="server" Text='<%# Bind("DC_NO") %>' Style="text-align: center;" Width="130px"></asp:Label>
                                                                   
                                      </ItemTemplate>
                                       <ItemStyle VerticalAlign="Top" />
                                     </asp:TemplateField>
                                      <asp:TemplateField HeaderText="GP NO" Visible="true">
                                      <ItemTemplate>
                                     <asp:Label ID="lblGPNO" runat="server" Text='<%# Bind("GP_NO") %>' Style="text-align: center;" Width="130px"></asp:Label>
                                                                   
                                      </ItemTemplate>
                                       <ItemStyle VerticalAlign="Top" />
                                     </asp:TemplateField>
                                    <asp:BoundField DataField="DC_DATE" HeaderText="Date" DataFormatString="{0:dd-MMM-yyyy}" ItemStyle-Width="120px" />
                                  
                                    <asp:BoundField DataField="GP_DATE" HeaderText="Date" DataFormatString="{0:dd-MMM-yyyy}" ItemStyle-Width="120px" />
                                    <%-- <asp:BoundField DataField="INVOICE_STATUS" HeaderText="INVOICE_STATUS" ItemStyle-Width="120" />--%>
                                   <asp:BoundField DataField="ITEMTYPEQTY" HeaderText="ITEMTYPEQTY" ItemStyle-Width="150px" />
                                    <asp:TemplateField HeaderText="DC Reprint">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnLinkDc" runat="server" CommandName="DC_RP" Width="100px" Font-Bold="true" >DC Reprint</asp:LinkButton>
                                    </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="GP Reprint">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnLinkGP" runat="server" CommandName="GP_RP" Width="100px"  Font-Bold="true" >GP Reprint</asp:LinkButton>
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
                                                        <asp:Label ID="Label1" runat="server" Text="Page Size:"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlGridPageSize" runat="server" CssClass="dropDownList" Width="50"
                                                            Height="18" AutoPostBack="True" OnSelectedIndexChanged="ddlGridPageSize_SelectedIndexChanged">
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
            <table>
                <tr>
                    <td></td>
                     <td>
                                <asp:DropDownList ID="ddlReportViewType" runat="server" CssClass="dropDownList">
                                        <asp:ListItem Value="0">Screen</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="1">PDF</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                     <td>
                                    <asp:DropDownList ID="ddlReportViewMode" runat="server" CssClass="dropDownList">
                                        <asp:ListItem Value="0">In This Tab</asp:ListItem>
                                        <asp:ListItem Value="1">In New Tab</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="2">In New Window</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                    <td>
                       
                    </td>
                    <td>
                       
                    </td>

                   

                    <td>
                        
                    </td>
                    <td>
                        
                    </td>
                    <td>
                       
                    </td>
                    <td>
                        <%--<uc:PrintButton runat="server" ID="ucPrintButton" DefaultPrintAction="Preview" AutoPrint="False" />--%>
                    </td>
                    <td>
                        <input id="btnClose" type="button" runat="server" class="buttonClose" value="Close" onclick="if (ContentForm) { ContentForm.CloseForm(); }" />
                    </td>
                    <td></td>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="Print Format:"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlReportFormat" runat="server" CssClass="dropDownList" Width="100px">
                        </asp:DropDownList>
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














































