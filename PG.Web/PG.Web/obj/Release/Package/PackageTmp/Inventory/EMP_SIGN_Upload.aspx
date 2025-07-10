<%@ Page Title="" Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="EMP_SIGN_Upload.aspx.cs" Inherits="PG.Web.Inventory.EMP_SIGN_Upload"   %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%--<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../javascript/jquery.ui.combogrid.js" type="text/javascript"></script>
    <script src="../javascript/jquery.attributeobserver.js" type="text/javascript"></script>
    <link href="../css/jquery.ui.combogrid.css" rel="stylesheet" type="text/css" />
    <%--<script type="text/javascript" src="../javascript/jquery-1.10.2.min.js"></script>--%>
    <%--<script type="text/javascript" src="../javascript/jquery-te-1.4.0.min.js"></script>--%>
    <%--<link href="../css/demo.css" rel="stylesheet" />
     <link href="../css/demo2.css" rel="stylesheet" />--%>
   <%-- <link href="../css/jquery-te-1.4.0.css" rel="stylesheet" />--%>
   <%-- <script type="text/javascript" src="../ckeditor/ckeditor.js"></script>--%>
    <%--<script type="text/javascript" src="../javascript/ckeditor.js"></script>--%>
   <%-- <script type="text/javascript" src="../javascript/ckeditor.js"></script>--%>
   <%-- <script src="../javascript/jquery.ui.combogrid.js" type="text/javascript"></script>--%>
    <%--<link href="../css/jquery.ui.combogrid.css" rel="stylesheet" type="text/css" />--%>
      <%--<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css"/> 
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap-theme.min.css"/>  
     <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js" type="text/javascript"></script> --%>
  
    <script language="javascript" type="text/javascript">
        // <!CDATA[

        var isPageResize = true;
        ContentForm.CalendarImageURL = "../image/calendar.png";
        var lblMessage = '<%=lblMessage.ClientID%>';

        var timerRefresh;
        //var delayRefresh = 2000; //3 min   // Delay in milliseconds

        <%--//var DealerListServiceLink = '<%=this.DealerListServiceLink%>';--%>
        var LocationListServiceLink = '<%=this.LocationListServiceLink%>';
        var UserListServiceLink = '<%=this.UserListServiceLink%>';


        var ReportViewPageLink = '<%=this.ReportViewPageLink%>';
        var ReportViewPDFPageLink = '<%=this.ReportViewPDFPageLink%>';
        var ReportPrintPageLink = '<%=this.ReportPrintPageLink%>';
        var ReportPDFPageLink = '<%=this.ReportPDFPageLink%>';
        var txtUserName = '<%=txtUserName.ClientID%>';
        var btnUserID = '<%= btnUserID.ClientID%>';
        var hdnEMPID = '<%=hdnEMPID.ClientID %>';
        var hdnUserID = '<%=hdnUserID.ClientID %>';
        
      <%--  var txtLocationCode = '<%=txtLocationCode.ClientID%>';
        var txtLocationName = '<%=txtLocationName.ClientID%>';
        var btnLocation = '<%=btnLocation.ClientID%>';
        var hdnLocationID = '<%=hdnLocationID.ClientID%>';--%>


