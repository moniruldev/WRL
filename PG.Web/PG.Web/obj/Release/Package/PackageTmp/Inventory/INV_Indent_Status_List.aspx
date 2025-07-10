<%@ Page Title="" Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="INV_Indent_Status_List.aspx.cs" Inherits="PG.Web.Inventory.INV_Indent_Status_List " %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  
 <%--<script src="../javascript/jquery.ui.combogrid.js" type="text/javascript"></script>
    <script src="../javascript/jquery.attributeobserver.js" type="text/javascript"></script>
    <link href="../css/jquery.ui.combogrid.css" rel="stylesheet" type="text/css" />--%>

      <script src="../javascript/jquery.ui.combogrid.js" type="text/javascript"></script>
    <link href="../css/jquery.ui.combogrid.css" rel="stylesheet" type="text/css" />

    <%-- <script src="http://code.jquery.com/jquery-1.10.2.js" type="text/javascript"></script>--%>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css"/> 
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap-theme.min.css"/>  
     <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js" type="text/javascript"></script> 

<%--    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css">
   <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap-theme.min.css"/>  
  <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.16.0/umd/popper.min.js" type="text/javascript"></script>
  <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js" type="text/javascript"></script>--%>
     

    <script language="javascript" type="text/javascript">
        // <!CDATA[


        var ReportViewPageLink = '<%=this.ReportViewPageLink%>';
        var ReportViewPDFPageLink = '<%=this.ReportViewPDFPageLink%>';
        var ReportPrintPageLink = '<%=this.ReportPrintPageLink%>';
        var ReportPDFPageLink = '<%=this.ReportPDFPageLink%>';
        var reportURL = '';

      
        var ItemListServiceLink = '<%=this.ItemListServiceLink%>';
        var isPageResize = true;
        ContentForm.CalendarImageURL = "../image/calendar.png";



