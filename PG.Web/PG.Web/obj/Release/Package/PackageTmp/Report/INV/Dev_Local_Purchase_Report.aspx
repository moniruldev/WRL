<%@ Page Title="" Language="C#" ViewStateMode="Disabled" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="Dev_Local_Purchase_Report.aspx.cs" Inherits="PG.Web.Report.INV.Dev_Local_Purchase_Report" %>

<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        ContentForm.CalendarImageURL = "../../image/calendar.png";
        var timeout;
        function scheduleGridUpdate(grid) {
            window.clearTimeout(timeout);
            timeout = window.setTimeout(
                function () { grid.Refresh(); },
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
        $(document).ready(function () {
            $('#' + txtGridPageNo).keydown(function (e) {
                if (e.keyCode == 13) {
                    e.preventDefault();
                    $('#' + btnGridPageGoTo).click();
                }
            });
        });
        function ReportPrint(key, isPDFAutoPrint) {
            var rptPageLink = ReportViewPageLink;
            if (isPDFAutoPrint) {

                rptPageLink = ReportViewPDFPageLink;
            }
            var now = new Date();
            var strTime = now.getTime().toString();
            var url = ReportViewPageLink + "?rk=" + key + "&_tt=" + strTime;

            iframe = document.getElementById(ifPrintButton);
            if (iframe === null) {
                iframe = document.createElement('iframe');
                iframe.id = hiddenIFrameID;
                document.body.appendChild(iframe);
            }
            iframe.src = url;
        }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="dvPageContent" style="width: 100%; height: auto;">
        <div id="dvContentHeader" class="dvContentHeader">
            <div id="dvHeader" class="dvHeader">
                <asp:Label ID="lblHeader" CssClass="lblHeader" runat="server" Text="Local Purchase Report"></asp:Label>
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
                        <td align="right">
                            <asp:Label ID="lblDateFrom" runat="server" Text="Date From:" Font-Bold="true"></asp:Label>&nbsp;
                        </td>
                        <td>
                            <asp:TextBox ID="txtFromDate" runat="server" Width="80px" CssClass="textBox textDate dateParse"></asp:TextBox>
                        </td>
                        <td style="width: 4px;"></td>
                        <td align="right">
                            <asp:Label ID="lblToDate" runat="server" Text="Date To:" Font-Bold="true"></asp:Label>&nbsp;
                        </td>

                        <td>
                            <asp:TextBox ID="txtToDate" runat="server" Width="80px" CssClass="textBox textDate dateParse"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;&nbsp;<asp:Button ID="btnShow" runat="server" CssClass="buttonSearch" Style="padding-left: 22px;"
                                Text="Show Data" OnClick="btnShow_Click" />
                        </td>

                    </tr>

                </table>
            </div>
            <div id="dvControls" style="width: 100%;">
                <div id="dvControlsInner" class="groupBoxContainer boxShadow" style="width: 1150px">
                    <div id="dvGridContainer" style="width: 100%; height: auto; text-align: left;">

                        <div id="dvGrid" style="width: 100%; height: 500px; overflow: auto;">
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
                                    <dx:GridViewDataTextColumn Caption="Group Name" FieldName="ITEM_GROUP_NAME" VisibleIndex="1" Width="100px">
                                        <Settings AllowGroup="True" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Item Name" FieldName="ITEM_NAME" VisibleIndex="2" Width="150px">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Quantity" FieldName="PURCHASE_QTY" VisibleIndex="3" Width="70px">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Uom" FieldName="UOM_NAME" VisibleIndex="4" Width="50px" />
                                    <dx:GridViewDataTextColumn Caption="Pur No" FieldName="PURCHASE_NO" VisibleIndex="3" Width="100px">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Purchase Date" FieldName="PURCHASE_DATE" VisibleIndex="3" Width="80px">
                                        <DataItemTemplate>
                                            <%#Eval("PURCHASE_DATE", "{0:dd-MMM-yyyy}")%>
                                        </DataItemTemplate>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Supplier" FieldName="SUP_NAME" VisibleIndex="8" Width="80px" />
                                    <dx:GridViewDataTextColumn Caption="SNS Type" FieldName="ITEM_SNS_NAME" VisibleIndex="9" Width="100px" />
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

    </div>
</asp:Content>
