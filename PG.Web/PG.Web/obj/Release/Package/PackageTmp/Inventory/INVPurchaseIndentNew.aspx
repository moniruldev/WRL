<%@ Page Title="" Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="INVPurchaseIndentNew.aspx.cs" Inherits="PG.Web.Inventory.INVPurchaseIndentNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="../javascript/jquery.ui.combogrid.js" type="text/javascript"></script>
    <script src="../javascript/jquery.attributeobserver.js" type="text/javascript"></script>
    <link href="../css/jquery.ui.combogrid.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        // <!CDATA[
        var isPageResize = true;
        ContentForm.CalendarImageURL = "../image/calendar.png";

        var ReportViewPageLink = '<%=this.ReportViewPageLink%>';
        var ReportViewPDFPageLink = '<%=this.ReportViewPDFPageLink%>';
        var ReportPrintPageLink = '<%=this.ReportPrintPageLink%>';
        var ReportPDFPageLink = '<%=this.ReportPDFPageLink%>';

        var ItemGroupListServiceLink = '<%=this.ItemGroupListServiceLink%>';

        var ItemListServiceLink = '<%=this.ItemListServiceLink%>';

      <%--  var txtCustomerID = '<%=txtCustomerID.ClientID%>';--%>
        var gridUpdatePanelIDDet = '<%=UpdatePanel1.ClientID%>';
        var gridViewIDDet = '<%=GridView1.ClientID%>';
        var updateProgressID = '<%=UpdateProgress2.ClientID%>';

        var ddlIndtType = '<%=ddlIndtType.ClientID%>';
        var hdnStoreID = '<%=hdnStoreID.ClientID%>';
        var ddlFromDepartment = '<%=ddlFromDepartment.ClientID%>'
        

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

        function checkDt(fld) {
            var mo, day, yr;
            var entry = fld.value;
            var reLong = /\b\d{1,2}[\/-]\d{1,2}[\/-]\d{4}\b/;
            var reShort = /\b\d{1,2}[\/-]\d{1,2}[\/-]\d{2}\b/;
            var valid = (reLong.test(entry)) || (reShort.test(entry));
            if (valid) {
                var delimChar = (entry.indexOf("/") != -1) ? "/" : "-";
                var delim1 = entry.indexOf(delimChar);
                var delim2 = entry.lastIndexOf(delimChar);
                mo = parseInt(entry.substring(0, delim1), 10);
                day = parseInt(entry.substring(delim1 + 1, delim2), 10);
                yr = parseInt(entry.substring(delim2 + 1), 10);
                // handle two-digit year
                if (yr < 100) {
                    var today = new Date();
                    // get current century floor (e.g., 2000)
                    var currCent = parseInt(today.getFullYear() / 100) * 100;
                    // two digits up to this year + 15 expands to current century
                    var threshold = (today.getFullYear() + 15) - currCent;
                    if (yr > threshold) {
                        yr += currCent - 100;
                    } else {
                        yr += currCent;
                    }
                }
                var testDate = new Date(yr, mo - 1, day);
                if (testDate.getDate() == day) {
                    if (testDate.getMonth() + 1 == mo) {
                        if (testDate.getFullYear() == yr) {
                            // fill field with database-friendly format
                            fld.value = mo + "/" + day + "/" + yr;
                            return true;
                        } else {
                            alert("Check the year entry.");
                        }
                    } else {
                        alert("Check the month entry.");
                    }
                } else {
                    alert("Check the date entry.");
                }
            } else {
                alert("Invalid date format. Enter as mm/dd/yyyy.");
            }
            return false;
        }


        //var ctrl = document.getElementById('<txtInvoiceDate>');

        //var v = ctrl.value;

        //if (v.replace(/^\\s+|\\s+$/, '').length != 0 && isNaN(Date.parse(v))) { 
        //    alert('Invalid value!'); 
        //    ctrl.focus();
        //    return false; 
        //}




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
            return confirm("Are you sure you want to Save ? ");
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
                    if (charCode === 46)
                        return true;
                    else
                        return false;
                }
                return true;
            }
            catch (err) {
                alert(err.Description);
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
            bindItemGroupList(gridViewIDDet);
            //alert('OK 2');
            bindItemList(gridViewIDDet);
        });



        function bindItemGroupList(gridViewID) {
            var cgColumns = [{ 'columnName': 'itemgroupdesc', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                             , { 'columnName': 'itemgroupcode', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Code' }
            ];

            //var companyid = $('#' + hdnCompanyID).val();
            //var depthead = $('#' + hdnEmpCode).val();
            //var locationid = $('#' + ddlLocation).val();

            //var serviceURL = GLAccountServiceLink + "?isterm=1&includeempty=1&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            var serviceURL = ItemGroupListServiceLink + "?isterm=1&hasitem=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
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
                             , { 'columnName': 'itemgroupdesc', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Group Name' }
                             , { 'columnName': 'closing_qty', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Cls Qty' }
                             , { 'columnName': 'class_name', 'width': '120', 'align': 'left', 'highlight': 4, 'label': 'Class Name' }
                             //, { 'columnName': 'item_type', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Item Type' }



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
                    width: 800,
                    url: serviceURL,
                    search: function (event, ui) {
                        debugger;
                        var vgroupid = 0;
                        var elemRowCur = $(elem).closest('tr.gridRow');
                        var itemName = $(elemRowCur).find('input[id$="txtITEM_NAME"]').val();
                       
                        //if (itemName != "") {
                            vgroupid = $(elemRowCur).find('input[id$="hdngroupId"]').val();
                        //} else {
                        //    $(elemRowCur).find('input[id$="hdngroupId"]').val('0');
                        //    $(elemRowCur).find('input[id$="txtGroupName"]').val('');                           
                        //}

                        //var vgroupid = $('#' + hdngroupId).val();
                        var storeid = $('#' + hdnStoreID).val();
                        var newServiceURL = serviceURL + "&groupid=" + vgroupid;
                        var DeptID = $('[id*=ddlFromDepartment] option:selected').val();
                        //var ddlItemType = $(elemRowCur).find('[id*=ddlItemType] option:selected').val();
                        var ddlIndtType = $('[id*=ddlIndtType] option:selected').val();
                        //newServiceURL = newServiceURL + "&itemtypeid=" + ddlItemType;
                        newServiceURL = newServiceURL + "&deptid=" + DeptID + "&storeid=" + storeid; //"&itemtypeid=" + ddlIndtType +
                       
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
                            debugger;
                            event.preventDefault();
                            ClearItemData(elemID);
                            return false;
                            //ClearGLAccountData(elemID);
                        }



                        if (ui.item.id == 0) {
                            alert('item clear');
                            debugger;
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
                            debugger;


                            ClearItemData(elemID);
                        }
                        else {
                            debugger;
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
        function ClearGroupWhenItemBlank(txtItemID) {
            //$('#' + txtGLAccCodeID).val('');
            var detRow = $('#' + txtItemID).closest('tr.gridRow');
            $(detRow).find('input[id$="hdngroupId"]').val('0');
            $(detRow).find('input[id$="txtGroupName"]').val('');
        }

        function SetItemData(txtItemCodeID, data) {
            $('#' + txtItemCodeID).val(data.itemid);

            var detRow = $('#' + txtItemCodeID).closest('tr.gridRow');
            $(detRow).find('input[id$="hdnItemID"]').val(data.itemid);
            $(detRow).find('input[id$="txtItemCode"]').val(data.itemcode);
            $(detRow).find('input[id$="txtITEM_NAME"]').val(data.itemname);
            $(detRow).find('input[id$="hdngroupId"]').val(data.itemgroupid);
            $(detRow).find('input[id$="txtGroupName"]').val(data.itemgroupdesc);
            $(detRow).find('input[id$="hdnUomID"]').val(data.uomid);
            $(detRow).find('input[id$="txtUOM_NAME"]').val(data.uomname);
            $(detRow).find('input[id$="txtUnitPrice"]').val(data.weightedAvgPrice);

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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hdnLoggedInUser" runat="server" />
    <div id="dvPageContent" style="width: 100%; height: 100%;">
        <div id="dvContentHeader" class="dvContentHeader">
            <div id="dvHeader" class="dvHeader">
                 <asp:Label ID="lblHeader" CssClass="lblHeader" runat="server" Text="PUR:PURCHASE INDENT "></asp:Label>
            </div>
            <div id="dvMsg" runat="server" class="dvMessage" style="width: 100%; min-height: 20px; height: auto; text-align: center;">
                <asp:Label ID="lblMessage" runat="server" Font-Size="Small" Width="100%" Height="16px"></asp:Label>
            </div>
        </div>
        <div id="dvContentMain" class="dvContentMain">
            <div id="dvControlsHead" style="height: auto; width: auto; text-align: left; vertical-align: top;">
            </div>
            <asp:HiddenField ID="hdnIndtID" runat="server" Value="0" />
             <asp:HiddenField ID="hdnStoreID" runat="server" Value="0" />
            <div id="dvControls" style="height: auto; width: 100%">
                <div id="dvControlsInner" class="groupBoxContainer boxShadow">
                    <div id="groupBox">
                        <div id="groupHeader" class="groupHeader">
                             PUR<span>:PURCHASE INDENT </span>
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

                                                        <td align="right">
                                                            <asp:Label ID="lblPurchaseNO" runat="server" Text="Indent No:"></asp:Label>
                                                        </td>

                                                        <td>

                                                            <asp:TextBox ID="txtIndentNo" runat="server" CssClass="colourdisabletextBox" ReadOnly="True" BackColor="#FFFFCC"></asp:TextBox>
                                                        </td>
                                                       <td></td>
                                                     <td align="right">
                                                            <asp:Label ID="lblInvoiceDate" runat="server" Text="Indent Date:"></asp:Label><span style="color: red">*</span>

                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtIndtDate" runat="server" CssClass="textBox textDate dateParse" Style="text-align: left;" Width="100px"></asp:TextBox>
                                                            <asp:RegularExpressionValidator runat="server" ControlToValidate="txtIndtDate" ValidationExpression="(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$" ErrorMessage="Invalid date format." ValidationGroup="Group1" ForeColor="Red" />
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="Label3" runat="server" Text="Required Date:"></asp:Label><span style="color: red">*</span>

                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtRequiredDate" runat="server" CssClass="textBox textDate dateParse" Style="text-align: left;" Width="100px"></asp:TextBox>
                                                            <asp:RegularExpressionValidator runat="server" ControlToValidate="txtRequiredDate" ValidationExpression="(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$" ErrorMessage="Invalid date format." ValidationGroup="Group1" ForeColor="Red" />
                                                        </td>
                                                        <td></td>


                                                    </tr>


                                                    <tr>

                                                        <td align="right">
                                                            <asp:Label ID="lblDept" runat="server" Text="Indent Dept.:"></asp:Label>
                                                        </td>
                                                      <td align="left">
                                                            <asp:DropDownList ID="ddlFromDepartment" runat="server" CssClass="dropDownList required" OnSelectedIndexChanged="ddlFromDepartment_SelectedIndexChanged" AutoPostBack="true">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblRemarks" runat="server" Text="Remarks:"></asp:Label>
                                                        </td>
                                                        <td colspan="8">
                                                            <asp:TextBox ID="txtRemarks" runat="server" CssClass="textBox" TextMode="MultiLine" Height="30px" Width="400px"></asp:TextBox>

                                                        </td>


                                                    </tr>
                                                       <tr>

                                                        <td align="right">
                                                            <asp:Label ID="lblIndtType" runat="server" Text="Purchase Type:"></asp:Label>
                                                        </td>
                                                      <td align="left">
                                                            <asp:DropDownList ID="ddlIndtType" runat="server" CssClass="dropDownList required" Width="120px">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td align="right">
                                                         
                                                        </td>
                                                        <td colspan="8">

                                                        </td>


                                                    </tr>

                                                     <tr>
                                        
                                        <td style="" align="right">
                                            <asp:Label ID="lblUploadPR" runat="server" Text="Upload PR File:"></asp:Label>

                                        </td>
                                        <td style="" align="left">
                                            
                                            <asp:FileUpload ID="FileUpload1" runat="server" style="margin-top: 0px" Height="20px" Width="250px" />

                                        </td>

                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td> 
                                            <asp:Button ID="btnUpload" CssClass="buttonSearch" runat="server" Text="Load Data" OnClick="btnUpload_Click" />
                                        </td>
                                        
                                        <td></td>
                                        
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
                                <div id="groupDataDetails" style="width: 100%; height: auto;">
                                    <div id="dvGridSeparator" runat="server" style="width: 100%;">
                                    </div>
                                    <div id="groupDataHeaderCredit" runat="server" class="" style="width: 100%; text-align: center;">
                                        <span style="font-weight: bold">INDENT DETAILS</span>
                                    </div>
                                    <div id="dvGridContainer2" runat="server" class="" style="width: auto; height: auto; text-align: left">
                                        <div id="dvGridHeader2" style="width: 100%; height: 25px; font-size: smaller;" class="subHeader">
                                            <table style="height: 100%; color: #FFFFFF; font-weight: bold; text-align: center;"
                                                class="defFont" cellspacing="1" cellpadding="1">
                                                <tr class="headerRow">
                                                    <td width="64px" class="headerColLeft">SL#
                                                    </td>
                                                    <td width="80px" class="headerColLeft">Item Group
                                                    </td>
                                                    <td width="10px" class="headerColLeft"></td>
                                                  <td width="80px" class="headerColLeft">Item Code
                                                    </td>
                                                    <td width="195px" class="headerColLeft">Item
                                                    </td>
                                                  
                                                    <td width="10px" class="headerColLeft"></td>
                                                 <%--    <td width="120px" class="headerColLeft">Item Type
                                                    </td>--%>
                                                    <td width="60px" class="headerColLeft">Item Qty
                                                    </td>
                                                    <td width="50px" class="headerColLeft">UOM
                                                    </td>
                                                    <td width="80px" class="headerColLeft">Priority
                                                    </td>
                                                  <%--  <td width="60px" class="headerColLeft">Total Cost
                                                    </td>--%>
                                                    <td width="160px" class="headerColCenter">Remarks
                                                    </td>

                                                </tr>
                                            </table>
                                        </div>
                                        <div id="dvGrid" style="width: 100%; height: 200px; overflow: auto;" class="dvGrid">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" ShowHeader="False"
                                                        CellPadding="1" CellSpacing="1" ForeColor="#333333" GridLines="Both" OnRowDataBound="GridView1_RowDataBound" OnRowCreated="GridView1_RowCreated" OnRowCommand="GridView1_RowCommand"  DataKeyNames="ITEM_ID"
                                                        EnableModelValidation="True" ClientIDMode="AutoID" OnRowDeleting="GridView1_RowDeleting1">
                                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="SL#">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSLNo" runat="server" Text='<%# Bind("INDT_SLNO") %>' Style="text-align: center;"
                                                                        Width="64px">
                                                                    </asp:Label>
                                                                    <asp:HiddenField ID="hdnRecordStateInt" runat="server" Value='<%# Bind("_RecordStateInt") %>' />
                                                                    <asp:HiddenField ID="hdnReqDetId" runat="server" Value='<%# Bind("INDT_DET_ID") %>' />
                                                                </ItemTemplate>
                                                                <ItemStyle VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Battery Type" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <div>
                                                                        <table border="0" cellpadding="1" cellspacing="1">
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td>
                                                                                        <%--<asp:DropDownList ID="ddlItemGroup" runat="server" CssClass="dropDownList" Width="80"></asp:DropDownList>--%>
                                                                                        <asp:TextBox ID="txtGroupName" runat="server" CssClass="textBox textAutoSelect" Width="80" Text='<%# Bind("ITEM_GROUP_NAME") %>'></asp:TextBox>
                                                                                        <asp:HiddenField ID="hdngroupId" runat="server" Value='<%# Bind("ITEM_GROUP_ID") %>' />
                                                                                    </td>
                                                                                    <td>
                                                                                        <input id="btnItemGroup" type="button" value="" runat="server" class="buttonDropdown" tabindex="-1" />
                                                                                    </td>
                                                                                <%--    <td>
                                                                                        <asp:HiddenField ID="hdnItemType" runat="server" Value='<%# Bind("ITEM_TYPE_ID") %>' />
                                                                                        <asp:DropDownList ID="ddlItemType" runat="server" CssClass="dropDownList" Width="120" >
                                                                                           
                                                                                        </asp:DropDownList>
                                                                                    </td>--%>
                                                                                     <td>
                                                                                        <%--<asp:DropDownList ID="ddlItemGroup" runat="server" CssClass="dropDownList" Width="80"></asp:DropDownList>--%>
                                                                                        <asp:TextBox ID="txtItemCode" runat="server" CssClass="textBox textAutoSelect" Width="80" Text='<%# Bind("item_code") %>'></asp:TextBox>
                                                                                       
                                                                                    </td>
                                                                                    <td>
                                                                                        <%-- <asp:DropDownList ID="ddlItem" runat="server" CssClass="dropDownList" Width="195"></asp:DropDownList>--%>
                                                                                        <asp:TextBox ID="txtITEM_NAME" runat="server" CssClass="textBox textAutoSelect" Width="185" Text='<%# Bind("ITEM_NAME") %>'></asp:TextBox>
                                                                                        <asp:HiddenField ID="hdnItemID" runat="server" Value='<%# Bind("ITEM_ID") %>' />
                                                                                    </td>
                                                                                    <td>
                                                                                        <input id="btnITEM_NAME" type="button" value="" runat="server" class="buttonDropdown"
                                                                                            tabindex="-1" />
                                                                                    </td>
                                                                                   <%-- <td>
                                                                                        <asp:Label ID="lblItemType" runat="server" Text='<%# Bind("ITEM_TYPE_CODE") %>' Width="120px"></asp:Label>
                                                                                    </td>--%>

                                                                                    <td>
                                                                                        <asp:TextBox ID="txtItem_qty" runat="server" CssClass="textBox textAutoSelect" Width="58" Text='<%# Bind("INDT_QTY") %>' onkeypress="return onlyNos(event,this);" OnTextChanged="txtItem_qty_TextChanged" AutoPostBack="true"></asp:TextBox>

                                                                                    </td>

                                                                                    <td>
                                                                                        <asp:TextBox ID="txtUOM_NAME" runat="server" CssClass="textBox textAutoSelect" Width="49" Text='<%# Bind("UOM_NAME") %>' Style=""></asp:TextBox>
                                                                                        <asp:HiddenField ID="hdnUomID" runat="server" Value='<%# Bind("UOM_ID") %>' />
                                                                                    </td>

                                                                                    <td>
                                                                                        <asp:TextBox ID="txtPriority" runat="server" CssClass="textBox textAutoSelect" Width="80" Text='<%# Bind("PRIORITY") %>' Style=""></asp:TextBox>
                                                                                        <%--<asp:HiddenField ID="hdnPriority" runat="server" Value='<%# Bind("PRIORITY") %>' />
                                                                                        <asp:DropDownList ID="ddlPriority" runat="server" Width="80px">
                                                                                            <asp:ListItem Value="High">High</asp:ListItem>
                                                                                            <asp:ListItem  Value="Medium">Medium</asp:ListItem>
                                                                                            <asp:ListItem Value="Low">Low</asp:ListItem>
                                                                                        </asp:DropDownList>--%>
                                                                                     </td>

                                                                                 <%--    <td>
                                                                                        <asp:TextBox ID="txtUnitPrice" Enabled="false" runat="server" CssClass="textBox textAutoSelect" Width="58" Text='<%# Bind("UNIT_PRICE") %>' onkeypress="return onlyNos(event,this);"></asp:TextBox>

                                                                                    </td>

                                                                                     <td>
                                                                                        <asp:TextBox ID="txtTotalCost" Enabled="false" runat="server" CssClass="textBox textAutoSelect" Width="58" Text='<%# Bind("TOTAL_COST") %>' onkeypress="return onlyNos(event,this);"></asp:TextBox>

                                                                                    </td>--%>


                                                                                    <td>
                                                                                        <asp:TextBox ID="txtRemarks" runat="server" CssClass="textBox textAutoSelect" Width="150" Text='<%# Bind("INDT_REMARKS") %>' Style=""></asp:TextBox>
                                                                                    </td>

                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                    </div>
                                                                    <div style="overflow: visible;">
                                                                    </div>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="10" />
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Delete" ShowHeader="false">
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
                                                    <td style="width: 90px" align="left">
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
                        <asp:Button ID="btnAuthorized" runat="server" Text="Authorize" CssClass="buttonAthorize" OnClick="btnAuthorized_Click" OnClientClick="return confirm('Are you sure to Authorize?');" />
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
                    <td>
                        <asp:Button ID="btnMRRPrint" runat="server" Text="Print Indent" CssClass="buttoncommon" Enabled="True" OnClick="btnMRRPrint_Click" />
                    </td>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="Print Format:"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlReportFormat" runat="server" CssClass="dropDownList" Width="100px">
                             <asp:ListItem Value="0">In This Tab</asp:ListItem>
                            <asp:ListItem Value="1">In New Tab</asp:ListItem>
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
                        <asp:Button ID="btnPopupTrigger" runat="server" Text="Button" CssClass="buttonHidden" />
                        <asp:HiddenField ID="hdnPopupTriggerID" runat="server" Value="" />
                        <asp:HiddenField ID="hdnPopupCommand" runat="server" Value="" />
                    </td>
                </tr>
            </table>
        </div>

    </div>
</asp:Content>
