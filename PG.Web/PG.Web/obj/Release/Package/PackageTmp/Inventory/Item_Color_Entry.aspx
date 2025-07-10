<%@ Page Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="Item_Color_Entry.aspx.cs" Inherits="PG.Web.Inventory.Item_Color_Entry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../../javascript/jquery.ui.combogrid.js" type="text/javascript"></script>
    <link href="../../css/jquery.ui.combogrid.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        // <!CDATA[

        var hdnCompanyID = '<%=hdnCompanyID.ClientID %>';

        var ItemGroupListServiceLink = '<%=this.ItemGroupListServiceLink%>';





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


            $("#groupBox").height(contInnerHeight - 10);
            var groupHeight = $("#groupBox").height();
            var groupHeaderHeight = $("#groupHeader").height();
            var groupFooterHeight = $("#groupFooter").height();
            $("#groupContent").height(groupHeight - groupHeaderHeight - groupFooterHeight - 2);

        }


        function tbopen(key) {
            if (!key) {
                key = '';
            }


            var url = "/Admin/SetPassword.aspx?uid=" + key
            //if (pageInTab == 1)
            if (TabVar.PageMode == Enums.PageMode.InTab) {

                var tdata = new xtabdata();
                tdata.linktype = Enums.LinkType.Direct;
                tdata.id = 6320;
                tdata.name = "SetPassword";
                //tdata.label = "User: " + userid;
                tdata.label = "Set Password";
                tdata.type = 0;
                tdata.url = url;
                tdata.tabaction = Enums.TabAction.InTabReuse;
                tdata.selecttab = 1;
                tdata.reload = 0;
                tdata.param = "";



                try {
                    window.parent.OpenMenuByData(tdata);
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

        function tbopenSalInfo(key) {
            if (!key) {
                key = '';
            }


            var url = "/Master/EmpSalaryInfo.aspx?eid=" + key
            //if (pageInTab == 1)
            if (TabVar.PageMode == Enums.PageMode.InTab) {

                var tdata = new xtabdata();
                tdata.linktype = Enums.LinkType.Direct;
                tdata.id = 6320;
                tdata.name = "EmpSalaryInfo";
                //tdata.label = "User: " + userid;
                tdata.label = "Emp. Salary Sturture";
                tdata.type = 0;
                tdata.url = url;
                tdata.tabaction = Enums.TabAction.InTabReuse;
                tdata.selecttab = 1;
                tdata.reload = 0;
                tdata.param = "";

                try {
                    window.parent.OpenMenuByData(tdata);
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
            if ($('#' + txtGroupName).is(':visible')) {
                bindGroupList();
            }


        });    //ready

        function bindGroupList() {
            var cgColumns = [
                             //{ 'columnName': 'itemgroupid', 'width': '80', 'align': 'left', 'highlight': 2, 'label': 'Group ID' }
                             { 'columnName': 'itemgroupcode', 'width': '80', 'align': 'left', 'highlight': 4, 'label': 'Code' }
                            , { 'columnName': 'itemgroupdesc', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                            , { 'columnName': 'itemgroupnameparent', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Group Parent' }


            ];
            var serviceURL = ItemGroupListServiceLink + "?isterm=1&includeempty=0&hasitem=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;

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

                var groupID = $(groupIDElem).val();
                if (groupID == '') {
                    // $('#' + hdnDealerID).val('0');
                    $('#' + txtGroupName).val('');
                    //$('#' + txtGroupCode).val('');
                }
            });
        }

        function fromParent(val1) {
            alert('this is called from parent: ' + val1);
        }

        function Button1_onclick() {
            //document.getElementById("btnSave").click();
            ///__doPostBack("<%= this.btnSave.UniqueID %>", "");
    __doPostBack("btnSave", "");
}


function btnSalaryInfo_onclick() {

}

function btnSalaryInfo_onclick() {

}

// ]]>
    </script>




</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="dvPageContent" style="width: 100%; height: auto;">
        <div id="dvContentHeader" class="dvContentHeader">
            <div id="dvHeader" class="dvHeader">
                <asp:Label ID="lblHeader" CssClass="lblHeader" runat="server" Text="Item Color"></asp:Label>
            </div>
            <!--Message Div -->
            <div id="dvMsg" runat="server" class="dvMessage">
                <asp:Label ID="lblMessage" runat="server" Font-Size="Small" Width="100%"> </asp:Label>
            </div>
            <div id="dvHeaderControl" class="dvHeaderControl">
            </div>
        </div>

        <div id="dvContentMain" class="dvContentMain">
            <div id="dvControls" style="height: auto; width: 100%">
                <div id="dvControlsInner" class="groupBoxContainer boxShadow">
                    <div id="groupBox">
                        <div id="groupHeader" class="groupHeader">
                            <div style="width: 100%; height: 20px;">
                                <table>
                                    <tr>
                                        <td>
                                            <div id="dvIconEditMode" class="iconView" runat="server"></div>
                                        </td>
                                        <td>
                                            <span>Item Color</span>
                                        </td>
                                    </tr>
                                </table>

                            </div>

                        </div>
                        <div id="groupContent" class="groupContent" style="width: 100%; height: 300px; overflow: auto;">
                            <div id="groupContenInner" style="width: 100%; height: auto; text-align: left;">

                                <table cellpadding="2" cellspacing="4">
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:HiddenField ID="hdnColorID" runat="server" Value="0" />

                                        </td>
                                        <td>

                                            <asp:HiddenField ID="hdnCompanyID" runat="server" Value="0" />

                                        </td>


                                        <td></td>

                                    </tr>

                              
                                     <tr>
                                        <td></td>
                                        <td align="right">
                                            <asp:Label ID="lblColorCode" runat="server" Text="Color Code:"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtcolorCode" runat="server" CssClass="textBoxReadOnly"
                                                Width="164px" MaxLength="50"></asp:TextBox>
                                        </td>
                                        <td align="right"></td>
                                        <td align="left"></td>
                                    </tr>
         

                                    <tr>
                                        <td></td>
                                        <td style="" align="right">
                                            <asp:Label ID="lblColorName" runat="server" Text="Color Name:"></asp:Label>
                                        </td>
                                        <td style="" align="left">
                                            <asp:TextBox ID="txtColorName" runat="server" CssClass="textBox fldRequired enableIsDirty"
                                                Width="380px" MaxLength="200"></asp:TextBox>
                                        </td>
                                        <td style="" align="right">&nbsp;</td>
                                        <td style="" align="left">&nbsp;</td>
                                    </tr>
             

                                    <tr>
                                        <td></td>
                                        <td style="" align="right">

                                            <asp:Label ID="Label6" runat="server" Text="Status:" Visible="true"></asp:Label>
                                        </td>
                                        <td style="" align="left">
                                            <asp:DropDownList ID="ddlIsActive" runat="server"
                                                CssClass="dropDownList enableIsDirty" Width="90" Visible="true">
                                                <asp:ListItem Value="Y" Selected="True">Active</asp:ListItem>
                                                <asp:ListItem Value="N" >Inactive</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="" align="right">&nbsp;</td>
                                        <td style="" align="left">&nbsp;</td>
                                    </tr>

                                    <tr>
                                        <td></td>
                                        <td style="" align="right">&nbsp;</td>
                                        <td style="" align="left">&nbsp;</td>
                                        <td style="" align="right">&nbsp;</td>
                                        <td style="" align="left">&nbsp;</td>
                                    </tr>

                             
                                </table>
                            </div>

                        </div>
                        <div id="groupFooter" class="groupFooter">
                            <div style="width: 100%; height: 12px;">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>


        <div id="dvContentFooter" class="dvContentFooter">
            <table>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="btnAddNew" runat="server" Text="New" CssClass="buttonNew"
                            OnClick="btnAddNew_Click" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonCancel checkIsDirty"
                            OnClick="btnCancel_Click" />
                    </td>
                    <td>
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonSave"
                            OnClick="btnSave_Click" />
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="buttonEdit"
                            OnClick="btnEdit_Click" />
                    </td>
                    <td>
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="buttonDelete"
                            OnClick="btnDelete_Click" />
                    </td>

                    <td>
                        <asp:Button ID="btnRefresh" runat="server" Text="Refresh" CssClass="buttonRefresh checkIsDirty"
                            OnClick="btnRefresh_Click" />
                    </td>


                    <td></td>


                    <td>
                        <input id="btnClose" type="button" runat="server" class="buttonClose" value="Close" onclick="if (ContentForm) { ContentForm.CloseForm(); }" />
                    </td>


                </tr>
            </table>

        </div>
    </div>



</asp:Content>

