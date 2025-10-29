<%@ Page Title="" Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="SingleParcelCreation.aspx.cs" Inherits="PG.Web.WREL.SingleParcelCreation" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"   Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

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
        var DepartmentListbyClientIDServiceLink = '<%=this.DepartmentListbyClientIDServiceLink%>';
        var ItemListServiceLink = '<%=this.ItemListServiceLink%>';
        var DistanceTypeServiceLink = '<%=this.DistanceTypeServiceLink%>';
      <%--  var txtAggrementDtl = '<%=txtAggrementDtl.ClientID%>';--%>
        var hdnAggrementDtlId = '<%=hdnAggrementDtlId.ClientID%>';
        var txtItemName = '<%=txtItemName.ClientID%>';
        var hdnItemId = '<%=hdnItemId.ClientID%>';
        var txtServiceCharge = '<%=txtServiceCharge.ClientID%>';
        var txtSLADays = '<%=txtSLADays.ClientID%>';
        var txtRate = '<%=txtRate.ClientID%>';

        var txtDistanceType = '<%=txtDistanceType.ClientID%>';
        var hdnDistanceTypeId = '<%=hdnDistanceTypeId.ClientID%>';

        var txtClientName = '<%=txtClientName.ClientID%>';
        var hdnClientId = '<%=hdnClientId.ClientID%>';

        var txtHubName = '<%=txtHubName.ClientID%>';
        var hdnHubId = '<%=hdnHubId.ClientID%>';

        var txtDepartment = '<%=txtDepartment.ClientID%>';
        var hdnDeptID = '<%=hdnDeptID.ClientID%>';



        $(document).ready(function () {

            //var pageInstance = Sys.WebForms.PageRequestManager.getInstance();

            //pageInstance.add_pageLoaded(function (sender, args) {
            //    var panels = args.get_panelsUpdated();
            //    for (i = 0; i < panels.length; i++) {

            //        if (panels[i].id == gridUpdatePanelIDDet) {
            //            bindDestinationDistList(gridViewIDDet);
            //            bindDestinationTownList(gridViewIDDet);
            //        }

            //    }

            //});


            if ($('#' + txtClientName).is(':visible')) {

                bindClientNameList();

            }


            if ($('#' + txtHubName).is(':visible')) {

                bindHubList();

            }


            if ($('#' + txtDepartment).is(':visible')) {

                bindDepartmentListByClientID();

            }

            if ($('#' + txtItemName).is(':visible')) {

                bindItemList();

            }

            if ($('#' + txtDistanceType).is(':visible')) {

                bindDistanceType();

            }

            

            //bindDestinationDistList(gridViewIDDet);
            //bindDestinationTownList(gridViewIDDet);



        });

        function bindClientNameList() {
            var cgColumns = [
                             { 'columnName': 'clientname', 'width': '250', 'align': 'left', 'highlight': 4, 'label': 'Client Name' }
                            , { 'columnName': 'mobile', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Mobile' }

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
                width: 400,
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

        function bindDepartmentListByClientID() {
            var cgColumns = [
                             { 'columnName': 'deptname', 'width': '200', 'align': 'left', 'highlight': 4, 'label': 'Dept Name' }


            ];
            var serviceURL = DepartmentListbyClientIDServiceLink + "?isterm=1&includeempty=0&hasitem=1&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;

            serviceURL += "&ispaging=0";
            var groupIDElem = $('#' + txtDepartment);

            $('#' + txtDepartment).click(function (e) {
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
                    var clientid = $('#' + hdnClientId).val();
                    var newServiceURL = serviceURL + " &clientid=" + clientid;
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
                        $('#' + hdnDeptID).val(ui.item.deptid);
                        $('#' + txtDepartment).val(ui.item.deptname);
                    }
                    return false;
                },

                lc: ''
            });


            $(groupIDElem).blur(function () {
                var self = this;

                var groupID = $(groupIDElem).val();
                if (groupID == '') {
                    $('#' + txtDepartment).val('');
                    $('#' + hdnDeptID).val('0');
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

        function bindItemList() {
            var cgColumns = [
                             { 'columnName': 'itemname', 'width': '200', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                            , { 'columnName': 'agreementdate', 'width': '120', 'align': 'left', 'highlight': 4, 'label': 'Agreement Date' }
                            , { 'columnName': 'serviceamt', 'width': '120', 'align': 'left', 'highlight': 4, 'label': 'Service Amount' }
                            , { 'columnName': 'sladays', 'width': '30', 'align': 'left', 'highlight': 4, 'label': 'SLA' }

            ];
            var serviceURL = ItemListServiceLink + "?isterm=1&includeempty=0&hasitem=1&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;

            serviceURL += "&ispaging=1";
            var groupIDElem = $('#' + txtItemName);

            $('#' + txtItemName).click(function (e) {
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
                width: 500,
                url: serviceURL,
                search: function (event, ui) {
                    var clientid = $('#' + hdnClientId).val();
                    var distancetypeid = $('#' + hdnDistanceTypeId).val();
                    var newServiceURL = serviceURL + "&clientid=" + clientid + "&distancetypeid=" +distancetypeid;
                    $(this).combogrid("option", "url", newServiceURL);


                },
                select: function (event, ui) {
                    if (!ui.item) {
                        event.preventDefault();
                        return false;
                    }

                    if (ui.item.itemid == '') {
                        event.preventDefault();
                        return false;
                    }
                    else {
                        $('#' + hdnItemId).val(ui.item.itemid);
                        $('#' + txtItemName).val(ui.item.itemname);
                        $('#' + txtRate).val(ui.item.serviceamt);
                        $('#' + txtSLADays).val(ui.item.sladays);
                        $('#' + hdnAggrementDtlId).val(ui.item.agrdtlid);
                    }
                    return false;
                },

                lc: ''
            });


            $(groupIDElem).blur(function () {
                var self = this;

                var groupID = $(groupIDElem).val();
                if (groupID == '') {
                    $('#' + txtItemName).val('');
                    $('#' + hdnItemId).val('0');
                    $('#' + txtRate).val('');
                    $('#' + txtSLADays).val('');
                    $('#' + hdnAggrementDtlId).val('0');
                }
            });
        }

        function bindDistanceType() {
            var cgColumns = [
                             { 'columnName': 'distancetypename', 'width': '300', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                            //, { 'columnName': 'distname', 'width': '200', 'align': 'left', 'highlight': 4, 'label': 'Name' }

            ];
            var serviceURL = DistanceTypeServiceLink + "?isterm=1&includeempty=0&hasitem=1&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;

            serviceURL += "&ispaging=0";
            var groupIDElem = $('#' + txtDistanceType);

            $('#' + txtDistanceType).click(function (e) {
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
                width: 450,
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

                    if (ui.item.hdnDistanceTypeId == '') {
                        event.preventDefault();
                        return false;
                    }
                    else {
                        $('#' + hdnDistanceTypeId).val(ui.item.distancetypeid);
                        $('#' + txtDistanceType).val(ui.item.distancetypename);
                    }
                    return false;
                },

                lc: ''
            });


            $(groupIDElem).blur(function () {
                var self = this;

                var groupID = $(groupIDElem).val();
                if (groupID == '') {
                    $('#' + txtDistanceType).val('');
                    $('#' + hdnDistanceTypeId).val('0');
                }
            });
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

        function calculateTotal() {
            var qty = parseFloat(document.getElementById('<%= txtQuantity.ClientID %>').value) || 0;
            var rate = parseFloat(document.getElementById('<%= txtRate.ClientID %>').value) || 0;
            var serviceamt = parseFloat(document.getElementById('<%= txtServiceCharge.ClientID %>').value) || 0;

                var totaltk = qty * rate;
                var total = totaltk + serviceamt;
            document.getElementById('<%= txtAmountTk.ClientID %>').value = totaltk.toFixed(2);
            document.getElementById('<%= txtTotalAmount.ClientID %>').value = total.toFixed(2);

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
          <asp:HiddenField ID="hdnAggrementDtlId" runat="server" Value="0" />

              <div class="row mb-0">

                <div class="col-md-4">
                  <div class="form-group row mb-0">
                    <label for="name" class="col-sm-5 col-form-label-sm">CN Number :</label>
                    <div class="col-sm-7">
                       
                       <asp:TextBox ID="txtCNNo" runat="server" CssClass="form-control form-control-sm" ></asp:TextBox>

                    </div>
                  </div>
                </div>
               <div class="col-md-4">
                  <div class="form-group row mb-0">
                    <label for="name" class="col-sm-5 col-form-label-sm required">Client :</label>
                    <div class="col-sm-7">
                      <asp:TextBox runat="server"  class="form-control form-control-sm"  ID="txtClientName" placeholder="Select" ></asp:TextBox> 
                           <asp:HiddenField runat="server" ID="hdnClientId" Value="0" /> 
                    </div>
                  </div>

                </div>
               
       <div class="col-md-4">
                  <div class="form-group row mb-0">
                    <label for="name" class="col-sm-5 col-form-label-sm required" >Department :</label>
                    <div class="col-sm-7">
                      <asp:TextBox runat="server"  class="form-control form-control-sm"  ID="txtDepartment" placeholder="Select" ></asp:TextBox> 
                           <asp:HiddenField runat="server" ID="hdnDeptID" Value="0" /> 
                    </div>
                  </div>

                </div>
             
                

             </div>

            <div class="row mb-0">
                <div class="col-md-4">
                  <div class="form-group row mb-0">
                    <label for="name" class="col-sm-5 col-form-label-sm">Hub :</label>
                    <div class="col-sm-7">
                      <asp:TextBox runat="server"  class="form-control form-control-sm"  ID="txtHubName" placeholder="Enter Hub Name" ></asp:TextBox> 
                       <asp:HiddenField runat="server" ID="hdnHubId" Value="0" /> 
                    </div>
                  </div>
                </div>
                  <div class="col-md-4">
                  <div class="form-group row mb-0">
                    <label for="name" class="col-sm-5 col-form-label-sm">Recipient Name :</label>
                    <div class="col-sm-7">
                      <asp:TextBox runat="server"  class="form-control form-control-sm"  ID="txtRecipientName" placeholder="Enter Recipient Name" ></asp:TextBox> 
                       <asp:HiddenField runat="server" ID="HiddenField1" Value="0" /> 
                    </div>
                  </div>

                </div>

                  <div class="col-md-4">
                  <div class="form-group row mb-0">
                    <label for="name" class="col-sm-5 col-form-label-sm">Recipient Address :</label>
                    <div class="col-sm-7">
                      <asp:TextBox runat="server"  class="form-control form-control-sm"  ID="txtRecipientAddress" placeholder="Enter Recipient Address"  ></asp:TextBox> 
                       
                    </div>
                  </div>

                </div>

               
               

                </div>

            <div class="row mb-0">
                   <div class="col-md-4">
                  <div class="form-group row mb-0">
                    <label for="name" class="col-sm-5 col-form-label-sm">Mobile No :</label>
                    <div class="col-sm-7">
                      <asp:TextBox runat="server"  class="form-control form-control-sm"  ID="txtRecipientMobileNo" placeholder="Enter Mobile No"  ></asp:TextBox> 
                       
                    </div>
                  </div>

                </div>
                   <div class="col-md-4">
                  <div class="form-group row mb-0">
                    <label for="name" class="col-sm-5 col-form-label-sm required">Distance Type :</label>
                    <div class="col-sm-7">
                      <asp:TextBox runat="server"  class="form-control form-control-sm"  ID="txtDistanceType" placeholder="Select" ></asp:TextBox> 
                       <asp:HiddenField runat="server" ID="hdnDistanceTypeId" Value="0" /> 
                    </div>
                  </div>

                </div>
                  <div class="col-md-4">
                  <div class="form-group row mb-0">
                    <label for="name" class="col-sm-5 col-form-label-sm required">Item :</label>
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
                    <label for="name" class="col-sm-5 col-form-label-sm">SLA Days :</label>
                    <div class="col-sm-7">
                      <asp:TextBox runat="server"  class="form-control form-control-sm"  ID="txtSLADays" placeholder="Enter SLA Days"  ></asp:TextBox> 
                       
                    </div>
                  </div>

                </div>
                   <div class="col-md-4">
                  <div class="form-group row mb-0">
                    <label for="name" class="col-sm-5 col-form-label-sm">Weight :</label>
                    <div class="col-sm-7">
                      <asp:TextBox runat="server"  class="form-control form-control-sm"  ID="txtWeight" placeholder="Enter Item Weight"  ></asp:TextBox> 
                       
                    </div>
                  </div>

                </div>

                  <div class="col-md-4">
                  <div class="form-group row mb-0">
                    <label for="name" class="col-sm-5 col-form-label-sm required">Quantity :</label>
                    <div class="col-sm-7">
                      <asp:TextBox runat="server"  class="form-control form-control-sm"  ID="txtQuantity" onkeyup="calculateTotal()" onkeypress="return isNumberKey(event,this);"  placeholder="Enter Item Quantity"  ></asp:TextBox> 
                       
                    </div>
                  </div>

                </div>

                </div>

            <div class="row mb-0">

                   <div class="col-md-4">
                  <div class="form-group row mb-0">
                    <label for="name" class="col-sm-5 col-form-label-sm required">Rate :</label>
                    <div class="col-sm-7">
                      <asp:TextBox runat="server"  class="form-control form-control-sm"  ID="txtRate" placeholder="Enter Rate" onkeyup="calculateTotal()" onkeypress="return isNumberKey(event,this);" ></asp:TextBox> 
                    </div>
                  </div>

                </div>

                   <div class="col-md-4">
                  <div class="form-group row mb-0">
                    <label for="name" class="col-sm-5 col-form-label-sm">Taka :</label>
                    <div class="col-sm-7">
                      <asp:TextBox runat="server"  class="form-control form-control-sm"  ID="txtAmountTk" placeholder="Amount"  ></asp:TextBox> 
                       
                    </div>
                  </div>

                </div>
               <div class="col-md-4">
                  <div class="form-group row mb-0">
                    <label for="name" class="col-sm-5 col-form-label-sm">Service Charge :</label>
                    <div class="col-sm-7">
                      <asp:TextBox runat="server"  class="form-control form-control-sm"  ID="txtServiceCharge" onkeyup="calculateTotal()" placeholder="Enter Service Charge"  ></asp:TextBox> 
                       
                    </div>
                  </div>

                </div>
               
                </div>
           <div class="row mb-0">
                <div class="col-md-4">
                  <div class="form-group row mb-0">
                    <label for="name" class="col-sm-5 col-form-label-sm">Total Amount :</label>
                    <div class="col-sm-7">
                      <asp:TextBox runat="server"  class="form-control form-control-sm"  ID="txtTotalAmount" placeholder="Total Amount" ></asp:TextBox> 
                    </div>
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

