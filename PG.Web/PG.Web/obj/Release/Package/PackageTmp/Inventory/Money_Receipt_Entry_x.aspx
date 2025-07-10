<%@ Page Title="" Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="Money_Receipt_Entry_x.aspx.cs" Inherits="PG.Web.Inventory.Money_Receipt_Entry_x" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="../javascript/jquery.ui.combogrid.js" type="text/javascript"></script>
    <link href="../css/jquery.ui.combogrid.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">



        var ReportViewPageLink = '<%=this.ReportViewPageLink%>';
        var ReportViewPDFPageLink = '<%=this.ReportViewPDFPageLink%>';
        var ReportPrintPageLink = '<%=this.ReportPrintPageLink%>';
        var ReportPDFPageLink = '<%=this.ReportPDFPageLink%>';

       
        var CustomerListServiceLink = '<%=this.CustomerListServiceLink%>';
        <%--var ExecutiveListServiceLink = '<%=this.ExecutiveListServiceLink%>';--%>
        var BankListServiceLink = '<%=this.BankListServiceLink%>';
        var BankBranchListServiceLink = '<%=this.BankBranchListServiceLink%>';
        var AccountHolderListServiceLink = '<%=this.AccountHolderListServiceLink%>';
       <%-- var DealerChequeListServiceLink = '<%=this.DealerChequeListServiceLink%>';--%>

        var txtCustCode = '<%=txtCustCode.ClientID%>';
        var btnCustID = '<%=btnCustID.ClientID%>';
        var hdnCustId = '<%=hdnCustId.ClientID%>';
        var txtCustomerName = '<%=txtCustomerName.ClientID%>';
        var txtCustAddress = '<%=txtCustAddress.ClientID%>';
        var txtCustPhone = '<%=txtCustPhone.ClientID%>';
        var txtMrDate = '<%=txtMrDate.ClientID%>';

       <%-- var txtDealerAddress = '<%=txtDealerAddress.ClientID%>';
        var txtDealerPhone = '<%=txtDealerPhone.ClientID%>';
        var txtDealerDistrict = '<%=txtDealerDistrict.ClientID%>';
        var txtDealerDivision = '<%=txtDealerDivision.ClientID%>';
        var txtDealerThana = '<%=txtDealerThana.ClientID%>';--%>
        var txtTTNo = '<%= txtTTNo.ClientID%>';

        var txtBranchName = '<%=txtBranchName.ClientID%>'; 
        var btnChkBank = '<%=btnChkBank.ClientID%>';
        var txtBankName = '<%=txtBankName.ClientID%>';
        var btnBranch = '<%=btnBranch.ClientID%>';
        var hdnBankId = '<%=hdnBankId.ClientID%>';
        var txtBankCode = '<%=txtBankCode.ClientID%>';
        var hdnACC_HOLDER_DTL_ID = '<%=hdnACC_HOLDER_DTL_ID.ClientID%>';
        var hdnACC_HOLDER_ID = '<%=hdnACC_HOLDER_ID.ClientID%>';
        var ddlPaymentMode = '<%=ddlPaymentMode.ClientID%>';
        <%--var hdnSegmentid = '<%=hdnSegmentid.ClientID%>';--%>
    
        <%--  var btnChkBranch = '<%=btnChkBranch.ClientID%>';
        var txtChkBranchName = '<%=txtChkBranchName.ClientID%>';--%>
      <%--  var hdnChkBranhId = '<%=hdnChkBranhId.ClientID%>';--%>
        
        var txtBranchCode = '<%=txtBranchCode.ClientID%>';
       
        var hdnBranchId = '<%=hdnBranchId.ClientID%>';
         
      <%--  var txtOnlineAccHolderName = '<%=txtOnlineAccHolderName.ClientID%>';--%>
        <%--var btnOnlineAccHolderName = '<%=btnOnlineAccHolderName.ClientID%>';--%>
        var txtOnlineAccHolderNameDtl = '<%=txtOnlineAccHolderNameDtl.ClientID%>';
        var hdnOnlineAccHolderId = '<%=hdnOnlineAccHolderId.ClientID%>';

        var btnDepositedAcNo = '<%=btnDepositedAcNo.ClientID%>';
        var txtOnlineBranchCode = '<%=txtOnlineBranchCode.ClientID%>';
        var txtOnlineBranchName = '<%=txtOnlineBranchName.ClientID%>';
        var txtOnlineBankCode = '<%=txtOnlineBankCode.ClientID%>';
        var txtOnlineBankName = '<%=txtOnlineBankName.ClientID%>';
        var txtOnLineAccountNo = '<%=txtOnLineAccountNo.ClientID%>';


       <%-- var txtChkChequeNo = '<%=txtChkChequeNo.ClientID%>';--%>
        var txtChkChequeAmt = '<%=txtChkChequeAmt.ClientID%>';
        var txtChkCreditDate = '<%=txtChkCreditDate.ClientID%>';
        var txtChkStatus = '<%=txtChkStatus.ClientID%>';
      <%--  var txtChkAccNo = '<%=txtChkAccNo.ClientID%>';--%>


        var txtExecutiveId = '<%=txtExecutiveId.ClientID%>';
        var txtExecutiveName = '<%=txtExecutiveName.ClientID%>';
       <%-- //var btnExecutiveId = '<%=btnExecutiveId.ClientID%>';--%>
        var txtExecutiveMobile = '<%=txtExecutiveMobile.ClientID%>';

        var ddlPaymentMode = '<%=ddlPaymentMode.ClientID%>';
        var hdnLocationID = '<%=hdnLocationID.ClientID%>';

        var txtDepositedById = '<%=txtDepositedById.ClientID%>';
        var txtDepositedByName = '<%=txtDepositedByName.ClientID%>';
        var btnDepositedBy = '<%=btnDepositedBy.ClientID%>';
        var hdnDepositedById = '<%=hdnDepositedById.ClientID%>';


