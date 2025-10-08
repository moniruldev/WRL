<%@ Page Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="RoleMenuAssignWR.aspx.cs" Inherits="PG.Web.Admin.RoleMenuAssignWR" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">



    <script src="../javascript/jquery.ui.combogrid.js" type="text/javascript"></script>
    <script src="../javascript/jquery.attributeobserver.js" type="text/javascript"></script>
    <link href="../css/jquery.ui.combogrid.css" rel="stylesheet" type="text/css" />


    <script language="javascript" type="text/javascript">
        // <!CDATA[

        var ItemListServiceLink = '<%=this.ItemListServiceLink%>';

        var menuItemListServiceLink = '<%=this.MenuItemListServiceLink%>';

        var txtMenu = '<%=txtMenu.ClientID%>';

        var hdnMenu = '<%=hdnMenu.ClientID%>';

        var hdnAppId = '<%=hdnAppId.ClientID%>';



        var btnSave = '<%=btnSave.ClientID%>';

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

        function checkAll(objRef) {
            var GridView = objRef.closest("table");
            var inputList = GridView.getElementsByTagName("input");

            for (var i = 0; i < inputList.length; i++) {
                var input = inputList[i];

                if (input.type === "checkbox" && input !== objRef) {
                    var row = input.closest("tr");

                    //if (!row.hasAttribute("data-orig-bg")) {
                    //    var originalColor = window.getComputedStyle(row).backgroundColor;
                    //    row.setAttribute("data-orig-bg", originalColor);
                    //}

                    if (objRef.checked) {
                        //row.style.backgroundColor = "#409cde";
                        input.checked = true;
                    } else {
                        input.checked = false;
                        //row.style.backgroundColor = row.getAttribute("data-orig-bg");
                    }
                }
            }
        }

        function Check_Click(objRef) {
            var row = objRef.closest("tr");
            var gridView = row.closest("table");

            //if (!row.hasAttribute("data-orig-bg")) {
            //    var origBg = window.getComputedStyle(row).backgroundColor;
            //    row.setAttribute("data-orig-bg", origBg);
            //}

            //if (objRef.checked) {
            //    row.style.backgroundColor = "#409cde";
            //} else {
            //    var originalBg = row.getAttribute("data-orig-bg");
            //    row.style.backgroundColor = originalBg;
            //}

            var inputList = gridView.getElementsByTagName("input");
            var allChecked = true;
            var headerCheckbox = null;

            for (var i = 0; i < inputList.length; i++) {
                var input = inputList[i];

                if (input.type === "checkbox") {
                    if (!headerCheckbox && input !== objRef) {
                        headerCheckbox = inputList[0];
                    }

                    if (input !== headerCheckbox) {
                        if (!input.checked) {
                            allChecked = false;
                            break;
                        }
                    }
                }
            }

            if (headerCheckbox) {
                headerCheckbox.checked = allChecked;
            }
        }


        $(document).ready(function () {
            if ($('#' + txtMenu).is(':visible')) {
                bindMenuList();
            }

        });

        function bindMenuList() {
            var cgColumns = [{ 'columnName': 'menuName', 'width': '300', 'align': 'left', 'highlight': 2, 'label': 'Name' }
                             , { 'columnName': 'menuId', 'width': '50', 'align': 'left', 'highlight': 0, 'label': 'ID' }

            ];


            var appId = $('#' + hdnAppId).val();
            //var serviceURL = GLAccountServiceLink + "?isterm=1&includeempty=1&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            var serviceURL = menuItemListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            serviceURL += "&namecomptype=" + Enums.DataCompareType.Contains;
            //serviceURL += "&companyid=" + companyid;
            serviceURL += "&ispaging=1";
            serviceURL += "&appId=" + appId;


            var menuElem = $('#' + txtMenu);

            $('#' + txtMenu).click(function (e) {
                //elmID = $(elem).attr('id');
                //$(elem).combogrid("show");
                $(menuElem).combogrid("dropdownClick");
            });


            $(menuElem).combogrid({
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
                width: 450,
                url: serviceURL,
                search: function (event, ui) {
                    //var companyCode = $('#' + ddlCompany).val();
                    //var newServiceURL = serviceURL + "&companycode=" + companyCode
                    //$(this).combogrid("option", "url", newServiceURL);
                },
                select: function (event, ui) {
                    if (!ui.item) {
                        event.preventDefault();

                        $('#' + hdnMenu).val('0');
                        $('#' + txtMenu).val('');
                        return false;
                        //ClearGLAccountData(elemID);
                    }


                    if (ui.item.menuId == '') {
                        event.preventDefault();
                        return false;
                        //ClearGLAccountData(elemID);
                    }
                    else {

                        $('#' + hdnMenu).val(ui.item.menuId);
                        $('#' + txtMenu).val(ui.item.menuName);
                    }
                    return false;
                },

                lc: ''
            });


            $(menuElem).blur(function () {
                var self = this;

                var menu = $(menuElem).val();
                if (menu == '') {
                    $('#' + hdnMenu).val('0');
                }
            });
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
                        <h5 class="card-title">Role Menu Assign</h5>
                    </div>
                </div>

                <div class="card-body">
                    <div class="row mb-0">

                        <div class="col-md-4">
                            <div class="form-group row mb-0">
                                <label for="Role" class="col-sm-4 col-form-label-sm">Role :</label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddlRole" runat="server" CssClass="form-control form-control-sm"></asp:DropDownList>
                                </div>
                            </div>

                        </div>
                        <div class="col-md-4">
                            <div class="form-group row mb-0">
                                <label for="lblMenu" class="col-sm-4 col-form-label-sm">Menu :</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtMenu" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                    <asp:HiddenField ID="hdnMenu" runat="server" Value="0" />
                                    <asp:HiddenField ID="hdnAppId" runat="server" Value="0" />
                                </div>
                            </div>

                        </div>


                        <div class="col-md-4">
                            <div class="form-group row mb-0">
                                <label for="name" class="col-sm-4 col-form-label-sm">App :</label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddlApp" runat="server" CssClass="form-control form-control-sm">
                                        <asp:ListItem Value="1" Text="Courier" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Client"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                        </div>

                    </div>


                    <div class="row-mb-0">
                        <div class="card-footer m-2 p-1">
                            <asp:LinkButton runat="server" ID="btnLoadData" OnClick="btnLoadData_Click" CssClass="btn btn-primary" Text="<i class='fa fa-list'></i> Show Data"></asp:LinkButton>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" ShowHeader="true" CssClass="table table-sm table-striped table-bordered table-responsive-sm"
                                DataKeyNames="APPMENUID" EnableModelValidation="True" ClientIDMode="AutoID" OnRowDataBound="GridView1_RowDataBound" AllowPaging="false" EmptyDataText="There is no record" PageSize="2"
                                OnPageIndexChanging="GridView1_PageIndexChanging" OnSelectedIndexChanged="GridView1_OnSelectedIndexChanged">
                                <PagerSettings Mode="NumericFirstLast" />
                                <HeaderStyle CssClass="table-info" Font-Size="Smaller" />
                                <Columns>
                                    <asp:TemplateField ItemStyle-Width="40px" ItemStyle-CssClass="gvhspadding" HeaderStyle-CssClass="gvhspadding">
                                        <HeaderTemplate>
                                            <input id="checkAll" type="checkbox" onclick="checkAll(this);" runat="server" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkMenuItem" runat="server" onclick="Check_Click(this)" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Menu ID" DataField="APPMENUID" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                    <asp:BoundField HeaderText="Parent Menu" DataField="PARENTMENUID" ItemStyle-Width="170px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                    <asp:BoundField HeaderText="Menu Name" DataField="APPMENUTEXT" ItemStyle-Width="170px" />
                                    <asp:BoundField HeaderText="Menu URL" DataField="APPMENUURL" ItemStyle-Width="250px" />


                                    <asp:TemplateField ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderText="Show Menu">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlShowMenu" runat="server">
                                                <asp:ListItem Text="True" Value="True"></asp:ListItem>
                                                <asp:ListItem Text="False" Value="False"></asp:ListItem>
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>

                            </asp:GridView>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">

                            <asp:LinkButton runat="server" ID="btnSave" OnClick="btnSave_Click" CssClass="btn btn-primary" Text="<i class='fas fa-save'></i> Save"></asp:LinkButton>
                            <asp:LinkButton runat="server" ID="btnClear" CssClass="btn btn-danger" Text="<i class='fa fa-ban'></i> Clear"></asp:LinkButton>


                            <%--  <div id="dvGridFooter" style="width: 100%; height: 25px; font-size: smaller;" class="subFooter">
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
                                                                <asp:ListItem Value="0">all</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>


                                                        <td>
                                                            <asp:Label ID="Label1" runat="server" CssClass="col-form-label-sm p-2" Text="Page:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtGridPageNo" runat="server" CssClass="form-control form-control-sm m-1 p-0" Width="50px">0</asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblGridPageInfo" runat="server" CssClass="col-form-label-sm p-2" Text=" of 0"></asp:Label>
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
                            </div>--%>
                        </div>

                    </div>

                </div>

            </div>

        </div>
    </div>
</asp:Content>
