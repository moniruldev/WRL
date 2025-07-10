<%@ Page Title="" Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="LC_ENTRY.aspx.cs" Inherits="PG.Web.Inventory.LC_ENTRY" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="../javascript/jquery.ui.combogrid.js" type="text/javascript"></script>
    <script src="../javascript/jquery.attributeobserver.js" type="text/javascript"></script>
    <link href="../css/jquery.ui.combogrid.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">
        // <!CDATA[

        var isPageResize = true;
        ContentForm.CalendarImageURL = "../image/calendar.png";

        var hdnCompanyID = '<%=hdnCompanyID.ClientID%>';

        var ReportViewPageLink = '<%=this.ReportViewPageLink%>';
        var ReportViewPDFPageLink = '<%=this.ReportViewPDFPageLink%>';
        var ReportPrintPageLink = '<%=this.ReportPrintPageLink%>';
        var ReportPDFPageLink = '<%=this.ReportPDFPageLink%>';

        var SupplierListServiceLink = '<%=this.SupplierListServiceLink%>';
        var CnfListServiceLink = '<%=this.CnfListServiceLink%>';
        var InsucomListServiceLink = '<%=this.InsucomListServiceLink%>';
        var ManufactListServiceLink = '<%=this.ManufactListServiceLink%>';
        
      <%-- var CompanyListServiceLink = '<%=this.CompanyListServiceLink%>';--%>

        var ItemGroupListServiceLink = '<%=this.ItemGroupListServiceLink%>';

        var ItemListServiceLink = '<%=this.ItemListServiceLink%>';




        var txtSupplierName = '<%=txtSupplierName.ClientID%>';
        var btnSupplierID = '<%=btnSupplierID.ClientID%>';
        var hdnSupplierID = '<%=hdnSupplierID.ClientID%>';

        var txtCNF = '<%=txtCNF.ClientID%>';
        var btnCNFID = '<%=btnCNFID.ClientID%>';
        var hdnCNFID = '<%=hdnCNFID.ClientID%>';
        <%-- var btnCompanyID = '<%=btnCompanyID.ClientID%>';--%>
        <%--var txtCompanyID = '<%=txtCompanyID.ClientID%>';--%>
       <%-- var hdnCompanyIDtxt = '<%=hdnCompanyIDtxt.ClientID%>';--%>

        var gridUpdatePanelIDDet = '<%=UpdatePanel1.ClientID%>';
        var gridViewIDDet = '<%=GridView1.ClientID%>';
        var updateProgressID = '<%=UpdateProgress2.ClientID%>';


        var txtInsucom = '<%=txtInsucom.ClientID%>'
        var btnInsComID = '<%=btnInsComID.ClientID%>';
        var hdnInsComID = '<%=hdnInsComID.ClientID%>';
            <%--  var btnReferenceBy = '<%=btnReferenceBy.ClientID%>';
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
                        bindManufactList(gridViewIDDet);
                    }

                }
                //$('#' + dvGridDetailsPopup).parent().appendTo(jQuery("form:first"));
                //gridTaskAfter();

            });


            // alert('OK 1');

            //if ($('#' + txtCompanyName).is(':visible')) {

            //    bindCompanyList();
            //}
            bindManufactList(gridViewIDDet);

            bindItemGroupList(gridViewIDDet);


            bindItemList(gridViewIDDet);


            if ($('#' + txtInsucom).is(':visible')) {
                //alert('OK 1');
                bindInsCompList();
                //alert('OK 2');
            }


            if ($('#' + txtCNF).is(':visible')) {

                bindCNFList();

            }

            if ($('#' + txtSupplierName).is(':visible')) {

                bindSupplierList();

            }





        });

        function bindSupplierList() {

            var cgColumns = [
                              { 'columnName': 'supname', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                             , { 'columnName': 'supcode', 'width': '80', 'align': 'left', 'highlight': 2, 'label': 'Code' }
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
            serviceURL += "&ispaging=1&suptype=F";
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


        function bindCNFList() {
            var cgColumns = [{ 'columnName': 'cnfcode', 'width': '80', 'align': 'left', 'highlight': 2, 'label': 'Code' }
                             , { 'columnName': 'cnfname', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                             , { 'columnName': 'cnfaddress', 'width': '200', 'align': 'left', 'highlight': 4, 'label': 'Address' }
                             , { 'columnName': 'cnfphone', 'width': '80', 'align': 'left', 'highlight': 0, 'label': 'phone' }



            ];


            //var companyid = $('#' + hdnCompanyID).val();
            //var depthead = $('#' + hdnEmpCode).val();
            //var locationid = $('#' + ddlLocation).val();
            // var seid = $('#' + txtExecutiveID).val();

            //var serviceURL = GLAccountServiceLink + "?isterm=1&includeempty=1&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            var serviceURL = CnfListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            //serviceURL += "&companyid=" + companyid;
            serviceURL += "&ispaging=1";
            // serviceURL += "&locationid=" + locationid;
            //serviceURL += "&seid=" + seid;
            // serviceURL += "&empstatus=" + "A";
            //serviceURL += "&acctypefilter=" + Enums.GLAccountTypeFilter.AllAccount;
            // serviceURL += "&namecomptype=" + Enums.DataCompareType.Contains;



            var cnfIDElem = $('#' + txtCNF);

            $('#' + btnCNFID).click(function (e) {
                //elmID = $(elem).attr('id');
                //$(elem).combogrid("show");
                $(cnfIDElem).combogrid("dropdownClick");
            });


            $(cnfIDElem).combogrid({
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
                        $('#' + hdnCNFID).val(ui.item.cnfid);
                        $('#' + txtCNF).val(ui.item.cnfname);


                    }
                    return false;
                },

                lc: ''
            });


            $(cnfIDElem).blur(function () {
                var self = this;

                var cnfID = $(cnfIDElem).val();
                if (cnfID == '') {
                    // $('#' + hdnDealerID).val('0');
                    $('#' + txtCNF).val('');

                }
            });
        }


        function bindInsCompList() {
            var cgColumns = [{ 'columnName': 'inscomcode', 'width': '80', 'align': 'left', 'highlight': 2, 'label': 'Code' }
                             , { 'columnName': 'inscomname', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                             , { 'columnName': 'inscomaddress', 'width': '200', 'align': 'left', 'highlight': 4, 'label': 'Address' }
                             , { 'columnName': 'inscomphone', 'width': '80', 'align': 'left', 'highlight': 0, 'label': 'phone' }



            ];


            //var companyid = $('#' + hdnCompanyID).val();
            //var depthead = $('#' + hdnEmpCode).val();
            //var locationid = $('#' + ddlLocation).val();
            // var seid = $('#' + txtExecutiveID).val();

            //var serviceURL = GLAccountServiceLink + "?isterm=1&includeempty=1&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            var serviceURL = InsucomListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            //serviceURL += "&companyid=" + companyid;
            serviceURL += "&ispaging=1";
            // serviceURL += "&locationid=" + locationid;
            //serviceURL += "&seid=" + seid;
            // serviceURL += "&empstatus=" + "A";
            //serviceURL += "&acctypefilter=" + Enums.GLAccountTypeFilter.AllAccount;
            // serviceURL += "&namecomptype=" + Enums.DataCompareType.Contains;



            var inscomIDElem = $('#' + txtInsucom);

            $('#' + btnInsComID).click(function (e) {
                //elmID = $(elem).attr('id');
                //$(elem).combogrid("show");
                $(inscomIDElem).combogrid("dropdownClick");
            });


            $(inscomIDElem).combogrid({
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
                        $('#' + hdnInsComID).val(ui.item.inscomid);
                        $('#' + txtInsucom).val(ui.item.inscomname);


                    }
                    return false;
                },

                lc: ''
            });


            $(inscomIDElem).blur(function () {
                var self = this;

                var inscomID = $(inscomIDElem).val();
                if (inscomID == '') {
                    // $('#' + hdnDealerID).val('0');
                    $('#' + txtInsucom).val('');

                }
            });
        }

        function bindCompanyList() {
            var cgColumns = [{ 'columnName': 'compid', 'width': '80', 'align': 'left', 'highlight': 2, 'label': 'Comp ID' }
                             , { 'columnName': 'compcode', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Code' }
                             , { 'columnName': 'compname', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                             , { 'columnName': 'compaddress', 'width': '180', 'align': 'left', 'highlight': 4, 'label': 'Address' }

            ];

            //var companyid = $('#' + hdnCompanyID).val();
            //var depthead = $('#' + hdnEmpCode).val();
            //var locationid = $('#' + ddlLocation).val();

            //var serviceURL = GLAccountServiceLink + "?isterm=1&includeempty=1&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            var serviceURL = CompanyListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            //serviceURL += "&companyid=" + companyid;
            serviceURL += "&ispaging=1";
            //serviceURL += "&locationid=" + locationid;
            // serviceURL += "&empstatus=" + "A";
            //serviceURL += "&acctypefilter=" + Enums.GLAccountTypeFilter.AllAccount;
            // serviceURL += "&namecomptype=" + Enums.DataCompareType.Contains;



            var compNameElem = $('#' + txtCompanyName);

            $('#' + btnCompanyID).click(function (e) {
                //elmID = $(elem).attr('id');
                //$(elem).combogrid("show");
                $(compNameElem).combogrid("dropdownClick");
            });


            $(compNameElem).combogrid({
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
                    //var newServiceURL = serviceURL + "&companycode=" + companyCode + "&branchcode=" + branchCode + "&deptcode=" + deptCode
                    var newServiceURL = serviceURL;
                    $(this).combogrid("option", "url", newServiceURL);


                },
                select: function (event, ui) {
                    if (!ui.item) {
                        event.preventDefault();

                        //$('#' + hdnExecutiveID).val('0');
                        $('#' + hdnCompanyIDtxt).val('');
                        return false;
                        //ClearGLAccountData(elemID);
                    }


                    if (ui.item.seid == '') {
                        event.preventDefault();
                        return false;
                        //ClearGLAccountData(elemID);
                    }
                    else {
                        // $('#' + hdnExecutiveID).val(ui.item.seid);
                        $('#' + hdnCompanyIDtxt).val(ui.item.compid);
                        $('#' + txtCompanyName).val(ui.item.compname);
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


        function bindItemGroupList(gridViewID) {
            var cgColumns = [{ 'columnName': 'itemgroupdesc', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                             , { 'columnName': 'itemgroupcode', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Code' }



            ];

            //var companyid = $('#' + hdnCompanyID).val();
            //var depthead = $('#' + hdnEmpCode).val();
            //var locationid = $('#' + ddlLocation).val();

            //var serviceURL = GLAccountServiceLink + "?isterm=1&includeempty=1&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            var serviceURL = ItemGroupListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            //serviceURL += "&companyid=" + companyid;
            serviceURL += "&ispaging=1";
            //serviceURL += "&locationid=" + locationid;
            // serviceURL += "&empstatus=" + "A";
            //serviceURL += "&acctypefilter=" + Enums.GLAccountTypeFilter.AllAccount;
            // serviceURL += "&namecomptype=" + Enums.DataCompareType.Contains;

            //alert(gridViewID);
            var gridSelector = "#" + gridViewID;


            $(gridSelector).find('input[id$="txtGroupName"]').each(function (index, elem) {
                ///list click

                var elemRow = $(elem).closest('tr.gridRow');

                //var hdnJournalDetID_LinkElem = $(elemRow).find('input[id$="hdnJournalDetID_Link"]');

                //var hdnDrCrElem = $(elemRow).find('select[id$="hdnDrCr"]');

                //var hdnItemGroupIDElem = $(elemRow).find('input[id$="txtGroupName"]');



                //alert('OK 3');
                //var prevGLCode = '';

                $(elem).closest('tr').find('input[id$="btnItemGroup"]').click(function (e) {

                    //alert('OK 4');

                    elmID = $(elem).attr('id');
                    //$(elem).combogrid("show");
                    $(elem).combogrid("dropdownClick");
                });

                //alert('OK 5');

                //var compNameElem = $('#' + txtCompanyName);

                //$('#' + btnCompanyID).click(function (e) {
                //    //elmID = $(elem).attr('id');
                //    //$(elem).combogrid("show");
                //    $(compNameElem).combogrid("dropdownClick");
                //});


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
                    width: 600,
                    url: serviceURL,
                    search: function (event, ui) {
                        //var companyCode = $('#' + ddlCompany).val();
                        //var branchCode = $('#' + hdnBranch).val();
                        //var deptCode = $('#' + hdnDepartment).val();
                        //var locationid = $('#' + lblLocationID).val();
                        //var newServiceURL = serviceURL + "&companycode=" + companyCode + "&branchcode=" + branchCode + "&deptcode=" + deptCode
                        var newServiceURL = JSUtility.AddTimeToQueryString(serviceURL);
                        $(this).combogrid("option", "url", newServiceURL);


                    },

                    select: function (event, ui) {
                        //alert(ui.item.typename);
                        //$(".txtComboGrid").val(ui.item.code);
                        elemID = $(elem).attr('id');
                        //                    if (!validateGLAccount(elemID, ui.item)) {
                        //                        $(elem).val(prevGLCode);
                        //                        return false;
                        //                    }
                        if (!ui.item) {
                            event.preventDefault();
                            //ClearItemGroupData(elemID);
                            //ClearItemData()
                            return false;
                            //ClearGLAccountData(elemID);
                        }



                        if (ui.item.id == 0) {
                            event.preventDefault();
                            return false;
                            //ClearGLAccountData(elemID);
                        }
                        else {
                            SetItemGroupData(elemID, ui.item);
                        }
                        // setDetInstrument(hdnIsInstrumentElem, txtInstrumentElem, btnInstrumentElem);
                        return false;
                    }
                    //select: function (event, ui) {
                    //    if (!ui.item) {
                    //        event.preventDefault();

                    //        //$('#' + hdnExecutiveID).val('0');
                    //        $('#' + txtGroupName).val('');
                    //        return false;
                    //        //ClearGLAccountData(elemID);
                    //    }


                    //    if (ui.item.itemgroupid == '') {
                    //        event.preventDefault();
                    //        return false;
                    //        //ClearGLAccountData(elemID);
                    //    }
                    //    else {
                    //        // $('#' + hdnExecutiveID).val(ui.item.seid);
                    //        $('#' + hdngroupId).val(ui.item.itemgroupid);
                    //        $('#' + txtGroupName).val(ui.item.itemgroupdesc);
                    //    }
                    //    return false;
                    //},

                    // lc: ''
                });


                $(elem).blur(function () {
                    var self = this;
                    elemID = $(elem).attr('id');
                    eCode = $(elem).val();
                    isComboGridOpen = $(self).combogrid('isOpened');
                    if (eCode == '') {
                        //                    if (!validateGLAccount(elemID, null)) {
                        //                        $(elem).val(prevGLCode);
                        //                        return false;
                        //                    }
                        ClearItemGroupData(elemID);
                    }
                    else {
                        //grp = GetGLGroup(eCode);
                        ////                    if (!validateGLAccount(elemID, grp)) {
                        ////                        $(elem).val(prevGLCode);
                        ////                        return false;
                        ////                    }

                        if (grp == null) {
                            ClearItemGroupData(elemID);
                        }
                        else {
                            SetItemGroupData(elemID, grp);
                        }
                    }
                    //setDetInstrument(hdnIsInstrumentElem, txtInstrumentElem, btnInstrumentElem);
                    grpID = $(self).closest('tr').find('input[id$="hdngroupId"]').val();
                    if (grpID == '0' | grpID == '') {
                        $(self).addClass('textError');
                    }

                });

                $(elem).blur(function () {
                    var self = this;

                    var seID = $(elem).val();
                    if (seID == '') {
                        $('#' + hdngroupId).val('0');
                        //  $('#' + txtExecutiveName).val('');
                    }
                });
            });
        }


        function ClearItemGroupData(txtItemGroupID) {
            //$('#' + txtGLAccCodeID).val('');
            var detRow = $('#' + txtItemGroupID).closest('tr.gridRow');
            $(detRow).find('input[id$="hdngroupId"]').val('0');
            //$(detRow).find('input[id$="txtGLGroupName"]').val('');

        }

        function SetItemGroupData(txtItemGroupCodeID, data) {
            $('#' + txtItemGroupCodeID).val(data.itemgroupid);

            var detRow = $('#' + txtItemGroupCodeID).closest('tr.gridRow');

            $(detRow).find('input[id$="hdngroupId"]').val(data.itemgroupid);
            $(detRow).find('input[id$="txtGroupName"]').val(data.itemgroupdesc);

        }


        function bindItemList(gridViewID) {
            var cgColumns = [{ 'columnName': 'itemname', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                             , { 'columnName': 'uomname', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Uom Name' }
                             , { 'columnName': 'itemcode', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Code' }
                              , { 'columnName': 'closing_qty', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Closing' }
                             , { 'columnName': 'itemgroupdesc', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Group Name' }



            ];


            //var companyid = $('#' + hdnCompanyID).val();
            //var depthead = $('#' + hdnEmpCode).val();
            //var locationid = $('#' + ddlLocation).val();

            //var serviceURL = GLAccountServiceLink + "?isterm=1&includeempty=1&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            var serviceURL = ItemListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            //serviceURL += "&companyid=" + companyid;
            serviceURL += "&ispaging=1";
            //serviceURL += "&locationid=" + locationid;
            // serviceURL += "&empstatus=" + "A";
            //serviceURL += "&acctypefilter=" + Enums.GLAccountTypeFilter.AllAccount;
            // serviceURL += "&namecomptype=" + Enums.DataCompareType.Contains;


            var gridSelector = "#" + gridViewID;


            $(gridSelector).find('input[id$="txtITEM_NAME"]').each(function (index, elem) {
                ///list click

                var elemRow = $(elem).closest('tr.gridRow');

                var hdnItemIDElem = $(elemRow).find('input[id$="txtITEM_NAME"]');

                //var prevGLCode = '';

                $(elem).closest('tr').find('input[id$="btnITEM_NAME"]').click(function (e) {
                    elmID = $(elem).attr('id');
                    //$(elem).combogrid("show");
                    $(elem).combogrid("dropdownClick");
                });



                //var compNameElem = $('#' + txtCompanyName);

                //$('#' + btnCompanyID).click(function (e) {
                //    //elmID = $(elem).attr('id');
                //    //$(elem).combogrid("show");
                //    $(compNameElem).combogrid("dropdownClick");
                //});


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
                    width: 750,
                    url: serviceURL,
                    search: function (event, ui) {
                        var elemRowCur = $(elem).closest('tr.gridRow');
                        var vgroupid = $(elemRowCur).find('input[id$="hdngroupId"]').val();
                        //var vgroupid = $('#' + hdngroupId).val();
                        var newServiceURL = serviceURL + "&groupid=" + vgroupid
                        var ddlItemType = $(elemRowCur).find('[id*=ddlItemType] option:selected').val();
                        newServiceURL = newServiceURL + "&itemtypeid=" + ddlItemType;
                        //var newServiceURL = serviceURL + "&companycode=" + companyCode + "&branchcode=" + branchCode + "&deptcode=" + deptCode
                        newServiceURL = JSUtility.AddTimeToQueryString(newServiceURL);
                        $(this).combogrid("option", "url", newServiceURL);


                    },

                    select: function (event, ui) {
                        //alert(ui.item.typename);
                        //$(".txtComboGrid").val(ui.item.code);
                        elemID = $(elem).attr('id');
                        //                    if (!validateGLAccount(elemID, ui.item)) {
                        //                        $(elem).val(prevGLCode);
                        //                        return false;
                        //                    }
                        if (!ui.item) {
                            event.preventDefault();
                            ClearItemData(elemID);
                            return false;
                            //ClearGLAccountData(elemID);
                        }



                        if (ui.item.id == 0) {
                            event.preventDefault();
                            return false;
                            //ClearGLAccountData(elemID);
                        }
                        else {
                            SetItemData(elemID, ui.item);
                        }
                        // setDetInstrument(hdnIsInstrumentElem, txtInstrumentElem, btnInstrumentElem);
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
                        //                    if (!validateGLAccount(elemID, null)) {
                        //                        $(elem).val(prevGLCode);
                        //                        return false;
                        //                    }
                        //ClearItemGroupData(elemID);
                    }
                    else {
                        //grp = GetGLGroup(eCode);
                        ////                    if (!validateGLAccount(elemID, grp)) {
                        ////                        $(elem).val(prevGLCode);
                        ////                        return false;
                        ////                    }

                        if (grp == null) {
                            ClearItemData(elemID);
                        }
                        else {
                            SetItemData(elemID, grp);
                        }
                    }
                    //setDetInstrument(hdnIsInstrumentElem, txtInstrumentElem, btnInstrumentElem);
                    grpID = $(self).closest('tr').find('input[id$="hdngroupId"]').val();
                    if (grpID == '0' | grpID == '') {
                        $(self).addClass('textError');
                    }

                });

            });

        }

        function ClearItemData(txtItemID) {
            //$('#' + txtGLAccCodeID).val('');
            var detRow = $('#' + txtItemID).closest('tr.gridRow');
            $(detRow).find('input[id$="hdnItemId"]').val('0');
            $(detRow).find('input[id$="txtITEM_NAME"]').val('');

        }
        function SetItemData(txtItemCodeID, data) {
            $('#' + txtItemCodeID).val(data.itemid);

            var detRow = $('#' + txtItemCodeID).closest('tr.gridRow');
            $(detRow).find('input[id$="hdnItemID"]').val(data.itemid);
            $(detRow).find('input[id$="txtITEM_NAME"]').val(data.itemname);
            $(detRow).find('input[id$="hdngroupId"]').val(data.itemgroupid);
            $(detRow).find('input[id$="txtGroupName"]').val(data.itemgroupdesc);
           // $(detRow).find('input[id$="hdnUomID"]').val(data.uomid);
           // $(detRow).find('input[id$="txtUOM_NAME"]').val(data.uomname);
            $(detRow).find('input[id$="txtWeighAvgPrice"]').val(data.weightedAvgPrice);
            //$(detRow).find('input[id$="txtUnitRate"]').val(data.weightedAvgPrice);

        }


        function bindManufactList(gridViewID) {
            var cgColumns = [
                               { 'columnName': 'manuname', 'width': '80', 'align': 'left', 'highlight': 2, 'label': 'Name' }
                             , { 'columnName': 'manucode', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Code' }
                             , { 'columnName': 'manuaddress', 'width': '200', 'align': 'left', 'highlight': 4, 'label': 'Address' }
                             , { 'columnName': 'manuphone', 'width': '80', 'align': 'left', 'highlight': 0, 'label': 'phone' }
                             , { 'columnName': 'cuntname', 'width': '80', 'align': 'left', 'highlight': 0, 'label': 'Country' }



            ];


            //var companyid = $('#' + hdnCompanyID).val();
            //var depthead = $('#' + hdnEmpCode).val();
            //var locationid = $('#' + ddlLocation).val();
            // var seid = $('#' + txtExecutiveID).val();

            //var serviceURL = GLAccountServiceLink + "?isterm=1&includeempty=1&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            var serviceURL = ManufactListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            //serviceURL += "&companyid=" + companyid;
            serviceURL += "&ispaging=1";
            // serviceURL += "&locationid=" + locationid;
            //serviceURL += "&seid=" + seid;
            // serviceURL += "&empstatus=" + "A";
            //serviceURL += "&acctypefilter=" + Enums.GLAccountTypeFilter.AllAccount;
            // serviceURL += "&namecomptype=" + Enums.DataCompareType.Contains;
            var gridSelector = "#" + gridViewID;


            $(gridSelector).find('input[id$="txtManufact_Name"]').each(function (index, elem) {
                ///list click

                var elemRow = $(elem).closest('tr.gridRow');

                //var hdnJournalDetID_LinkElem = $(elemRow).find('input[id$="hdnJournalDetID_Link"]');

                //var hdnDrCrElem = $(elemRow).find('select[id$="hdnDrCr"]');

                //var hdnItemGroupIDElem = $(elemRow).find('input[id$="txtGroupName"]');



                //alert('OK 3');
                //var prevGLCode = '';

                $(elem).closest('tr').find('input[id$="btnmanufacid"]').click(function (e) {

                    //alert('OK 4');

                    elmID = $(elem).attr('id');
                    //$(elem).combogrid("show");
                    $(elem).combogrid("dropdownClick");
                });

                //alert('OK 5');

                //var compNameElem = $('#' + txtCompanyName);

                //$('#' + btnCompanyID).click(function (e) {
                //    //elmID = $(elem).attr('id');
                //    //$(elem).combogrid("show");
                //    $(compNameElem).combogrid("dropdownClick");
                //});


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
                    width: 600,
                    url: serviceURL,
                    search: function (event, ui) {
                        //var companyCode = $('#' + ddlCompany).val();
                        //var branchCode = $('#' + hdnBranch).val();
                        //var deptCode = $('#' + hdnDepartment).val();
                        //var locationid = $('#' + lblLocationID).val();
                        //var newServiceURL = serviceURL + "&companycode=" + companyCode + "&branchcode=" + branchCode + "&deptcode=" + deptCode
                        var newServiceURL = JSUtility.AddTimeToQueryString(serviceURL);
                        $(this).combogrid("option", "url", newServiceURL);


                    },

                    select: function (event, ui) {
                        //alert(ui.item.typename);
                        //$(".txtComboGrid").val(ui.item.code);
                        elemID = $(elem).attr('id');
                        //                    if (!validateGLAccount(elemID, ui.item)) {
                        //                        $(elem).val(prevGLCode);
                        //                        return false;
                        //                    }
                        if (!ui.item) {
                            event.preventDefault();
                            ClearManufactData(elemID);
                            //ClearItemData()
                            return false;
                            //ClearGLAccountData(elemID);
                        }



                        if (ui.item.id == 0) {
                            event.preventDefault();
                            return false;
                            //ClearGLAccountData(elemID);
                        }
                        else {
                            SetManufactData(elemID, ui.item);
                        }
                        // setDetInstrument(hdnIsInstrumentElem, txtInstrumentElem, btnInstrumentElem);
                        return false;
                    }
                    //select: function (event, ui) {
                    //    if (!ui.item) {
                    //        event.preventDefault();

                    //        //$('#' + hdnExecutiveID).val('0');
                    //        $('#' + txtGroupName).val('');
                    //        return false;
                    //        //ClearGLAccountData(elemID);
                    //    }


                    //    if (ui.item.itemgroupid == '') {
                    //        event.preventDefault();
                    //        return false;
                    //        //ClearGLAccountData(elemID);
                    //    }
                    //    else {
                    //        // $('#' + hdnExecutiveID).val(ui.item.seid);
                    //        $('#' + hdngroupId).val(ui.item.itemgroupid);
                    //        $('#' + txtGroupName).val(ui.item.itemgroupdesc);
                    //    }
                    //    return false;
                    //},

                    // lc: ''
                });


                $(elem).blur(function () {
                    var self = this;
                    elemID = $(elem).attr('id');
                    eCode = $(elem).val();
                    isComboGridOpen = $(self).combogrid('isOpened');
                    if (eCode == '') {
                        //                    if (!validateGLAccount(elemID, null)) {
                        //                        $(elem).val(prevGLCode);
                        //                        return false;
                        //                    }
                        ClearManufactData(elemID);
                    }
                    else {
                        //grp = GetGLGroup(eCode);
                        ////                    if (!validateGLAccount(elemID, grp)) {
                        ////                        $(elem).val(prevGLCode);
                        ////                        return false;
                        ////                    }

                        if (grp == null) {
                            ClearManufactData(elemID);
                        }
                        else {
                            SetManufactData(elemID, grp);
                        }
                    }
                    //setDetInstrument(hdnIsInstrumentElem, txtInstrumentElem, btnInstrumentElem);
                    grpID = $(self).closest('tr').find('input[id$="hdnmanufacid"]').val();
                    if (grpID == '0' | grpID == '') {
                        $(self).addClass('textError');
                    }

                });

                $(elem).blur(function () {
                    var self = this;

                    var seID = $(elem).val();
                    if (seID == '') {
                        $('#' + hdnmanufacid).val('0');
                        //  $('#' + txtExecutiveName).val('');
                    }
                });
            });
        }


        function ClearManufactData(txtManufactID) {
            //$('#' + txtGLAccCodeID).val('');
            var detRow = $('#' + txtManufactID).closest('tr.gridRow');
            $(detRow).find('input[id$="hdnmanufacid"]').val('0');
            //$(detRow).find('input[id$="txtGLGroupName"]').val('');

        }

        function SetManufactData(txtManufactID, data) {

            $('#' + txtManufactID).val(data.itemgroupid);
            var detRow = $('#' + txtManufactID).closest('tr.gridRow');
            $(detRow).find('input[id$="hdnmanufacid"]').val(data.manuid);
            $(detRow).find('input[id$="txtManufact_Name"]').val(data.manuname);
            $(detRow).find('input[id$="txtCountryName"]').val(data.cuntname);
            $(detRow).find('input[id$="hdnCountryID"]').val(data.countid);

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

        .auto-style2 {
            height: 24px;
        }
    </style>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="dvPageContent" style="width: 100%; height: 100%;">
        <div id="dvContentHeader" class="dvContentHeader">
            <div id="dvHeader" class="dvHeader">
                <asp:Label ID="lblHeader" CssClass="lblHeader" runat="server" Text="LC Entry"></asp:Label>
            </div>
            <div id="dvMsg" runat="server" class="dvMessage" style="width: 100%; min-height: 20px; height: auto; text-align: center;">
                <asp:Label ID="lblMessage" runat="server" Font-Size="Small" Width="100%" Height="16px"></asp:Label>
            </div>
        </div>
        <div id="dvContentMain" class="dvContentMain">
            <div id="dvControlsHead" style="height: auto; width: auto; text-align: left; vertical-align: top;">
            </div>
            <div id="dvControls" style="height: auto; width: 100%">
                <div id="dvControlsInner" class="groupBoxContainer boxShadow" style="height: auto; width: 100%">
                    <div id="groupBox">
                        <div id="groupHeader" class="groupHeader">
                            <span>LC Entry</span>
                        </div>
                        <div id="groupContent" class="groupContent scrollBar">
                            <div id="groupContenInner">
                                <div id="groupDataMaster" style="width: 109%; height: auto;">
                                    <table style="" border="0" cellspacing="2" cellpadding="1">
                                    </table>

                                    <table style="width: 101%" border="0" cellspacing="2" cellpadding="1">
                                        <tr>
                                            <td style="width: 100%">
                                                <table style="" border="0" cellspacing="2" cellpadding="1">



                                                    <tr>
                                                        <td align="right" class="auto-style2">
                                                            <asp:Label ID="lblIMPPONo" runat="server" Text="PO No:"></asp:Label>
                                                        </td>

                                                        <td class="auto-style2">
                                                            <asp:TextBox ID="txtIMPPONo" runat="server" CssClass="textBox" Width="200px" Enabled="false"></asp:TextBox>
                                                        </td>

                                                        <td align="right" class="auto-style2">
                                                            <asp:Label ID="lblLCNO" runat="server" Text="LC No:"></asp:Label><span style="color: red">*</span>
                                                        </td>

                                                        <td class="auto-style2">

                                                            <asp:TextBox ID="txtLCNo" runat="server" CssClass="textBox"></asp:TextBox>
                                                        </td>

                                                        <td align="right" class="auto-style2">
                                                            <asp:Label ID="lblLCDate" runat="server" Text="LC Date:"></asp:Label><span style="color: red">*</span>

                                                        </td>
                                                        <td class="auto-style2">
                                                            <asp:TextBox ID="txtLCDate" runat="server" CssClass="textBox textDate dateParse" Style="text-align: left;" Width="100px"></asp:TextBox>

                                                        </td>

                                                     


                                                        <td align="right" class="auto-style2">
                                                            <asp:Label ID="lblLCTYPE" runat="server" Text="LC Type:"></asp:Label><span style="color: red">*</span>
                                                        </td>
                                                        <td align="right" class="auto-style2">
                                                            <asp:DropDownList ID="ddlLCTYPE" runat="server" Width="200px" CssClass="dropDownList"></asp:DropDownList>

                                                        </td>
                                                   

                                                    </tr>


                                                    <tr>

                                                        <td align="right">
                                                            <asp:Label ID="lblLCANO" runat="server" Text="LCA No:"></asp:Label>

                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtLCANO" runat="server" CssClass="textBox"></asp:TextBox>
                                                        </td>

                                     <%--                   <td align="right">
                                                            <asp:Label ID="lblLCADATE" runat="server" Text="LCA Date:"></asp:Label>

                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtLCADate" runat="server" CssClass="textBox textDate dateParse" Style="text-align: left;" Width="100px"></asp:TextBox>
                                                        </td>--%>


                                                        <td align="right" class="auto-style2">
                                                            <asp:Label ID="lblTransportType" runat="server" Text="Transport Type:"></asp:Label><span style="color: red">*</span>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlTransportType" runat="server" Width="200px" CssClass="dropDownList"></asp:DropDownList>

                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblPartialShipment" runat="server" Text="Partial Shipment:"></asp:Label>
                                                        </td>

                                                        <td align="left">
                                                            <asp:DropDownList ID="ddlPartialShipment" runat="server" Width="200px" CssClass="dropDownList">
                                                                <asp:ListItem Text="Allowed" Value="Y"></asp:ListItem>
                                                                <asp:ListItem Selected="True" Text="Not Allowed" Value="N"></asp:ListItem>
                                                            </asp:DropDownList>

                                                        </td>
                                                           <td align="right">
                                                            <asp:Label ID="lblBank" runat="server" Text="Bank:"></asp:Label><span style="color: red">*</span>
                                                        </td>

                                                        <td align="left">
                                                            <asp:DropDownList ID="ddlBank" runat="server" Width="180px" CssClass="dropDownList">
                                                                <asp:ListItem Selected="True" Text="--Select--" Value="0"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <%-- <asp:TextBox ID="txtBank" runat="server" CssClass="textBox"  Width="180px"></asp:TextBox>
                                                             <input id="btnBankID" type="button" value="" runat="server" class="buttonDropdown"
                                                                tabindex="-1" />--%>
                                                        </td>

                                                    </tr>

                                                    <tr>

                                                        <td align="right">
                                                            <asp:Label ID="lblCurrency" runat="server" Text="Currency:"></asp:Label><span style="color: red">*</span>

                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlCurrency" runat="server" Width="165px" CssClass="dropDownList" Visible="true"></asp:DropDownList>
                                                        </td>

                                        <%--                <td align="right">
                                                            <asp:Label ID="lblConversionRate" runat="server" Text="Conv. Rate:" Visible="false"></asp:Label>

                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtConvrate" runat="server" CssClass="textBox textNumberOnly" Style="text-align: left;" Width="100px" onkeypress="return onlyNos(event,this);"></asp:TextBox>
                                                        </td>--%>

                                                        <td align="right" class="auto-style2">
                                                            <asp:Label ID="lblSupplier" runat="server" Text="Supplier:"></asp:Label><span style="color: red">*</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtSupplierName" runat="server" CssClass="textBox" Width="180px"></asp:TextBox>
                                                            <input id="btnSupplierID" type="button" value="" runat="server" class="buttonDropdown"
                                                                tabindex="-1" />

                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblCNF" runat="server" Text="CNF Agent:"></asp:Label>
                                                        </td>

                                                        <td align="left">
                                                            <asp:TextBox ID="txtCNF" runat="server" CssClass="textBox" Width="180px"></asp:TextBox>
                                                            <input id="btnCNFID" type="button" value="" runat="server" class="buttonDropdown"
                                                                tabindex="-1" />
                                                        </td>
                                                            <td align="right">
                                                            <asp:Label ID="lblBranch" runat="server" Text="Bank Branch:"></asp:Label>

                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlBranch" runat="server" Width="180px" CssClass="dropDownList"></asp:DropDownList>
                                                            <%-- <asp:TextBox ID="txtBranch" runat="server" CssClass="textBox"  Width="155px"></asp:TextBox>
                                                             <input id="btnBranchID" type="button" value="" runat="server" class="buttonDropdown"
                                                                tabindex="-1" />--%>
                                                        </td>

                                                    </tr>

                                                    <tr>

                                                        <td align="right">
                                                            <asp:Label ID="lblInsurance" runat="server" Text="Insur. Com:"></asp:Label>

                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtInsucom" runat="server" CssClass="textBox" Width="155px"></asp:TextBox>
                                                            <input id="btnInsComID" type="button" value="" runat="server" class="buttonDropdown"
                                                                tabindex="-1" />
                                                        </td>

                                                        <td align="right">
                                                            <asp:Label ID="lblInsCoverNote" runat="server" Text="Insur.cover Note:"></asp:Label>

                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtInsCoverNote" runat="server" CssClass="textBox" Style="text-align: left;" Width="100px"></asp:TextBox>
                                                        </td>

                                                        <td align="right" class="auto-style2">
                                                            <asp:Label ID="lblInsCoverDate" runat="server" Text="Ins Cover Dt:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtInscovdate" runat="server" CssClass="textBox textDate dateParse" Width="180px"></asp:TextBox>

                                                        </td>

                                                        <td align="right" class="auto-style2">
                                                            <asp:Label ID="lblMOP" runat="server" Text="LC/TT:"></asp:Label><span style="color: red">*</span>
                                                        </td>

                                                        <td align="left" class="auto-style2">
                                                            <asp:DropDownList ID="ddlMOP" runat="server" Width="200px" CssClass="dropDownList"></asp:DropDownList>

                                                        </td>
                                                     

                                                    </tr>

                                                    <tr>

                                                    

                                                        <td align="right">
                                                            <asp:Label ID="Label3" runat="server" Text="Remarks:"></asp:Label>

                                                        </td>

                                                        <td align="left" colspan="4">
                                                            <asp:TextBox ID="txtLCREmarks" runat="server" CssClass="textBox" TextMode="MultiLine" Height="30px" Width="500px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:CheckBox runat="server" ID="chkIsMultiplePI" />
                                                            <asp:Label runat="server" ID="lblIsMultiplePI" Text="Is Multiple PI" Font-Bold="true"></asp:Label>
                                                        </td>


                                                    </tr>


                                                </table>

                                            </td>

                                        </tr>




                                    </table>

                                    <table style="display: none;" border="0" cellspacing="0" cellpadding="1">
                                        <tr>
                                            <td></td>
                                            <td style="" align="right" valign="top">&nbsp;
                                            </td>
                                            <td style="" align="left">
                                                <asp:HiddenField ID="hdnCompanyID" runat="server" Value="0" />
                                                <asp:HiddenField ID="hdnSupplierID" runat="server" Value="0" />
                                                <asp:HiddenField ID="hdnCNFID" runat="server" Value="0" />
                                                 <asp:HiddenField ID="hdnCountryID" runat="server" Value="0" />

                                            </td>
                                            <td>
                                                <asp:HiddenField ID="hdnJournalID" runat="server" Value="0" />
                                                <asp:HiddenField ID="hdnJournalUpdateNo" runat="server" Value="0" />
                                                <asp:HiddenField ID="hdnInsComID" runat="server" Value="0" />
                                            </td>
                                            <td>
                                                <asp:HiddenField ID="hdnGLGroupClassInclude" runat="server" Value="0" />
                                                <asp:HiddenField ID="hdnGLGroupClassExclude" runat="server" Value="0" />
                                                <asp:HiddenField ID="hdnBankID" runat="server" Value="0" />
                                            </td>
                                            <td>
                                                <asp:HiddenField ID="hdnAccSLNoID" runat="server" Value="0" />
                                                <asp:HiddenField ID="hdnEditDataModeInt" runat="server" Value="0" />
                                                <asp:HiddenField ID="hdnBranchID" runat="server" Value="0" />
                                            </td>
                                        </tr>

                                        <tr style="display: none; visibility: hidden;">
                                            <td></td>
                                            <td align="right">
                                                <asp:Label ID="Label18" runat="server" Text="Description:"></asp:Label>
                                            </td>
                                            <td align="left" colspan="3">
                                                <asp:TextBox ID="txtJournalDesc" runat="server" CssClass="textBox enableIsDirty"
                                                    Style="text-align: left;" Width="334px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlJournalAdjustType" runat="server" CssClass="dropDownList"
                                                    Width="50px" Visible="False">
                                                </asp:DropDownList>
                                                <asp:Label ID="lblPosted" runat="server" Text="-" Visible="False"></asp:Label>
                                            </td>
                                            <td align="right">&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div id="groupDataDetails" style="width: 100%; height: auto;">
                                    <div id="dvGridSeparator" runat="server" style="width: 100%;">
                                    </div>
                                    <div id="groupDataHeaderCredit" runat="server" class="" style="width: 100%; text-align: center;">
                                        <span style="font-weight: bold">LC Details: </span>
                                    </div>
                                    <div id="dvGridContainer2" runat="server" class="" style="width: auto; height: auto; text-align: left">
                                        <%-- <div id="dvGridHeader2" style="width: 100%; height: 25px; font-size: smaller;" class="subHeader">
                                            <table style="height: 80%; color: #FFFFFF; font-weight: bold; text-align: center;"
                                                class="defFont" cellspacing="1" cellpadding="1">
                                                <tr class="headerRow">
                                                    <td width="64px" class="headerColLeft">SL#
                                                    </td>
                                                    <td width="80px" class="headerColLeft">Item Group
                                                    </td>
                                                   <td width="10px" class="headerColLeft">
                                                    </td>
                                                    <td width="195px" class="headerColLeft">Item
                                                    </td>
                                                       <td width="10px" class="headerColLeft">
                                                    </td>
                                                    <td width="60px" class="headerColLeft">Item Qty
                                                    </td>
                                                    <td width="50px" class="headerColLeft">UOM
                                                    </td>
                                                    
                                                    <td width="60px" class="headerColRight">Item Rate
                                                    </td>

                                                    <td width="72px" class="headerColCenter">Total cost
                                                    </td>
                                                  
                                                       <td width="160px" class="headerColCenter">Manufacturer
                                                    </td>
                                                    <td width="10px">
                                                    </td>
                                                    <td width="160px" class="headerColCenter">Origin
                                                    </td>
                                                     <td width="100px" class="headerColCenter">HS_CODE1
                                                    </td>
                                                     <td width="100px" class="headerColCenter">HS_CODE2
                                                    </td>
                                                     <td width="100px" class="headerColCenter">HS_CODE3
                                                    </td>
                                                    <td width="160px" class="headerColCenter">Remarks
                                                    </td>

                                                </tr>
                                            </table>
                                        </div>--%>
                                        <div id="dvGrid" style="width: 100%; height: 200px; overflow: auto;" class="dvGrid">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" ShowHeader="true"
                                                        CellPadding="1" CellSpacing="1" ForeColor="#333333" GridLines="Both" OnRowDataBound="GridView1_RowDataBound" OnRowCreated="GridView1_RowCreated" OnRowCommand="GridView1_RowCommand" DataKeyNames="ITEM_ID"
                                                        EnableModelValidation="True" ClientIDMode="AutoID" OnRowDeleting="GridView1_RowDeleting">
                                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="SL#">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSLNo" runat="server" Text='<%# Bind("LC_DET_SLNO") %>' Style="text-align: center;"
                                                                        Width="62px">
                                                                    </asp:Label>
                                                                    <asp:HiddenField ID="hdnLCDetId" runat="server" Value='<%# Bind("LC_DET_ID") %>' />
                                                                </ItemTemplate>
                                                                <ItemStyle VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Group Name" Visible="true">
                                                                <ItemTemplate>
                                                                    <table>
                                                                        <tr>
                                                                            <td>



                                                                                <asp:TextBox ID="txtGroupName" runat="server" CssClass="textBox textAutoSelect" Width="78" Text='<%# Bind("ITEM_GROUP_DESC") %>'></asp:TextBox>
                                                                                <asp:HiddenField ID="hdngroupId" runat="server" Value='<%# Bind("ITEM_GROUP_ID") %>' />
                                                                            </td>
                                                                            <td>
                                                                                <input id="btnItemGroup" type="button" value="" runat="server" class="buttonDropdown" tabindex="-1" />
                                                                            </td>


                                                                        </tr>
                                                                    </table>
                                                                </ItemTemplate>
                                                                <ItemStyle VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Item Type" Visible="true">
                                                                <ItemTemplate>
                                                                      <asp:DropDownList ID="ddlItemType" runat="server" CssClass="dropDownList" Width="120" >
                                                                                           
                                                                     </asp:DropDownList>
                                                                    <asp:HiddenField ID="hdnItemType" runat="server" Value='<%# Bind("ITEM_TYPE_ID") %>' />
                                                                </ItemTemplate>
                                                                <ItemStyle VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="ITEM Name" Visible="true">
                                                                <ItemTemplate>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:TextBox ID="txtITEM_NAME" runat="server" CssClass="textBox textAutoSelect" Width="200" Text='<%# Bind("ITEM_NAME") %>'></asp:TextBox>
                                                                                <asp:HiddenField ID="hdnItemID" runat="server" Value='<%# Bind("ITEM_ID") %>' />
                                                                            </td>
                                                                            <td>
                                                                                <input id="btnITEM_NAME" type="button" value="" runat="server" class="buttonDropdown" tabindex="-1" />
                                                                            </td>


                                                                        </tr>
                                                                    </table>

                                                                </ItemTemplate>
                                                                <ItemStyle VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                           

                                                            <asp:TemplateField HeaderText="Item Qnty" Visible="true">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtItem_qty" runat="server" CssClass="textBox textAutoSelect" Width="58" Text='<%# Bind("ITEM_QNTY") %>' OnTextChanged="txtITEMRate_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <ItemStyle VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="UOM" Visible="true">
                                                                <ItemTemplate>
                                                                  <%--  <asp:TextBox ID="txtUOM_NAME" runat="server" CssClass="textBox textAutoSelect" Width="48" Text='<%# Bind("UOM_NAME") %>' Style=""></asp:TextBox>--%>
                                                                    <asp:HiddenField ID="hdnUomID" runat="server" Value='<%# Bind("UOM_ID") %>' />
                                                                    <asp:DropDownList ID="ddlUOM" runat="server" Width="48px" CssClass="dropDownList">

                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                                <ItemStyle VerticalAlign="Top" />
                                                            </asp:TemplateField>



                                                            <asp:TemplateField HeaderText="Item Rate" Visible="true">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtUnitRate" runat="server" CssClass="textBox textAutoSelect" Width="58" Text='<%# Bind("UNIT_PRICE") %>' Style="" Enabled="true" OnTextChanged="txtITEMRate_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <ItemStyle VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Item Total Cost" Visible="true">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtItemTotalCost" runat="server" CssClass="textBox textAutoSelect" Width="68" Text='<%# Bind("ITEM_TOTAL_COST") %>' Style=""></asp:TextBox>
                                                                </ItemTemplate>
                                                                <ItemStyle VerticalAlign="Top" />
                                                            </asp:TemplateField>



                                                            <asp:TemplateField HeaderText="Manufacture" Visible="false">
                                                                <ItemTemplate>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:TextBox ID="txtManufact_Name" runat="server" CssClass="textBox textAutoSelect" Width="155" Text='<%# Bind("SUP_NAME") %>'></asp:TextBox>
                                                                                <asp:HiddenField ID="hdnmanufacid" runat="server" Value='<%# Bind("MANUFAC_ID") %>' />
                                                                            </td>
                                                                            <td>
                                                                                <input id="btnmanufacid" type="button" value="" runat="server" class="buttonDropdown" tabindex="-1" />

                                                                            </td>


                                                                        </tr>
                                                                    </table>
                                                                </ItemTemplate>
                                                                <ItemStyle VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Country" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtCountryName" runat="server" CssClass="textBox textAutoSelect" Width="150" Text='<%# Bind("COUNTRY_NAME") %>' Style=""></asp:TextBox>
                                                                    <asp:HiddenField ID="hdnCountryID" runat="server" Value='<%# Bind("COUNTRY_ID") %>' />
                                                                </ItemTemplate>
                                                                <ItemStyle VerticalAlign="Top" />
                                                            </asp:TemplateField>




                                                            <asp:TemplateField HeaderText="HS Code1" Visible="true">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txthscode1" runat="server" CssClass="textBox textAutoSelect" MaxLength="10" Width="98" Text='<%# Bind("HS_CODE1") %>' Style=""></asp:TextBox>
                                                                </ItemTemplate>
                                                                <ItemStyle VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="HS Code2" Visible="true">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txthscode2" runat="server" CssClass="textBox textAutoSelect" MaxLength="10" Width="98" Text='<%# Bind("HS_CODE2") %>' Style=""></asp:TextBox>
                                                                </ItemTemplate>
                                                                <ItemStyle VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="HS Code3" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txthscode3" runat="server" CssClass="textBox textAutoSelect" MaxLength="10" Width="98" Text='<%# Bind("HS_CODE3") %>' Style=""></asp:TextBox>
                                                                </ItemTemplate>
                                                                <ItemStyle VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Remarks" Visible="true">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtITEM_DET_REMARKS" runat="server" CssClass="textBox textAutoSelect" Width="155" Text='<%# Bind("ITEM_DET_REMARKS") %>' Style=""></asp:TextBox>
                                                                </ItemTemplate>
                                                                <ItemStyle VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                               <asp:TemplateField HeaderText="Weigh. Avg. Price" Visible="true">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtWeighAvgPrice" runat="server"  CssClass="textBox textAutoSelect" Width="58" Text='<%# Bind("WEIGHTED_AVERAGE_PRICE") %>' Style="" Enabled="false"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <ItemStyle VerticalAlign="Top" />
                                                            </asp:TemplateField>


                                                            <%-- <asp:TemplateField HeaderText="Battery Type" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <div >
                                                                        <table border="0" cellpadding="1" cellspacing="1" width="100%">
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td>
                                                                                         <%--<asp:DropDownList ID="ddlItemGroup" runat="server" CssClass="dropDownList" Width="80"></asp:DropDownList>--%>
                                                            <%--  <asp:TextBox ID="txtGroupName" runat="server" CssClass="textBox textAutoSelect" Width="78" Text='<%# Bind("ITEM_GROUP_DESC") %>'></asp:TextBox>
                                                                                        <asp:HiddenField ID="hdngroupId" runat="server" Value='<%# Bind("ITEM_GROUP_ID") %>' />
                                                                                    </td>
                                                                                    <td>
                                                                                        <input id="btnItemGroup" type="button" value="" runat="server" class="buttonDropdown" tabindex="-1" />
                                                                                    </td>

                                                                                    <td>--%>
                                                            <%-- <asp:DropDownList ID="ddlItem" runat="server" CssClass="dropDownList" Width="195"></asp:DropDownList>--%>
                                                            <%--<asp:TextBox ID="txtITEM_NAME" runat="server" CssClass="textBox textAutoSelect" Width="188" Text='<%# Bind("ITEM_NAME") %>'></asp:TextBox>
                                                                                        <asp:HiddenField ID="hdnItemID" runat="server" Value='<%# Bind("ITEM_ID") %>' />
                                                                                    </td>
                                                                                    <td>
                                                                                        <input id="btnITEM_NAME" type="button" value="" runat="server" class="buttonDropdown"
                                                                                            tabindex="-1" />
                                                                                    </td>

                                                                                    <td>
                                                                                        <asp:TextBox ID="txtItem_qty" runat="server" CssClass="textBox textAutoSelect" Width="58" Text='<%# Bind("ITEM_QNTY") %>' onkeypress="return onlyNos(event,this);"></asp:TextBox>

                                                                                    </td>

                                                                                    <td>
                                                                                        <asp:TextBox ID="txtUOM_NAME" runat="server" CssClass="textBox textAutoSelect" Width="48" Text='<%# Bind("UOM_NAME") %>' Style=""></asp:TextBox>
                                                                                        <asp:HiddenField ID="hdnUomID" runat="server" Value='<%# Bind("UOM_ID") %>' />
                                                                                    </td>
                                                            --%>
                                                            <%--<td>
                                                                                        <asp:TextBox ID="txtUnitRate" runat="server" CssClass="textBox textAutoSelect" Width="58" Text='<%# Bind("UNIT_PRICE") %>' Style=""  Enabled="true" ></asp:TextBox>
                                                                                    </td>

                                                                                    <td>
                                                                                        <asp:TextBox ID="txtItemTotalCost" runat="server" CssClass="textBox textAutoSelect" Width="68" Text='<%# Bind("ITEM_TOTAL_COST") %>'  Style=""></asp:TextBox>
                                                                                    </td>

                                                                                   
                                                                                     <td>--%>
                                                            <%-- <asp:DropDownList ID="ddlItem" runat="server" CssClass="dropDownList" Width="195"></asp:DropDownList>--%>
                                                            <%-- <asp:TextBox ID="txtManufact_Name" runat="server" CssClass="textBox textAutoSelect" Width="155" Text='<%# Bind("MANUFAC_ID") %>'></asp:TextBox>
                                                                                        <asp:HiddenField ID="hdnmanufacid" runat="server" Value='<%# Bind("MANUFAC_ID") %>' />
                                                                                    </td>
                                                                                    <td>
                                                                                        <input id="btnmanufacid" type="button" value="" runat="server" class="buttonDropdown"
                                                                                            tabindex="-1" />
                                                                                    </td>
                                                                                      <td>
                                                                                        <asp:Label ID="lblCountryName" runat="server" CssClass="textBox textAutoSelect" Width="150" Text='<%# Bind("COUNTRY_ID") %>' Style=""></asp:Label>
                                                                                          <asp:HiddenField ID="hdnCountryID" runat="server" Value='<%# Bind("COUNTRY_ID") %>' />
                                                                                    </td>
                                                                                    
                                                                                     <td>
                                                                                        <asp:TextBox ID="txthscode1" runat="server" CssClass="textBox textAutoSelect" Width="98" Text='<%# Bind("HS_CODE1") %>' Style=""></asp:TextBox>
                                                                                    </td>
                                                                                     <td>
                                                                                        <asp:TextBox ID="txthscode2" runat="server" CssClass="textBox textAutoSelect" Width="98" Text='<%# Bind("HS_CODE2") %>' Style=""></asp:TextBox>
                                                                                    </td>
                                                                                 
                                                                                     <td>
                                                                                        <asp:TextBox ID="txthscode3" runat="server" CssClass="textBox textAutoSelect" Width="98" Text='<%# Bind("HS_CODE3") %>' Style=""></asp:TextBox>
                                                                                    </td>
                                                                                     <td>
                                                                                        <asp:TextBox ID="txtITEM_DET_REMARKS" runat="server" CssClass="textBox textAutoSelect" Width="155" Text='<%# Bind("ITEM_DET_REMARKS") %>' Style=""></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>--%>
                                                            <%--</div>
                                                                    <div style="overflow: visible;">
                                                                    </div>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="10" />
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                            </asp:TemplateField>--%>
                                                            <asp:TemplateField HeaderText="Delete" ShowHeader="true">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="btnDeleteRow" CssClass="buttonDeleteIcon" Height="16px" Width="16px"
                                                                        CommandName="delete" runat="server">
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField Visible="false">
                                                                <ItemTemplate>
                                                                    <div style="width: 10px;">
                                                                        <div>
                                                                            <div style="background-position: right center; height: 25px; cursor: pointer; background-image: url('../image/more.png'); background-repeat: no-repeat; text-align: left; vertical-align: middle;"
                                                                                onclick="togglePannelStatus(this)"
                                                                                title="More..">
                                                                                ...
                                                                            </div>
                                                                            <div style="display: none;">
                                                                                <div class="gridPanel" style="float: right; width: 0px; height: 0px;">
                                                                                    <div style="position: relative; height: 100%; width: 100%;">
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Font-Size="Smaller" />
                                                        <EditRowStyle BackColor="#999999" />
                                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                    </asp:GridView>
                                                    <input id="hdnJournalDetRefJson2" type="hidden" runat="server" value="[]" />
                                                    <input id="hdnJournalDetInsJson2" type="hidden" runat="server" value="[]" />
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnNewRow2" EventName="Click" />
                                                </Triggers>
                                            </asp:UpdatePanel>


                                        </div>
                                        <div id="divGridControls2" style="width: 100%; height: 25px; border-top: solid 1px #C0C0C0;">
                                            <table style="width: auto; height: 100%; text-align: center;" cellspacing="1" cellpadding="1"
                                                border="0">
                                                <tr>
                                                    <td style="width: 2px"></td>
                                                    <td style="width: 100px" align="left">
                                                        <asp:Button ID="btnNewRow2" runat="server" CssClass="buttonNewRow" Text="" OnClientClick="ShowProgress()" OnClick="btnNewRow2_Click" />
                                                    </td>
                                                    <td style="width: 20px;">
                                                        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                                                            DisplayAfter="300">
                                                            <ProgressTemplate>
                                                                <asp:Image ID="imgProgress2" runat="server" ImageUrl="~/image/loader.gif" />
                                                            </ProgressTemplate>
                                                        </asp:UpdateProgress>
                                                    </td>
                                                    <td style="width: 370px"></td>
                                                    <td align="right">&nbsp;
                                                    </td>
                                                    <td align="right">
                                                        <asp:Label ID="lbltotalSalesAmount" runat="server" Text="Total Qnty:" Font-Bold="True"></asp:Label>
                                                    </td>
                                                    <td align="right">
                                                        <asp:TextBox ID="txtTotSalesAmt" runat="server" CssClass="textBox" Style="text-align: right;"
                                                            Width="100" TabIndex="-1" Font-Bold="True"></asp:TextBox>
                                                    </td>
                                                    <td align="right">&nbsp;
                                                    </td>

                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>

            <div id="dvControlsFooter" style="height: auto; width: auto">
                <div style="height: 10px;">
                </div>
            </div>
        </div>

        <div id="dvContentFooter" class="dvContentFooter">
            <table>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="btnAddNew" runat="server" Text="New" CssClass="buttonNew" OnClick="btnAddNew_Click" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonCancel checkIsDirty" />
                    </td>
                    <td>
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonSave checkRequired" AccessKey="s" OnClick="btnSave_Click" OnClientClick="if ( ! UserDeleteConfirmation()) return false;" />
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="buttonEdit" OnClick="btnEdit_Click" />
                    </td>
                    <td>
                        <asp:Button ID="btnPost" runat="server" Text="Post" CssClass="buttoncommon" Visible="false" />
                    </td>
                    <td>
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="buttonDelete" />
                    </td>
                    <td>
                        <asp:Button ID="btnRefresh" runat="server" Text="Refresh" CssClass="buttonRefresh checkIsDirty" />
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
