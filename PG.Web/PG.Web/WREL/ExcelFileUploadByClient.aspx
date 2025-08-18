<%@ Page Title="" Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="ExcelFileUploadByClient.aspx.cs" Inherits="PG.Web.WREL.ExcelFileUploadByClient" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"   Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

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


        $(document).ready(function () {

            var pageInstance = Sys.WebForms.PageRequestManager.getInstance();

            pageInstance.add_pageLoaded(function (sender, args) {
                var panels = args.get_panelsUpdated();
                for (i = 0; i < panels.length; i++) {

                    if (panels[i].id == gridUpdatePanelIDDet) {
                        //bindCNList(gridViewIDDet);
                    }

                }

            });


            //if ($('#' + txtStartingDist).is(':visible')) {

            //    bindStartingDistrictList();

            //}

            //if ($('#' + txtDestinationDist).is(':visible')) {

            //    bindDestinationDistrictList();

            //}

            //if ($('#' + txtDestinationTown).is(':visible')) {

            //    bindDestinationTownList();

            //}

            //if ($('#' + txtRoute).is(':visible')) {

            //    bindRouteList();

            //}

            //if ($('#' + txtManagerName).is(':visible')) {

            //    bindEmpList();

            //}
            
            //bindCNList(gridViewIDDet);



        });
     
        

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
             <h5 class="card-title">Excel File Upload By Client</h5>
             <%--<a class="btn btn-primary p-1"> <i class="fas fa-list"></i> Cargo List </a>--%>
         </div>

       </div>
      <div class="card-body">
            <asp:HiddenField ID="hdnCARGO_ID" runat="server" Value="0" />
         <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
           <div class="row mb-0">
    <div class="col-md-4">
        <div class="form-group row mb-0 align-items-center">
            <label for="FileUpload1" class="col-sm-5 col-form-label col-form-label-sm">Upload Excel :</label>
            <div class="col-sm-7 d-flex gap-2">
                <asp:FileUpload ID="fileUploadExcel" runat="server" CssClass="form-control form-control-sm" Width="250" />
                <asp:Button ID="btnUpload" CssClass="btn btn-sm btn-primary" runat="server" Text="Load Excel" OnClick="btnUpload_Click" />
                
            </div>
        </div>
    </div>
</div>

      </div>
 
  
           
    <%-- <table>
    <tr>
        <td align="center">
            <asp:LinkButton Text="Show Modal" ID="LinkButton2" runat="server" />
            
            <ajaxToolkit:ModalPopupExtender ID="Modalpopupextender2" runat="server"
                PopupControlID="Panel4"
                TargetControlID="LinkButton2"
                CancelControlID="Button4"
                BackgroundCssClass="modalBackground">
            </ajaxToolkit:ModalPopupExtender>

            <asp:Panel ID="Panel4" runat="server" CssClass="modalPopup" Width="400px"
                Style="display: none; background-color: lightsteelblue; padding:10px;">
                
                <asp:Label ID="Label1" runat="server" Text="Paste Excel [ without Header / Caption ]" Font-Bold="true"></asp:Label>
                <br /><br />

                <asp:TextBox ID="DrPasteTextBox" runat="server" TextMode="MultiLine" Width="99%" Height="150px"></asp:TextBox>
                <br /><br />

                <asp:Button ID="Button4" runat="server" Text="Close" />
                <asp:Button ID="doneButton" runat="server" Text="Confirm" OnClick="doneButton_OnClick" />
                <asp:Button ID="ClearButton" runat="server" Text="Clear" OnClick="ClearButton_OnClick" />
            </asp:Panel>
        </td>
    </tr>
</table>--%>
         
   </div>
     
     </div>
    </div>
</asp:Content>
