<%@ Page Title="" Language="C#" ViewStateMode="Disabled" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="Mrr_Authorization.aspx.cs" Inherits="PG.Web.Report.INV.Mrr_Authorization" %>

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


        $(document).ready(function () {
            $('#' + txtGridPageNo).keydown(function (e) {
                if (e.keyCode == 13) {
                    e.preventDefault();
                    $('#' + btnGridPageGoTo).click();
                }
            });
        });


        function tbopen(key, userid) {
            key = key || '';
            var url = IForm.RootPath + "Inventory/INVNewLP_MRR_Authorize.aspx?id=" + key;

            if (IForm.PageMode == Enums.PageMode.InTab) {

                var tdata = new xtabdata();
                tdata.linktype = Enums.LinkType.Direct;
                tdata.id = 0;
                tdata.name = "Mrr Authorization";

                tdata.label = "Mrr Authorization";
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
           // return;

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
                        url: "../Inventory/Mrr_Authorization.aspx/Authorize_MRR",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        async: false,
                        data: JSON.stringify({ reqIdList: cObjList }),
                        success: function (result) {
                            var returnObj = JSON.parse(result.d);
                            if (returnObj.Status == "Success") {
                                alert('MRR authorized successfully.');
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
                alert('Please select MRR first to authorize.');
            }


        }





        // ]]>

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="dvPageContent" style="width: 100%; height: auto;">
        <div id="dvContentHeader" class="dvContentHeader">
            <div id="dvHeader" class="dvHeader">
                <asp:Label ID="lblHeader" CssClass="lblHeader" runat="server" Text="MRR Authorization"></asp:Label>
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
                        <td>&nbsp;&nbsp;<asp:Button ID="btnShow" runat="server" CssClass="buttonSearch" Style="padding-left: 22px;height:25px"
                            Text="Show Data" OnClick="btnShow_Click" />
                        </td>
                    </tr>


                </table>
            </div>
            <div id="dvControls" style="width: 100%;">
                <div id="dvControlsInner" class="groupBoxContainer boxShadow" style="width: 1150px">
                    <div id="dvGridContainer" style="width: 100%; height: auto; text-align: left;">

                        <div id="dvGrid" style="width: 100%; height: 500px; overflow: auto;">
                            <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="ASPxGridView1" runat="server" Width="100%" AutoGenerateColumns="False" KeyFieldName="MRR_NO">
                                <Settings VerticalScrollableHeight="250" ShowFooter="True" ShowGroupFooter="VisibleAlways" />

                                <TotalSummary>
                                    <dx:ASPxSummaryItem FieldName="Size" SummaryType="Sum" />
                                    <dx:ASPxSummaryItem FieldName="MRR_QTY" ShowInColumn="MRR Qty" DisplayFormat="Total:{0}" ShowInGroupFooterColumn="MRR Qty" SummaryType="Sum" />
                                </TotalSummary>

                                <GroupSummary>
                                    <dx:ASPxSummaryItem SummaryType="Count" />
                                    <dx:ASPxSummaryItem FieldName="MRR_QTY" ShowInGroupFooterColumn="MRR Qty" SummaryType="Sum" />
                                </GroupSummary>
                                <Columns>

                                    <dx:GridViewDataTextColumn Caption="Action" UnboundType="String" Width="55px" VisibleIndex="1">
                                        <DataItemTemplate>
                                            <dx:ASPxHyperLink ID="hyperLink" runat="server" OnInit="hyperLink_Init">
                                            </dx:ASPxHyperLink>
                                        </DataItemTemplate>
                                    </dx:GridViewDataTextColumn>

                                    <dx:GridViewDataTextColumn Caption="Select" FieldName="IS_SELECTED" VisibleIndex="2" Width="10px">
                                        <DataItemTemplate>
                                            <input type="hidden" id="hdnReqId" value="<%#Eval("MRR_ID")%>" />

                                            <input type="checkbox" name="cbIsSelected" class="cbIgr" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataTextColumn>

                                    <dx:GridViewDataTextColumn Caption="MRR No" FieldName="MRR_NO" VisibleIndex="3" Width="150px" />
                               <%--     <dx:GridViewDataTextColumn Caption="Group Name" FieldName="ITEM_GROUP_NAME" VisibleIndex="4" Width="100px">
                                        <Settings AllowGroup="True" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Item Code" FieldName="ITEM_CODE" VisibleIndex="5" Width="120px">
                                    </dx:GridViewDataTextColumn>--%>
                                    <dx:GridViewDataTextColumn Caption="Item Name" FieldName="ITEM_NAME" VisibleIndex="6" Width="180px">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Supplier" FieldName="SUP_NAME" VisibleIndex="7" Width="130px">
                                    </dx:GridViewDataTextColumn>

                                    <dx:GridViewDataTextColumn Caption="MRR Date" FieldName="MRR_DATE" VisibleIndex="4" Width="100px">
                                        <DataItemTemplate>
                                            <%#Eval("MRR_DATE", "{0:dd-MMM-yyyy}")%>
                                        </DataItemTemplate>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="PUR No" FieldName="PURCHASE_NO" VisibleIndex="9" Width="150px" />
                                <%--    <dx:GridViewDataTextColumn Caption="Type" FieldName="INV_TRANS_TYPE_NAME" VisibleIndex="10" Width="80px">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="MRR Qty" FieldName="MRR_QTY" VisibleIndex="11" Width="100px" />
                                    <dx:GridViewDataTextColumn Caption="Uom" FieldName="UOM_CODE" VisibleIndex="12" Width="80px" />--%>
                                    <dx:GridViewDataTextColumn Caption="MRR ID" FieldName="MRR_ID" Visible="false" Width="100px" />
                                </Columns>
                                <SettingsBehavior AllowFixedGroups="True" AutoExpandAllGroups="True" SortMode="Value" />
                                <SettingsPager NumericButtonCount="20">
                                    <PageSizeItemSettings Visible="true" Items="50,100" />
                                </SettingsPager>

                                <Settings ShowGroupPanel="true" ShowFilterBar="Visible" ShowFilterRow="True" ShowFilterRowMenu="True" ShowGroupedColumns="True" ShowHeaderFilterButton="True" ShowGroupButtons="True" />

                                <SettingsDataSecurity AllowInsert="false" AllowEdit="false" AllowDelete="false" />
                                <Styles>
                                    <Header BackColor="#2E913D" ForeColor="White">
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
                        <input type="button" id="btnCloseIGR" value="Authorize" class="buttonSave" onclick="SaveReplaceData()" style="width: 95px !important; background-repeat: no-repeat; text-align: right"  />
                        <asp:Button ID="btnSave" Visible="false" runat="server" Text="Authorize" Width="100px" CssClass="buttonSave checkRequired" AccessKey="s" OnClick="btnSave_Click"  OnClientClick="if ( ! UserDeleteConfirmation()) return false;"/>

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
