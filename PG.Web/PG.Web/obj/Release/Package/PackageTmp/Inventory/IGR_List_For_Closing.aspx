<%@ Page Title="" Language="C#" ViewStateMode="Disabled" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="IGR_List_For_Closing.aspx.cs" Inherits="PG.Web.Report.INV.IGR_List_For_Closing" %>

<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">
        // <!CDATA[

        var isPageResize = true;
        ContentForm.CalendarImageURL = "../image/calendar.png";




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
            var url = IForm.RootPath + "Inventory/INVNewIGR_For_Closing.aspx?id=" + key;

            if (IForm.PageMode == Enums.PageMode.InTab) {

                var tdata = new xtabdata();
                tdata.linktype = Enums.LinkType.Direct;
                tdata.id = 0;
                tdata.name = "IGR Closing";

                tdata.label = "IGR Closing";
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


        function SaveReplaceData() {
            var isselect = false;
            var cObjList = new Array();
            $(".cbIgr:checked").each(function () {
                if ($(this).is(':checked')) {
                    var id = $(this).parent().parent().find('#hdnReqId').val();
                    cObjList.push(id);
                    isselect = true;
                }
            });

            if (isselect) {
                var y = confirm('Are you sure?');
                if (y) {
                    $.ajax({
                        type: "POST",
                        url: "../Inventory/IGR_List_For_Closing.aspx/Close_IGR",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        async: false,
                        data: JSON.stringify({ reqIdList: cObjList }),
                        success: function (result) {
                            var returnObj = JSON.parse(result.d);
                            if (returnObj.Status == "Success") {
                                alert('IGR Closed successfully.');
                                $("[id*=btnShow]").click();
                            } else if (returnObj.Status == "Failed") {
                                alert(returnObj.ErrorMessage);
                            }

                        },
                        error: function (result) {
                            alert(result.responseText);
                        }

                    });
                }
            } else {
                alert('Please select IGR first.');
            }


        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="dvPageContent" style="width: 100%; height: auto;">
        <div id="dvContentHeader" class="dvContentHeader">
            <div id="dvHeader" class="dvHeader">
                <asp:Label ID="lblHeader" CssClass="lblHeader" runat="server" Text="IGR Closing"></asp:Label>
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
                        <td></td>


                        <td style="" align="left">&nbsp;
                        </td>
                        <td style="" align="left">&nbsp;</td>

                        <td style="" align="left">
                            <asp:Label ID="lblDeptName" runat="server" Text="IGR Department:"></asp:Label>&nbsp;
                        </td>
                        <td style="" align="left">
                            <asp:DropDownList ID="ddlDeptName" runat="server" Width="200" CssClass="dropDownList enableIsDirty" ViewStateMode="Enabled"></asp:DropDownList>
                        </td>
                        <td style="" align="left">
                            <asp:Label ID="lblDateFrom" runat="server" Text="Date From:" Font-Bold="true"></asp:Label>&nbsp;
                        </td>
                        <td style="" align="left">
                            <asp:TextBox ID="txtFromDate" runat="server" Width="80px" CssClass="textBox textDate dateParse"></asp:TextBox>
                        </td>
                        <td></td>
                        <td style="" align="left">
                            <asp:Label ID="lblToDate" runat="server" Text="Date To:" Font-Bold="true"></asp:Label>&nbsp;
                        </td>
                        <td style="" align="left">
                            <asp:TextBox ID="txtToDate" runat="server" Width="80px" CssClass="textBox textDate dateParse"></asp:TextBox>
                        </td>
                        <td>&nbsp;&nbsp;<asp:Button ID="btnShow" runat="server" CssClass="buttonSearch"
                            Text="Show Data" OnClick="btnShow_Click" />
                        </td>

                    </tr>


                </table>
            </div>
            <div id="dvControls" style="width: 100%;">
                <div id="dvControlsInner" class="groupBoxContainer boxShadow" style="width: 1150px">
                    <div id="dvGridContainer" style="width: 100%; height: auto; text-align: left;">

                        <div id="dvGrid" style="width: 100%; height: 500px; overflow: auto;">
                            <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="ASPxGridView1" runat="server" Width="100%" AutoGenerateColumns="False" KeyFieldName="REQ_NO">
                                <Settings VerticalScrollableHeight="250" ShowFooter="True" ShowGroupFooter="VisibleAlways" />
                                <TotalSummary>
                                    <dx:ASPxSummaryItem FieldName="Size" SummaryType="Sum" />
                                    <dx:ASPxSummaryItem FieldName="REQ_QNTY" ShowInColumn="REQ_QNTY" DisplayFormat="Total:{0}" ShowInGroupFooterColumn="REQ_QNTY" SummaryType="Sum" />
                                    <dx:ASPxSummaryItem FieldName="ISSUE_QNTY" ShowInColumn="ISSUE_QNTY" DisplayFormat="Total:{0}" ShowInGroupFooterColumn="ISSUE_QNTY" SummaryType="Sum" />
                                    <dx:ASPxSummaryItem FieldName="RCV_QNTY" ShowInColumn="RCV_QNTY" DisplayFormat="Total:{0}" ShowInGroupFooterColumn="RCV_QNTY" SummaryType="Sum" />
                                </TotalSummary>

                                <GroupSummary>
                                    <dx:ASPxSummaryItem SummaryType="Count" />
                                    <dx:ASPxSummaryItem FieldName="REQ_QNTY" ShowInGroupFooterColumn="REQ_QNTY" SummaryType="Sum" />
                                    <dx:ASPxSummaryItem FieldName="ISSUE_QNTY" ShowInGroupFooterColumn="ISSUE_QNTY" SummaryType="Sum" />
                                    <dx:ASPxSummaryItem FieldName="RCV_QNTY" ShowInGroupFooterColumn="RCV_QNTY" SummaryType="Sum" />
                                </GroupSummary>

                                <Columns>

                                    <dx:GridViewDataTextColumn Caption="Action" UnboundType="String" Width="55px" VisibleIndex="1">
                                        <DataItemTemplate>
                                            <dx:ASPxHyperLink ID="hyperLink" runat="server" OnInit="hyperLink_Init">
                                            </dx:ASPxHyperLink>
                                        </DataItemTemplate>
                                    </dx:GridViewDataTextColumn>

                                    <dx:GridViewDataCheckColumn Caption="Select" FieldName="IS_CLOSE" VisibleIndex="1">
                                        <DataItemTemplate>
                                            <%--<dx:ASPxHiddenField ID="<%#Eval("REQ_Id")%>" runat="server"></dx:ASPxHiddenField>--%>
                                            <input type="hidden" id="hdnReqId" value="<%#Eval("REQ_ID")%>" />

                                            <input type="checkbox" name="cbIsSelected" class="cbIgr" />
                                            <%--<dx:ASPxCheckBox ID="cbIsSelected" CssClass="cbIgr" ClientInstanceName="cbIsSelected" runat="server"></dx:ASPxCheckBox>--%>
                                        </DataItemTemplate>
                                    </dx:GridViewDataCheckColumn>
                                    <%--    <dx:GridViewDataTextColumn UnboundType="Boolean" Width="55px" VisibleIndex="1">
                                        <DataItemTemplate>
                                            <dx:ASPxCheckBox ID="cbIsSelected" runat="server"></dx:ASPxCheckBox>                                          
                                        </DataItemTemplate>
                                    </dx:GridViewDataTextColumn>--%>

                                    <dx:GridViewDataTextColumn Caption="Req No" FieldName="REQ_NO" VisibleIndex="1" Width="100px">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Department" FieldName="FROM_DEPARTMENT_NAME" VisibleIndex="2" Width="60px">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Group Name" FieldName="ITEM_GROUP_NAME" VisibleIndex="3" Width="100px">
                                        <Settings AllowGroup="True" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Item Name" FieldName="ITEM_NAME" VisibleIndex="4" Width="180px">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Req Date" FieldName="REQ_DATE" VisibleIndex="5" Width="130px">
                                        <DataItemTemplate>
                                            <%#Eval("REQ_DATE", "{0:dd-MMM-yyyy}")%>
                                        </DataItemTemplate>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="IGR Qty" FieldName="REQ_QNTY" VisibleIndex="6" Width="100px" />
                                    <dx:GridViewDataTextColumn Caption="ITC Qty" FieldName="ISSUE_QNTY" VisibleIndex="7" Width="100px" />
                                    <dx:GridViewDataTextColumn Caption="IRR Qty" FieldName="RCV_QNTY" VisibleIndex="8" Width="100px" />
                                    <dx:GridViewDataTextColumn Caption="REQ ID" FieldName="REQ_ID" VisibleIndex="9" Visible="false" />

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
            <table>
                <tr>
                    <td></td>
                    <td></td>
                    <td>
                        <asp:Button ID="btnSave" runat="server" Text="Close IGR" Width="100px" CssClass="buttonSave checkRequired" Visible="false" AccessKey="s" OnClick="btnSave_Click" OnClientClick="if ( ! UserDeleteConfirmation()) return false;" />
                        <input type="button" id="btnCloseIGR" value="Close IGR" class="buttonSave" onclick="SaveReplaceData()" style="width: 100px !important;  background-repeat: no-repeat; text-align: right" />
                    </td>
                    <td>
                        <input id="btnClose" type="button" runat="server" class="buttonClose" value="Close" onclick="if (ContentForm) { ContentForm.CloseForm(); }" />
                    </td>

                    <td></td>
                    <td></td>

                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
            </table>
        </div>

    </div>
</asp:Content>
