<%@ Page Title="" Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="Resarvation_Entry.aspx.cs" Inherits="PG.Web.HMS.Resarvation_Entry" %>

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


        var CountryListServiceLink = '<%=this.CountryListServiceLink%>';
      
        var gridUpdatePanelIDDet = '<%=UpdatePanel1.ClientID%>';
        var gridViewIDDet = '<%=GridView1.ClientID%>';
        var txtCountry = '<%=txtCountry.ClientID%>';
        var hdnCountryId = '<%=hdnCountryId.ClientID%>';

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


            if ($('#' + txtCountry).is(':visible')) {

                bindCountryList();

            }





        });
     
        function bindCountryList() {
            var cgColumns = [
                             { 'columnName': 'countrycode', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Code' }
                            , { 'columnName': 'countryname', 'width': '200', 'align': 'left', 'highlight': 4, 'label': 'Name' }

            ];
            var serviceURL = CountryListServiceLink + "?isterm=1&includeempty=0&hasitem=1&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;

            serviceURL += "&ispaging=1";
            var groupIDElem = $('#' + txtCountry);

            $('#' + txtCountry).click(function (e) {
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
                        $('#' + hdnCountryId).val(ui.item.countryid);
                        $('#' + txtCountry).val(ui.item.countryname);
                    }
                    return false;
                },

                lc: ''
            });


            $(groupIDElem).blur(function () {
                var self = this;

                var groupID = $(groupIDElem).val();
                if (groupID == '') {
                    $('#' + txtCountry).val('');
                    $('#' + hdnCountryId).val('0');
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
             <h5 class="card-title">Resarvation Entry</h5>
             <a class="btn btn-primary p-1"> <i class="fas fa-list"></i> Resarvation List </a>
         </div>

       </div>
      <div class="card-body">
            <asp:HiddenField ID="hdnGuestId" runat="server" Value="0" />
            <asp:HiddenField ID="hdnReservationId" runat="server" Value="0" />

              <div class="row mb-0">

                <div class="col-md-4">
                  <div class="form-group row mb-0">
                    <label for="name" class="col-sm-4 col-form-label-sm">Check In :</label>
                    <div class="col-sm-8">
                        <table>
                            <tr>
                                <td>
                                     <asp:TextBox ID="txtCheckInDate" runat="server" CssClass="TextBoxnew textDate dateParse" ></asp:TextBox>

                                </td>
                            </tr>
                        </table>
                     
                    </div>
                  </div>
                </div>

                  <div class="col-md-4">
                  <div class="form-group row mb-0">
                    <label for="name" class="col-sm-4 col-form-label-sm">Check Out :</label>
                    <div class="col-sm-8">
                        <table>
                            <tr>
                                <td>
                                     <asp:TextBox ID="txtCheckOutDate" runat="server" CssClass="TextBoxnew textDate dateParse" ></asp:TextBox>

                                </td>
                            </tr>
                        </table>
                     
                    </div>
                  </div>
                </div>

                 <div class="col-md-4">
                  <div class="form-group row mb-0">
                    <label for="name" class="col-sm-4 col-form-label-sm">Note :</label>
                    <div class="col-sm-8">
                      <asp:TextBox runat="server"  class="form-control form-control-sm"  ID="txtNote" placeholder="Note" ></asp:TextBox> 
                    </div>
                  </div>

                </div>

             </div>

            <div class="row mb-0">

                <div class="col-md-4">
                  <div class="form-group row mb-0">
                    <label for="name" class="col-sm-4 col-form-label-sm">Name :</label>
                    <div class="col-sm-8">
                      <asp:TextBox runat="server"  class="form-control form-control-sm"  ID="txtName" placeholder="Name" ></asp:TextBox> 
                    </div>
                  </div>

                </div>

                 <div class="col-md-4">
                  <div class="form-group row mb-0">
                    <label for="name" class="col-sm-4 col-form-label-sm">Address :</label>
                    <div class="col-sm-8">
                      <asp:TextBox runat="server"  class="form-control form-control-sm"  ID="txtAddress" placeholder="Enter Address" TextMode="MultiLine" Rows="1" ></asp:TextBox> 
                    </div>
                  </div>

                </div>

                 <div class="col-md-4">
                  <div class="form-group row mb-0">
                    <label for="name" class="col-sm-4 col-form-label-sm">Mobile No :</label>
                    <div class="col-sm-8">
                      <asp:TextBox runat="server"  class="form-control form-control-sm"  ID="txtMobileNo" placeholder="Enter Mobile No" ></asp:TextBox> 
                    </div>
                  </div>

                </div>

                </div>


           <div class="row mb-0">

                 <div class="col-md-4">
                  <div class="form-group row mb-0">
                    <label for="name" class="col-sm-4 col-form-label-sm">Email :</label>
                    <div class="col-sm-8">
                      <asp:TextBox runat="server"  class="form-control form-control-sm"  ID="txtEmail" placeholder="Enter Email" ></asp:TextBox> 
                    </div>
                  </div>
                </div>

                  <div class="col-md-4">
                  <div class="form-group row mb-0">
                    <label for="name" class="col-sm-4 col-form-label-sm">Date of Birth :</label>
                    <div class="col-sm-8">
                        <table>
                            <tr>
                                <td>
                                     <asp:TextBox ID="txtBirthDate" runat="server" CssClass="TextBoxnew textDate dateParse" ></asp:TextBox>

                                </td>
                            </tr>
                        </table>
                     
                    </div>
                  </div>
                </div>

                 <div class="col-md-4">
                  <div class="form-group row mb-0">
                    <label for="name" class="col-sm-4 col-form-label-sm">Phone :</label>
                    <div class="col-sm-8">
                      <asp:TextBox runat="server"  class="form-control form-control-sm"  ID="txtPhoneNo" placeholder="Enter Phone No" ></asp:TextBox> 
                    </div>
                  </div>
                </div>

             </div>

           <div class="row mb-0">

                 <div class="col-md-4">
                  <div class="form-group row mb-0">
                    <label for="name" class="col-sm-4 col-form-label-sm">Gender :</label>
                    <div class="col-sm-8">
                     <asp:RadioButtonList ID="rblGender" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal" CssClass="form-check-label" >
                         <asp:ListItem  Value="1" Text="Male" Selected="True"></asp:ListItem>
                         <asp:ListItem  Value="2" Text="Female"></asp:ListItem>
                         <asp:ListItem  Value="3" Text="Others"></asp:ListItem>
                      </asp:RadioButtonList>
                    </div>
                  </div>
                </div>

                 <div class="col-md-4">
                  <div class="form-group row mb-0">
                    <label for="name" class="col-sm-4 col-form-label-sm">Country :</label>
                    <div class="col-sm-8">
                       <%-- <asp:DropDownList runat="server" ID="ddlCountryId" CssClass="form-control form-control-sm"></asp:DropDownList>--%>
                      <asp:TextBox runat="server"  class="form-control form-control-sm "  ID="txtCountry" placeholder="Select" ></asp:TextBox>
                        <asp:HiddenField runat="server" ID="hdnCountryId" Value="0" /> 
                    </div>
                  </div>
                </div>

                 <div class="col-md-4">
                  <div class="form-group row mb-0">
                    <label for="name" class="col-sm-4 col-form-label-sm">Passport :</label>
                    <div class="col-sm-8">
                      <asp:TextBox runat="server"  class="form-control form-control-sm"  ID="txtPassportNo" placeholder="Enter Passport No" ></asp:TextBox> 
                          
                        
                    </div>
                  </div>
                </div>

             </div>
      

      </div>
    <div class="row">
    <div class="col-md-12">
        <div class="card">
           <div class="card-header mb-0 p-1">
              <strong>Room Details :</strong>

           </div>

            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
              <ContentTemplate>
               <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" ShowHeader="true" CssClass="table table-sm table-striped table-bordered table-responsive-sm"  
                DataKeyNames="ROOM_ID" EnableModelValidation="True" ClientIDMode="AutoID" OnRowDataBound="GridView1_RowDataBound" OnRowCommand="GridView1_RowCommand">
                <HeaderStyle CssClass="table-info" Font-Size="Smaller" />                                      
              <Columns>
                   <asp:TemplateField HeaderText="">
                      <ItemTemplate>
                          <asp:LinkButton runat="server" ID="lnkRoomDetails" CssClass="btn btn-info btn-sm" Text="" CommandName="roomdetials" CommandArgument='<%#Bind("ROOM_TYPE_ID") %>'> <i class="fa fa-info-circle" > </i>  Details</asp:LinkButton>
                      </ItemTemplate>
                      
                  </asp:TemplateField>
                  <asp:TemplateField HeaderText="Room Type">
                      <ItemTemplate>
                          <asp:Label runat="server"  ID="lblRoomType" Text='<%# Bind("ROOM_TYPE_NAME") %>' ></asp:Label>
                           <asp:HiddenField runat="server" ID="hdnRoomTypeId" Value='<%#Bind("ROOM_TYPE_ID") %>' />
                            <asp:HiddenField runat="server" ID="hdnReservationDtlId" Value='<%#Bind("RESERVATION_DTL_ID") %>' />
                      </ItemTemplate>
                      
                  </asp:TemplateField>
                   <asp:BoundField DataField="ROOM_DESCRIPTION" HeaderText="Room Description" />
                  <asp:BoundField DataField="MAX_PERSON" HeaderText="Max Person" />
                  <asp:BoundField DataField="NORMAL_RATE" HeaderText="Normal Rate" />
                  <asp:BoundField DataField="DISCOUNTED_RATE" HeaderText="Discounted Rate" />

                  <asp:TemplateField HeaderText="No of Room">
                      <ItemTemplate>
                        <asp:DropDownList runat="server" ID="ddlNoOfRoom" CssClass="form-control form-control-sm">
                           
                        </asp:DropDownList>
                          <asp:HiddenField runat="server" ID="hdnNoOfRoom" Value='<%# Bind("NO_OF_ROOM") %>' />
                            <asp:HiddenField runat="server" ID="hdnRoomQty" Value='<%# Bind("ROOM_QTY") %>' />
                      </ItemTemplate>
                      
                  </asp:TemplateField>
                                                           
               </Columns>
                                                     
          </asp:GridView>

             <%-- Details Modal strat--%>

         <div class="modal fade" id="modalRoomDetails">
        <div class="modal-dialog ">
          <div class="modal-content">
            <div class="modal-header">
              <h4 class="modal-title">Room Details</h4>
              <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                <span aria-hidden="true">&times;</span>
              </button>
            </div>
            <div class="modal-body">
               <!-- Default box -->
      <div class="card card-solid">
          <div class="card-header p-1">
              <asp:Label runat="server" ID="lblRoomTitle"></asp:Label>
          </div>
        <div class="card-body">
          <div class="row">
            <div class="col-md-12 d-inline-block">
            
              <%--  <img src="../../dist/img/photo2.png" class="product-image" alt="Product Image" width="100%" height="100%">--%>
                  <asp:Image ID="ImgRoomType" runat="server"  CssClass="product-image" alt="Product Image" width="100%" height="100%" />
             
            </div>
           
          </div>
          <div class="row mt-4">
            <nav class="w-100">
              <div class="nav nav-tabs" id="product-tab" role="tablist">
                <a class="nav-item nav-link active" id="product-desc-tab" data-toggle="tab" href="#product-desc" role="tab" aria-controls="product-desc" aria-selected="true">Description</a>
               
              </div>
            </nav>
            <div class="tab-content p-3" id="nav-tabContent">
              <div class="tab-pane fade show active" id="product-desc" role="tabpanel" aria-labelledby="product-desc-tab"> 
                  <asp:label runat="server" ID="lblRoomDescription"></asp:label>

              </div>
            </div>
          </div>
        </div>
        <!-- /.card-body -->
      </div>
      <!-- /.card -->
            </div>
            <div class="modal-footer justify-content-between">
              <button type="button" class="btn btn-outline-default" data-dismiss="modal">Close</button>
             
            </div>
          </div>
          <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
      </div>
      <!-- /.modal -->

         <%-- Details Modal End--%>

               <input id="Hidden1" type="hidden" runat="server" value="[]" />
               <input id="Hidden2" type="hidden" runat="server" value="[]" />
              </ContentTemplate>
              <Triggers>
                <%--  <asp:AsyncPostBackTrigger ControlID="btnNewRow2" EventName="Click" />--%>
              </Triggers>
             </asp:UpdatePanel>


        </div>

    </div>
   </div>
  
    <div class="card-footer">
     <div class="row">
      <div class="col-md-12">
       <asp:LinkButton runat="server" ID="btnAddNew"  CssClass="btn btn-primary" Text="<i class='fa fa-plus'></i> Add New"></asp:LinkButton>
       <asp:LinkButton runat="server" ID="btnSave" OnClick="btnSave_Click" CssClass="btn btn-primary" Text="<i class='fas fa-save'></i> Save"></asp:LinkButton>
       <asp:LinkButton runat="server" ID="btnEdit" OnClick="btnEdit_Click" CssClass="btn btn-primary" Text="<i class='fas fa-edit'></i> Edit"></asp:LinkButton>
       <asp:LinkButton runat="server" ID="btnConfirm" OnClick="btnConfirm_Click" CssClass="btn btn-primary" Text="<i class='fas fa-check'></i> Confirm Reservation"></asp:LinkButton>
       <asp:LinkButton runat="server" ID="btnCancel" OnClick="btnCancel_Click" CssClass="btn btn-danger" Text="<i class='fas fa-times'></i> Cancel Reservation"></asp:LinkButton>
     
      
      </div>
     </div>

           <%-- Confirm Modal strat--%>

         <div class="modal fade" id="modalbtnConfirm">
        <div class="modal-dialog ">
          <div class="modal-content">
            <div class="modal-header">
              <h4 class="modal-title">Confirm Note</h4>
              <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                <span aria-hidden="true">&times;</span>
              </button>
            </div>
            <div class="modal-body">
               <!-- Default box -->
      <div class="card">
        
        <div class="card-body">
          <div class="row">
            <div class="col-md-12 d-inline-block">

                 <div class="col-md-12">
                  <div class="form-group row mb-0">
                    <label for="name" class="col-sm-4 col-form-label-sm">Confirm Note :</label>
                    <div class="col-sm-8">
                      <asp:TextBox runat="server"  class="form-control form-control-sm"  ID="txtConfirmNote" placeholder="Enter Confirm Note" TextMode="MultiLine" Rows="2" ></asp:TextBox> 
                    </div>
                  </div>

                </div>
            
             
            </div>
           
          </div>
      
        </div>
        <!-- /.card-body -->
      </div>
      <!-- /.card -->
            </div>
            <div class="modal-footer justify-content-between">
               <asp:Button runat="server" ID="btnConfirmSave" CssClass="btn btn-sm btn-primary" Text="Save" OnClick="btnConfirmSave_Click" />
              <button type="button" class="btn btn-outline-default" data-dismiss="modal">Close</button>
             
            </div>
          </div>
          <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
      </div>
      <!-- /.modal -->

         <%-- Confirm Modal End--%>

              <%-- Cancel Modal strat--%>

         <div class="modal fade" id="modalbtnCancel">
        <div class="modal-dialog ">
          <div class="modal-content">
            <div class="modal-header">
              <h4 class="modal-title">Cancel Note</h4>
              <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                <span aria-hidden="true">&times;</span>
              </button>
            </div>
            <div class="modal-body">
               <!-- Default box -->
      <div class="card">
        
        <div class="card-body">
          <div class="row">
            <div class="col-md-12 d-inline-block">

                 <div class="col-md-12">
                  <div class="form-group row mb-0">
                    <label for="name" class="col-sm-4 col-form-label-sm">Cancel Note :</label>
                    <div class="col-sm-8">
                      <asp:TextBox runat="server"  class="form-control form-control-sm"  ID="txtCancelNote" placeholder="Enter Cancel Note" TextMode="MultiLine" Rows="2" ></asp:TextBox> 
                    </div>
                  </div>

                </div>
            
             
            </div>
           
          </div>
      
        </div>
        <!-- /.card-body -->
      </div>
      <!-- /.card -->
            </div>
            <div class="modal-footer justify-content-between">
               <asp:Button runat="server" ID="btnCancelSave" CssClass="btn btn-sm btn-primary" Text="Save" OnClick="btnCancelSave_Click" />
              <button type="button" class="btn btn-outline-default" data-dismiss="modal">Close</button>
             
            </div>
          </div>
          <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
      </div>
      <!-- /.modal -->

         <%-- Cancel Modal End--%>

    </div>
   </div>
      
     
     </div>
    </div>
</asp:Content>
