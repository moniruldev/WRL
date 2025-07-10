<%@ Page Title="" Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="ProductionOxidList_SPL.aspx.cs" Inherits="PG.Web.Production.ProductionOxidList_SPL" ViewStateMode="Disabled" %>
<%@ Register assembly="DevExpress.Web.v14.2, Version=14.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>
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


        function tbopen(key, userid) {
            key = key || '';

            //var url = IForm.RootPath + "Production/ProductionGridCastingEntry.aspx?id=" + key;
            
            var url = IForm.RootPath + "Production/ProductionOxideEntry_withBOM_SPL.aspx?id=" + key;
            //var url = IForm.RootPath + "Accounting/GeneralLedger/JournalFull.aspx?id=" + key;
            if (IForm.PageMode == Enums.PageMode.InTab) {

                var tdata = new xtabdata();
                tdata.linktype = Enums.LinkType.Direct;
                tdata.id = 0;
                tdata.name = "OXIDE Confirmation";
                //tdata.label = "User: " + userid;
                tdata.label = "OXIDE Confirmation";
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
                window.location = url;
            }
        }
   </script>

        
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <div id="dvPageContent" style="width: 100%; height: auto;">
          <div id="dvContentHeader" class="dvContentHeader">
                <div id="dvHeader" class="dvHeader_Prod">
                    <asp:Label ID="lblHeader" CssClass="lblHeader" runat="server" Text="OXIDE Production List"></asp:Label>
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
                     <table style="width : 900px">
                              <tr>

                        <td align="right" class="auto-style2">
                            <asp:Label ID="Label8" runat="server" Text="From Date :" Width="100px"></asp:Label>
                        </td>
                        <td class="auto-style2"  >
                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="textBox textDate dateParse" Style="text-align: left;" Width="100px" autofocus></asp:TextBox>
                        </td>
                        <td align="right">
                             <asp:Label ID="Label1" runat="server" Text="To Date :" Width="70px"></asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtToDate" runat="server" CssClass="textBox textDate dateParse" Style="text-align: left;" Width="100px" ></asp:TextBox></td>
                                  <td align="right">
                                        <asp:Label ID="lblAutho" runat="server" Text="Status :"></asp:Label>
                                  </td>
                                  <td>
                                      <asp:DropDownList ID="ddlAUTHO_STATUS" runat="server"  CssClass="dropDownList enableIsDirty">
                                                <asp:ListItem Selected="True" Value="">-All-</asp:ListItem>
                                                <asp:ListItem Value="Y">Authorized</asp:ListItem>
                                                <asp:ListItem Value="N">Un-Authorized</asp:ListItem>
                                            </asp:DropDownList>
                                  </td>
                    </tr>

                    <tr>
                        <td align="right">
                             <asp:Label ID="lblDEPT_ID" runat="server" Text="Department : " Width="100px"></asp:Label>
                        </td>
                            <td colspan="4">
                                <%--<asp:Button ID="btnUpload" CssClass="buttonSearch" runat="server" OnClick="btnUpload_Click" Text="Show Data" />--%>
                                  <asp:HiddenField ID="hdnCompanyID" runat="server" Value ="0" />
                            <asp:DropDownList ID="ddlDEPT_ID" runat="server" CssClass="dropDownList required" Width="250px" ViewStateMode="Enabled" Enabled="False"></asp:DropDownList>

                            </td>
                        

                       
                        <td> <asp:HiddenField ID="hdnLoggedInUser" runat="server" Value ="0" />

                             <asp:Button ID="btnUpload" runat="server" CssClass="buttonSearch" Style="padding-left: 22px;"
                                        Text="Show Data" OnClick="btnUpload_Click"  /> 
                        </td>
                        <td>
                            <asp:Button ID="btnAddNew" runat="server" CssClass="buttonNew" Text="New Oxide" Height="26px" />
                        </td>
                    </tr>
                         </table>
                    </div>


             <div id="dvControls" style="width: 100%;">
                <div id="dvControlsInner" class="groupBoxContainer boxShadow" style="width: 1000px">
                    <dx:aspxgridview ID="grdCastingList" runat="server" AutoGenerateColumns="False" Width="98%"  ClientInstanceName="grdPastingList" >
                                <Columns>
                                      <dx:GridViewDataTextColumn Caption="Action" UnboundType="String"  Width="25px" VisibleIndex="0">
                                        <DataItemTemplate>
                                            <dx:ASPxHyperLink ID="hyperLink"  runat="server" OnInit="hyperLink_Init">
                                            </dx:ASPxHyperLink>
                                        </DataItemTemplate>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Production NO" Name="lblPROD_NO" VisibleIndex="3"  Width="70px" FieldName="PROD_NO">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Entry By" Name="lblEDIT_BY_ID" Width="80px" VisibleIndex="4" FieldName="ENTRY_BY">
                                    </dx:GridViewDataTextColumn>
                                     <dx:GridViewDataDateColumn Caption="Entry Date" FieldName="ENTRY_DATE" Width="50px"   Name="lblENTRY_DATE" VisibleIndex="5">
                                         <PropertiesDateEdit DisplayFormatString="dd/MMM/yyyy hh:mm tt" ></PropertiesDateEdit>
                                    </dx:GridViewDataDateColumn>
                                    <dx:GridViewDataTextColumn Caption="Dept. ID" Name="lblDEPT_ID" VisibleIndex="5" Visible="false" FieldName="DEPT_ID">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Shift" Name="lblSHIFT_NAME" VisibleIndex="6" Width="40px" FieldName="SHIFT_NAME">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Shift Incharge" Visible="true" Name="lblSUPERVISOR_NAME" VisibleIndex="2" Width="100px" FieldName="FULL_NAME">
                                    </dx:GridViewDataTextColumn>
                                   
                                    <dx:GridViewDataTextColumn Caption="PROD_ID" FieldName="PROD_ID" Name="hdnPROD_ID" Visible="false" VisibleIndex="1">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataDateColumn Caption="Pro. Date" FieldName="PRODUCTION_DATE" Width="50px"   Name="lblPRODUCTION_DATE" VisibleIndex="3">
                                         <PropertiesDateEdit DisplayFormatString="dd-MMM-yyyy" ></PropertiesDateEdit>
                                    </dx:GridViewDataDateColumn>
                                   
                                      <dx:GridViewDataTextColumn Caption="Batch" FieldName="PROD_BATCH_NO" Width="30px" Name="txtPROD_BATCH_NO" VisibleIndex="1">
                                      </dx:GridViewDataTextColumn>
                                     <dx:GridViewDataTextColumn Caption="Status" FieldName="AUTH_STATUS" Width="20px" Name="txtAUTH_STATUS" VisibleIndex="8">
                                      </dx:GridViewDataTextColumn>
                                </Columns>
                                <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
                                <SettingsSearchPanel Visible="True"  />
                                <SettingsPager AlwaysShowPager="True" PageSize="20">
                                </SettingsPager>
                                <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="400"    />
                                <Styles>
                                    <Header CssClass="headerRow_Prod" HorizontalAlign="Center" Font-Bold="True" ForeColor="White" Font-Size="9pt">
                                         
                                    </Header>
                                    <AlternatingRow BackColor="#FFFFCC">
                                    </AlternatingRow>
                                </Styles>
                            </dx:aspxgridview>
                </div>
            </div>
             </div>
        </div>
</asp:Content>
