<%@ Page Title="" Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="BOMList.aspx.cs" EnableViewState="false"   Inherits="PG.Web.Production.BOMList" %>
<%@ Register assembly="DevExpress.Web.v14.2, Version=14.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web"  tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script src="../javascript/jquery.ui.combogrid.js" type="text/javascript"></script>
    <script src="../javascript/jquery.attributeobserver.js" type="text/javascript"></script>
    <link href="../css/jquery.ui.combogrid.css" rel="stylesheet" type="text/css" />
  

    <script language="javascript" type="text/javascript">

        var hdnDeptID = '<%=hdnDeptID.ClientID %>';
        var txtDeptName = '<%=txtDeptName.ClientID %>';
        var btnDeptID = '<%= btnDeptID.ClientID%>';

        var ItemListServiceLinkd = '<%=this.ItemListServiceLinkd%>';
        var GetStorageLocationList = '<%=this.GetStorageLocationList%>';

        var DeptListLinkd = '<%=this.DeptListLinkd%>';
        var hdnLoggedInUser='<%=hdnLoggedInUser.ClientID%>';
        var hdnItemID = '<%=hdnItemID.ClientID %>';
        var txtItemName = '<%=txtItemName.ClientID %>';
        var btnItemID = '<%= btnItemID.ClientID%>';

        var txtStorageloc = '<%=txtStorageloc.ClientID %>';
        var btnStorageLoc = '<%=btnStorageLoc.ClientID %>';
        var hdnStorageLocID = '<%= hdnStorageLocID.ClientID%>';

        var isPageResize = true;
        ContentForm.CalendarImageURL = "../image/calendar.png";

        function ShowProgress() {
            $('#' + updateProgressID).show();
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

        $(document).ready(function () {
            //$('#' + txtGridPageNo).keydown(function (e) {
            //    if (e.keyCode == 13) {
            //        e.preventDefault();
            //        $('#' + btnGridPageGoTo).click();
            //    }
            //});


            if ($('#' + txtDeptName).is(':visible')) {
                // alert(3);
                bindDeptList();
                // alert(4);
            }

            if ($('#' + txtItemName).is(':visible')) {
                // alert(3);
                bindItemList();
                // alert(4);
            }
            if ($('#' + txtStorageloc).is(':visible')) {

                bindStorageLocList();

            }
        });


        function tbopen(key, userid) {
            key = key || '';
            var url = '';
           
                  url = IForm.RootPath + "Production/BOMEntryDeptWise.aspx?id=" + key;
                //var url = IForm.RootPath + "Accounting/GeneralLedger/JournalFull.aspx?id=" + key;


                if (IForm.PageMode == Enums.PageMode.InTab) {

                    var tdata = new xtabdata();
                    tdata.linktype = Enums.LinkType.Direct;
                    tdata.id = 0;
                    tdata.name = "BOM Entry";
                    //tdata.label = "User: " + userid;
                    tdata.label = "BOM Entry";
                    tdata.type = 0;
                    tdata.url = url;
                    tdata.tabaction = Enums.TabAction.InNewTab;
                    tdata.selecttab = 1;
                    tdata.reload = 0;
                    tdata.param = "";


                    try {
                        var vdeptid = $('#' + hdnDeptID).val();
                       
                        if (vdeptid > 0)
                        {
                        window.parent.TabMenu.OpenMenuByData(tdata);
                        }
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
                    var vdeptid = $('#' + hdnDeptID).val();
                    var newServiceURL = itemServiceURL + "&deptid=" + vdeptid;
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
                    
                    var userid = $('#' + hdnLoggedInUser).val();
                   
                    var newServiceURL = itemServiceURL + "&userid=" + userid;
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
                    $('#' + hdnDeptID).val('0');
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

                    var vdeptid = $('#' + hdnDeptID).val();
                    //var deptName = $('#' + txtDeptName).val();
                    var newServiceURL = itemServiceURL + "&deptid=" + vdeptid;

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
                            <asp:HiddenField ID="HiddenField1" runat="server" Value="0"  />
                                  <asp:HiddenField ID="hdnCompanyID" runat="server" Value ="0" />
                                <asp:HiddenField ID="hdnLoggedInUser" runat="server" Value ="0" />
                            <asp:HiddenField ID="hdnDeptID" runat="server" Value="0" />
    <asp:HiddenField ID="hdnStorageLocID" runat="server" Value="0"  />

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
                     <table style="width : 800px">
                       <tr>

                        <td align="right" class="auto-style2">
                            <asp:Label ID="lblDEPT_ID" runat="server" Text="Department : " Width="100px" style="font-weight: 700"></asp:Label>
                        </td>
                        <td class="auto-style2">
                           <%-- <asp:DropDownList ID="ddlDEPT_ID" runat="server" CssClass="dropDownList required"   Width="150px" ></asp:DropDownList>--%>

                             <asp:TextBox ID="txtDeptName" runat="server" CssClass="textBox required" Enabled="true"></asp:TextBox> 
                                                 <input id="btnDeptID" type="button" value="" runat="server" class="buttonDropdown" tabindex="-1" />
                          
                        </td>
                        <td align="right"> 
                             <asp:Label ID="lblisActive" runat="server" Text="IsActive:" Font-Bold="true"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlIsActive" runat="server" CssClass="dropDownList " >
                                                <asp:ListItem Text="All" Value="0"></asp:ListItem>
                                                <asp:ListItem Selected="True" Text="Active" Value="Y"></asp:ListItem>
                                                <asp:ListItem Text="InActive" Value="N"></asp:ListItem>
                                            </asp:DropDownList>
                        </td>
                           <td>
                               <asp:Button ID="btnAddNew" runat="server" CssClass="buttonNew" Text="New Loading"   Height="26px" OnClick="btnAddNew_Click" />
                           </td>
                    </tr>
                         <tr class="rowParam">
                                           
                                            <td style="" align="right">
                                                <asp:Label ID="lblItemName" runat="server" Text="Item Name:" Visible="True" style="font-weight: 700"></asp:Label>
                                            </td>
                                            <td>
                                               <asp:TextBox ID="txtItemName" runat="server" CssClass="textBox required" Enabled="true"></asp:TextBox> 
                                                 <input id="btnItemID" type="button" value="" runat="server" class="buttonDropdown" tabindex="-1" />
                                                <asp:HiddenField ID="hdnItemID" runat="server" Value="0"  />
                                            </td>
                                           <td style="" align="right">
                                                <asp:Label ID="lblStorageLoc" runat="server" Text="Storage Loc Name:" Visible="True" style="font-weight: 700"></asp:Label>
                                            </td>
                                            <td>
                                               <asp:TextBox ID="txtStorageloc" runat="server" CssClass="textBox required" Width="100px" Enabled="true"></asp:TextBox> 
                                                 <input id="btnStorageLoc" type="button" value="" runat="server" class="buttonDropdown" tabindex="-1" />

                                            </td>
                                            
                                            <td>
                                                <asp:Button ID="btnUpload" runat="server" CssClass="buttonSearch" Style="padding-left: 22px;"
                                Text="Show Data" OnClick="btnUpload_Click"  />
                                            </td>
                                        </tr>
                         

                         

             
                         </table>
                    </div>
           
             
                    
             <div id="dvControls" style="width: 100%; height : auto;">
                <div id="dvControlsInner" class="groupBoxContainer boxShadow" style="width: 80%; height : auto;">
                 
                        <div id="dvGrid" style="width: 98%; height: 95%; overflow: auto;">
                              

                            <dx:ASPxGridView ID="grdBOMList" runat="server" AutoGenerateColumns="False" Width="98%"  ClientInstanceName="grdBOMList"  >
                                <Columns>
                                      <dx:GridViewDataTextColumn Caption="Action" UnboundType="String"  Width="55px" VisibleIndex="0">
                                        <DataItemTemplate>
                                            <dx:ASPxHyperLink ID="hyperLink"  runat="server" OnInit="hyperLink_Init">
                                            </dx:ASPxHyperLink>
                                        </DataItemTemplate>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="BOM NO" Name="lblBOM_NO" VisibleIndex="2"  Width="150px" FieldName="BOM_NO">
                                    </dx:GridViewDataTextColumn>
                                     <dx:GridViewDataTextColumn Caption="BOM Name" Name="lblBOM_ITEM_DESC" VisibleIndex="2"     FieldName="BOM_ITEM_DESC">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="BOM ITEM ID" Name="lblBOM_ITEM_ID" Width="80px" VisibleIndex="4" Visible="false" FieldName="BOM_ITEM_ID">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="ITEM NAME" Name="lblITEM_NAME" VisibleIndex="5"   FieldName="ITEM_NAME">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataDateColumn Caption="Create By" Name="lblFULL_NAME" VisibleIndex="6" Width="100px" FieldName="FULL_NAME">
                                    </dx:GridViewDataDateColumn>
                                    <dx:GridViewDataDateColumn Caption="Create Date" Name="lblCREATE_DATE" VisibleIndex="7" Width="200px" FieldName="CREATE_DATE">
                                        <PropertiesDateEdit DisplayFormatString="dd-MMM-yyyy" ></PropertiesDateEdit>
                                    </dx:GridViewDataDateColumn>
                                    <dx:GridViewDataTextColumn Caption="BOM_ID" FieldName="BOM_ID" Name="hdnBOM_ID"  Visible="false"  VisibleIndex="1">
                                    </dx:GridViewDataTextColumn>
                                </Columns>
                                <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
                                <SettingsSearchPanel Visible="True"  />
                                <SettingsPager AlwaysShowPager="True" PageSize="20">
                                </SettingsPager>
                                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="380"    />
                                <Styles>
                                    <Header CssClass="headerRow_Prod" HorizontalAlign="Center" Font-Bold="True" ForeColor="White" Font-Size="9pt">
                                         
                                    </Header>
                                    <AlternatingRow BackColor="#FFFFCC">
                                    </AlternatingRow>
                                    <HeaderPanel BackColor="#669999">
                                    </HeaderPanel>
                                </Styles>
                            </dx:ASPxGridView>

                        
                       
                        </div>
                        

                </div>
                    
            </div>

                    



             </div>
        </div>
</asp:Content>
