<%@ Page Title="" Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="INVInvoiceApproveList.aspx.cs" Inherits="PG.Web.Inventory.INVInvoiceApproveList" %>

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

        var InvoiceListServiceLink = '<%=this.InvoiceListServiceLink%>';




        var txtInvoiceNo = '<%=txtInvoiceNo.ClientID%>';
        var btnInvoiceID = '<%=btnInvoiceID.ClientID%>';

        var hdnInvoiceID = '<%=hdnInvoiceID.ClientID%>';
       <%--  var txtCustomerAddress = '<%=txtCustomerAddress.ClientID%>';

        var txtCompanyName = '<%=txtCompanyName.ClientID%>';
        var btnCompanyID = '<%=btnCompanyID.ClientID%>';
        var txtCompanyID = '<%=txtCompanyID.ClientID%>';


        var gridUpdatePanelIDDet = '<%=UpdatePanel1.ClientID%>';
        var gridViewIDDet = '<%=GridView1.ClientID%>';
        var updateProgressID = '<%=UpdateProgress2.ClientID%>';--%>

        <%--var txtGroupName='<%=txtGroupName.ClientID%>'--%>

        <%-- var txtSalesExecutiveName = '<%=txtSalesExecutiveName.ClientID%>';

             var txtReferenceby = '<%=txtReferenceby.ClientID%>';
             var btnReferenceBy = '<%=btnReferenceBy.ClientID%>';
             var lblReferencebyName = '<%=lblReferencebyName.ClientID%>';


             var txtPreparedBy = '<%=txtPreparedBy.ClientID%>';
             var btnPreparedby = '<%=btnPreparedby.ClientID%>';
             var lblPreparedByName = '<%=lblPreparedByName.ClientID%>';

             var lblLocationID = '<%=lblLocationID.ClientID%>';



             var txtAuthorizedBy = '<%=txtAuthorizedBy.ClientID%>';
             var btnAuthorizedBy = '<%=btnAuthorizedBy.ClientID%>';
             var lblAuthorizedByName = '<%=lblAuthorizedByName.ClientID%>';

             var txtLocation = '<%=txtLocation.ClientID%>';--%>

       
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
            return confirm("Are you sure you want to Save and Authorized?");
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


            // alert('OK 1');

            if ($('#' + txtInvoiceNo).is(':visible')) {

                bindInvoiceList();
            }
           
        });

        function bindInvoiceList() {
            var cgColumns = [ { 'columnName': 'invoiceNo', 'width': '120', 'align': 'left', 'highlight': 4, 'label': 'Inv No' }
                             //, { 'columnName': 'invoiceDate', 'width': '180', 'align': 'left', 'highlight': 4, 'label': 'Inv Date' }
                             , { 'columnName': 'customer', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Customer' }
                             , { 'columnName': 'cusaddr', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Address' }
                            


            ];


            //var companyid = $('#' + hdnCompanyID).val();
            //var depthead = $('#' + hdnEmpCode).val();
            //var locationid = $('#' + ddlLocation).val();
            // var seid = $('#' + txtExecutiveID).val();

            //var serviceURL = GLAccountServiceLink + "?isterm=1&includeempty=1&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            var serviceURL = InvoiceListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            //serviceURL += "&companyid=" + companyid;
            serviceURL += "&ispaging=1";
            // serviceURL += "&locationid=" + locationid;
            //serviceURL += "&seid=" + seid;
            // serviceURL += "&empstatus=" + "A";
            //serviceURL += "&acctypefilter=" + Enums.GLAccountTypeFilter.AllAccount;
            // serviceURL += "&namecomptype=" + Enums.DataCompareType.Contains;



            var invoicenoElem = $('#' + txtInvoiceNo);

            $('#' + btnInvoiceID).click(function (e) {
                //elmID = $(elem).attr('id');
                //$(elem).combogrid("show");
                $(invoicenoElem).combogrid("dropdownClick");
            });


            $(invoicenoElem).combogrid({
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
                        $('#' + txtInvoiceNo).val(ui.item.invoiceNo);
                        $('#' + hdnInvoiceID).val(ui.item.invoiceid);
                    }
                    return false;
                },

                lc: ''
            });


            $(invoicenoElem).blur(function () {
                var self = this;

                var invoiceID = $(invoicenoElem).val();
                if (invoiceID == '') {
                    // $('#' + hdnDealerID).val('0');
                    $('#' + txtInvoiceNo).val('');
                    $('#' + hdnInvoiceID).val('');
                }
            });
        }

 




        


        // ]]>
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

        .auto-style2 {
            height: 24px;
        }
    </style>  
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <div  id="dvPageContent" style="width:100%; height:100%;" >
 
  <div id="dvContentHeader" class="dvContentHeader">  
    <div id="dvHeader" class="dvHeader" align="center">
        <asp:Label ID="lblHeader" runat="server" Text="Invoice Approved" CssClass="lblHeader" Font-Bold="true" Font-Size="15px"></asp:Label>
    </div>
    <div id="dvMessage" class="dvMessage" >
        <asp:Label ID="lblMessage" runat="server" CssClass="lblMessage" Text="" Width="100%"></asp:Label>
    </div>
  
     <div id="dvHeaderControl" class="dvHeaderControl">
          
     </div>
    
  </div>  
  
  <div id="dvContentMain" class="dvContentMain" align="center"> 
       <div id="dvControlsHead" style="height:auto;width:100%;" align="center">
      <table style="" cellspacing="2" border="0" align="center">
            <tr>
              <td>
              </td>
              <td>
               <asp:Label ID="lblInvoiceNo" runat="server" Text="Invoice No" style="height: 19px; width: 43px; " Font-Bold="True" Visible="true"></asp:Label>
              </td>
              <td>
                <asp:TextBox ID="txtInvoiceNo" runat="server" CssClass="textBox"  OnTextChanged="txtInvoiceNo_TextChanged" AutoPostBack="true"></asp:TextBox>
              </td>
              <td>
                    <input id="btnInvoiceID" type="button" value="" runat="server" class="buttonDropdown" tabindex="-1" />
                   
              </td>
              <td> 
                  <asp:HiddenField ID="hdnInvoiceID" runat="server" Value="0" />
                <%-- <asp:Button ID="btnRefresh" runat="server"  CssClass="buttoncommon"
                    style="" Text="Refresh" OnClick="btnRefresh_Click" />--%>
              </td>
              <td>
                 <%--<input id="btnAddNew" type="button" runat="server" value="New Vehicle User" class="buttoncommon" style="width:110px" />--%> 
                   <asp:Button ID="btnfind" runat="server"  Text="Find" Width="100" CssClass="buttoncommon" OnClick="btnfind_Click" />   
              </td>
               
              <td>
                
              </td>
                 <td>
                
              </td>
            </tr>
           <tr>
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
               
              </td>
               
              <td>
                
              </td>
                <td>
                
              </td>
            </tr>
          <tr>
              <td>
              </td>
              <td>
               <asp:Label ID="lblInvoiceDate" runat="server" Text="Invoice Date" style="height: 19px; width: 43px; " Font-Bold="True" Visible="true"></asp:Label>
              </td>
              <td>
                <asp:Label ID="lblInvoiceDatetxt" runat="server" CssClass="textBox" ></asp:Label>
              </td>
              <td colspan="2">
                   <asp:Label ID="lblCustomerName" runat="server" Text="Customer Name" style="height: 19px; width: 43px; " Font-Bold="True" Visible="true" ></asp:Label>
                   
              </td>
              <td> 
                   <asp:Label ID="lblCustomerNametxt" runat="server" CssClass="textBox" ></asp:Label>
              </td>
              <td>
                 <asp:Label ID="lblCustomerAddress" runat="server" Text="Address" style="height: 19px; width: 43px; " Font-Bold="True" Visible="true"  ></asp:Label>
                     
              </td>
              <td>
                 <asp:Label ID="lblCustomerAddresstxt" runat="server" CssClass="textBox" ></asp:Label>
              </td>
              
            </tr>
         
            
         </table>        
     
        </div>
      <br /> 
  
    <div id = "dvControls" style="height:auto;width:100%;"> 
   <div id="dvControlsInner" class="groupBoxContainer boxShadow" style="height:auto;width:100%;">
   
        
  <div id="dvGridContainer"   style="width:100%; height:  100%;">
             
