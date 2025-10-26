<%@ Page Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="ClientWiseBillReport.aspx.cs" Inherits="PG.Web.WREL.ClientWiseBillReport" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">



    <script src="../javascript/jquery.ui.combogrid.js" type="text/javascript"></script>
    <script src="../javascript/jquery.attributeobserver.js" type="text/javascript"></script>
    <link href="../css/jquery.ui.combogrid.css" rel="stylesheet" type="text/css" />


    <script language="javascript" type="text/javascript">
        // <!CDATA[

        <%--var ItemListServiceLink = '<%=this.ItemListServiceLink%>';--%>

       
        var ReportViewPageLink = '<%=this.ReportViewPageLink%>';
        var ReportViewPDFPageLink = '<%=this.ReportViewPDFPageLink%>';
        var ReportPrintPageLink = '<%=this.ReportPrintPageLink%>';
        var ReportPDFPageLink = '<%=this.ReportPDFPageLink%>';
        var ClientListServiceLink = '<%=this.ClientListServiceLink%>';
        var DepartmentListbyClientIDServiceLink = '<%=this.DepartmentListbyClientIDServiceLink%>';
        var ifPrintButton = '<%=ifPrintButton.ClientID%>';
     <%--   var ReportViewPageLink = '<%=this.ReportViewPageLink%>';
        var ReportViewPDFPageLink = '<%=this.ReportViewPDFPageLink%>';
       --%>
        var txtClientName = '<%=txtClientName.ClientID%>';
        var hdnClientId = '<%=hdnClientId.ClientID%>';
        var txtDepartment = '<%=txtDepartment.ClientID%>';
        var hdnDeptID = '<%=hdnDeptID.ClientID%>';

        function PageResizeCompleted(pg, cntMain) {
            resizeContentInner(cntMain);

        }

        //$(document).ready(function () {
        //    $('#myTable').DataTable({
        //        paging: true,
        //        searching: true,
        //        ordering: true,
        //        scrollX: true,
        //        autoWidth: false
        //    });
        //});
        $(document).ready(function () {
            var exportTitle = $('.header-title').text().trim();
            $.fn.dataTable.Buttons.defaults.dom.button.className = 'btn btn-primary btn-sm';

            $('#myTable').DataTable({
                paging: true,
                searching: true,
                ordering: true,
                scrollX: true,
                autoWidth: false,
                dom:
                    '<"row"<"col-sm-12 d-flex justify-content-between align-items-center"lBf>>' + 
                    'rtip', 
                buttons: [
                      {
                          extend: 'excelHtml5',
                          text: '<i class="bi bi-file-earmark-excel"></i> Export Excel',
                          filename: exportTitle.replace(/\s+/g, '_'), 
                          title: exportTitle,
                          exportOptions: {
                              columns: ':not(:last-child)' 
                          }
                      },
                    // {
                    //     extend: 'pdfHtml5',
                    //     text: '<i class="bi bi-file-earmark-pdf"></i> Export PDF',
                    //     filename: exportTitle.replace(/\s+/g, '_'),
                    //     title: exportTitle,
                    //     customize: function (doc) {
                    //         // Fix header wrapping by disabling line breaks on header cells
                    //         var headerRows = doc.content[1].table.headerRows;

                    //         // Set noWrap: true on all header cells
                    //         doc.content[1].table.body[0].forEach(function (cell) {
                    //             cell.noWrap = true;
                    //         });
                    //         doc.pageMargins = [20, 20, 20, 20];
                    //         // Add cell borders (all sides)
                    //         doc.content[1].layout = {
                    //             hLineWidth: function(i, node) {
                    //                 return 0.5; // horizontal line thickness
                    //             },
                    //             vLineWidth: function(i, node) {
                    //                 return 0.5; // vertical line thickness
                    //             },
                    //             hLineColor: function(i, node) {
                    //                 return 'black'; // horizontal line color
                    //             },
                    //             vLineColor: function(i, node) {
                    //                 return 'black'; // vertical line color
                    //             },
                    //             paddingLeft: function(i, node) { return 4; },
                    //             paddingRight: function(i, node) { return 4; },
                    //             paddingTop: function(i, node) { return 2; },
                    //             paddingBottom: function(i, node) { return 2; }
                    //         };
                    //     },
                    //     exportOptions: {
                    //         columns: ':not(:last-child)'
                    //     }
                    // },
                    ////{ extend: 'copyHtml5', text: '<i class="bi bi-clipboard"></i> Copy' },
                    //{
                    //    extend: 'print', text: '<i class="bi bi-printer"></i> Print',
                    //    filename: exportTitle.replace(/\s+/g, '_'),
                    //    title: exportTitle,
                    //    customize: function (win) {
                    //        $(win.document.body).find('h1').css({
                    //            'text-align': 'center',
                    //            'width': '100%'
                    //        });
                    //    },
                    //    exportOptions: {
                    //        columns: ':not(:last-child)'
                    //    }
                    //}
                ],
                language: {
                    emptyTable: "No data available"
                }
            });
        });






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
            //$('#' + txtGridPageNo).keydown(function (e) {
            //    if (e.keyCode == 13) {
            //        e.preventDefault();
            //        $('#' + btnGridPageGoTo).click();
            //    }
            //});

         


        });
 


        function tbopen(key, userid) {
            if (!key) {
                key = '';
            }

            var url = IForm.RootPath + "WREL/ParcelCreation.aspx?id=" + key;

            if (IForm.PageMode == Enums.PageMode.InTab) {

                var tdata = new xtabdata();
                tdata.linktype = Enums.LinkType.Direct;
                tdata.id = 0;
                tdata.name = "Parcel Creation";
                tdata.label = "Parcel Creation";
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
        $(document).ready(function () {
            str = document.body.innerHTML


            $("#tblParam tr.rowParam").each(function () {
                var cell = $.trim($(this).find('td').text());
                if (cell.length == 0) {
                    //console.log('empty');
                    //$(this).addClass('nodisplay');
                    $(this).hide();
                }
            });

            $("#btnOpenReportWindow").click(function () {
                window.open(reportURL, '_blank');
                //hideOverlayReport();
            });

            $("#btnCacnelReportWindow").click(function () {
                //hideOverlayReport();
            });

            //hideOverlay();

        });
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
        function reportInNewWindow(url) {
            var rWin = window.open(url, '_blank');
            if (rWin == null) {
                reportURL = url;
                //showOverlayReport();
            }
        }

        function fromParent(val1) {
            alert('this is called from parent: ' + val1);
        }


        $(document).ready(function () {
           
            if ($('#' + txtClientName).is(':visible')) {
               
                bindClientList();
                
            }

            if ($('#' + txtDepartment).is(':visible')) {

                bindDepartmentListByClientID();

            }
        });    

        function bindClientList() {
            var cgColumns = [
                             { 'columnName': 'clientname', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Name' }
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
                    $('#' + hdnClientId).val('');
                    $('#' + txtClientName).val('');
                }
            });
        }

        function bindDepartmentListByClientID() {
            var cgColumns = [
                             { 'columnName': 'deptname', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Dept Name' }


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
    </script>

 
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
    <div class="row">
    <div class="container-fluid">
      <div class="card">
      <div class="card-header p-0">
       <div class="d-flex align-items-center justify-content-between p-1">
         <h5 class="card-title header-title">CN Bill Report</h5>
        
       </div>
       </div>

        <div class="card-body">

           <div class="container-fluid">
  <div class="d-flex justify-content-center align-items-center flex-wrap my-2">

    <!-- Client Name -->
    <div class="d-flex align-items-center mx-2 mb-2">
      <label for="txtClientName" class="mb-0 mr-2 small">Client Name:</label>
      <asp:TextBox runat="server" ID="txtClientName" CssClass="form-control form-control-sm" Style="width:150px;"></asp:TextBox>
      <asp:HiddenField ID="hdnClientId" runat="server" Value="0" />
    </div>
       <div class="d-flex align-items-center mx-2 mb-2">
      <label for="txtDepartment" class="mb-0 mr-2 small">Department:</label>
      <asp:TextBox runat="server" ID="txtDepartment" CssClass="form-control form-control-sm" Style="width:150px;"></asp:TextBox>
      <asp:HiddenField ID="hdnDeptID" runat="server" Value="0" />
    </div>
    <!-- From Date -->
    <div class="d-flex align-items-center mx-2 mb-2">
      <label for="txtFromDate" class="mb-0 mr-2 small">From Date:</label>
      <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control form-control-sm textDate dateParse" Style="width:130px;"></asp:TextBox>
    </div>

    <!-- To Date -->
    <div class="d-flex align-items-center mx-2 mb-2">
      <label for="txtToDate" class="mb-0 mr-2 small">To Date:</label>
      <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control form-control-sm textDate dateParse" Style="width:130px;"></asp:TextBox>
    </div>

    <!-- Buttons -->
    <div class="d-flex align-items-center mx-2 mb-2">
      <asp:LinkButton runat="server" ID="btnDownloadPdf" OnClick="btnDownloadPdf_Click" CssClass="btn btn-primary btn-sm mx-1">
        <i class="fas fa-file-pdf text-danger"></i> View PDF
      </asp:LinkButton>
      <asp:LinkButton runat="server" ID="btnExcelExport" OnClick="btnExcelExport_Click" CssClass="btn btn-success btn-sm mx-1" >
        <i class="fa fa-file-excel"></i> Load Data
      </asp:LinkButton>
    </div>

  </div>
</div>


        
             <div class="row">
  

<asp:Repeater ID="rptData" runat="server">
    <HeaderTemplate>
        <table id="myTable" class="display table table-striped table-bordered" style="width:100%">
            <thead class="table-info text-center">
                <tr>
                    <th style="width:5%">SLNo</th>
                    <th style="width:10%">Date</th>
                    <th style="width:15%">CN Number</th>
                    <th style="width:10%">Department</th>
                    <th style="width:10%">Booking</th>
                    <th style="width:20%">Destination</th>
                    <th style="width:15%">Item Name</th>
                    <th style="width:5%">UOM</th>
                    <th style="width:5%">Quantity</th>
                    <th style="width:5%">Rate</th>
                    <th style="width:5%">Taka</th>
                </tr>
            </thead>
            <tbody>
    </HeaderTemplate>

    <ItemTemplate>
        <tr>
            <td class="text-center"><%# Container.ItemIndex + 1 %></td>
            <td><%# Eval("BOOKING_DATE", "{0:dd-MMM-yyyy}") %></td>
            <td><%# Eval("CN_NUMBER") %></td>
            <td><%# Eval("DEPT") %></td>
            <td><%# Eval("BOOKING") %></td>
            <td><%# Eval("DESTINATION") %></td>
            <td><%# Eval("ITEM_NAME") %></td>
            <td class="text-center"><%# Eval("UOM_NAME") %></td>
            <td class="text-center"><%# Eval("QUANTITY") %></td>
            <td class="text-right"><%# Eval("RATE") %></td>
            <td class="text-right"><%# Eval("TOTAMT") %></td>
        </tr>
    </ItemTemplate>

    <FooterTemplate>
            </tbody>
        </table>
    </FooterTemplate>
</asp:Repeater>


            </div>

        <div class="row">
                <div class="col-md-12">
 
            <div id="dvContentFooterInner" class="d-none"  >
                <div style="width: 100%; height: 100%; margin-bottom: 0px;">
                    <div style="width: auto; min-width: 300px; height: auto; text-align: left;">
                        <table border="0">
                            <tr>
                                <td style="width: 100px;"></td>
                                <td>
                                    <asp:Label ID="Label3" runat="server" Text="Report View"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlReportViewMode" runat="server" CssClass="dropDownList">
                                        <asp:ListItem Value="0">In This Tab</asp:ListItem>
                                        <asp:ListItem  Value="1">In New Tab</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="2">In New Window</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlReportViewType" runat="server" CssClass="dropDownList">
                                        <asp:ListItem Value="0">Screen</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="1">PDF</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td></td>

                                <td style="width: 20px;"></td>
                                <td style="width: 10px;"></td>
                                <td>
                                    <div id="dvPrintIFrame" class="dvPrintIFrame">
                                        <iframe id="ifPrintButton" runat="server" width="0" height="0"></iframe>
                                    </div>
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
    </div>
</asp:Content>
