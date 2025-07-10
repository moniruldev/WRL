<%@ Page Title="" Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="CustomerMoneyAdjustment.aspx.cs" Inherits="PG.Web.Inventory.CustomerMoneyAdjustment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../javascript/jquery.ui.combogrid.js" type="text/javascript"></script>
    <link href="../css/jquery.ui.combogrid.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        // <!CDATA[

        var isPageResize = true;
        ContentForm.CalendarImageURL = "../image/calendar.png";
        var lblMessage = '<%=lblMessage.ClientID%>';

        var timerRefresh;
        //var delayRefresh = 2000; //3 min   // Delay in milliseconds

       


        var ReportViewPageLink = '<%=this.ReportViewPageLink%>';
        var ReportViewPDFPageLink = '<%=this.ReportViewPDFPageLink%>';
        var ReportPrintPageLink = '<%=this.ReportPrintPageLink%>';
        var ReportPDFPageLink = '<%=this.ReportPDFPageLink%>';


        var CustomerListServiceLink = '<%=this.CustomerListServiceLink%>';


        var txtCustCode = '<%=txtCustCode.ClientID%>';
        var btnCustID = '<%=btnCustID.ClientID%>';
        var hdnCustId = '<%=hdnCustId.ClientID%>';
        var txtCustomerName = '<%=txtCustomerName.ClientID%>';

        var hdnLocationID = '<%=hdnLocationID.ClientID%>';
        var reportURL = '';
        //var dealerCode = $('#' + txtDealerID).val();
        //var locationid = $('#' + ).val();

        function PageResizeCompleted(pg, cntMain) {
            resizeContentInner(cntMain);

        }


        function resizeContentInner(cntMain) {
            var contHeight = $("#dvContentMain").height();
            var contHead = $("#dvControlsHead").height();
            var contFooter = $("#dvControlsFooter").height();

            var contInnerHeight = contHeight - contHead - contFooter - 5;
            $("#dvControls").height(contInnerHeight);

            $("#dvControlsInner").height(contInnerHeight - 10);


            $("#groupBox").height(contInnerHeight - 10);
            var groupHeight = $("#groupBox").height();
            var groupHeaderHeight = $("#groupHeader").height();
            var groupFooterHeight = $("#groupFooter").height();
            $("#groupContent").height(groupHeight - groupHeaderHeight - groupFooterHeight - 2);

        }


        function showOverlay() {
            document.getElementById("overlay").style.display = "block";
        }

        function hideOverlay() {
            document.getElementById("overlay").style.display = "none";
        }
        function showOverlayReport() {
            document.getElementById("overlayReport").style.display = "block";
        }


        function hideOverlayReport() {
            document.getElementById("overlayReport").style.display = "none";
        }

        function reportInNewWindow(url) {
            var rWin = window.open(url, '_blank');
            if (rWin == null) {
                reportURL = url;
                showOverlayReport();
            }
        }


        function tbopen(key, pdfView, isPrint, isPDFAutoPrint, showWait) {
            hideOverlay();


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
            if (pdfView) {
                url = ReportViewPDFPageLink + "?rk=" + key + "&_tt=" + strTime;
            }
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

            hideOverlay();
        }

        $(document).ready(function () {
            str = document.body.innerHTML


            //    $('#tblParam tr').each(function () {
            //        if ($(this).find('td:empty').length) $(this).remove();
            //    });

            $("#tblParam tr.rowParam").each(function () {
                var cell = $.trim($(this).find('td').text());
                if (cell.length == 0) {
                    //console.log('empty');
                    //$(this).addClass('nodisplay');
                    $(this).hide();
                }
            });

            $("#btnOpenReportWindow").click(function () {
                window.open(reportURL, '_blank');
                hideOverlayReport();
            });

            $("#btnCacnelReportWindow").click(function () {
                hideOverlayReport();
            });

            hideOverlay();

        });

        $(document).ready(function () {
            if ($('#' + txtCustomerName).is(':visible')) {

                bindCustomerList();
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
            serviceURL += "&isService_Center=" + "N";
            // serviceURL += "&locationid=" + locationid;
            //serviceURL += "&seid=" + seid;
            // serviceURL += "&empstatus=" + "A";
            //serviceURL += "&acctypefilter=" + Enums.GLAccountTypeFilter.AllAccount;
            // serviceURL += "&namecomptype=" + Enums.DataCompareType.Contains;



            var customerIDElem = $('#' + txtCustCode);

            $('#' + btnCustID).click(function (e) {
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
                        $('#' + txtCustCode).val('');
                        return false;
                        //ClearGLAccountData(elemID);
                    }


                    if (ui.item.customerid == '') {
                        event.preventDefault();
                        return false;
                        //ClearGLAccountData(elemID);
                    }
                    else {
                        //alert(ui.item.custaddress);

                        $('#' + hdnCustId).val(ui.item.customerid);
                        $('#' + txtCustCode).val(ui.item.custcode);
                        $('#' + txtCustomerName).val(ui.item.custname);

                    }
                    return false;
                },

                lc: ''
            });


            $(customerIDElem).blur(function () {
                var self = this;

                var customerID = $(customerIDElem).val();
                if (customerID == '') {

                    $('#' + txtCustCode).val('');
                    $('#' + txtCustomerName).val('');


                }
            });
        }

        function tbopen(key) {
            if (!key) {
                key = '';
            }


            var url = "/Admin/SetPassword.aspx?uid=" + key
            //if (pageInTab == 1)
            if (TabVar.PageMode == Enums.PageMode.InTab) {

                var tdata = new xtabdata();
                tdata.linktype = Enums.LinkType.Direct;
                tdata.id = 6320;
                tdata.name = "SetPassword";
                //tdata.label = "User: " + userid;
                tdata.label = "Set Password";
                tdata.type = 0;
                tdata.url = url;
                tdata.tabaction = Enums.TabAction.InTabReuse;
                tdata.selecttab = 1;
                tdata.reload = 0;
                tdata.param = "";



                try {
                    window.parent.OpenMenuByData(tdata);
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





        function fromParent(val1) {
            alert('this is called from parent: ' + val1);
        }

        function Button1_onclick() {
            //document.getElementById("btnSave").click();
            ///__doPostBack("<%= this.btnSave.UniqueID %>", "");
            __doPostBack("btnSave", "");
        }

        function isNumberKey(evt, obj) {

            var charCode = (evt.which) ? evt.which : event.keyCode
            var value = obj.value;
            var dotcontains = value.indexOf(".") != -1;
            var mncontains = value.indexOf("-") != -1;
            if (dotcontains)
                if (charCode == 46) return false;
            if (charCode == 46) return true;
            if (!mncontains) return true;
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }








        // ]]>

    </script>

    <style type="text/css">
        .groupBoxContainer {
            width: 750px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:HiddenField ID="hdnLocationID" runat="server" Value="0" />
     <asp:HiddenField ID="hdnCustId" runat="server" Value="0" />
    <asp:HiddenField ID="hdnAdjustID" runat="server" Value="0" />
    
    <div id="dvPageContent" style="width: 100%; height: auto;">
        <div id="dvContentHeader" class="dvContentHeader">
            <div id="dvHeader" class="dvHeader">
                <asp:Label ID="lblHeader" CssClass="lblHeader" runat="server" Text="Customer Adjustment"></asp:Label>
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
                    <div id="groupBox" style="">
                        <div id="groupHeader" class="groupHeader">
                            <div style="width: 100%; height: 20px;">
                                <table>
                                    <tr>
                                        <td>
                                            <div id="dvIconEditMode" class="iconView" runat="server"></div>
                                        </td>
                                        <td>
                                            <span>Customer Adjustment</span>
                                        </td>
                                    </tr>
                                </table>

                            </div>

                        </div>
                        <div id="groupContent" class="groupContent" style="width: 100%; height: 300px; overflow: auto;">
                            <div id="groupContenInner" style="width: 100%; height: auto; text-align: left;">
                                <table style="text-align: left;" border="0" cellspacing="4" cellpadding="2">

                                    <tr>
                                        <td></td>
                                        <td style="" align="right">
                                            <asp:Label ID="Label10" runat="server" Text="Location:" Visible="false"></asp:Label>
                                        </td>
                                        <td style="" align="left">

                                            <table cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td>
                                                         <asp:TextBox ID="txtLocationCode" Enabled="false" runat="server"  Width="170px" CssClass="textBox" Visible="false"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                       
                                                    </td>
                                                    <td class="tdSpacer"></td>
                                                    <td>
                                                         <asp:TextBox ID="txtLocationName" Enabled="false" runat="server" Width="250px" CssClass="textBox" Visible="false"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                       
                                                    </td>
                                                </tr>
                                            </table>


                                           
                                        </td>

                                    </tr>


                                    <tr>
                                        <td></td>
                                        <td style="" align="right">
                                            <asp:Label ID="lblAdjustmentID" runat="server" Text="Adjustment ID:"></asp:Label>
                                        </td>
                                        <td style="" align="left">
                                            <asp:TextBox ID="txtAdjustmentId" Enabled="false" runat="server" ReadOnly="true" Width="170px" CssClass="textBox"></asp:TextBox>
                                            
                                        </td>
                                        

                                    </tr>
                                   
                                    <tr>
                                        <td></td>
                                        <td style="" align="right">
                                            <asp:Label ID="lblAdjustment" runat="server" Text="Adjustment Date:"></asp:Label>

                                        </td>
                                        <td style="" align="left">
                                            <asp:TextBox ID="txtAdjustmentDate" runat="server" Width="170px" CssClass="textBox textBox textDate dateParse"></asp:TextBox>

                                        </td>

                                    </tr>


                                    <tr>
                                        <td></td>
                                        <td style="" align="right">
                                            <asp:Label ID="lblRefNo" runat="server" Text="Ref No:"></asp:Label>

                                        </td>
                                        <td style="" align="left">
                                            <asp:TextBox ID="txtRefNo" runat="server" Width="170px" CssClass="textBox"></asp:TextBox>

                                        </td>

                                    </tr>

                                    <tr>
                                        <td></td>
                                        <td style="" align="right">
                                            <asp:Label ID="lblAdjustmentType" runat="server" Text="Adjustment Type:"></asp:Label>

                                        </td>
                                        <td style="" align="left">
                                            <asp:DropDownList ID="ddlAdjustmentType" runat="server" Width="170px" CssClass="dropDownList"></asp:DropDownList>

                                        </td>

                                    </tr>


                                    <tr>
                                        <td></td>
                                        <td style="" align="right">
                                            <asp:Label ID="lblCustcode" runat="server" Text="Cust Code :"></asp:Label>
                                        </td>
                                        <td style="" align="left">
                                            <table cellspacing="0" cellpadding="0">
                                                              
            <tr>
               
                
                            <td >
                                <asp:TextBox ID="txtCustCode" runat="server" Width="100px" CssClass="textBox" TabIndex="1"></asp:TextBox>
                                <input id="btnCustID" type="button" value="" runat="server" class="buttonDropdown"
                                    tabindex="-1" />
                                <asp:TextBox ID="txtCustomerName" runat="server" Width="330px" CssClass="textBox"></asp:TextBox>
                            </td>
                          
                             
                        


            </tr>
                                            </table>
                                        </td>

                                    </tr>

                                    <tr>
                                        <td></td>
                                        <td style="" align="right">
                                            <asp:Label ID="Label5" runat="server" Text="Sales Month Cycle:"></asp:Label>

                                        </td>
                                        <td style="" align="left">
                                            <asp:TextBox ID="txtMonthCycle" Enabled="false" runat="server" Width="170px" CssClass="textBox" MaxLength="6"></asp:TextBox>

                                        </td>

                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td style="" align="right">
                                            <asp:Label ID="Label8" runat="server" Text="Adjustment Amount:"></asp:Label>

                                        </td>
                                        <td style="" align="left">
                                            <asp:TextBox ID="txtAdjustMentAmount" runat="server" Width="170px" CssClass="textBox" onkeypress="return isNumberKey(event,this);" ></asp:TextBox>

                                        </td>

                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td style="" align="right">
                                            <asp:Label ID="Label9" runat="server" Text="Adjustment Reason:"></asp:Label>

                                        </td>
                                        <td style="" align="left">
                                            <asp:TextBox ID="txtAdjustMentReason" TextMode="MultiLine" runat="server" Rows="3" Width="400px" CssClass="textBox" MaxLength="6"></asp:TextBox>

                                        </td>

                                    </tr>

                                </table>

                            </div>
                        </div>

                    </div>
                    <div id="groupFooter" class="groupFooter">
                        <div style="width: 100%; height: 12px;">
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
                        <asp:Button ID="btnAddNew" runat="server" Text="New" CssClass="buttonNew"
                            OnClick="btnAddNew_Click" Visible="true" />
                       
                    </td>
                    <td>
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonSave"
                            OnClick="btnSave_Click"  />
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="buttonEdit"
                            OnClick="btnEdit_Click"  />
                    </td>
                    <td>
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="buttonDelete" Visible="false"
                            OnClick="btnDelete_Click" />
                    </td>
                       <td>
                        <asp:Button ID="btnAuthorized" runat="server" Text="Authorized" CssClass="buttoncommon" OnClick="btnAuthorized_Click" />
                    </td>
                    <td>
                        <asp:Button ID="btnRefresh" runat="server" Text="Refresh" CssClass="buttonRefresh checkIsDirty" Visible="false"  OnClick="btnRefresh_Click" />
                    </td>


                    <td></td>


                    <td>
                        <input id="btnClose" type="button" runat="server" class="buttonClose" value="Close" onclick="if (ContentForm) { ContentForm.CloseForm(); }" />
                    </td>
                    <td>

                    </td>
                    <td>
                        <asp:Button ID="btnAdjustprint" runat="server" Text="Print" CssClass="buttonPrintPreview" OnClick="btnAdjustprint_Click" OnClientClick="showOverlay();" />
                       
                    </td>
                      <td>
                        <asp:Label ID="Label11" runat="server" Text="Report View"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlReportViewMode" runat="server" CssClass="dropDownList">
                            <asp:ListItem Value="0">In This Tab</asp:ListItem>
                            <asp:ListItem Value="1">In New Tab</asp:ListItem>
                            <asp:ListItem Selected="True" Value="2">In New Window</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlExport" runat="server" Width="70" CssClass="dropDownList" Style="display: none">
                            <asp:ListItem Selected="True" Value="0">PDF</asp:ListItem>
                            <asp:ListItem Value="1">Excel</asp:ListItem>
                            <asp:ListItem Value="2">Word</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label ID="Label12" runat="server" Text="Type:"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlReportViewType" runat="server" CssClass="dropDownList">
                            <asp:ListItem Value="0">Screen</asp:ListItem>
                            <asp:ListItem Selected="True" Value="1">PDF</asp:ListItem>
                        </asp:DropDownList>
                    </td>

                </tr>
            </table>

        </div>
    </div>

     <div id="overlay" class="overlay">
        <div style="margin: auto; width: 200px; height: 400px; background-color: black; border: solid 1px black; text-align: center; vertical-align: middle;">
            <span style="color: white; font-size: medium;">Please Wait...</span>
            <br />
            <img alt="" src="../../image/progress.gif" />
        </div>
    </div>

    <div id="overlayReport" class="overlay" style="opacity: 0.8;">
        <div style="margin: auto; width: 450px; height: 80px; position: relative; background-color: blue; text-align: center; vertical-align: middle; cursor: auto; z-index: 9999999;">
            <table width="100%">
                <tr>
                    <td>
                        <span style="color: white; font-size: medium;">Click Open Report to view Report.</span>
                    </td>
                </tr>
                <tr></tr>
                <tr>
                    <td>
                        <input id="btnOpenReportWindow" type="button" value="Open Report" class="buttoncommon" />
                        <input id="btnCacnelReportWindow" type="button" value="Cancel" class="buttoncommon" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