<%--        var gridUpdatePanelIDDet = '<%=UpdatePanel1.ClientID%>';
        var gridViewIDDet = '<%=grdMR.ClientID%>';
        var updateProgressID = '<%=UpdateProgress2.ClientID%>'; --%>
        var reportURL = '';
        <%-- var txtDepositedAcNo = '<%=txtDepositedAcNo.ClientID%>';
       var txtDepositedAccName = '<%=txtDepositedAccName.ClientID%>';--%>
       
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

        var reportURL = '';

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



        function reportInNewWindow(url) {
            var rWin = window.open(url, '_blank');
            if (rWin == null) {
                reportURL = url;
                showOverlayReport();
            }
        }

        function tbopen(key, pdfView, isPrint, isPDFAutoPrint, showWait) {
           // hideOverlay();


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
                tdata.id = 1911;
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

           // hideOverlay();
        }
 

       
        function SaveConfirmation() {
            return confirm("Are you sure you to Save?");
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


        $(function() {
            $('#txtMrDate').datepicker( {
                changeMonth: true,
                changeYear: true,
                showButtonPanel: true,
                dateFormat: 'MM yy',
                onClose: function(dateText, inst) { 
                    alert('25655656');
                }
            });
        });


        $(document).ready(function () {
            var pageInstance = Sys.WebForms.PageRequestManager.getInstance();

            pageInstance.add_pageLoaded(function (sender, args) {
                var panels = args.get_panelsUpdated();
                for (i = 0; i < panels.length; i++) {
                    if (panels[i].id == gridUpdatePanelIDDet) {
                        //bindSegmentList(gridViewIDDet);
                    }
                }
            });

            if ($('#' + txtCustomerName).is(':visible')) {

                bindCustomerList();
            }

                //bindDepositedExecutiveList();
                bindBankList();
                bindBankBranchList();
                bindAccountHolderList();
                //bindSegmentList(gridViewIDDet);

                //$('#txtMrDate').datepicker({
                //    onSelect: function (dateText, inst) {
                //        alert('25655656');
                //    }
                //});
              
  
        });

        
        function mrdateValidation() {
            $("[id*=btnDateValid]").click();
        }

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
                        $('#' + txtCustAddress).val(ui.item.custaddress);
                        $('#' + txtCustPhone).val(ui.item.custphone);
                        //alert(txtCustAddress);
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
                    $('#' + txtCustAddress).val('');
                    $('#' + txtCustPhone).val('');
                    
                }
            });
        }


        function bindSegmentList(gridViewID) {
            var cgColumns = [{ 'columnName': 'segment_code', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Segment Code' }
                             , { 'columnName': 'segment_name', 'width': '170', 'align': 'left', 'highlight': 4, 'label': 'Segment Name' }


            ];
            var serviceURL = SegmentListServiceLink + "?isterm=1&includeempty=0&forinvoice=1&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            serviceURL += "&ispaging=0";
            var gridSelector = "#" + gridViewID;
            $(gridSelector).find('input[id$="txtITEM_SEGMENT_NAME"]').each(function (index, elem) {
                ///list click
                var elemRow = $(elem).closest('tr.gridRow');

                var hdnItemIDElem = $(elemRow).find('input[id$="txtITEM_SEGMENT_NAME"]');

                //var prevGLCode = '';

                $(elem).closest('tr').find('input[id$="btnSEG_ID"]').click(function (e) {
                    elmID = $(elem).attr('id');
                    $(elem).combogrid("dropdownClick");
                });

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
                    width:350,
                    url: serviceURL,
                    search: function (event, ui) {
                        var elemRowCur = $(elem).closest('tr.gridRow');
                        var segmentName = $(elemRowCur).find('input[id$="txtITEM_SEGMENT_NAME"]').val();
                        var newServiceURL = serviceURL + "&segmentName=" + segmentName;
                        newServiceURL = JSUtility.AddTimeToQueryString(newServiceURL);
                        $(this).combogrid("option", "url", newServiceURL);
                    },

                    select: function (event, ui) {
                        //alert(ui.item.itemname);
                        //$(".txtComboGrid").val(ui.item.code);
                        elemID = $(elem).attr('id');
                        if (!ui.item) {
                            ClearSegmentData(elemID);
                            event.preventDefault();
                            return false;
                        }

                        if (ui.item.segment_id == '') {
                            ClearSegmentData(elemID);
                            event.preventDefault();
                            return false;
                        }
                        else {
                            SetSegmentData(elemID, ui.item);
                        }
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
                        ClearSegmentData(elemID);
                    }
                    else {
                        var serviceURL = SegmentListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
                        serviceURL += "&ispaging=1";

                        var prcNo = GetSegmentNo(eCode, serviceURL);
                      
                        if (prcNo == null) {
                            ClearSegmentData(elemID);
                        }
                        else {
                            SetSegmentData(elemID, grp);
                        }
                    }
                    grpID = $(self).closest('tr').find('input[id$="txtITEM_SEGMENT_ID"]').val();
                    if (grpID == '0' | grpID == '') {
                        $(self).addClass('textError');
                    }

                });

            });
        }
        function GetSegmentNo(eCode, serviceURL) {
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


        function ClearSegmentData(txtITEM_SEGMENT_NAME) {
            var detRow = $('#' + txtITEM_SEGMENT_NAME).closest('tr.gridRow');
            $(detRow).find('input[id$="txtITEM_SEGMENT_NAME"]').val('');
            $(detRow).find('input[id$="txtITEM_SEGMENT_ID"]').val('');
            $(detRow).find('input[id$="txtCHEQUE_AMT"]').val('0');
            $(detRow).find('input[id$="hdnSegmentid"]').val('0');
            
        }
        function SetSegmentData(txtITEM_SEGMENT_NAME, data) {
            $('#' + txtITEM_SEGMENT_NAME).val(data.segment_name);
            var detRow = $('#' + txtITEM_SEGMENT_NAME).closest('tr.gridRow');
            $(detRow).find('input[id$="txtITEM_SEGMENT_NAME"]').val(data.segment_name);
            $(detRow).find('input[id$="txtITEM_SEGMENT_ID"]').val(data.segment_id);
            $(detRow).find('input[id$="hdnSegmentid"]').val(data.segment_id);
        }


        function SelectTab() {

            var paymentMode = $('#' + ddlPaymentMode).val();        
            if (paymentMode == "Q" || paymentMode == "D") {
                $('#tabs').tabs('select', '#tabs-1');
                $("#tabs").tabs("disable", 1);
            }
            if (paymentMode == "O" || paymentMode == "T") {
                $('#tabs').tabs('select', '#tabs-2');
                $("#tabs").tabs("disable", 0);
            }
        }


        function bindDealerList() {
            var cgColumns = [{ 'columnName': 'dealerid', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Dealer ID' }
                             //, { 'columnName': 'dealerid', 'width': '80', 'align': 'left', 'highlight': 2, 'label': 'Code' }
                             , { 'columnName': 'dealername', 'width': '120', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                             , { 'columnName': 'loccode', 'width': '50', 'align': 'left', 'highlight': 0, 'label': 'Location' }
                             , { 'columnName': 'address', 'width': '150', 'align': 'left', 'highlight': 0, 'label': 'Address' }
                               , { 'columnName': 'thana', 'width': '60', 'align': 'left', 'highlight': 0, 'label': 'Thana' }
                               , { 'columnName': 'division', 'width': '70', 'align': 'left', 'highlight': 0, 'label': 'Division' }
                               , { 'columnName': 'district', 'width': '70', 'align': 'left', 'highlight': 0, 'label': 'District' }
                                , { 'columnName': 'contact', 'width': '20', 'align': 'left', 'highlight': 0, 'label': 'Mobile' }
                                //, { 'columnName': 'sename', 'width': '50', 'align': 'left', 'highlight': 0, 'label': 'SE' }

            ];

            //var companyid = $('#' + hdnCompanyID).val();
            //var depthead = $('#' + hdnEmpCode).val();
            //var locationid = $('#' + ddlLocation).val();
            // var seid = $('#' + txtExecutiveID).val();

            //var serviceURL = GLAccountServiceLink + "?isterm=1&includeempty=1&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            var serviceURL = DealerListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            //serviceURL += "&companyid=" + companyid;
            serviceURL += "&ispaging=1";
            // serviceURL += "&locationid=" + locationid;
            //serviceURL += "&seid=" + seid;
            // serviceURL += "&empstatus=" + "A";
            //serviceURL += "&acctypefilter=" + Enums.GLAccountTypeFilter.AllAccount;
            // serviceURL += "&namecomptype=" + Enums.DataCompareType.Contains;



            var dealerIDElem = $('#' + txtDealerID);

            $('#' + btnDealerID).click(function (e) {
                //elmID = $(elem).attr('id');
                //$(elem).combogrid("show");
                $(dealerIDElem).combogrid("dropdownClick");
            });


            $(dealerIDElem).combogrid({
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
                width: 900,
                url: serviceURL,
                search: function (event, ui) {
                    //var companyCode = $('#' + ddlCompany).val();
                    //var branchCode = $('#' + hdnBranch).val();
                    //var deptCode = $('#' + hdnDepartment).val();
                    // var locationids = getCheckBoxes();
                    var locationid = $('#' + hdnLocationID).val();
                    var searchKey = $('#' + txtDealerName).val();

                    var newServiceURL = serviceURL + " &searchKey=" + searchKey + "&locationid=" + locationid;
                    $(this).combogrid("option", "url", newServiceURL);
                },
                select: function (event, ui) {
                    if (!ui.item) {
                        event.preventDefault();
                        $('#' + hdnDealerID).val('0');
                        $('#' + txtDealerID).val('');
                        $('#' + txtDealerName).val('');
                        $('#' + txtDealerAddress).val('');                     
                        $('#' + txtDealerPhone).val('');
                        $('#' + txtDealerDistrict).val('');
                        $('#' + txtDealerDivision).val('');
                        $('#' + txtDealerThana).val('');

                        $('#' + txtExecutiveId).val('');
                        $('#' + txtExecutiveName).val('');
                        $('#' + txtExecutiveMobile).val('');


                        return false;
                        //ClearGLAccountData(elemID);
                    }
                    if (ui.item.dealerid == '') {
                        event.preventDefault();
                        return false;
                        //ClearGLAccountData(elemID);
                    }
                    else {
                     
                        $('#' + hdnDealerID).val(ui.item.dealercode);
                        $('#' + txtDealerID).val(ui.item.dealercode);
                        $('#' + txtDealerName).val(ui.item.dealername);

                        $('#' + txtDealerAddress).val(ui.item.address);
                        $('#' + txtDealerPhone).val(ui.item.contact);
                        $('#' + txtDealerDistrict).val(ui.item.district);
                        $('#' + txtDealerDivision).val(ui.item.division);
                        $('#' + txtDealerThana).val(ui.item.thana);

                        $('#' + txtExecutiveId).val(ui.item.seid);
                        $('#' + txtExecutiveName).val(ui.item.sename);
                        $('#' + txtExecutiveMobile).val(ui.item.secontact);
                    }
                    return false;
                },

                lc: ''
            });


            $(dealerIDElem).blur(function () {
                var self = this;
                
                var delaerID = $(dealerIDElem).val();
                if (delaerID == '') {
                   
                    $('#' + hdnDealerID).val('0');
                    $('#' + txtDealerID).val('');
                    $('#' + txtDealerName).val('');
                    $('#' + txtDealerAddress).val('');
                    $('#' + txtDealerPhone).val('');
                    $('#' + txtDealerDistrict).val('');
                    $('#' + txtDealerDivision).val('');
                    $('#' + txtDealerThana).val('');


                    $('#' + txtExecutiveId).val('');
                    $('#' + txtExecutiveName).val('');
                    $('#' + txtExecutiveMobile).val('');
                }
                else {
                    var locationid = $('#' + hdnLocationID).val();
                    var serviceURL = DealerListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
                    serviceURL += "&ispaging=1&locationid=" + locationid;
                    var prcNo = GetProcessNo(delaerID, serviceURL);
                    if (prcNo == null) {
                        $('#' + hdnDealerID).val('0');
                        $('#' + txtDealerID).val('');
                        $('#' + txtDealerName).val('');
                        $('#' + txtDealerAddress).val('');
                        $('#' + txtDealerPhone).val('');
                        $('#' + txtDealerDistrict).val('');
                        $('#' + txtDealerDivision).val('');
                        $('#' + txtDealerThana).val('');
                        $('#' + txtExecutiveId).val('');
                        $('#' + txtExecutiveName).val('');
                        $('#' + txtExecutiveMobile).val('');
                    }
                }
            });
        }

        function GetProcessNo(processNo, serviceURL) {
            var prcNo = null;
            var isError = false;
            var isComplete = false;
            //ajax call
            var newServiceURL = serviceURL + " &selectedId=" + processNo;

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

        function bindDepositedExecutiveList() {
            var cgColumns = [{ 'columnName': 'seid', 'width': '80', 'align': 'left', 'highlight': 2, 'label': 'SeID' }
                             , { 'columnName': 'sename', 'width': '180', 'align': 'left', 'highlight': 4, 'label': 'Name' }

            ];



            //var serviceURL = GLAccountServiceLink + "?isterm=1&includeempty=1&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            var serviceURL = ExecutiveListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            //serviceURL += "&companyid=" + companyid;
            serviceURL += "&ispaging=1";




            var depositedByIdElem = $('#' + txtDepositedById);

            $('#' + btnDepositedBy).click(function (e) {
                //elmID = $(elem).attr('id');
                //$(elem).combogrid("show");
                $(depositedByIdElem).combogrid("dropdownClick");
            });


            $(depositedByIdElem).combogrid({
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
                width: 350,
                url: serviceURL,
                search: function (event, ui) {
                    //var companyCode = $('#' + ddlCompany).val();
                    //var branchCode = $('#' + hdnBranch).val();
                    //var deptCode = $('#' + hdnDepartment).val();
                    var locationid = $('#' + hdnLocationID).val();
                    //var newServiceURL = serviceURL + "&companycode=" + companyCode + "&branchcode=" + branchCode + "&deptcode=" + deptCode
                    var newServiceURL = serviceURL + "&locationid=" + locationid;
                    $(this).combogrid("option", "url", newServiceURL);


                },


                select: function (event, ui) {
                    if (!ui.item) {
                        event.preventDefault();
                        $('#' + txtDepositedById).val('');
                        $('#' + txtDepositedByName).val('');
                        $('#' + hdnDepositedById).val('');
                        return false;

                    }
                    if (ui.item.seid == '') {
                        event.preventDefault();
                        return false;
                        //ClearGLAccountData(elemID);
                    }
                    else {
                        $('#' + txtDepositedById).val(ui.item.seid);
                        $('#' + txtDepositedByName).val(ui.item.sename);
                        $('#' + hdnDepositedById).val(ui.item.seid);
                    }
                    return false;
                },

                lc: ''
            });


            // $(seIDElem).blur(function () {
            //  var self = this;

            //var seID = $(seIDElem).val();
            //  if (seID == '') {
            //    $('#' + hdnExecutiveID).val('0');
            //   //  $('#' + txtExecutiveName).val('');
            // }
            // });
        }


        function bindBankList() {
            var cgColumns = [{ 'columnName': 'bank_code', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Code' }
                             , { 'columnName': 'bank_desc', 'width': '200', 'align': 'left', 'highlight': 4, 'label': 'Name' }

            ];

            var bankserviceURL = BankListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            //serviceURL += "&companyid=" + companyid;
            bankserviceURL += "&ispaging=1";


            var bankIDElem = $('#' + txtBankName);

            $('#' + btnChkBank).click(function (e) {
                $(bankIDElem).combogrid("dropdownClick");
            });


            $(bankIDElem).combogrid({
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
                width: 400,
                url: bankserviceURL,
                search: function (event, ui) {
                    var bankname = $('#' + txtBankName).val();
                    var newServiceURL = bankserviceURL + " &dealerName=" + bankname;
                    $(this).combogrid("option", "url", newServiceURL);
                },
                select: function (event, ui) {
                    if (!ui.item) {
                        event.preventDefault();
                        $('#' + hdnBankId).val('');
                        $('#' + txtBankCode).val('');
                        $('#' + txtBankName).val('');

                        $('#' + txtBranchCode).val('');
                        $('#' + txtBranchName).val('');
                        $('#' + hdnBranchId).val('');

                        //$('#' + hdnDepositedAcNo).val('');
                        //$('#' + txtDepositedAcNo).val('');
                        //$('#' + txtDepositedAccName).val('');

                        return false;
                    }
                    if (ui.item.bank_code == '') {
                        event.preventDefault();
                        return false;

                    }
                    else {
                       
                        $('#' + hdnBankId).val(ui.item.bank_id);
                        $('#' + txtBankCode).val(ui.item.bank_code);
                        $('#' + txtBankName).val(ui.item.bank_desc);

                        $('#' + txtBranchCode).val('');
                        $('#' + txtBranchName).val('');
                        $('#' + hdnBranchId).val('');

                        //$('#' + hdnDepositedAcNo).val('');
                        //$('#' + txtDepositedAcNo).val('');
                        //$('#' + txtDepositedAccName).val('');
                    }
                    return false;
                },

                lc: ''
            });


            $(bankIDElem).blur(function () {
                var self = this;
                var delaerID = $(bankIDElem).val();
                if (delaerID == '') {
                    $('#' + hdnBankId).val('');
                    $('#' + txtBankCode).val('');
                    $('#' + txtBankName).val('');

                    $('#' + txtBranchCode).val('');
                    $('#' + txtBranchName).val('');
                    $('#' + hdnBranchId).val('');

                    //$('#' + hdnDepositedAcNo).val('');
                    //$('#' + txtDepositedAcNo).val('');
                    //$('#' + txtDepositedAccName).val('');
                }
            });

        }

        function bindBankBranchList() {

            var cgColumns = [{ 'columnName': 'branch_code', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Branch Code' }
                             , { 'columnName': 'branch_desc', 'width': '120', 'align': 'left', 'highlight': 4, 'label': 'Name' }

            ];

            var serviceURL = BankBranchListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            //serviceURL += "&companyid=" + companyid;
            serviceURL += "&ispaging=1";

            var bankBranchIDElem = $('#' + txtBranchName);

            $('#' + btnBranch).click(function (e) {
                $(bankBranchIDElem).combogrid("dropdownClick");
            });


            $(bankBranchIDElem).combogrid({
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
                    var bank_code = $('#' + txtBankCode).val();
                    var is_bank_mandatory = "Y";
                    var searchKey = $('#' + txtBranchCode).val();
                    var newServiceURL = serviceURL + " &searchKey=" + searchKey + " &bank_code=" + bank_code + " &is_bank_mandatory=" + is_bank_mandatory;
                    $(this).combogrid("option", "url", newServiceURL);
                },
                select: function (event, ui) {
                    if (!ui.item) {
                        event.preventDefault();
                        $('#' + hdnBranchId).val('');
                        $('#' + txtBranchCode).val('');
                        $('#' + txtBranchName).val('');
                       


                        //$('#' + hdnDepositedAcNo).val('');
                        //$('#' + txtDepositedAcNo).val('');
                        //$('#' + txtDepositedAccName).val('');
                        return false;

                    }
                    if (ui.item.branch_code == '') {
                        event.preventDefault();
                        return false;

                    }
                    else {

                        $('#' + hdnBranchId).val(ui.item.branch_id);
                        $('#' + txtBranchCode).val(ui.item.branch_code);
                        $('#' + txtBranchName).val(ui.item.branch_desc);
                       
                    }
                    return false;
                },

                lc: ''
            });


            $(bankBranchIDElem).blur(function () {
                var self = this;

                var delaerID = $(bankBranchIDElem).val();
                if (delaerID == '') {
                    $('#' + hdnBranchId).val('');
                    $('#' + txtBranchCode).val('');
                    $('#' + txtBranchName).val('');
                    

                    //$('#' + hdnDepositedAcNo).val('');
                    //$('#' + txtDepositedAcNo).val('');
                    //$('#' + txtDepositedAccName).val('');

                }
            });
        }
        

      
        function bindAccountHolderList() {

            var cgColumns = [{ 'columnName': 'acc_holder_name', 'width': '120', 'align': 'left', 'highlight': 4, 'label': 'Account Name' }
                             , { 'columnName': 'bank_desc', 'width': '120', 'align': 'left', 'highlight': 4, 'label': 'Bank' }
                             , { 'columnName': 'branch_desc', 'width': '120', 'align': 'left', 'highlight': 4, 'label': 'Branch' }
                             , { 'columnName': 'acc_no', 'width': '120', 'align': 'left', 'highlight': 4, 'label': 'AC No' }

            ];
            var serviceURL = AccountHolderListServiceLink + "?isterm=0&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;

            serviceURL += "&ispaging=1";

            var DepositedAcNoIDElem = $('#' + txtOnLineAccountNo);

            $('#' + btnDepositedAcNo).click(function (e) {

                $(DepositedAcNoIDElem).combogrid("dropdownClick");
            });


            $(DepositedAcNoIDElem).combogrid({
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

                    var bank_code = "";
                    var branch_code = "";

                    var is_ba_br_mandatory = "N";
                    var searchKey = $('#' + txtOnLineAccountNo).val();
                    var newServiceURL = serviceURL + " &searchKey=" + searchKey + " &bank_code=" + bank_code + " &branch_code=" + branch_code + " &is_ba_br_mandatory=" + is_ba_br_mandatory;
                    $(this).combogrid("option", "url", newServiceURL);
                },
                select: function (event, ui) {
                    if (!ui.item) {
                        event.preventDefault();
                        
                        $('#' + txtOnLineAccountNo).val('');
                        $('#' + txtOnlineAccHolderNameDtl).val('');
                        $('#' + txtOnlineBankCode).val('');
                        $('#' + txtOnlineBankName).val('');
                        $('#' + txtOnlineBranchCode).val('');
                        $('#' + txtOnlineBranchName).val('');
                        $('#' + hdnACC_HOLDER_ID).val('');
                        $('#' + hdnACC_HOLDER_DTL_ID).val('');
                        return false;

                    }
                    if (ui.item.acc_holder_name == '') {
                        event.preventDefault();
                        return false;

                    }
                    else {
 
                        $('#' + txtOnLineAccountNo).val(ui.item.acc_no);
                        $('#' + txtOnlineAccHolderNameDtl).val(ui.item.acc_holder_name);
                        $('#' + txtOnlineBankCode).val(ui.item.bank_code);
                        $('#' + txtOnlineBankName).val(ui.item.bank_desc);
                        $('#' + txtOnlineBranchCode).val(ui.item.branch_code);
                        $('#' + txtOnlineBranchName).val(ui.item.branch_desc);
                        $('#' + hdnACC_HOLDER_ID).val(ui.item.acc_id);
                        $('#' + hdnACC_HOLDER_DTL_ID).val(ui.item.acc_holder_dtl_id);
                        
                    }
                    return false;
                },

                lc: ''
            });

            $(DepositedAcNoIDElem).blur(function () {
                var self = this;
                var delaerID = $(DepositedAcNoIDElem).val();
                if (delaerID == '') {
                   
                    $('#' + txtOnLineAccountNo).val('');
                    $('#' + txtOnlineAccHolderNameDtl).val('');
                    $('#' + txtOnlineBankCode).val('');
                    $('#' + txtOnlineBankName).val('');
                    $('#' + txtOnlineBranchCode).val('');
                    $('#' + txtOnlineBranchName).val('');
                    $('#' + hdnACC_HOLDER_ID).val('');
                    $('#' + hdnACC_HOLDER_DTL_ID).val('');
                }
            });
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


        .auto-style1 {
            height: 26px;
        }
        .auto-style2 {
            height: 24px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hdnLocationID" runat="server" />
     <asp:HiddenField ID="hdnOnlineAccHolderId" runat="server" Value="0" />
    <asp:HiddenField ID="hdnCustId" runat="server" Value="0" />
    <asp:HiddenField ID="hdnMR_ID" runat="server" Value="0" />
    <asp:Button ID="btnDateValid" runat="server" OnClick="btnDateValid_Click" />
                                           <%-- <asp:HiddenField ID="hdnChkBankId" runat="server" Value="0" />--%>
    <%-- <div id="dvPageContent" style="width: 100%; height: 100%;">--%>



      <div id="dvContentHeader" class="dvContentHeader">
            <div id="dvHeader" class="dvHeader">
                <asp:Label ID="lblHeader" CssClass="lblHeader" runat="server" Text="Money Receipt"></asp:Label>
            </div>
            <div id="dvMsg" runat="server" class="dvMessage" style="width: 100%; min-height: 20px;
                height: auto; text-align: center;">
                <asp:Label ID="lblMessage" runat="server" Font-Size="Small" Width="100%" Height="16px"></asp:Label>
            </div>
        </div>
    <%-- class="dvContentMain"--%>
    <div id="dvContentMain" class="dvContentMain" >
        
        <div id="groupBox">
                        <div id="groupHeader" class="dvHeader">
                            <span style="font-weight: bold; color:red;  text-align: left;">Money Receipt Information :</span>
                        </div>
              <div id="groupContent" class="groupContent scrollBar">

        <table border="0"   >

            <tr>
                <td align="right" >
                    <asp:Label ID="lblSLNO" runat="server" Text="MR No. :" Font-Bold="true" Enabled="false"></asp:Label>
                </td>
                <td align="left" >
                    <asp:TextBox ID="txtMRNo" runat="server" Enabled="false" Font-Bold="true" Width="250px" CssClass="textBox"></asp:TextBox>&nbsp;
                       
                </td>
                <td align="right" class="auto-style1">
                    &nbsp;</td>
                <td align="right"  >
                    <asp:Label ID="lblReceivedDate" runat="server" Text="Receive Date :" Font-Bold="true"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtMrDate" runat="server" Font-Bold="true" CssClass="textBox textDate dateParse fldRequired enableIsDirty"></asp:TextBox>
                    </td> <td></td> <td></td> <td></td>
            </tr>
            <%--<tr>
                <td align="right" class="auto-style1">
                   <asp:Label ID="Label15" runat="server" Text="Segment Type:" Font-Bold="true" Enabled="false"></asp:Label> 
                </td>
                <td align="left">
                   <asp:DropDownList ID="ddlSegmentType" runat="server" CssClass="dropDownList" Width="163px">
                    </asp:DropDownList> 
                </td>
                <td align="right" class="auto-style1">  </td>
                <td align="right">
                     &nbsp;</td>
                 <td>
                     &nbsp;</td> <td></td> <td></td> <td></td>
            </tr>--%>

            <tr>
                <td style="" align="right">
                    <asp:Label ID="lblCustomerCode" runat="server" Text="Customer Code :" Font-Bold="true"></asp:Label>
                </td>
                
                            <td colspan="5">
                                <asp:TextBox ID="txtCustCode" runat="server" Width="100px" CssClass="textBox" TabIndex="1"></asp:TextBox>
                                <input id="btnCustID" type="button" value="" runat="server" class="buttonDropdown"
                                    tabindex="-1" />
                                <asp:TextBox ID="txtCustomerName" runat="server" Width="415px" CssClass="textBox"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td >
                                &nbsp;</td>
                             
                        


            </tr>

            <tr>
                <td style="" align="right">
                    <asp:Label ID="Label1" runat="server" Text="Address :" Font-Bold="true"></asp:Label>
                </td>
                <td style="" align="left" colspan="6" >
                    <asp:TextBox ID="txtCustAddress" runat="server" CssClass="textBox" Width="538px" Font-Bold="true"></asp:TextBox>
                     </td>
                
                 <td></td>  

            </tr>

            <tr>
                <td align="right" class="auto-style2">
                    <asp:Label ID="lblCustPhone" runat="server" Text="Phone :" Font-Bold="true"></asp:Label>
                </td>
                <td align="left" class="auto-style2">
                    <asp:TextBox ID="txtCustPhone" runat="server" CssClass="textBox" Font-Bold="true"></asp:TextBox>
                    &nbsp;</td>

                <td align="right" class="auto-style2">
                    &nbsp;</td>
                <td align="right" >

                    <asp:Label ID="lblDivisionName" runat="server" Text="Division:" Font-Bold="true"></asp:Label>
                </td>
                 <td class="auto-style2">

                    <asp:TextBox ID="txtCustDivision" runat="server" CssClass="textBox" Font-Bold="true"></asp:TextBox>
                    </td> <td class="auto-style2"></td>   <td class="auto-style2"></td>

            </tr>
            <tr>
                <td style="" align="right">
                    <asp:Label ID="lblDistrict" runat="server" Text="District :" Font-Bold="true"></asp:Label>
                </td>
                <td align="left" class="auto-style1">
                    <asp:TextBox ID="txtCustDistrict" runat="server" CssClass="textBox" Font-Bold="true"></asp:TextBox>
                    &nbsp;</td>

                <td style="" align="right">
                    &nbsp;</td>
                <td align="right" >
                    <asp:Label ID="lblThana" runat="server" Text="Thana :" Font-Bold="true"></asp:Label>
                </td>
                 <td>
                    <asp:TextBox ID="txtCustThana" runat="server" CssClass="textBox" Font-Bold="true"></asp:TextBox>
                    </td> <td></td> <td></td>  

            </tr>

            <tr>
                <td style="" align="right">
                    <asp:Label ID="lblSEName" runat="server" Text="Sales Executive :" Font-Bold="true"></asp:Label>
                </td>
                
                            <td colspan="7">
                                <asp:TextBox ID="txtExecutiveId" runat="server" Width="100px" CssClass="textBox"></asp:TextBox>
                                <asp:TextBox ID="txtExecutiveName" runat="server" Width="432px" CssClass="textBox"></asp:TextBox>
                            </td>
                <%--<td>--%>                                <%--<input id="btnExecutiveId" visible="false" type="button" value="" runat="server" style="display :none;" class="buttonDropdown"
                                    tabindex="-1" />--%>                            <%--</td>--%>                          <%--  <td colspan="5">
                                &nbsp;</td>--%>
                            


            </tr>

            <tr>
                <td style="" align="right">
                    <asp:Label ID="lblMobile" runat="server" Text="Mobile :" Font-Bold="true"></asp:Label>
                </td>
                <td align="left" class="auto-style1">
                    <asp:TextBox ID="txtExecutiveMobile" runat="server" CssClass="textBox" Font-Bold="true"></asp:TextBox>
                    &nbsp;</td>

                <td style="" align="right">
                    &nbsp;</td>
                <td align="right" >
                    <asp:Label ID="lblPaymentMedia" runat="server" Text="Payment Media :" Font-Bold="true"></asp:Label>

                </td>
                 <td>
                    <asp:DropDownList ID="ddlPaymentMedia" runat="server" CssClass="dropDownList" Width="163px">
                        <%--<asp:ListItem Value="G">General</asp:ListItem>
                        <asp:ListItem Value="A">AIT</asp:ListItem>
                        <asp:ListItem Value="S">Scrap</asp:ListItem>
                        <asp:ListItem Value="V">Vat</asp:ListItem>
                        <asp:ListItem Value="C">Cash</asp:ListItem>
                        <asp:ListItem Value="O">Online</asp:ListItem>
                        <asp:ListItem Value="C">Cheque</asp:ListItem>--%>
                                           </asp:DropDownList>

                </td> <td></td> <td></td> <td></td>

            </tr>

            <tr>
                <td style="" align="right">
                    <asp:Label ID="lblUserName" runat="server" Text=" Payment Mode :" Font-Bold="true"></asp:Label>
                </td>
                <td align="left"  >
                    <asp:DropDownList ID="ddlPaymentMode" runat="server" CssClass="dropDownList" Width="163px" TabIndex="2" AutoPostBack="True" OnSelectedIndexChanged="ddlPaymentMode_SelectedIndexChanged">
                        <asp:ListItem Value="O">Online</asp:ListItem>
                        <asp:ListItem Value="C" Selected="True">Cash</asp:ListItem>
                        <asp:ListItem Value="Q">Cheque</asp:ListItem>
                        <asp:ListItem Value="D">DD</asp:ListItem>
                        <asp:ListItem Value="T">TT</asp:ListItem>
                    </asp:DropDownList>

                    
                   
                </td>

                <td style="" align="right">
                    &nbsp;</td>
                <td align="right" >
                    <asp:Label ID="lblRcvAmount" runat="server" Text="Cash MR Amount :" Font-Bold="true">
                    </asp:Label>
                

                </td>
                 <td>
                     <asp:TextBox ID="txtRcvAmount"  onkeypress="return isNumberKey(event,this);" runat="server" Font-Bold="true" CssClass="textBox textNumberFormat"></asp:TextBox>
                

                </td> <td></td> <td></td> <td></td>

            </tr>

            <tr>
                <td style="" align="right">
                    <asp:Label ID="lblDealerSalesDate" runat="server" Text="Remarks :" Font-Bold="true"></asp:Label>
                </td>
                <td align="left" colspan="3">

                    <asp:TextBox ID="taMRRemarks" TextMode="MultiLine" runat="server" Font-Bold="true" Width="375px" CssClass="textAreaAutoSize"></asp:TextBox>
                </td>

                <td style="" align="center">
                     <asp:TextBox ID="txtAutho_Status" runat="server" BackColor="Red" ForeColor="Green" Font-Bold="true"  Width="50px" CssClass="textBox"></asp:TextBox>
                </td>
                
                 <td>
                     &nbsp;</td>  
                <td></td>

            </tr>
            <tr>
                <td style="" align="right">
                    <asp:Label ID="lblIsRetail" runat="server" Text="Is Retail :" Font-Bold="true" Visible="false" ></asp:Label>
                </td>
                <td align="left" colspan="3">

                   <asp:DropDownList ID="ddlIsRetail" runat="server" CssClass="dropDownList" OnSelectedIndexChanged="ddlIsRetail_SelectedIndexChanged" AutoPostBack="true" Visible="false">
                       <asp:ListItem Selected="True" Text="No" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                   </asp:DropDownList>
                    <asp:Label ID="lblonlytxt" runat="server" Text="Only Retail Report Purpose" BackColor="Yellow" Visible="false"></asp:Label>
                  
                </td>

                <td style="" align="center">
                   
                </td>
                
                 <td>
                     &nbsp;</td>  
                <td></td>

            </tr>
             <tr>
                <td style="" align="right">
                    <asp:Label ID="lblinvno" runat="server" Text="Invoice No :" Font-Bold="true" Visible="false"></asp:Label>
                </td>
                <td align="left" colspan="3">

                    <asp:TextBox ID="txtInvoiceNo"  runat="server" Font-Bold="true" CssClass="textBox" Visible="false"></asp:TextBox>
                    <asp:Label ID="lbltxt" runat="server" Text="**Invoice Authorized First" Font-Bold="true" ForeColor="Red" Visible="false"></asp:Label>
                   
                </td>

                <td style="" align="center">
                   
                </td>
                
                 <td>
                     &nbsp;</td>  
                <td></td>

            </tr>

        </table>
                  </div>
            </div>

        <div id="dvGridSeparator" runat="server"   style="width: 100%;">
                                    </div>
           <div id="groupDataHeaderCredit" class="dvHeader" >
                                        <span style="font-weight: bold; color:red;  text-align: left;"> Cheque/DD/TT/Online Details: </span>
                                    </div>                         

        <div id="divCheq"   class="groupBox">
            
            <table border="0"    >

                        <tr>
                            <td align="right">
                                <asp:Label ID="Label5" runat="server" Text="Chq.No/TT No:" Font-Bold="true" Enabled="false"></asp:Label>
                            </td>
                            <td align="left" style="width : 258px;">
                                <asp:TextBox ID="txtTTNo" runat="server" Font-Bold="true" Width="250px" CssClass="textBox"></asp:TextBox>
                       
                            </td>
                            <td>
                       
                            <%--<input id="btnChkBank" type="button" value="" runat="server" class="buttonDropdown" tabindex="-1" />--%></td>
                            <td align="right" >
                                <asp:Label ID="Label9" runat="server" Text="Credit Date :" Font-Bold="true"></asp:Label>
                            </td>

                            <td align="left">
                                <asp:TextBox ID="txtChkCreditDate" runat="server" Font-Bold="true" CssClass="textBox textDate dateParse fldRequired "></asp:TextBox>
                            </td>
                            <td  align="left">
                                &nbsp;</td>
                            <td  align="right">
                                &nbsp;</td>
                            <td  align="left">
                                <asp:HiddenField ID="hdnBankId" runat="server" Value="0" />
                            </td>

                        </tr>

                <tr>
                    <td align="right">
                        <asp:Label ID="lblChqueBank" runat="server" Text="Cheque Bank :" Font-Bold="true"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtBankCode" runat="server" Width="40px" CssClass="textBox"></asp:TextBox>
                      <asp:TextBox ID="txtBankName" runat="server" Width="205px" CssClass="textBox"></asp:TextBox>
                    </td> <td>
                        <input id="btnChkBank" type="button" value="" runat="server" class="buttonDropdown"
                                    tabindex="-1" />
                          </td>
                    <td align="right">
                                <asp:Label ID="Label10" runat="server" Text="Cheque Status :" Font-Bold="true"></asp:Label>
                       </td><td align="left">
                                <asp:TextBox ID="txtChkStatus"  runat="server" Font-Bold="true" Text="Credited" CssClass="textBox"></asp:TextBox>
                            </td>
                    <td>
                        &nbsp;</td> <td></td>
                    <td>
                         <asp:HiddenField ID="hdnBranchId" runat="server" Value="0" />
                    </td>
                </tr>
                <tr>
                            <td align="right">
                        <asp:Label ID="Label7" runat="server" Text="Cheque Branch :" Font-Bold="true"></asp:Label>
                            </td>
                            <td align="left">
                           <asp:TextBox ID="txtBranchCode" runat="server" Width="40px" CssClass="textBox" TabIndex="-1"></asp:TextBox>
                           <asp:TextBox ID="txtBranchName" runat="server"  Width="205px" CssClass="textBox" TabIndex="-1"></asp:TextBox>
                                </td>
                            <td>
                        <input id="btnBranch" type="button" value="" runat="server" class="buttonDropdown"
                                    tabindex="-1" /></td>
                            <td align="right">
                                &nbsp;</td>
                            <td align="left">
                                
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td></td>
                            <td><asp:HiddenField ID="hdnACC_HOLDER_ID" runat="server" Value="0" /></td>
                        </tr>
                <tr>
                    <td align="right">
                       <asp:Label ID="Label6" runat="server" Text="Deposited A/C No. :" Font-Bold="true" Enabled="false"></asp:Label>
                    </td>
                    <td align="left">
                       <asp:TextBox ID="txtOnLineAccountNo" runat="server" Font-Bold="true" width="250px" CssClass="textBox" TabIndex="3"></asp:TextBox>
                       </td>
                    <td><input id="btnDepositedAcNo" type="button" value="" runat="server" class="buttonDropdown" tabindex="-1" /></td>
                    <td align="right">
                                <%--<asp:Label ID="Label7" runat="server" Text="MR Amount :" Font-Bold="true" Enabled="false"></asp:Label>--%>
                                <asp:Label ID="Label25" runat="server" Text="Amount :" Font-Bold="true"></asp:Label>
                                        </td>
                    <td align="left">
                                <%--<asp:TextBox ID="txtOnlineAmount" runat="server"  onkeypress="return isNumberKey(event,this);" Font-Bold="true" BackColor="Yellow" CssClass="textBox"></asp:TextBox>--%>
                                
                                <asp:TextBox ID="txtChkChequeAmt" runat="server" BackColor="Khaki" Font-Bold="true" CssClass="textBox textNumberFormat" TabIndex="4"  onkeypress="return isNumberKey(event,this);"></asp:TextBox>
                                
                                        </td>
                    <td></td>
                    <td></td>
                    <td><asp:HiddenField ID="hdnACC_HOLDER_DTL_ID" runat="server" Value="0" /></td>
                </tr>
                <tr>
                            <td style="" align="right">
                                <asp:Label ID="Label11" runat="server" Text="A/C Holder Name. :" Font-Bold="true" Enabled="false"></asp:Label>
                            </td>
                            
                                        <td align="left">
                                            <asp:TextBox ID="txtOnlineAccHolderNameDtl" runat="server" Width="250px" CssClass="textBox" ></asp:TextBox>
                                        </td>
                                        <td>
                                            <%--<input id="btnOnlineBranch" type="button" value="" runat="server" class="buttonDropdown" tabindex="-1" />--%>
                                        </td>
                                        <td  align="right">
                                <asp:Label ID="Label12" runat="server" Text="Bank :" Font-Bold="true" Enabled="false"></asp:Label>
                                        </td>
                                        <td align="left">
                                
                                            <asp:TextBox ID="txtOnlineBankCode" runat="server" Width="40px" CssClass="textBox" ></asp:TextBox>
                                            <asp:TextBox ID="txtOnlineBankName" runat="server" Width="205px" CssClass="textBox" ></asp:TextBox>
                                        </td>
                                        <td>
                                
                                            &nbsp;</td>
                                   
                            <td style="" align="right">
                                &nbsp;</td>
                            <td style="" align="left">
                              <asp:HiddenField ID="hdnREG_ID" runat="server" Value="0" />
                            </td>


                        </tr>
                <tr>
                    <td align="right">
                         <asp:Label ID="Label3" runat="server" Text="Deposited By :" Font-Bold="true"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDepositedById" runat="server" Width="85px" CssClass="textBox"></asp:TextBox>
                        <asp:TextBox ID="txtDepositedByName" runat="server" CssClass="textBox"></asp:TextBox>
                    </td>
                    <td>
                         <input id="btnDepositedBy" type="button" value="" runat="server" class="buttonDropdown"
                                                            tabindex="-1" />
                    </td>
                    <td align="right">
                                <asp:Label ID="Label13" runat="server" Text="Branch :" Font-Bold="true" Enabled="false"></asp:Label>
                            </td>
                    <td> 
                                
                                            <asp:TextBox ID="txtOnlineBranchCode" runat="server" Width="40px" CssClass="textBox" ></asp:TextBox>
                                            <asp:TextBox ID="txtOnlineBranchName" runat="server" Width="205px" CssClass="textBox" ></asp:TextBox>
                                        </td>
                    <td></td>
                    <td></td>
                    <td> 
                        <asp:HiddenField ID="hdnDepositedById" runat="server" Value="0" />
                    </td>
                </tr>
                        
                        
                        
                        
                        
                    </table>
        </div>
          <div id="Div1" runat="server"   style="width: 100%;">
                                    </div>
         

     </div>
    <div id="dvContentFooter" class="dvContentFooter" >
        <table>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="btnAddNew" runat="server" Text="New" CssClass="buttonNew" OnClick="btnAddNew_Click" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonCancel checkIsDirty" Visible="false" />
                </td>
                <td>
                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonSave" OnClick="btnSave_Click" OnClientClick="if ( ! SaveConfirmation()) return false;" />
                    <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="buttonEdit" OnClick="btnEdit_Click" />
                </td>
                <td>
                    <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="buttonDelete" Visible="false" />
                </td>

                <td>
                    <asp:Button ID="btnRefresh" runat="server" Text="Refresh" CssClass="buttonRefresh checkIsDirty" OnClick="btnRefresh_Click1" />
                </td>

                <td>
                    <asp:Button ID="btnAuthorized" runat="server" Text="Authorized" CssClass="buttonAthorize" OnClientClick="if ( ! UserAuthorizeConfirmation()) return false;" OnClick="btnAuthorized_Click" />
                </td>

                <td>
                    <input id="btnClose" type="button" runat="server" class="buttonClose" value="Close" onclick="if (ContentForm) { ContentForm.CloseForm(); }" />
                </td>

                <td>

                    <asp:Button ID="btnPrintMR" runat="server" Text="Money Receive" CssClass="buttoncommon" OnClick="btnPrintMR_Click" OnClientClick="showOverlay();" />

                </td>
                 <td>
                        <asp:Label ID="Label2" runat="server" Text="Logo" Font-Bold="true"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlLogoType" runat="server" CssClass="dropDownList">
                            <asp:ListItem Value="PBL">PBL</asp:ListItem>
                            <asp:ListItem Value="PDL">PDL</asp:ListItem> 
                            <asp:ListItem Value="LUB">LUB</asp:ListItem> 
                                                     
                        </asp:DropDownList>
                    </td>
                <td>
                        <asp:Label ID="Label14" runat="server" Text="Report View"></asp:Label>
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
                        <asp:Label ID="Label16" runat="server" Text="Type:"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlReportViewType" runat="server" CssClass="dropDownList">
                            <asp:ListItem Value="0">Screen</asp:ListItem>
                            <asp:ListItem Selected="True" Value="1">PDF</asp:ListItem>
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
           <tr></tr>
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
