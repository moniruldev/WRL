<%@ Page Title="" Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" ViewStateMode="Disabled" CodeBehind="ProductionSolarChargePackingList.aspx.cs" Inherits="PG.Web.Production.ProductionSolarChargePackingList" %>

<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../javascript/jquery.ui.combogrid.js" type="text/javascript"></script>
    <script src="../javascript/jquery.attributeobserver.js" type="text/javascript"></script>
    <link href="../css/jquery.ui.combogrid.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">
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
            $('#' + txtGridPageNo).keydown(function (e) {
                if (e.keyCode == 13) {
                    e.preventDefault();
                    $('#' + btnGridPageGoTo).click();
                }
            });
        });


        function tbopen(key, type) {
            key = key || '';
            var url = "";
          
            //url = IForm.RootPath + "Production/ProductionSolarBatteryEntryPacking.aspx?id=" + key;
            //ProductionSolarBatteryEntryPackingWithBom
            url = IForm.RootPath + "Production/ProductionChargingPackingEntry.aspx?id=" + key;

            if (IForm.PageMode == Enums.PageMode.InTab) {

                var tdata = new xtabdata();
                tdata.linktype = Enums.LinkType.Direct;
                tdata.id = 0;
                tdata.name = "Charging Packing Entry";
                tdata.label = "Chargin Packing Entry";
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
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="dvPageContent" style="width: 100%; height: auto;">
        <div id="dvContentHeader" class="dvContentHeader">
            <div id="dvHeader" class="dvHeader_Prod">
                <asp:Label ID="lblHeader" CssClass="lblHeader" runat="server" Text="Production Charging Packing List"></asp:Label>
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
                <table style="width: 800px">
                    <tr>

                        <td align="right" class="auto-style2">
                            <asp:Label ID="Label8" runat="server" Text="From Date :" Width="100px"></asp:Label>
                        </td>
                        <td class="auto-style2" colspan="2">
                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="textBox textDate dateParse" Style="text-align: left;" Width="100px"></asp:TextBox>
                        </td>

                        <td align="right">
                            <asp:Label ID="Label2" runat="server" Text="To Date :" Width="100px"></asp:Label>
                        </td>
                        <td class="auto-style2" colspan="2">
                            <asp:TextBox ID="txtToDate" runat="server" CssClass="textBox textDate dateParse" Style="text-align: left;" Width="100px"></asp:TextBox>
                        </td>
                       


                    </tr>

                    <tr>

                        <td align="right" class="auto-style2">
                            <asp:Label ID="lblDEPT_ID" runat="server" Text="Department : " Width="100px"></asp:Label>
                        </td>
                        <td class="auto-style2" colspan="2">
                            <asp:DropDownList ID="ddlDEPT_ID" runat="server" CssClass="dropDownList required" Width="150px" ViewStateMode="Enabled" Enabled="False"></asp:DropDownList>

                        </td>
                        <td align="right" class="auto-style2">
                            <asp:Label ID="Label3" runat="server" Text="Auth Status :" Width="100px"></asp:Label>
                        </td>
                        <td class="auto-style2" colspan="3">
                            <asp:DropDownList ID="ddlAuthStatus" CssClass="dropDownList" Width="90px" runat="server" EnableViewState="true">
                                <asp:ListItem Value="">--All--</asp:ListItem>
                                <asp:ListItem Value="Y">Authorised</asp:ListItem>
                                <asp:ListItem Value="N">UnAothorised</asp:ListItem>
                            </asp:DropDownList>
                        </td>

                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>

                    <tr>
                        <td></td>
                        <td>
                            <%--<asp:Button ID="btnUpload" CssClass="buttonSearch" runat="server" OnClick="btnUpload_Click" Text="Load Data" />--%>
                            <asp:Button ID="Button1" runat="server" CssClass="buttonSearch" Style="padding-left: 22px;"
                                Text="Show Data" OnClick="btnUpload_Click" />
                            <asp:HiddenField ID="hdnCompanyID" runat="server" Value="0" />
                        </td>
                        <td>
                            <asp:Button ID="btnAddNew" runat="server" CssClass="buttonNew" Text="New Entry" Height="26px" />
                        </td>
                        <td></td>

                        <td></td>
                        <td>
                            <asp:HiddenField ID="hdnLoggedInUser" runat="server" Value="0" />
                        </td>
                    </tr>
                </table>
            </div>


            <div id="dvControls" style="width: 100%; height: auto; overflow: auto;">
                <div id="dvControlsInner" class="groupBoxContainer boxShadow" style="width: 1160px;">

                    <div id="dvGrid" style="width: 100%; height: auto; overflow: auto;">
                        <dx:ASPxGridView ID="grdPastingList" runat="server" EnableRowsCache="false" EnableViewState="false" AutoGenerateColumns="False" Width="98%" ClientInstanceName="grdPastingList" ViewStateMode="Disabled">
                            <Columns>
                                <dx:GridViewDataTextColumn Caption="Action" UnboundType="String" Width="50px" VisibleIndex="0">
                                    <DataItemTemplate>
                                        <dx:ASPxHyperLink ID="hyperLink" runat="server" OnInit="hyperLink_Init">
                                        </dx:ASPxHyperLink>
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>

                                <dx:GridViewDataTextColumn Caption="Loading NO" Name="lblPROD_NO" VisibleIndex="1" Width="130px" FieldName="PROD_NO">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="A.Status" FieldName="AUTH_STATUS" Width="60px" Name="txtAUTH_STATUS" VisibleIndex="2">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Entry By" Name="lblEDIT_BY_ID" Width="70px" VisibleIndex="2" FieldName="ENTRY_BY">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Dept. ID" Name="lblDEPT_ID" VisibleIndex="5" Visible="false" FieldName="DEPT_ID">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Shift Name" Name="lblSHIFT_NAME" VisibleIndex="3" Width="60px" FieldName="SHIFT_NAME">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Supervisor" Name="lblSUPERVISOR_NAME" VisibleIndex="4" Width="100px" FieldName="FULL_NAME">
                                </dx:GridViewDataTextColumn>

                                <dx:GridViewDataTextColumn Caption="PROD_ID" FieldName="PROD_ID" Name="hdnPROD_ID" Visible="false" VisibleIndex="1">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataDateColumn Caption="Pro.Date" FieldName="PRODUCTION_DATE" Width="70px" Name="lblPRODUCTION_DATE" VisibleIndex="5">
                                    <PropertiesDateEdit DisplayFormatString="dd-MMM-yyyy"></PropertiesDateEdit>
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataTextColumn Caption="Pro. Batch NO" FieldName="PROD_BATCH_NO" Width="80px" Name="txtPROD_BATCH_NO" VisibleIndex="6">
                                </dx:GridViewDataTextColumn>
                                <%--<dx:GridViewDataTextColumn Caption="Unload Prod No" FieldName="UN_LOAD_PROD_NO" Width="130px" Name="txtUnLoadProdNO" VisibleIndex="10">
                                </dx:GridViewDataTextColumn>--%>
                                <%--  <dx:GridViewDataTextColumn Caption="A.Status" FieldName="UN_LOAD_AUTH_STATUS" Width="60px" Name="txtUnLoadAuthStatus" VisibleIndex="11">
                                </dx:GridViewDataTextColumn>--%>
                                <%--  <dx:GridViewDataTextColumn Caption="Unload" UnboundType="String" Width="50px" VisibleIndex="9">
                                    <DataItemTemplate>
                                        <dx:ASPxHyperLink ID="hyperUnloadLink" runat="server" OnInit="hyperUnloadLink_Init">
                                        </dx:ASPxHyperLink>
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>--%>

                                <%--  <dx:GridViewDataTextColumn Caption="UN_LOADED_PROD_ID" FieldName="UN_LOADED_PROD_ID" Name="hdnUnLoaded_PROD_ID" Visible="false" VisibleIndex="13">
                                </dx:GridViewDataTextColumn>--%>

                                 <dx:GridViewDataTextColumn Visible="false" Caption="Loading Type" Name="lblUnloadingType" VisibleIndex="15" Width="100px" FieldName="IS_UNLOAD">
                                </dx:GridViewDataTextColumn>
                            </Columns>
                            <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
                            <SettingsSearchPanel Visible="True" />
                            <SettingsPager AlwaysShowPager="True" PageSize="20">
                            </SettingsPager>
                            <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="400" />
                            <Styles>
                                <Header CssClass="headerRow_Prod" HorizontalAlign="Center" Font-Bold="True" ForeColor="White" Font-Size="9pt">
                                </Header>
                                <AlternatingRow BackColor="#FFFFCC">
                                </AlternatingRow>
                            </Styles>
                        </dx:ASPxGridView>



                    </div>

                </div>
            </div>
        </div>
    </div>
</asp:Content>
