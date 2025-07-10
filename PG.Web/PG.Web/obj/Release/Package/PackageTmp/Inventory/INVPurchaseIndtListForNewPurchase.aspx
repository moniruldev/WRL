<%@ Page Title="" Language="C#" ViewStateMode="Disabled" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="INVPurchaseIndtListForNewPurchase.aspx.cs" Inherits="PG.Web.Inventory.INVPurchaseIndtListForNewPurchase" %>

<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">

        ContentForm.CalendarImageURL = "../image/calendar.png";

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

        });



        function tbopen(key, userid) {
            key = key || '';

            var url = IForm.RootPath + "Inventory/INVNewIndentPurchase.aspx?id=" + key;

            if (IForm.PageMode == Enums.PageMode.InTab) {

                var tdata = new xtabdata();
                tdata.linktype = Enums.LinkType.Direct;
                tdata.id = 0;
                tdata.name = "Purchase Against Indent";
                tdata.label = "Purchase Against Indent";
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
    <div id="dvPageContent" style="width: 100%; height: auto;">
        <div id="dvContentHeader" class="dvContentHeader">
            <div id="dvHeader" class="dvHeader">
                <asp:Label ID="lblHeader" CssClass="lblHeader" runat="server" Text="Indent Purchase List"></asp:Label>
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
                    <tr>

                        <td>
                            <asp:Label ID="LblLocation" runat="server" Text=" Indent Department"></asp:Label><span style="color: red">*</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlDepartment" runat="server"></asp:DropDownList>
                        </td>
                        <td>
                            <asp:Label ID="Label5" runat="server" Text="Req. No:"></asp:Label>

                        </td>
                        <td>
                            <asp:TextBox ID="txtReqNo" runat="server" CssClass="textBox notEnterToTab"></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label6" runat="server" Text="Date From"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="textBox textDate dateParse"></asp:TextBox>
                        </td>

                        <td>
                            <asp:Label ID="lblToDate" runat="server" Text="Date To:"></asp:Label>
                        </td>

                        <td>
                            <asp:TextBox ID="txtToDate" runat="server" CssClass="textBox textDate dateParse"></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                        <td></td>
                        <td></td>

                        <td>
                            <asp:Button ID="btnShow" runat="server" CssClass="buttonSearch" Style="padding-left: 22px;"
                                Text="Show Data" OnClick="btnShow_Click" Height="26px"/>
                        </td>
                        <td>
                            <asp:Button ID="btnAddNew" runat="server" CssClass="buttonNew" Text="New Purchase" Height="26px" />
                            <%--<input id="btnAddNew" type="button" runat="server" value="New IGR (Requisition)" class="buttonNew" />--%>
                        </td>
                    </tr>

                </table>
            </div>
            <div id="dvControls" style="width: 100%;">
                <div id="dvControlsInner" class="groupBoxContainer boxShadow" style="width:90%">
                    <div id="dvGridContainer" style="width: 100%; height: auto; text-align: left;">

                        <div id="dvGrid" style="width:100%x; height: 250px; overflow: auto;">
                            <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="ASPxGridView1" runat="server" Width="100%" AutoGenerateColumns="False" KeyFieldName="PURCHASE_NO">
                                <Settings VerticalScrollableHeight="250" ShowFooter="True" ShowGroupFooter="VisibleAlways" />
                                <TotalSummary>
                                    <dx:ASPxSummaryItem FieldName="Size" SummaryType="Sum" />
                                    <dx:ASPxSummaryItem FieldName="PURCHASE_QTY" ShowInColumn="PURCHASE_QTY" DisplayFormat="Total:{0}" ShowInGroupFooterColumn="PURCHASE_QTY" SummaryType="Sum" />
                                </TotalSummary>
                                <GroupSummary>
                                    <dx:ASPxSummaryItem SummaryType="Count" />
                                    <dx:ASPxSummaryItem FieldName="PURCHASE_QTY" ShowInGroupFooterColumn="PURCHASE_QTY" SummaryType="Sum" />
                                </GroupSummary>
                                <Columns>
                                    <dx:GridViewDataTextColumn Caption="Action" UnboundType="String" Width="55px" VisibleIndex="1">
                                        <DataItemTemplate>
                                            <dx:ASPxHyperLink ID="hyperLink" runat="server" OnInit="hyperLink_Init">
                                            </dx:ASPxHyperLink>
                                        </DataItemTemplate>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Pur No" FieldName="PURCHASE_NO" VisibleIndex="1" Width="100px">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Group Name" FieldName="ITEM_GROUP_NAME" VisibleIndex="1" Width="100px">
                                        <Settings AllowGroup="True" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Item Name" FieldName="ITEM_NAME" VisibleIndex="2" Width="150px">
                                    </dx:GridViewDataTextColumn>
                                     <dx:GridViewDataTextColumn Caption="Item Code" FieldName="ITEM_CODE" VisibleIndex="3" Width="70px">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Quantity" FieldName="PURCHASE_QTY" VisibleIndex="3" Width="70px">
                                    </dx:GridViewDataTextColumn>
                                     <dx:GridViewDataTextColumn Caption="Unit Price" FieldName="UNIT_PRICE" VisibleIndex="3" Width="70px">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Uom" FieldName="UOM_NAME" VisibleIndex="4" Width="50px" />

                                    <dx:GridViewDataTextColumn Caption="Purchase Date" FieldName="PURCHASE_DATE" VisibleIndex="3" Width="80px">
                                        <DataItemTemplate>
                                            <%#Eval("PURCHASE_DATE", "{0:dd-MMM-yyyy}")%>
                                        </DataItemTemplate>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Supplier" FieldName="SUP_NAME" VisibleIndex="8" Width="80px" />
                                    <dx:GridViewDataTextColumn Caption="Dept" FieldName="DEPARTMENT_NAME" VisibleIndex="9" Width="100px" />
                                    <dx:GridViewDataTextColumn Caption="Indt No" FieldName="INDT_NO" VisibleIndex="10" Width="80px" />
                                    
                                </Columns>
                                <SettingsBehavior AllowFixedGroups="True" AutoExpandAllGroups="True" SortMode="Value" />
                                <SettingsPager NumericButtonCount="20">
                                    <PageSizeItemSettings Visible="true" Items="50,100" />
                                </SettingsPager>
                                <Settings ShowGroupPanel="true" ShowFilterBar="Visible" ShowFilterRow="True" ShowFilterRowMenu="True" ShowGroupedColumns="True" ShowHeaderFilterButton="True" ShowGroupButtons="True" />

                                <SettingsDataSecurity AllowInsert="false" AllowEdit="false" AllowDelete="false" />
                                <Styles>
                                    <Header BackColor="#0033CC" ForeColor="White">
                                    </Header>
                                    <AlternatingRow BackColor="#FFFFCC">
                                    </AlternatingRow>
                                    <GroupFooter BackColor="#CCCCFF">
                                    </GroupFooter>
                                    <GroupPanel BackColor="#9999FF">
                                    </GroupPanel>
                                </Styles>
                            </dx:ASPxGridView>

                            <dx:ASPxButton ID="btnXlsExport" runat="server" Text="Export to XLS" UseSubmitBehavior="False"
                                OnClick="btnXlsExport_Click" />

                            <dx:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="ASPxGridView1"></dx:ASPxGridViewExporter>
                        </div>

                    </div>
                </div>
            </div>
        </div>
        <div id="dvContentFooter" class="dvContentFooter">
        </div>
    </div>
</asp:Content>
