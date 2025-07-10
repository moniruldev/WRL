<%@ Page Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="OutsaleReport.aspx.cs" Inherits="PG.Web.Report.INV.OutsaleReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="DropDownCheckBoxes" Namespace="Saplin.Controls" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../../javascript/jquery.ui.combogrid.js" type="text/javascript"></script>
    <link href="../../css/jquery.ui.combogrid.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        // <!CDATA[


        var ItemGroupListServiceLink = '<%=this.ItemGroupListServiceLink%>';
        var CustomerListServiceLink = '<%=this.CustomerListServiceLink%>';
        var hdnGroupID = '<%= hdnGroupID.ClientID%>';
        var txtGroupName = '<%= txtGroupName.ClientID%>';
        var btnGroupID = '<%= btnGroupID.ClientID%>';


        var isPageResize = true;
        ContentForm.CalendarImageURL = "../../image/calendar.png";

        var ReportViewPageLink = '<%=this.ReportViewPageLink%>';
        var ReportViewPDFPageLink = '<%=this.ReportViewPDFPageLink%>';
        var ItemListServiceLink = '<%=this.ItemListServiceLink%>';
        var ifPrintButton = '<%=ifPrintButton.ClientID%>';


        var txtFromDateID = '<%=txtFromDate.ClientID%>';
        var txtToDateID = '<%=txtToDate.ClientID%>';


        var btnItemLoad = '<%= btnItemLoad.ClientID%>';
        var hdnItemIdForFilter = '<%= hdnItemIdForFilter.ClientID%>';
        var txtItemName = '<%= txtItemName.ClientID%>';

        var txtCustomerName = '<%=txtCustomerName.ClientID%>';
        var btnCustomerID = '<%=btnCustomerID.ClientID%>';

        <%--  var txtCustomerID = '<%=txtCustomerID.ClientID%>';--%>
        var hdnCustomerID = '<%=hdnCustomerID.ClientID%>';
        var txtCustomerAddress = '<%=txtCustomerAddress.ClientID%>';




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

            if ($('#' + txtCustomerName).is(':visible')) {

                bindCustomerList();
            }

            if ($('#' + txtItemName).is(':visible')) {
                bindItemList();
            }
            if ($('#' + txtGroupName).is(':visible')) {
                bindGroupList();
            }


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
            serviceURL += "&isRefundable=" + "N";
            serviceURL += "&isRotary=" + "N";
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

                        $('#' + hdnCustomerID).val('0');
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

        function bindGroupList() {
            var cgColumns = [
                             //{ 'columnName': 'itemgroupid', 'width': '80', 'align': 'left', 'highlight': 2, 'label': 'Group ID' }
                             { 'columnName': 'itemgroupcode', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Code' }
                            , { 'columnName': 'itemgroupdesc', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                            , { 'columnName': 'itemgroupnameparent', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Group Parent' }


            ];
            var serviceURL = ItemGroupListServiceLink + "?isterm=1&includeempty=0&hasitem=1&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;

            serviceURL += "&ispaging=1";
            var groupIDElem = $('#' + txtGroupName);

            $('#' + btnGroupID).click(function (e) {
                $(groupIDElem).combogrid("dropdownClick");
            });

            $(groupIDElem).combogrid({
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
                        // $('#' + hdnDealerID).val(ui.item.dealerid);
                        $('#' + hdnGroupID).val(ui.item.itemgroupid);
                        $('#' + txtGroupName).val(ui.item.itemgroupdesc);
                        //$('#' + txtGroupCode).val(ui.item.itemgroupcode);

                    }
                    return false;
                },

                lc: ''
            });


            $(groupIDElem).blur(function () {
                var self = this;

                var groupID = $(groupIDElem).val();
                if (groupID == '') {
                    // $('#' + hdnDealerID).val('0');
                    $('#' + txtGroupName).val('');
                    //$('#' + txtGroupCode).val('');
                }
            });
        }


        function bindItemList() {

            var cgColumns = [{ 'columnName': 'itemname', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                             , { 'columnName': 'uomname', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Uom Name' }
                             , { 'columnName': 'itemcode', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Code' }
                             , { 'columnName': 'itemgroupdesc', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Group Name' }
                             , { 'columnName': 'class_name', 'width': '120', 'align': 'left', 'highlight': 4, 'label': 'Class Name' }
                             , { 'columnName': 'item_type', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Item Type' }



            ];
            var serviceURL = ItemListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;


            serviceURL += "&ispaging=1";
            var groupIDElem = $('#' + txtItemName);

            $('#' + btnItemLoad).click(function (e) {
                $(groupIDElem).combogrid("dropdownClick");
            });

            $(groupIDElem).combogrid({
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
                width: 850,
                url: serviceURL,
                search: function (event, ui) {
                    var vgroupid = 0;
                    var groupName = $('#' + txtGroupName).val();
                    if (groupName != "") {
                        vgroupid = $('#' + hdnGroupID).val();
                        if (vgroupid == "0" || vgroupid == "") {
                            vgroupid = 0;
                        }
                    } else {
                        $('#' + hdnGroupID).val('0');

                    }
                    var newServiceURL = serviceURL + "&groupid=" + vgroupid;

                    newServiceURL = JSUtility.AddTimeToQueryString(newServiceURL);
                    // var newServiceURL = serviceURL;
                    $(this).combogrid("option", "url", newServiceURL);


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
                        $('#' + hdnItemIdForFilter).val('0');
                        // $('#' + hdnDealerID).val(ui.item.dealerid);
                        $('#' + hdnItemIdForFilter).val(ui.item.itemid);
                        $('#' + txtItemName).val(ui.item.itemname);
                        //$('#' + txtGroupCode).val(ui.item.itemgroupcode);

                    }
                    return false;
                },

                lc: ''
            });


            $(groupIDElem).blur(function () {
                var self = this;

                var groupID = $(groupIDElem).val();
                if (groupID == '') {
                    // $('#' + hdnDealerID).val('0');
                    $('#' + txtItemName).val('');
                    //$('#' + txtGroupCode).val('');
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



        .auto-style1 {
            width: 200px;
        }



        </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="dvPageContent" style="width:100%; height:auto;">
    <div id="dvContentHeader" class="dvContentHeader">
    <div id="dvHeader" class="dvHeader" >
        <asp:Label ID="lblHeader" CssClass="lblHeader" runat="server" Text="Out Sale Report"></asp:Label>
    </div>
    <!--Message Div -->
    <div id="dvMsg" runat="server" class="dvMessage">
        <asp:Label ID="lblMessage" runat="server" Font-Size="Small" Width="100%"> </asp:Label>
    </div>
     <div id="dvHeaderControl" class="dvHeaderControl">
     </div>
    </div>

    <div id="dvContentMain" class="dvContentMain">
    <div id = "dvControls" style="height:auto; width:100%">
        <div id="dvControlsInner" class="groupBoxContainer boxShadow">    
             <div id="groupBox">
                  <div id="groupHeader" class="groupHeader">
                      <div style="width:100%;height:20px;">
                         <table>
                            <tr>
                             <td>
                                <div id="dvIconEditMode" class="iconView" runat="server" ></div>
                             </td>
                             <td>
                                <span>Out Sale Report</span> 
                             </td>
                            </tr>
                         </table>
                         
                      </div>
                      
                  </div>
                  <div id="groupContent" class="groupContent" style="width:100%; height:auto; overflow:auto;">
                  <div id="groupContenInner" style="width:100%;height:auto; text-align:left;">
              <table style="text-align:left; width:100%;" border="0" cellspacing="4" cellpadding="2">
                
             
              <tr>
                 <td>
                   </td>
                 <td style="" align="right">
                   <asp:Label id="Label5" runat="server" Text="Report Type:"  Width="80px" ></asp:Label>
                 </td>
                 <td style="" align="left">
                   <asp:DropDownList ID="ddlReportType" runat="server" Width="240"  CssClass="dropDownList enableIsDirty"> 
                       <asp:ListItem Text="Item Wise Details" Value="IW"></asp:ListItem>
                        <asp:ListItem Selected="True" Text="Date Wise Details"  Value="DW"></asp:ListItem>
                       <asp:ListItem Text="Date Wise Summary" Value="DS"></asp:ListItem>
                   </asp:DropDownList>
                 </td>
                  <td></td>
                  <td class="auto-style1"></td>
                 </tr>

                   <tr>
                 <td>
                   </td>
                 <td style="" align="right">
                   <asp:Label id="lblDONO" runat="server" Text="DO No:" ></asp:Label>
                     </td>
                 <td style="" align="left">
                      
                     <asp:TextBox id="txtInvoiceNo" runat="server" Width="235px"  CssClass="textBox fldRequired enableIsDirty"></asp:TextBox>
                     
                      </td>
                  <td >
                    </td>
                 
                     <td class="auto-style1"></td>
                 </tr> 
                   <tr>
                 <td>
                   </td>
                 <td style="" align="right">
                   <asp:Label id="Label8" runat="server" Text="DC No:" ></asp:Label>
                     </td>
                 <td style="" align="left">
                      
                     <asp:TextBox id="txtDCNo" runat="server" Width="235px"  CssClass="textBox fldRequired enableIsDirty"></asp:TextBox>
                     
                      </td>
                  <td >
                    </td>
                <td class="auto-style1"></td>
                     
                 </tr> 
                  
                   <tr>
                 <td>
                   </td>
                 <td style="" align="right">
                   <asp:Label id="lblInvoiceNo" runat="server" Text="Invoice No:" ></asp:Label>
                     </td>
                 <td style="" align="left">
                      
                     <asp:TextBox id="txtSalesInvoiceNo" runat="server" Width="235px"  CssClass="textBox fldRequired enableIsDirty"></asp:TextBox>
                     
                      </td>
                  <td>
                    </td>
                <td class="auto-style1"></td>
                 </tr> 

                   <tr>
                       <td></td>

                                                        <td align="right" class="auto-style2">
                                                            <asp:Label ID="lblCustomer" runat="server" Text="Customer:"></asp:Label>
                                                        </td>

                                                        <td>

                                                            <asp:TextBox ID="txtCustomerName" runat="server" CssClass="textBox" Enabled="true" Width="235px"></asp:TextBox>
                                                            
                                                        </td>
                       <td> <input id="btnCustomerID" type="button" value="" runat="server" class="buttonDropdown"
                                                                tabindex="-1" /></td>
                       <td class="auto-style1">
                            <asp:TextBox ID="txtCustomerAddress" runat="server" CssClass="textBox" Enabled="false" Width="200px"></asp:TextBox>
                                                            <%--<asp:TextBox ID="txtCustomerID" runat="server" CssClass="textBox" Visible="true" Width="10px"></asp:TextBox>--%>
                                                             <asp:HiddenField ID="hdnCustomerID" runat="server" Value="0"  />
                       </td>
                                                        



                                                    </tr>
                  <%-- <tr>
                 <td>
                   </td>
                 <td style="" align="right">
                   <asp:Label id="lblDeptName" runat="server" Text="Customer" ></asp:Label>
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
                 </tr>--%>
                   
                  
                  
                  

                  <tr>
                                        <td></td>
                                        <td style="" align="right">
                                            <asp:Label ID="lblITEMGROUP" runat="server" Text="Item Group:"></asp:Label>
                                        </td>
                                        <td style="" align="left">

                                            <asp:TextBox ID="txtGroupName" runat="server" CssClass="textBox required" Enabled="true" Width="235px"></asp:TextBox>
                                           

                                        </td>
                                       <td><input id="btnGroupID" type="button" value="" runat="server" class="buttonDropdown" tabindex="-1" /></td>
                       <td class="auto-style1"> <asp:HiddenField ID="hdnGroupID" runat="server" /></td>
                                    </tr>

                 
                    <tr>
                    <td></td>
                    <td style="" align="right">
                    <asp:Label ID="Label4" runat="server" Text="Item :"></asp:Label>
                    </td>
                    <td style="" align="left">
                    <asp:TextBox ID="txtItemName"  Width="235px" runat="server" CssClass="textBox required" Enabled="true"></asp:TextBox>

                    </td>
                        <td>
                            <input id="btnItemLoad" type="button" value="" runat="server" class="buttonDropdown" tabindex="-1" />
                        </td>
                         <td class="auto-style1"><asp:HiddenField ID="hdnItemIdForFilter" runat="server" /></td>
                   
                   </tr>

                    <tr class="rowParam">
                        <td></td>
                        <td align="right" class="auto-style2">
                            <asp:Label ID="Label10" runat="server" Text="SNS Type:"></asp:Label>
                        </td>
                        <td class="auto-style2">
                            <asp:DropDownList ID="ddlItemSNS" runat="server" CssClass="dropDownList enableIsDirty"  Width="235px">
		                    <asp:ListItem Value="0" >All</asp:ListItem>
	                    </asp:DropDownList>
                        </td>
                                                      
                    </tr>

                   <tr class="rowParam">
                  <td>
                  </td>
                  <td align="right">
                  <asp:Label ID="lblFromDate" runat="server" Text="DC Date From:"></asp:Label>
                   </td>
                  <td colspan="4">
                     <table cellpadding="0" cellspacing="0">
                     <tr>
                     <td>
                      <asp:TextBox ID="txtFromDate" runat="server" Width="65px" CssClass="textBox textDate dateParse"></asp:TextBox>
                      </td>
                     
                      <td>
                      &nbsp;<asp:Label ID="lblToDate" runat="server" Text="DC Date To:"></asp:Label>
                      </td>
                                                  
                          <td>                                                              
                          <asp:TextBox ID="txtToDate" runat="server" Width="65px" CssClass="textBox textDate dateParse"></asp:TextBox>
                         </td>
                         </tr>
                         </table>
                        </td>
                     
                       
                      
                   </tr> 

                    <tr>
                 <td>
                   </td>
                 <td style="" align="left">
                   
                 </td>
                 <td style="" align="left">
                      
                 <%--   <asp:Button ID="btnSearch" runat="server"  CssClass="buttonRefresh" style="padding-left:22px;"
                Text="Load" onclick="btnSearch_Click" /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;--%>
                     <asp:Button ID="buttonIRRPreview" runat="server"  CssClass="buttoncommon buttonPrint" style="padding-left:22px;" Width="100px"
                Text="Preview" onclick="btnIRRPreview_Click" />
                      </td>
                        
                  <td style="" align="right">
                      </td>
                 
                 </tr>
              </table>
              </div>

              </div>
                  
            </div>
          </div>
        </div>  
         <br />
        <br />
         </div>
         <div id="dvContentFooter" class="dvContentFooter">
            <div id="dvContentFooterInner" class="dvContentFooterInner">
                <div style="width: 100%; height: 100%; margin-bottom: 0px;">
                    <div style="width: auto; min-width: 300px; height: auto; text-align: left;">
                        <table border="0">
                            <tr>
                                <td style="width: 100px;">
                                
                                </td>
                                <td>
                                    <asp:Label ID="Label2" runat="server" Text="Report View"></asp:Label> 
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlReportViewMode" runat="server" CssClass="dropDownList">
                                        <asp:ListItem Value="0">In This Tab</asp:ListItem>
                                        <asp:ListItem  Value="1">In New Tab</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="2">In New Window</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                <asp:DropDownList ID="ddlReportViewType" runat="server" CssClass="dropDownList">
                                        <asp:ListItem Value="0">Screen</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="1">PDF</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                </td>
                               
                                <td style="width: 20px;">
                                </td>
                                <td style="width: 10px;">
                                </td>
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
</asp:Content>


