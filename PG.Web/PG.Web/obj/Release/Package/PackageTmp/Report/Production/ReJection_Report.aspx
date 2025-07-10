<%@ Page Title="" Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="ReJection_Report.aspx.cs" Inherits="PG.Web.Report.INV.ReJection_Report" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register assembly="DropDownCheckBoxes" namespace="Saplin.Controls" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <script src="../../javascript/jquery.ui.combogrid.js" type="text/javascript"></script>
    <script src="../../javascript/jquery.attributeobserver.js" type="text/javascript"></script>
    <link href="../../css/jquery.ui.combogrid.css" rel="stylesheet" type="text/css" />


    <script language="javascript" type="text/javascript">
        // <!CDATA[

        var isPageResize = true;
        ContentForm.CalendarImageURL = "../../image/calendar.png";

        var hdnCompanyID = '<%=hdnCompanyID.ClientID%>';

     var ReportViewPageLink = '<%=this.ReportViewPageLink%>';
      var ReportViewPDFPageLink = '<%=this.ReportViewPDFPageLink%>';
        var ReportPrintPageLink = '<%=this.ReportPrintPageLink%>';
        var ReportPDFPageLink = '<%=this.ReportPDFPageLink%>';

        var ItemGroupListServiceLink = '<%=this.ItemGroupListServiceLink%>';
        var ItemListServiceLink = '<%=this.ItemListServiceLink%>';


        var hdnGroupID = '<%= hdnGroupID.ClientID%>';
        var txtGroupName = '<%= txtGroupName.ClientID%>';
        var btnGroupID = '<%= btnGroupID.ClientID%>';

        var hdnItemID = '<%=hdnItemID.ClientID %>';
        var txtItemName = '<%=txtItemName.ClientID %>';
        var btnItemID = '<%= btnItemID.ClientID%>';



        var ifPrintButton = '<%=ifPrintButton.ClientID%>';


        var txtFromDateID = '<%=txtFromDate.ClientID%>';
        var txtToDateID = '<%=txtToDate.ClientID%>';


       <%-- var ddlLocation = '<%=ddlLocation.ClientID%>';--%>

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

        function showOverlay() {
            document.getElementById("overlay").style.display = "block";
        }

        function hideOverlay() {
            document.getElementById("overlay").style.display = "none";
        }


        function tbopen(key, pdfView, isPrint, isPDFAutoPrint, showWait) {
            hideOverlay();


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
            if (pdfView) {
                url = ReportViewPDFPageLink + "?rk=" + key + "&_tt=" + strTime;
            }
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

            hideOverlay();
        }





        function fromParent(val1) {
            alert('this is called from parent: ' + val1);
        }

        $(document).ready(function () {
            str = document.body.innerHTML


            //    $('#tblParam tr').each(function () {
            //        if ($(this).find('td:empty').length) $(this).remove();
            //    });

            $("#tblParam tr.rowParam").each(function () {
                var cell = $.trim($(this).find('td').text());
                if (cell.length == 0) {
                    //console.log('empty');
                    //$(this).addClass('nodisplay');
                    $(this).hide();
                }
            });

            hideOverlay();

        });



        $(document).ready(function () {

            
            //alert("error in page");
            if ($('#' + txtGroupName).is(':visible')) {
                //alert(1);
                bindGroupList();
                //alert(2);
            }
            //alert(3);
            if ($('#' + txtItemName).is(':visible')) {
                bindItemList();
            }
            //alert('OK 5');




        });


        function getCheckBoxes() {
            var checkValues = '';
            var comma = '';
            $('#' + ddlLocation + ' input:checked').each(function () {
                // selected.push($(this).attr('name'));
                checkValues = checkValues + comma + $(this).val();
                comma = ',';
            });

            /// alert(checkValues);
            return checkValues;

        }

        //this is for group dropdown
        function bindGroupList() {
            var cgColumns = [
                             //{ 'columnName': 'itemgroupid', 'width': '80', 'align': 'left', 'highlight': 2, 'label': 'Group ID' }
                             { 'columnName': 'itemgroupcode', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Code' }
                            , { 'columnName': 'itemgroupdesc', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                            , { 'columnName': 'itemgroupnameparent', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Group Parent' }


            ];
            var serviceURL = ItemGroupListServiceLink + "?isterm=1&includeempty=0&hasitem=1&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;

            serviceURL += "&ispaging=1";
            var groupIDElem = $('#' + txtGroupName);

            $('#' + btnGroupID).click(function (e) {
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
                width: 600,
                url: serviceURL,
                search: function (event, ui) {
                    //var companyCode = $('#' + ddlCompany).val();
                    //var branchCode = $('#' + hdnBranch).val();
                    //var deptCode = $('#' + hdnDepartment).val();
                    //var locationid = $('#' + lblLocationID).val();
                    // var seid = $('#' + txtExecutiveID).val();
                    var newServiceURL = serviceURL;
                    $(this).combogrid("option", "url", newServiceURL);


                },
                select: function (event, ui) {
                    if (!ui.item) {
                        event.preventDefault();

                        $('#' + hdnGroupID).val('0');
                        //$('#' + txtDealerID).val('');
                        return false;
                        //ClearGLAccountData(elemID);
                    }


                    if (ui.item.itemgroupid == '') {
                        event.preventDefault();
                        return false;
                        //ClearGLAccountData(elemID);
                    }
                    else {
                        // $('#' + hdnDealerID).val(ui.item.dealerid);
                        $('#' + hdnGroupID).val(ui.item.itemgroupid);
                        $('#' + txtGroupName).val(ui.item.itemgroupdesc);
                        //$('#' + txtGroupCode).val(ui.item.itemgroupcode);

                    }
                    return false;
                },

                lc: ''
            });

            $(groupIDElem).blur(function () {
                var self = this;
                elemID = $(groupIDElem).attr('id');
                eCode = $(groupIDElem).val();
                isComboGridOpen = $(self).combogrid('isOpened');
                if (eCode == '') {
                    //                    if (!validateGLAccount(elemID, null)) {
                    //                        $(elem).val(prevGLCode);
                    //                        return false;
                    //                    }
                    $('#' + hdnGroupID).val('0');
                }
                else {
                    //grp = GetGLGroup(eCode);
                    ////                    if (!validateGLAccount(elemID, grp)) {
                    ////                        $(elem).val(prevGLCode);
                    ////                        return false;
                    ////                    }

                    if (grp == null) {
                        $('#' + hdnGroupID).val('0');
                    }
                    else {
                        //SetItemGroupData(elemID, grp);
                    }
                }
                //setDetInstrument(hdnIsInstrumentElem, txtInstrumentElem, btnInstrumentElem);
                grpID = $(self).closest('tr').find('input[id$="hdnGroupID"]').val();
                if (grpID == '0' | grpID == '') {
                    $(self).addClass('textError');
                }

            });


            $(groupIDElem).blur(function () {
                var self = this;

                var groupID = $(groupIDElem).val();
                if (groupID == '') {
                    // $('#' + hdnDealerID).val('0');
                    $('#' + txtGroupName).val('');
                    //$('#' + txtGroupCode).val('');
                }
            });
        }

        //this is for item dropdown
        function bindItemList() {

            var cgColumns = [
                              { 'columnName': 'itemname', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                             , { 'columnName': 'itemcode', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Code' }
                             , { 'columnName': 'itemgroupdesc', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Group Name' }
                             , { 'columnName': 'uomname', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Uom Name' }
                              , { 'columnName': 'class_name', 'width': '120', 'align': 'left', 'highlight': 4, 'label': 'Class Name' }
                             , { 'columnName': 'item_type', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Item Type' }
            ];

            var itemServiceURL = ItemListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;

            itemServiceURL += "&ispaging=1";
            var itemIDElem = $('#' + txtItemName);

            $('#' + btnItemID).click(function (e) {
                $(itemIDElem).combogrid("dropdownClick");
            });

            $(itemIDElem).combogrid({
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
                url: itemServiceURL,
                search: function (event, ui) {
                    //var companyCode = $('#' + ddlCompany).val();
                    //var branchCode = $('#' + hdnBranch).val();
                    //var deptCode = $('#' + hdnDepartment).val();
                    //var locationid = $('#' + lblLocationID).val();
                    // var seid = $('#' + txtExecutiveID).val();
                    var vgroupid = $('#' + hdnGroupID).val();
                    var newServiceURL = itemServiceURL + "&groupid=" + vgroupid;
                    $(this).combogrid("option", "url", newServiceURL);


                },
                select: function (event, ui) {
                    if (!ui.item) {
                        event.preventDefault();

                        $('#' + hdnGroupID).val('0');
                        $('#' + hdnItemID).val('0');
                        return false;
                        //ClearGLAccountData(elemID);
                    }


                    if (ui.item.itemid == '') {
                        event.preventDefault();
                        return false;
                        //ClearGLAccountData(elemID);
                    }
                    else {
                        // $('#' + hdnDealerID).val(ui.item.dealerid);
                        $('#' + hdnItemID).val(ui.item.itemid);
                        $('#' + txtItemName).val(ui.item.itemname);

                        //$('#' + hdnUomID).val(ui.item.itemid);
                        //$('#' + hdnUomName).val(ui.item.uomname);

                        //$('#' + txtGroupCode).val(ui.item.itemgroupcode);

                    }
                    return false;
                },

                lc: ''
            });


            $(itemIDElem).blur(function () {
                var self = this;
                var groupID = $(itemIDElem).val();
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
        .dvGroup
        {
            width: 182px;
            height: 20px;
            border: 1px solid lightgrey;
        }
        
        
        .dvGroupListPopup
        {
            display: none;
            height: 0px;
            width: 0px;
        }
        
        
        .textPopup
        {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            border: 1px #1B68B8 solid;
            background-color: #FFFFFF;
            color: #000000;
            font-size: 11px;
            width: 160px;
            height: 16px;
            padding-left: 2px;
        }
        
        .btnPopup
        {
            height: 20px;
            width: 16px;
            background-image: url(/image/dropdown.gif);
            background-repeat: no-repeat;
            background-position: center bottom;
            cursor: pointer;
        }
        
        .btnPopup:hover
        {
            background-image: url(/image/dropdown_over.gif);
        }
        
        .dvSpacer
        {
            height: 10px;
            width: 100%;
        }
        
        
        .dvReportList
        {
            height: 100%;
            width: 100%;
            overflow: auto;
        }
        .dvParam
        {
            height: 100%;
            width: 100%;
            overflow: auto;
        }
        
        
        .tblParam
        {
            /* border-collapse: collapse;    */
            height: auto;
        }
        
        

        
        .tblParam td
        {
            height: auto;
        }
        
        
        .cboYesNo
        {
            width: 50px;
        }
        
        .tdSpacer
        {
            width: 10px;
        }
        
        .rowParam
        {
        }
        
        .rowSpacer
        {
            height:20px;
        }
        
        
        .dvPrintIFrame
        {
            height: 0px;
            width: 0px;
        }

        .overlay {
        background-color: #000;
        cursor: wait;
        display: none;
        height: 100%;
        left: 0;
        opacity: 0.4;
        position: fixed;
        top: 0;
        width: 100%;
        z-index: 9999998;
    }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="dvPageContent" style="width: 100%; height: 100%;">
        <div id="dvContentHeader" class="dvContentHeader">
            <div id="dvHeader" class="dvHeader_Prod">
                
            </div>
            <div id="dvMessage" class="dvMessage">
                <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
            </div>
            <div id="dvHeaderControl" class="dvHeader_Prod">
            </div>
        </div>
        <div id="dvContentMain" class="dvContentMain">
            <div id="dvContentMainInner" class="dvContentMainInner_Prod" style="height: 100%;">
                <div id="dvTop" style="height: auto; width: 100%;">
                    <table cellspacing="0" cellpadding="0" style="height: auto; width: 100%;">
                        <tr>
                            <td>
                            </td>
                          
                            <td style="border-left: 1px solid grey;">
                            </td>
                            <td valign="top" style="">
                                <div id="dvParamHeader" class="dvParamHeader" style="height: auto; width: 100%;">
                                    <table cellspacing="0" cellpadding="0" border="0" style="height: auto; width: 100%;">
                                        <tr>
                                            <td style="border-bottom: 1px solid grey;">
                                                <asp:Label ID="lblReportName" runat="server" Text="Rejection Report" Font-Bold="True"
                                                    Font-Size="10pt"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="dvMiddle" style="height: auto; width: 100%">
                    <table id="tblMiddle" cellspacing="0" cellpadding="0" style="height: 100%; width: 100%;
                        min-width: 700px;">
                        <tr style="height: 100%">                        
                         
                            <td valign="top" style="height: 100%;">
                                <div id="dvParam" class="dvParam">
                                    <table id="tblParam" cellspacing="4" cellpadding="2" border="0" class="tblParam">
                                        <tr>
                                            <td style="width: 200px;">
                                                <div class="dvSpacer">
                                                </div>
                                            </td>
                                            <td>
                                                <asp:HiddenField ID="hdnCompanyID" runat="server" Value="0" />
                                               <%-- <asp:HiddenField ID="hdnEmpCode" runat="server" Value="" />--%>
                                            </td>
                                        </tr>

                                          <tr class="rowParam">
                                            <td style="" align="right">
                                                <asp:Label ID="lblReportType" runat="server" Text="Report Type:" Visible="True"></asp:Label>
                                             </td>
                                            <td>
                                                <asp:DropDownList ID="ddlReportType" runat="server" Width="180px" CssClass="dropDownList required">
                                                   <%-- <asp:ListItem  Value="0" Text="Summary"></asp:ListItem>--%>
                                                    <asp:ListItem Selected="True"  Value="1" Text="Details"></asp:ListItem>
                                                </asp:DropDownList>
                                             </td>
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                            </td>
                                            <td>
                                            </td>
                                        </tr>

                                         <tr class="rowParam">
                                            <td style="" align="right">
                                                <asp:Label ID="lblItemType0" runat="server" Text="Department:" Visible="True"></asp:Label>
                                             </td>
                                            <td>
                                                <asp:DropDownList ID="ddlDepartment" runat="server" Width="180px" CssClass="dropDownList required">
                                                </asp:DropDownList>
                                             </td>
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                            </td>
                                            <td>
                                            </td>
                                        </tr>



                                        <tr class="rowParam">
                                            <td style="" align="right">
                                                <asp:Label ID="lblItemClass" runat="server" Text="Item Class:" Visible="True"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlItemClass" runat="server" Width="180px" CssClass="dropDownList" Visible="True">
                                                    <%--<asp:ListItem Selected="True" Text="List" Value="0"> </asp:ListItem>--%>
                                                </asp:DropDownList>

                                            </td>
                                            <td>
                                                
                                            </td>
                                            <td style="" align="right">
                                            </td>
                                            <td>
                                            </td>
                                        </tr>


                                       


                                          <tr class="rowParam">
                                            <td style="" align="right">
                                                <asp:Label ID="lblItemGroup" runat="server" Text="Item Group:" Visible="True"></asp:Label>
                                            </td>
                                            <td>
                                               <asp:TextBox ID="txtGroupName" runat="server" CssClass="textBox required" Enabled="true"></asp:TextBox>
                                                <input id="btnGroupID" type="button" value="" runat="server" class="buttonDropdown" tabindex="-1" />

                                            </td>
                                            <td>
                                                 <asp:HiddenField ID="hdnGroupID" runat="server" Value="0"  />
                                            </td>
                                            <td style="" align="right">
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                      

                                        <tr class="rowParam">
                                            <td style="" align="right">
                                                <asp:Label ID="lblItemName" runat="server" Text="Item Name:" Visible="True"></asp:Label>
                                            </td>
                                            <td>
                                               <asp:TextBox ID="txtItemName" runat="server" CssClass="textBox required" Enabled="true"></asp:TextBox> 
                                                 <input id="btnItemID" type="button" value="" runat="server" class="buttonDropdown" tabindex="-1" />

                                            </td>
                                            <td>
                                                 <asp:HiddenField ID="hdnItemID" runat="server" Value="0"  />
                                            </td>
                                            <td style="" align="right">
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        


                                     

                                        <tr class="rowParam">
                                            <td align="right">
                                                <asp:Label ID="lblFromDate" runat="server" Text="Date From:"></asp:Label>
                                            </td>
                                            <td>
                                                <table cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txtFromDate" runat="server" Width="85px" CssClass="textBox textDate dateParse"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 4px;">
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblToDate" runat="server" Text="To:"></asp:Label>
                                                        </td>
                                                        <td style="width: 2px;">
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtToDate" runat="server" Width="85px" CssClass="textBox textDate dateParse"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td align="right" class="">
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>

                                     
                                        

                                       
                                    </table>
                                </div>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div id="dvContentFooter" class="dvContentFooter">
            <div id="dvContentFooterInner" class="dvContentFooterInner">
                <div style="width: 100%; height: 100%; margin-bottom: 0px;">
                    <div style="width: auto; min-width: 300px; height: auto; text-align: left;">
                        <table border="0">
                            <tr>
                                <td style="width: 100px;">
                                
                                </td>
                                <td>
                                    <asp:Label ID="Label2" runat="server" Text="Report View"></asp:Label>
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
                                    <asp:Label ID="Label1" runat="server" Text="Type:"></asp:Label>
                                </td>
                                <td>
                                <asp:DropDownList ID="ddlReportViewType" runat="server" CssClass="dropDownList">
                                        <asp:ListItem Value="0">Screen</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="1">PDF</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <asp:Button ID="btnView" runat="server" Text="View Report" Width="100px" CssClass="buttoncommon buttonPrintPreview"
                                        OnClick="btnView_Click" OnClientClick="showOverlay();" />
                                </td>
                                <td>
                                    <asp:Button ID="btnPrint" runat="server" Text="Print Report" Width="100px" CssClass="buttoncommon buttonPrint"
                                        OnClick="btnPrint_Click" OnClientClick="showOverlay();" Visible="False" />
                                </td>
                                <td style="width: 20px;">
                                </td>
                                <td>
                                    <asp:Label ID="Label3" runat="server" Text="Get Report As:"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlExport" runat="server" Width="70" CssClass="dropDownList">
                                        <asp:ListItem Selected="True" Value="0">PDF</asp:ListItem>
                                        <asp:ListItem Value="1">Excel</asp:ListItem>
                                        <asp:ListItem Value="2">Word</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:Button ID="btnExport" runat="server" Text="Get Report" Width="100px" CssClass="buttoncommon buttonExport"
                                        OnClick="btnExport_Click" OnClientClick="showOverlay();" />
                                </td>
                                <td style="width: 10px;">
                                </td>
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


    <div id="overlay" class="overlay">
         <div style="margin:auto;width:200px;height:400px;background-color:black;border:solid 1px black;
                  text-align:center; vertical-align:middle;"> 
           <span style="color:white; font-size:medium;" >Please Wait...</span>
             <br />
             <img alt="" src="../../image/progress.gif" />
         </div>
    </div>
</asp:Content>

