<%@ Page Title="" Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="SalesReturnAgainstInvoice.aspx.cs" Inherits="PG.Web.Inventory.SalesReturnAgainstInvoice" %>
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

            var url = IForm.RootPath + "Inventory/SalesReturn.aspx?id=" + key;
            //var url = IForm.RootPath + "Accounting/GeneralLedger/JournalFull.aspx?id=" + key;


            if (IForm.PageMode == Enums.PageMode.InTab) {

                var tdata = new xtabdata();
                tdata.linktype = Enums.LinkType.Direct;
                tdata.id = 0;
                tdata.name = "Sales Return";
                //tdata.label = "User: " + userid;
                tdata.label = "Sales Return";
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
                        $('#' + hdnCustomerID).val('');
                        return false;
                        //ClearGLAccountData(elemID);
                    }


                    if (ui.item.customerid == '') {
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
                    $('#' + hdnCustomerID).val('0');
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

<%--    <style type="text/css">
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

        .auto-style2 {
            height: 24px;
        }
    </style>  --%>
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
    <div id="dvPageContent" style="width: 100%; height: auto;">
        <div id="dvContentHeader" class="dvContentHeader">
            <div id="dvHeader" class="dvHeader">
                <asp:Label ID="lblHeader" CssClass="lblHeader" runat="server" Text="Sales Return Against Invoice:"></asp:Label>
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
               <td></td>
                                                        <td align="right" class="auto-style2">
                                                            <asp:Label ID="lblCustomer" runat="server" Text="Customer:" Font-Bold="true"></asp:Label>
                                                        </td>

                                                        <td class="auto-style2">

                                                            <asp:TextBox ID="txtCustomerName" runat="server" CssClass="textBox" Enabled="true"></asp:TextBox>
                                                             <input id="btnCustomerID" type="button" value="" runat="server" class="buttonDropdown"
                                                                tabindex="-1" />
                                                        </td>
                                                        
                                                        <td colspan="2" align="left">

                                                            <asp:TextBox ID="txtCustomerAddress" runat="server" CssClass="textBox" Enabled="false" Width="200px"></asp:TextBox>
                                                            <%--<asp:TextBox ID="txtCustomerID" runat="server" CssClass="textBox" Visible="true" Width="10px"></asp:TextBox>--%>
                                                            
                                                        </td>

             

                                                    </tr>

            <tr>
              <td>
              </td>
              <td>
               <asp:Label ID="lblFromDate" runat="server" Text="Invoice From Date" style="height: 19px; width: 43px; " Font-Bold="True" Visible="true"></asp:Label>
              </td>
              <td align="left">
                 <asp:TextBox ID="txtFromDate" runat="server" CssClass="textBox textDate dateParse" Style="text-align: left;" Width="100px"></asp:TextBox>
                <%-- <asp:RegularExpressionValidator runat="server" ControlToValidate="txtFromDate" ValidationExpression="(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$" ErrorMessage="Invalid date format." ValidationGroup="Group1" ForeColor="Red" />--%>
              </td>
              <td>
                    <asp:Label ID="lblInvoiceToDate" runat="server" Text="To Date" style="height: 19px; width: 43px; " Font-Bold="True" Visible="true"></asp:Label>
                   
              </td>
            
              <td>
                 <asp:TextBox ID="txtTodate" runat="server" CssClass="textBox textDate dateParse" Style="text-align: left;" Width="100px"></asp:TextBox>&nbsp;
                   
                 <asp:RegularExpressionValidator runat="server" ControlToValidate="txtTodate" ValidationExpression="(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$" ErrorMessage="Invalid date format." ValidationGroup="Group1" ForeColor="Red" />
              </td>
               
             
                 
            </tr>

        

           <tr>
              <td>
              </td>
              <td>
            
              </td>
              <td>
                <asp:Button ID="btnFind" runat="server" Text="Show Data" CssClass="buttonRefresh checkIsDirty" OnClick="btnFind_Click"  />
              </td>
              <td>
                  
                   
              </td>
              
               
              <td>
                <asp:HiddenField ID="hdnCustomerID" runat="server" Value="0"  />
              </td>
               
            </tr>
         
         
            
         </table>    
            </div>
            <br />
            <div id="dvControls" style="width: 100%;">
                <div id="dvControlsInner" class="groupBoxContainer boxShadow" style="width: 1024px">
                    <div id="dvGridContainer" style="width: 100%; height: auto; text-align: left;">
                        <div id="dvGridHeader" style="width: 100%; height: 25px; font-size: smaller;" class="subHeader">
                           <table style="height: 100%; color: #FFFFFF; font-weight: bold; text-align: center;" class="defFont"  cellspacing="1" cellpadding="1" rules="all">
                                <tr>
                                    <td width="80px" align="left"></td>
                                    <%-- <td width="52px" align="left">SL#</td>--%>
                                  <%--  <td width="152px" align="left">Invoice Id
                                    </td>--%>
                                    <td width="130px" align="left">Invoice No
                                    </td>
                                    <td width="100px" align="left">DC Date
                                    </td>
                                    <td width="130px" align="left">DC Time
                                    </td>
                                     <td width="130px" align="left">DC NO
                                    </td>
                                     <td width="130px" align="left">GP NO
                                    </td>
                                     <td width="130px" align="left">Customer
                                    </td>
                                     <td width="200px" align="left">Remarks
                                    </td>
                                    
                                    
                                </tr>
                            </table>
                        </div>
                        <div id="dvGrid" style="width: auto; height: 350px; overflow: auto;">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="1"
                                CellSpacing="1" ForeColor="#333333" GridLines="None" Font-Names="Arial" Font-Size="9pt"
                                OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="GridView1_RowDeleting"
                                EmptyDataText="There is no record" PageSize="15" OnPageIndexChanging="GridView1_PageIndexChanging"
                                OnSelectedIndexChanged="GridView1_SelectedIndexChanged" ShowHeader="False">
                                <PagerSettings Mode="NumericFirstLast" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <Columns>
                                    <asp:HyperLinkField HeaderText="" Text="RTN/Cancel">
                                        <ControlStyle Height="20px" Width="80px" />
                                        <ItemStyle Width="80px" />
                                    </asp:HyperLinkField>

                                  <%--  <asp:TemplateField HeaderText="SL#">
                                       <ItemTemplate>
                                       <asp:Label ID="lblSLNo" runat="server" Text='<%# Bind("SLNO") %>' Style="text-align: center;" Width="60px"> </asp:Label></ItemTemplate>
                                       <ItemStyle VerticalAlign="Top" />
                                   </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Invoice ID" Visible="false">
                                  <ItemTemplate>
                                 <asp:Label ID="lblINVOICEID" runat="server" Text='<%# Bind("INVOICE_ID") %>' Style="text-align: center;" Width="150px"></asp:Label>
                                                                   
                                  </ItemTemplate>
                                   <ItemStyle VerticalAlign="Top" />
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="DC ID" Visible="false">
                                  <ItemTemplate>
                                 <asp:Label ID="lblINVOICEID" runat="server" Text='<%# Bind("DC_ID") %>' Style="text-align: center;" Width="150px"></asp:Label>
                                                                   
                                  </ItemTemplate>
                                   <ItemStyle VerticalAlign="Top" />
                                </asp:TemplateField>
                                    <asp:BoundField DataField="SALES_INVOICE_NO" HeaderText="Invoice no" ItemStyle-Width="130px" ItemStyle-HorizontalAlign="Left" />
                                      
                                    <asp:BoundField DataField="DC_DATE" HeaderText="DC Date" DataFormatString="{0:dd-MMM-yyyy}" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Left" />

                                    <asp:BoundField DataField="CREATE_DATE" HeaderText="DC Time"  DataFormatString="{0:hh:mm tt}"  ItemStyle-Width="130px" ItemStyle-HorizontalAlign="Left" />

                                     <asp:BoundField DataField="DC_NO" HeaderText="DC NO" ItemStyle-Width="130px" ItemStyle-HorizontalAlign="Left" />
                                     <asp:BoundField DataField="GP_NO" HeaderText="GP NO" ItemStyle-Width="130px" ItemStyle-HorizontalAlign="Left" />

                                     <asp:BoundField DataField="CUST_NAME" HeaderText="Cust Nmae" ItemStyle-Width="130px" ItemStyle-HorizontalAlign="Left" />

                                    <asp:BoundField DataField="DC_REMARKS" HeaderText="Remarks" ItemStyle-Width="200px" ItemStyle-HorizontalAlign="Left" />
                                  
                                  
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
        </div>
    </div>
</asp:Content>