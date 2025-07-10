<%@ Page Title="" Language="C#"  MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="Production_BomList.aspx.cs" Inherits="PG.Web.Report.Production.Production_BomList" %>

<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
       <script src="../../javascript/jquery.ui.combogrid.js" type="text/javascript"></script>
    <script src="../../javascript/jquery.attributeobserver.js" type="text/javascript"></script>
    <link href="../../css/jquery.ui.combogrid.css" rel="stylesheet" type="text/css" />

     <script type="text/javascript">
         ContentForm.CalendarImageURL = "../../image/calendar.png";
         var ReportViewPageLink = '<%=this.ReportViewPageLink%>';
         var ReportViewPDFPageLink = '<%=this.ReportViewPDFPageLink%>';
         var ReportPrintPageLink = '<%=this.ReportPrintPageLink%>';
         var ReportPDFPageLink = '<%=this.ReportPDFPageLink%>';

         var ItemListServiceLinkd = '<%=this.ItemListServiceLinkd%>';
         var DeptListLinkd = '<%=this.DeptListLinkd%>';
         var GetStorageLocationList = '<%=this.GetStorageLocationList%>';

         var hdnItemID = '<%=hdnItemID.ClientID %>';
         var txtItemName = '<%=txtItemName.ClientID %>';
         var btnItemID = '<%= btnItemID.ClientID%>';

         <%--var ddlDEPT_ID = '<%=ddlDEPT_ID.ClientID%>';--%>

        <%-- var hdnDeptID = '<%=hdnDeptID.ClientID %>';
         var txtDeptName = '<%=txtDeptName.ClientID %>';--%>
         var ddlDepartment = '<%= ddlDepartment.ClientID%>';

         var txtStorageloc = '<%=txtStorageloc.ClientID %>';
         var btnStorageLoc = '<%=btnStorageLoc.ClientID %>';
         var hdnStorageLocID = '<%= hdnStorageLocID.ClientID%>';
         
         
         

        var timeout;
        function scheduleGridUpdate(grid) {
            window.clearTimeout(timeout);
            timeout = window.setTimeout( 
                function() { grid.Refresh(); },
                2000
            );
        }
        function grid_Init(s, e) {
            scheduleGridUpdate(s);
        }
        function grid_BeginCallback(s, e) {
            window.clearTimeout(timeout);
        }
        function grid_EndCallback(s, e) {
            scheduleGridUpdate(s);
        }




        function ShowProgress() {
            $('#' + updateProgressID).show();
        }

        //function UserConfirmation() {
        //    return confirm("Are you sure you want to Stock Issue and Print DC,GP ?");
        //}


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


        //function tbopen(key, userid) {
        //    key = key || '';

        //    var url = IForm.RootPath + "Inventory/NewDCGPAgainstInvoice.aspx?id=" + key;
        //    //var url = IForm.RootPath + "Accounting/GeneralLedger/JournalFull.aspx?id=" + key;


        //    if (IForm.PageMode == Enums.PageMode.InTab) {

        //        var tdata = new xtabdata();
        //        tdata.linktype = Enums.LinkType.Direct;
        //        tdata.id = 0;
        //        tdata.name = "DC and GP Against Invoice";
        //        //tdata.label = "User: " + userid;
        //        tdata.label = "DC and GP Against Invoice";
        //        tdata.type = 0;
        //        tdata.url = url;
        //        tdata.tabaction = Enums.TabAction.InNewTab;
        //        tdata.selecttab = 1;
        //        tdata.reload = 0;
        //        tdata.param = "";


        //        try {
        //            window.parent.TabMenu.OpenMenuByData(tdata);
        //        }
        //        catch (err) {
        //            alert("error in page");
        //        }
        //    }
        //    else {
        //        //on new window/tab
        //        //window.open(url,'_blank');   

        //        window.location = url;
        //    }
        //}


        $(document).ready(function () {


            //alert("error in page");
            //if ($('#' + txtGroupName).is(':visible')) {
            //    //alert(1);
            //    bindGroupList();
            //    //alert(2);
            //}


            //if ($('#' + txtDeptName).is(':visible')) {
            //    // alert(3);
            //    bindDeptList();
            //    // alert(4);
            //}
           
            if ($('#' + txtItemName).is(':visible')) {
               // alert(3);
                bindItemList();
               // alert(4);
            }
            //alert('OK 5');

            //bindDeptList();

            if ($('#' + txtStorageloc).is(':visible')) {
               
                bindStorageLocList();
               
            }


        });

       

        function bindItemList() {

            var cgColumns = [
                              { 'columnName': 'itemname', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                             , { 'columnName': 'itemcode', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Code' }
                             , { 'columnName': 'itemgroupdesc', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Group Name' }
                             , { 'columnName': 'uomname', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Uom Name' }
                              , { 'columnName': 'class_name', 'width': '120', 'align': 'left', 'highlight': 4, 'label': 'Class Name' }
                             , { 'columnName': 'item_type', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Item Type' }
            ];

            var itemServiceURL = ItemListServiceLinkd + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;

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
                    var vdeptid = $('#' + ddlDepartment).val();
                    var newServiceURL = itemServiceURL+ "&deptid=" + vdeptid;
                    $(this).combogrid("option", "url", newServiceURL);


                },
                select: function (event, ui) {
                    if (!ui.item) {
                        event.preventDefault();

                       // $('#' + hdnGroupID).val('0');
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


        function bindDeptList() {

            var cgColumns = [
                              { 'columnName': 'deptname', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                             , { 'columnName': 'deptcode', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Code' }
                            
            ];

            var itemServiceURL = DeptListLinkd + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;

            itemServiceURL += "&ispaging=1";
            var deptIDElem = $('#' + txtDeptName);

            $('#' + btnDeptID).click(function (e) {
                $(deptIDElem).combogrid("dropdownClick");
            });

            $(deptIDElem).combogrid({
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
                    //var vdeptid = $('#' + ddlDEPT_ID).val();
                    //var deptName = $('#' + txtDeptName).val();
                    var newServiceURL = itemServiceURL ;//+ "&deptid=" + vdeptid;
                  
                    $(this).combogrid("option", "url", newServiceURL);


                },
                select: function (event, ui) {
                    if (!ui.item) {
                        event.preventDefault();

                        // $('#' + hdnGroupID).val('0');
                        $('#' + hdnDeptID).val('0');
                        return false;
                        //ClearGLAccountData(elemID);
                    }


                    if (ui.item.deptid == '') {
                        event.preventDefault();
                        return false;
                        //ClearGLAccountData(elemID);
                    }
                    else {
                        // $('#' + hdnDealerID).val(ui.item.dealerid);
                        $('#' + hdnDeptID).val(ui.item.deptid);
                        $('#' + txtDeptName).val(ui.item.deptname);

                        //$('#' + hdnUomID).val(ui.item.itemid);
                        //$('#' + hdnUomName).val(ui.item.uomname);

                        //$('#' + txtGroupCode).val(ui.item.itemgroupcode);

                    }
                    return false;
                },

                lc: ''
            });


            $(deptIDElem).blur(function () {
                var self = this;
                var groupID = $(deptIDElem).val();
                if (groupID == '') {
                    // $('#' + hdnDealerID).val('0');
                    $('#' + txtDeptName).val('');
                    //$('#' + txtGroupCode).val('');
                }
            });
        }

        function bindStorageLocList() {

            var cgColumns = [
                              { 'columnName': 'stlmdesc', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                             

            ];

            var itemServiceURL = GetStorageLocationList + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;

            itemServiceURL += "&ispaging=1";
            var storagelocIDElem = $('#' + txtStorageloc);

            $('#' + btnStorageLoc).click(function (e) {
                $(storagelocIDElem).combogrid("dropdownClick");
            });

            $(storagelocIDElem).combogrid({
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
                width: 250,
                url: itemServiceURL,
                search: function (event, ui) {
                    
                    var vdeptid = $('#' + ddlDepartment).val();
                    //var deptName = $('#' + txtDeptName).val();
                    var newServiceURL = itemServiceURL+ "&deptid=" + vdeptid;

                    $(this).combogrid("option", "url", newServiceURL);


                },
                select: function (event, ui) {
                    if (!ui.item) {
                        event.preventDefault();

                        // $('#' + hdnGroupID).val('0');
                        $('#' + hdnStorageLocID).val('0');
                        return false;
                        //ClearGLAccountData(elemID);
                    }


                    if (ui.item.stlmid == '') {
                        event.preventDefault();
                        return false;
                        //ClearGLAccountData(elemID);
                    }
                    else {
                      
                        $('#' + hdnStorageLocID).val(ui.item.stlmid);
                        $('#' + txtStorageloc).val(ui.item.stlmdesc);

                        

                    }
                    return false;
                },

                lc: ''
            });


            $(storagelocIDElem).blur(function () {
                var self = this;
                var groupID = $(storagelocIDElem).val();
                if (groupID == '') {
                    // $('#' + hdnDealerID).val('0');
                    $('#' + txtStorageloc).val('');
                    //$('#' + txtGroupCode).val('');
                }
            });
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

       <div id="dvPageContent" style="width: 100%; height: auto;">
        <div id="dvContentHeader" class="dvContentHeader">
            <div id="dvHeader" class="dvHeader_Prod">
                <asp:Label ID="lblHeader" CssClass="lblHeader" runat="server" Text="BOM List"></asp:Label>
            </div>
            <!--Message Div -->
            <div id="dvMsg" runat="server" class="dvMessage" style="">
                <asp:Label ID="lblMessage" runat="server" Font-Size="Small" Width="100%"> </asp:Label>
            </div>
            <div id="dvHeaderControl" class="dvHeaderControl">
            </div>
        </div>
        <div id="dvContentMain" class="dvContentMain">
            <div id="dvControlsHead" style="height: auto; width: 100%;">
                <table>
                   
                     
                   <%--<tr class="rowParam">
                                           
                                            <td style="" align="right">
                                                <asp:Label ID="lblDeptName" runat="server" Text="Department Name:" Visible="True" style="font-weight: 700"></asp:Label>
                                            </td>
                                            <td>
                                               <asp:TextBox ID="txtDeptName" runat="server" CssClass="textBox required" Enabled="true"></asp:TextBox> 
                                                 <input id="btnDeptID" type="button" value="" runat="server" class="buttonDropdown" tabindex="-1" />

                                            </td>
                                            <td>
                                                 <asp:HiddenField ID="hdnDeptID" runat="server" Value="0"  />
                                            </td>
                                            <td style="" align="right">
                                            </td>
                                            <td>
                                            </td>
                                        </tr>--%>
                     <tr class="rowParam">
                                            <td style="" align="right">
                                                <asp:Label ID="lblItemType0" runat="server" Text="Department:" Visible="True" Font-Bold="true"></asp:Label>
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
                                                <asp:Label ID="lblItemName" runat="server" Text="Item Name:" Visible="True" style="font-weight: 700"></asp:Label>
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


                    <tr>
                        <td align="right">
                            <asp:Label ID="lblDateFrom" runat="server" Text="Bom Date From:" Font-Bold="true" Visible="false"></asp:Label>
                        </td>
                        <td colspan="2">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtFromDate" runat="server" Width="80px" CssClass="textBox textDate dateParse" Visible="false"></asp:TextBox>
                                    </td>
                                    <td style="width: 4px;">
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="lblToDate" runat="server" Text="Date To:" Font-Bold="true" Visible="false"></asp:Label>
                                    </td>
                                
                                    <td >
                                        <asp:TextBox ID="txtToDate" runat="server" Width="80px" CssClass="textBox textDate dateParse" Visible="false"></asp:TextBox>
                                    </td>
                                </tr>

                               

                            </table>
                        </td>
                  
                       
                       
                    </tr>

                     <tr>
                                    <td align="right">
                                   <asp:Label ID="lblisActive" runat="server" Text="IsActive:" Font-Bold="true"></asp:Label>
                                    </td>
                                      <td >
                                            <asp:DropDownList ID="ddlIsActive" runat="server" >
                                                <asp:ListItem Text="All" Value="0"></asp:ListItem>
                                                <asp:ListItem Selected="True" Text="Active" Value="Y"></asp:ListItem>
                                                <asp:ListItem Text="InActive" Value="N"></asp:ListItem>
                                            </asp:DropDownList>               
                                    </td>

                                                        <td >

                                                           
                                                        </td>
                                                        
                                                        <td >

                                                           
               
             
                                                        </td>
                    </tr>
                    <tr>
                        <td style="" align="right">
                                                <asp:Label ID="lblStorageLoc" runat="server" Text="Storage Loc Name:" Visible="True" style="font-weight: 700"></asp:Label>
                                            </td>
                                            <td>
                                               <asp:TextBox ID="txtStorageloc" runat="server" CssClass="textBox required" Enabled="true"></asp:TextBox> 
                                                 <input id="btnStorageLoc" type="button" value="" runat="server" class="buttonDropdown" tabindex="-1" />

                                            </td>
                                            <td>
                                                 <asp:HiddenField ID="hdnStorageLocID" runat="server" Value="0"  />
                                            </td>
                                            <td style="" align="right">
                                            </td>
                                            <td>
                                            </td>
                    </tr>

                     <tr>
                                    <td>
                                  
                                    </td>
                          <td >
                                                    <asp:Button ID="btnShow" runat="server" CssClass="buttonSearch" Style="padding-left: 22px;"
                                Text="Show Data" OnClick="btnShow_Click" Visible="false"  />  &nbsp; &nbsp; &nbsp; &nbsp;   
                               <asp:Button ID="btnShowPdf" runat="server" CssClass="buttonPrintPreview" Style="padding-left: 22px;"
                                Text="Show PDF Data" Width="120px" OnClick="btnShowPdf_Click"   />             
                                                        </td>

                        <td>
                                    <asp:Button ID="btnExportExcel" runat="server" Text="Get Excel" Width="100px" CssClass="buttoncommon buttonExcel"
                                         OnClick="btnExportExcel_Click" />
                                </td>
                                                        
                                                             <td style="padding-right: 4px">
                                                                 &nbsp;</td>
                                                           
                                                        
                                                        
                                                        <td >

                                                           
              
             
                                                        </td>
                    </tr>
                </table>
            </div>
            <div id="dvControls" style="width: 100%;">
             <%--   <div id="dvControlsInner" class="groupBoxContainer boxShadow" style="width: 1124px">
                    <div id="dvGridContainer" style="width: 100%; height: auto; text-align: left;">
                      
                        <div id="dvGrid" style="width: auto; height: 450px; overflow: auto;">
                            <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="ASPxGridView1" runat="server"  Width="100%" AutoGenerateColumns="False"  DataKeyNames="BOM_ID">
        
                          <Settings  ShowGroupPanel="True" ShowFilterBar="Visible" ShowFilterRow="True"  ShowFilterRowMenu="True" ShowGroupedColumns="True" ShowHeaderFilterButton="false" />
          
            <TotalSummary>
                <dx:ASPxSummaryItem FieldName="Size" SummaryType="Sum" />
            </TotalSummary>
            <GroupSummary>
                <dx:ASPxSummaryItem SummaryType="Count" />
            </GroupSummary>

            <Columns>
                <dx:GridViewDataTextColumn Caption="BOM Name" FieldName="BOM_ITEM_DESC" VisibleIndex="0" Width="150px">
                                <DataItemTemplate>
                                    <asp:LinkButton ID="lnkBillOfMaterial" runat="server" CommandArgs="BM" OnClick="lnkBillOfMaterial_Click" Text='<%# Bind("BOM_ITEM_DESC") %>'></asp:LinkButton>
                                </DataItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <CellStyle HorizontalAlign="Center"></CellStyle>
                            </dx:GridViewDataTextColumn>

                
                <dx:GridViewDataTextColumn FieldName="BOM_ID" VisibleIndex="1" Width="100px" Visible="false" />
                <dx:GridViewDataTextColumn Caption="BOM No" FieldName="BOM_NO" VisibleIndex="2" Width="150px" />
                <dx:GridViewDataTextColumn FieldName="BOM_ITEM_ID" VisibleIndex="3" Width="100px" Visible="false" />
                 <dx:GridViewDataTextColumn Caption="BOM Item Name" FieldName="ITEM_NAME" VisibleIndex="4" Width="150px" />
                 <dx:GridViewDataTextColumn Caption="Create By" FieldName="FULL_NAME" VisibleIndex="5" Width="100px" />
                <dx:GridViewDataTextColumn Caption="Create Date" FieldName="CREATE_DATE" VisibleIndex="6" Width="100px" />
                 
              
            </Columns>
            <SettingsPager NumericButtonCount="20">
                <PageSizeItemSettings Visible="true" Items="10,20,50" />
            </SettingsPager>
            <Settings ShowGroupPanel="True" VerticalScrollableHeight="250" />
            <SettingsDataSecurity AllowInsert="false" AllowEdit="false" AllowDelete="false" />
        </dx:ASPxGridView>

                <dx:ASPxButton ID="btnXlsExport" runat="server" Text="Export to XLS" UseSubmitBehavior="False"
                    OnClick="btnXlsExport_Click" />

                             <dx:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="ASPxGridView1"></dx:ASPxGridViewExporter>
                        </div>
                       
                    </div>
                </div>--%>
            </div>
        </div>
         <div id="dvContentFooter" class="dvContentFooter">
            <table>
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
                        <asp:Button ID="btnAddNew" runat="server" Text="New" CssClass="buttonNew" Visible="false"/>
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonCancel checkIsDirty"  Visible="false" />
                    </td>
                    <td>
                        <asp:Button ID="btnSave" runat="server" Text="Generate DC,GP" CssClass="buttonSave checkRequired" AccessKey="s" OnClientClick="if ( ! UserDeleteConfirmation()) return false;" Width="100px" Visible="false"   />
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="buttonEdit" Visible="false" />
                    </td>

                   

                    <td>
                        <asp:Button ID="btnPost" runat="server" Text="Post" CssClass="buttoncommon" Visible="false" />
                    </td>
                    <td>
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="buttonDelete" Visible="false" />
                    </td>
                    <td>
                        <asp:Button ID="btnRefresh" runat="server" Text="Refresh" CssClass="buttonRefresh checkIsDirty" Visible="false" />
                    </td>
                    <td>
                        <%--<uc:PrintButton runat="server" ID="ucPrintButton" DefaultPrintAction="Preview" AutoPrint="False" />--%>
                    </td>
                    <td>
                        <input id="btnClose" type="button" runat="server" class="buttonClose" value="Close" onclick="if (ContentForm) { ContentForm.CloseForm(); }"  />
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
                        <asp:Button ID="btnPopupTrigger" runat="server" Text="Button" CssClass="buttonHidden" />
                        <asp:HiddenField ID="hdnPopupTriggerID" runat="server" Value="" />
                        <asp:HiddenField ID="hdnPopupCommand" runat="server" Value="" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
   
</asp:Content>
