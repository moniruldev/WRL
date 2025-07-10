<%@ Page Title="" Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="User_Department_Permission.aspx.cs" Inherits="PG.Web.Production.User_Department_Permission" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="../javascript/jquery.ui.combogrid.js" type="text/javascript"></script>
    <script src="../javascript/jquery.attributeobserver.js" type="text/javascript"></script>
    <link href="../css/jquery.ui.combogrid.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" >

        var isPageResize = true;
        ContentForm.CalendarImageURL = "../image/calendar.png";
        var ItemListServiceLink = '<%=this.ItemListServiceLink%>';
        var DeptListLinkd = '<%=this.DeptListLinkd%>';
        var UserListServiceLink = '<%=this.UserListServiceLink%>';

        var txtUserName = '<%=txtUserName.ClientID%>';
        var btnUserID = '<%= btnUserID.ClientID%>';
        var hdnUserID = '<%=hdnUserID.ClientID %>';

        var txthDeptName = '<%=txthDeptName.ClientID%>';
        var btnhDeptName = '<%= btnhDeptName.ClientID%>';
        var hdnhDeptID = '<%=hdnhDeptID.ClientID %>';

        var gridViewIDDet = '<%=grdDeptItemMapping.ClientID%>';
        var gridUpdatePanelIDDet = '<%=UpdatePanel1.ClientID%>';
        var updateProgressID = '<%=UpdateProgress2.ClientID%>';


        
      <%--  var btnSave = '<%=btnSave.ClientID%>';--%>

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

        function ShowProgress() {
            $('#' + updateProgressID).show();
        }
        function UserSaveConfirmation() {
            return confirm("Are you sure you want to Save?");
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
        $(document).ready(function () {

            var pageInstance = Sys.WebForms.PageRequestManager.getInstance();

            pageInstance.add_pageLoaded(function (sender, args) {
                var panels = args.get_panelsUpdated();
                for (i = 0; i < panels.length; i++) {
                    if (panels[i].id == gridUpdatePanelIDDet) {
                        bindDeptList(gridViewIDDet);
                    }

                }

                bindDeptList(gridViewIDDet);
            });


            if ($('#' + txtUserName).is(':visible')) {
                
                bindUserList();
                
            }

            if ($('#' + txthDeptName).is(':visible')) {

                bindHeaderDeptList();

            }

        });


        function bindDeptList(gridViewID) {
            var cgColumns = [
                                { 'columnName': 'deptname', 'width': '280', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                               , { 'columnName': 'deptcode', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Code' }


            ];
            
            var serviceURL = DeptListLinkd + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            serviceURL += "&ispaging=1";
            
            var gridSelector = "#" + gridViewID;
            $(gridSelector).find('input[id$="txtDeptName"]').each(function (index, elem) {
                
                var elemRow = $(elem).closest('tr.gridRow');
                var hdnItemIDElem = $(elemRow).find('input[id$="txtDeptName"]');
                 
                $(elem).closest('tr').find('input[id$="btnDeptName"]').click(function (e) {
                    elmID = $(elem).attr('id');
                    $(elem).combogrid("dropdownClick");
                   
                });
               
               

                $(elem).combogrid({
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
                    width: 400,
                    url: serviceURL,
                    search: function (event, ui) {
                        var elemRowCur = $(elem).closest('tr.gridRow');
                        var newServiceURL = serviceURL;
                        
                        newServiceURL = JSUtility.AddTimeToQueryString(newServiceURL);
                        $(this).combogrid("option", "url", newServiceURL);


                    },

                    select: function (event, ui) {
                        
                        elemID = $(elem).attr('id');
                       
                        if (!ui.item) {
                            event.preventDefault();
                            ClearItemData(elemID);
                            return false;
                            
                        }
                        if (ui.item.id == 0) {
                            event.preventDefault();
                            return false;
                            
                        }
                        else {
                           
                            SetItemData(elemID, ui.item);
                        }
                        
                        return false;
                    }


                    // lc: ''
                });

                $(elem).blur(function () {
                    var self = this;
                    elemID = $(elem).attr('id');
                    eCode = $(elem).val();
                    isComboGridOpen = $(self).combogrid('isOpened');
                    if (eCode == '') {
                        ClearItemData(elemID);
                    }
                    else {
                        var serviceURL = DeptListLinkd + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
                        serviceURL += "&ispaging=1";

                        var prcNo = GetItemNo(eCode, serviceURL);

                        if (prcNo == null) {
                            ClearItemData(elemID);
                        }
                        else {
                            SetItemData(elemID, grp);
                        }

                    }
                });

            });

        }


        function SetItemData(txtItemCodeID, data) {
            $('#' + txtItemCodeID).val(data.deptid);
            var detRow = $('#' + txtItemCodeID).closest('tr.gridRow');
            $(detRow).find('input[id$="hdnDeptID"]').val(data.deptid);
            $(detRow).find('input[id$="hdnDeptCode"]').val(data.deptcode);
            $(detRow).find('input[id$="txtDeptName"]').val(data.deptname);

            
            
        }

        function ClearItemData(txtItemID) {
           
            var detRow = $('#' + txtItemID).closest('tr.gridRow');
            $(detRow).find('input[id$="hdnDeptID"]').val('0');
            $(detRow).find('input[id$="txtDeptName"]').val('');
            

        }
        function GetItemNo(eCode, serviceURL) {
            var prcNo = null;
            var isError = false;
            var isComplete = false;
            //ajax call

            var newServiceURL = serviceURL + " &selectedId=" + eCode;
            var dummyVar = $.ajax({
                type: "GET",
                cache: false,
                async: false,
                dataType: "json",
                url: newServiceURL,

                success: function (prddata) {
                    //            if (accdata.menu[0].count > 0) {
                    //                menu = menudata.menu[0];
                    //            }
                    if (prddata.rows.length > 0) {
                        prcNo = prddata.rows[0];
                    }
                },
                complete: function () {
                    if (!isError) {
                        //return;
                        //alert (menu.name);
                    }
                    isComplete = true;
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    isError = true;
                    alert(textStatus);
                }
            });
            return prcNo;
        }


        function bindUserList() {

            var cgColumns = [
                              { 'columnName': 'name', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'User ID' }
                             , { 'columnName': 'fullname', 'width': '200', 'align': 'left', 'highlight': 4, 'label': 'Full Name' }
                              , { 'columnName': 'designation', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Designation' }
                               
            ];

            var itemServiceURL = UserListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;

            itemServiceURL += "&ispaging=1";
            var userIDElem = $('#' + txtUserName);
            
            $('#' + btnUserID).click(function (e) {
                $(userIDElem).combogrid("dropdownClick");
            });

            $(userIDElem).combogrid({
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
                width: 400,
                url: itemServiceURL,
                search: function (event, ui) {
                   
                    var newServiceURL = itemServiceURL;
                    
                    $(this).combogrid("option", "url", newServiceURL);

                   
                },
                select: function (event, ui) {
                    if (!ui.item) {
                       
                        event.preventDefault();
                        $('#' + hdnUserID).val('0');
                        return false;
                       
                    }


                    if (ui.item.id == '') {
                       
                        event.preventDefault();
                        return false;
                        
                    }
                    else {
                       
                        $('#' + hdnUserID).val(ui.item.id);
                        $('#' + txtUserName).val(ui.item.name);

                    }
                    return false;
                },

                lc: ''
            });


            //$(userIDElem).blur(function () {
            //    var self = this;
            //    var groupID = $(userIDElem).val();
            //    if (groupID == '') {
            //         $('#' + hdnUserID).val('0');
            //        $('#' + txtUserName).val('');
                    
            //    }
            //});


            $(userIDElem).blur(function () {
                var self = this;
                elemID = $(userIDElem).attr('id');
                eCode = $(userIDElem).val();
                isComboGridOpen = $(self).combogrid('isOpened');
                if (eCode == '') {
                    $('#' + hdnUserID).val('0');
                    $('#' + txtUserName).val('');
                    
                    //ClearItemData(elemID);
                }
                else {
                    var itemServiceURL = UserListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;

                    itemServiceURL += "&ispaging=1";
                    

                    var prcNo = GetItemNo(eCode, itemServiceURL);
                    
                    if (prcNo == null) {
                        $('#' + hdnUserID).val('0');
                        $('#' + txtUserName).val('');
                        //ClearItemData(elemID);
                    }
                    else {
                        $('#' + hdnUserID).val(ui.item.id);
                        $('#' + txtUserName).val(ui.item.name);
                       // SetItemData(elemID, grp);
                    }

                }
            });
        }


        function bindHeaderDeptList() {

            var cgColumns = [
                               { 'columnName': 'deptname', 'width': '280', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                              , { 'columnName': 'deptcode', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Code' }


            ];

            var itemServiceURL = DeptListLinkd + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            
            itemServiceURL += "&ispaging=1";
            
            var userIDElem = $('#' + txthDeptName);

            $('#' + btnhDeptName).click(function (e) {
                $(userIDElem).combogrid("dropdownClick");
            });

            $(userIDElem).combogrid({
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
                width: 400,
                url: itemServiceURL,
                search: function (event, ui) {

                    var newServiceURL = itemServiceURL;

                    $(this).combogrid("option", "url", newServiceURL);


                },
                select: function (event, ui) {
                    if (!ui.item) {

                        event.preventDefault();
                        $('#' + hdnhDeptID).val('0');
                        return false;

                    }


                    if (ui.item.deptid == '') {

                        event.preventDefault();
                        return false;

                    }
                    else {

                        $('#' + hdnhDeptID).val(ui.item.deptid);
                        $('#' + txthDeptName).val(ui.item.deptname);

                    }
                    return false;
                },

                lc: ''
            });


            $(userIDElem).blur(function () {
                var self = this;
                var groupID = $(userIDElem).val();
                if (groupID == '') {
                     $('#' + hdnhDeptID).val('0');
                    $('#' + txthDeptName).val('');

                }
            });


        }




       




    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="dvPageContent" style="width:100%;height:100%;" onkeydown="if(event.keyCode==13){event.keyCode=9; return event.keyCode;}">
        <div id="dvContentHeader" class="dvContentHeader">
            <div id="dvHeader" class="dvHeader_Prod" align="left">
                <asp:Label ID="lblHeader" runat="server" Text="User Department Permission" CssClass="lblHeader" Font-Bold="true" Font-Size="15px"></asp:Label>

            </div>
            <div id="dvMessage" class="dvMessage">
                <asp:Label ID="lblMessage" runat="server" CssClass="" Text="" Width="100%"></asp:Label>
            </div>
            <div id="dvHeaderControl" class="dvHeaderControl">
            </div>

        </div>
        <div id="dvContentMain" align="left" class="dvContentMain" style="height:auto">
            <div id="dvControlsHead" style="height: auto; width: 100%;" align="left">
                <table>
                    <tr>
                        <td></td>
                        <td></td>
                        <td><asp:HiddenField ID="hdnUserID" runat="server" Value="" /></td>
                        <td style="text-align:right;">
                            <asp:Label runat="server" ID="lblUserName" Text="User Name : "></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtUserName" runat="server" CssClass="textBox textAutoSelect"></asp:TextBox>
                           
                        </td>
                        <td ><input id="btnUserID" type="button" value="" runat="server" class="buttonDropdown" tabindex="-1" /></td>
                        <td>
                            
                        </td>
                        <td width="5px"></td>
                        
                        <td>
                           <%-- <asp:CheckBox ID="chkAdmin" Text="Is Admin" runat="server" />--%>
                            

                        </td>
                    </tr>
                     <tr>
                        <td></td>
                        <td></td>
                        <td><asp:HiddenField ID="hdnhDeptID" runat="server" Value="" /></td>
                        <td>
                            <asp:Label runat="server" ID="lblhDeptName" Text="Department Name : "></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txthDeptName" runat="server" CssClass="textBox textAutoSelect"></asp:TextBox>
                           
                        </td>
                        <td ><input id="btnhDeptName" type="button" value="" runat="server" class="buttonDropdown" tabindex="-1" /></td>
                        <td>
                            
                        </td>
                       
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td>
                            <asp:Button ID="btnShow" runat="server" CssClass="buttonSearch" Text="Show" OnClick="btnShow_Click" />
                        </td>
                    </tr>
                   
                    </table>

            </div>
            <div id="dvGridControl" style="height:auto">
            <div id="dvGrid"  style="border:1px solid blue; width:1000px" >

                <div id="dvGridHeader" style="width:1000px;">
                    <table width="420px">
                      <tr class="headerRow_Prod" >
                          <td style="width:80px " align="center">SL#</td>
                          <td style="width:300px " align="center">Department Name</td>
                          <td style="width:40px " align="center">Delete</td>
                      </tr>
                  </table> 
                </div>
                <div id="dvGridMain" class="dvGrid"  style="width:900px; height:300px; overflow: auto;">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:GridView ID="grdDeptItemMapping" runat="server" AutoGenerateColumns="false" ShowHeader="false" CellPadding="1" 
                                 CellSpacing="1" GridLines="Both" DataKeyNames="DEPTID" ClientIDMode="AutoID" OnRowCommand="grdDeptItemMapping_RowCommand"
                                  OnRowCreated="grdDeptItemMapping_RowCreated" OnRowDataBound="grdDeptItemMapping_RowDataBound" OnRowDeleting="grdDeptItemMapping_RowDeleting">
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <Columns>
                                    <asp:TemplateField ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSlNo" runat="server" Text='<%# Bind("SLNO")%>' style="text-align:center; width:80px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <div>
                                                <table>
                                                    <tbody>
                                                        <tr>
                                                            <td style="width:280px">
                                                                <asp:TextBox ID="txtDeptName" runat="server" CssClass="textBox textAutoSelect" Width="280px" Text='<%# Bind("DEPARTMENT_NAME") %>'></asp:TextBox>
                                                                <asp:HiddenField ID="hdnDeptID" runat="server" Value='<%# Bind("DEPTID") %>' />
                                                                <asp:HiddenField ID="hdnUserDeptID" runat="server" Value='<%# Bind("USERDEPTID") %>' />
                                                                <asp:HiddenField ID="hdnDeptCode" runat="server" Value='<%# Bind("DEPTCODE") %>' />
                                                            </td>
                                                            <td>
                                                                <input id="btnDeptName" type="button" value="" runat="server" class="buttonDropdown" tabindex="-1" />
                                                            </td>
                                                          
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnDeleteRow" runat="server" CssClass="buttonDeleteIcon" Height="16px" Width="18px" CommandName="delete"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField Visible="false">
                                                        <ItemTemplate>
                                                            <div style="width: 10px;">
                                                                <div>
                                                                    <div style="background-position: right center; height: 25px; cursor: pointer; background-image: url('../image/more.png'); background-repeat: no-repeat; text-align: left; vertical-align: middle;"
                                                                        onclick="togglePannelStatus(this)"
                                                                        title="More..">
                                                                        ...
                                                                    </div>
                                                                    <div style="display: none;">
                                                                        <div class="gridPanel" style="float: right; width: 0px; height: 0px;">
                                                                            <div style="position: relative; height: 80%; width: 100%;">
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                 </Columns>

                                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Font-Size="Smaller" />
                                                <EditRowStyle BackColor="#999999" />
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            </asp:GridView>
                            <input id="hdnJournalDetRefJson2" type="hidden" runat="server" value="[]" />
                             <input id="hdnJournalDetInsJson2" type="hidden" runat="server" value="[]" />
                        </ContentTemplate>

                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnNewRow" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>

                <div id="divGridControls2" style="width: 100%; height: 30px; border-top: solid 1px #C0C0C0; border-bottom : solid 1px #0b07f5; ">
                     <table style="width: 70%; height: 100%; text-align: center;" cellspacing="1" cellpadding="1" border="0">
                            <tr>
                                            <%--<td style="width: 2px"></td>
                              
                                            <td style="width: 160px; text-align: right;">
                                                                             </td>
                                            <td align="right" style="text-align: left">
                                               &nbsp;
                                            </td>
                                             <td align="right">
                                                &nbsp;</td>--%>
                                            <td style="width:10px" align="left">
                                                </td>
                                              <td style="width: 90px" align="left">
                                                <asp:Button ID="btnNewRow" runat="server" CssClass="buttonNewRow" Text="" OnClientClick="ShowProgress()" OnClick="btnNewRow_Click" />
                                            </td>
                                            <td style="width: 20px;">
                                                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                                                    DisplayAfter="300">
                                                    <ProgressTemplate>
                                                        <asp:Image ID="imgProgress2" runat="server" ImageUrl="~/image/loader.gif" />
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>
                                            </td>
                                            <td align="left"> 
                                             <asp:Button ID="btnAdmin" runat="server" Height="20px" Width="120px" CssClass=" buttonNew" Text="Add Admin User" OnClick="btnAdmin_Click" />

                                             </td>

                                            <td align="right">
                                                &nbsp;</td>
                                           
                                            <td align="right" style="width: 90px">&nbsp;
                                            </td>

                                        </tr>
                            
                                    </table>

                </div>
            </div>
            <div id="dvContentFooter" class="" align="left">
            <table>
                <tr>
                    <td style=""></td>
                    <td>
                        <asp:Button ID="btnAddNew" runat="server" Text="New" CssClass="buttonNew" OnClick="btnAddNew_Click"  />
                        
                    </td>
                    <td>
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonSave checkRequired" AccessKey="s" OnClientClick="if ( ! UserSaveConfirmation()) return false;" OnClick="btnSave_Click" />
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="buttonEdit" OnClick="btnEdit_Click" />
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass=" buttonDelete" OnClick="btnDelete_Click" />
                    </td>
                   
                    <td>
                        <asp:Button ID="btnRefresh" runat="server" Text="Refresh" CssClass="buttonRefresh checkIsDirty" OnClick="btnRefresh_Click" />
                    </td>
                    
                    <td>
                        <input id="btnClose" type="button" runat="server" class="buttonClose" value="Close" onclick="if (ContentForm) { ContentForm.CloseForm(); }" />
                    </td>
                   
                </tr>
            </table>
        </div>
        </div>
           
    </div>
</asp:Content>