<%--        var txtDealerID = '<%=txtDealerID.ClientID%>';--%>
        <%--var btnDealerID = '<%=btnDealerID.ClientID%>';--%>
        <%-- var hdnDealerID = '<%=hdnDealerID.ClientID%>';
        var txtDealerName = '<%=txtDealerName.ClientID%>';

        var btnAUTHO_BY_ID = '<%=btnAUTHO_BY_ID.ClientID%>';
        var txtAUTHO_BY_ID = '<%=txtAUTHO_BY_ID.ClientID%>';
        var lblAUTHO_BY_DP = '<%=lblAUTHO_BY_DP.ClientID%>';--%>
       <%-- var txtMemoBody = '<%=txtMemoBody.ClientID%>';--%>
        
        var reportURL = '';
        //var dealerCode = $('#' + txtDealerID).val();
        //var locationid = $('#' + ).val();
        //alert(1);

        //CKEDITOR.replace('txtMemoBody');
        
        //alert(2);
        //CKEDITOR.replace()
        function PageResizeCompleted(pg, cntMain) {
            resizeContentInner(cntMain);

        }
       <%-- function CheckDuplicates(sender, args) {
            alert(1);
            //Reference the GridView.
            var grid = document.getElementById("<%=grdArrearDate.ClientID %>");

            //Reference all Rows of the GridView.
            var rows = grid.getElementsByTagName("TR");
            for (var i = 1; i < rows.length; i++) {
                alert(2);
                //Reference the Cells of the Row.
                var cells = rows[i].getElementsByTagName("TD");

                //Fetch the values from the Cells.
                var name = cells[1].innerHTML;
                //var country = cells[2].innerHTML;
                alert(name);
                //Compare the values and check for Duplicates.
                if (name == document.getElementById("<%=txtArearDate.ClientID %>").value.trim()) {
                    sender.innerHTML = "Duplicate Date";
                    args.IsValid = false;
                    return;
                }
               
            }

            args.IsValid = true;
        }--%>

        function validate() {
            //var doc = document.getElementById('FreeTextBox1');
            //if (doc.value.length == 0) {
            //    alert('Please Enter data in Richtextbox');
            //    return false;
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

            //$("#btnOpenReportWindow").click(function () {
            //    window.open(reportURL, '_blank');
            //    hideOverlayReport();
            //});

            //$("#btnCacnelReportWindow").click(function () {
            //    hideOverlayReport();
            //});

            //hideOverlay();

        });

        $(document).ready(function () {

           
            //if ($('#' + txtLocationCode).is(':visible')) {
            //    bindLocationList();
            //}

            //if ($('#' + txtDealerID).is(':visible')) {
                
            //    bindDealerList();
               
            //}


            if ($('#' + txtUserName).is(':visible')) {

                bindUserList();

            }
        });


        function bindUserList() {

            var cgColumns = [
                              { 'columnName': 'name', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'User ID' }
                             , { 'columnName': 'fullname', 'width': '200', 'align': 'left', 'highlight': 4, 'label': 'Full Name' }
                              , { 'columnName': 'designation', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Designation' }

            ];

            var itemServiceURL = UserListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;

            itemServiceURL += "&ispaging=1";
            var userIDElem = $('#' + txtUserName);

            $('#' + btnUserID).click(function (e) {
                $(userIDElem).combogrid("dropdownClick");
            });

            $(userIDElem).combogrid({
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
                url: itemServiceURL,
                search: function (event, ui) {

                    var newServiceURL = itemServiceURL;

                    $(this).combogrid("option", "url", newServiceURL);


                },
                select: function (event, ui) {
                    if (!ui.item) {

                        event.preventDefault();
                        $('#' + hdnEMPID).val('0');
                        $('#' + hdnUserID).val('0');
                        return false;

                    }


                    if (ui.item.id == '') {

                        event.preventDefault();
                        return false;

                    }
                    else {
                        
                        $('#' + hdnUserID).val(ui.item.id);
                        $('#' + hdnEMPID).val(ui.item.name);
                        $('#' + txtUserName).val(ui.item.fullname);

                    }
                    return false;
                },

                lc: ''
            });


            //$(userIDElem).blur(function () {
            //    var self = this;
            //    var groupID = $(userIDElem).val();
            //    if (groupID == '') {
            //         $('#' + hdnUserID).val('0');
            //        $('#' + txtUserName).val('');

            //    }
            //});


            $(userIDElem).blur(function () {
                var self = this;
                elemID = $(userIDElem).attr('id');
                eCode = $(userIDElem).val();
                isComboGridOpen = $(self).combogrid('isOpened');
                if (eCode == '') {
                    $('#' + hdnEMPID).val('0');
                    $('#' + hdnUserID).val('0');
                    $('#' + txtUserName).val('');

                    //ClearItemData(elemID);
                }
                else {
                    var itemServiceURL = UserListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;

                    itemServiceURL += "&ispaging=1";


                    var prcNo = GetItemNo(eCode, itemServiceURL);

                    if (prcNo == null) {
                        $('#' + hdnEMPID).val('0');
                        $('#' + txtUserName).val('');
                        //ClearItemData(elemID);
                    }
                    else {
                        $('#' + hdnUserID).val(ui.item.id);
                        $('#' + hdnEMPID).val(ui.item.name);
                        $('#' + txtUserName).val(ui.item.fullname);
                        // SetItemData(elemID, grp);
                    }

                }
            });
        }


        function bindDealerList() {
            var cgColumns = [{ 'columnName': 'dealerid', 'width': '80', 'align': 'left', 'highlight': 2, 'label': 'Code' }
							 , { 'columnName': 'dealercodeex', 'width': '50', 'align': 'left', 'highlight': 4, 'label': 'No' }
							 , { 'columnName': 'dealername', 'width': '180', 'align': 'left', 'highlight': 4, 'label': 'Name' }
							 , { 'columnName': 'loccodename', 'width': '100', 'align': 'left', 'highlight': 0, 'label': 'Location' }

            ];

            //var companyid = $('#' + hdnCompanyID).val();
            //var depthead = $('#' + hdnEmpCode).val();

            //var serviceURL = GLAccountServiceLink + "?isterm=1&includeempty=1&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            var serviceURL = DealerListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            //serviceURL += "&companyid=" + companyid;
            serviceURL += "&excludefatorydealer=1";
            serviceURL += "&ispaging=1";
            // serviceURL += "&depthead=" + depthead;
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
                width: 600,
                url: serviceURL,
                search: function (event, ui) {

                    var dealerCode = $('#' + txtDealerID).val();
                    var locationid = $('#' + hdnLocationID).val();
                    debugger;

                    //var newServiceURL = serviceURL + "&locationid=" + locationid + "&dealerCode=" + dealerCode;
                    var newServiceURL = serviceURL + "&dealerCode=" + dealerCode;
                    $(this).combogrid("option", "url", newServiceURL);


                },
                select: function (event, ui) {
                    if (!ui.item) {
                        event.preventDefault();

                        $('#' + hdnDealerID).val('0');
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
                        $('#' + hdnDealerID).val(ui.item.dealerid);
                        $('#' + txtDealerID).val(ui.item.dealercode);
                        $('#' + txtDealerName).val(ui.item.dealername);

                        $("[id*=btnload]").click();
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
                    $('#' + txtDealerName).val('');
                }
            });
        }

        function bindLocationList() {
            var cgColumns = [{ 'columnName': 'code', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Code' }
                              , { 'columnName': 'name', 'width': '250', 'align': 'left', 'highlight': 4, 'label': 'Location Name' }
                             // , { 'columnName': 'locaddress', 'width': '70', 'align': 'left', 'highlight': 4, 'label': 'Loc Address' }
            ];
            var serviceURL = LocationListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            serviceURL += "&ispaging=1";

            var locCodeElem = $('#' + txtLocationCode);
            $('#' + btnLocation).click(function (e) {
                $(locCodeElem).combogrid("dropdownClick");
            });


            $(locCodeElem).combogrid({
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
                width: 450,
                url: serviceURL,
                search: function (event, ui) {

                    //var comp_id = $('#' + hdnCompanyID).val();
                    //var locgroup = $('#' + hdnFROM_LOC_GROUP).val();
                    //var newServiceURL = serviceURL + " &locgroup=" + locgroup;  //+ "&companyid=" + comp_id
                    var newServiceURL = serviceURL;
                    $(this).combogrid("option", "url", newServiceURL);
                },
                select: function (event, ui) {
                    if (!ui.item) {
                        event.preventDefault();
                        $('#' + hdnLocationID).val('');
                        $('#' + txtLocationCode).val('');
                        $('#' + txtLocationName).val('');
                        //event.preventDefault();
                        return false;
                    }


                    if (ui.item.code == '') {

                        $('#' + hdnLocationID).val('');
                        //$('#' + txtTO_LOC).val('');
                        $('#' + txtLocationName).val('');
                        event.preventDefault();
                        return false;
                    }
                    else {
                        $('#' + hdnLocationID).val(ui.item.id);
                        //$('#' + hdnLocationID).val(ui.item.code);
                        $('#' + txtLocationCode).val(ui.item.code);
                        $('#' + txtLocationName).val(ui.item.name);

                    }
                    return false;
                },

                lc: ''
            });

            $(locCodeElem).blur(function () {

                var self = this;
                var locCode = $(locCodeElem).val();

                if (locCode == '') {
                    $('#' + hdnLocationID).val('');
                    $('#' + txtLocationCode).val('');
                    $('#' + txtLocationName).val('');
                }
                else {
                    var loc = GetLocation(locCode);
                    if (loc != null) {
                        $('#' + hdnLocationID).val(loc.id);
                        // $('#' + hdnLocationID).val(loc.code);
                        $('#' + txtLocationName).val(loc.name);
                        //$('#' + hdnLocationID).val(ui.item.code);
                    }
                    else {
                        $('#' + hdnLocationID).val('');
                        $('#' + txtLocationCode).val('');
                        $('#' + txtLocationName).val('');
                    }
                }
            });

        }

        function GetLocation(selectloccode) {
            var prcNo = null;
            var isError = false;
            var isComplete = false;
            //ajax call

            //var locgroup = $('#' + hdnFROM_LOC_GROUP).val();
            //var newServiceURL = serviceURL + " &locgroup=" + locgroup + " &selectloccode=" + selectloccode;

            var serviceURL = LocationListServiceLink + "?selectloccode=" + selectloccode;

            var dummyVar = $.ajax({
                type: "GET",
                cache: false,
                async: false,
                dataType: "json",
                url: serviceURL,

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

        function bindAuthorizedList() {
            var cgColumns = [{ 'columnName': 'autho_emp_id', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Emp ID' }
                              , { 'columnName': 'full_name', 'width': '250', 'align': 'left', 'highlight': 4, 'label': ' Name' }
                              , { 'columnName': 'desig_name', 'width': '70', 'align': 'left', 'highlight': 4, 'label': 'Designation' }
            ];
            var serviceURL = ApprovalEmpListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            serviceURL += "&ispaging=1&dept_code=E&status=A";

            var txtAUTHO_BY_ID_Elem = $('#' + txtAUTHO_BY_ID);
            $('#' + btnAUTHO_BY_ID).click(function (e) {
                $(txtAUTHO_BY_ID_Elem).combogrid("dropdownClick");
            });

            $(txtAUTHO_BY_ID_Elem).combogrid({
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
                width: 450,
                url: serviceURL,
                search: function (event, ui) {
                    //var comp_id = $('#' + hdnCompanyID).val();
                    var loc_code = "00";
                    var empid = $('#' + txtAUTHO_BY_ID).val();
                    var newServiceURL = serviceURL + " &loc_code=" + loc_code + " &empid=" + empid ;
                    $(this).combogrid("option", "url", newServiceURL);
                },
                select: function (event, ui) {
                    if (!ui.item) {
                        $('#' + txtAUTHO_BY_ID).val('');
                        $('#' + lblAUTHO_BY_DP).val('');
                        event.preventDefault();
                        return false;
                    }

                    if (ui.item.autho_emp_id == '') {
                        $('#' + txtAUTHO_BY_ID).val('');
                        $('#' + lblAUTHO_BY_DP).val('');
                        event.preventDefault();
                        return false;
                    }
                    else {
                        $('#' + txtAUTHO_BY_ID).val(ui.item.autho_emp_id);
                        $('#' + lblAUTHO_BY_DP).val(ui.item.full_name);
                    }
                    return false;
                },

                lc: ''
            });
            $(txtAUTHO_BY_ID_Elem).blur(function () {
                var self = this;

                var groupID = $(txtAUTHO_BY_ID_Elem).val();

                if (groupID == '') {
                    $('#' + txtAUTHO_BY_ID).val('');
                    $('#' + lblAUTHO_BY_DP).val('');
                }
                else {
                    var empid = $('#' + txtAUTHO_BY_ID).val();
                    var serviceURL = ApprovalEmpListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
                    serviceURL += "&ispaging=1&dept_code=E&status=A";

                    var prcNo = GetProcessNo(empid, serviceURL);

                    if (prcNo == null) {
                        $('#' + txtAUTHO_BY_ID).val('');
                        $('#' + lblAUTHO_BY_DP).val('');
                    }
                }
            });
        }

        function GetProcessNo(processNo, serviceURL) {
            var prcNo = null;
            var isError = false;
            var isComplete = false;
            //ajax call

            var loc_code = "00";
            var newServiceURL = serviceURL + " &loc_code=" + loc_code + " &selectedId=" + processNo ;

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
            height:auto;
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
    <asp:HiddenField ID="hdnHR_REQ_SAL_APP_ID" runat="server" Value="0" />
    <asp:HiddenField ID="hdnAPP_STATUS_ID" runat="server" Value="0" />
      <asp:HiddenField ID="hdnSubmitstatus" runat="server" Value="0" />
    <asp:HiddenField ID="hdnEMPID" runat="server" Value="0" />
    <asp:HiddenField ID="hdnUserID" runat="server" Value="0" />
    <div id="dvPageContent" style="width: 100%; height: 100%;">
        <div id="dvContentHeader" class="dvContentHeader">
            <div id="dvHeader" class="dvHeader">
                <asp:Label ID="lblHeader" CssClass="lblHeader" runat="server" Text="Signature Upload Form "></asp:Label>
            </div>
            <div id="dvMsg" runat="server" class="dvMessage" style="width: 100%; min-height: 20px; height: auto; text-align: center;">
                <asp:Label ID="lblMessage" runat="server" Font-Size="Small" Width="100%" Height="16px"></asp:Label>
            </div>
        </div>
        <div id="dvContentMain" class="dvContentMain">
            <div id="dvControlsHead" style="height: auto; width: auto; text-align: left; vertical-align: top;">
            </div>
            <div id="dvControls" style="height: auto; width: 95%">
                <div id="dvControlsInner" class="groupBoxContainer boxShadow" style="height: auto; width: 70%">
                    <div id="groupBox">
                        <div id="groupHeader" class="groupHeader">
                            <span>Signature Upload Form</span>
                        </div>
                        <div id="groupContent" class="groupContent scrollBar">
                            <div id="groupContenInner">
                                <div id="groupDataMaster" style="width: 70%; height: auto;">
                                    <table style="" border="0" cellspacing="2" cellpadding="1">
                                        <tr>
                                            <td></td>

                                            <td></td>

                                            <td></td>

                                            <td></td>

                                        </tr>
                                    </table>

                                    <table style="width: 100%" border="0" cellspacing="2" cellpadding="1">
                                        <tr>
                                            <td style="width: 100%">
                                                <table style="width: 100%" border="0" cellspacing="2" cellpadding="1" >
                                                    <tr>
                        
                        <td style="text-align:right;">
                            <asp:Label runat="server" ID="lblUserName" Text="Employee Name : " Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtUserName" runat="server" CssClass="textBox textAutoSelect"></asp:TextBox>
                           <input id="btnUserID" type="button" value="" runat="server" class="buttonDropdown" tabindex="-1" />
                        </td>
                        <%--<td ></td>--%>
                        
                        
                    </tr>
                                                    
                                                 
                                                    <tr>

                                                        
                                                   
                                                        <td align="right">
                                                        <asp:Label ID="lblApplicationDate" runat="server" Text="Date:" Font-Bold="true"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <%--textDate dateParse--%>
                                                             <asp:TextBox ID="txtApplicationDate" runat="server" CssClass="textBox textDate dateParse " Style="text-align: left;" Width="130px"></asp:TextBox>
                                                        </td>
                                                       
                                                      
                                                      

                                                    </tr>
                                                    
                                                       
                                                      
                                                    
                                                       
                                                    <tr>
                                                        <td></td>
                                                           <td  class="auto-style2">
                                                              <asp:FileUpload ID="FileUpload1" runat="server" BorderColor="White" BorderStyle="Dashed" /></td>
                                                        
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td style="text-align: left">
                                                            <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="Upload" BackColor="Silver" BorderColor="#CCCCCC" BorderStyle="Solid" /></td>
                                                    </tr>

                                                    <tr>
                                                        <td></td>
                                                          <td style="text-align: right">
                                                           <asp:GridView ID="GridView2" runat="server" HeaderStyle-BackColor="#3AC0F2" HeaderStyle-ForeColor="White"
            RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White" AlternatingRowStyle-ForeColor="#000"
            AutoGenerateColumns="false">
            <Columns>
                <asp:TemplateField HeaderText="SL#" HeaderStyle-Width="40px">
                      <ItemTemplate>
                        <asp:Label ID="lblSLNo" runat="server" Text='<%#Container.DataItemIndex+1 %>' Style="text-align:left;"  Width="30px">

                        </asp:Label>
                                                                               
                         </ItemTemplate>
                      <ItemStyle VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:BoundField DataField="EMP_ID" HeaderText="EMP ID" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="200px" ItemStyle-VerticalAlign="Top"  />
                <asp:BoundField DataField="EMP_NAME" HeaderText="EMP Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="300px" ItemStyle-VerticalAlign="Top"  />
                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkDownload" runat="server"  OnClick="DownloadFile" CssClass="buttonAttach"
                            CommandArgument='<%# Eval("EMP_SIGN_ID") %>'><i class="glyphicon glyphicon-download" style="padding-right:4px;color:green;font-size:14px;"></i>Download</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkDelete" runat="server"   OnClick="DeleteFile"  CssClass="buttonAttach"
                            CommandArgument='<%# Eval("EMP_SIGN_ID") %>'><i class="glyphicon glyphicon-remove" style="padding-right:4px;color:red;font-size:14px;"></i>Delete</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

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
                                            </td>
                                            <td>
                                                <asp:HiddenField ID="hdnJournalID" runat="server" Value="0" />
                                                <asp:HiddenField ID="hdnJournalUpdateNo" runat="server" Value="0" />
                                            </td>
                                            <td>
                                                <asp:HiddenField ID="hdnGLGroupClassInclude" runat="server" Value="0" />
                                                <asp:HiddenField ID="hdnGLGroupClassExclude" runat="server" Value="0" />
                                            </td>
                                            <td>
                                                <asp:HiddenField ID="hdnAccSLNoID" runat="server" Value="0" />
                                                <asp:HiddenField ID="hdnEditDataModeInt" runat="server" Value="0" />
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
                              
                            </div>
                        </div>
                    </div>
                </div>

            </div>

           
        </div>

        <div id="dvContentFooter" class="dvContentFooter">
            <table style="display:none;">
                <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="btnAddNew" runat="server" Text="New" CssClass="buttonNew" OnClick="btnAddNew_Click" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonCancel checkIsDirty" OnClick="btnCancel_Click" />
                    </td>
                    <td>
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonSave" AccessKey="s"  OnClientClick="return validate()" OnClick="btnSave_Click" />
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="buttonEdit" OnClick="btnEdit_Click" />
                    </td>
                    <td>
                        <asp:Button ID="btnAuthorized" runat="server" Text="Submit" CssClass="buttoncommon" OnClick="btnAuthorized_Click" />
                    </td>
                    <td>
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="buttonDelete" Visible="false" />
                    </td>
                    <td>
                        <asp:Button ID="btnRefresh" runat="server" Text="Refresh" CssClass="buttonRefresh checkIsDirty" OnClick="btnRefresh_Click" />
                    </td>
                     <td>
                     <asp:Button ID="btnchallanprepareprint" runat="server" Text="Prepare Print" CssClass="buttonPrint" OnClick="btnprepareprint_Click" OnClientClick="showOverlay();" />
                       
                    </td>
                    
                    <td>
                        <input id="btnClose" type="button" runat="server" class="buttonClose" value="Close" onclick="if (ContentForm) { ContentForm.CloseForm(); }" />
                    </td>
                   
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

    </div>

</asp:Content>
