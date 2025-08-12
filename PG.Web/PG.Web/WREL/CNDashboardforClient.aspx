<%@ Page Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="CNDashboardforClient.aspx.cs" Inherits="PG.Web.WREL.CNDashboardforClient" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">



    <script src="../javascript/jquery.ui.combogrid.js" type="text/javascript"></script>
    <script src="../javascript/jquery.attributeobserver.js" type="text/javascript"></script>
    <link href="../css/jquery.ui.combogrid.css" rel="stylesheet" type="text/css" />


    <script language="javascript" type="text/javascript">
        // <!CDATA[

        <%--var ItemListServiceLink = '<%=this.ItemListServiceLink%>';--%>

        var btnGridPageGoTo = '<%=btnGridPageGoTo.ClientID %>';
        var txtGridPageNo = '<%=txtGridPageNo.ClientID %>';
        var ReportViewPageLink = '<%=this.ReportViewPageLink%>';
        var ReportViewPDFPageLink = '<%=this.ReportViewPDFPageLink%>';
        var ReportPrintPageLink = '<%=this.ReportPrintPageLink%>';
        var ReportPDFPageLink = '<%=this.ReportPDFPageLink%>';
        var ifPrintButton = '<%=ifPrintButton.ClientID%>';
     <%--   var ReportViewPageLink = '<%=this.ReportViewPageLink%>';
        var ReportViewPDFPageLink = '<%=this.ReportViewPDFPageLink%>';
       --%>

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
                     {
                         extend: 'pdfHtml5',
                         text: '<i class="bi bi-file-earmark-pdf"></i> Export PDF',
                         filename: exportTitle.replace(/\s+/g, '_'),
                         title: exportTitle,
                         customize: function (doc) {
                             // Fix header wrapping by disabling line breaks on header cells
                             var headerRows = doc.content[1].table.headerRows;

                             // Set noWrap: true on all header cells
                             doc.content[1].table.body[0].forEach(function (cell) {
                                 cell.noWrap = true;
                             });
                             doc.pageMargins = [20, 20, 20, 20];
                             // Add cell borders (all sides)
                             doc.content[1].layout = {
                                 hLineWidth: function(i, node) {
                                     return 0.5; // horizontal line thickness
                                 },
                                 vLineWidth: function(i, node) {
                                     return 0.5; // vertical line thickness
                                 },
                                 hLineColor: function(i, node) {
                                     return 'black'; // horizontal line color
                                 },
                                 vLineColor: function(i, node) {
                                     return 'black'; // vertical line color
                                 },
                                 paddingLeft: function(i, node) { return 4; },
                                 paddingRight: function(i, node) { return 4; },
                                 paddingTop: function(i, node) { return 2; },
                                 paddingBottom: function(i, node) { return 2; }
                             };
                         },
                         exportOptions: {
                             columns: ':not(:last-child)'
                         }
                     },
                    //{ extend: 'copyHtml5', text: '<i class="bi bi-clipboard"></i> Copy' },
                    {
                        extend: 'print', text: '<i class="bi bi-printer"></i> Print',
                        filename: exportTitle.replace(/\s+/g, '_'),
                        title: exportTitle,
                        customize: function (win) {
                            $(win.document.body).find('h1').css({
                                'text-align': 'center',
                                'width': '100%'
                            });
                        },
                        exportOptions: {
                            columns: ':not(:last-child)'
                        }
                    }
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
            $('#' + txtGridPageNo).keydown(function (e) {
                if (e.keyCode == 13) {
                    e.preventDefault();
                    $('#' + btnGridPageGoTo).click();
                }
            });

         


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


        });    

     
    </script>

 
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hdnClientId" runat="server" Value="0" />
    <div class="row">
    <div class="container-fluid">
      <div class="card">
      <div class="card-header p-0">
       <div class="d-flex align-items-center justify-content-between p-1">
         <h5 class="card-title header-title">CN List</h5>
        
       </div>
       </div>

        <div class="card-body">

        <div class="d-flex align-items-center border-bottom pb-2 mb-2">
            <div class="d-flex align-items-center mr-3">
                <label for="txtFromDate" class="mr-2 mb-0 small">From Date:</label>
                <asp:TextBox ID="txtFromDate" runat="server" CssClass="TextBoxnew textDate dateParse form-control form-control-sm" Style="width:130px;"></asp:TextBox>
            </div>

            <div class="d-flex align-items-center mr-3">
                <label for="txtToDate" class="mr-2 mb-0 small">To Date:</label>
                <asp:TextBox ID="txtToDate" runat="server" CssClass="TextBoxnew textDate dateParse form-control form-control-sm" Style="width:130px;"></asp:TextBox>
            </div>

            <asp:LinkButton runat="server" ID="btnLoadData" OnClick="btnLoadData_Click" CssClass="btn btn-primary btn-sm">
                <i class="fa fa-list"></i> Load Data
            </asp:LinkButton>
        </div>



               <div class="row mb-0 d-none">
                <div class="m-2 p-1 d-flex justify-content-between align-items-center w-100">
                    <div>
                     
                          <asp:LinkButton runat="server" ID="btnClearFilter" OnClick="btnClearFilter_Click" CssClass="btn btn-primary" Visible="false">
                            <i class="fa fa-times text-danger"></i> Clear Filter
                        </asp:LinkButton>
                    </div>

                    <div class="d-none">
                        <asp:LinkButton runat="server" ID="btnDownloadPdf" OnClick="btnDownloadPdf_Click" CssClass="btn btn-primary">
                            <i class="fas fa-file-pdf text-danger"></i> View PDF
                        </asp:LinkButton>
                        <asp:LinkButton runat="server" ID="LinkButton2" OnClick="btnLoadData_Click" CssClass="btn btn-primary">
                            <i class="fa fa-file-excel"></i> Export Excel
                        </asp:LinkButton>
                    </div>
                </div>
            </div>


            <div class="row d-none">
             <div class="col-md-12 d-none">
                   <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" ShowHeader="true" CssClass="table table-sm table-striped table-bordered table-responsive-sm"  
                DataKeyNames="CN_ID" EnableModelValidation="True" ClientIDMode="AutoID" OnRowDataBound="GridView1_RowDataBound" AllowPaging="true" EmptyDataText="There is no record" PageSize="2" 
                 OnPageIndexChanging="GridView1_PageIndexChanging" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowCommand="GridView1_RowCommand">
                  <PagerSettings Mode="NumericFirstLast" />
                <HeaderStyle CssClass="table-info" Font-Size="Smaller" />                                      
              <Columns>
                   
                   <asp:BoundField DataField="CN_NUMBER" HeaderText="CN Number" /> 
                   <asp:BoundField DataField="ITEM_NAME" HeaderText="Item Name" />
                  <asp:BoundField DataField="CONSIGNEE_NAME" HeaderText="Recipient Name" />
                  <asp:BoundField DataField="CONSIGNEE_ADDRESS" HeaderText="Recipient Address" />
                  <asp:BoundField DataField="CONSIGNEE_MOBILE_NO" HeaderText="Mobile No" />
                    <asp:BoundField DataField="IS_DELIVERED" HeaderText="Status" />

               </Columns>
                                                     
          </asp:GridView>
             </div>
            </div>
            <div class="row">
   <asp:Repeater ID="rptData" runat="server" OnItemCommand="rptData_ItemCommand">
    <HeaderTemplate>
        <table id="myTable" class="display table table-striped table-bordered" style="width:100%">
            <thead class="table-info">
                <tr>
                    <th>CN Number</th>
                    <th>Item Name</th>
                    <th>Recipient Name</th>
                    <th>Recipient Address</th>
                    <th>Mobile No</th>
                    <th>Status</th>
                    <th>Action</th>
                </tr>
            </thead>
            <tbody>
    </HeaderTemplate>

    <ItemTemplate>
        <tr>
            <td><%# Eval("CN_NUMBER") %></td>
            <td><%# Eval("ITEM_NAME") %></td>
            <td><%# Eval("CONSIGNEE_NAME") %></td>
            <td><%# Eval("CONSIGNEE_ADDRESS") %></td>
            <td><%# Eval("CONSIGNEE_MOBILE_NO") %></td>
            <td><%# Eval("IS_DELIVERED") %></td>
             <td>
                <asp:LinkButton ID="lnkPrint" runat="server"
                    CommandName="print"
                    CommandArgument='<%# Eval("CN_ID") %>'
                    CssClass="btn btn-sm btn-primary" Width="70px">
                    CN Print
                </asp:LinkButton>
            </td>
        </tr>
    </ItemTemplate>

    <FooterTemplate>
            </tbody>
        </table>
    </FooterTemplate>