<%--        var BankListServiceLink = '<%=this.BankListServiceLink%>';--%>


      <%--  var txtBankCode = '<%=txtBankCode.ClientID%>';
        var txtBankName = '<%=txtBankName.ClientID%>';
        var btnBank = '<%=btnBank.ClientID%>';--%>
        var hdnBankId = '<%=hdnBankId%>';

        var btnItemLoad = '<%= btnItemLoad.ClientID%>';
        var hdnItemIdForFilter = '<%= hdnItemIdForFilter.ClientID%>';
        var txtItemName = '<%= txtItemName.ClientID%>';

        var btnGridPageGoTo = '<%=btnGridPageGoTo.ClientID %>';
        var txtGridPageNo = '<%=txtGridPageNo.ClientID %>';
        var txtComments = '<%=txtComments.ClientID%>';
        var btnSave = '<%=btnSave.UniqueID%>';


        function ShowProgress() {
            $('#' + updateProgressID).show();
        }

        function UserConfirmation() {
            return confirm("Are you sure you want to Stock Issue and Print DC,GP ?");
        }

        //function openModal() {
        //    $('[id*=myModal]').modal('show');
        //}

        //function openModalComm() {
        //    $('[id*=myModalCom]').modal('show');
        //}
        

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


        function tbopenp(key, userid) {
            key = key || '';
           // alert(key);
            var url = IForm.RootPath + "/Inventory/INVNewIGR.aspx?id=" + key;
            //var url = IForm.RootPath + "Accounting/GeneralLedger/JournalFull.aspx?id=" + key;


            if (IForm.PageMode == Enums.PageMode.InTab) {

                var tdata = new xtabdata();
                tdata.linktype = Enums.LinkType.Direct;
                tdata.id = 0;
                tdata.name = "Entry Form View";
                //tdata.label = "User: " + userid;
                tdata.label = "Entry Form View";
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
                        //bindItemList(gridViewIDDet);
                        //bindItemGroupList(gridViewIDDet);
                    }

                }
                //$('#' + dvGridDetailsPopup).parent().appendTo(jQuery("form:first"));
                //gridTaskAfter();

            });


            //if ($('#' + txtBankCode).is(':visible')) {
            //    bindBankList();
            //}
           

            //if ($('#' + txtInvoiceNo).is(':visible')) {

            //    bindInvoiceList();
            //}

            if ($('#' + txtItemName).is(':visible')) {
                bindItemList();
            }


        });


        function bindItemList() {

            var cgColumns = [{ 'columnName': 'itemname', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                             , { 'columnName': 'uomname', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Uom Name' }
                             , { 'columnName': 'itemcode', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Code' }
                             , { 'columnName': 'itemgroupdesc', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Group Name' }
                             , { 'columnName': 'class_name', 'width': '120', 'align': 'left', 'highlight': 4, 'label': 'Class Name' }
                             , { 'columnName': 'item_type', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Item Type' }



            ];
            var serviceURL = ItemListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;


            serviceURL += "&ispaging=1&isigr=1";
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

                    //var vgroupid = 0;
                    //var groupName = $('#' + txtGroupName).val();
                    //if (groupName != "") {
                    //    vgroupid = $('#' + hdnGroupID).val();
                    //    if (vgroupid == "0" || vgroupid == "") {
                    //        vgroupid = 0;
                    //    }
                    //} else {
                    //    $('#' + hdnGroupID).val('0');

                    //}

                    var newServiceURL = serviceURL;//+ "&groupid=" + vgroupid;

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

        function bindBankList() {
            var cgColumns = [{ 'columnName': 'bank_code', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Code' }
                             , { 'columnName': 'bank_desc', 'width': '120', 'align': 'left', 'highlight': 4, 'label': 'Name' }

            ];

            var bankserviceURL = BankListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            //serviceURL += "&companyid=" + companyid;
            bankserviceURL += "&ispaging=1";


            var bankIDElem = $('#' + txtBankCode);

            $('#' + btnBank).click(function (e) {
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
                width: 600,
                url: bankserviceURL,
                search: function (event, ui) {
                    var searchKey = $('#' + txtBankCode).val();

                    var newServiceURL = bankserviceURL + " &searchKey=" + searchKey;
                    $(this).combogrid("option", "url", newServiceURL);
                },
                select: function (event, ui) {
                    if (!ui.item) {
                        event.preventDefault();
                        $('#' + hdnBankId).val('');
                        $('#' + txtBankCode).val('');
                        $('#' + txtBankName).val('');

                        //$('#' + txtBranchCode).val('');
                        //$('#' + txtBranchName).val('');
                        //$('#' + hdnBranchId).val('');


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
                        $('#' + hdnBankId).val(ui.item.bank_code);
                        $('#' + txtBankCode).val(ui.item.bank_code);
                        $('#' + txtBankName).val(ui.item.bank_desc);

                        //$('#' + txtBranchCode).val('');
                        //$('#' + txtBranchName).val('');
                        //$('#' + hdnBranchId).val('');

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

                    //$('#' + txtBranchCode).val('');
                    //$('#' + txtBranchName).val('');
                    //$('#' + hdnBranchId).val('');

                    //$('#' + hdnDepositedAcNo).val('');
                    //$('#' + txtDepositedAcNo).val('');
                    //$('#' + txtDepositedAccName).val('');
                }
            });

        }

        function bindDealerList() {
            var cgColumns = [{ 'columnName': 'dealercodeex', 'width': '50', 'align': 'left', 'highlight': 4, 'label': 'Ex ID' }
                           , { 'columnName': 'dealerid', 'width': '80', 'align': 'left', 'highlight': 2, 'label': 'Dlr ID' }
                             , { 'columnName': 'dealername', 'width': '180', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                             , { 'columnName': 'loccodename', 'width': '100', 'align': 'left', 'highlight': 0, 'label': 'Location' }
                             , { 'columnName': 'address', 'width': '150', 'align': 'left', 'highlight': 0, 'label': 'Address' }


            ];

           

            //var serviceURL = GLAccountServiceLink + "?isterm=1&includeempty=1&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            var serviceURL = DealerListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            //serviceURL += "&companyid=" + companyid;
            serviceURL += "&excludefatorydealer=1";
            serviceURL += "&dealerType=D";
            serviceURL += "&ispaging=1";




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
                width: 650,
                url: serviceURL,
                search: function (event, ui) {
                    //var companyCode = $('#' + ddlCompany).val();
                    //var branchCode = $('#' + hdnBranch).val();
                    var dealerCode = $('#' + txtDealerID).val();
                    var locationid = $('#' + hdnLocationID).val();

                    //debugger;
                    // var seid = $('#' + txtExecutiveID).val();
                    //var newServiceURL = serviceURL + "&locationid=" + locationid + "&dealerCode=" + dealerCode;
                    var newServiceURL = serviceURL + "&dealerCode=" + dealerCode;
                    $(this).combogrid("option", "url", newServiceURL);


                },

                select: function (event, ui) {
                    if (!ui.item) {
                        $('#' + txtDealerID).val('');
                        $('#' + txtDealerName).val('');
                        $('#' + txtDealerAddress).val('');
                       
                        event.preventDefault();
                        return false;
                    }

                    if (ui.item.dealername == '') {
                        $('#' + txtDealerID).val('');
                        $('#' + txtDealerName).val('');
                        $('#' + txtDealerAddress).val('');
                        
                        event.preventDefault();
                        return false;
                    }
                    else {
                        $('#' + txtDealerID).val(ui.item.dealerid);
                        $('#' + txtDealerName).val(ui.item.dealername);
                        $('#' + txtDealerAddress).val(ui.item.address);
                        
                    }
                    return false;
                },

                lc: ''
                
            });

            $(dealerIDElem).blur(function () {
                var self = this;
                var groupID = $(dealerIDElem).val();
                if (groupID == '') {
                    $('#' + txtDealerID).val('');
                    $('#' + txtDealerName).val('');
                    $('#' + txtDealerAddress).val('');
                   
                }
                else {
                    // var locationid = $('#' + hdnLocationID).val();
                    var dealerCode = $('#' + txtDealerID).val();
                    var serviceURL = DealerListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
                    //serviceURL += "&dealerCode=" + dealerCode;
                    serviceURL += "&ispaging=1";
                    //alert(serviceURL);
                    //debugger;
                    var prcNo = GetdealerID(dealerCode, serviceURL);
                    //alert(prcNo);
                    if (prcNo == null) {
                        $('#' + txtDealerID).val('');
                        $('#' + txtDealerName).val('');
                        $('#' + txtDealerAddress).val('');
                       
                    }
                }
            });
            
        }


        //$("#" + txtComments).keydown(function (e) {
        //    if (e.keyCode == 13) {
        //        __doPostBack(btnSave, '');
        //    }
        //});






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

        .modal-dialog {
 
          width: 1080px;
 
        }

        .FixedHeader {
            position: relative;
            background-color: white;
        }

        #dvMessage {
            height: 20px;
        }
         .buttonAttach
        {
               background-color: #0094ff; 
                  border: none;
                  color: white;
                  padding: 4px 6px;
                  text-align: center;
                  text-decoration: none;
                  display: inline-block;
                  font-size: 12px;
                  margin: 4px 2px;
                  transition-duration: 0.4s;
                  letter-spacing:0px;
                  text-transform:capitalize;
                  border-radius:4px;
                  cursor: pointer;
                  white-space:nowrap;
                  

        }
        .buttonAttach:hover {
                  background-color: #0094ff;
                  text-decoration:none;
                  color: white;
                  box-shadow: 0 0 15px 2px #134eef;
                  
                   }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hdnBankId" runat="server" Value="0" />
    <div id="dvPageContent" style="width: 100%; height: auto;">
        <div id="dvContentHeader" class="dvContentHeader">
            <div id="dvHeader" class="dvHeader">
                <asp:Label ID="lblHeader" CssClass="lblHeader" runat="server" Text="Indent Register:"></asp:Label>
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
              <td>
              </td>
              <td align="right">
               <asp:Label ID="lblFromDate" runat="server" Text="From Date:" style="height: 19px; width: 43px; " Font-Bold="True" Visible="true"></asp:Label>
              </td>
              <td align="left">
                 <asp:TextBox ID="txtFromDate" runat="server" CssClass="textBox textDate dateParse" Style="text-align: left;" Width="100px"></asp:TextBox>
                
              </td>
              <td align="right">
                    <asp:Label ID="lblInvoiceToDate" runat="server" Text="To Date:" style="height: 19px; width: 43px; " Font-Bold="True" Visible="true"></asp:Label>
                   
              </td>
            
              <td>
                 <asp:TextBox ID="txtTodate" runat="server" CssClass="textBox textDate dateParse" Style="text-align: left;" Width="100px"></asp:TextBox>&nbsp;
                   
                 <asp:RegularExpressionValidator runat="server" ControlToValidate="txtTodate" ValidationExpression="(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$" ErrorMessage="Invalid date format." ValidationGroup="Group1" ForeColor="Red" />
              </td>
               
             
                 
            </tr>
             <tr>
              <td>
              </td>
              <td align="right">
               <asp:Label ID="lblIndtNo" runat="server" Text="Indent No:" style="height: 19px; width: 43px; " Font-Bold="True" Visible="true"></asp:Label>
              </td>
              <td align="left" colspan="4">
                 <asp:TextBox ID="txtIndtNo" runat="server" CssClass="textBox  enableIsDirty" Style="text-align: left;" Width="200px"></asp:TextBox>
                
              </td>
              <td align="right">
                   
              </td>
            
              <td>
                   
              </td>
               
             
                 
            </tr>

               <tr>
              <td>
              </td>
              <td align="right">
                 <asp:Label id="lblDeptName" runat="server" Text="Department Name:" Font-Bold="true" ></asp:Label>
              </td>
              <td align="left" colspan="4">
             <asp:DropDownList ID="ddlDeptName" runat="server" Width="240"  CssClass="dropDownList enableIsDirty"> </asp:DropDownList>
                
              </td>
              <td align="right">
                   
              </td>
            
              <td>
              </td>
               
             
                 
            </tr>
                    <tr>
                                        <td></td>
                                        <td style="" align="right">
                                            <asp:Label ID="Label3" runat="server" Text="Item:" Visible="false"></asp:Label>
                                        </td>
                                        <td style="" align="left">
                                            <asp:TextBox ID="txtItemName" Width="235px" runat="server" CssClass="textBox required" Enabled="true" Visible="false"></asp:TextBox>

                                        </td>
                                        <td>
                                            <input id="btnItemLoad" type="button" value="" runat="server" class="buttonDropdown" tabindex="-1"  visible="false"/>
                                        </td>

                                        <td>
                                            <asp:HiddenField ID="hdnItemIdForFilter" runat="server" />
                                        </td>

                                    </tr>
                  <%-- <tr>
                        <td></td>
                       
                       <td align="right">
                        <asp:Label ID="lblAppType" runat="server" Text="Type:" Font-Bold="true" ></asp:Label>
                         </td>
                       <td colspan="4"> 
                        <asp:DropDownList ID="ddlAppType" runat="server" CssClass="dropDownList"  Font-Bold="true" Width="220px">
                                                             
                       </asp:DropDownList>
                      </td>
                   </tr>--%>

         
                   <tr class="rowParam">
                                           <td></td>
                                            <td align="right" class="auto-style1">
                                   <asp:Label ID="lblstatus" runat="server" Text="Status:" Font-Bold="True" ></asp:Label>
                                            </td>
                                            <td colspan="4">
                                               <asp:DropDownList ID="ddlstatus" runat="server" CssClass="dropDownList "  >
                                                
                                              
                                            </asp:DropDownList>               

                                            </td>
                                         
                                         
                                         
                                        </tr>


           <tr>
              <td>
              </td>
              <td>
            
              </td>
              <td colspan="2">
                <asp:Button ID="btnFind" runat="server" Text="Show Data" CssClass="buttonRefresh checkIsDirty" OnClick="btnFind_Click"  />
              </td>
             <td align="left">
               <%--  <input id="btnAddNew" type="button" runat="server" value="New Application" class="buttonNew" style="padding-left: 22px; width: 120px;" />--%>
             </td>
               
            </tr>
         
         
            
         </table>    
            </div>
            <br />
            <div id="dvControls" style="width: 1150px">
                <div id="dvControlsInner" class="groupBoxContainer boxShadow" style="width: 1050px">
                    <div id="dvGridContainer" style="width: 100%; height: auto; text-align: left;">
                        <div id="dvGridHeader" style="width: 100%; height: 25px; font-size: smaller;" class="subHeader">
                           <table style="height: 100%; color: #FFFFFF; font-weight: bold; text-align: center;" class="defFont"  cellspacing="1" cellpadding="1" rules="all">
                                <tr>
                                  <%--  <td width="50px" align="left">
                                        Action
                                    </td>--%>
                                      <td width="200px" align="center">
                                        Indent No
                                    </td>
                                     <td width="100px" align="center">
                                       Indt. Date
                                    </td>
                                  
                                     <td width="150px" align="center">
                                       Department
                                    </td>
                                   <%-- <td width="200px" align="center">
                                       Item
                                    </td>--%>
                                    
                                   
                                   <td width="150px" align="center">
                                       
                                     Status
                                    </td>
                                    <td width="120px" align="center">
                                       
                                      Status Details
                                    </td>
                                  <%--   <td width="80px" align="left">
                                       
                                      Pdf View
                                    </td>
                                     <td width="100px" align="left">
                                       
                                      Attachment
                                    </td>--%>
                                    <td width="100px" align="left">
                                       
                                      Comments
                                    </td>
                                  <%--   <td width="80px" align="center">
                                      Reject
                                    </td>--%>
                                </tr>
                            </table>
                        </div>
                        <div id="dvGrid" style="width: auto; height: 350px; overflow: auto;">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="1" Width="830px"
                                CellSpacing="1" ForeColor="#333333" GridLines="None" Font-Names="Arial" Font-Size="9pt" DataKeyNames="INDT_ID"
                                OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="GridView1_RowDeleting"
                                EmptyDataText="There is no record" PageSize="15" OnPageIndexChanging="GridView1_PageIndexChanging"
                                OnSelectedIndexChanged="GridView1_SelectedIndexChanged" ShowHeader="False" OnRowCommand="GridView1_RowCommand">
                                <PagerSettings Mode="NumericFirstLast" />
                                  <RowStyle BackColor="#A1DCF2" ForeColor="#333333" />
                                <Columns>
                                    <asp:HyperLinkField HeaderText="" Text="View" Visible="false">
                                        <ControlStyle Height="20px" Width="40px" />
                                        <ItemStyle Width="50px" />
                                    </asp:HyperLinkField>


                                    <asp:TemplateField HeaderText="Indt ID" Visible="false">
                                      <ItemTemplate>
                                     <asp:Label ID="lblIndtID" runat="server" Text='<%# Bind("INDT_ID") %>' Style="text-align: center;" ></asp:Label>
                                                                   
                                      </ItemTemplate>
                                       <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sub type ID" Visible="false">
                                      <ItemTemplate>
                                     <asp:Label ID="lblSubtypeID" runat="server" Text='<%# Bind("INDT_ID") %>' Style="text-align: center;" ></asp:Label>
                                                                   
                                      </ItemTemplate>
                                       <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="IndtNo" Visible="false">
                                      <ItemTemplate>
                                     <asp:Label ID="lblIndtNo" runat="server" Text='<%# Bind("INDT_NO") %>' Style="text-align: center;"></asp:Label>
                                                                   
                                      </ItemTemplate>
                                       <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>
                                       <asp:BoundField DataField="INDT_NO" HeaderText="Date" DataFormatString="{0:dd-MMM-yyyy}" ItemStyle-Width="200px" />
                                     <asp:BoundField DataField="INDT_DATE" HeaderText="Date" DataFormatString="{0:dd-MMM-yyyy}" ItemStyle-Width="100px" />
                                      <%--<asp:BoundField DataField="EMP_ID" HeaderText="EMPID" ItemStyle-Width="95px" ItemStyle-HorizontalAlign="Center"  />--%>
                                   
                                      <asp:BoundField DataField="FROM_DEPARTMENT_NAME" HeaderText="DEPARTMENT" ItemStyle-Width="150px"  ItemStyle-HorizontalAlign="Center" />
                                    
                                <%--   <asp:BoundField DataField="ITEM_NAME" HeaderText="Type" ItemStyle-Width="200px" ItemStyle-HorizontalAlign="Center"  />--%>
                                   
                                    
                                     <asp:BoundField DataField="STATUS_NAME" HeaderText="STATUS" ItemStyle-Width="150px" ItemStyle-HorizontalAlign="Center"  />
                                      <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                    <asp:LinkButton ID="btnStatusdtl" runat="server"  CssClass="buttonAttach" CommandName="statusd"  CommandArgument='<%# Bind("INDT_NO") %>' ><i class="glyphicon glyphicon-info-sign" style="padding-right:4px;color:blue;font-size:14px;"></i>Details</asp:LinkButton>
                                    </ItemTemplate>
                                         
                                   </asp:TemplateField>
                             <%--    <asp:TemplateField HeaderText="Action"  ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                    <asp:LinkButton ID="btnMemoprint" runat="server"  CommandName="app" CssClass="buttonAttach" Style="border-radius:2px;"  CommandArgument='<%# Bind("HR_APP_NO") %>' ><i class="glyphicon glyphicon-print" style="padding-right:4px;color:blue;font-size:14px;"></i>Report</asp:LinkButton>
                                    </ItemTemplate>
                                     
                                   </asp:TemplateField>--%>
                                    <%--href="#myModal" data-toggle="modal"--%>
                <%--         <asp:TemplateField HeaderText="Attachment"  ItemStyle-HorizontalAlign="Center">  
                        <ItemTemplate>  
                            <asp:LinkButton ID="lnkdetail"  CommandName="attach"  CssClass="buttonAttach" Style="border-radius:2px;"  CommandArgument='<%# Bind("HR_APP_NO") %>' runat="server" CausesValidation="false" ><i class="glyphicon glyphicon-paperclip" style="padding-right:4px;color:blue;font-size:14px"></i>Attachment</asp:LinkButton>  
                        </ItemTemplate>  
                                     
                    </asp:TemplateField> --%> 
                       <asp:TemplateField HeaderText="Comments"  ItemStyle-HorizontalAlign="Center">  
                        <ItemTemplate>  
                            <asp:LinkButton ID="lnkdetailcom"  CommandName="comments"  CssClass="buttonAttach"  CommandArgument='<%# Bind("INDT_NO") %>' runat="server" CausesValidation="false" ><i class="glyphicon glyphicon-comment" style="padding-right:4px;color:blue;font-size:14px"></i>Comments</asp:LinkButton>  
                              <asp:HiddenField ID="hdnIndtNo" runat="server" Value='<%# Bind("INDT_NO") %>' />
                        </ItemTemplate>  
                          
                    </asp:TemplateField>  
                       <asp:TemplateField >
                            <ItemTemplate>
                           <asp:Label ID="lblComments" style=" padding:4px 4px; margin:0px 0px 0px -16px;  border-radius:50px; font-weight:bold; float:left;  color:red;background-color:#0094ff;" Text="0" runat="server"></asp:Label>
                         </ItemTemplate>  

                      </asp:TemplateField>
                                <%--     <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                    <asp:LinkButton ID="btnReject" runat="server"  CssClass="buttonAttach" CommandName="reject"  CommandArgument='<%# Bind("HR_APP_NO") %>' ><i class="" style="padding-right:4px;color:red;font-size:14px;"></i>Reason</asp:LinkButton>
                                    </ItemTemplate>
                                         
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


          <div id="myModalCom" class="modal fade" >  
            <div class="modal-dialog" style="max-width:600px; align-content:space-around;">  
                <div class="modal-content">  
                    <div class="modal-header">  

                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true" >×</button>  
                        <h4 class="modal-title">Comments detail page</h4>  
                    </div>  
                    <div class="modal-body"  style="overflow-y: scroll; max-height: 85%; margin-top: 10px; margin-bottom: 10px;"  >  
                        <asp:Label ID="lblHRAppNo" runat="server" ClientIDMode="Static"></asp:Label>  
                        <asp:Label ID="lblMemoNocomm" runat="server" Visible="false" ></asp:Label>
                        <asp:HiddenField ID="hdnCommentsID" runat="server" />
                        <table> 
                             <tr>
                                <td align="right">
                                    <asp:Label ID="lblIndt_No" runat="server" Text="Indent No:" Font-Bold="true" ></asp:Label>
                                </td>
                                <td>
                                     <asp:Label ID="lblIndtNotxt" runat="server" Visible="true" Font-Bold="true"  ></asp:Label>
                                </td>
                            </tr>
                            <tr style="height:10px;">
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblcomments" runat="server" Text="Comment :" Font-Bold="true"  ></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtComments" runat="server" CssClass="textBox" TextMode="MultiLine" Height="35px" Width="250px"  Enabled="true"></asp:TextBox>
                                    
                                </td>
                                <td>&nbsp;&nbsp;</td>
                                <td> <asp:Button ID="btnSave" runat="server" Text="Add" CssClass="buttonNew" OnClick="btnSave_Click" Visible="true"   /></td>
                            </tr>
                              <tr style="height:10px;">
                                <td></td>
                                <td></td>
                            </tr>

                        </table>
                    <asp:Label runat="server" ID="lblDetailsView" Text="Comment Details :" Font-Bold="true" ForeColor="Black"></asp:Label>

           <asp:GridView ID="grdCommentDetails" runat="server" HeaderStyle-BackColor="#3AC0F2" HeaderStyle-ForeColor="Black"
            RowStyle-BackColor="White" AlternatingRowStyle-BackColor="#A1DCF2" AlternatingRowStyle-ForeColor="#000" 
              BorderStyle="None" BorderWidth="5px" CellPadding="10"  GridLines="Vertical" CssClass="gridView"
            AutoGenerateColumns="false">
            <Columns>
              
                  <asp:BoundField DataField="COMMENT_BY" HeaderText="Comment By" ItemStyle-Width="150px"  />
                  <asp:BoundField DataField="COMMENT_DESC" HeaderText="Comment" ItemStyle-Width="250px" />
                  <asp:BoundField DataField="COMMENT_DATE" HeaderText="Time" ItemStyle-Width="180px" />
            
          
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



                        <%--Status Details strat--%>
             <div id="myModalStatus" class="modal fade" >  
            <div class="modal-dialog" style="max-width:1080px; align-content:space-around;"> <%-- --%>
                <div class="modal-content" style="max-width:1080px;">  
                    <div class="modal-header" style="max-width:1080px;">  
                        
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>  
                        <h4 class="modal-title">Status details page</h4>  
                    </div>  
                    <div class="modal-body" style="overflow-y:scroll; overflow-x:scroll; max-height: 95%; max-width:1080px; margin-top: 10px; margin-bottom: 10px;"> <%-- --%>
                        <asp:Label ID="Label8" runat="server" ClientIDMode="Static"></asp:Label>  
                         <asp:Label ID="lblIndentNo" runat="server" Text="Indent No : " ></asp:Label>
                        <asp:Label ID="lblReqDetailsAppno" runat="server" Visible="true"  Font-Bold="true" ></asp:Label>
                        <asp:HiddenField ID="hdnStatusDetailsIndtID" runat="server" />
                       <asp:GridView ID="grdStatusDetails" runat="server" HeaderStyle-BackColor="#3AC0F2" HeaderStyle-ForeColor="Black"
            RowStyle-BackColor="White" AlternatingRowStyle-BackColor="#A1DCF2" AlternatingRowStyle-ForeColor="#000"
            BorderStyle="None" BorderWidth="5px" CellPadding="10"  GridLines="Vertical" CssClass="gridView"
            AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField DataField="STATUS_NAME" HeaderText="Action" ItemStyle-Width="180px" />
                   <asp:BoundField DataField="TRANS_NO" HeaderText="Trans. No " ItemStyle-Width="150px" />
                  <asp:BoundField DataField="TRANS_TIME" HeaderText="Trans. Time" ItemStyle-Width="180px" />
                  <asp:BoundField DataField="FULLNAME" HeaderText="Action By" ItemStyle-Width="140px" />
                  <asp:BoundField DataField="ITEM_QTY" HeaderText="Item's" ItemStyle-Width="300px" Visible="false" />
                <asp:BoundField DataField="DURATION_TIME" HeaderText="Duration" ItemStyle-Width="120px" />
          
            </Columns>
        </asp:GridView>
                      
                    </div>  
                    <div >
                         <table  >
                           
                           <tr align="right">
                               <td style="width:80%"></td>
                               <td align="right"><asp:Label ID="lblTotalDuration" runat="server" Text="Total Duration:" Font-Bold="true"></asp:Label></td>
                               <td align="right"><asp:Label ID="lblTotalDurationText" runat="server" Font-Bold="true" ForeColor="Black" BackColor="#A1DCF2"></asp:Label></td>
                           </tr>
                       </table>
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
                                                    <td>
                        <asp:Label ID="Label6" runat="server" Text="Report View"></asp:Label>
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
                        <asp:Label ID="Label7" runat="server" Text="Type:"></asp:Label>
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
