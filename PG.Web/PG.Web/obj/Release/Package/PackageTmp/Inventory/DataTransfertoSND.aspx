<%@ Page Title="" Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="DataTransfertoSND.aspx.cs" Inherits="PG.Web.Inventory.DataTransfertoSND" %>
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


        var txtCustomerName = '<%=txtCustomerName.ClientID%>';
        var btnCustomerID = '<%=btnCustomerID.ClientID%>';

      <%--  var txtCustomerID = '<%=txtCustomerID.ClientID%>';--%>
        var hdnCustomerID = '<%=hdnCustomerID.ClientID%>';
        var txtCustomerAddress = '<%=txtCustomerAddress.ClientID%>';

        <%--var txtInvoiceNo = '<%=txtInvoiceNo.ClientID%>';
        var btnInvoiceID = '<%=btnInvoiceID.ClientID%>';

        var hdnInvoiceID = '<%=hdnInvoiceID.ClientID%>';--%>


        var btnGridPageGoTo = '<%=btnGridPageGoTo.ClientID %>';
        var txtGridPageNo = '<%=txtGridPageNo.ClientID %>';


        function ShowProgress() {
            $('#' + updateProgressID).show();
        }

        function UserDeleteConfirmation() {
            return confirm("Are you sure you want to Save and Transfer data To SND System?");
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


        //function tbopen(key, isPrint, isPDFAutoPrint, showWait) {
        //    key = key || '';
        //    isPrint = isPrint || false;
        //    showWait = showWait || true;

        //    if (isPrint) {
        //        if (key != '') {
        //            ReportPrint(key, isPDFAutoPrint);
        //            return;
        //        }
        //    }

        //    //var url = "/Report/ReportView.aspx?rk=" + key

        //    var now = new Date();
        //    var strTime = now.getTime().toString();
        //    var url = ReportViewPageLink + "?rk=" + key + "&_tt=" + strTime;
        //    //var url = ReportViewPageLink + "?rk=" + key;

        //    //if (pageInTab == 1)
        //    if (TabVar.PageMode == Enums.PageMode.InTab) {

        //        var tdata = new xtabdata();
        //        tdata.linktype = Enums.LinkType.Direct;
        //        tdata.id = 7999;
        //        tdata.name = "Report view";
        //        //tdata.label = "User: " + userid;
        //        tdata.label = "Report view";
        //        tdata.type = 0;
        //        tdata.url = url;
        //        tdata.tabaction = Enums.TabAction.InNewTab;
        //        tdata.selecttab = 1;
        //        tdata.reload = 0;
        //        tdata.param = "";
        //        tdata.showWait = showWait;

        //        try {
        //            //window.parent.OpenMenuByData(tdata);
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

        //function bindInvoiceList() {
        //    var cgColumns = [{ 'columnName': 'invoiceid', 'width': '80', 'align': 'left', 'highlight': 2, 'label': 'Inv ID' }
        //                     , { 'columnName': 'invoiceNo', 'width': '120', 'align': 'left', 'highlight': 4, 'label': 'Inv No' }
        //                     //, { 'columnName': 'invoiceDate', 'width': '180', 'align': 'left', 'highlight': 4, 'label': 'Inv Date' }



        //    ];


        //    //var companyid = $('#' + hdnCompanyID).val();
        //    //var depthead = $('#' + hdnEmpCode).val();
        //    //var locationid = $('#' + ddlLocation).val();
        //    // var seid = $('#' + txtExecutiveID).val();

        //    //var serviceURL = GLAccountServiceLink + "?isterm=1&includeempty=1&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
        //    var serviceURL = InvoiceListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
        //    //serviceURL += "&companyid=" + companyid;
        //    serviceURL += "&ispaging=1";
        //    // serviceURL += "&locationid=" + locationid;
        //    //serviceURL += "&seid=" + seid;
        //    // serviceURL += "&empstatus=" + "A";
        //    //serviceURL += "&acctypefilter=" + Enums.GLAccountTypeFilter.AllAccount;
        //    // serviceURL += "&namecomptype=" + Enums.DataCompareType.Contains;



        //    var invoicenoElem = $('#' + txtInvoiceNo);

        //    $('#' + btnInvoiceID).click(function (e) {
        //        //elmID = $(elem).attr('id');
        //        //$(elem).combogrid("show");
        //        $(invoicenoElem).combogrid("dropdownClick");
        //    });


        //    $(invoicenoElem).combogrid({
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
        //                $('#' + txtDealerID).val('');
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
        //                $('#' + txtInvoiceNo).val(ui.item.invoiceNo);
        //                $('#' + hdnInvoiceID).val(ui.item.invoiceid);
        //            }
        //            return false;
        //        },

        //        lc: ''
        //    });


        //    $(invoicenoElem).blur(function () {
        //        var self = this;

        //        var invoiceID = $(invoicenoElem).val();
        //        if (invoiceID == '') {
        //            // $('#' + hdnDealerID).val('0');
        //            $('#' + txtInvoiceNo).val('');
        //            $('#' + hdnInvoiceID).val('');
        //        }
        //    });
        //}









        // ]]>
    </script>
    <style type="text/css">
        /*.FixedHeader { POSITION: relative; TOP: expression(this.offsetParent.scrollTop); BACKGROUND-COLOR: white } */
        
        .FixedHeader
        {
            position: relative;
            background-color: white;
        }
        
        #dvMessage
        {
            height: 20px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="dvPageContent" style="width: 100%; height: auto;">
        <div id="dvContentHeader" class="dvContentHeader">
            <div id="dvHeader" class="dvHeader">
                <asp:Label ID="lblHeader" CssClass="lblHeader" runat="server" Text="Data Transfer to SND"></asp:Label>
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
                       
                        <td align="right">
                            <asp:Label ID="Label5" runat="server" Text="Invoice No:" Visible="false"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtInvoiceNo" runat="server" CssClass="textBox notEnterToTab" Width="135" Visible="false"></asp:TextBox>
                        </td>

                        
                      
                        <td>
                        </td>
                    </tr>

                    <tr>
                          <td align="right" class="auto-style2">
                                                            <asp:Label ID="lblCustomer" runat="server" Text="Customer:" Font-Bold="true" Visible="false"></asp:Label>
                                                        </td>

                                                        <td >

                                                            <asp:TextBox ID="txtCustomerName" runat="server" CssClass="textBox" Enabled="true" Visible="false"></asp:TextBox>
                                                             <input id="btnCustomerID" type="button" value="" runat="server" class="buttonDropdown"
                                                                tabindex="-1" Visible="false" />
                                                        </td>
                                                        
                                                        <td colspan="2" align="left">

                                                            <asp:TextBox ID="txtCustomerAddress" runat="server" CssClass="textBox" Enabled="false" Width="200px" Visible="false"></asp:TextBox>
                                                            <%--<asp:TextBox ID="txtCustomerID" runat="server" CssClass="textBox" Visible="true" Width="10px"></asp:TextBox>--%>
                                                            
                                                        </td>
                    </tr>

                     <tr>
                          <td align="right" class="auto-style2">
                                                            <asp:Label ID="lblBatteryType" runat="server" Text="Battery Type:" Font-Bold="true" Visible="true"></asp:Label>
                                                        </td>

                                                        <td >
                                                          <asp:DropDownList ID="ddlBatteryType" runat="server"></asp:DropDownList>

                                                        </td>
                                                        
                                                        <td colspan="2" align="left">

                                                            
                                                            
                                                        </td>
                    </tr>

                    <tr>
                        <td align="right">
                            <asp:Label ID="lblDateFrom" runat="server" Text="Date From:" Font-Bold="true"></asp:Label>
                        </td>
                        <td colspan="2">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtFromDate" runat="server" Width="80px" CssClass="textBox textDate dateParse"></asp:TextBox>
                                    </td>
                                    <td style="width: 4px;">
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="lblToDate" runat="server" Text="Date To:" Font-Bold="true"></asp:Label>
                                    </td>
                                
                                    <td >
                                        <asp:TextBox ID="txtToDate" runat="server" Width="80px" CssClass="textBox textDate dateParse"></asp:TextBox>
                                    </td>
                                </tr>

                               

                            </table>
                        </td>
                  
                       
                       
                    </tr>

                     <tr>
                                    <td>
                                   
                                    </td>
                          <td >
                                                    <asp:Button ID="btnRefresh" runat="server" CssClass="buttonRefresh" Style="padding-left: 22px;"
                                Text="Show Data" OnClick="btnRefresh_Click" />            
                                                        </td>

                                                        <td >

                                                           
                                                        </td>
                                                        
                                                        <td >

                                                           
                <asp:HiddenField ID="hdnCustomerID" runat="server" Value="0"  />
             
                                                        </td>
                    </tr>
                </table>
            </div>
            <div id="dvControls" style="width: 100%;">
                <div id="dvControlsInner" class="groupBoxContainer boxShadow" style="width: 1124px">
                    <div id="dvGridContainer" style="width: 100%; height: auto; text-align: left;">
                        <div id="dvGridHeader" style="width: 100%; height: 25px; font-size: smaller;" class="subHeader">
                            <table style="height: 100%; color: #FFFFFF; font-weight: bold; text-align: center;"
                                class="defFont" cellspacing="1" cellpadding="1" rules="all">
                                <tr>
                                    <td width="52px" align="left">Select</td>
                                   
                                    <td width="152px" align="left">
                                       Receive No
                                    </td>
                                    <td width="120px" align="left">
                                       Receive Date
                                    </td>
                                     <td width="120px" align="left">
                                       Receive Time
                                    </td>
                                    
                                    <td width="152px" align="left">
                                       Receive Remarks
                                    </td>
                                    <td width="152px" align="left">
                                       Issue Dept
                                    </td>
                                   
                                     <td width="400px" align="left">
                                       Type Qty
                                    </td>
                                   

                                </tr>
                            </table>
                        </div>
                        <div id="dvGrid" style="width: auto; height: 350px; overflow: auto;">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="1"
                                CellSpacing="1" ForeColor="#333333" GridLines="None" Font-Names="Arial" Font-Size="9pt"
                                DataKeyNames="ISSUE_RECEIVE_ID" OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="GridView1_RowDeleting"
                                EmptyDataText="There is no record" PageSize="15" OnPageIndexChanging="GridView1_PageIndexChanging"
                                OnSelectedIndexChanged="GridView1_SelectedIndexChanged" ShowHeader="False">
                                <PagerSettings Mode="NumericFirstLast" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <Columns>
                                <%--    <asp:HyperLinkField HeaderText="" Text="">
                                        <ControlStyle CssClass="buttonViewGrid" Height="20px" Width="40px" />
                                        <ItemStyle Width="50px" />
                                    </asp:HyperLinkField>--%>

                                      <asp:TemplateField HeaderText="Select Data">  
                    
                                        <ItemTemplate>  
                                            <asp:CheckBox ID="CheckBox1" runat="server" Width="52px" Checked="true" />  
                                        </ItemTemplate>  
                                    </asp:TemplateField>  

                                     <asp:TemplateField HeaderText="Receive ID" Visible="false">
                                      <ItemTemplate>
                                     <asp:Label ID="lblReceiveid" runat="server" Text='<%# Bind("ISSUE_RECEIVE_ID") %>' Style="text-align: center;" Width="130px"></asp:Label>
                                                                   
                                      </ItemTemplate>
                                       <ItemStyle VerticalAlign="Top" />
                                     </asp:TemplateField>
                                   
                                    <asp:BoundField DataField="ISSUE_RECEIVE_NO" HeaderText="Receive No" ItemStyle-Width="150px" />
                                    <asp:BoundField DataField="ISSUE_RECEIVE_DATE" HeaderText="Date" DataFormatString="{0:dd-MMM-yyyy}" ItemStyle-Width="120" />
                                    <asp:BoundField DataField="ISSUE_RECEIVE_TIME" HeaderText="ISSUE_RECEIVE_TIME" ItemStyle-Width="120" />
                                    
                                    <asp:BoundField DataField="ISSUE_RCV_REMARKS" HeaderText="Remarks" ItemStyle-Width="150px" />
                                    <asp:BoundField DataField="from_department_name" HeaderText="Dept" ItemStyle-Width="150px" />
               
                                   <asp:BoundField DataField="RCV_TYPE" HeaderText="ITEMTYPEQTY" ItemStyle-Width="400" />
                                   
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
                                                            <asp:ListItem Value="50" Selected="True">50</asp:ListItem>
                                                            <asp:ListItem Value="100">100</asp:ListItem>
                                                            <asp:ListItem Value="200">200</asp:ListItem>
                                                            <asp:ListItem Value="0">all</asp:ListItem>
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
            <table>
                <tr>
                    <td></td>
                     <td>
                                <asp:DropDownList ID="ddlReportViewType" runat="server" CssClass="dropDownList" Visible="false">
                                        <asp:ListItem Value="0">Screen</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="1">PDF</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                     <td>
                                    <asp:DropDownList ID="ddlReportViewMode" runat="server" CssClass="dropDownList" Visible="false">
                                        <asp:ListItem Value="0">In This Tab</asp:ListItem>
                                        <asp:ListItem Value="1">In New Tab</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="2">In New Window</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                    <td>
                        <asp:Button ID="btnAddNew" runat="server" Text="New" CssClass="buttonNew" Visible="false"/>
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonCancel checkIsDirty" />
                    </td>
                    <td>
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonSave checkRequired" AccessKey="s" OnClick="btnSave_Click" OnClientClick="if ( ! UserDeleteConfirmation()) return false;" Width="100px" Height="27px"  />
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="buttonEdit" Visible="false" />
                    </td>

                    <td>
                        <asp:Button ID="btnDcPrint" runat="server" Text="Print DC" CssClass="buttoncommon" Enabled="false" OnClick="btnDcPrint_Click" Visible="false" />
                    </td>

                    <td>
                        <asp:Button ID="btnGpPrint" runat="server" Text="Print GP" CssClass="buttoncommon" Enabled="false" OnClick="btnGpPrint_Click" OnClientClick="if ( ! UserDeleteConfirmation()) return false;" Visible="false" />
                    </td>

                    <td>
                        <asp:Button ID="btnPost" runat="server" Text="Post" CssClass="buttoncommon" Visible="false" />
                    </td>
                    <td>
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="buttonDelete" Visible="false" />
                    </td>
                    <td>
                        <asp:Button ID="Button1" runat="server" Text="Refresh" CssClass="buttonRefresh checkIsDirty" Visible="false" />
                    </td>
                    <td>
                        <%--<uc:PrintButton runat="server" ID="ucPrintButton" DefaultPrintAction="Preview" AutoPrint="False" />--%>
                    </td>
                    <td>
                        <input id="btnClose" type="button" runat="server" class="buttonClose" value="Close" onclick="if (ContentForm) { ContentForm.CloseForm(); }" />
                    </td>
                    <td></td>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text="Print Format:" Visible="false"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlReportFormat" runat="server" CssClass="dropDownList" Width="100px" Visible="false">
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