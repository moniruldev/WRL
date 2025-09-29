<%@ Page Title="" Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="UserEntry.aspx.cs" Inherits="PG.Web.Admin.UserEntry" %>

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

      
        var ClientListServiceLink = '<%=this.ClientListServiceLink%>';
    
        var txtClientName = '<%=txtClientName.ClientID%>';
        var hdnClientId = '<%=hdnClientId.ClientID%>';

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


            if ($('#' + txtClientName).is(':visible')) {

                bindClientList();

            }


        });



        function bindClientList() {
            var cgColumns = [
                             { 'columnName': 'clientname', 'width': '200', 'align': 'left', 'highlight': 4, 'label': 'Name' }
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
                width: 450,
                url: serviceURL,
                search: function (event, ui) {
                  //  var distid = $('#' + hdnDistId).val();
                    var newServiceURL = serviceURL;//+ "&distid=" + distid;
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


        function tbopen(key, userid) {
            if (!key) {
                key = '';
            }

            var url = IForm.RootPath + "WREL/DeliveryManEntry.aspx?id=" + key;

            if (IForm.PageMode == Enums.PageMode.InTab) {

                var tdata = new xtabdata();
                tdata.linktype = Enums.LinkType.Direct;
                tdata.id = 0;
                //tdata.name = "Resarvation Entry";
                //tdata.label = "Resarvation Entry";
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
                window.location = url;
            }
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


        /*input[type="radio"] + label
        {
            margin-left: 4px;
            margin-right: 4px;
        }*/ 
        
        /*label.col-form-label-sm{
            text-align:right;
        }*/
        
      
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hdnLoggedInUser" runat="server" />
    <div class="row">
     <div class="container-fluid">
       <div class="card ">  <%-- "d-flex flex-column min-vh-100" this class for set footer on bottom--%>
         <div class="card-header p-0">
           <div class="d-flex align-items-center justify-content-between p-1">
             <h5 class="card-title">User Entry</h5>
          <%--   <asp:LinkButton runat="server" ID="lnkList" class="btn btn-primary p-1"> <i class="fas fa-list"></i> Room List </asp:LinkButton>--%>
         </div>

       </div>
        
      <div class="card-body">
            <asp:HiddenField ID="hdnDeliveryManId" runat="server" />

              <div class="row mb-0">
                 
                <div class="col-md-2">
                 
                </div>

                 <div class="col-md-6">
                  <div class="form-group row mb-0">
                    <label for="name" class="col-sm-3 col-form-label-sm required">User Name :</label>
                    <div class="col-sm-9">
                      <asp:TextBox runat="server"  class="form-control form-control-sm"  ID="txtUserName" placeholder="Enter User Name" ></asp:TextBox> 
                    </div>
                  </div>
                </div>


                


                 <div class="col-md-2">
                 
                </div>

             </div>

           <div class="row mb-0">
                 
                <div class="col-md-2">
                 
                </div>

                


               <div class="col-md-6">
                  <div class="form-group row mb-0">
                    <label for="name" class="col-sm-3 col-form-label-sm">Full Name:</label>
                    <div class="col-sm-9">
                    <asp:TextBox runat="server"  class="form-control form-control-sm"  ID="txtFullName" placeholder="Full Name" ></asp:TextBox> 
                    </div>
                  </div>
                </div>

                 <div class="col-md-2">
                 
                </div>

             </div>
           <div class="row mb-0">
                 
                <div class="col-md-2">
                 
                </div>

                


             <div class="col-md-6">
                  <div class="form-group row mb-0">
                    <label for="name" class="col-sm-3 col-form-label-sm">App :</label>
                    <div class="col-sm-9">
                      <asp:DropDownList ID="ddlApp" runat="server" CssClass="form-control form-control-sm">
                         <asp:ListItem Value="1" Text="Courier" Selected="True" ></asp:ListItem>
                         <asp:ListItem Value="2" Text="Bank" ></asp:ListItem>   
                      </asp:DropDownList>
                    </div>
                  </div>

                </div>

                 <div class="col-md-2">
                 
                </div>

             </div>
          <div class="row mb-0">
                 
                <div class="col-md-2">
                 
                </div>

                


             <div class="col-md-6">
                  <div class="form-group row mb-0">
                    <label for="name" class="col-sm-3 col-form-label-sm">Role :</label>
                    <div class="col-sm-9">
                      <asp:DropDownList ID="ddlRole" runat="server" CssClass="form-control form-control-sm"></asp:DropDownList>
                    </div>
                  </div>

                </div>

                 <div class="col-md-2">
                 
                </div>

             </div>
       

          <div class="row mb-0">
                 
                <div class="col-md-2">
                 
                </div>

                


               <div class="col-md-6">
                  <div class="form-group row mb-0">
                    <label for="name" class="col-sm-3 col-form-label-sm required">Password:</label>
                    <div class="col-sm-9">
                     <asp:TextBox runat="server"  class="form-control form-control-sm"  ID="txtPassword" placeholder="Password" ></asp:TextBox>
                    </div>
                  </div>
                </div>

                 <div class="col-md-2">
                 
                </div>

             </div>

          <div class="row mb-0">
                 
                <div class="col-md-2">
                 
                </div>

                


               <div class="col-md-6">
                  <div class="form-group row mb-0">
                    <label for="name" class="col-sm-3 col-form-label-sm required">Confirm Password:</label>
                    <div class="col-sm-9">
                     <asp:TextBox runat="server"  class="form-control form-control-sm"  ID="txtConfirmPassword" placeholder="Confirm Password" ></asp:TextBox>
                      
                    </div>
                  </div>
                </div>

                 <div class="col-md-2">
                 
                </div>

             </div>
           <div class="row mb-0">
                 
                <div class="col-md-2">
                 
                </div>

                


                <div class="col-md-6">
                  <div class="form-group row mb-0">
                    <label for="name" class="col-sm-3 col-form-label-sm">User Type :</label>
                    <div class="col-sm-9">
                      <asp:DropDownList runat="server" ID="ddlUserType" OnSelectedIndexChanged="ddlUserType_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control form-control-sm">
                          <asp:ListItem Value="0" Text="select" Selected="True"></asp:ListItem>
                          <asp:ListItem Value="ADMIN" Text="Admin" ></asp:ListItem>
                          <asp:ListItem Value="CLIENT" Text="Client" ></asp:ListItem>
                      </asp:DropDownList>
                    </div>
                  </div>

                </div>

                 <div class="col-md-2">
                 
                </div>

             </div>
             <div class="row mb-0" id="dvClient" runat="server" visible="false">
                 
                <div class="col-md-2">
                 
                </div>

                


                <div class="col-md-6">
                  <div class="form-group row mb-0">
                    <label for="name" class="col-sm-3 col-form-label-sm required">Client :</label>
                    <div class="col-sm-9">
                      <asp:TextBox runat="server"  class="form-control form-control-sm"  ID="txtClientName" placeholder="Select" ></asp:TextBox> 
                           <asp:HiddenField runat="server" ID="hdnClientId" Value="0" /> 
                    </div>
                  </div>

                </div>

                 <div class="col-md-2">
                 
                </div>

             </div>

             <div class="row mb-0">
                 
                <div class="col-md-2">
                 
                </div>



                <div class="col-md-6">
                  <div class="form-group row mb-0">
                    <label for="name" class="col-sm-3 col-form-label-sm">Status:</label>
                    <div class="col-sm-9">
                      <asp:DropDownList runat="server"  class="form-control form-control-sm"  ID="ddlStatus" >
                          <asp:ListItem Text="Active" Value="Y" ></asp:ListItem>
                           <asp:ListItem Text="Inactive" Value="N"></asp:ListItem>
                      </asp:DropDownList> 
                    </div>
                  </div>

                </div>

                 <div class="col-md-2">
                 
                </div>

             </div>

           <div class="row justify-content-end">
                 

                <div class="col-md-6 text-left">
                  <asp:Image ID="Image1" runat="server"  CssClass="img-fluid"  />
                </div>


             </div>


      </div>
        
    <div class="card-footer mt-auto">
         <div class="row">
      <div class="col-md-12">
       <asp:LinkButton runat="server" ID="btnAddNew" OnClick="btnAddNew_Click" CssClass="btn btn-primary" Text="<i class='fa fa-plus'></i> Add New"></asp:LinkButton>
       <asp:LinkButton runat="server" ID="btnSave" OnClick="btnSave_Click" CssClass="btn btn-primary" Text="<i class='fas fa-save'></i> Save"></asp:LinkButton>
       <asp:LinkButton runat="server" ID="btnEdit" OnClick="btnEdit_Click" CssClass="btn btn-primary" Text="<i class='fas fa-edit'></i> Edit"></asp:LinkButton>
       <asp:LinkButton runat="server" ID="btnClear" OnClick="btnClear_Click" CssClass="btn btn-danger" Text="<i class='fa fa-ban'></i> Clear"></asp:LinkButton>
      
      </div>
      </div>

    </div>

      
       </div>
     </div>
   </div>
</asp:Content>
