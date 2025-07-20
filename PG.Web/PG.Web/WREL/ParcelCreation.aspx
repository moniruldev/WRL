<%@ Page Title="" Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="ParcelCreation.aspx.cs" Inherits="PG.Web.WREL.ParcelCreation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <script src="../javascript/jquery.ui.combogrid.js" type="text/javascript"></script>
    <script src="../javascript/jquery.attributeobserver.js" type="text/javascript"></script>
    <link href="../css/jquery.ui.combogrid.css" rel="stylesheet" type="text/css" />
<%--    <link href="../css/toastr.css" rel="stylesheet" />
    <script src="../javascript/toastr.js" type="text/javascript"></script>--%>
  
    <script language="javascript" type="text/javascript">
        // <!CDATA[
        var isPageResize = true;
        ContentForm.CalendarImageURL = "../image/calendar.png";

        var ReportViewPageLink = '<%=this.ReportViewPageLink%>';
        var ReportViewPDFPageLink = '<%=this.ReportViewPDFPageLink%>';
        var ReportPrintPageLink = '<%=this.ReportPrintPageLink%>';
        var ReportPDFPageLink = '<%=this.ReportPDFPageLink%>';


        var DistrictListServiceLink = '<%=this.DistrictListServiceLink%>';
        var TownListServiceLink = '<%=this.TownListServiceLink%>';
        var RouteListServiceLink = '<%=this.RouteListServiceLink%>';
        var CNListServiceLink = '<%=this.CNListServiceLink%>';
        var ClientListServiceLink = '<%=this.ClientListServiceLink%>';
        var AgreementDetailsListServiceLink = '<%=this.AgreementDetailsListServiceLink%>';
        var HubListServiceLink = '<%=this.HubListServiceLink%>';
        
        
        
      
        var gridUpdatePanelIDDet = '<%=UpdatePanel1.ClientID%>';
        var gridViewIDDet = '<%=GridView1.ClientID%>';

        var txtAggrementDtl = '<%=txtAggrementDtl.ClientID%>';
        var hdnAggrementDtlId = '<%=hdnAggrementDtlId.ClientID%>';
        var txtItemName = '<%=txtItemName.ClientID%>';
        var hdnItemId = '<%=hdnItemId.ClientID%>';
        var txtServiceAmt = '<%=txtServiceAmt.ClientID%>';

        var txtClientName = '<%=txtClientName.ClientID%>';
        var hdnClientId = '<%=hdnClientId.ClientID%>';

        var txtRoute = '<%=txtRoute.ClientID%>';
        var hdnRouteId = '<%=hdnRouteId.ClientID%>';

        var txtHubName = '<%=txtHubName.ClientID%>';
        var hdnHubId = '<%=hdnHubId.ClientID%>';
      

        

        $(document).ready(function () {

            var pageInstance = Sys.WebForms.PageRequestManager.getInstance();

            pageInstance.add_pageLoaded(function (sender, args) {
                var panels = args.get_panelsUpdated();
                for (i = 0; i < panels.length; i++) {

                    if (panels[i].id == gridUpdatePanelIDDet) {
                        bindDestinationDistList(gridViewIDDet);
                        bindDestinationTownList(gridViewIDDet);
                    }

                }

            });


            if ($('#' + txtClientName).is(':visible')) {

                bindClientNameList();

            }

            if ($('#' + txtAggrementDtl).is(':visible')) {

                bindAgreementDetailsList();

            }

            if ($('#' + txtHubName).is(':visible')) {

                bindHubList();

            }

            if ($('#' + txtRoute).is(':visible')) {

                bindRouteList();

            }

            bindDestinationDistList(gridViewIDDet);
            bindDestinationTownList(gridViewIDDet);



        });
     
        function bindClientNameList() {
            var cgColumns = [
                             { 'columnName': 'clientname', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Client Name' }
                            , { 'columnName': 'mobile', 'width': '200', 'align': 'left', 'highlight': 4, 'label': 'Mobile' }

            ];
            var serviceURL = ClientListServiceLink + "?isterm=1&includeempty=0&hasitem=1&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;

            serviceURL += "&ispaging=0";
            var groupIDElem = $('#' + txtClientName);

            $('#' + txtClientName).click(function (e) {
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
                width: 350,
                url: serviceURL,
                search: function (event, ui) {

                    var newServiceURL = serviceURL;
                    $(this).combogrid("option", "url", newServiceURL);


                },
                select: function (event, ui) {
                    if (!ui.item) {
                        event.preventDefault();
                        return false;
                    }

                    if (ui.item.dealerid == '') {
                        event.preventDefault();
                        return false;
                    }
                    else {
                        $('#' + hdnClientId).val(ui.item.clientid);
                        $('#' + txtClientName).val(ui.item.clientname);
                    }
                    return false;
                },

                lc: ''
            });


            $(groupIDElem).blur(function () {
                var self = this;

                var groupID = $(groupIDElem).val();
                if (groupID == '') {
                    $('#' + txtClientName).val('');
                    $('#' + hdnClientId).val('0');
                }
            });
        }

        function bindAgreementDetailsList() {
            var cgColumns = [
                             { 'columnName': 'description', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Agrement' }
                            , { 'columnName': 'itemname', 'width': '200', 'align': 'left', 'highlight': 4, 'label': 'Item' }

            ];
            var serviceURL = AgreementDetailsListServiceLink + "?isterm=1&includeempty=0&hasitem=1&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;

            serviceURL += "&ispaging=0";
            var groupIDElem = $('#' + txtAggrementDtl);

            $('#' + txtAggrementDtl).click(function (e) {
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
                width: 350,
                url: serviceURL,
                search: function (event, ui) {

                    var newServiceURL = serviceURL;
                    $(this).combogrid("option", "url", newServiceURL);


                },
                select: function (event, ui) {
                    if (!ui.item) {
                        event.preventDefault();
                        return false;
                    }

                    if (ui.item.dealerid == '') {
                        event.preventDefault();
                        return false;
                    }
                    else {
                        $('#' + hdnAggrementDtlId).val(ui.item.agrdtlid);
                        $('#' + txtAggrementDtl).val(ui.item.description);
                        $('#' + txtItemName).val(ui.item.itemname);
                        $('#' + hdnItemId).val(ui.item.itemid);
                        $('#' + txtServiceAmt).val(ui.item.serviceamt);
                    }
                    return false;
                },

                lc: ''
            });


            $(groupIDElem).blur(function () {
                var self = this;

                var groupID = $(groupIDElem).val();
                if (groupID == '') {
                    $('#' + txtAggrementDtl).val('');
                    $('#' + hdnAggrementDtlId).val('0');
                }
            });
        }

        function bindHubList() {
            var cgColumns = [
                             { 'columnName': 'hubname', 'width': '200', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                            //, { 'columnName': 'distname', 'width': '200', 'align': 'left', 'highlight': 4, 'label': 'Name' }

            ];
            var serviceURL = HubListServiceLink + "?isterm=1&includeempty=0&hasitem=1&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;

            serviceURL += "&ispaging=0";
            var groupIDElem = $('#' + txtHubName);

            $('#' + txtHubName).click(function (e) {
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
                width: 350,
                url: serviceURL,
                search: function (event, ui) {

                    var newServiceURL = serviceURL;
                    $(this).combogrid("option", "url", newServiceURL);


                },
                select: function (event, ui) {
                    if (!ui.item) {
                        event.preventDefault();
                        return false;
                    }

                    if (ui.item.dealerid == '') {
                        event.preventDefault();
                        return false;
                    }
                    else {
                        $('#' + hdnHubId).val(ui.item.hubid);
                        $('#' + txtHubName).val(ui.item.hubname);
                    }
                    return false;
                },

                lc: ''
            });


            $(groupIDElem).blur(function () {
                var self = this;

                var groupID = $(groupIDElem).val();
                if (groupID == '') {
                    $('#' + txtHubName).val('');
                    $('#' + hdnHubId).val('0');
                }
            });
        }

        function bindRouteList() {
            var cgColumns = [
                             { 'columnName': 'routeid', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Code' }
                            , { 'columnName': 'routename', 'width': '200', 'align': 'left', 'highlight': 4, 'label': 'Name' }

            ];
            var serviceURL = RouteListServiceLink + "?isterm=1&includeempty=0&hasitem=1&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;

            serviceURL += "&ispaging=0";
            var groupIDElem = $('#' + txtRoute);

            $('#' + txtRoute).click(function (e) {
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
                width: 350,
                url: serviceURL,
                search: function (event, ui) {

                    var newServiceURL = serviceURL;
                    $(this).combogrid("option", "url", newServiceURL);


                },
                select: function (event, ui) {
                    if (!ui.item) {
                        event.preventDefault();
                        return false;
                    }

                    if (ui.item.dealerid == '') {
                        event.preventDefault();
                        return false;
                    }
                    else {
                        $('#' + hdnRouteId).val(ui.item.routeid);
                        $('#' + txtRoute).val(ui.item.routename);
                    }
                    return false;
                },

                lc: ''
            });


            $(groupIDElem).blur(function () {
                var self = this;

                var groupID = $(groupIDElem).val();
                if (groupID == '') {
                    $('#' + txtRoute).val('');
                    $('#' + hdnRouteId).val('0');
                }
            });
        }

        function bindDestinationDistList(gridViewID) {
            var cgColumns = [{ 'columnName': 'distname', 'width': '200', 'align': 'left', 'highlight': 4, 'label': 'District Name' }
                        

            ];

           
            var serviceURL = DistrictListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            serviceURL += "&ispaging=0";

            var gridSelector = "#" + gridViewID;

            $(gridSelector).find('input[id$="txtDestinationDist"]').each(function (index, elem) {

                var elemRow = $(elem).closest('tr.gridRow');

                var hdnItemIDElem = $(elemRow).find('input[id$="txtDestinationDist"]');

                $(elem).closest('tr').find('input[id$="txtDestinationDist"]').click(function (e) {
                    elmID = $(elem).attr('id');
                    $(elem).combogrid("dropdownClick");
                });

                $(elem).data("selectedItem", null);
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
                    width: 400,
                    url: serviceURL,
                    search: function (event, ui) {
                        debugger;
                        var vgroupid = 0;
                        var elemRowCur = $(elem).closest('tr.gridRow');
                        var itemName = $(elemRowCur).find('input[id$="txtDestinationDist"]').val();
                       // vgroupid = $(elemRowCur).find('input[id$="hdngroupId"]').val();

                        var newServiceURL = serviceURL;//+ "&groupid=" + vgroupid
                        newServiceURL = JSUtility.AddTimeToQueryString(newServiceURL);
                        $(this).combogrid("option", "url", newServiceURL);

                    },

                    select: function (event, ui) {
                     
                        elemID = $(elem).attr('id');
                     
                        if (!ui.item) {
                            debugger;
                            event.preventDefault();
                            ClearItemData(elemID);
                            return false;
                        }

                        if (ui.item.id == 0) {
                            alert('item clear');
                            debugger;
                            event.preventDefault();
                            return false;
                        }
                        else {
                            $(elem).data("selectedItem", ui.item);
                            SetItemData(elemID, ui.item);
                        }
                        return false;
                    }

                });

                $(elem).blur(function () {
                    var self = this;
                    elemID = $(elem).attr('id');
                    eCode = $(elem).val();

                    isComboGridOpen = $(self).combogrid('isOpened');
                    var selectedData = $(self).data("selectedItem");

                    if (eCode === '') {
                        ClearItemData(elemID);
                    } else if (selectedData) {
                       // SetItemData(elemID, selectedData);
                    } else {
                        ClearItemData(elemID);
                    }

                });

            });

        }

        function ClearItemData(txtItemID) {
            var detRow = $('#' + txtItemID).closest('tr.gridRow');
            $(detRow).find('input[id$="hdnDestinationDistId"]').val('0');
            $(detRow).find('input[id$="txtDestinationDist"]').val('');
        }
      
        function SetItemData(txtItemCodeID, data) {
            $('#' + txtItemCodeID).val(data.cnnumber);
            var detRow = $('#' + txtItemCodeID).closest('tr.gridRow');
            $(detRow).find('input[id$="hdnDestinationDistId"]').val(data.distid);
            $(detRow).find('input[id$="txtDestinationDist"]').val(data.distname);

        }


        function bindDestinationTownList(gridViewID) {
            var cgColumns = [{ 'columnName': 'townname', 'width': '200', 'align': 'left', 'highlight': 4, 'label': 'Town Name' }


            ];


            var serviceURL = TownListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            serviceURL += "&ispaging=0";

            var gridSelector = "#" + gridViewID;

            $(gridSelector).find('input[id$="txtTownName"]').each(function (index, elem) {

                var elemRow = $(elem).closest('tr.gridRow');

                var hdnItemIDElem = $(elemRow).find('input[id$="txtTownName"]');

                $(elem).closest('tr').find('input[id$="txtTownName"]').click(function (e) {
                    elmID = $(elem).attr('id');
                    $(elem).combogrid("dropdownClick");
                });

                $(elem).data("selectedItem", null);
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
                    width: 400,
                    url: serviceURL,
                    search: function (event, ui) {
                        debugger;
                        var vgroupid = 0;
                        var elemRowCur = $(elem).closest('tr.gridRow');
                        var itemName = $(elemRowCur).find('input[id$="txtTownName"]').val();
                        // vgroupid = $(elemRowCur).find('input[id$="hdngroupId"]').val();

                        var newServiceURL = serviceURL;//+ "&groupid=" + vgroupid
                        newServiceURL = JSUtility.AddTimeToQueryString(newServiceURL);
                        $(this).combogrid("option", "url", newServiceURL);

                    },

                    select: function (event, ui) {

                        elemID = $(elem).attr('id');

                        if (!ui.item) {
                            debugger;
                            event.preventDefault();
                            ClearTownData(elemID);
                            return false;
                        }

                        if (ui.item.id == 0) {
                            alert('item clear');
                            debugger;
                            event.preventDefault();
                            return false;
                        }
                        else {
                            $(elem).data("selectedItem", ui.item);
                            SetTownData(elemID, ui.item);
                        }
                        return false;
                    }

                });

                $(elem).blur(function () {
                    var self = this;
                    elemID = $(elem).attr('id');
                    eCode = $(elem).val();

                    isComboGridOpen = $(self).combogrid('isOpened');
                    var selectedData = $(self).data("selectedItem");

                    if (eCode === '') {
                        ClearTownData(elemID);
                    } else if (selectedData) {
                        // SetItemData(elemID, selectedData);
                    } else {
                        ClearTownData(elemID);
                    }

                });

            });

        }

        function ClearTownData(txtItemID) {
            var detRow = $('#' + txtItemID).closest('tr.gridRow');
            $(detRow).find('input[id$="hdnTownId"]').val('0');
            $(detRow).find('input[id$="txtTownName"]').val('');
        }

        function SetTownData(txtItemCodeID, data) {
            $('#' + txtItemCodeID).val(data.cnnumber);
            var detRow = $('#' + txtItemCodeID).closest('tr.gridRow');
            $(detRow).find('input[id$="hdnTownId"]').val(data.townid);
            $(detRow).find('input[id$="txtTownName"]').val(data.townname);

        }
      

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


       


        function ShowProgress() {
            $('#' + updateProgressID).show();
        }

        function UserSaveConfirmation() {
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

        $(document).ready(function () {

           
        });


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


        input[type="radio"] + label
        {
            margin-left: 4px;
            margin-right: 4px;
        } 


        
        /*label.col-form-label-sm{
            text-align:right;
        }*/
        
      
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hdnLoggedInUser" runat="server" />
    <div class="row">
     <div class="container-fluid">
       <div class="card">
         <div class="card-header p-0">
           <div class="d-flex align-items-center justify-content-between p-1">
             <h5 class="card-title">Parcel Create</h5>
             <a class="btn btn-primary p-1"> <i class="fas fa-list"></i> Parcel List </a>
         </div>

       </div>
      <div class="card-body">
            <asp:HiddenField ID="hdnCN_ID" runat="server" Value="0" />
            <asp:HiddenField ID="hdnReservationId" runat="server" Value="0" />

              <div class="row mb-0">

              <%--  <div class="col-md-4">
                  <div class="form-group row mb-0">
                    <label for="name" class="col-sm-5 col-form-label-sm">CN Number :</label>
                    <div class="col-sm-7">
                       
                       <asp:TextBox ID="txtCNNo" runat="server" CssClass="form-control form-control-sm" ></asp:TextBox>

                    </div>
                  </div>
                </div>--%>
               <div class="col-md-4">
                  <div class="form-group row mb-0">
                    <label for="name" class="col-sm-5 col-form-label-sm">Client :</label>
                    <div class="col-sm-7">
                      <asp:TextBox runat="server"  class="form-control form-control-sm"  ID="txtClientName" placeholder="Select" ></asp:TextBox> 
                           <asp:HiddenField runat="server" ID="hdnClientId" Value="0" /> 
                    </div>
                  </div>

                </div>
               

                 <div class="col-md-4">
                  <div class="form-group row mb-0">
                    <label for="name" class="col-sm-5 col-form-label-sm">Aggrement Details :</label>
                    <div class="col-sm-7">
                      <asp:TextBox runat="server"  class="form-control form-control-sm"  ID="txtAggrementDtl" placeholder="Select" ></asp:TextBox> 
                           <asp:HiddenField runat="server" ID="hdnAggrementDtlId" Value="0" /> 
                    </div>
                  </div>

                </div>
                    <div class="col-md-4">
                  <div class="form-group row mb-0">
                    <label for="name" class="col-sm-5 col-form-label-sm">Item :</label>
                    <div class="col-sm-7">
                      <asp:TextBox runat="server"  class="form-control form-control-sm"  ID="txtItemName" placeholder="Select" ></asp:TextBox> 
                       <asp:HiddenField runat="server" ID="hdnItemId" Value="0" /> 
                    </div>
                  </div>

                </div>

             </div>

            <div class="row mb-0">

              

                 <div class="col-md-4">
                  <div class="form-group row mb-0">
                    <label for="name" class="col-sm-5 col-form-label-sm">Service Amount :</label>
                    <div class="col-sm-7">
                      <asp:TextBox runat="server"  class="form-control form-control-sm"  ID="txtServiceAmt" placeholder="Select"  ></asp:TextBox> 
                       
                    </div>
                  </div>

                </div>

                 <div class="col-md-4">
                  <div class="form-group row mb-0">
                    <label for="name" class="col-sm-5 col-form-label-sm">Route :</label>
                    <div class="col-sm-7">
                      <asp:TextBox runat="server"  class="form-control form-control-sm"  ID="txtRoute" placeholder="Select" ></asp:TextBox> 
                        <asp:HiddenField runat="server" ID="hdnRouteId" Value="0" /> 
                    </div>
                  </div>

                </div>
                  <div class="col-md-4">
                  <div class="form-group row mb-0">
                    <label for="name" class="col-sm-5 col-form-label-sm">Hub :</label>
                    <div class="col-sm-7">
                      <asp:TextBox runat="server"  class="form-control form-control-sm"  ID="txtHubName" placeholder="Enter Hub Name" ></asp:TextBox> 
                       <asp:HiddenField runat="server" ID="hdnHubId" Value="0" /> 
                    </div>
                  </div>
                </div>

                </div>


           <div class="row mb-0">

               

                  <div class="col-md-4">
                    <div class="form-group row align-items-center mb-0">
                        <label for="FileUpload1" class="col-sm-5 col-form-label-sm">Upload Details :</label>
                        <div class="col-sm-7">
                            <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control form-control-sm p-0 m-0" />
                        </div>
                      
                    </div>
                
                </div>

          <div class="col-md-4">
                <div class="form-group d-flex align-items-center mb-0 gap-2">
                    <asp:Button ID="btnUpload" runat="server" Text="Upload Excel"
                        CssClass="btn btn-sm btn-primary mr-2" OnClick="btnUpload_Click" />

                    <asp:Button ID="btnDownloadSample" runat="server" Text="Download Sample"
                        CssClass="btn btn-sm btn-primary" OnClick="btnDownloadSample_Click" />
                </div>
            </div>



               

             </div>


      </div>
    <div class="row">
    <div class="col-md-12">
        <div class="card">
           <div class="card-header mb-0 p-1">
              <strong>Recipient Details :</strong>

           </div>

            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
              <ContentTemplate>
             <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" ShowHeader="true"
    CssClass="table table-sm table-striped table-bordered w-auto"  
    DataKeyNames="CN_ID" EnableModelValidation="True" ClientIDMode="AutoID"
    OnRowDataBound="GridView1_RowDataBound" OnRowCommand="GridView1_RowCommand" OnRowDeleting="GridView1_RowDeleting" OnRowCreated="GridView1_RowCreated" >
    
    <HeaderStyle CssClass="table-info" Font-Size="Smaller" />

    <Columns>
     <asp:TemplateField HeaderText="SL" HeaderStyle-HorizontalAlign="Center">
        <ItemTemplate>
            <asp:Label ID="lblSerialNo" runat="server" Text=""></asp:Label>
              <asp:HiddenField runat="server" ID="hdnCNId" Value='<%# Bind("CN_ID") %>' />
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" Width="40px" />
    </asp:TemplateField>

   <%--      <asp:TemplateField HeaderText="CN Number">
    <ItemTemplate>
        <div class="d-flex align-items-center">
            <table>
                <tr>
                    <td class="p-0">
                        <asp:TextBox ID="txtCNNumber" runat="server"
                            CssClass="form-control form-control-sm"
                            Style="width: 100px;"
                            Text='<%# Bind("CN_NUMBER") %>' ></asp:TextBox>
                      

                    </td>
                </tr>
            </table>
        </div>
    </ItemTemplate>
</asp:TemplateField>--%>
    <asp:TemplateField HeaderText="Recipient Name">
    <ItemTemplate>
        <div class="d-flex align-items-center">
            <table>
                <tr>
                    <td class="p-0">
                        <asp:TextBox ID="txtRecipientName" runat="server"
                            CssClass="form-control form-control-sm"
                            Style="width: 200px;"
                            Text='<%# Bind("CONSIGNEE_NAME") %>' ></asp:TextBox>

                    </td>
                </tr>
            </table>
        </div>
    </ItemTemplate>
</asp:TemplateField>
   <asp:TemplateField HeaderText="Recipient Address">
    <ItemTemplate>
        <div class="d-flex align-items-center">
            <table>
                <tr>
                    <td class="p-0">
                        <asp:TextBox ID="txtRecipientAddress" runat="server"
                            CssClass="form-control form-control-sm"
                            Style="width: 250px;"
                            Text='<%# Bind("CONSIGNEE_ADDRESS") %>' ></asp:TextBox>

                    </td>
                </tr>
            </table>
        </div>
    </ItemTemplate>
</asp:TemplateField>

      <asp:TemplateField HeaderText="Recipient Mobile No">
    <ItemTemplate>
        <div class="d-flex align-items-center">
            <table>
                <tr>
                    <td class="p-0">
                        <asp:TextBox ID="txtRecipientMobileNo" runat="server"
                            CssClass="form-control form-control-sm"
                            Style="width: 200px;"
                            Text='<%# Bind("CONSIGNEE_MOBILE_NO") %>' ></asp:TextBox>

                    </td>
                </tr>
            </table>
        </div>
    </ItemTemplate>
</asp:TemplateField>

            <asp:TemplateField HeaderText="Destination District">
    <ItemTemplate>
        <div class="d-flex align-items-center">
            <table>
                <tr>
                    <td class="p-0">
                        <asp:TextBox ID="txtDestinationDist" runat="server"
                            CssClass="form-control form-control-sm"
                            Style="width: 200px;"
                            Text='<%# Bind("DESTINATION_DIST_NAME") %>' ></asp:TextBox>

                        <asp:HiddenField ID="hdnDestinationDistId" runat="server" Value='<%# Bind("DESTINATION_DIST_ID") %>' />
                     
                    </td>
                </tr>
            </table>
        </div>
    </ItemTemplate>
</asp:TemplateField>
        
    <asp:TemplateField HeaderText="Destination Upozilla">
    <ItemTemplate>
        <div class="d-flex align-items-center">
            <table>
                <tr>
                    <td class="p-0">
                        <asp:TextBox ID="txtTownName" runat="server"
                            CssClass="form-control form-control-sm"
                            Style="width: 200px;"
                            Text='<%# Bind("DESTINATION_TOWN_NAME") %>' ></asp:TextBox>

                        <asp:HiddenField ID="hdnTownId" runat="server" Value='<%# Bind("DESTINATION_TOWN_ID") %>' />
                     
                    </td>
                </tr>
            </table>
        </div>
    </ItemTemplate>
</asp:TemplateField>

<asp:TemplateField HeaderText="Delete" ShowHeader="false">
    <ItemTemplate>
        <div class="d-flex align-items-center justify-content-center">
            <asp:LinkButton ID="btnDeleteRow" runat="server" CommandName="delete"
                CssClass="btn btn-sm btn-outline-danger"
                ToolTip="Delete">
                <i class="fa fa-trash"></i>
            </asp:LinkButton>
        </div>
    </ItemTemplate>
    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
</asp:TemplateField>

    </Columns>
</asp:GridView>


               <input id="Hidden1" type="hidden" runat="server" value="[]" />
               <input id="Hidden2" type="hidden" runat="server" value="[]" />
              </ContentTemplate>
              <Triggers>
                  <asp:AsyncPostBackTrigger ControlID="btnNewRow" EventName="Click" />
              </Triggers>
             </asp:UpdatePanel>
                  <div class="d-flex justify-content-start align-items-center mt-2 border-top pt-2">
    <asp:LinkButton runat="server" ID="btnNewRow" OnClick="btnNewRow_Click"
        CssClass="btn btn-sm btn-primary me-2" Text="<i class='fa fa-plus'></i> New Row"></asp:LinkButton>

    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="300">
        <ProgressTemplate>
            <asp:Image ID="imgProgress2" runat="server" ImageUrl="~/image/loader.gif" />
        </ProgressTemplate>
    </asp:UpdateProgress>
</div>

        </div>
  

    </div>
   </div>
  
    <div class="card-footer">
     <div class="row">
      <div class="col-md-12">
       <asp:LinkButton runat="server" ID="btnAddNew"  CssClass="btn btn-primary" Text="<i class='fa fa-plus'></i> Add New"></asp:LinkButton>
       <asp:LinkButton runat="server" ID="btnSave" OnClick="btnSave_Click" CssClass="btn btn-success" Text="<i class='fas fa-save'></i> Save"></asp:LinkButton>
       <asp:LinkButton runat="server" ID="btnEdit" OnClick="btnEdit_Click" CssClass="btn btn-primary" Text="<i class='fas fa-edit'></i> Edit"></asp:LinkButton>
     
      
      </div>
     </div>

      

    </div>
   </div>
      
     
     </div>
    </div>
</asp:Content>
