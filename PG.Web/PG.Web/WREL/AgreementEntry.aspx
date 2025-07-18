<%@ Page Title="" Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="AgreementEntry.aspx.cs" Inherits="PG.Web.WREL.AgreementEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
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


        var DistrictListServiceLink = '<%=this.DistrictListServiceLink%>';
        var TownListServiceLink = '<%=this.TownListServiceLink%>';
        var RouteListServiceLink = '<%=this.RouteListServiceLink%>';
        
      
        var gridUpdatePanelIDDet = '<%=UpdatePanel1.ClientID%>';
        var gridViewIDDet = '<%=GridView1.ClientID%>';
        <%--var txtStartingDist = '<%=txtStartingDist.ClientID%>';
        var hdnStartingDistId = '<%=hdnStartingDistId.ClientID%>';
        var txtDestinationDist = '<%=txtDestinationDist.ClientID%>';
        var hdnDestDistId = '<%=hdnDestDistId.ClientID%>';

        var txtDestinationTown = '<%=txtDestinationTown.ClientID%>';
        var hdnDestTownId = '<%=hdnDestTownId.ClientID%>';

        var txtRoute = '<%=txtRoute.ClientID%>';
        var hdnRouteId = '<%=hdnRouteId.ClientID%>';

        var txtManagerName = '<%=txtManagerName.ClientID%>';--%>
      
        
        

        $(document).ready(function () {

            //var pageInstance = Sys.WebForms.PageRequestManager.getInstance();

            //pageInstance.add_pageLoaded(function (sender, args) {
            //    var panels = args.get_panelsUpdated();
            //    for (i = 0; i < panels.length; i++) {

            //        if (panels[i].id == gridUpdatePanelIDDet) {
            //            bindItemList(gridViewIDDet);
            //        }

            //    }

            //});


            if ($('#' + txtStartingDist).is(':visible')) {

                bindStartingDistrictList();

            }

            if ($('#' + txtDestinationDist).is(':visible')) {

                bindDestinationDistrictList();

            }

            if ($('#' + txtDestinationTown).is(':visible')) {

                bindDestinationTownList();

            }

            if ($('#' + txtRoute).is(':visible')) {

                bindRouteList();

            }





        });
     
        function bindStartingDistrictList() {
            var cgColumns = [
                             { 'columnName': 'distcode', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Code' }
                            , { 'columnName': 'distname', 'width': '200', 'align': 'left', 'highlight': 4, 'label': 'Name' }

            ];
            var serviceURL = DistrictListServiceLink + "?isterm=1&includeempty=0&hasitem=1&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;

            serviceURL += "&ispaging=0";
            var groupIDElem = $('#' + txtStartingDist);

            $('#' + txtStartingDist).click(function (e) {
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
                        $('#' + hdnStartingDistId).val(ui.item.distid);
                        $('#' + txtStartingDist).val(ui.item.distname);
                    }
                    return false;
                },

                lc: ''
            });


            $(groupIDElem).blur(function () {
                var self = this;

                var groupID = $(groupIDElem).val();
                if (groupID == '') {
                    $('#' + txtStartingDist).val('');
                    $('#' + hdnStartingDistId).val('0');
                }
            });
        }

        function bindDestinationDistrictList() {
            var cgColumns = [
                             { 'columnName': 'distcode', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Code' }
                            , { 'columnName': 'distname', 'width': '200', 'align': 'left', 'highlight': 4, 'label': 'Name' }

            ];
            var serviceURL = DistrictListServiceLink + "?isterm=1&includeempty=0&hasitem=1&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;

            serviceURL += "&ispaging=0";
            var groupIDElem = $('#' + txtDestinationDist);

            $('#' + txtDestinationDist).click(function (e) {
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
                        $('#' + hdnDestDistId).val(ui.item.distid);
                        $('#' + txtDestinationDist).val(ui.item.distname);
                    }
                    return false;
                },

                lc: ''
            });


            $(groupIDElem).blur(function () {
                var self = this;

                var groupID = $(groupIDElem).val();
                if (groupID == '') {
                    $('#' + txtDestinationDist).val('');
                    $('#' + hdnDestDistId).val('0');
                }
            });
        }

        function bindDestinationTownList() {
            var cgColumns = [
                             { 'columnName': 'townname', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Code' }
                            , { 'columnName': 'distname', 'width': '200', 'align': 'left', 'highlight': 4, 'label': 'Name' }

            ];
            var serviceURL = TownListServiceLink + "?isterm=1&includeempty=0&hasitem=1&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;

            serviceURL += "&ispaging=0";
            var groupIDElem = $('#' + txtDestinationTown);

            $('#' + txtDestinationTown).click(function (e) {
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
                        $('#' + hdnDestTownId).val(ui.item.townid);
                        $('#' + txtDestinationTown).val(ui.item.townname);
                    }
                    return false;
                },

                lc: ''
            });


            $(groupIDElem).blur(function () {
                var self = this;

                var groupID = $(groupIDElem).val();
                if (groupID == '') {
                    $('#' + txtDestinationTown).val('');
                    $('#' + hdnDestTownId).val('0');
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
             <h5 class="card-title">Agreement Entry</h5>
             <a class="btn btn-primary p-1"> <i class="fas fa-list"></i> Agreement List </a>
         </div>

       </div>
      <div class="card-body">
            <asp:HiddenField ID="hdnCARGO_ID" runat="server" Value="0" />
            <asp:HiddenField ID="hdnReservationId" runat="server" Value="0" />

              <div class="row mb-0">

                <div class="col-md-4">
                  <div class="form-group row mb-0">
                    <label for="name" class="col-sm-6 col-form-label-sm">Agreement Name :</label>
                    <div class="col-sm-6">
                       
                       <asp:TextBox ID="txtAgreementName" runat="server" CssClass="form-control form-control-sm" ></asp:TextBox>

                    </div>
                  </div>
                </div>

                  <div class="col-md-4">
                  <div class="form-group row mb-0">
                    <label for="name" class="col-sm-6 col-form-label-sm">Agreement Date :</label>
                    <div class="col-sm-6">
                        <table>
                            <tr>
                                <td>
                                     <asp:TextBox ID="txtAgreementdt" runat="server" CssClass="TextBoxnew textDate dateParse" Width="130px" ></asp:TextBox>

                                </td>
                            </tr>
                        </table>
                     
                    </div>
                  </div>
                </div>

               

             </div>

            <div class="row mb-0">

                <div class="col-md-4">
                  <div class="form-group row mb-0">
                    <label for="name" class="col-sm-6 col-form-label-sm">Client :</label>
                    <div class="col-sm-6">
                      <asp:TextBox runat="server"  class="form-control form-control-sm"  ID="txtClientName" placeholder="Select" ></asp:TextBox> 
                       <asp:HiddenField runat="server" ID="hdnClientId" Value="0" /> 
                    </div>
                  </div>

                </div>

                 <div class="col-md-4">
                  <div class="form-group row mb-0">
                    <label for="name" class="col-sm-6 col-form-label-sm">Department :</label>
                    <div class="col-sm-6">
                      <asp:TextBox runat="server"  class="form-control form-control-sm"  ID="txtDepartment" placeholder="Select"  ></asp:TextBox> 
                         <asp:HiddenField runat="server" ID="hdnDepartmentId" Value="0" /> 
                    </div>
                  </div>

                </div>

                 

                </div>


           <div class="row mb-0">

                 <div class="col-md-4">
                  <div class="form-group row mb-0">
                    <label for="name" class="col-sm-6 col-form-label-sm">Agreement Start Date :</label>
                    <div class="col-sm-6">
                        <table>
                            <tr>
                                <td>
                                     <asp:TextBox ID="txtAgreementStDate" runat="server" CssClass="TextBoxnew textDate dateParse" Width="130px" ></asp:TextBox>

                                </td>
                            </tr>
                        </table>
                     
                    </div>
                  </div>
                </div>

                  <div class="col-md-4">
                  <div class="form-group row mb-0">
                    <label for="name" class="col-sm-6 col-form-label-sm">Agreement End Date :</label>
                    <div class="col-sm-6">
                        <table>
                            <tr>
                                <td>
                                     <asp:TextBox ID="txtAgrEndDate" runat="server" CssClass="TextBoxnew textDate dateParse" Width="130px" ></asp:TextBox>

                                </td>
                            </tr>
                        </table>
                     
                    </div>
                  </div>
                </div>
              

             </div>

        <div class="row mb-0">

            <div class="col-md-4">
                  <div class="form-group row mb-0">
                    <label for="name" class="col-sm-6 col-form-label-sm">Description :</label>
                    <div class="col-sm-6">
                       
                       <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control form-control-sm" ></asp:TextBox>

                    </div>
                  </div>
                </div>
             <div class="col-md-4">
                  <div class="form-group row mb-0">
                    <label for="name" class="col-sm-6 col-form-label-sm">Status :</label>
                    <div class="col-sm-6">
                      <asp:DropDownList runat="server"  class="form-control form-control-sm"  ID="ddlStatus" >
                          <asp:ListItem Text="Active" Value="Y" Selected="True"></asp:ListItem>
                           <asp:ListItem Text="Inactive" Value="N"></asp:ListItem>
                      </asp:DropDownList> 
                    </div>
                  </div>

                </div>
                 
              

             </div>
      

      </div>
    <div class="row">
    <div class="col-md-12">
        <div class="card">
           <div class="card-header mb-0 p-1">
              <strong>Agreement Details :</strong>

           </div>

            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
              <ContentTemplate>
             <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" ShowHeader="true"
    CssClass="table table-sm table-striped table-bordered w-auto"  
    DataKeyNames="AGR_ID" EnableModelValidation="True" ClientIDMode="AutoID"
    OnRowDataBound="GridView1_RowDataBound" OnRowCommand="GridView1_RowCommand" OnRowDeleting="GridView1_RowDeleting">
    
    <HeaderStyle CssClass="table-info" Font-Size="Smaller" />

    <Columns>
        <asp:TemplateField HeaderText="Item Name">
            <ItemTemplate>
                <div class="d-flex">
                    <asp:TextBox ID="txtItemName" runat="server" CssClass="form-control form-control-sm" 
                        Text='<%# Bind("ITEM_NAME") %>' Style="width: 200px;"></asp:TextBox>
                    <asp:HiddenField ID="hdnItemID" runat="server" Value='<%# Bind("ITEM_ID") %>' />
                    <asp:HiddenField ID="hdnAGR_DETAIL_ID" runat="server" Value='<%# Bind("AGR_DETAIL_ID") %>' />
                </div>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Distance Type">
            <ItemTemplate>
                <div class="d-flex">
                    <asp:TextBox ID="txtDistanceType" runat="server" CssClass="form-control form-control-sm" 
                        Text='<%# Bind("DISTANCE_TYPE_ID") %>' Style="width: 150px;"></asp:TextBox>
                    
                </div>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Service Amount">
            <ItemTemplate>
                <div class="d-flex">
                    <asp:TextBox ID="txtServiceAmt" runat="server" CssClass="form-control form-control-sm" 
                        Text='<%# Bind("SERVICE_AMOUNT") %>' Style="width: 100px;"></asp:TextBox>
                    
                </div>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Return Price">
            <ItemTemplate>
                <div class="d-flex">
                    <asp:TextBox ID="txtReturnPrice" runat="server" CssClass="form-control form-control-sm" 
                        Text='<%# Bind("RETURN_PRICE") %>' Style="width: 100px;"></asp:TextBox>
                    
                </div>
            </ItemTemplate>
        </asp:TemplateField>

         <asp:TemplateField HeaderText="REMARKS">
            <ItemTemplate>
                <div class="d-flex">
                    <asp:TextBox ID="txtREMARKS" runat="server" CssClass="form-control form-control-sm" 
                        Text='<%# Bind("REMARKS") %>' Style="width: 150px;"></asp:TextBox>
                    
                </div>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Delete" ShowHeader="false">
            <ItemTemplate>
                <asp:LinkButton ID="btnDeleteRow" runat="server" CommandName="delete"
                    CssClass="btn btn-sm btn-outline-danger ms-2" ToolTip="Delete">
                    <i class="fa fa-trash"></i>
                </asp:LinkButton>
            </ItemTemplate>
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
       <asp:LinkButton runat="server" ID="btnSave" OnClick="btnSave_Click" CssClass="btn btn-primary" Text="<i class='fas fa-save'></i> Save"></asp:LinkButton>
       <asp:LinkButton runat="server" ID="btnEdit" OnClick="btnEdit_Click" CssClass="btn btn-primary" Text="<i class='fas fa-edit'></i> Edit"></asp:LinkButton>
     
      
      </div>
     </div>

      

    </div>
   </div>
      
     
     </div>
    </div>
</asp:Content>