<%--<div id="dvGridHeader" style="width:100%;height:25px; font-size:larger;" class="subHeader">
            <table style="height: 100%;width:100%; color: #FFFFFF; font-weight: bold; text-align: center;" class="defFont" 
                cellspacing="1" cellpadding="1" rules="all" >
            <tr>
               
                <td width="80px" align="left">SL NO</td>
                 <td width="150px" align="left"> Item Name </td>
                <td width="100px" align="left">Item ID</td>
                <td width="150px" align="left">Invoice Qty</td>
                <td width="150px" align="left">Approve Qty"</td>
                <td width="300px" align="left">Approve Note</td>
                
            </tr>
            </table>
        </div> --%>
   
    <div id="dvGrid" style="width:80%; height: 300px; overflow: auto;">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            CellPadding="1" CellSpacing="1" ForeColor="#333333" GridLines="None" 
            Font-Names="Arial" Font-Size="9pt" PageSize="15" 
            EmptyDataText="There is no record" 
            ShowHeader="true" Width="80%"  style="margin-bottom: 0px">
             <PagerSettings Mode="NumericFirstLast" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <Columns>
            <asp:TemplateField HeaderText="SL#">
              <ItemTemplate>
                   <asp:Label ID="lblSLNo" runat="server" Text='<%# Bind("INV_DET_SLNO") %>' Style="text-align: center;" Width="80px"> </asp:Label></ItemTemplate>
                   <ItemStyle VerticalAlign="Top" />
                   </asp:TemplateField>

                <asp:TemplateField HeaderText="Item Name" Visible="true">
                  <ItemTemplate>
                 <asp:Label ID="lblItemName" runat="server" Text='<%# Bind("ITEM_NAME") %>' Style="text-align: center;" Width="150px"></asp:Label>
                                                                   
                  </ItemTemplate>
                   <ItemStyle VerticalAlign="Top" />
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="ITEM_ID" Visible="false">
                  <ItemTemplate>
                 <asp:Label ID="lblITEM_ID" runat="server" Text='<%# Bind("ITEM_ID") %>' Style="text-align: center;" Width="100px"></asp:Label>
                                                                   
                  </ItemTemplate>
                   <ItemStyle VerticalAlign="Top" />
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="Invoice Qty" Visible="true" >
                  <ItemTemplate>
                 <asp:Label ID="lblInvoiceQty" runat="server" Text='<%# Bind("ITEM_QNTY") %>' Style="text-align: center;" Width="100px" ></asp:Label>
                                                                   
                  </ItemTemplate>
                   <ItemStyle VerticalAlign="Top" />
                </asp:TemplateField>
             
                <asp:TemplateField HeaderText="Approve Qty" Visible="true">
                  <ItemTemplate>
                 <asp:TextBox ID="txtApproveQty" runat="server" Text='<%# Bind("ITEM_QNTY_APRROVED") %>' Style="text-align: center;" Width="100px" onkeypress="return onlyNos(event,this);"></asp:TextBox>
                                                                   
                  </ItemTemplate>
                   <ItemStyle VerticalAlign="Top" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Approve Note" Visible="true">
                  <ItemTemplate>
                 <asp:TextBox ID="txtApproveNote" runat="server" Text='<%# Bind("APPROVED_NOTE") %>' Style="text-align: center;" Width="180px"></asp:TextBox>
                                                                   
                  </ItemTemplate>
                   <ItemStyle VerticalAlign="Top" />
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="INV_DETL_ID" Visible="false">
                  <ItemTemplate>
                 <asp:Label ID="lblINV_DET_ID" runat="server" Text='<%# Bind("INVOICE_DET_ID") %>' Style="text-align: center;" Width="100px"></asp:Label>
                                                                   
                  </ItemTemplate>
                   <ItemStyle VerticalAlign="Top" />
                </asp:TemplateField>
                

               <%--  <asp:TemplateField HeaderText="IsVisible">
                <ItemTemplate >
                    <asp:CheckBox ID="chk" runat="server"   />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center"  />
                <ItemStyle BorderColor="Gray" HorizontalAlign="Center" Width="100px"  />
            </asp:TemplateField>--%>
                
            </Columns>
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#999999" />
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        </asp:GridView>
      </div>
   
