<%@ Page Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="ParcelTracking.aspx.cs" Inherits="PG.Web.WREL.ParcelTracking" %>


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


        });    
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
            h2 {
            font-family: 'Poppins', sans-serif;
            font-weight: 600;
            color: #2c3e50;
            letter-spacing: 1px;
        }
            .tracker {
            display: flex;
            justify-content: space-between;
            position: relative;
            margin-top: 40px;
            margin-bottom: 40px;
        }

        .tracker::before {
            content: '';
            position: absolute;
            top: 25px;
            left: 5%;
            right: 5%;
            height: 4px;
            background-color: #dee2e6;
            z-index: 0;
            border-radius: 2px;
        }

        .step {
            position: relative;
            z-index: 1;
            text-align: center;
            width: 20%;
        }

        .step .circle {
            width: 50px;
            height: 50px;
            margin: 0 auto 10px;
            border-radius: 50%;
            background-color: #dee2e6;
            line-height: 50px;
            font-weight: bold;
            color: white;
            font-size: 20px;
            box-shadow: 0 0 6px rgba(0,0,0,0.1);
            transition: background-color 0.3s, box-shadow 0.3s;
        }

        .step.completed .circle,
        .step.current .circle {
            background-color: #28a745;
            box-shadow: 0 0 10px #28a745;
        }

        .step.completed .circle::after {
            content: "✔";
            color: white;
            display: block;
        }

        .step.current .circle::after {
            content: attr(data-step);
            color: white;
            display: block;
        }

        .step.pending .circle::after {
            content: attr(data-step);
            color: white;
            display: block;
        }

        .step .label {
            font-size: 14px;
            color: #6c757d;
        }

        .step.completed .label,
        .step.current .label {
            font-weight: 600;
            color: #28a745;
        }
      
    </style>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
       <div class="container py-5">
        <h2 class="text-center mb-4">Parcel Tracking</h2>

        <div class="form-row justify-content-center mb-4">
            <div class="col-md-6">
                <asp:TextBox ID="txtParcelNumber" runat="server" CssClass="form-control" Placeholder="Enter Parcel Number"></asp:TextBox>
            </div>
            <div class="col-md-2">
                <asp:Button ID="btnTrack" runat="server" CssClass="btn btn-primary btn-block" Text="Track Parcel" OnClick="btnTrack_Click" />
            </div>
        </div>

        <asp:Panel ID="pnlTracking" runat="server" Visible="false">

            <div class="tracker" id="trackerDiv" runat="server">
                <div class="step" id="step1" runat="server">
                    <div class="circle" data-step="1"></div>
                    <div class="label">Information Received</div>
                </div>
                <div class="step" id="step2" runat="server">
                    <div class="circle" data-step="2"></div>
                    <div class="label">Shipment Picked Up</div>
                </div>
                <div class="step" id="step3" runat="server">
                    <div class="circle" data-step="3"></div>
                    <div class="label">In Transit</div>
                </div>
                 <div class="step" id="step4" runat="server">
                    <div class="circle" data-step="4"></div>
                    <div class="label">Arrived at Destination</div>
                </div>
                <div class="step" id="step5" runat="server">
                    <div class="circle" data-step="5"></div>
                    <div class="label">Out for Delivery</div>
                </div>
                <div class="step" id="step6" runat="server">
                    <div class="circle" data-step="6"></div>
                    <div class="label">Delivered</div>
                </div>
            </div>

            <asp:Label ID="lblStatusMessage" runat="server" CssClass="text-center d-block mt-3 font-weight-bold"></asp:Label>
        </asp:Panel>

        <asp:Label ID="lblError" runat="server" CssClass="text-danger text-center d-block mt-3"></asp:Label>
    </div>

      <div class="row-mb-0 d-none">
              <div class="card-footer m-2 p-1">

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
</asp:Content>
