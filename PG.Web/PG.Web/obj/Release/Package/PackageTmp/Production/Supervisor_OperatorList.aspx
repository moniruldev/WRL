<%@ Page Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="Supervisor_OperatorList.aspx.cs" Inherits="PG.Web.Production.Supervisor_OperatorList" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">



    <script src="../javascript/jquery.ui.combogrid.js" type="text/javascript"></script>
    <script src="../javascript/jquery.attributeobserver.js" type="text/javascript"></script>
    <link href="../css/jquery.ui.combogrid.css" rel="stylesheet" type="text/css" />


    <script language="javascript" type="text/javascript">
        // <!CDATA[

        var ItemListServiceLink = '<%=this.ItemListServiceLink%>';

        var btnGridPageGoTo = '<%=btnGridPageGoTo.ClientID %>';
        var txtGridPageNo = '<%=txtGridPageNo.ClientID %>';
     


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

            var url = IForm.RootPath + "Production/Supervisor_OperatorEntry.aspx?id=" + key;

            if (IForm.PageMode == Enums.PageMode.InTab) {

                var tdata = new xtabdata();
                tdata.linktype = Enums.LinkType.Direct;
                tdata.id = 0;
                tdata.name = "Operator Entry";
                //tdata.label = "User: " + userid;
                tdata.label = "Operator Entry";
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

        function fromParent(val1) {
            alert('this is called from parent: ' + val1);
        }


        $(document).ready(function () {
            //alert($('#' + ddlBalanceType).val());


            $groupPopup = $('#' + groupPopupID).ItemGroupTree({
                title: 'Select Item Group',
                autoLink: true,
                autoLinkUpdate: true,
                linkControlID: dvItemGroupID,
                highlightLink: true,
                keyboard: true,

                okclick: function (event, data) {
                    //alert('ok');
                    SetItemGroupData(data);
                    //OnGLGroupChange(data.glclassid, data.glgroupid);
                    //ContentForm.MakeControlIsDirty(txtGLGroupNameParent, true);
                },
                open: function (event, ui) {
                    // $("#dvGLGroup").addClass("dvGLGroupSelected");
                },
                close: function (event, ui) {
                    //            $("#dvGLGroup").removeClass("dvGLGroupSelected");
                    //            $('#' + ctlGLGroupText).focus();
                    //            $('#' + ctlGLGroupText).select();
                }
            });


            $("#" + dvItemGroupID).find('.btnPopup').click(function (e) {
                //alert('ok');
                OpenItemGroupTree();
                //$("#" + groupPopupID).GroupTree("show", '');
            });

            $("#" + txtItemGroupNameParent).keydown(function (e) {
                switch (e.keyCode) {
                    case 46:  //delete
                        ClearData();
                        break;
                    case 8:  //backspace
                        ClearData();
                        e.preventDefault();
                        break;
                    case 13:  //enter
                        OpenItemGroupTree();
                        e.preventDefault();
                        break;

                }

                //delete 
                if (e.keyCode == 46) {
                    //alert('delete');
                    ClearData();
                }
                // backspace
                if (e.keyCode == 8) {
                    //alert('delete');
                    ClearData();
                    e.preventDefault();
                }

            });


            $("#" + dvItemGroupID).find('.btnClear').click(function (e) {
                ClearData();
            });

           


        });    //ready

        function OpenItemGroupTree() {


            if ($("#" + txtItemGroupNameParent).is(":disabled")) {
                $("#" + groupPopupID).ItemGroupTree("option", "enableSelect", false);
            }
            else {
                $("#" + groupPopupID).ItemGroupTree("option", "enableSelect", true);
            }

            if ($("#" + txtItemGroupNameParent).is(":disabled") == false) {
                var itemGroupKey = $("#" + hdnItemGroupParentKey).val();

                $("#" + groupPopupID).ItemGroupTree("show", itemGroupKey);
            }
        }


      


    </script>

    <style type="text/css">
        /*.FixedHeader { POSITION: relative; TOP: expression(this.offsetParent.scrollTop); BACKGROUND-COLOR: white } */

        .FixedHeader {
            POSITION: relative;
            BACKGROUND-COLOR: white;
        }

        #dvMessage {
            height: 20px;
        }

        .dvGroup {
            height: auto;
            width: auto;
        }

        .dvPopupItemGroup {
            display: none;
            border: 0px solid black;
            width: 0px;
            height: 0px;
        }
    </style>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="dvPageContent" style="width: 100%; height: auto;">
        <div id="dvContentHeader" class="dvContentHeader">
            <div id="dvHeader" class="dvHeader">
                <asp:Label ID="lblHeader" CssClass="lblHeader" runat="server" Text="Supervisor/Operator List"></asp:Label>
            </div>

            <!--Message Div -->
            <div id="dvMsg" runat="server" class="dvMessage"
                style="">
                <asp:Label ID="lblMessage" runat="server" Font-Size="Small" Width="100%"> </asp:Label>
            </div>



            <div id="dvHeaderControl" class="dvHeaderControl">
                <asp:HiddenField ID="hdnItemIdForFilter" runat="server" />
            </div>


        </div>
        <div id="dvContentMain" class="dvContentMain">
            <div id="dvControlsHead" style="height: auto; width: 100%;">
                <table>

                    <tr>
                                        <td style="" align="right">
                                            <asp:Label ID="lblDepartment" runat="server" Text="Department:"></asp:Label>
                                        </td>
                                        <td style="" align="left">
                                             <asp:DropDownList ID="ddlFromDepartment" runat="server" CssClass=" TextBoxnew" Width="200px">  </asp:DropDownList>
                                        </td>
                                        <td style="" align="right"></td>
                                        <td style="" align="right">
                                            <asp:Label ID="lblStatus" runat="server" Text="Status :"></asp:Label>
                                        </td>
                                        <td>

                                             <asp:DropDownList ID="ddlIsActive" runat="server" CssClass="TextBoxnew"  Width="100px">
						                         <asp:ListItem Value="0" Selected="True">All</asp:ListItem>
                                                 <asp:ListItem Value="Y">Active</asp:ListItem>
                                                 <asp:ListItem Value="N">Inactive</asp:ListItem>
					                         </asp:DropDownList>

                                        </td>
                                        <td style="" align="right">
                                            <asp:Label ID="lblIsOperator" runat="server" Text="Is Operator :"></asp:Label>
                                        </td>
                                        <td>

                                             <asp:DropDownList ID="ddlIsOperator" runat="server" CssClass="TextBoxnew"  Width="100px">
						                         <asp:ListItem Value="" Selected="True">All</asp:ListItem>
                                                 <asp:ListItem Value="1">Yes</asp:ListItem>
                                                 <asp:ListItem Value="0">No</asp:ListItem>
					                         </asp:DropDownList>

                                        </td>
                                    </tr>

                </table>
                <table>
                    <tr>





                        <td></td>

                        <td>

                            <asp:Button ID="btnRefresh" runat="server" CssClass="buttonRefresh" Style="padding-left: 22px;"
                                Text="Load"
                                OnClick="btnRefresh_Click" />

                        </td>

                        <td>

                            <input id="btnAddNew" type="button" runat="server" value="New Entry" class="buttonNew" style="padding-left: 22px; width: 120px;" />


                        </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>

                        <td></td>
                        <td></td>
                    </tr>
                </table>
            </div>

            <div id="dvControls" style="width: 100%;">
                <div id="dvControlsInner" class="groupBoxContainer boxShadow" style="width: 750px;">

                    <div id="dvGridContainer" style="width: 100%; height: auto; text-align: left;">
                        <div id="dvGridHeader" style="width: 100%; height: 25px; font-size: smaller;" class="subHeader">
                            <table style="height: 100%; color: #FFFFFF; font-weight: bold; text-align: center;" class="defFont"
                                cellspacing="1" cellpadding="1" rules="all">
                                <tr>
                                    <td width="52px" align="left"></td>
                                    <td width="100px" align="center">Emp Id</td>
                                    <td width="200px" align="center">Name</td>
                                    <td width="60px" align="center">Operator?</td>
                                    <td width="50px" align="center">Active</td>
                                </tr>
                            </table>
                        </div>
                        <div id="dvGrid" style="width: auto; height: 250px; overflow: auto;">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                                CellPadding="1" CellSpacing="1" ForeColor="#333333" GridLines="None"
                                Font-Names="Arial" Font-Size="9pt"
                                DataKeyNames="SUPPERVISOR_MSTID" OnRowDataBound="GridView1_RowDataBound"
                                OnRowDeleting="GridView1_RowDeleting" EmptyDataText="There is no record" PageSize="2"
                                OnPageIndexChanging="GridView1_PageIndexChanging"
                                OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
                                ShowHeader="False" AllowPaging="True">
                                <PagerSettings Mode="NumericFirstLast" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <Columns>
                                    <asp:HyperLinkField HeaderText="" Text="">
                                        <ControlStyle CssClass="buttonViewGrid" Height="20px" Width="40px" />
                                        <ItemStyle Width="50px" />
                                    </asp:HyperLinkField>
                                    <asp:BoundField DataField="EMP_ID" HeaderText="Id" ItemStyle-Width="100px" />

                                    <asp:BoundField DataField="FULL_NAME" HeaderText="Name">

                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle Width="200px" HorizontalAlign="Left" />
                                    </asp:BoundField>
                                   <asp:BoundField DataField="ISOPERATOR" HeaderText="Operator" ItemStyle-Width="60px" />
                                    <asp:BoundField DataField="ISACTIVE" HeaderText="Active" ItemStyle-Width="50px" />
                                </Columns>
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#999999" />
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            </asp:GridView>
                        </div>
                        <div id="dvGridFooter" style="width: 100%; height: 25px; font-size: smaller;" class="subFooter">
                            <table style="height: 100%; width: 100%; font-weight: bold;"
                                cellspacing="2" cellpadding="1" rules="all">
                                <tr>
                                    <td align="left" style="width: 40%">
                                        <table>
                                            <tr>
                                                <td style="width: 2px;"></td>
                                                <td>
                                                    <asp:Label ID="lblTotal" runat="server" Text="Rows: 0 of 0"></asp:Label>
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
                                                        <asp:Label ID="Label2" runat="server" Text="Page Size:"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlGridPageSize" runat="server" CssClass="dropDownList"
                                                            Width="50" Height="18" AutoPostBack="True"
                                                            OnSelectedIndexChanged="ddlGridPageSize_SelectedIndexChanged">
                                                            <asp:ListItem Value="10">10</asp:ListItem>
                                                            <asp:ListItem Value="20">20</asp:ListItem>
                                                            <asp:ListItem Value="30">30</asp:ListItem>
                                                            <asp:ListItem Value="50">50</asp:ListItem>
                                                            <asp:ListItem Value="100">100</asp:ListItem>
                                                            <asp:ListItem Value="200">200</asp:ListItem>
                                                            <asp:ListItem Value="0" Selected="True">all</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>


                                                    <td>
                                                        <asp:Label ID="Label1" runat="server" Text="Page:"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtGridPageNo" runat="server" CssClass="textBox" Width="30"
                                                            Height="14" Style="text-align: center;">0</asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblGridPageInfo" runat="server" Text=" of 0"></asp:Label>
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




        <div id="dvContentFooter" class="dvContentFooter">
        </div>

    </div>


</asp:Content>