<%--     <div id="dvGridFooter" style="width:100%;height:25px; font-size: smaller;" class="subFooter">
            <table style="height: 100%; font-weight: bold;"
                cellspacing="1" cellpadding="1" rules="all" >
            <tr>
                <td width="5px" align="left"></td>
                <td align="left">
                  <asp:Label ID="lblTotal" runat="server" Text="Total: 0" 
                     style="width: 96px;"></asp:Label>
                <asp:HiddenField ID="hdnRowCount" runat="server" Value="0" />
                </td>
                <td width="50px"></td>
                
            </tr>
            </table>
        </div>--%> 
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
     </div>

    </div>
    
  <div id="dvContentFooter" class="dvContentFooter">
    <table align="center">
			  <tr>
				<td>
				</td>
				<td>
				   <asp:Button ID="btnAddNew" runat="server" Text="New" CssClass="buttonNew"  />
					<asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonCancel checkIsDirty"  />
				</td>
				<td>
				  <asp:Button ID="btnSave" runat="server" Text="Update" CssClass="buttonSave" OnClick="btnSave_Click"   />
					
				</td>
				<td>
				 <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="buttonDelete" />
				</td>
				
				<td>
				   <asp:Button ID="btnRefresh" runat="server" Text="Refresh" CssClass="buttonRefresh checkIsDirty"  />
				   </td>

			   
				<td>
				   
				 </td>
				

				 <td>
					<input id="btnClose" type="button" runat="server" class="buttonClose" value="Close" onclick="if (ContentForm) { ContentForm.CloseForm(); }" />
				</td>


			  </tr>
		   </table> 
    </div> 


 </div>       
</asp:Content>
