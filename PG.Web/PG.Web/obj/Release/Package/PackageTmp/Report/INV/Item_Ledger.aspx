<%@ Page Title="" Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="Item_Ledger.aspx.cs" Inherits="PG.Web.Report.INV.Item_Ledger" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

 <script src="../../javascript/jquery.ui.combogrid.js" type="text/javascript"></script>
    <script src="../../javascript/jquery.attributeobserver.js" type="text/javascript"></script>
    <link href="../../css/jquery.ui.combogrid.css" rel="stylesheet" type="text/css" />


    <script language="javascript" type="text/javascript">
        // <!CDATA[

        var isPageResize = true;
        ContentForm.CalendarImageURL = "../../image/calendar.png";

       <%-- var hdnCompanyID = '<%=hdnCompanyID.ClientID%>';--%>

        var ReportViewPageLink = '<%=this.ReportViewPageLink%>';
        var ReportViewPDFPageLink = '<%=this.ReportViewPDFPageLink%>';
        var ReportPrintPageLink = '<%=this.ReportPrintPageLink%>';
        var ReportPDFPageLink = '<%=this.ReportPDFPageLink%>';
        var ItemListServiceLink = '<%=this.ItemListServiceLink%>';
        <%--var InvoiceListServiceLink = '<%=this.InvoiceListServiceLink%>';--%>


        var btnItemLoad = '<%= btnItemLoad.ClientID%>';
        var hdnItemIdForFilter = '<%= hdnItemIdForFilter.ClientID%>';
        var txtItemName = '<%= txtItemName.ClientID%>';



  


        function ShowProgress() {
            $('#' + updateProgressID).show();
        }

        function UserConfirmation() {
            return confirm("Are you sure you want to Stock Issue and Print DC,GP ?");
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


        //$(document).ready(function () {
        //    $('#' + txtGridPageNo).keydown(function (e) {
        //        if (e.keyCode == 13) {
        //            e.preventDefault();
        //            $('#' + btnGridPageGoTo).click();
        //        }
        //    });
        //});


        function tbopen(key, userid) {
            key = key || '';

            var url = IForm.RootPath + "Inventory/NewDCGPAgainstInvoice.aspx?id=" + key;
            //var url = IForm.RootPath + "Accounting/GeneralLedger/JournalFull.aspx?id=" + key;


            if (IForm.PageMode == Enums.PageMode.InTab) {

                var tdata = new xtabdata();
                tdata.linktype = Enums.LinkType.Direct;
                tdata.id = 0;
                tdata.name = "DC and GP Against Invoice";
                //tdata.label = "User: " + userid;
                tdata.label = "DC and GP Against Invoice";
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

        // alert('OK');
        $(document).ready(function () {

            var pageInstance = Sys.WebForms.PageRequestManager.getInstance();

            pageInstance.add_pageLoaded(function (sender, args) {
                var panels = args.get_panelsUpdated();
                for (i = 0; i < panels.length; i++) {
                    
                    if (panels[i].id == gridUpdatePanelIDDet) {
                    }

                }
            });

            if ($('#' + txtItemName).is(':visible')) {
                bindItemList();
            }
        });


        function bindItemList() {

            var cgColumns = [{ 'columnName': 'itemname', 'width': '400', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                             , { 'columnName': 'uomname', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'UOM' }
                             , { 'columnName': 'itemcode', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Code' }
                             //, { 'columnName': 'itemgroupdesc', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Group Name' }
                             //, { 'columnName': 'class_name', 'width': '120', 'align': 'left', 'highlight': 4, 'label': 'Class Name' }
                             //, { 'columnName': 'item_type', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Item Type' }
            ];
            var serviceURL = ItemListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;


            serviceURL += "&ispaging=1";
            var groupIDElem = $('#' + txtItemName);

            $('#' + btnItemLoad).click(function (e) {
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
                width: 750,
                url: serviceURL,
                search: function (event, ui) {
                    //var vgroupid = 0;
                    //var groupName = $('#' + txtGroupName).val();
                    //if (groupName != "") {
                    //    vgroupid = $('#' + hdnGroupID).val();
                    //    if (vgroupid == "0" || vgroupid == "") {
                    //        vgroupid = 0;
                    //    }
                    //} else {
                    //    $('#' + hdnGroupID).val('0');

                    //}

                    var newServiceURL = serviceURL ;

                    newServiceURL = JSUtility.AddTimeToQueryString(newServiceURL);
                    // var newServiceURL = serviceURL;
                    $(this).combogrid("option", "url", newServiceURL);


                },
                select: function (event, ui) {
                    if (!ui.item) {
                        event.preventDefault();

                        // $('#' + hdnDealerID).val('0');
                        //$('#' + txtDealerID).val('');
                        return false;
                        //ClearGLAccountData(elemID);
                    }


                    if (ui.item.dealerid == '') {
                        event.preventDefault();
                        return false;
                        //ClearGLAccountData(elemID);
                    }
                    else {
                        $('#' + hdnItemIdForFilter).val('0');
                        // $('#' + hdnDealerID).val(ui.item.dealerid);
                        $('#' + hdnItemIdForFilter).val(ui.item.itemid);
                        $('#' + txtItemName).val(ui.item.itemname);
                        //$('#' + txtGroupCode).val(ui.item.itemgroupcode);

                    }
                    return false;
                },

                lc: ''
            });


            $(groupIDElem).blur(function () {
                var self = this;

                var groupID = $(groupIDElem).val();
                if (groupID == '') {
                    // $('#' + hdnDealerID).val('0');
                    $('#' + txtItemName).val('');
                    //$('#' + txtGroupCode).val('');
                }
            });
        }

        // ]]>
    </script>


   <style type="text/css">
        /*.FixedHeader { POSITION: relative; TOP: expression(this.offsetParent.scrollTop); BACKGROUND-COLOR: white } */

        .FixedHeader {
            position: relative;
            background-color: white;
        }

        #dvMessage {
            height: 20px;
        }
    </style>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    

    <div id="dvPageContent" style="width: 100%; height: 50%;">

        <div id="dvContentHeader" class="dvContentHeader">
            <div id="dvHeader" class="dvHeader" align="center">
                <asp:Label ID="lblHeader" runat="server" Text="Item Ledger" CssClass="lblHeader" Font-Bold="true" Font-Size="15px"></asp:Label>
            </div>
            <div id="dvMessage" class="dvMessage">
                <asp:Label ID="lblMessage" runat="server" CssClass="lblMessage" Text="" Width="100%"></asp:Label>
            </div>

            <div id="dvHeaderControl" class="dvHeaderControl">
            </div>
            <asp:HiddenField ID="hdnItemIdForFilter" runat="server" />
        </div>

               <div id="dvContentMain" class="dvContentMain">
            <div id="dvControlsHead" style="height: auto; width: 100%;" align="center">
                <br />
                <br />
               <table align="center" cellpadding="1" style="border-style:solid;border-color:greenyellow;border-width: 1px; padding:10px;">

                     <tr>
               <td></td>
                                                        <td align="right" class="auto-style2">
                                                            <asp:Label ID="lblCustomer" runat="server" Text="Item :" Font-Bold="true"></asp:Label>
                                                        </td>

                                                        <td colspan="3" style=" text-align : left;">
                                                            <asp:TextBox ID="txtItemName" Width="290px" runat="server" CssClass="textBox required" Enabled="true"></asp:TextBox>
                                                       <input id="btnItemLoad" type="button" value="" runat="server" class="buttonDropdown" tabindex="-1" />
                                                        </td>
                                                        
                                                        <td colspan="4"  align="left">

                                                             
                                                        </td>

             

                                                    </tr>


            <tr>
              <td>
              </td>
              <td>
               <asp:Label ID="lblFromDate" runat="server" Text="From Date:" style="height: 19px; width: 43px; " Font-Bold="True" Visible="true"></asp:Label>
              </td>
              <td align="left">
                 <asp:TextBox ID="txtFromDate" runat="server" CssClass="textBox textDate dateParse" Style="text-align: left;" Width="100px"></asp:TextBox>
                <%-- <asp:RegularExpressionValidator runat="server" ControlToValidate="txtFromDate" ValidationExpression="(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$" ErrorMessage="Invalid date format." ValidationGroup="Group1" ForeColor="Red" />--%>
              </td>
              <td align="right">
                    <asp:Label ID="lblInvoiceToDate" runat="server" Text="To Date:" style="height: 19px; width: 43px; " Font-Bold="True" Visible="true"></asp:Label>
                   
              </td>
            
              <td>
                 <asp:TextBox ID="txtTodate" runat="server" CssClass="textBox textDate dateParse" Style="text-align: left;" Width="100px"></asp:TextBox>&nbsp;
                   
                 <asp:RegularExpressionValidator runat="server" ControlToValidate="txtTodate" ValidationExpression="(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$" ErrorMessage="Invalid date format." ValidationGroup="Group1" ForeColor="Red" />
              </td>
               
             
                 
            </tr>

                    <tr>
              <td>
              </td>
              <td align="right">
               <%--<asp:Label ID="lblReportType" runat="server" Text="Report Type:" style="height: 19px; width: 43px; " Font-Bold="True" Visible="true"></asp:Label>--%>
              </td>
              <td align="left">
                 <%--<asp:DropDownList ID="ddlReportType" runat="server" CssClass="dropDownList">
                     <asp:ListItem Selected="True" Text="Summary" Value="1"></asp:ListItem>
                      <asp:ListItem  Text="Details" Value="2"></asp:ListItem>
                 </asp:DropDownList>--%>
              </td>
              <td align="right">
                  
                   
              </td>
            
              <td>
                
              </td>
               
             
                 
            </tr>
           
            <tr>
              <td>
              </td>
              <td>
              
              </td>
              <td colspan="2">
                 <asp:Button ID="btnPreview" runat="server" Text="Preview" CssClass="buttonPrintPreview" OnClick="btnPreview_Click"  />&nbsp;&nbsp;
                   <input id="btnClose" type="button" runat="server" class="buttonClose" value="Close" onclick="if (ContentForm) { ContentForm.CloseForm(); }" /> 
              </td>
              
              
               
              <td>
                <asp:HiddenField ID="hdnCustomerID" runat="server" Value="0"  />
              </td>
               
            </tr>
         
         
            
         </table>    
            </div>
            <br />
           
        </div>

        <div id="dvContentFooter" class="dvContentFooter" align="center">
            <table align="center">
                <tr>
                    <td></td>
                     <td>
                                <asp:DropDownList ID="ddlReportViewType" runat="server" CssClass="dropDownList">
                                        <asp:ListItem Value="0">Screen</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="1">PDF</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                     <td>
                                    <asp:DropDownList ID="ddlReportViewMode" runat="server" CssClass="dropDownList">
                                        <asp:ListItem Value="0">In This Tab</asp:ListItem>
                                        <asp:ListItem Value="1">In New Tab</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="2">In New Window</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                   
                    
                   
                    <td>
                       
                    </td>
                    <td>
                        <%--<uc:PrintButton runat="server" ID="ucPrintButton" DefaultPrintAction="Preview" AutoPrint="False" />--%>
                    </td>
                    <td>
                      
                    </td>
                    <td></td>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="Print Format:"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlReportFormat" runat="server" CssClass="dropDownList" Width="100px">
                        </asp:DropDownList>
                    </td>
                    <td>
                      <%--  <asp:Button ID="btnPopupTrigger" runat="server" Text="Button" CssClass="buttonHidden" />--%>
                       <%-- <asp:HiddenField ID="hdnPopupTriggerID" runat="server" Value="" />
                        <asp:HiddenField ID="hdnPopupCommand" runat="server" Value="" />--%>
                    </td>
                </tr>
            </table>
        </div>

       


    </div>


</asp:Content>
