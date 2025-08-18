<%@ Page Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="ExcelUploadByClientList.aspx.cs" Inherits="PG.Web.WREL.ExcelUploadByClientList" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">



    <script src="../javascript/jquery.ui.combogrid.js" type="text/javascript"></script>
    <script src="../javascript/jquery.attributeobserver.js" type="text/javascript"></script>
    <link href="../css/jquery.ui.combogrid.css" rel="stylesheet" type="text/css" />


    <script language="javascript" type="text/javascript">
        // <!CDATA[

        var ReportViewPageLink = '<%=this.ReportViewPageLink%>';
        var ReportViewPDFPageLink = '<%=this.ReportViewPDFPageLink%>';
        var ReportPrintPageLink = '<%=this.ReportPrintPageLink%>';
        var ReportPDFPageLink = '<%=this.ReportPDFPageLink%>';

        var ItemListServiceLink = '<%=this.ItemListServiceLink%>';
        var ClientListServiceLink = '<%=this.ClientListServiceLink%>';
        var btnGridPageGoTo = '<%=btnGridPageGoTo.ClientID %>';
        var txtGridPageNo = '<%=txtGridPageNo.ClientID %>';
     
        var txtClientName = '<%=txtClientName.ClientID%>';
        var hdnClientId = '<%=hdnClientId.ClientID%>';

        function PageResizeCompleted(pg, cntMain) {
            resizeContentInner(cntMain);

        }

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

        function fromParent(val1) {
            alert('this is called from parent: ' + val1);
        }


        $(document).ready(function () {

            if ($('#' + txtClientName).is(':visible')) {

                bindClientNameList();

            }

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

        function reportInNewWindow(url) {
            var rWin = window.open(url, '_blank');
            if (rWin == null) {
                reportURL = url;
                showOverlayReport();
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
     
    </script>

    <style type="text/css">
      
    </style>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
    <div class="container-fluid">
      <div class="card">
      <div class="card-header p-0">
       <div class="d-flex align-items-center justify-content-between p-1">
         <h5 class="card-title">Parcel List</h5>
           <asp:LinkButton runat="server" ID="btnNewAdd" CssClass="btn btn-primary p-1"><i class="fas fa-plus"></i> New Entry</asp:LinkButton>
       </div>
       </div>

        <div class="card-body">
          <div class="row mb-0">

               

               <div class="col-md-4">
                  <div class="form-group row mb-0">
                    <label for="name" class="col-sm-5 col-form-label-sm">Client :</label>
                    <div class="col-sm-7">
                      <asp:TextBox runat="server"  class="form-control form-control-sm"  ID="txtClientName" placeholder="Select" ></asp:TextBox> 
                           <asp:HiddenField runat="server" ID="hdnClientId" Value="0" /> 
                    </div>
                  </div>

                </div>

           </div>


            <div class="row-mb-0">
              <div class="card-footer m-2 p-1">
              <asp:LinkButton runat="server" ID="btnLoadData" OnClick="btnLoadData_Click"  CssClass="btn btn-primary" Text="<i class='fa fa-list'></i> Show Data"></asp:LinkButton>

                   <asp:DropDownList ID="ddlReportViewType" runat="server" CssClass="dropDownList" Visible="false">
                            <asp:ListItem Value="0">Screen</asp:ListItem>
                            <asp:ListItem Selected="True" Value="1">PDF</asp:ListItem>
                        </asp:DropDownList>
                  <asp:DropDownList ID="ddlReportViewMode" runat="server" CssClass="dropDownList" Visible="false">
                            <asp:ListItem Value="0">In This Tab</asp:ListItem>
                            <asp:ListItem Value="1">In New Tab</asp:ListItem>
                            <asp:ListItem Selected="True" Value="2">In New Window</asp:ListItem>
                        </asp:DropDownList>
             </div>
            </div>

            <div class="row">
             <div class="col-md-12">
                   <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" ShowHeader="true" CssClass="table table-sm table-striped table-bordered table-responsive-sm"  
                DataKeyNames="UPLOAD_ID" EnableModelValidation="True" ClientIDMode="AutoID" OnRowDataBound="GridView1_RowDataBound" AllowPaging="true" EmptyDataText="There is no record" PageSize="2" 
                 OnPageIndexChanging="GridView1_PageIndexChanging" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowCommand="GridView1_RowCommand">
                  <PagerSettings Mode="NumericFirstLast" />
                <HeaderStyle CssClass="table-info" Font-Size="Smaller" />                                      
              <Columns>
                   <asp:HyperLinkField HeaderText="" Text="">
                   <ControlStyle CssClass="buttonViewGrid" Height="20px" Width="40px" />
                  <ItemStyle Width="50px" />
                  </asp:HyperLinkField>
              
                   <asp:BoundField DataField="FILE_NAME" HeaderText="File Name" />
        <asp:BoundField DataField="FILE_PATH" HeaderText="Path" />
           <asp:BoundField DataField="UPLOAD_DATE" HeaderText="Upload Date" />       
        <asp:TemplateField HeaderText="Action">
            <ItemTemplate>
                <asp:LinkButton ID="lnkDownload" runat="server" 
                    CommandName="DownloadFile" 
                    CommandArgument='<%# Eval("FILE_PATH") %>'>
                    Download
                </asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
               </Columns>
                                                     
          </asp:GridView>
             </div>
            </div>
            <div class="row">
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

        </div>

      </div>

    </div>
    </div>
</asp:Content>
