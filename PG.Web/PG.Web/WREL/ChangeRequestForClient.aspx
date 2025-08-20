<%@ Page Title="" Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="ChangeRequestForClient.aspx.cs" Inherits="PG.Web.WREL.ChangeRequestForClient" %>

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


  
        var CNListServiceLink = '<%=this.CNListServiceLink%>';
        var AgentListServiceLink = '<%=this.AgentListServiceLink%>';

        var txtCnNumber = '<%=txtCnNumber.ClientID%>';
        var txtClientName = '<%=txtClientName.ClientID%>';
        var txtConsigneeName = '<%=txtConsigneeName.ClientID%>';
        var txtConsigneeMobileNo = '<%=txtConsigneeMobileNo.ClientID%>';
        var txtConsigneeAddress = '<%=txtConsigneeAddress.ClientID%>';
        var hdnCnId = '<%=hdnCnId.ClientID%>';
        var hdnSelectedClientId = '<%=hdnSelectedClientId.ClientID%>';
        
     


        $(document).ready(function () {

            //var pageInstance = Sys.WebForms.PageRequestManager.getInstance();

            //pageInstance.add_pageLoaded(function (sender, args) {
            //    var panels = args.get_panelsUpdated();
            //    for (i = 0; i < panels.length; i++) {

            //        if (panels[i].id == gridUpdatePanelIDDet) {
            //            //bindDestinationDistList(gridViewIDDet);
            //            bindDestinationTownList(gridViewIDDet);
            //        }

            //    }

            //});


            if ($('#' + txtCnNumber).is(':visible')) {

                bindCNList();

            }


            //bindDestinationDistList(gridViewIDDet);
            //bindDestinationTownList(gridViewIDDet);



        });
     
        function bindCNList() {
            var cgColumns = [
                             { 'columnName': 'cnnumber', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'CN Number' }
                            , { 'columnName': 'clientname', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Client' }
                            , { 'columnName': 'consigneename', 'width': '200', 'align': 'left', 'highlight': 4, 'label': 'Consignee Name' }

            ];
            var serviceURL = CNListServiceLink + "?isterm=1&includeempty=0&hasitem=1&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;

            serviceURL += "&ispaging=0";
            var groupIDElem = $('#' + txtCnNumber);
            
            $('#' + txtCnNumber).click(function (e) {
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
                    var clientId = $("#<%= hdnClientId.ClientID %>").val(); 
                    //var newServiceURL = serviceURL;
                    var newServiceURL = serviceURL + "&clientId=" + clientId; 
                   
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
                        $('#' + hdnCnId).val(ui.item.cnid);
                        $('#' + txtCnNumber).val(ui.item.cnnumber);
                        $('#' + txtClientName).val(ui.item.clientname);
                        $('#' + txtConsigneeName).val(ui.item.consigneename);
                        $('#' + txtConsigneeMobileNo).val(ui.item.consigneemobileno);
                        $('#' + txtConsigneeAddress).val(ui.item.consigneeaddress);
                        $('#' + hdnSelectedClientId).val(ui.item.clientid);
                        
                        //$("[id*=btnLoad]").click();
                    }
                    return false;
                },

                lc: ''
            });


            $(groupIDElem).blur(function () {
                var self = this;

                var groupID = $(groupIDElem).val();
                if (groupID == '') {
                    $('#' + txtCnNumber).val('');
                    $('#' + hdnCnId).val('0');
                    $('#' + txtClientName).val('');
                    $('#' + txtConsigneeName).val('');
                    $('#' + txtConsigneeMobileNo).val('');
                    $('#' + txtConsigneeAddress).val('');
                    
                }
            });
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
                    width: 450,
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
             <h5 class="card-title">Information Change Request</h5>
           <%--  <a class="btn btn-primary p-1"> <i class="fas fa-list"></i> Parcel List </a>--%>
         </div>

       </div>
      <div class="card-body">
            <asp:HiddenField ID="hdnREQUEST_ID" runat="server" Value="0" />
           <asp:HiddenField ID="hdnClientId" runat="server" Value="0" />

              <div class="row mb-0">

               <div class="col-md-6">
                  <div class="form-group row mb-0">
                    <label for="name" class="col-sm-4 col-form-label-sm">CN Number :</label>
                    <div class="col-sm-6">
                      <asp:TextBox runat="server"  class="form-control form-control-sm"  ID="txtCnNumber" placeholder="Select" ></asp:TextBox> 
                           <asp:HiddenField runat="server" ID="hdnCnId" Value="0" /> 
                          <asp:HiddenField runat="server" ID="hdnSelectedClientId" Value="0" /> 
                    </div>
                  </div>

                </div>
               

             </div>

          <div>
             <div class="row mb-0">

               <div class="col-md-6">
                  <div class="form-group row mb-0">
                    <label for="name" class="col-sm-4 col-form-label-sm">Client Name :</label>
                    <div class="col-sm-6">
                      <asp:TextBox runat="server"  class="form-control form-control-sm"  ID="txtClientName" placeholder="" ></asp:TextBox> 
                         
                    </div>
                  </div>

                </div>

             </div>

              <div class="row mb-0">

               <div class="col-md-6">
                  <div class="form-group row mb-0">
                    <label for="name" class="col-sm-4 col-form-label-sm">Consignee Name :</label>
                    <div class="col-sm-6">
                      <asp:TextBox runat="server"  class="form-control form-control-sm"  ID="txtConsigneeName" placeholder="" ></asp:TextBox> 
                         
                    </div>
                  </div>

                </div>

             </div>

             <div class="row mb-0">

               <div class="col-md-6">
                  <div class="form-group row mb-0">
                    <label for="name" class="col-sm-4 col-form-label-sm">Consignee Mobile No :</label>
                    <div class="col-sm-6">
                      <asp:TextBox runat="server"  class="form-control form-control-sm"  ID="txtConsigneeMobileNo" placeholder="" ></asp:TextBox> 
                         
                    </div>
                  </div>

                </div>

             </div>

                <div class="row mb-2">

               <div class="col-md-6">
                  <div class="form-group row mb-0">
                    <label for="name" class="col-sm-4 col-form-label-sm">Consignee Address :</label>
                    <div class="col-sm-6">
                      <asp:TextBox runat="server"  class="form-control form-control-sm"  ID="txtConsigneeAddress" placeholder=""  TextMode="MultiLine" Height="50px"></asp:TextBox> 
                         
                    </div>
                  </div>

                </div>

             </div>

             <div class="row mb-0">

               <div class="col-md-6">
                  <div class="form-group row mb-0">
                    <label for="name" class="col-sm-4 col-form-label-sm">Request New Address :</label>
                    <div class="col-sm-6">
                      <asp:TextBox runat="server"  class="form-control form-control-sm"  ID="txtNewAddress" placeholder="" TextMode="MultiLine" Height="50px" ></asp:TextBox> 
                         
                    </div>
                  </div>

                </div>

             </div>

          </div>


      </div>

  
    <div class="card-footer">
     <div class="row">
      <div class="col-md-12">
     <%--  <asp:LinkButton runat="server" ID="btnAddNew"  CssClass="btn btn-primary" Text="<i class='fa fa-plus'></i> Add New"></asp:LinkButton>--%>
       <asp:LinkButton runat="server" ID="btnSave" OnClick="btnSave_Click" CssClass="btn btn-primary" Text="<i class='fas fa-save'></i> Save"></asp:LinkButton>
       <asp:LinkButton runat="server" ID="btnEdit" OnClick="btnEdit_Click" CssClass="btn btn-primary" Text="<i class='fas fa-edit'></i> Edit"></asp:LinkButton>
     
      
      </div>
     </div>

      

    </div>
   </div>
      
     
     </div>
    </div>
</asp:Content>