</asp:Repeater>



            </div>
            <div class="row d-none">
                <div class="col-md-12">
                     <div id="dvGridFooter" style="width: 100%; height: 25px; font-size: smaller;" class="subFooter">
                            <table style="height: 100%; width: 100%;"
                                cellspacing="2" cellpadding="1" rules="all">
                                <tr>
                                    <td align="left" style="width: 40%">
                                        <table>
                                            <tr>
                                                <td style="width: 2px;"></td>
                                                <td>
                                                    <asp:Label ID="lblTotal" CssClass="col-form-label-sm" runat="server" Text="Rows: 0 of 0"></asp:Label>
                                                    <asp:HiddenField ID="hdnRowCount" runat="server" Value="0" />
                                                </td>
                                            </tr>
                                        </table>



                                    </td>
                                    <td align="right" style="width: 60%">
                                        <div id="dvGridPager" class="dvGridPager">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="btnGridPageGoTo" runat="server" Text="Go"
                                                            OnClick="btnGridPageGoTo_Click" />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label2" CssClass="col-form-label-sm p-2" runat="server" Text="Page Size:"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlGridPageSize" runat="server" CssClass="form-control form-control-sm m-1 p-0" AutoPostBack="True"
                                                            OnSelectedIndexChanged="ddlGridPageSize_SelectedIndexChanged">
                                                            <asp:ListItem Value="10" Selected="True">10</asp:ListItem>
                                                            <asp:ListItem Value="20">20</asp:ListItem>
                                                            <asp:ListItem Value="30">30</asp:ListItem>
                                                            <asp:ListItem Value="50">50</asp:ListItem>
                                                            <asp:ListItem Value="100">100</asp:ListItem>
                                                            <asp:ListItem Value="200">200</asp:ListItem>
                                                            <asp:ListItem Value="0" >all</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>


                                                    <td>
                                                        <asp:Label ID="Label1" runat="server" CssClass="col-form-label-sm p-2" Text="Page:"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtGridPageNo" runat="server" CssClass="form-control form-control-sm m-1 p-0" Width="50px">0</asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblGridPageInfo" runat="server" CssClass="col-form-label-sm p-2"  Text=" of 0"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnGridPageFirst" runat="server" Text="" CssClass="btnGridPageFirst"
                                                            OnClick="btnGridPageFirst_Click" ToolTip="First" />
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnGridPagePrev" runat="server" Text="" CssClass="btnGridPagePrev"
                                                            OnClick="btnGridPagePrev_Click" ToolTip="Previous" />
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnGridPageNext" runat="server" Text="" CssClass="btnGridPageNext"
                                                            OnClick="btnGridPageNext_Click" ToolTip="Next" />
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnGridPageLast" runat="server" Text="" CssClass="btnGridPageLast"
                                                            OnClick="btnGridPageLast_Click" ToolTip="Last" />
                                                    </td>
                                                    <td style="width: 2px;"></td>
                                                </tr>
                                            </table>
                                        </div>

                                    </td>

                                </tr>
                            </table>
                        </div>

                </div>

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
